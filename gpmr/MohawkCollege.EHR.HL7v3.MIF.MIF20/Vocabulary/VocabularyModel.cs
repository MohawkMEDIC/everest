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
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// A package containing information about the vocabulary artifacts defined by or 
    /// used within a namespace realm. May include vocabulary domains code systems, 
    /// value sets and/or runtime binding definitions
    /// </summary>
    [XmlType(TypeName = "VocabularyModel", Namespace = "urn:hl7-org:v3/mif2")]
    public class VocabularyModel : Package
    {
        /// <summary>
        /// Identifies whter this rendering of the model represents a complete definition
        /// a partial definition, or merely a used vocabulary associated with the 
        /// underlying model
        /// </summary>
        [XmlAttribute("definitionKind")]
        public VocabularyModelDefinitionKind DefinitionKind { get; set; }

        /// <summary>
        /// Descriptive information about the vocabulary model
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations { get; set; }

        /// <summary>
        /// Identifies a vocabulary model on whose contents the current model depends
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Vocab"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("dependsOnVocabModel")]
        public List<ArtifactDependency> DependsOnVocabModel { get; set; }

        /// <summary>
        /// Information about a vocabulary domain that constrains the value 
        /// of one or more coded attributes in a static model
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("conceptDomain")]
        public List<ConceptDomain> ConceptDomain { get; set; }

        /// <summary>
        /// Information about a code system that is referenced by one or more value sets
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("codeSystem")]
        public List<CodeSystem> CodeSystem { get; set; }

        /// <summary>
        /// Information about a value set that implements one or more
        /// vocabulary domains in a specified context
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("valueSet")]
        public List<ValueSet> ValueSet { get; set; }

        /// <summary>
        /// A collection of binding realms relevant to this vocabulary model
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("bindingRealm")]
        public List<BindingRealm> BindingRealm { get; set; }

        /// <summary>
        /// Information about a value set that implements one or more vocabulary 
        /// domains in a specified context
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("contextBinding")]
        public List<ContextBinding> ContextBinding { get; set; }

        /// <summary>
        /// Captures known translations between codes from difference code systems
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("codeTranslations")]
        public List<CodeTranslationCollection> CodeTranslations { get; set; }

        /// <summary>
        /// Information about an extension to a code system that is referenced by one or more
        /// value sets. Created to allow code system extensions to be maintained in seperate files
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("codeSystemSupplement")]
        public List<CodeSystemSupplement> CodeSystemSupplement { get; set; }

        internal override void Initialize()
        {
            // No initialization required
        }

        //internal override void Merge(PackageArtifact pkg)
        //{
        //    if (!(pkg is VocabularyModel))
        //        throw new NotImplementedException("Can't merge vocabulary model with a different package type");
        //    VocabularyModel dest = pkg as VocabularyModel;
        //    this.CodeSystem.AddRange(dest.CodeSystem ?? new List<CodeSystem>());
        //    this.CodeSystemSupplement.AddRange(dest.CodeSystemSupplement ?? new List<CodeSystemSupplement>());
        //    this.CodeTranslations.AddRange(dest.CodeTranslations ?? new List<CodeTranslation>());
        //    this.ContextBinding.AddRange(dest.ContextBinding ?? new List<ContextBinding>());
        //    this.DependsOnVocabModel.AddRange(dest.DependsOnVocabModel ?? new List<PackageRef>());
        //    this.BindingRealm.AddRange(dest.BindingRealm ?? new List<BindingRealm>());

        //}
    }
}
