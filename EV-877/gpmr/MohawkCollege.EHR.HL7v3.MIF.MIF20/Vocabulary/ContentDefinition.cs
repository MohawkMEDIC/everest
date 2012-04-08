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
    /// Represents a value set build up of the inclusion, exclusion and/or union of multiple other value sets. If
    /// no constraints are specified then the content is the complete set of codes in the referenced code
    /// system.
    /// </summary>
    [XmlType(TypeName = "ContentDefinition", Namespace = "urn:hl7-org:v3/mif2")]
    public class ContentDefinition
    {
        /// <summary>
        /// The code system from which the codes are drawn
        /// </summary>
        [XmlAttribute("codeSystem")]
        public string CodeSystem { get; set; }
        /// <summary>
        /// If present, identifies the code system version from which the codes are drawn. If not
        /// specified the current version of the code system is assumed
        /// </summary>
        [XmlAttribute("versionDate")]
        public DateTime VersionDate { get; set; }
        /// <summary>
        /// Descriptive information about the content definition
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations { get; set; }
        /// <summary>
        /// Content derived from multiple other value sets
        /// </summary>
        [XmlElement("combinedContent")]
        public CombinedContentDefinition CombinedContent { get; set; }
        /// <summary>
        /// Content based on a code and its related codes
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("codeBasedContent")]
        public List<CodeBasedContentDefinition> CodeBasedContent { get; set; }
        /// <summary>
        /// Content derived through constraints on the properties of allowed codes
        /// </summary>
        [XmlElement("propertyBasedContent")]
        public PropertyBasedContentDefinition PropertyBasedContent { get; set; }
        /// <summary>
        /// Content derived through explicit filters on code mnemonics
        /// </summary>
        [XmlElement("codeFilterContent")]
        public CodedFilterContentDefinition CodeFilterContent { get; set; }
        /// <summary>
        /// Defines a value-set in a non-computable manner (or at least a manner that is not supported by
        /// MIF representation). This may include free hand descriptions, pseudo expressions or even formal
        /// expressions against some local terminology data model
        /// </summary>
        /// <remarks>
        /// It is an error for structural domains and attributes to be bound to a value set with
        /// non-computable content
        /// </remarks>
        [XmlElement("nonComputableContent")]
        public NonComputableContentDefinition NonComputableContent { get; set; }
        /// <summary>
        /// Content imported from another value set reference
        /// </summary>
        [XmlElement("valueSetRef")]
        public VocabularyValueSetRef ValueSetRef { get; set; }
        /// <summary>
        /// Identifies any constraints on qualifiers for codes associated with this value-set.
        /// </summary>
        [XmlElement("qualifierContent")]
        public ContentDefinition QualifierContent { get; set; }
    }
}