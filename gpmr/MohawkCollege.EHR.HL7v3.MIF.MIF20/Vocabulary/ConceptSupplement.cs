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
    /// An extension to a single concept represented in the referenced code system
    /// </summary>
    [XmlType(TypeName = "ConceptSupplement", Namespace = "urn:hl7-org:v3/mif2")]
    public class ConceptSupplement: ConceptBase
    {
        /// <summary>
        /// Specifies one of the code for the concept in the code system allowing the concept to be 
        /// referenced.
        /// </summary>
        [XmlAttribute("code")]
        public string Code { get; set; }
        /// <summary>
        /// Descriptive information about the coded concept
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations { get; set; }
        /// <summary>
        /// An additional relationship between this concept and another coded concept in the
        /// same code system
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("supplementalConceptRelationship")]
        public List<ConceptRelationship> SupplementalConceptRelationship { get; set; }
        /// <summary>
        /// An additional property of the concept expressed as a coded name with an associated
        /// value
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("supplementalConceptProperty")]
        public List<ConceptDomainProperty> SupplementalConceptProperty { get; set; }
        /// <summary>
        /// Preferred and alternative print names for this concept that are independent of
        /// code in a specified language
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("printName")]
        public List<PrintName> PrintName { get; set; }
        /// <summary>
        /// Identifies supplements to the codes that may be used to represent the concept
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("codeSupplement")]
        public List<CodeSupplement> CodeSupplement { get; set; }
    }
}