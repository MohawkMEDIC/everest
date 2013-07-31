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
    /// A relationship between the containing coded concept and another concept in the same
    /// or another code system
    /// </summary>
    [XmlType(TypeName = "ConceptRelationship", Namespace = "urn:hl7-org:v3/mif2")]
    public class ConceptRelationship
    {
        /// <summary>
        /// Identifies what kind of relationship is being asserted
        /// </summary>
        [XmlAttribute("relationshipName")]
        public string RelationshipName { get; set; }
        /// <summary>
        /// If true, indicates that the relationship is derivable by a corresponding reverse
        /// relationship from the relationship target. Derived relationships may be included for 
        /// publishing or processing efficiency purposes but should not be presented in definitional
        /// packages.
        /// </summary>
        [XmlAttribute("isDerived")]
        public bool IsDerived { get; set; }
        /// <summary>
        /// A property associated with this particular relationship
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("property")]
        public List<ConceptDomainProperty> Property { get; set; }
        /// <summary>
        /// Identifies the concept to which the relationship is being asserted.
        /// </summary>
        [XmlElement("targetConcept")]
        public ConceptRef TargetConcept { get; set; }
    }
}