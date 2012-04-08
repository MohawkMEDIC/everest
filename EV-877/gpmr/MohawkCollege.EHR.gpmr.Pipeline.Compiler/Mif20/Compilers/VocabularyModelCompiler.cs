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
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers;
using MohawkCollege.EHR.HL7v3.MIF.MIF20;
using System.Diagnostics;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Compilers
{
    /// <summary>
    /// Summary of VocabularyModelCompiler
    /// </summary>
    public class VocabularyModelCompiler : IPackageCompiler
    {
        #region IPackageCompiler Members

        private GlobalVocabularyModel vocabularyModel;
        private PackageRepository repository;

        /// <summary>
        /// Gets the type of package this compiler is responsible for compiling
        /// </summary>
        public Type PackageType
        {
            get { return typeof(GlobalVocabularyModel); }
        }

        /// <summary>
        /// Set the package (model) that will be compiled
        /// </summary>
        public object Package
        {
            set { vocabularyModel = value as GlobalVocabularyModel; }
        }

        /// <summary>
        /// Get or set the repository that the package belongs to
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
        /// Get or set the class repository that the compiled COR classes will be placed into
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public MohawkCollege.EHR.gpmr.COR.ClassRepository ClassRepository { get; set; }

        /// <summary>
        /// Compile the vocabulary model
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void Compile()
        {
            
            System.Diagnostics.Trace.WriteLine(string.Format("Processing global vocabulary model '{0}'...", vocabularyModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT)), "debug");

            // JF: This is safe to ignore here, this is handled by each of the individual binding
            //     realms
            // Bind all contexts so that property parsing will perform normally
            //foreach (ContextBinding cb in vocabularyModel.ContextBinding)
            //    if (cb.BindingRealmName == MifCompiler.BindingRealm && cb.BindingPriority == 0)
            //        PropertyParser.defaultCodingStrengths.Add(cb.ConceptDomain, cb.CodingStrength == CodingStrengthKind.CNE ? Property.CodingStrengthKind.CodedNoExceptions : Property.CodingStrengthKind.CodedWithExceptions);

            // Try to compile all concept domains from the model
            #region ConceptDomains
            foreach (MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.ConceptDomain cd in vocabularyModel.ConceptDomain)
            {
                // Process
                if (ClassRepository.Find(o => o.Name == cd.Name && o is MohawkCollege.EHR.gpmr.COR.ConceptDomain) == null)
                {
                    EnumerationParser.Parse(cd, vocabularyModel, repository, ClassRepository);

                    // Specialize domains now and get them out of the way
                    foreach (ConceptDomainRef cdr in cd.SpecializesDomain)
                    {
                        // Has the specialized domain been processed?
                        if (!ClassRepository.ContainsKey(cdr.Name))
                        {
                            // Find the model that contains this concept domain
                            GlobalVocabularyModel gvm = repository.Find(delegate(PackageArtifact p)
                            {
                                if (!(p is GlobalVocabularyModel)) return false; // Don't search anything but vocab

                                // Try to find something in this model
                                GlobalVocabularyModel gvc = p as GlobalVocabularyModel;

                                return (gvc.CodeSystem.Find(o => o.Name == cdr.Name) as object ??
                                gvc.ValueSet.Find(o => o.Name == cdr.Name) as object) != null;
                            }) as GlobalVocabularyModel;

                            // Was a global vocab model found
                            if (gvm != null)
                            {
                                // Sometimes there is no concept domain, but a code system only
                                MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.CodeSystem cs = gvm.CodeSystem.Find(o => o.Name == cdr.Name);
                                MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.ValueSet vs = gvm.ValueSet.Find(o => o.Name == cdr.Name);
                                if (cs != null) // We found a code system, built it out
                                {
                                    Enumeration enumeration = new MohawkCollege.EHR.gpmr.COR.CodeSystem();
                                    enumeration.Name = cs.Name;
                                    EnumerationParser.Parse(cs, enumeration, ClassRepository, vocabularyModel);
                                    enumeration.FireParsed();
                                }
                                else if (vs != null)
                                {
                                    Enumeration enumeration = new MohawkCollege.EHR.gpmr.COR.ValueSet();
                                    enumeration.Name = vs.Name;
                                    EnumerationParser.Parse(vs, enumeration, ClassRepository, vocabularyModel);
                                    enumeration.FireParsed();
                                }
                            }
                        }

                        // Lookup the specialized domain in COR format
                        if (ClassRepository.ContainsKey(cdr.Name) && ClassRepository.ContainsKey(cd.Name))
                        {
                            Enumeration specializor = ClassRepository[cd.Name] as Enumeration; // One who specializes
                            Enumeration specializee = ClassRepository[cdr.Name] as Enumeration; // One who is specialized

                            // Add properties
                            specializee.Literals.AddRange(specializor.Literals);
                        }
                    }
                }
                else
                    Trace.WriteLine(String.Format("Concept domain '{0}' is already represented, skipping...", cd.Name), "warn");

            }
            #endregion

            #region Code Systems

            // Any un-parsed code-systems
            foreach (MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.CodeSystem cs in vocabularyModel.CodeSystem)
            {
                if (ClassRepository.Find(o=>o.Name == cs.Name && o is MohawkCollege.EHR.gpmr.COR.CodeSystem) == null)
                {
                    Enumeration enumeration = new MohawkCollege.EHR.gpmr.COR.CodeSystem();
                    enumeration.Name = cs.Name;
                    EnumerationParser.Parse(cs, enumeration, ClassRepository, vocabularyModel);
                    enumeration.FireParsed();
                }
                else
                    Trace.WriteLine(String.Format("Code system '{0}' is already represented, skipping...", cs.Name), "warn");
            }

            #endregion

            #region Value Sets

            // Any un-parsed value-sets
            foreach (MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.ValueSet vs in vocabularyModel.ValueSet)
            {

                string realm = vocabularyModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT),
                    name = vs.Name;

                // Contains the key
                if (ClassRepository.Find(o => o.Name == vs.Name && o is MohawkCollege.EHR.gpmr.COR.ValueSet) == null)
                {
                    Enumeration enumeration = new MohawkCollege.EHR.gpmr.COR.ValueSet();
                    enumeration.Name = name;
                    EnumerationParser.Parse(vs, enumeration, ClassRepository, vocabularyModel);
                    enumeration.FireParsed();
                }
                else
                    Trace.WriteLine(String.Format("Value set '{0}' is already represented, skipping...", vs.Name), "warn");

            }
            #endregion

            #region Default Coding Strengths

            foreach (var strength in vocabularyModel.ContextBinding)
            {
                string id = strength.ValueSet;
                var valueSet = ClassRepository.Find(o => o is MohawkCollege.EHR.gpmr.COR.ValueSet && (o as MohawkCollege.EHR.gpmr.COR.ValueSet).Id == id);
                if (valueSet != null && !PropertyParser.defaultCodingStrengths.ContainsKey(valueSet.Name))
                    PropertyParser.defaultCodingStrengths.Add(valueSet.Name, strength.CodingStrength == CodingStrengthKind.CNE ? Property.CodingStrengthKind.CodedNoExtensions : Property.CodingStrengthKind.CodedWithExtensions);
                else if (valueSet == null)
                    Trace.WriteLine(String.Format("Value set '{0}' was not parsed, therefore context binding cannot be placed on it", id));
                else if (PropertyParser.defaultCodingStrengths[valueSet.Name] != (strength.CodingStrength == CodingStrengthKind.CNE ? Property.CodingStrengthKind.CodedNoExtensions : Property.CodingStrengthKind.CodedWithExtensions))
                    System.Diagnostics.Trace.WriteLine(String.Format("Binding strength for '{0}' is declared twice and both instances don't match!", strength.ConceptDomain), "warn");

                var conceptDomain = EnumerationParser.GetOrParseConceptDomainByName(strength.ConceptDomain, ClassRepository, PackageRepository as PackageRepository);
                // Context binding
                if (valueSet is MohawkCollege.EHR.gpmr.COR.ValueSet)
                    (valueSet as MohawkCollege.EHR.gpmr.COR.ValueSet).ContextBinding = conceptDomain;
                if (conceptDomain != null && valueSet != null)
                {
                    if (conceptDomain.ContextBinding == null) conceptDomain.ContextBinding = new List<Enumeration>();
                    // Add the binding realm
                    (valueSet as Enumeration).OwnerRealm = strength.BindingRealmName;
                    conceptDomain.ContextBinding.Add(valueSet as Enumeration);
                }
            }

            #endregion

            #region Supplements

            // Code systems supplement:
            // Apparently in v3 it is possible to add codes to a code system
            // after it has been defined
            foreach (var css in vocabularyModel.CodeSystemSupplement)
            {

                // First, find the code system to supplement
                var cs = EnumerationParser.GetOrParseCodeSystem(css.CodeSystemId, this.ClassRepository, this.vocabularyModel.MemberOfRepository);
                if (cs == null && MifCompiler.hostContext.Mode == Pipeline.OperationModeType.Quirks)
                {
                    // Try to find a value set 
                    Trace.WriteLine(String.Format("Could not supplement code system '{0}' as it was not defined in the repository", css.CodeSystemId), "quirks");
                    continue;
                }
                else if (css == null)// not in quirks
                    throw new InvalidOperationException(String.Format("Could not find target code system '{0}' of supplement '{1}', aborting", css.CodeSystemId, css.SupplementId));

                string docTitle = "Supplement";
                if (css.Header != null && css.Header.ResponsibleGroup != null && css.Header.ResponsibleGroup.Count > 0)
                    docTitle = String.Format("{0} Supplement:",css.Header.ResponsibleGroup[0].GroupName ?? css.Header.ResponsibleGroup[0].OrganizationName);
                
                // Create documentation
                var doc = new MohawkCollege.EHR.gpmr.COR.Documentation.TitledDocumentation()
                {
                    Name = "supplement",
                    Title = docTitle    
                };

                // Add text
                if (css.Annotations != null)
                {
                    var docContent = DocumentationParser.ParseAddPrefix(doc.Title, css.Annotations.Documentation);
                    if (cs.Documentation == null)
                        cs.Documentation = new COR.Documentation();
                    cs.Documentation.Merge(docContent);
                }

                // Now supplement the appropriate codes
                if(css.CodeSystemVersionSupplement.Count == 0)
                {
                    Trace.WriteLine(String.Format("Supplement '{0}' does not contain a any supplements", css.SupplementId), "warn");
                    continue;
                }
                else if (css.CodeSystemVersionSupplement.Count > 1) // More than one
                {
                    if (MifCompiler.hostContext.Mode == Pipeline.OperationModeType.Quirks)
                        Trace.WriteLine(String.Format("Supplement '{0}' contains more than one version to supplement. Since GPMR does not version enumeration structures only the first supplement ('{1}') will be used",
                            css.SupplementId, css.CodeSystemVersionSupplement[0].AppliesToReleaseDate), "quirks");
                    else
                    {
                        Trace.WriteLine(String.Format("Supplement '{0}' contains more than one version to supplement. GPMR does not version its enumeration releases and cannot apply this supplement!",
                            css.SupplementId, css.CodeSystemVersionSupplement[0].AppliesToReleaseDate), "error");
                        continue;
                    }
                }
                foreach (var concept in css.CodeSystemVersionSupplement[0].ConceptSupplement)
                {
                    // Attempt to find the ev
                    var ev = cs.GetEnumeratedLiterals().Find(o => o.Name == concept.Code);
                    if (ev == null)
                    {
                        Trace.WriteLine(String.Format("Could not find code '{0}' in '{1}' to which the supplemented concept applies", concept.Code, cs.Id), "error");
                        continue;
                    }
                    
                    // Supplement annotations?
                    if (concept.Annotations != null && concept.Annotations.Documentation != null)
                    {
                        var csDocumentation = DocumentationParser.ParseAddPrefix(doc.Name, concept.Annotations.Documentation);
                        // Append the supplement notes as comments
                        if (ev.Documentation == null)
                            ev.Documentation = new COR.Documentation();
                        ev.Documentation.Merge(csDocumentation);
                    }
                }
                
            }

            // These structures seem to needlessly complex the MIF ...
            // Why can't this data be added directly to the 
            // exported MIF? Meh, I guess we have to process it :@
            // This is equivalent in C++ to doing this:
            // Include STDIO but add some data to cout and 
            // change the name of X to Y ... stupid
            if(vocabularyModel.DependsOnVocabModel != null)
            {
                foreach (var dep in vocabularyModel.DependsOnVocabModel)
                {
                    List<Enumeration> alreadySupplemented = new List<Enumeration>();

                    foreach (var supp in dep.SupplementArtifact)
                        try
                        {

                            // First we have to find the object that is being supplemented
                            string artifactName = supp.SupplementedObject.Params.Find(o => o.Name == "artifactName").Value;
                            Type artifactType = supp.SupplementedObject.Params.Exists(o => o.Name == "subArtifact" && o.Value == "VS") ? typeof(MohawkCollege.EHR.gpmr.COR.ValueSet) : typeof(MohawkCollege.EHR.gpmr.COR.ConceptDomain);
                            if (artifactType == null)
                                throw new InvalidOperationException(String.Format("Cannot supplement object of type '{0}'", supp.SupplementedObject.Name));
                            Enumeration supplementObject = artifactType == typeof(MohawkCollege.EHR.gpmr.COR.ConceptDomain) ? EnumerationParser.GetOrParseConceptDomainByName(artifactName, ClassRepository, (PackageRepository)PackageRepository) : (Enumeration)EnumerationParser.GetOrParseValueSetByName(artifactName, ClassRepository, (PackageRepository)PackageRepository);

                            // HACK: Does the trick though
                            if (alreadySupplemented.Contains(supplementObject))
                            {
                                Trace.WriteLine(String.Format("Object '{0}' has already been supplemented, skipping", supplementObject.Name), "warn");
                                continue;
                            }
                            alreadySupplemented.Add(supplementObject);

                            // Supplemented object?
                            //if(supplementObject == null && MifCompiler.hostContext.Mode == Pipeline.OperationModeType.Quirks)
                            //{
                            //    supplementObject = ClassRepository.Find(o=>o.Name == artifactName) as Enumeration;
                            //    if (supplementObject != null)
                            //        Trace.WriteLine(String.Format("Could not find '{0}' of type '{1}' but found '{2}' named '{0}', supplementing that object instead",
                            //            artifactName, artifactType.Name, supplementObject.EnumerationType), "quirks");
                            //    else
                            //        Trace.WriteLine(String.Format("Could not find '{0}' of type '{1}', looked for any vocabulary structure named '{0}' but couldn't find any, will cause an error",
                            //            artifactType, artifactType.Name), "quirks");
                            //}
                            if (supplementObject == null)
                            {
                                Trace.WriteLine(String.Format("Cannot supplement object '{0}' as it was not found in the repository", artifactName), "warn");
                                continue;

                            }

                            Trace.WriteLine(String.Format("Adding supplements to '{0}'...", artifactName), "debug");
                            // Supplement the business name...
                            foreach (var bn in supp.BusinessName)
                                if (bn.Language == MifCompiler.Language || bn.Language == null)
                                    supplementObject.BusinessName = bn.Name;

                            // Supplement annotations
                            if (supp.Annotations != null)
                            {
                                if (supplementObject.Documentation == null)
                                    supplementObject.Documentation = new COR.Documentation();
                                supplementObject.Documentation.Merge(DocumentationParser.Parse(supp.Annotations.Documentation));
                            }
                        }
                        catch (Exception e)
                        {
                            if (MifCompiler.hostContext.Mode == Pipeline.OperationModeType.Quirks)
                                Trace.WriteLine(String.Format("Won't supplement object due to error : '{0}'.", e.Message), "quirks");
                            else
                                throw e;
                        }
                }

            }


            #endregion

        }

        #endregion
    }
}