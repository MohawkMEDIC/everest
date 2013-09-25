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
    /// A specific code or mnemonic that is part of a code system
    /// </summary>
    [XmlType(TypeName = "Code", Namespace = "urn:hl7-org:v3/mif2")]
    public class Code
    {
        /// <summary>
        /// The mnemonic or id of the code that is supported by the code system
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "CodeName"), XmlAttribute("code")]
        public string CodeName { get; set; }
        /// <summary>
        /// Indicates the first date on which the code is expected to be used
        /// </summary>
        [XmlAttribute("effectiveDate")]
        public DateTime EffectiveDate { get; set; }
        /// <summary>
        /// Indicates that last date on which the code is considered valid to be used
        /// </summary>
        [XmlAttribute("retirementDate")]
        public DateTime RetirementDate { get; set; }
        /// <summary>
        /// Indicates whether the concept is intended to be currently used
        /// </summary>
        [XmlAttribute("status")]
        public CodeStatusKind Status { get; set; }
        /// <summary>
        /// A reference to a pre-defined group of property values that apply to this code
        /// </summary>
        [XmlAttribute("propertyGroup")]
        public string PropertyGroup { get; set; }
        /// <summary>
        /// Preferred and alternative print names for this particular code in a specified language
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("printName")]
        public List<PrintName> PrintName { get; set; }
        /// <summary>
        /// An additional property of the concept expressed as a coded name within an associated
        /// value
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("codeProperty")]
        public List<ConceptDomainProperty> CodeProperty { get; set; }
    }
}