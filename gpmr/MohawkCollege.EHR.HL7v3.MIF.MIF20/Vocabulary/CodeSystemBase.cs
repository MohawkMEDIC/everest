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
    /// Information about a code system or code system suppliment
    /// </summary>
    [XmlType(TypeName = "CodeSystemBase", Namespace = "urn:hl7-org:v3/mif2")]
    public class CodeSystemBase : PackageBase
    {

        /// <summary>
        /// The descripttive name of the package in cicumstances where name is more of an identifier
        /// </summary>
        [XmlAttribute("title")]
        public string Title { get; set; }
        /// <summary>
        /// The unqiue OID by which the code system identifies within the HL7 message
        /// interfaces
        /// </summary>
        [XmlAttribute("codeSystemId")]
        public string CodeSystemId { get; set; }
        /// <summary>
        /// An estimate of the number of codes in a code system.
        /// </summary>
        [XmlAttribute("approxCodeCount")]
        public int ApproxCodeCount { get; set; }
        /// <summary>
        /// The language in which the code system is primarily maintained
        /// </summary>
        [XmlAttribute("primaryLanguage")]
        public string PrimaryLanguage { get; set; }
        /// <summary>
        /// The realm in which the code system is primarily maintained
        /// </summary>
        [XmlAttribute("primaryRealm")]
        public string PrimaryRealm { get; set; }
        /// <summary>
        /// If true, indicates that some concepts have more than one code
        /// </summary>
        [XmlAttribute("hasSynononymy")]
        public bool HasSynonymy { get; set; }
        /// <summary>
        /// If true, indicates that some codes exist for more than one concept
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Hononymy"), XmlAttribute("hasHononymy")]
        public bool HasHononymy { get; set; }
        /// <summary>
        /// If false, indicates that comparisons to codes are independent of 
        /// whether characters are upper-case or lower-case
        /// </summary>
        [XmlAttribute("isCaseSensitive")]
        public bool IsCaseSensitive { get; set; }
        /// <summary>
        /// If false, indicates that whitespace may be ignored when comparing codes
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Whitespace"), XmlAttribute("isWhitespaceSensitive")]
        public bool IsWhitespaceSensitive { get; set; }

        /// <summary>
        /// General metadata information about the package
        /// </summary>
        [XmlElement("header")]
        public Header Header { get; set; }

        /// <summary>
        /// Descriptive information about the code system
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations { get; set; }

        /// <summary>
        /// Defines a group of properties and their values that commonly appear together 
        /// to simplify serialization of code system by minimizing repetiion of property 
        /// values 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("propertyGroup")]
        public List<PropertyGroup> PropertyGroup { get; set; }
    }
}