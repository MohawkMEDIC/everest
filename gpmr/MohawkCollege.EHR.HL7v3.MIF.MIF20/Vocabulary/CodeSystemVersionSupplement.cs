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
    /// Identifies a specific published version of a code system supplement
    /// </summary>
    [XmlType(TypeName = "CodeSystemVersionSupplement", Namespace = "urn:hl7-org:v3/mif2")]
    public class CodeSystemVersionSupplement : CodeSystemVersionBase
    {
        /// <summary>
        /// The date on which the particular version of the code system being supplemented was published as recognized
        /// by HL7
        /// </summary>
        [XmlAttribute("appliesToReleaseDate")]
        public DateTime AppliesToReleaseDate { get; set; }
        /// <summary>
        /// Identifies a type of sepplemental relationship between codes that is supported by this 
        /// code system version
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("supplementalSupportedConceptRelationship")]
        public List<SupportedConceptRelationship> SupplementalSupportedConceptRelationship { get; set; }
        /// <summary>
        /// Identifies a type of supplemental property that may/must be associated with a code system
        /// concept
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("supplementalSupportedConceptProperty")]
        public List<SupportedProperty> SupplementalSupportedConceptProperty { get; set; }
        /// <summary>
        /// Identifies a type of supplemental property that may/must be associated with a code 
        /// system code
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("supplementalSupportedCodeProperty")]
        public List<SupportedProperty> SupplementalSupportedCodeProperty { get; set; }
        /// <summary>
        /// A supplement to single encoded concept that is represented in the code system
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("conceptSupplement")]
        public List<ConceptSupplement> ConceptSupplement { get; set; }
    }
}