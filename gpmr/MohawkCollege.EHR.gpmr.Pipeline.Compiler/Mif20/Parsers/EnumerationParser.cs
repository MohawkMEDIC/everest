/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
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
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.HL7v3.MIF.MIF20;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository;
using System.Linq;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers
{
    /// <summary>
    /// Summary of EnumerationParser
    /// </summary>
    internal class EnumerationParser
    {

        private class LiteralComparer : IEqualityComparer<Enumeration.EnumerationValue>
        {
            #region IEqualityComparer<EnumerationValue> Members

            /// <summary>
            /// Equals
            /// </summary>
            public bool Equals(Enumeration.EnumerationValue x, Enumeration.EnumerationValue y)
            {
                return x.Name == y.Name && x.CodeSystem == y.CodeSystem;
            }

            /// <summary>
            /// Get hash code of an object
            /// </summary>
            public int GetHashCode(Enumeration.EnumerationValue obj)
            {
                return obj == null ? 0 : obj.GetHashCode();
            }

            #endregion
        }

        /// <summary>
        /// Parse a MIF Concept domain into a COR enumeration
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        internal static void Parse(MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.ConceptDomain cd, GlobalVocabularyModel vocabularyModel, PackageRepository repository, ClassRepository classRepository)
        {

            
            // Process the concept domain
            MohawkCollege.EHR.gpmr.COR.ConceptDomain enumeration = new MohawkCollege.EHR.gpmr.COR.ConceptDomain();

            // Set the owner realm
            enumeration.OwnerRealm = vocabularyModel.PackageLocation.Realm;

            // Id for the concept domain
            enumeration.Id = String.Empty;

            // Name
            enumeration.Name = cd.Name;

            // Process documentation
            enumeration.Documentation = DocumentationParser.Parse(cd.Annotations.Documentation);

            // Derived from
            enumeration.DerivedFrom = cd;
            
            // Try to find a code system or value set within this model
            //ModelElement codeOrValueSet = vocabularyModel.CodeSystem.Find(o => o.Name == enumeration.Name) as ModelElement ??
            //    vocabularyModel.ValueSet.Find(o => o.Name == enumeration.Name) as ModelElement;

            //// Try to find it in another model if it is not in this model
            //if (codeOrValueSet == null)
            //{
            //    // Find the other package
            //    GlobalVocabularyModel other = repository.Find(delegate(PackageArtifact p)
            //    {
            //        if (!(p is GlobalVocabularyModel)) return false; // Don't search anything but vocab

            //        // Try to find something in this model
            //        GlobalVocabularyModel gvc = p as GlobalVocabularyModel;

            //        return (gvc.CodeSystem.Find(o => o.Name == enumeration.Name) as object ??
            //        gvc.ValueSet.Find(o => o.Name == enumeration.Name) as object) != null;
            //    }) as GlobalVocabularyModel;

            //    // Grab the code or value set
            //    if (other != null)
            //        codeOrValueSet = other.CodeSystem.Find(o => o.Name == enumeration.Name) as ModelElement ??
            //            other.ValueSet.Find(o => o.Name == enumeration.Name) as ModelElement;
            //}


            //// Determine what we'll process
            //if (codeOrValueSet is MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.CodeSystem)
            //    Parse(codeOrValueSet as MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.CodeSystem, enumeration, classRepository, vocabularyModel);
            //else if (codeOrValueSet is MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.ValueSet)
            //{
            //    Parse(codeOrValueSet as MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.ValueSet, enumeration, classRepository, vocabularyModel);
            //}
            //else
            //    System.Diagnostics.Trace.WriteLine(string.Format("Can't find code or concept domain details for '{0}'", enumeration.Name), "error");
            
            // Fire parsed so we don't get a stack overflow
            enumeration.FireParsed();

            // JF - Task 2019 - Support for specializations
            if (cd.SpecializesDomain != null && cd.SpecializesDomain.Count > 0)
            {
                enumeration.Specializes = new List<MohawkCollege.EHR.gpmr.COR.ConceptDomain>();
                foreach (var spec in cd.SpecializesDomain)
                {
                    // Attempt to locate the parsed specialized domain
                    var specDomain = classRepository.Find(o => o is MohawkCollege.EHR.gpmr.COR.ConceptDomain && o.Name == spec.Name) as MohawkCollege.EHR.gpmr.COR.ConceptDomain;
                    if (specDomain == null) // Could not find a parsed enumeration so we must find the raw
                    {
                        // Grab the vocab model and attempt to process it first
                        var conceptDomainRaw = vocabularyModel.ConceptDomain.Find(o => o.Name == spec.Name);

                        // One last kick at the can so to speak when we can't find it in the current vocab model
                        if (conceptDomainRaw == null)
                        {
                            var vocabModel = repository.Find(o => o is GlobalVocabularyModel && (o as GlobalVocabularyModel).ConceptDomain.Exists(c => c.Name == spec.Name)) as GlobalVocabularyModel;
                            if (vocabModel != null)
                                conceptDomainRaw = vocabModel.ConceptDomain.Find(o => o.Name == spec.Name);
                        }

                        if (conceptDomainRaw != null)
                        {
                            EnumerationParser.Parse(conceptDomainRaw, vocabularyModel, repository, classRepository);
                            specDomain = classRepository.Find(o => o is MohawkCollege.EHR.gpmr.COR.ConceptDomain && o.Name == spec.Name) as MohawkCollege.EHR.gpmr.COR.ConceptDomain;
                        }
                        else
                        {
                            Trace.WriteLine(String.Format("'{0}' cannot specialize '{1}' as '{1}' was not found!", enumeration.Name, spec.Name), "error");
                            continue;
                        }
                    }

                    // Is spec domain still null
                    if (specDomain == null)
                        Trace.WriteLine(String.Format("'{0}' cannot specialize '{1}' as '{1}' was not found", enumeration.Name, spec.Name));
                    else
                        if(!enumeration.Specializes.Contains(specDomain))
                            enumeration.Specializes.Add(specDomain);
                }
                    
            }
            // JF - Task 2019 - This is an odd MIF thing, don't know why some CD do this, but 
            // sometimes they will specify specialized by rather than specializes
            if (cd.SpecializedByDomain != null && cd.SpecializedByDomain.Count > 0)
            {
                foreach (var spec in cd.SpecializedByDomain)
                {
                    // First we have to find the "by" domain that specializes this
                    var specDomain = classRepository.Find(o => o is MohawkCollege.EHR.gpmr.COR.ConceptDomain && o.Name == spec.Name) as MohawkCollege.EHR.gpmr.COR.ConceptDomain;
                    // If we didn't find it, then process it from the mif
                    if (specDomain == null)
                    {
                        // Grab the vocab model and attempt to process it first
                        var conceptDomainRaw = vocabularyModel.ConceptDomain.Find(o => o.Name == spec.Name);
                        if (conceptDomainRaw != null)
                        {
                            EnumerationParser.Parse(conceptDomainRaw, vocabularyModel, repository, classRepository);
                            specDomain = classRepository.Find(o => o is MohawkCollege.EHR.gpmr.COR.ConceptDomain && o.Name == spec.Name) as MohawkCollege.EHR.gpmr.COR.ConceptDomain;
                        }
                        else
                        {
                            Trace.WriteLine(String.Format("'{0}' cannot specialize '{1}' as '{0}' was not found!", spec.Name, enumeration.Name), "error");
                            continue;
                        }
                    }

                    // Is spec domain still null
                    if (specDomain == null)
                        Trace.WriteLine(String.Format("'{0}' cannot specialize '{1}' as '{0}' was not found", spec.Name, enumeration.Name));
                    else
                    {
                        // Verify that the spec domain doesn't already spec 
                        if (specDomain.Specializes != null && specDomain.Specializes.Contains(enumeration))
                            Trace.WriteLine(String.Format("'{0}' cannot specialize '{1}' as '{0}' already specializes '{1}'", spec.Name, enumeration.Name));
                        else
                        {
                            if (specDomain.Specializes == null) specDomain.Specializes = new List<MohawkCollege.EHR.gpmr.COR.ConceptDomain>();
                            if (!specDomain.Specializes.Contains(enumeration))
                                specDomain.Specializes.Add(enumeration);
                        }
                    }
                }
            }

            // Ensure that the concept domain contains something
            if (enumeration.Literals.Count == 0)
                Trace.WriteLine(String.Format("Enumeration '{0}' contains no literals or was not interpretable, adding anywyas", enumeration.Name), "warn");
            //else
            //    System.Diagnostics.Trace.WriteLine(string.Format("Enumeration '{0}' contains no literals, won't add to repository", enumeration.Name), "warn");
        }

        /// <summary>
        /// Process the combined content
        /// </summary>
        /// <param name="cd">The combined content</param>
        /// <param name="classRepository">Repository of Classes</param>
        /// <param name="packageRep">Reference of all packages loaded into memory</param>
        /// <returns></returns>
        // This function is fugly, but unfortunately so are the MIFs so I don't see a way of getting around this. 
        // Maybe one day I will come back and clean it up.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        private static List<Enumeration.EnumerationValue> ProcessCombinedContent(Enumeration owner, CombinedContentDefinition cd, ClassRepository classRepository, PackageRepository packageRep, string csReference, List<FilterExpression> expression)
        {
            List<Enumeration.EnumerationValue> retVal = new List<Enumeration.EnumerationValue>();

            // Unions
            foreach (var content in cd.UnionWithContent)
                retVal = retVal.Union(ProcessContentDefinition(Operator.Union, content, owner, classRepository, packageRep, content.CodeSystem ?? csReference, expression), new LiteralComparer()).ToList();
            foreach (var content in cd.IntersectionWithContent)
                retVal = retVal.Intersect(ProcessContentDefinition(Operator.Intersect, content, owner, classRepository, packageRep, content.CodeSystem ?? csReference, expression), new LiteralComparer()).ToList();

            return retVal;
        }

        /// <summary>
        /// Process content definition
        /// </summary>
        private static List<Enumeration.EnumerationValue> ProcessContentDefinition(Operator operate, ContentDefinition content, Enumeration owner, ClassRepository classRepository, PackageRepository packageRep, string csReference, List<FilterExpression> expression)
        {
            List<Enumeration.EnumerationValue> retVal = new List<Enumeration.EnumerationValue>();
            
            // Find the referenced value set, etc...
            if (content.ValueSetRef != null) // content is a value set
            {
                #region Value Set Reference
                MohawkCollege.EHR.gpmr.COR.Feature valueSetDeref = null;
                if (classRepository.TryGetValue(content.ValueSetRef.Name, out valueSetDeref)) // Union with a named set
                {
                    retVal.AddRange((valueSetDeref as Enumeration).Literals);
                    owner.IsPartial &= (valueSetDeref as Enumeration).IsPartial;
                }
                else
                {
                    // Can't find the enumeration in our 'compiled' list, so we need to search the paster package repository
                    GlobalVocabularyModel vocabModel = packageRep.Find(pkg => pkg is GlobalVocabularyModel && (pkg as GlobalVocabularyModel).ValueSet.Find(vs => vs.Name == content.ValueSetRef.Name) != null) as GlobalVocabularyModel;
                    MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.ValueSet rawValueSet = null;

                    // Was the vocal model found?
                    if (vocabModel != null)
                        rawValueSet = vocabModel.ValueSet.Find(vs => vs.Name == content.ValueSetRef.Name);

                    // Still can't find, so error out
                    if (rawValueSet == null)
                    {
                        System.Diagnostics.Trace.WriteLine(String.Format("Can't find value set '{0}'...", content.ValueSetRef.Name), "error");
                        return new List<Enumeration.EnumerationValue>();
                    }

                    // Compile
                    Enumeration enumeration = new MohawkCollege.EHR.gpmr.COR.ValueSet();
                    enumeration.Name = rawValueSet.Name;
                    Parse(rawValueSet, enumeration, classRepository, vocabModel);
                    enumeration.FireParsed();

                    // Filter express
                    expression.Add(new FilterExpression()
                    {
                        Filter = Filter.All,
                        Operator = operate,
                        FromReference = enumeration
                    });

                    // Now add if it is a union
                    if (operate == Operator.Union)
                        retVal = retVal.Union(enumeration.Literals, new LiteralComparer()).ToList();
                    else
                        retVal = retVal.Intersect(enumeration.Literals, new LiteralComparer()).ToList();

                    owner.IsPartial |= enumeration.IsPartial;
                }
                #endregion
            }
            else if (content.CombinedContent != null) // combined content
            {
                #region Nested Combined Content
                var cs = GetOrParseCodeSystem(content.CodeSystem, classRepository, packageRep);
                GroupFilterExpression gfe = new GroupFilterExpression()
                {
                    Operator = operate,
                    Filter = Filter.All,
                    FromReference = cs
                };
                if (cs == null)
                    gfe.From = content.CodeSystem;

                var combinedContent = ProcessCombinedContent(owner, content.CombinedContent, classRepository, packageRep, content.CodeSystem ?? csReference, gfe.SubExpressions);
                // Perform set operation
                if (operate == Operator.Union)
                    retVal = retVal.Union(combinedContent, new LiteralComparer()).ToList();
                else
                    retVal = retVal.Intersect(combinedContent, new LiteralComparer()).ToList();

                expression.Add(gfe);
                #endregion
            }
            else if (content.PropertyBasedContent != null)
            {
                #region Property Based Content
                var cs = GetOrParseCodeSystem(content.CodeSystem ?? csReference, classRepository, packageRep);

                // Property based content is not processable so we just write it out
                var fs = new FilterExpression();
                fs.Operator = operate;
                fs.Filter = Filter.All;

                // Add the reference
                if (cs != null)
                    fs.FromReference = cs;
                else
                    fs.From = content.CodeSystem ?? csReference;

                // Determine the type
                List<ConceptPropertyRef> refs = new List<ConceptPropertyRef>();
                if (content.PropertyBasedContent.IncludeWithConceptProperty.Count > 0)
                {
                    fs.Filter |= Filter.Include;
                    refs = content.PropertyBasedContent.IncludeWithConceptProperty;
                }
                else if (content.PropertyBasedContent.ExcludeWithConceptProperty.Count > 0)
                {
                    fs.Filter |= Filter.Exclude;
                    refs = refs = content.PropertyBasedContent.ExcludeWithConceptProperty;
                }
                else
                {
                    string errorText = String.Format("Cannot determine the propertyBaseContent filter on '{0}'", content.CodeSystem ?? csReference);
                    Trace.WriteLine(errorText, MifCompiler.hostContext.Mode == Pipeline.OperationModeType.Quirks ? "quirks" : "error");
                    if (MifCompiler.hostContext.Mode != Pipeline.OperationModeType.Quirks)
                        throw new InvalidOperationException(errorText);
                    else
                        return new List<Enumeration.EnumerationValue>();
                }

                fs.Property = String.Empty;
                // Add the properties
                foreach (var rf in refs)
                    fs.Property += String.Format("{0} {1} and ", rf.Name, rf.Value);

                fs.Property = fs.Property.Substring(0, fs.Property.Length - 5);

                owner.IsPartial = true;
                expression.Add(fs);
                #endregion
            }
            else if (content.NonComputableContent != null) // Non computable content
            {
                #region Non Computable Content

                var cs = GetOrParseCodeSystem(csReference, classRepository, packageRep);

                // Create filter expression
                var fs = new TextFilterExpression()
                {
                    Text = new List<string>(),
                    FromReference = cs,
                    Operator = operate,
                    Filter = Filter.All
                };
                if (fs.FromReference == null)
                    fs.From = csReference;

                // Parse the text
                foreach (ComplexMarkupWithLanguage cmwl in content.NonComputableContent.Text)
                    if (cmwl.Language == MifCompiler.Language || cmwl.Language == null)
                    {
                        foreach (XmlElement xel in cmwl.MarkupElements ?? new List<XmlElement>().ToArray())
                            fs.Text.Add(xel.OuterXml.Replace(" xmlns:html=\"http://www.w3.org/1999/xhtml\"", "").Replace("html:", ""));
                        if (cmwl.MarkupText != null) fs.Text.Add(cmwl.MarkupText);
                    }

                // Add the filter
                owner.IsPartial = true;
                expression.Add(fs);
                #endregion
            }
            else if (content.CodeBasedContent != null) // Code based content
            {
                var cs = GetOrParseCodeSystem(content.CodeSystem, classRepository, packageRep);

                // If there is no specific mnemonics to be unioned and the code system was not parsed, then we
                // add the basic documentaiton
                if (content.CodeBasedContent.Count == 0 && cs == null)
                {
                    Trace.WriteLine(String.Format("Cannot locate CodeSystem '{0}' from which to union content. Only documentation will be added", content.CodeSystem), "warn");

                    // Add to filter expression only
                    expression.Add(new FilterExpression()
                    {
                        Operator = operate,
                        Filter = Filter.All,
                        From = content.CodeSystem
                    });

                    // Add partial set annotation
                    owner.IsPartial = true;
                }
                // There are no specific mnemonics to be unioned, so the entire code system is included
                else if (content.CodeBasedContent.Count == 0)
                {
                    // Add to filter expression
                    expression.Add(new FilterExpression()
                    {
                        Operator = operate,
                        Filter = Filter.All,
                        FromReference = cs
                    });
                    // Perform set operation
                    if (operate == Operator.Union)
                        retVal = retVal.Union(cs.Literals, new LiteralComparer()).ToList();
                    else
                        retVal = retVal.Intersect(cs.Literals, new LiteralComparer()).ToList();

                }
                else // There are mnemonics to be unioned, so just union them
                {
                    List<FilterExpression> codeExpression = new List<FilterExpression>();
                    foreach (var codeContent in content.CodeBasedContent)
                    {
                        Enumeration.EnumerationValue ev = new Enumeration.EnumerationValue();
                        ev.Name = codeContent.Code;
                        ev.CodeSystem = content.CodeSystem ?? csReference;
                        ev.CodeSystemType = cs == null ? new MohawkCollege.EHR.gpmr.COR.CodeSystem().EnumerationType : cs.EnumerationType;

                        // Is there a code in the code system that contains the ev?
                        if (cs != null)
                        {
                            var evCs = cs.Literals.Find(o => o.Name == ev.Name);
                            if (evCs != null)
                            {
                                ev.BusinessName = evCs.BusinessName;
                                ev.Documentation = evCs.Documentation;
                            }
                        }
                        else
                        {
                            Trace.WriteLine(String.Format("Cannot locate CodeSystem '{0}' from which to union content. Only documentation will be added", ev.CodeSystem), "warn");

                            // Add partial set annotation
                            owner.IsPartial = true;
                        }

                        // Get the code system name
                        //var csEnum = classRepository.Find(o => o is Enumeration && (o as Enumeration).Id.Equals(ev.CodeSystem)) as Enumeration;
                        //if (csEnum != null)
                        //    ev.CodeSystemName = csEnum.Name;
                        Filter f = Filter.All;

                        // do we include related codes
                        // JF: To Support fix in bug 787
                        if (codeContent.IncludeRelatedCodes != null && codeContent.IncludeRelatedCodes.Count > 0)
                        {
                            try
                            {
                                ev.RelatedCodes = new List<Enumeration.EnumerationValue>();
                                ev.RelatedCodes.AddRange(ProcessRelatedCodes(owner, ev, codeContent.IncludeRelatedCodes.Find(o => o.RelationshipTraversal == RelationshipTraversalKind.TransitiveClosure && o.RelationshipName == "Generalizes"), classRepository, packageRep));


                            }
                            catch (Exception e)
                            {
                                if (MifCompiler.hostContext.Mode == Pipeline.OperationModeType.Quirks)
                                    Trace.WriteLine(String.Format("An error occured processing related codes : '{0}'", e.Message), "quirks");
                                else
                                    throw;
                            }
                            // TODO: Should the related codes be promoted to the value set?
                        }

                        Enumeration.EnumerationValue[] literals = null;

                        // Add filter expression
                        if (cs != null)
                            codeExpression.Add(new FilterExpression()
                            {
                                FromReference = cs,
                                HeadCodeReference = ev,
                                Operator = operate,
                                Filter = codeContent.IncludeHeadCode ? Filter.Specializations | Filter.Include : Filter.Specializations | Filter.Exclude
                            });
                        else
                            codeExpression.Add(new FilterExpression()
                            {
                                From = content.CodeSystem ?? csReference,
                                HeadCode = codeContent.Code,
                                Operator = operate,
                                Filter = codeContent.IncludeHeadCode ? Filter.Specializations | Filter.Include : Filter.Specializations | Filter.Exclude
                            });

                        // Correct the verbiage
                        if (codeContent.IncludeRelatedCodes == null || codeContent.IncludeRelatedCodes.Count == 0)
                            codeExpression.Last().Filter = Filter.Include;
                        // JF- Ensures that the head code is suppressed if not included
                        if (!codeContent.IncludeHeadCode)
                        {
                            ev.Annotations.Add(new SuppressBrowseAnnotation());
                            literals = ev.RelatedCodes.ToArray();
                        }
                        else
                            literals = new Enumeration.EnumerationValue[] { ev };

                        
                        // Perform set operation
                        if (operate == Operator.Union)
                            retVal = retVal.Union(literals, new LiteralComparer()).ToList();
                        else
                            retVal = retVal.Intersect(literals, new LiteralComparer()).ToList();
                    }

                    if (!codeExpression.Exists(o => o.Filter == Filter.ExcludeSpecializations || o.Filter == Filter.IncludeSpecializations)) // There are no specializations so this is a group expression
                    {
                        SubsetFilterExpression expr = new SubsetFilterExpression()
                        {
                            FromReference = codeExpression[0].FromReference,
                            From = codeExpression[0].From,
                            Filter = Filter.All,
                            Operator = codeExpression[0].Operator,
                            Codes = new List<string>()
                        };

                        // Code expression
                        foreach (var cv in codeExpression)
                            expr.Codes.Add(cv.HeadCode);
                        expression.Add(expr);
                    }
                    else
                        expression.AddRange(codeExpression);

                }
                // Partial set of cs parent? If yes then we have to mark this enumeration
                // as a partial set as well
                if (cs != null)
                    owner.IsPartial |= cs.IsPartial;

            }

            return retVal;
        }




        /// <summary>
        /// Get or parse a code system
        /// </summary>
        internal static MohawkCollege.EHR.gpmr.COR.CodeSystem GetOrParseCodeSystem(string codeSystem, ClassRepository classRepository, PackageRepository packageRep)
        {
            var cs = classRepository.Find(o => o is MohawkCollege.EHR.gpmr.COR.CodeSystem && (o as Enumeration).Id == codeSystem) as MohawkCollege.EHR.gpmr.COR.CodeSystem;


            if (cs == null) // Can't locate the code system in our parsed repository, so try to find the raw
            {
                // Can't find the enumeration in our 'compiled' list, so we need to search the paster package repository
                GlobalVocabularyModel vocabModel = packageRep.Find(pkg => pkg is GlobalVocabularyModel && (pkg as GlobalVocabularyModel).CodeSystem.Find(rvs => rvs.CodeSystemId == codeSystem) != null) as GlobalVocabularyModel;
                MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.CodeSystem rawCodeSystem = null;

                // Was the vocal model found?
                if (vocabModel != null)
                    rawCodeSystem = vocabModel.CodeSystem.Find(vs => vs.CodeSystemId == codeSystem);

                // Found the raw code system so parse it
                if (rawCodeSystem != null)
                {
                    // Compile
                    cs = new MohawkCollege.EHR.gpmr.COR.CodeSystem();
                    cs.Name = rawCodeSystem.Name;
                    Parse(rawCodeSystem, cs, classRepository, vocabModel);
                    cs.FireParsed();
                }
            }

            return cs;
        }

        /// <summary>
        /// Process related codes
        /// </summary>
        private static IEnumerable<Enumeration.EnumerationValue> ProcessRelatedCodes(Enumeration owner, Enumeration.EnumerationValue code, IncludeRelatedCodes relatedCodes, ClassRepository classRepository, PackageRepository packageRep)
        {

            List<Enumeration.EnumerationValue> retVal = new List<Enumeration.EnumerationValue>(10);

            // First, get a ref to the package code system that is defined
            Enumeration relatedCodesEnum = classRepository.Find(o => o is Enumeration && (o as Enumeration).Id.Equals(code.CodeSystem)) as Enumeration;
            if(relatedCodesEnum == null)
            {
                Trace.WriteLine(String.Format("Can't find code-system '{0}' in this class repository, cannot include related codes", code.CodeSystem), "error");
                return retVal;
            }

            // Find the code within the found enum
            Enumeration.EnumerationValue srcEv = relatedCodesEnum.Literals.Find(o => o.Name.Equals(code.Name));
            if (srcEv == null)
            {
                Trace.WriteLine(String.Format("Can't find code '{0}' within '{1}'. Cannot include related codes", code.Name, relatedCodesEnum.ContentOid));
                return retVal;
            }

            // Process related codes
            if (srcEv.RelatedCodes == null)
                Trace.WriteLine(String.Format("Cannot include related codes for '{0}' as there aren't any code relationships for this concept", srcEv.Name), "warn");
            else if (relatedCodes != null && relatedCodes.RelationshipTraversal == RelationshipTraversalKind.TransitiveClosure &&
                relatedCodes.RelationshipName == "Generalizes")
                retVal.AddRange(srcEv.RelatedCodes);
            else
                Trace.WriteLine(String.Format("The only 'includeRelatedCodes' relationship supported is a TransitiveClosure with a Generalizes relationship", "error"));
            return retVal;
        }

        /// <summary>
        /// Parse a MIF value set and update <paramref name="enumeration"/> where necessary
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.CompareTo(System.String)")]
        internal static void Parse(MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.ValueSet vs, Enumeration enumeration, ClassRepository classRepository, GlobalVocabularyModel vocabModel)
        {
            // Valueset documentation is a little more descriptive than the concept domain so if there
            // is docs here, use them instead of the cd
            if (vs.Annotations != null && vs.Annotations.Documentation != null)
                enumeration.Documentation = DocumentationParser.Parse(vs.Annotations.Documentation);

            // Link the code system
            enumeration.Id = vs.Id;
            enumeration.ContentOid = vs.Id;

            // Set the owner realm
            enumeration.OwnerRealm = vocabModel.PackageLocation.Realm;

            if (vs.Version.Count == 0)
            {
                Trace.WriteLine(String.Format("Valueset '{0}' does not have any released versions, cannot process this valueset", vs.Name), "error");
                return;
            }
            else if (vs.Version.Count != 1)
                Trace.WriteLine(String.Format("Valueset '{0}' has multiple released versions, GPMR will assume version '{1}' is the most recent version", vs.Name, vs.Version[0].VersionDate), "warn");


            enumeration.VersionDate = vs.Version[0].VersionDate;

            // IF the content is not null then we can do some additional processing
            if (vs.Version[0].Content != null)
            {
                enumeration.ContentOid = vs.Version[0].Content.CodeSystem ?? vs.Id;

                //// Combined Content?
                //    if (vs.Version[0].Content.CombinedContent != null)
                //    {
                //        List<FilterExpression> expr = new List<FilterExpression>();
                //        enumeration.Literals.AddRange(ProcessCombinedContent(enumeration, vs.Version[0].Content.CombinedContent, classRepository, vocabModel.MemberOfRepository, vs.Version[0].Content.CodeSystem, expr));
                //        (enumeration as MohawkCollege.EHR.gpmr.COR.ValueSet).FilterExpression = expr;
                //    }
                //    if (vs.Version[0].Content.CodeBasedContent != null) // Code based content
                //    {
                //        if(vs.Version[0].Content.CodeBasedContent.Count == 0)
                //        {
                //            var cs = GetOrParseCodeSystem(vs.Version[0].Content.CodeSystem, classRepository, vocabModel.MemberOfRepository);
                //            if (cs != null)
                //            {
                //                enumeration.IsPartial = cs.IsPartial;
                //                enumeration.Literals.AddRange(cs.Literals);

                //                MohawkCollege.EHR.gpmr.COR.ValueSet vsEnumeration = enumeration as MohawkCollege.EHR.gpmr.COR.ValueSet;
                //                // Add filter express 
                //                if (vsEnumeration.FilterExpression == null)
                //                    vsEnumeration.FilterExpression = new List<FilterExpression>();
                //                vsEnumeration.FilterExpression.Add(new FilterExpression()
                //                {
                //                    FromReference = cs,
                //                    Operator = Operator.Union,
                //                    Filter = Filter.All
                //                });
                //            }
                //            else
                //            {
                //                Trace.WriteLine(String.Format("Cannot find code system '{0}' to bind literals for '{1}' ... ", vs.Version[0].Content.CodeSystem, enumeration.Name));
                //                // Add filter express 
                //                MohawkCollege.EHR.gpmr.COR.ValueSet vsEnumeration = enumeration as MohawkCollege.EHR.gpmr.COR.ValueSet;
                //                if (vsEnumeration.FilterExpression == null)
                //                    vsEnumeration.FilterExpression = new List<FilterExpression>();
                //                vsEnumeration.FilterExpression.Add(new FilterExpression()
                //                {
                //                    FromReference = cs,
                //                    Operator = Operator.Union,
                //                    Filter = Filter.All
                //                });
                //            }
                //        }
                //        else
                //        {
                //            List<FilterExpression> expr = new List<FilterExpression>();
                //            foreach (var codeContent in vs.Version[0].Content.CodeBasedContent)
                //            {

                //                enumeration.Literals.AddRange(ProcessContentDefinition(Operator.Union,
                //                    codeContent,
                //                    enumeration,
                //                    classRepository,
                //                    vocabModel.MemberOfRepository,
                //                    codeContent.Code ?? vs.Version[0].Content.CodeSystem,
                //                    expr));
                //            }
                //        }
                //    }
                if (vs.Version[0].Content != null)
                {
                    List<FilterExpression> expr = new List<FilterExpression>();
                    enumeration.Literals.AddRange(ProcessContentDefinition(Operator.Union,
                        vs.Version[0].Content,
                        enumeration,
                        classRepository,
                        vocabModel.MemberOfRepository,
                        vs.Version[0].Content.CodeSystem,
                        expr));

                    if (expr.Count == 1 && expr[0] is GroupFilterExpression)
                        (enumeration as MohawkCollege.EHR.gpmr.COR.ValueSet).FilterExpression = (expr[0] as GroupFilterExpression).SubExpressions;
                    else
                        (enumeration as MohawkCollege.EHR.gpmr.COR.ValueSet).FilterExpression = expr;
                }
                else if (vs.Version[0].SupportedCodeSystem != null && vs.Version[0].SupportedCodeSystem.Count > 0
                        && (vs.Version[0].Content.CodeBasedContent == null || vs.Version[0].Content.CodeBasedContent.Count == 0))
                    {
                        // Supported code system
                        foreach (var codeSystem in vs.Version[0].SupportedCodeSystem)
                        {
                            // Find the code system
                            var csReferences = from kv in classRepository
                                               where kv.Value is MohawkCollege.EHR.gpmr.COR.CodeSystem && (kv.Value as MohawkCollege.EHR.gpmr.COR.CodeSystem).Id == codeSystem
                                               select kv.Value;
                            Enumeration enumerationRef = null;
                            foreach (var csReference in csReferences)
                                enumerationRef = csReference as Enumeration;

                            // Check that an enumeration was found
                            if (enumerationRef == null)
                            {
                                // Attempt to parse
                                var fCodeSet = vocabModel.CodeSystem.Find(o => o.CodeSystemId == codeSystem);
                                var fValueSet = vocabModel.ValueSet.Find(o => o.Id == codeSystem);
                                if (fCodeSet != null)
                                {
                                    Enumeration tEnum = new MohawkCollege.EHR.gpmr.COR.CodeSystem();
                                    tEnum.Name = fCodeSet.Name;
                                    EnumerationParser.Parse(fCodeSet, tEnum, classRepository, vocabModel);
                                    tEnum.FireParsed();
                                    enumerationRef = tEnum;
                                }
                                else if (fValueSet != null)
                                {
                                    Enumeration tValueSet = new MohawkCollege.EHR.gpmr.COR.ValueSet();
                                    tValueSet.Name = fValueSet.Name;
                                    EnumerationParser.Parse(fValueSet, tValueSet, classRepository, vocabModel);
                                    tValueSet.FireParsed();
                                    enumerationRef = tValueSet;
                                }
                                else
                                {
                                    System.Diagnostics.Trace.WriteLine(String.Format("Can't bind '{1}' to code system '{0}'. Can't find the code system in the repository!", codeSystem, enumeration.Name), "warn");
                                    continue;
                                }
                            }

                            if (enumeration.EnumerationReference == null)
                                enumeration.EnumerationReference = new List<Enumeration>();

                            enumeration.EnumerationReference.Add(enumerationRef);

                            MohawkCollege.EHR.gpmr.COR.ValueSet vsEnumeration = enumeration as MohawkCollege.EHR.gpmr.COR.ValueSet;
                            if (vsEnumeration.FilterExpression == null)
                                vsEnumeration.FilterExpression = new List<FilterExpression>();
                            vsEnumeration.FilterExpression.Add(new FilterExpression()
                            {
                                FromReference = enumerationRef,
                                Operator = Operator.Union,
                                Filter = Filter.All
                            });
                        }
                    }
            }

            // Loop through enumerated values (if they exist)
            if (vs.Version[0].EnumeratedContent != null)
            {
                enumeration.IsPartial = false;
                foreach (VocabularyCodeRef c in vs.Version[0].EnumeratedContent.Codes)
                {
                    
                    // Find if the enumeration value already exists
                    Enumeration.EnumerationValue fev = enumeration.Literals.Find(o => o.Name == c.Code);
                    if (fev != null)
                        enumeration.Literals.Remove(fev);

                    if (c.Code == null || c.Code.Length == 0 ) continue; // no use in adding a blank code
                    Enumeration.EnumerationValue ev = new Enumeration.EnumerationValue();
                    ev.BusinessName = c.CodePrintName;
                    ev.Name = c.Code;
                    ev.CodeSystemType = enumeration.EnumerationType;

                    ev.CodeSystem = c.CodeSystem ?? enumeration.ContentOid;
                    //ev.CodeSystemName = c.CodeSystemName;

                    // Sanity check
                    //if (!ev.CodeSystem.Equals(enumeration.ContentOid))
                    //    Trace.WriteLine(String.Format("Code '{0}' from enumerated content is from '{1}' but enumeration specifies '{2}'",
                    //        ev.Name, ev.CodeSystem, enumeration.ContentOid), "warn");
                    enumeration.Literals.Add(ev);
                }
            }

            // If there are examples, then add a new documentation area
            if (vs.Version[0].ExampleContent != null && enumeration.Literals.Count == 0) // Some example content that we can use
            {
                foreach (var eg in vs.Version[0].ExampleContent.Codes)
                {

                    Enumeration.EnumerationValue ev = null;
                    
                    // Attempt to get a reference code instead
                    var cs = GetOrParseCodeSystem(eg.CodeSystem, classRepository, vocabModel.MemberOfRepository); 

                    // Did we get a code?
                    if (cs != null)
                        ev = cs.Literals.Find(o => o.Name == eg.Code && o.CodeSystem == eg.CodeSystem);
                    if(ev == null)
                        ev = new Enumeration.EnumerationValue()
                        {
                            Name = eg.Code,
                            CodeSystem = eg.CodeSystem,
                            BusinessName = eg.CodePrintName
                        };
                    enumeration.SampleLiterals.Add(ev);
                }
                //enumeration.IsPartial = true;
            }

            // Sort the enumeration literals according to name
            enumeration.Literals.Sort(delegate(Enumeration.EnumerationValue a, Enumeration.EnumerationValue b)
            {
                return a.Name.CompareTo(b.Name);
            });

            //    var otherDoc = new MohawkCollege.EHR.gpmr.COR.Documentation.TitledDocumentation()
            //    {
            //        Name = "Example Codes",
            //        Title = "Example Codes",
            //        Text = new List<string>()
            //    };
                
            //    otherDoc.Text.Add("<ul>");
            //    foreach (var cd in vs.Version[0].ExampleContent.Codes)
            //        otherDoc.Text.Add(String.Format("<li><b>{0}</b> - &lt;code code=\"{1}\" codeSystem=\"{2}\"/></li>",
            //            cd.CodePrintName, cd.Code, cd.CodeSystem));
            //    otherDoc.Text.Add("</ul>");
            //    if (enumeration.Documentation == null)
            //        enumeration.Documentation = new MohawkCollege.EHR.gpmr.COR.Documentation();
            //    if (enumeration.Documentation.Other == null)
            //        enumeration.Documentation.Other = new List<MohawkCollege.EHR.gpmr.COR.Documentation.TitledDocumentation>();
            //    enumeration.Documentation.Other.Add(otherDoc);
            //}

            // Copy any documentation or other items from 
            // the code content ref (if available) and the
            // literals in the value set
            foreach (var lit in enumeration.Literals)
            {
                if(enumeration.ContentRef == null)
                    continue;
                var csList = enumeration.ContentRef.Literals.Find(o => o.CodeSystem == lit.CodeSystem && o.Name == lit.Name); // find the literal in the ref code
                lit.Documentation = lit.Documentation ?? csList.Documentation;
                lit.BusinessName = lit.BusinessName ?? csList.BusinessName;
            }
        }

        
        /// <summary>
        /// Parse the code system and update <paramref name="enumeration"/> where necessary
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "classRepository"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.CompareTo(System.String)")]
        internal static void Parse(MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.CodeSystem cs, Enumeration enumeration, ClassRepository classRepository, GlobalVocabularyModel vocabModel)
        {
            // Code documentation is a little more descriptive than the concept domain so if there
            // is docs here, use them instead of the cd
            if (cs.Annotations != null && cs.Annotations.Documentation != null)
                enumeration.Documentation = DocumentationParser.Parse(cs.Annotations.Documentation);

            enumeration.OwnerRealm = vocabModel.PackageLocation.Realm;


            // Set copyright
            if (vocabModel.Header != null &&
                vocabModel.Header.Copyright != null &&
                !String.IsNullOrEmpty(vocabModel.Header.Copyright.Owner))
            {
                if (enumeration.Documentation == null)
                    enumeration.Documentation = new COR.Documentation();
                enumeration.Documentation.Copyright = String.Format("Copyright (C) {0}, {1}", vocabModel.Header.Copyright.Year, vocabModel.Header.Copyright.Owner);
            }
            // Set the responsible groups
            if (cs.Header != null && cs.Header.ResponsibleGroup != null)
            {
                if (enumeration.Documentation == null)
                    enumeration.Documentation = new COR.Documentation();
                if (enumeration.Documentation.Other == null)
                    enumeration.Documentation.Other = new List<COR.Documentation.TitledDocumentation>();

                MohawkCollege.EHR.gpmr.COR.Documentation.TitledDocumentation resp = new COR.Documentation.TitledDocumentation();
                resp.Title = "Maintaining Organization";
                resp.Name = "responsibleGroup";
                resp.Text = new List<string>();
                foreach (var respGrp in cs.Header.ResponsibleGroup)
                {
                    resp.Text.Add(String.Format("{0} {1}{2}{3}", respGrp.OrganizationName,
                        !String.IsNullOrEmpty(respGrp.GroupName) ? "(" : String.Empty,
                        respGrp.GroupName,
                        !String.IsNullOrEmpty(respGrp.GroupName) ? ")" : String.Empty));
                }
                enumeration.Documentation.Other.Add(resp);
            }

            // Set the primary repository
            if (cs.Header != null && cs.Header.PrimaryRepository != null)
            {
                if (enumeration.Documentation == null)
                    enumeration.Documentation = new COR.Documentation();
                if (enumeration.Documentation.Other == null)
                    enumeration.Documentation.Other = new List<COR.Documentation.TitledDocumentation>();
                enumeration.Documentation.Other.Add(new COR.Documentation.TitledDocumentation()
                {
                    Title = "Source Definition",
                    Name = "primaryRepository",
                    Text = new List<string>() { 
                        String.Format("<a href=\"{0}\">{0}</a>", cs.Header.PrimaryRepository)
                    }
                });
            }
                
            if (cs.ReleasedVersion.Count == 0)
            {
                Trace.WriteLine(String.Format("Code system '{0}' does not have any released versions, cannot process this code system", cs.Name), "error");
                return;
            }
            else if (cs.ReleasedVersion.Count != 1)
                Trace.WriteLine(String.Format("Code system '{0}' has multiple released versions, GPMR will assume version '{1}' is the most recent version", cs.Name, cs.ReleasedVersion[0].PublisherVersionId), "assumption");

            if (!cs.ReleasedVersion[0].CompleteCodesIndicator)
            {
                enumeration.IsPartial = true;
                // Output the remark
                Trace.WriteLine(String.Format("Code system '{0}' is marked as a partial set, your renderer may not provide a method of extending this enumeration", cs.Name), "info");
            }

            // Link code system id
            enumeration.Id = cs.CodeSystemId;
            enumeration.ContentOid = enumeration.Id;
            enumeration.VersionDate = cs.ReleasedVersion[0].ReleaseDate;
            enumeration.BusinessName = enumeration.BusinessName ?? cs.Title;
            // Process all codes
            foreach (Concept c in cs.ReleasedVersion[0].Concept)
            {
                var activeCode = c.Code.Find(o => o.Status == CodeStatusKind.Active);
                if (c.Code != null && activeCode != null)
                {
                    Enumeration.EnumerationValue ev = new Enumeration.EnumerationValue(); // Create

                    if (c.Annotations != null)
                        ev.Documentation = DocumentationParser.Parse(c.Annotations.Documentation);  // Parse docs

                    PrintName pn = (activeCode.PrintName.Count == 0 ? c.PrintName : activeCode.PrintName).Find(o => o.Language.ToLower() == MifCompiler.Language.ToLower()); // Find print name that matches the lang
                    if (pn != null) ev.BusinessName = pn.Text; // Set the print name if one was found
                    ev.Name = activeCode.CodeName; // Add the code
                    ev.CodeSystem = enumeration.ContentOid ?? enumeration.Id; // Copy the content OID
                    ev.CodeSystemType = enumeration.EnumerationType;

                    // Don't show
                    if (!c.IsSelectable)
                        ev.Annotations.Add(new SuppressBrowseAnnotation());

                    // Enumeration literals
                    if (!String.IsNullOrEmpty(ev.Name) && activeCode.Status == CodeStatusKind.Active)
                        enumeration.Literals.Add(ev);

                }
            }

            // Process relations
            foreach (Concept c in cs.ReleasedVersion[0].Concept)
            {
                var activeCode = c.Code.Find(o => o.Status == CodeStatusKind.Active);
                if (c.Code != null && activeCode != null && c.ConceptRelationship != null) // related codes
                    foreach (var rel in c.ConceptRelationship)
                        if (rel.TargetConcept != null && rel.RelationshipName == "Specializes") // We're specializing another code so we add this code to the other code
                        {

                            var relEv = enumeration.Literals.Find(o => o.Name.Equals(rel.TargetConcept.Code)); // get the related code
                            if (relEv == null)
                                Trace.WriteLine(String.Format("Code '{0}' cannot specialize '{1}' as '{1}' is not a valid code. Code system '{2}'", activeCode.CodeName, rel.TargetConcept.Code, enumeration.Id ?? enumeration.ContentOid ?? enumeration.Name), "error");
                            else
                            {
                                if (relEv.RelatedCodes == null)
                                    relEv.RelatedCodes = new List<Enumeration.EnumerationValue>();

                                relEv.RelatedCodes.Add(enumeration.Literals.Find(o => o.Name.Equals(activeCode.CodeName)));
                            }
                        }
                        else
                            Trace.WriteLine(String.Format("Cannot add code relationship '{0}'. Either the relationship is not supported or the target concept is not specified", activeCode.CodeName), "error");

            }
            
            // Clean out codes that are nested
            List<Enumeration.EnumerationValue> garbagePail = new List<Enumeration.EnumerationValue>();
            BuildCodeSystemGarbagePail(enumeration.Literals, garbagePail);
            enumeration.Literals.RemoveAll(o => garbagePail.Contains(o));

            // Sort the enumeration literals according to name
            enumeration.Literals.Sort(delegate(Enumeration.EnumerationValue a, Enumeration.EnumerationValue b)
            {
                return a.Name.CompareTo(b.Name);
            });

        }

        /// <summary>
        /// Builds a garbage pail of literals that should be removed from the enumeration
        /// </summary>
        private static void BuildCodeSystemGarbagePail(List<Enumeration.EnumerationValue> literals, List<Enumeration.EnumerationValue> garbagePail)
        {
            foreach (var literal in literals)
            {
                if (!literal.Annotations.Exists(o=>o is SuppressBrowseAnnotation) && literal.RelatedCodes != null)
                {
                    garbagePail.AddRange(literal.RelatedCodes);
                    BuildCodeSystemGarbagePail(literal.RelatedCodes, garbagePail);
                }
            }
        }

        /// <summary>
        /// Get or parse a value set
        /// </summary>
        internal static COR.ValueSet GetOrParseValueSetByName(string valueSetName, ClassRepository classRepository, PackageRepository packageRep)
        {
            var vs = classRepository.Find(o => o is MohawkCollege.EHR.gpmr.COR.ValueSet && (o as Enumeration).Name == valueSetName) as MohawkCollege.EHR.gpmr.COR.ValueSet;


            if (vs == null) // Can't locate the code system in our parsed repository, so try to find the raw
            {
                // Can't find the enumeration in our 'compiled' list, so we need to search the paster package repository
                GlobalVocabularyModel vocabModel = packageRep.Find(pkg => pkg is GlobalVocabularyModel && (pkg as GlobalVocabularyModel).ValueSet.Find(rvs => rvs.Name == valueSetName) != null) as GlobalVocabularyModel;
                MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.ValueSet rawCodeSystem = null;

                // Was the vocal model found?
                if (vocabModel != null)
                    rawCodeSystem = vocabModel.ValueSet.Find(cs => cs.Name == valueSetName);

                // Found the raw code system so parse it
                if (rawCodeSystem != null)
                {
                    // Compile
                    vs = new MohawkCollege.EHR.gpmr.COR.ValueSet();
                    vs.Name = rawCodeSystem.Name;
                    Parse(rawCodeSystem, vs, classRepository, vocabModel);
                    vs.FireParsed();
                }
            }

            return vs;
        }

        /// <summary>
        /// Get or parse a code system
        /// </summary>
        internal static MohawkCollege.EHR.gpmr.COR.CodeSystem GetOrParseCodeSystemByName(string codeSystemName, ClassRepository classRepository, PackageRepository packageRep)
        {
            var cs = classRepository.Find(o => o is MohawkCollege.EHR.gpmr.COR.CodeSystem && (o as Enumeration).Name == codeSystemName) as MohawkCollege.EHR.gpmr.COR.CodeSystem;


            if (cs == null) // Can't locate the code system in our parsed repository, so try to find the raw
            {
                // Can't find the enumeration in our 'compiled' list, so we need to search the paster package repository
                GlobalVocabularyModel vocabModel = packageRep.Find(pkg => pkg is GlobalVocabularyModel && (pkg as GlobalVocabularyModel).CodeSystem.Find(rvs => rvs.Name == codeSystemName) != null) as GlobalVocabularyModel;
                MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.CodeSystem rawCodeSystem = null;

                // Was the vocal model found?
                if (vocabModel != null)
                    rawCodeSystem = vocabModel.CodeSystem.Find(vs => vs.Name == codeSystemName);

                // Found the raw code system so parse it
                if (rawCodeSystem != null)
                {
                    // Compile
                    cs = new MohawkCollege.EHR.gpmr.COR.CodeSystem();
                    cs.Name = rawCodeSystem.Name;
                    Parse(rawCodeSystem, cs, classRepository, vocabModel);
                    cs.FireParsed();
                }
            }

            return cs;
        }
        
        /// <summary>
        /// Get or parse concept domain
        /// </summary>
        internal static MohawkCollege.EHR.gpmr.COR.ConceptDomain GetOrParseConceptDomainByName(string conceptDomainName, ClassRepository classRepository, PackageRepository packageRep)
        {
            var cs = classRepository.Find(o => o is MohawkCollege.EHR.gpmr.COR.ConceptDomain && (o as Enumeration).Name == conceptDomainName) as MohawkCollege.EHR.gpmr.COR.ConceptDomain;


            if (cs == null) // Can't locate the code system in our parsed repository, so try to find the raw
            {
                // Can't find the enumeration in our 'compiled' list, so we need to search the paster package repository
                GlobalVocabularyModel vocabModel = packageRep.Find(pkg => pkg is GlobalVocabularyModel && (pkg as GlobalVocabularyModel).ConceptDomain.Find(rvs => rvs.Name == conceptDomainName) != null) as GlobalVocabularyModel;
                MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary.ConceptDomain rawCodeSystem = null;

                // Was the vocal model found?
                if (vocabModel != null)
                    rawCodeSystem = vocabModel.ConceptDomain.Find(vs => vs.Name == conceptDomainName);

                // Found the raw code system so parse it
                if (rawCodeSystem != null)
                {
                    // Compile
                    Parse(rawCodeSystem, vocabModel, packageRep, classRepository);
                    cs = classRepository.Find(o => o is MohawkCollege.EHR.gpmr.COR.ConceptDomain && (o as Enumeration).Name == conceptDomainName) as MohawkCollege.EHR.gpmr.COR.ConceptDomain;
                }
            }

            return cs;
        }
    }
}
