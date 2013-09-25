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
    /// A single concept represented in the containing code system
    /// </summary>
    [XmlType(TypeName = "Concept", Namespace = "urn:hl7-org:v3/mif2")]
    public class Concept : ConceptBase
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public Concept()
        {
            IsSelectable = true;
        }

        /// <summary>
        /// If true, indicates that the concept is intended for use in communication. If
        /// false indicates that the concept is NOT intended for use in communication, 
        /// but rather only exists for semantic or navigational purposes
        /// </summary>
        [XmlAttribute("isSelectable")]
        public bool IsSelectable { get; set; }
        /// <summary>
        /// A reference to a pre-defined group of property values that apply to this concept
        /// </summary>
        [XmlAttribute("propertyGroup")]
        public string PropertyGroup { get; set; }
        /// <summary>
        /// Descriptive inforamtion about the coded concept
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations { get; set; }
        /// <summary>
        /// Indicates how the concept is intended to be used
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("intendedUse")]
        public List<ConceptUse> IntendedUse { get; set; }
        /// <summary>
        /// A relationship between this concept and another coded concept in the same code system
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("conceptRelationship")]
        public List<ConceptRelationship> ConceptRelationship { get; set; }
        /// <summary>
        /// An additional property of the concept expressed as a coded name with an associated 
        /// value
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("conceptProperty")]
        public List<ConceptDomainProperty> ConceptProperty { get; set; }
        /// <summary>
        /// Preferred and alternative print names for this concept that are independent of code
        /// in a specified language
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("printName")]
        public List<PrintName> PrintName { get; set; }
        /// <summary>
        /// Identifies the codes that may be used to represent this concept
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("code")]
        public List<Code> Code { get; set; }
    }
}