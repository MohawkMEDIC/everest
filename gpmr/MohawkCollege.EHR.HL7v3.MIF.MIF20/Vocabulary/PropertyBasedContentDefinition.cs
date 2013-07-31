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
    /// Filters the codes to include based on properties. If includeProperties are specified, 
    /// only those having all the included properties and not having any of the excluded properties
    /// are included. Otherwise all those codes without any of the excluded properties are included.
    /// </summary>
    [XmlType(TypeName = "PropertyBasedContentDefinition", Namespace = "urn:hl7-org:v3/mif2")]
    public class PropertyBasedContentDefinition
    {
        /// <summary>
        /// Indicates that the value set should be filtered to only include codes for concepts
        /// from the code system with the specified properties
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("includeWithConceptProperty")]
        public List<ConceptPropertyRef> IncludeWithConceptProperty { get; set; }
        /// <summary>
        /// Indicates that the value set should be filtered to only include codes for concepts 
        /// from the code system that do not have the specified properties
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("excludeWithConceptProperty")]
        public List<ConceptPropertyRef> ExcludeWithConceptProperty { get; set; }
        /// <summary>
        /// Indicates that the value set should be filtered to only include codes from the code
        /// system with the specified properties
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("includeWithCodeProperty")]
        public List<ConceptPropertyRef> IncludeWithCodeProperty { get; set; }
        /// <summary>
        /// Indicates that the value set should be filtered to only include codes from 
        /// the code system that do not have the specified properties
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("excludeWithCodeProperty")]
        public List<ConceptPropertyRef> ExcludeWithCodeProperty { get; set; }

    }
}