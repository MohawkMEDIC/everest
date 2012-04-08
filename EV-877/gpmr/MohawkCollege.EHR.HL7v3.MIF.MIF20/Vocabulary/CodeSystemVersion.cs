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
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// Identifies a specific published version of a code system
    /// </summary>
    [XmlType(TypeName = "CodeSystemVersion", Namespace = "urn:hl7-org:v3/mif2")]
    public class CodeSystemVersion : CodeSystemVersionBase
    {
        /// <summary>
        /// Indicates whether the complete set of codes are documented here or
        /// whehter the listed codes represent only a partial set.
        /// </summary>
        [XmlAttribute("completeCodesIndicator")]
        public bool CompleteCodesIndicator { get; set; }
        /// <summary>
        /// Identifies a type of relationship between codes that are supported by this 
        /// code system version containing coded concept and another concept
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("supportedConceptRelationship")]
        public List<SupportedConceptRelationship> SupportedConceptRelationship { get; set; }
        /// <summary>
        /// Identifies a type of supplimental property that may/must be associated with
        /// a code system concept
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("supportedConceptProperty")]
        public List<SupportedConceptProperty> SupportedConceptProperty { get; set; }
        /// <summary>
        /// Identifies a type of supplemental property that may/must be associated
        /// with a code system code
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("supportedCodeProperty")]
        public List<SupportedProperty> SupportedCodeProperty { get; set; }
        /// <summary>
        /// A single encoded concept that is represented in the code system
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("concept")]
        public List<Concept> Concept { get; set; }
    }
}