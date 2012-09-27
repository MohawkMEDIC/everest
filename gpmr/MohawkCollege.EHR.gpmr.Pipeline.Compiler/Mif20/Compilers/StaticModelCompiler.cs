/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: Justin Fyfe
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository;
using MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers;
using MohawkCollege.EHR.HL7v3.MIF.MIF20;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Compilers
{
    /// <summary>
    /// The static model compiler is responsible for the parsing and compilation of MIF2.0 static models into 
    /// the common object representation (COR) format for later translation into any one of the formats 
    /// supported by an available renderer.
    /// </summary>
    internal class StaticModelCompiler : IPackageCompiler
    {
        #region IPackageCompiler Members

        /// <summary>
        /// The static model that we are compiling into the COR format
        /// </summary>
        protected GlobalStaticModel staticModel;
        protected PackageRepository repository;
        protected Dictionary<string, MohawkCollege.EHR.gpmr.COR.TypeParameter> templateParameters = new Dictionary<string, MohawkCollege.EHR.gpmr.COR.TypeParameter>();

        /// <summary>
        /// Get the type of package that this compiler can compile
        /// </summary>
        public virtual Type PackageType
        {
            get { return typeof(GlobalStaticModel); }
        }

        /// <summary>
        /// Get or set the package that is to be compiled
        /// </summary>
        public virtual object Package
        {
            set { this.staticModel = value as GlobalStaticModel; }
        }

        /// <summary>
        /// Get or set the package repository that this compiler will read data from
        /// </summary>
        public object PackageRepository
        {
            get
            {
                return repository;
            }
            set
            {
                repository = value as PackageRepository;
            }
        }

        #endregion

        #region ICompiler Members

        /// <summary>
        /// Get or set the class repository in COR that this compiler will publish any class results to
        /// </summary>
        public MohawkCollege.EHR.gpmr.COR.ClassRepository ClassRepository
        {
            get;
            set;
        }

        /// <summary>
        /// Compile the actual package item
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public virtual void Compile()
        {
            System.Diagnostics.Trace.WriteLine(string.Format("Compiling static model package '{0}'...", staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT)), "debug");

            // Check if the package has already been "compiled"
            if (ClassRepository.ContainsKey(staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT)))
                return; // Already compiled

            // Create a COR namespace or package area based on the static model
            MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers.SubsystemParser.Parse(staticModel);

            // Static Model Derivation Suppliers
            Dictionary<string, Package> derivationSuppliers = new Dictionary<string, Package>();
            foreach (StaticModelDerivation smd in staticModel.DerivedFrom)
                derivationSuppliers.Add(smd.StaticModelDerivationId, this.repository.Find(smd.TargetStaticModel) as Package);

            // Compile all contained classes in the package
            foreach (ClassElement c in staticModel.OwnedClass)
            {
                if (c.Choice is MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat.Class) // todo: Finish this with CMET and Template Parameter
                {
                    ClassParser.ClassParameterInfo pi = new ClassParser.ClassParameterInfo();
                    pi.Class = c.Choice as MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat.Class;
                    pi.CompilerRepository = ClassRepository;
                    pi.DerivationSuppliers = derivationSuppliers; 
                    pi.MifContainer = this.staticModel;
                    pi.ScopedPackageName = staticModel.PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT);
                    ClassParser.Parse(pi);
                }
                else if (c.Choice is StaticModelClassTemplateParameter)
                {
                    TypeParameter parm = TypeParameterParser.Parse(c.Choice as StaticModelClassTemplateParameter);
                    templateParameters.Add(parm.ParameterName, parm);
                }
                else if (c.Choice is CommonModelElementRef)
                {
                    // Process a common model element reference
                    CommonModelElementRef cmetRef = c.Choice as CommonModelElementRef;
                    
                }
            }

            // Get a ref to the current sub-system
            MohawkCollege.EHR.gpmr.COR.SubSystem ss = (ClassRepository[staticModel.PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT)]
                as MohawkCollege.EHR.gpmr.COR.SubSystem);

            // Compile all contained associations
            foreach(Association a in staticModel.OwnedAssociation)
                ParseAssociation(a, derivationSuppliers);

            // Process entry points
            foreach (EntryPoint ep in staticModel.OwnedEntryPoint)
            {
                // Find the class
                MohawkCollege.EHR.gpmr.COR.Class cls = ss.FindClass(ep.ClassName);

                // The ProcStack keeps track of the properties the CascadeGenerics is 
                // processing so that we don't get a stack overflow
                Stack<string> procStack = new Stack<string>();
                // Process any generic providers that need to be cascaded down to child properties 
                // This is required for interactions to function properly.
                CascadeGenerics(cls, procStack);

                // Assign as the ep
                if (ss.EntryPoint == null) ss.EntryPoint = new List<MohawkCollege.EHR.gpmr.COR.Class>(); // Have to do this here so we don't have E/P array with 0 elements
                ss.EntryPoint.Add(cls);
            }

        }
        
        

        
       /// <summary>
       /// Cascade generic parameters to the class if any of the properties are concrete classes
       /// that require a generic parameter
       /// </summary>
       /// <param name="cls">The class to propegate to</param>
       /// <param name="CurrentlyProcessing"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        private void CascadeGenerics(MohawkCollege.EHR.gpmr.COR.Class cls, Stack<string> CurrentlyProcessing)
        {
            if (cls == null || CurrentlyProcessing.Contains(cls.Name)) // Something to process and we aren't already processing it
                return;

            // Push on the current class
            CurrentlyProcessing.Push(cls.Name);

            foreach(ClassContent cc in cls.Content)
                if (cc is Property)
                {
                    
                    // Now set the generics of the class to this
                    if ((cc as Property).Type.Class == null) continue; // Base datatype so don't worry
                    
                    CascadeGenerics((cc as Property).Type.Class, CurrentlyProcessing);

                    foreach (TypeParameter p in (cc as Property).Type.Class.TypeParameters ?? new List<TypeParameter>())
                    {
                        cls.AddTypeParameter(p);
                        (cc as Property).Type.AddGenericSupplier(p.ParameterName, p);
                    }
                }

            CurrentlyProcessing.Pop(); // Pop off the current class
        }

        #endregion

        //DOC: Documentation Required
        /// <summary>
        /// Create a type reference from an association end
        /// </summary>
        /// <param name="ae"></param>
        /// <param name="cc"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        private TypeReference CreateTypeReference(AssociationEnd ae, ClassContent cc)
        {
            
            // Resolve CMET
            ClassElement cel = staticModel.OwnedClass.Find(o => (o.Choice is CommonModelElementRef) && (o.Choice as CommonModelElementRef).Name == ae.ParticipantClassName);
            if (cel != null) // It is a CMET - Note: This is where late binding may occur.. 
                ae.ParticipantClassName = (cel.Choice as CommonModelElementRef).CmetName ?? (cel.Choice as CommonModelElementRef).Name; // Resolve to CMET

            // The type of this end is the type of the association
            TypeReference tr = new TypeReference();
            tr.Container = cc;
            tr.MemberOf = ClassRepository;

            #region Type Reference
            // IS the traversable association referencing a CMET type?
            // if it is, then we require some extra processing to de-reference the CMET
            if (ClassRepository.ContainsKey(ae.ParticipantClassName) &&
                    ClassRepository[ae.ParticipantClassName] is CommonTypeReference)
            {
                // Get the CMET references (which is a CTR in COR)
                CommonTypeReference ctr = (ClassRepository[ae.ParticipantClassName] as CommonTypeReference);
                tr = ctr.Class; // Assign the class type reference
                tr.MemberOf = ClassRepository;

                // Process this class?
                if (!ClassRepository.ContainsKey(tr.Name))
                    PackageParser.ParseClassFromPackage(tr.Name, repository, ClassRepository);

                if (cc is Property)
                {
                    (cc as Property).FixedValue = ctr.ClassifierCode; // Assign a fixed classifier code
                    (cc as Property).Documentation = ctr.Documentation; // Assign the documentation
                }
            }
            else if (cel != null) // Bad CMET ref
            {
                // JF - Bug processing payload models
                //// the --ignore-cmet flag
                if (MifCompiler.hostContext.Mode == Pipeline.OperationModeType.Quirks)
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("can't make type reference to CMET '{0}' as it wasn't found in the classes. The user has specified the --quirks flag so this error won't be classified as fatal...", (cel.Choice as CommonModelElementRef).CmetName), "quirks");
                    tr.Name = null;
                }
                else 
                    throw new InvalidOperationException(string.Format("can't make type reference to CMET '{0}' as it wasn't found anywhere in the repository ({1}).", (cel.Choice as CommonModelElementRef).CmetName, staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT)));
            }
            else
            {
                if (templateParameters.ContainsKey(ae.ParticipantClassName))
                    tr.Name = ae.ParticipantClassName;
                else
                    tr.Name = string.Format("{0}.{1}", staticModel.PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT),
                        ae.ParticipantClassName);
            }
            #endregion



            return tr;

        }

        /// <summary>
        /// Parse a static model association end
        /// </summary>
        /// <param name="asc">The association end to parse</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void ParseAssociation(Association asc, Dictionary<string, Package> derivationSuppliers)
        {

            // There are two scenarios
            // A: Two traversable connections
            // B: A traversable and non traversable end
            

            // Case A: Two traversable connections, this means that the connection appears in both classes
            if (asc.Ends[0] is AssociationEnd && asc.Ends[1] is AssociationEnd)
            {
                // Make processing a little easier
                AssociationEnd[] ends = new AssociationEnd[] { 
                    asc.Ends[0] as AssociationEnd,
                    asc.Ends[1] as AssociationEnd
                };

                // Loop so we write the code once
                for (int i = 0; i < 2; i++)
                {
                    Property p = new Property();
                    p.Name = ends[i].Name;

                    // Business Name
                    foreach(BusinessName bn in ends[i].BusinessName ?? new List<BusinessName>())
                        if(bn.Language == MifCompiler.Language || bn.Language == null)
                            p.BusinessName = bn.Name;

                    // The multiplicity of the opposing end influences the property on this end of the 
                    // association
                    p.MinOccurs = ends[1 - i].MinimumMultiplicity;
                    p.MaxOccurs = ends[1 - i].MaximumMultiplicity;
                    p.Conformance = ends[1 - i].IsMandatory ? ClassContent.ConformanceKind.Mandatory :
                        ends[1 - i].Conformance == ConformanceKind.Required ? ClassContent.ConformanceKind.Required :
                        ends[1 - i].MinimumMultiplicity == "1" ? ClassContent.ConformanceKind.Populated :
                        ClassContent.ConformanceKind.Optional;

                    // The type of this end is the type of the association
                    p.Type = CreateTypeReference(ends[i], p);

                    if (p.Type.Name == null)
                    {
                        if (p.Documentation == null) p.Documentation = new MohawkCollege.EHR.gpmr.COR.Documentation();
                        if (p.Documentation.Description == null) p.Documentation.Description = new List<string>();

                        p.Documentation.Description.Add(String.Format("GPMR: Association to type '{0}' was ignored and set as nothing as '{0}' could not be found. You should consider revising this", ends[i].ParticipantClassName));
                    }

                    // Traversable association
                    p.PropertyType = Property.PropertyTypes.TraversableAssociation;

                    // Annotations
                    if (asc.Annotations != null)
                        p.Documentation = MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers.DocumentationParser.Parse(asc.Annotations.Documentation);

                    // Find the class this end belongs in
                    if (!ClassRepository.ContainsKey(string.Format("{0}.{1}", staticModel.PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT),
                        ends[1 - i].ParticipantClassName)))
                        throw new Exception(string.Format("Can't bind property '{0}' to class '{1}'... Class does not exist", p.Name, ends[1 - i].ParticipantClassName));

                    // Set derivation
                    p.DerivedFrom = asc;
                    p.SortKey = asc.SortKey;

                    try
                    {
                        if (ends[i].DerivedFrom != null)
                        {
                            p.Realization = new List<ClassContent>();
                            foreach (var dei in ends[i].DerivedFrom)
                            {
                                MohawkCollege.EHR.gpmr.COR.Feature f = null;
                                if (!ClassRepository.TryGetValue(string.Format("{0}.{1}", derivationSuppliers[dei.StaticModelDerivationId].PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : derivationSuppliers[dei.StaticModelDerivationId].PackageLocation.ToString(MifCompiler.NAME_FORMAT),
                                    dei.ClassName), out f))
                                    System.Diagnostics.Trace.WriteLine(String.Format("Can't find derivation class '{0}' for association '{1}'", dei.ClassName, ends[i].Name), "debug");
                                else
                                {
                                    ClassContent cc = (f as MohawkCollege.EHR.gpmr.COR.Class).GetFullContent().Find(o => o.Name == dei.AssociationEndName);
                                    p.Realization.Add(cc);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(String.Format("Cannot append derivation information to {0} (reason:{1})", ends[i].Name, ex.ToString()), "error");
                    }

                    // Add to repository
                    (ClassRepository[string.Format("{0}.{1}", staticModel.PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT),
                        ends[1 - i].ParticipantClassName)] as MohawkCollege.EHR.gpmr.COR.Class).AddContent(p);

                }
            }
            else // Case 2: A traversable and non-traversable connection, 
            {
                AssociationEnd ae = (asc.Ends[0] as AssociationEnd) ?? (asc.Ends[1] as AssociationEnd);
                NonTraversableAssociationEnd ntae = (asc.Ends[0] as NonTraversableAssociationEnd) ?? (asc.Ends[1] as NonTraversableAssociationEnd);

                // Start to process the property.
                ClassContent cc;
                if (ae.ChoiceItem != null && ae.ChoiceItem.Count > 0)
                    cc = new Choice();
                else
                    cc = new Property();

                cc.Name = ae.Name;
                
                // Business Name
                foreach(BusinessName bn in ae.BusinessName ?? new List<BusinessName>())
                    if(bn.Language == MifCompiler.Language || bn.Language == null)
                        cc.BusinessName = bn.Name;
                
                // The multiplicity of the opposing end influences the property on this end of the 
                // association
                cc.MinOccurs = ae.MinimumMultiplicity;
                cc.MaxOccurs = ae.MaximumMultiplicity;
                cc.Conformance = ae.IsMandatory ? ClassContent.ConformanceKind.Mandatory :
                    ae.Conformance == ConformanceKind.Required  && ae.MinimumMultiplicity == "1" ? ClassContent.ConformanceKind.Populated :
                    ae.Conformance == ConformanceKind.Required ? ClassContent.ConformanceKind.Required :
                    ClassContent.ConformanceKind.Optional;

                #region Bind To Class
                // Find the class on the traversable end, we'll need to append the property to it
                if (!ClassRepository.ContainsKey(string.Format("{0}.{1}", staticModel.PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT),
                    ntae.ParticipantClassName)))
                    System.Diagnostics.Trace.WriteLine(string.Format("Can't bind property '{0}' to class '{1}'... Class does not exist", cc.Name, ntae.ParticipantClassName), "error");
                    //throw new Exception(string.Format("Can't bind property '{0}' to class '{1}'... Class does not exist", p.Name, ae.ParticipantClassName));
                else // Append the property to the class
                {
                    MohawkCollege.EHR.gpmr.COR.Class cls = ClassRepository[string.Format("{0}.{1}", staticModel.PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT),
                    ntae.ParticipantClassName)] as MohawkCollege.EHR.gpmr.COR.Class;
                    cls.AddContent(cc);
                    // Add template parameter
                    if (templateParameters.ContainsKey(ae.ParticipantClassName))
                        cls.AddTypeParameter(templateParameters[ae.ParticipantClassName]);
                }
                #endregion

                // Choice or property?
                if (cc is Property)
                {
                    Property p = cc as Property;
                    p.Type = CreateTypeReference(ae, p);
                    if (p.Type.Name == null)
                    {
                        if (p.Documentation == null) p.Documentation = new MohawkCollege.EHR.gpmr.COR.Documentation();
                        if (p.Documentation.Description == null) p.Documentation.Description = new List<string>();
                        p.Documentation.Description.Add(String.Format("GPMR: Association to type '{0}' was ignored and set as nothing as '{0}' could not be found. You should consider revising this", ae.ParticipantClassName));
                    }
                    // Traversable association
                    p.PropertyType = Property.PropertyTypes.TraversableAssociation;
                }
                else // Choice
                {
                    Choice chc = cc as Choice;
                    chc.MemberOf = ClassRepository;
                    chc.Content = new List<ClassContent>();

                    // Get a type reference to the CMET or main type to be used
                    TypeReference trf = CreateTypeReference(ae, cc);

                    // Cannot find association
                    if (trf.Name == null)
                    {
                        if (chc.Documentation == null) chc.Documentation = new MohawkCollege.EHR.gpmr.COR.Documentation();
                        if (chc.Documentation.Description == null) chc.Documentation.Description = new List<string>();

                        chc.Documentation.Description.Add(String.Format("GPMR: Association to type '{0}' was ignored and set as nothing as '{0}' could not be found. You should consider revising this", ae.ParticipantClassName));
                    }

                    // Warn
                    if (trf.Class == null)
                        throw new InvalidOperationException(String.Format("Cannot make association to class '{0}' as it was not defined!", ae.ParticipantClassName));
                    if (ae.ChoiceItem.Count != trf.Class.SpecializedBy.Count)
                        System.Diagnostics.Trace.WriteLine(string.Format("Number of choices on property does not match the number of child classes for its data type for association '{0}'", cc.Name), "warn");

                    chc.Content.AddRange(ProcessAssociations(ae, ae.ChoiceItem, trf, chc));

                    /*
                    // Specializations
                    List<TypeReference> specializations = new List<TypeReference>(trf.Class.SpecializedBy);

                    // Flatten choice members
                    for (int i = 0; i < ae.ChoiceItem.Count ; i++)
                        if (ae.ChoiceItem[i].Specialization != null && ae.ChoiceItem[i].Specialization.Count > 0)
                        {
                            AssociationEndSpecialization aes = ae.ChoiceItem[i]; // Get local reference
                            // Remove redundant data
                            ae.ChoiceItem.RemoveAt(i); // Remove the redundant choice item
                            i--; // redo this item
                            // is this a cmet?
                            if (ClassRepository.ContainsKey(aes.ClassName) && ClassRepository[aes.ClassName] is CommonTypeReference)
                            {
                                specializations.RemoveAll(o => o.Name == (ClassRepository[aes.ClassName] as CommonTypeReference).Class.Name);
                                specializations.AddRange((ClassRepository[aes.ClassName] as CommonTypeReference).Class.Class.SpecializedBy);
                            }
                            
                            ae.ChoiceItem.AddRange(aes.Specialization); // Now add the choices
                        }

                    int ip = 0;

                    // Add choice members
                    foreach (AssociationEndSpecialization aes in ae.ChoiceItem)
                    {
                        Property p = new Property();

                        // Now, construct the properties from the CMET entries
                        // Try ...

                        var rClassName = specializations.Find(o => o.Class != null && o.Class.Name.Equals(aes.ClassName));

                        // Determine if this is a CMET
                        if (ClassRepository.ContainsKey(aes.ClassName) && ClassRepository[aes.ClassName] is CommonTypeReference)
                            p.Type = (ClassRepository[aes.ClassName] as CommonTypeReference).Class;
                        else if (rClassName != null) // Try using the inheritence method
                            p.Type = rClassName;
                        else if (ClassRepository.ContainsKey(String.Format("{0}.{1}", staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT), aes.ClassName)))
                            p.Type = ((ClassRepository[String.Format("{0}.{1}", staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT), aes.ClassName)] as MohawkCollege.EHR.gpmr.COR.Class).CreateTypeReference());
                        else
                        {
                            System.Diagnostics.Trace.WriteLine(string.Format("Class '{2}' of CMET '{1}' could not be found or was not processed. The processing of the property '{0}' in '{3}.{4}' will NOT continue", cc.Name, aes.ClassName, ae.ParticipantClassName, staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT), ntae.ParticipantClassName), "error");
                            break;
                        }

                        p.PropertyType = Property.PropertyTypes.TraversableAssociation;

                        // Fix bug with optional choice
                        p.MinOccurs = chc.MinOccurs;
                        p.MaxOccurs = chc.MaxOccurs;
                        p.Conformance = chc.Conformance;

                        p.DerivedFrom = aes;
                        p.Documentation = p.Type.ClassDocumentation;

                        p.MemberOf = ClassRepository;
                        p.Container = chc;

                        // Traversal names
                        p.Name = aes.TraversalName;

                        chc.Content.Add(p);

                        ip++;
                    }
                    */
                    
                }
                
                
                // Annotations
                if (asc.Annotations != null)
                    cc.Documentation = MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers.DocumentationParser.Parse(asc.Annotations.Documentation);

                // Set derivation
                cc.DerivedFrom = asc;
                cc.SortKey = ae.SortKey;

                try
                {
                    if (ae.DerivedFrom != null)
                    {
                        cc.Realization = new List<ClassContent>();
                        foreach (var dei in ae.DerivedFrom)
                        {
                            MohawkCollege.EHR.gpmr.COR.Feature ss = null;
                            Package derivationPkg = null;
                            if (!derivationSuppliers.TryGetValue(dei.StaticModelDerivationId, out derivationPkg) || derivationPkg == null)
                            {
                                continue;
                            }

                            // Has the package been compiled?
                            if (!ClassRepository.TryGetValue(string.Format("{0}", derivationPkg.PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : derivationPkg.PackageLocation.ToString(MifCompiler.NAME_FORMAT)), out ss))
                            {
                                // Attempt to parse 
                                PackageParser.Parse(derivationPkg.PackageLocation.ToString(MifCompiler.NAME_FORMAT), derivationPkg.MemberOfRepository, ClassRepository);
                                // Ditch if still can't find
                                if (!ClassRepository.TryGetValue(string.Format("{0}", derivationPkg.PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : derivationPkg.PackageLocation.ToString(MifCompiler.NAME_FORMAT)), out ss))
                                    System.Diagnostics.Trace.WriteLine(String.Format("Can't find derivation class '{0}' for association '{1}' (derivation supplier {2})", dei.ClassName, dei.AssociationEndName, dei.StaticModelDerivationId), "warn");
                            }

                            // Feature was found
                            var f = (ss as MohawkCollege.EHR.gpmr.COR.SubSystem).FindClass(dei.ClassName);
                            if (f != null)
                            {
                                // Realized Class content
                                ClassContent rcc = f.GetFullContent().Find(o => o.Name == dei.AssociationEndName);
                                cc.Realization.Add(rcc);
                            }
                            else
                                System.Diagnostics.Trace.WriteLine(String.Format("Can't find derivation class '{0}' for association '{1}' (derivation supplier {2})", dei.ClassName, dei.AssociationEndName, dei.StaticModelDerivationId), "warn");

                        }
                    }
                }
               catch (Exception ex)
                {
                    Trace.WriteLine(String.Format("Cannot append derivation information to {0} (reason:{1})", ae.Name, ex.ToString()), "error");
                }

                (cc.Container as MohawkCollege.EHR.gpmr.COR.Class).Content.Sort(new ClassContent.Comparator());

            }
        }

        /// <summary>
        /// Process Associations
        /// </summary>
        private List<ClassContent> ProcessAssociations(AssociationEnd assoc, List<AssociationEndSpecialization> choiceItems, TypeReference typeReference, Choice container)
        {
            
            // Return value
            List<ClassContent> retVal = new List<ClassContent>();
            int nInher = 0;

            // Process the choice items
            foreach (var choice in choiceItems)
            {
                // Attempt to find a class
                var candidateType = typeReference.Class.SpecializedBy.FindAll(o => o.Class.Name.Equals(choice.ClassName));

                Property p = new Property();

                // Did the candidate type find?

                // Found exactly one candidate type with name 
                if (candidateType.Count == 1) 
                    p.Type = candidateType[0];
                // Sometimes we find more than one class with the same name that are specializations
                // in this case we need to correlate the numeric value
                else if (typeReference.Class.SpecializedBy.Count > nInher && typeReference.Class.SpecializedBy[nInher].Class.Name == choice.ClassName)
                    p.Type = typeReference.Class.SpecializedBy[nInher];
                // Otherwise we are referencing a CMET in which case we need to find it
                else if (ClassRepository.ContainsKey(choice.ClassName) && ClassRepository[choice.ClassName] is CommonTypeReference) // The candidate type is a CMET
                    p.Type = (ClassRepository[choice.ClassName] as CommonTypeReference).Class;
                // Last ditch attempt is to look for a local class with the same name
                else if (ClassRepository.ContainsKey(String.Format("{0}.{1}", staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT), choice.ClassName))) // Local class reference
                    p.Type = ((ClassRepository[String.Format("{0}.{1}", staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT), choice.ClassName)] as MohawkCollege.EHR.gpmr.COR.Class).CreateTypeReference());
                else
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("Association class '{0}' in traversal '{1}' is not a sub-class of '{2}'. The association will NOT be processed", choice.ClassName, assoc.Name, typeReference), "error");
                    break;
                }

                nInher++;

                p.PropertyType = Property.PropertyTypes.TraversableAssociation;

                // Fix bug with optional choice
                p.MinOccurs = container.MinOccurs;
                p.MaxOccurs = container.MaxOccurs;
                p.Conformance = container.Conformance;

                p.DerivedFrom = choice;
                p.Documentation = p.Type.ClassDocumentation;

                p.MemberOf = ClassRepository;
                p.Container = container;

                // Traversal names
                p.Name = choice.TraversalName;

                retVal.Add(p);

                // Specializations
                if (choice.Specialization != null && choice.Specialization.Count > 0)
                {
                    retVal.AddRange(ProcessAssociations(assoc, choice.Specialization, p.Type, container));
                }
                
            }

            return retVal;
        }
    }
}
