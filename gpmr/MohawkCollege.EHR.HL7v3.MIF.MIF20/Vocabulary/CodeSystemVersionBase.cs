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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// Identifies a specific published version of a code system or code system
    /// supplement
    /// </summary>
    [XmlType(TypeName = "CodeSystemVersionBase", Namespace = "urn:hl7-org:v3/mif2")]
    public class CodeSystemVersionBase : ModelElement
    {
        /// <summary>
        /// The date on which this particular version of the code system was published as 
        /// recognized by HL7
        /// </summary>
        [XmlAttribute("releaseDate")]
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// Inidcates whether the code system version has been approved for use in HL7.
        /// Reasons for lack of approval should be captured in an annotation. Non-embraced
        /// code system should not be used in HL7 bindings
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Hl"), XmlAttribute("hl7ApprovedIndicator")]
        public bool Hl7ApprovedIndicator { get; set; }
        /// <summary>
        /// Identifies whether HL7 international or an HL7 affiliate
        /// has responsibility for maintaining the code system version
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Hl"), XmlAttribute("hl7MaintainedIndicator")]
        public bool Hl7MaintainedIndicator { get; set; }
        /// <summary>
        /// Version included here is a version id of external code system
        /// </summary>
        [XmlAttribute("publisherVersionId")]
        public string PublisherVersionId { get; set; }
        /// <summary>
        /// Descriptive information about the code system
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations { get; set; }
        /// <summary>
        /// Reference to an external server able to access the content of this 
        /// code system
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("publicTerminologyServer")]
        public List<string> PublicTerminologyServer { get; set; }
        /// <summary>
        /// Inidcates one of the languages supported by the specified value-set
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("supportedLanguage")]
        public List<string> SupportedLanguage { get; set; }
    }
}