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
    /// Information about a vocabulary domain that constrains the semantic values of one
    /// or more coded attributes or datatype properties
    /// </summary>
    [XmlType(TypeName = "ConceptDomain", Namespace = "urn:hl7-org:v3/mif2")]
    public class ConceptDomain : ModelElement
    {
        /// <summary>
        /// The unique name of the concept domain
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Indicates whether this domain can be bound to a value-set as part of 
        /// a context binding. If false, the domain can't be bound directly, 
        /// though specializations might be bound. Direct substitution with valuesets
        /// in derived statis models is still possible
        /// </summary>
        [XmlAttribute("isBindable")]
        public bool IsBindable { get; set; }

        /// <summary>
        /// Used for model elements that might have declared sort order but don't have one
        /// </summary>
        [XmlAttribute("sortKey")]
        public string SortKey { get; set; }

        /// <summary>
        /// The business names associated with the element
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("businessName")]
        public List<BusinessName> BusinessName { get; set; }

        /// <summary>
        /// Descriptive information about this vocabulary
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations { get; set; }

        /// <summary>
        /// A reference to another domain that is a proper superset of this
        /// domain
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("specializesDomain")]
        public List<ConceptDomainRef> SpecializesDomain { get; set; }

        /// <summary>
        /// A textual description of a concept considered to be part of this domain. Used
        /// when domains are not bound to a universal, representative or
        /// example value set.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("exampleContent")]
        public List<String> ExampleContent { get; set; }

        /// <summary>
        /// A reference to a property associated with the domain
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("property")]
        public List<ConceptDomainProperty> Property { get; set; }

        /// <summary>
        /// A reference to another domain that is a proper subset of this domain
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("specializedByDomain")]
        public List<ConceptDomainRef> SpecializedByDomain { get; set; }
    }
}