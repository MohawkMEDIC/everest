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
 * User: $user$
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// Identifies a specific version of a given value set
    /// </summary>
    [XmlType(TypeName = "ValueSetVersion", Namespace = "urn:hl7-org:v3/mif2")]
    public class ValueSetVersion : ModelElement
    {
        /// <summary>
        /// The date on which this particular value set version came into being
        /// </summary>
        [XmlAttribute("versionDate")]
        public DateTime VersionDate { get; set; }
        /// <summary>
        /// The time on which this particular value set version came into being. Only 
        /// needed when multiple versions of a value set are created on a given day
        /// </summary>
        [XmlAttribute("versionTime")]
        public DateTime VersionTime { get; set; }
        /// <summary>
        /// List code system extensions used by this value set
        /// </summary>
        /// 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Suppliment"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("usesCodeSystemSupplement")]
        public List<String> UsesCodeSystemSuppliment { get; set; }
        /// <summary>
        /// Lists code systems used by this value set
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("supportedCodeSystem")]
        public List<String> SupportedCodeSystem { get; set; }
        /// <summary>
        /// Lists languages fully supported(print names for all concepts exist in the
        /// language) used by this value set
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("supportedLanguage")]
        public List<String> SupportedLanguage { get; set; }
        /// <summary>
        /// Concept properties that are associated with this value set version because the property is
        /// DECLARED as value set associatable by the code system from which this value set draws its 
        /// enumerated content
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("associatedConceptProperty")]
        public List<ConceptDomainProperty> AssociatedConceptProperty { get; set; }
        /// <summary>
        /// Defines the codes from a given code system allowed as part of the value set version
        /// </summary>
        [XmlElement("content")]
        public ContentDefinition Content { get; set; }
        /// <summary>
        /// If present, overrides the default selectable characteristics of the codes defined by content
        /// such that all codes are selectable with the exception of those found in this content definition
        /// </summary>
        [XmlElement("nonSelectableContent")]
        public ContentDefinition NonSelectableContent { get; set; }
        /// <summary>
        /// Provides an explicit enumeration of the codes from a given code system associated with the value
        /// set version based on its definition
        /// </summary>
        [XmlElement("enumeratedContent")]
        public VocabularyCodeRefCollection EnumeratedContent { get; set; }
        /// <summary>
        /// A subset of the content of the value set for publication purposes to provide an example of the
        /// codes available in the value set
        /// </summary>
        /// HACK: Real MIFS seem to use a VocabularyCodeRefCollection instead of ContentDefinition
        [XmlElement("exampleContent")]
        public VocabularyCodeRefCollection ExampleContent { get; set; }
    }
}