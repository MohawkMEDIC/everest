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
using MohawkCollege.EHR.HL7v3.MIF.MIF20;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Compilers;
using System.Linq;
using System.Diagnostics;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers
{
    /// <summary>
    /// Responsible for parsing MIF 2.0 class structures into the corresponding COR class structures
    /// </summary>
    internal class ClassParser
    {

        /// <summary>
        /// Contains data required for the processing of a MIF 2.0 class into a COR class.
        /// </summary>
        public struct ClassParameterInfo
        {
            /// <summary>
            /// The class that is being processed
            /// </summary>
            public ClassBase Class;
            /// <summary>
            /// Derivation suppliers to be used in this class conversion process
            /// </summary>
            public Dictionary<string, Package> DerivationSuppliers;
            /// <summary>
            /// The name of the MIF 2.0 package this class belongs to
            /// </summary>
            public string ScopedPackageName;
            /// <summary>
            /// The class repository currently being constructed. Indicates where compiled sub-classes should be placed
            /// </summary>
            public ClassRepository CompilerRepository;
            /// <summary>
            /// The MIF package being parsed
            /// </summary>
            public Package MifContainer;
            /// <summary>
            /// Forces the type to have this base class
            /// </summary>
            public TypeReference EnforcedBaseClass { get; set; }
        }

        /// <summary>
        /// Parses common class base data from the MIF 2.0 structure
        /// </summary>
        /// <param name="cls">The classbase to parse</param>
        /// <param name="vocabularyBindingRealm">The vocabulary binding realm that all suppliant codes are bound with</param>
        /// <returns>A parsed COR class object</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        internal static MohawkCollege.EHR.gpmr.COR.Class Parse(ClassBase cls, string vocabularyBindingRealm, ClassRepository cr, Dictionary<string, Package> derivationSuppliers)
        {
            // Return value
            MohawkCollege.EHR.gpmr.COR.Class retVal = new MohawkCollege.EHR.gpmr.COR.Class();

            // Name of the class
            retVal.Name = cls.Name;

            // Identify where this class came from
            retVal.DerivedFrom = cls;

            // Set the business name
            foreach (BusinessName bn in cls.BusinessName)
                if (bn.Language == MifCompiler.Language || bn.Language == null)
                    retVal.BusinessName = bn.Name;

            // Set the documentation & Copyright
            retVal.Documentation = new MohawkCollege.EHR.gpmr.COR.Documentation();
            if(cls.Annotations != null)
                retVal.Documentation = DocumentationParser.Parse(cls.Annotations.Documentation);
            if (cls.Container.Header.Copyright != null)
                retVal.Documentation.Copyright = string.Format("Copyright (C){0}, {1}", cls.Container.Header.Copyright.Year,
                    cls.Container.Header.Copyright.Owner);

            // Set abstractiveness of the class
            retVal.IsAbstract = cls.IsAbstract;

            // Sort the properties and add them to the class
            cls.Attribute.Sort(new ClassAttribute.Comparator());
            retVal.Content = new List<ClassContent>();
            foreach (ClassAttribute ca in cls.Attribute)
            {
                MohawkCollege.EHR.gpmr.COR.Property prp = PropertyParser.Parse(ca, vocabularyBindingRealm, cr,derivationSuppliers);
                prp.Container = retVal;
                retVal.AddContent(prp);
            }

            // Set sort key
            retVal.SortKey = cls.SortKey;

            // Return
            return retVal;
        }

        /// <summary>
        /// Parse a MIF 2.0 class element into COR representation class
        /// </summary>
        /// <param name="classInfo">The class info structure to parse</param>
        /// <returns>The compiled COR representation of the class</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        internal static MohawkCollege.EHR.gpmr.COR.Class Parse(ClassParameterInfo classInfo)
        {

            // Has this been processed?
            if(classInfo.CompilerRepository.ContainsKey(string.Format("{0}.{1}", classInfo.ScopedPackageName, classInfo.Class.Name)))
                return null;

            // Get the vocabulary realm that this package is bound to
            string vocabularyRealm = classInfo.MifContainer is StaticModel && (classInfo.MifContainer as StaticModel).ImportedVocabularyModelPackage != null ? (classInfo.MifContainer as StaticModel).ImportedVocabularyModelPackage.Realm : null;

            // Parse the common (class base) portion of the classes
            MohawkCollege.EHR.gpmr.COR.Class retVal = Parse(classInfo.Class as ClassBase, vocabularyRealm, classInfo.CompilerRepository, classInfo.DerivationSuppliers);

            // Base class
            retVal.BaseClass = classInfo.EnforcedBaseClass;

            // Containing subsystem
            retVal.ContainerPackage = classInfo.CompilerRepository[classInfo.ScopedPackageName] as MohawkCollege.EHR.gpmr.COR.SubSystem;

            #region Specializations

            retVal.SpecializedBy = new List<TypeReference>();

            // Iterate through each child class and process where neccessary
            foreach (ClassGeneralization cg in (classInfo.Class as MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat.Class).SpecializationChild)
            {
                TypeReference tr = new TypeReference();
                tr.Name = string.Format("{0}.{1}", classInfo.ScopedPackageName, cg.ChildClassName); // Make a ref to the child class
                tr.MemberOf = classInfo.CompilerRepository;

                bool isCMET = false;
                // Get the child class
                if (classInfo.MifContainer is GlobalStaticModel)
                {
                    // Get the CMET reference from the MIF class hierarchy
                    ClassElement ce = (classInfo.MifContainer as GlobalStaticModel).OwnedClass.Find(delegate(ClassElement c)
                    {
                        if (c != null && c.Choice != null && c.Choice is CommonModelElementRef)
                            return (c.Choice as CommonModelElementRef).Name == cg.ChildClassName;
                        else
                            return false;
                    });

                    string cmetName = ce != null ? (ce.Choice as CommonModelElementRef).CmetName ?? (ce.Choice as CommonModelElementRef).Name : "";

                    // Determine if the reference is a cmet and gathe t
                    isCMET = cmetName != null && ce != null && classInfo.CompilerRepository.ContainsKey(cmetName) &&
                        classInfo.CompilerRepository[cmetName] is CommonTypeReference;

                    if (isCMET) // A CMET was successfully resolved
                        tr = (classInfo.CompilerRepository[cmetName] as CommonTypeReference).Class;
                    else if (ce != null && MifCompiler.cmetBindings.ContainsKey(cmetName)) // A CMET was not resolved, but a binding exists!
                    {
                        string boundModel = MifCompiler.cmetBindings[cmetName];
                        MohawkCollege.EHR.gpmr.COR.Feature boundSubSystem = null;
                        if (classInfo.CompilerRepository.TryGetValue(boundModel, out boundSubSystem))
                            tr = (boundSubSystem as MohawkCollege.EHR.gpmr.COR.SubSystem).EntryPoint[0].CreateTypeReference();
                        else
                            PackageParser.Parse(boundModel, classInfo.MifContainer.MemberOfRepository, classInfo.CompilerRepository);
                    }
                    else if (ce != null) // MIF References a CMET but one was never processed? This is odd!
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format("CMET '{0}' was never processed, therefore CommonModelElementRef can't be processed in file '{1}'", cmetName, classInfo.ScopedPackageName), "warn");
                        continue;
                    }
                }

                // The child class has not been processed yet
                if (!classInfo.CompilerRepository.ContainsKey(tr.Name))
                {
                    if (isCMET) // Explicit, have to process another model
                    {
                        PackageParser.ParseClassFromPackage(tr.Name, classInfo.MifContainer.MemberOfRepository, classInfo.CompilerRepository);
                        // Assign the base class 
                        //if ((classInfo.CompilerRepository[tr.Name] as MohawkCollege.EHR.gpmr.COR.Class).BaseClass != null)
                        //    System.Diagnostics.Debugger.Break();
                    }
                    else
                    {
                        ClassParameterInfo parm2 = new ClassParameterInfo();
                        parm2.Class = (classInfo.MifContainer as StaticModel).OwnedClass.Find(o => (o.Choice is MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat.Class && string.Format("{0}.{1}", classInfo.ScopedPackageName, (o.Choice as MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat.Class).Name) == tr.Name)).Choice as ClassBase;
                        parm2.CompilerRepository = classInfo.CompilerRepository;
                        parm2.DerivationSuppliers = classInfo.DerivationSuppliers;
                        parm2.MifContainer = classInfo.MifContainer;
                        parm2.ScopedPackageName = classInfo.ScopedPackageName;
                        parm2.EnforcedBaseClass = retVal.CreateTypeReference();
                        ClassParser.Parse(parm2);

                    }
                }


                // Add to specialization list
                MohawkCollege.EHR.gpmr.COR.Feature f = null;
                if (classInfo.CompilerRepository.TryGetValue(tr.Name, out f))
                {
                    retVal.SpecializedBy.Add((f as MohawkCollege.EHR.gpmr.COR.Class).CreateTypeReference());
                    (f as MohawkCollege.EHR.gpmr.COR.Class).BaseClass = retVal.CreateTypeReference();
                }
                else
                    Trace.WriteLine(String.Format("Cannot find base class '{0}' for '{1}'. Base class must exist before it can be specialized", tr.Name, retVal.Name), "error");
            }

           
            #endregion

            // Assign Temporary Membership to the compiler class repository so the 
            // locating of a structural member will succeed
            retVal.MemberOf = classInfo.CompilerRepository;

            // Is a classCode enforcement placed on this class?
            #region Supplier Structural Domain
            if (classInfo.Class.SupplierStructuralDomain != null && classInfo.Class.SupplierStructuralDomain.Code != null)
            {
                MohawkCollege.EHR.gpmr.COR.Property clsa = retVal.FindMember("classCode", false, MohawkCollege.EHR.gpmr.COR.Property.PropertyTypes.Structural);

                if(clsa == null)// Class code is derived... we need to override it
                {
                    clsa = retVal.FindMember("classCode", true, MohawkCollege.EHR.gpmr.COR.Property.PropertyTypes.Structural);
                    // Any class code defined?
                    if (clsa != null)
                    {
                        clsa = PropertyParser.Parse(clsa.DerivedFrom as ClassAttribute, vocabularyRealm, classInfo.CompilerRepository, classInfo.DerivationSuppliers);
                        retVal.AddContent(clsa);
                    }
                    else // Makes no sense, we have a class code enforcement but no class code?
                    {
                        if (MifCompiler.hostContext.Mode == Pipeline.OperationModeType.Quirks)
                        {
                            Trace.WriteLine(String.Format("Supplier domain is specified on class '{0}' which has class code. Since GPMR is in quirks mode, the classCode attribute will be created", retVal.Name), "quirks");
                            clsa = new Property();
                            clsa.Name = "classCode";
                            clsa.PropertyType = Property.PropertyTypes.Structural;
                            clsa.Conformance = ClassContent.ConformanceKind.Mandatory;
                            retVal.AddContent(clsa);
                        }
                        else
                            throw new InvalidOperationException("Shouldn't be here. Supplier domain specified on a class with no classCode");
                    }
                }

                #region Now populate the code domain
                // Supplier domains
                if (classInfo.Class.SupplierStructuralDomain != null)
                {
                    var supplierStruct = classInfo.Class.SupplierStructuralDomain;

                    if (supplierStruct.Code != null && !String.IsNullOrEmpty(supplierStruct.Code.Code)) // Fixed code 
                        clsa.FixedValue = string.Format("{0}", supplierStruct.Code.Code);

                    // JF: If the code system is identified, then bind
                    if (supplierStruct.Code != null && !String.IsNullOrEmpty(supplierStruct.Code.CodeSystemName)) // Very odd thing that is present in UV mifs
                    {
                        Trace.WriteLine(String.Format("'{0}' is specified as fixed code's code system, however no fixed code is present. Assuming this is a bound code system instead", "assumption"));
                        clsa.SupplierDomain = classInfo.CompilerRepository.Find(o => o is CodeSystem && (o as CodeSystem).Name.Equals(supplierStruct.Code.CodeSystemName)) as Enumeration;
                        if (clsa.SupplierDomain == null)
                            Trace.WriteLine(String.Format("'{0}' could not be bound to '{1}' as the code system was not found", clsa.Name, supplierStruct.Code.CodeSystemName), "warn");
                    }
                    if (supplierStruct.ConceptDomain != null)
                    {
                        clsa.SupplierDomain = classInfo.CompilerRepository.Find(o => o is ConceptDomain && (o as ConceptDomain).Name.Equals(supplierStruct.ConceptDomain.Name)) as Enumeration;
                        if (clsa.SupplierDomain == null)
                            Trace.WriteLine(String.Format("'{0}' could not be bound to '{1}' as the concept domain was not found", clsa.Name, supplierStruct.ConceptDomain.Name), "warn");
                    }
                    if (supplierStruct.ValueSet != null)
                    {
                        clsa.SupplierDomain = classInfo.CompilerRepository.Find(o => o is ValueSet && (o as ValueSet).Name.Equals(supplierStruct.ValueSet.Name)) as Enumeration;
                        if (clsa.SupplierDomain == null)
                            Trace.WriteLine(String.Format("'{0}' could not be bound to '{1}' as the value set was not found", clsa.Name, supplierStruct.ValueSet.Name), "warn");

                        bool shouldFix = false;
                        if (clsa.SupplierDomain != null)
                        {
                            var enumLiteral = clsa.SupplierDomain.Literals.Find(o => o.Name == supplierStruct.ValueSet.RootCode);
                            shouldFix = enumLiteral != null && enumLiteral.RelatedCodes != null && enumLiteral.RelatedCodes.Count == 0;
                        }
                        if (shouldFix) clsa.FixedValue = String.Format("{0}", supplierStruct.ValueSet.RootCode);
                    }

                    // Supplier strength(s)
                    if (supplierStruct.ValueSet != null)
                        clsa.SupplierStrength = supplierStruct.ValueSet.CodingStrength == CodingStrengthKind.CNE ? (Property.CodingStrengthKind?)Property.CodingStrengthKind.CodedNoExtensions :
                            supplierStruct.ValueSet.CodingStrength == CodingStrengthKind.CWE ? (Property.CodingStrengthKind?)Property.CodingStrengthKind.CodedNoExtensions : null;
                    else
                    {
                        clsa.SupplierStrength = Property.CodingStrengthKind.CodedNoExtensions;
                        System.Diagnostics.Trace.WriteLine(string.Format("No vocabulary value set on property {0}! Defaulting to CNE for supplier strength", retVal.Name), "assumption");
                    }

                }



                //if (classInfo.Class.SupplierStructuralDomain.Code != null)
                //    clsa.FixedValue = string.Format("{0}{1}", classInfo.Class.SupplierStructuralDomain.Code.CodeSystem == null ? "" : classInfo.Class.SupplierStructuralDomain.Code.CodeSystem + ":", classInfo.Class.SupplierStructuralDomain.Code.Code);
                
                //if (classInfo.Class.SupplierStructuralDomain.ConceptDomain != null)
                //    clsa.SupplierDomain = classInfo.Class.SupplierStructuralDomain.ConceptDomain.Name;
                //if (classInfo.Class.SupplierStructuralDomain.ValueSet != null)
                //{
                //    clsa.SupplierStrength = classInfo.Class.SupplierStructuralDomain.ValueSet.CodingStrength == CodingStrengthKind.CNE ? (Property.CodingStrengthKind?)Property.CodingStrengthKind.CodedNoExtensions :
                //        classInfo.Class.SupplierStructuralDomain.ValueSet.CodingStrength == CodingStrengthKind.CWE ? (Property.CodingStrengthKind?)Property.CodingStrengthKind.CodedNoExtensions : null;
                //    clsa.SupplierDomainValueSet = classInfo.Class.SupplierStructuralDomain.ValueSet.Id;
                //}
                //else
                //{
                //    clsa.SupplierStrength = Property.CodingStrengthKind.CodedNoExtensions;
                //    System.Diagnostics.Trace.WriteLine(string.Format("No vocabulary value set on property {0}! Defaulting to CNE for supplier strength", retVal.Name), "assumption");
                //}
                #endregion
            }
            #endregion

            // All specifications
            #region Specializations

            //if ((classInfo.Class as MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat.Class).SpecializationChild != null && (classInfo.Class as MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat.Class).SpecializationChild.Count > 0)
            //{
            //    retVal.SpecializedBy = new List<MohawkCollege.EHR.gpmr.COR.TypeReference>();
            //    foreach (ClassGeneralization cg in (classInfo.Class as MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat.Class).SpecializationChild)
            //    {
            //        MohawkCollege.EHR.gpmr.COR.TypeReference specRef = new MohawkCollege.EHR.gpmr.COR.TypeReference();
            //        specRef.Name = string.Format("{0}.{1}", classInfo.ScopedPackageName, cg.ChildClassName);
            //        MohawkCollege.EHR.gpmr.COR.Property noProp = new MohawkCollege.EHR.gpmr.COR.Property();
            //        noProp.Container = retVal;
            //        specRef.Container = noProp;
            //        retVal.SpecializedBy.Add(specRef);
            //    }
            //}

            #endregion

            #region Behaviors

            // State machine on the class
            if (classInfo.Class.Behavior != null)
            {
                // Find the property that this state machine acts on

                Property stateProperty = retVal.FindMember(classInfo.Class.Behavior.SupplierStateAttributeName, false, Property.PropertyTypes.NonStructural | Property.PropertyTypes.Structural);
                
                stateProperty.StateMachine = StateMachineParser.Parse(classInfo.Class.Behavior);
            }

            #endregion

            #region Derivation Supplier
            if (classInfo.Class.DerivedFrom != null)
            {

                // Init retVal projection
                retVal.Realizations = new List<MohawkCollege.EHR.gpmr.COR.TypeReference>();

                // Iterate through each class this class is a projection of (is based on)
                foreach (ClassDerivation cd in classInfo.Class.DerivedFrom)
                {
                    try
                    {
                        // See if this class has been parsed
                        Package derivationPackage = null;
                        if(!classInfo.DerivationSuppliers.TryGetValue(cd.StaticModelDerivationId, out derivationPackage) || derivationPackage == null)
                        {
                            continue;
                        }

                        MohawkCollege.EHR.gpmr.COR.Feature ss = null;
 
                        // Has the package been compiled?
                        if (!classInfo.CompilerRepository.TryGetValue(string.Format("{0}", derivationPackage.PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : derivationPackage.PackageLocation.ToString(MifCompiler.NAME_FORMAT)), out ss))
                        {
                            // Attempt to parse 
                            PackageParser.Parse(derivationPackage.PackageLocation.ToString(MifCompiler.NAME_FORMAT), derivationPackage.MemberOfRepository, classInfo.CompilerRepository);
                            // Ditch if still can't find
                            if (!classInfo.CompilerRepository.TryGetValue(string.Format("{0}", derivationPackage.PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : derivationPackage.PackageLocation.ToString(MifCompiler.NAME_FORMAT)), out ss) || ss == null)
                                System.Diagnostics.Trace.WriteLine(String.Format("Can't find derivation class '{0}' (derivation supplier {1})", cd.ClassName, cd.StaticModelDerivationId), "warn");
                        }
                        

                        // Attempt to parse
                        MohawkCollege.EHR.gpmr.COR.SubSystem supplierSubSystem = ss as MohawkCollege.EHR.gpmr.COR.SubSystem;

                        // Find the class
                        MohawkCollege.EHR.gpmr.COR.Class supplierClass = classInfo.CompilerRepository[string.Format("{0}.{1}", supplierSubSystem.Name, cd.ClassName)] as MohawkCollege.EHR.gpmr.COR.Class;

                        // Add a realization
                        retVal.Realizations.Add(supplierClass.CreateTypeReference());

                        // Now do some processing
                        foreach (ClassContent cc in supplierClass.Content)
                        {
                            ClassContent rcc = null;

                            // Find the class content we are talking about
                            if (cc is Property)
                                rcc = retVal.FindMember(cc.Name, true, (cc as MohawkCollege.EHR.gpmr.COR.Property).PropertyType);
                            else
                                ; //TODO: Clean up choices so they match the realization class. Not sure if this supposed to be done

                            // Correct Items
                            if (rcc != null)
                            {
                                if (MohawkCollege.EHR.gpmr.COR.Documentation.IsEmpty(rcc.Documentation))// Correct documentation
                                    rcc.Documentation = cc.Documentation;
                                // Removed: This was supposed to correct missing supplier domains from the RMIMs using the RIM data, however it does 
                                // cause some issues.
                            //    if (rcc is MohawkCollege.EHR.gpmr.COR.Property && (rcc as MohawkCollege.EHR.gpmr.COR.Property).SupplierDomain == null &&
                            //        (cc as MohawkCollege.EHR.gpmr.COR.Property).SupplierDomain != null)
                            //    {
                            //        (rcc as MohawkCollege.EHR.gpmr.COR.Property).SupplierDomain = (cc as MohawkCollege.EHR.gpmr.COR.Property).SupplierDomain;
                            //        (rcc as MohawkCollege.EHR.gpmr.COR.Property).SupplierStrength = (cc as MohawkCollege.EHR.gpmr.COR.Property).SupplierStrength;
                            //    }
                            }

                        }

                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format("Could not find derivation supplier information for {0}", retVal.Name), "warn");
                    }
                }
            }
            #endregion

            #region Correct Vocabulary

            // This is quite a problem, when a property is bound to a value set, and that value set
            // is merely a pointer to other value sets of data we need to correct the pointer
            foreach (var property in retVal.Content)
            {
                // If the content is a property and it has a supplier domain, try to find the supplier domain
                if (property is Property && (property as Property).SupplierDomain != null)
                {

                        // Now to correct the reference
                    if ((property as Property).SupplierDomain.EnumerationReference != null &&
                            (property as Property).SupplierDomain.EnumerationReference.Count == 1)
                        (property as Property).SupplierDomain = (property as Property).SupplierDomain.EnumerationReference[0];
                }
            }

            #endregion

            // Sort the class content
            retVal.Content.Sort(new ClassContent.Comparator());

            // Fire parsed event
            retVal.FireParsed();

            // Return 
            return retVal;
        }


    }
}