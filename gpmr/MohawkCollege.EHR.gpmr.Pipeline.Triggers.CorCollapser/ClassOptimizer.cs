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
 * Date: 08-11-2009
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.gpmr.COR;
using MARC.Everest.Attributes;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorCollapser
{
    /// <summary>
    /// Optimization class for COR Classes
    /// </summary>
    public class ClassOptimizer : ICorOptimizer
    {
        #region ICorOptimizer Members

       
        /// <summary>
        /// Gets the type that this optimizer handles
        /// </summary>
        public Type HandlesType
        {
            get { return typeof(Class); }
        }

        /// <summary>
        /// Source repository of the optimization
        /// </summary>
        public MohawkCollege.EHR.gpmr.COR.ClassRepository Repository { get; set; }

        /// <summary>
        /// Optimize the <paramref name="feature"/>
        /// </summary>
        public MohawkCollege.EHR.gpmr.COR.Feature Optimize(MohawkCollege.EHR.gpmr.COR.Feature f, CombineLog workingLog)
        {
            string qualifiedName = String.Format("{0}.{1}", (f as Class).ContainerName, f.Name);
            // Garbage bin
            List<String> classGarbageBin = new List<string>();
            List<ClassContent> contentGarbageBin = new List<ClassContent>();

            // Still valid to process this feature
            if (!Repository.ContainsKey(qualifiedName)) 
                return null; // Can't process non-existant class

            // First determine if a class that is identical to this already exists
            FeatureComparer comparer = new FeatureComparer();
            var matchingFeatures = from kv in Repository
                                            where comparer.Compare(kv.Value, f) == 0
                                            select kv.Value;

            CombineInfo currentCombinationLog = new CombineInfo();

            // Find matching features in each of the sub-systems
            if (matchingFeatures.Count() > 1 && CorCollapserPipelineTrigger.combine == true)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("{0} other classes can be represented by this class", matchingFeatures.Count()), "debug");
                currentCombinationLog.Destination = qualifiedName;

                foreach (var s in matchingFeatures)
                {
                    string qName = String.Format("{0}.{1}", (s as Class).ContainerName, s.Name);
                    if (qName != qualifiedName)
                    {
                        
                        System.Diagnostics.Trace.WriteLine(String.Format("\tReplaces '{0}'", qName), "debug");
                        currentCombinationLog.Class.Add(qName);

                        // Copy alternate traversal data
                        foreach (ClassContent cc in (s as Class).Content)
                            if (cc is Property && (cc as Property).AlternateTraversalNames != null)
                            {
                                if (((f as Class).Content.Find(o => o.Name == cc.Name) as Property).AlternateTraversalNames == null)
                                    ((f as Class).Content.Find(o => o.Name == cc.Name) as Property).AlternateTraversalNames = new List<Property.AlternateTraversalData>();
                                ((f as Class).Content.Find(o => o.Name == cc.Name) as Property).AlternateTraversalNames.AddRange((cc as Property).AlternateTraversalNames);
                            }
                        // Replace referneces
                        ReplaceReferences(s as Class, f as Class);

                        // Add an annotation
                        f.Annotations.Add(new CodeCombineAnnotation((s as Class).CreateTypeReference()));

                        // Remove this class (Add it to the garbage bin)
                        classGarbageBin.Add(qName);
                    }
                }
                workingLog.CombineOps.Add(currentCombinationLog);
            }

            // Now collapse members
            if (CorCollapserPipelineTrigger.collapse)
            {
                for(int i = 0; i < (f as Class).Content.Count; i++)
                {
                    ClassContent cc = (f as Class).Content[i];
                    // Determine if it is a candidate for collapsing needless complexity
                    if (IsCandidateForMeaninglessComplexityCollapse(cc))
                    {
                        while (IsCandidateForMeaninglessComplexityCollapse(cc))
                        {
                            System.Diagnostics.Trace.WriteLine(string.Format("\tCollapsing '{0}'", cc.Name), "debug");
                            CollapseMemberType(cc, f as Class);
                        }
                    }
                    // Determine if it is a candidate for collapsing the entire type
                    else if (IsCandidateForTypeCollapse(cc))
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format("\tCollapsing '{0}'", cc.Name), "debug");
                        CopyMembers((cc as Property).Type, f as Class, cc);
                        contentGarbageBin.Add(cc);
                    }
                }

                // Clean up garbage bin
                (f as Class).Content.RemoveAll(a => contentGarbageBin.Contains(a));
                (f as Class).Content.Sort(new ClassContent.Comparator());
            }
            
            // Clean the garbage bin
            foreach (string s in classGarbageBin)
                Repository.Remove(s);

            return null;
        }

        /// <summary>
        /// Collapse the data type of <paramref name="cc"/> so that it is the only meaningful data type provided by its 
        /// natural type
        /// </summary>
        private void CollapseMemberType(ClassContent cc, Class context)
        {
            if(!(cc is Property)) throw new InvalidOperationException("Can't collapse this member type"); // Not a candidate

            // The condition for this type of collection is that the referenced type:
            //  1. Provides only ONE property (after --collapse-ignore are removed)
            //  2. The ONE property is mandatory (must be provided)
            List<ClassContent> content = (cc as Property).Type.Class.GetFullContent();
            content.RemoveAll(a => CorCollapserPipelineTrigger.collapseIgnore.Contains(a.Name)); // Remove the ignore

            if (CorCollapserPipelineTrigger.collapseIgnoreFixed)
                content.RemoveAll(o => o is Property && !String.IsNullOrEmpty((o as Property).FixedValue) && (o as Property).PropertyType == Property.PropertyTypes.Structural);

            if(content.Count != 1) throw new InvalidOperationException("Can't collapse type with more than one meaningful content"); // can't collapse this relation as there is more than sub-property
            ClassContent candidate = content[0];

            cc.Annotations.Add(new CodeCollapseAnnotation() { Name = cc.Name, Order = 0, Property = cc as Property });

            // Has this collapsed member had any collapsing
            if (candidate.Annotations.Count > 0)
            {
                // Get all code collapse annotations from the candidate
                List<Annotation> collapseAnnotations = candidate.Annotations.FindAll(o => o is CodeCollapseAnnotation);
                // Find one that links it to the candidate

                //collapseAnnotations.RemoveAll(o => candidate.Annotations.Count(ca => (ca as CodeCollapseAnnotation).Property == (o as CodeCollapseAnnotation).Property) != 1);

                CodeCollapseAnnotation oldCca = null;
                foreach (CodeCollapseAnnotation cca in collapseAnnotations)
                {
                    cca.Order = cc.Annotations.Count;
                    if (cc.Annotations.Count(ca =>ca is CodeCollapseAnnotation && (ca as CodeCollapseAnnotation).Property == cca.Property) == 0)
                        cc.Annotations.Add(cca);
                    oldCca = cca;
                }
                
            }
            else
                cc.Annotations.Add(new CodeCollapseAnnotation() { Name = candidate.Name, Order = 1, Property = candidate as Property });


            // Determine a better name
            if (CorCollapserPipelineTrigger.collapseSpecialNaming)
            {
                // We collapse the collapsee's name into the parent by default.
                string preferredName = candidate.Name;

                // If the container for the property already has an element by the name we prefer, we must 
                // or the collapsee's name is in the useless words list we must keep the current name, or if the 
                // parent's name is in the structurally important collection
                if((cc.Container as Class).GetFullContent().Find(o=>o.Name == preferredName) != null || CorCollapserPipelineTrigger.uselessWords.Contains(preferredName)
                    || CorCollapserPipelineTrigger.importantWords.Contains(cc.Name))
                    preferredName = cc.Name;

                cc.Name = preferredName;
            }

            // Now determine if we can collapse
            (cc as Property).Type = (candidate as Property).Type.Clone() as TypeReference;
            (cc as Property).SupplierDomain = (candidate as Property).SupplierDomain;
            (cc as Property).SupplierStrength = (candidate as Property).SupplierStrength;
            (cc as Property).Type.Container = cc;
            (cc as Property).Type.MemberOf = cc.MemberOf;

            // Update documentation
            if((cc as Property).Documentation == null && 
                candidate.Documentation != null)
                (cc as Property).Documentation = candidate.Documentation;

        }

        /// <summary>
        /// Is this a candidate for collapsing meaningless complexity
        /// </summary>
        private bool IsCandidateForMeaninglessComplexityCollapse(ClassContent cc)
        {
            if(!(cc is Property) || (cc as Property).Type.Class == null) return false; // Not a candidate

            // The condition for this type of collection is that the referenced type:
            //  1. Provides only ONE property (after --collapse-ignore are removed)
            //  2. The ONE property is mandatory (must be provided)
            List<ClassContent> content = (cc as Property).Type.Class.GetFullContent();
            content.RemoveAll(a => CorCollapserPipelineTrigger.collapseIgnore.Contains(a.Name)); // Remove the ignore

            if (CorCollapserPipelineTrigger.collapseIgnoreFixed)
                content.RemoveAll(o => o is Property && !String.IsNullOrEmpty((o as Property).FixedValue) && (o as Property).PropertyType == Property.PropertyTypes.Structural);

            if (content.Count != 1) return false; // can't collapse this relation as there is more than sub-property
            
            ClassContent candidate = content[0];

            // Is this a generic parameter
            if ((candidate.Container as Class).TypeParameters != null && (cc.Container as Class).TypeParameters.Find(o => o.ParameterName == (candidate as Property).Type.Name) != null)
                return false;

            // Now determine if we can collapse
            return (candidate.Conformance == ClassContent.ConformanceKind.Mandatory
                || candidate.Conformance >= cc.Conformance
                || (cc.Conformance <= ClassContent.ConformanceKind.Required
                && candidate.Conformance == ClassContent.ConformanceKind.Populated)) // Experiment: && candidate.Conformance != ClassContent.ConformanceKind.Populated) 
                && candidate.MaxOccurs == "1"
                && candidate is Property;
        }

        /// <summary>
        /// Copy members from <paramref name="typeReference"/> into the class <paramref name="p"/> in the context of the 
        /// <paramref name="context"/>
        /// </summary>
        private void CopyMembers(TypeReference typeReference, Class p, ClassContent context)
        {
            if(typeReference.Class == null) throw new InvalidOperationException("Impossible condition to proceed");
            foreach (ClassContent cc in typeReference.Class.Content)
            {
                // Can't use AddContent as that sorts the content and would mess up the outer loops
                ClassContent newCc = cc.Clone() as ClassContent;
                newCc.Container = p;
                newCc.MemberOf = p.MemberOf;
                newCc.Annotations.Add(new CodeCollapseAnnotation() { Name = context.Name, Order = 0, Property = context });
                newCc.Annotations.Add(new CodeCollapseAnnotation() { Name = newCc.Name, Order = 1, Property =  newCc});

                // Assign code collapse attributes
                p.Content.Add(newCc);
            }
        }

        /// <summary>
        /// Determine if the class content is a candidate for collapsing
        /// </summary>
        private bool IsCandidateForTypeCollapse(ClassContent cc)
        {
            // Candidates must be a property with 0..1 and must be 
            // traversable in nature.
            if (cc.MaxOccurs != "1" || !(cc is Property) || 
                (cc as Property).PropertyType != Property.PropertyTypes.TraversableAssociation ||
                cc.MinOccurs != "0")
                return false;

            // Determine the conformance meets criteria
            if((cc.Conformance == ClassContent.ConformanceKind.Optional || cc.Conformance == ClassContent.ConformanceKind.Required)
                && !QueryConformance((cc as Property).Type, ClassContent.ConformanceKind.Optional | ClassContent.ConformanceKind.Required))
                        return false;
            else if (cc.Conformance == ClassContent.ConformanceKind.Populated)
                return false;

            // Make sure none of the child member names appear in the parent class they are to be merged with
            bool isCandidate = CountReferences((cc as Property).Type) == 1;
            foreach(ClassContent content in (cc.Container as Class).Content)
                isCandidate &= HasMember((cc as Property).Type, content.Name);
            // Now we must determine if there are no other references to the class to be collapsed
            return isCandidate;
        }

        /// <summary>
        /// Returns true if <paramref name="typeReference"/> has a member called <paramref name="p"/>
        /// </summary>
        private bool HasMember(TypeReference typeReference, string p)
        {
            if(typeReference.Class == null) return false;
            foreach (ClassContent cc in typeReference.Class.Content)
                if (cc.Name.Equals(p))
                    return false;
            return true;
        }

        /// <summary>
        /// Query conformance
        /// </summary>
        private bool QueryConformance(TypeReference typeReference, ClassContent.ConformanceKind p)
        {
            if(typeReference.Class == null) return false;
            bool retVal = true;
            // Class content
            foreach (ClassContent cc in typeReference.Class.GetFullContent())
                if (cc is Property)
                    retVal &= ((cc.Conformance & p) == cc.Conformance);
                else if (cc is Choice)
                    retVal &= QueryConformance(cc as Choice, typeReference, p);

            return retVal;
        }

        /// <summary>
        /// Query the conformance for a choice
        /// </summary>
        private bool QueryConformance(Choice chc, TypeReference typeReference, ClassContent.ConformanceKind p)
        {
            bool retVal = true;
            foreach (ClassContent cc in chc.Content)
                if (cc is Property)
                    retVal &= ((cc.Conformance & p) == cc.Conformance);
                else if (cc is Choice)
                    retVal &= QueryConformance(cc as Choice, typeReference, p);
            return retVal;
        }

        /// <summary>
        /// Replace references in the repository from <paramref name="s"/> to 
        /// <paramref name="f"/>
        /// </summary>
        private void ReplaceReferences(Class s, Class f)
        {
            // Get all classes
            var classes = from cls in Repository
                          where cls.Value is Class
                          select cls.Value as Class;
            string fqnF = String.Format("{0}.{1}", f.ContainerName, f.Name),
                fqnS = String.Format("{0}.{1}", s.ContainerName, s.Name);

            // Iterate through classes and update the references
            foreach (var cls in classes)
            {
                // Replace the base class
                if (cls.BaseClass != null && cls.BaseClass.Name == fqnS)
                    cls.BaseClass.Name = fqnF;

                // Replace all references
                foreach (ClassContent cc in cls.Content)
                    ReplaceReferences(s, f, cc);

                // Replace sub classes
                if (cls.SpecializedBy != null)
                {
                    foreach (TypeReference tr in cls.SpecializedBy)
                        UpdateReference(tr, fqnS, fqnF);
                }
            }

            // Iterate through interactions and update any references
            var interactions = from cls in Repository
                               where cls.Value is Interaction 
                               select cls.Value as Interaction;
            foreach (var interaction in interactions)
                UpdateReference(interaction.MessageType, fqnS, fqnF);

            // Iterate through type references (CMETS) and update any referneces
            var cmets = from cls in Repository
                        where cls.Value is CommonTypeReference && (cls.Value as CommonTypeReference).Class.Name == fqnS
                        select cls.Value as CommonTypeReference;
            foreach (var cmet in cmets)
                UpdateReference(cmet.Class, fqnS, fqnF);
                
        }

        /// <summary>
        /// Count the references to a particular type reference
        /// </summary>
        private int CountReferences(TypeReference tr)
        {
            // Select class from the repository
            var classes = from cls in Repository
                          where cls.Value is Class
                          select cls.Value as Class;

            int retVal = 0;

            // Search the classes
            foreach (var cls in classes)
                foreach (ClassContent cc in cls.Content)
                    retVal += CountReferences(cc, tr);

            return retVal; // Return the class count
        }

        /// <summary>
        /// Count references of the type in the specified class content
        /// </summary>
        private int CountReferences(ClassContent cc, TypeReference tr)
        {
            int retVal = 0;
            FeatureComparer fc = new FeatureComparer();
            if (cc is Property && fc.CompareTypeReference((cc as Property).Type, tr) == 0)
                retVal++;
            else if (cc is Choice)
                foreach (ClassContent scc in (cc as Choice).Content)
                    retVal += CountReferences(scc, tr);
            return retVal;
                
        }

        /// <summary>
        /// Update reference
        /// </summary>
        private void UpdateReference(TypeReference typeReference, string fqnS, string fqnF)
        {
            if (typeReference.Name == fqnS)
                typeReference.Name = fqnF;
            if(typeReference.GenericSupplier != null)
                foreach (TypeReference tr in typeReference.GenericSupplier)
                    UpdateReference(tr, fqnS, fqnF);
        }

        /// <summary>
        /// Replace references in the class content <paramref name="cc"/> from <paramref name="s"/>
        /// to <paramref name="f"/>
        /// </summary>
        private void ReplaceReferences(Class s, Class f, ClassContent cc)
        {
            string fqnF = String.Format("{0}.{1}", f.ContainerName, f.Name),
                fqnS = String.Format("{0}.{1}", s.ContainerName, s.Name);
            if (cc is Property && (cc as Property).Type.Name == fqnS)
                UpdateReference((cc as Property).Type, fqnS, fqnF);
            else if (cc is Choice)
                foreach (var c in (cc as Choice).Content)
                    ReplaceReferences(s, f, c);

            // Alternate traversals
            if(cc is Property)
                foreach (var alt in (cc as Property).AlternateTraversalNames ?? new List<Property.AlternateTraversalData>())
                    if (alt.CaseWhen.Name.Equals(fqnS))
                    {
                        alt.CaseWhen.Name = fqnF;
                        alt.CaseWhen.MemberOf = s.MemberOf;
                    }

            
        }

        #endregion
    }
}
