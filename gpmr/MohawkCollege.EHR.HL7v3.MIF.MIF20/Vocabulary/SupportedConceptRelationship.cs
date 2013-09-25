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
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// A type of concept relationship supported by containing code system
    /// </summary>
    [XmlType(TypeName = "SupportedConceptRelationship", Namespace = "urn:hl7-org:v3/mif2")]
    public class SupportedConceptRelationship
    {
        /// <summary>
        /// Identifies the stereotypical behavior associated with the
        /// relationship
        /// </summary>
        [XmlAttribute("relationshipKind")]
        public ConceptRelationshipKind RelationshipKind { get; set; }
        /// <summary>
        /// The label for the specific type of concept relationship supported by the code
        /// system
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        /// <summary>
        /// Identifies the name of the relationship. Allows linking a relationship and its derived inverse
        /// </summary>
        [XmlAttribute("inverseName")]
        public string InverseName { get; set; }
        /// <summary>
        /// A unique identifier within the code system for this particular relationship type
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }
        /// <summary>
        /// Indicates whether the relationship is intended to be navigated 
        /// when selecting a code
        /// </summary>
        [XmlAttribute("isNavigable")]
        public bool IsNavigable { get; set; }
        /// <summary>
        /// Indicates whether codes are limited to only one out going relationship, 
        /// only one incoming relationship or both
        /// </summary>
        [XmlAttribute("functionalism")]
        public bool Functionalism { get; set; }
        /// <summary>
        /// Indicates if the associate always holds for a concept within itself
        /// </summary>
        [XmlAttribute("reflexivity")]
        public string Reflexivity { get; set; }
        /// <summary>
        /// Inidcates if the relationship always holds in the reverse direction as well.
        /// </summary>
        [XmlAttribute("symmetry")]
        public string Symmetry { get; set; }
        /// <summary>
        /// Indicates whether the relationship always propagates or never propagates.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Transivity"), XmlAttribute("transivity")]
        public string Transivity { get; set; }
        /// <summary>
        /// Describes how the relationship is intended to be used and what its for
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays"), XmlElement("description")]
        public XmlNode[] Description { get; set; }
        /// <summary>
        /// If present, indicates that only codes within the defined value set are allowed
        /// to be the source of this type of relationship
        /// </summary>
        [XmlElement("allowedForSources")]
        public ContentDefinition AllowedForSources { get; set; }
        /// <summary>
        /// If present, indicates that only codes within the defined value set are allowed
        /// to be the target of this type of relationship
        /// </summary>
        [XmlElement("allowedForTargets")]
        public ContentDefinition AllowedForTargets { get; set; }
        /// <summary>
        /// If present, indicates that only codes within the defined value set are allowed
        /// to be the target of this type of relationship
        /// </summary>
        [XmlElement("requiredForSources")]
        public ContentDefinition RequiredForSources { get; set; }
        /// <summary>
        /// If present, indicates that only codes within the defined value set are allowed
        /// to be the target of this type of relationship
        /// </summary>
        [XmlElement("requiredForTargets")]
        public ContentDefinition RequiredForTargets { get; set; }
        /// <summary>
        /// No documentation available
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("supportedProperty")]
        public List<SupportedProperty> SupportedProperty { get; set; }
        /// <summary>
        /// Identifies the concept within the vocabulary that is considered to define this particular relationship
        /// </summary>
        [XmlElement("definingConcept")]
        public ConceptRef DefiningConcept { get; set; }
    }
}