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
    /// A type of concept property supported both by the containing code system either for concepts or for 
    /// codes
    /// </summary>
    [XmlType(TypeName = "SupportedProperty", Namespace = "urn:hl7-org:v3/mif2")]
    public class SupportedProperty
    {
        /// <summary>
        /// Identifies what sort of relationship is supported
        /// </summary>
        [XmlAttribute("propertyName")]
        public string PropertyName { get; set; }
        /// <summary>
        /// Identifies the allowed content type for the property
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods"), XmlAttribute("type")]
        public ConceptPropertyTypeKind Type { get; set; }
        /// <summary>
        /// Identifies whether the property must be specified for all concepts 
        /// within the code system
        /// </summary>
        [XmlAttribute("isMandatoryIndicator")]
        public bool IsMandatoryIndicator { get; set; }
        /// <summary>
        /// Identifies the default value for the property, if it is not specified.
        /// </summary>
        [XmlAttribute("defaultValue")]
        public string DefaultValue { get; set; }
        /// <summary>
        /// Describes how the property is intended to be used and what it's for
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays"), XmlElement("description")]
        public XmlNode[] Description { get; set; }
        /// <summary>
        /// Identifies the allowed value for a property with a type of enumeration
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("enumerationValue")]
        public List<string> EnumerationValue { get; set; }
    }
}