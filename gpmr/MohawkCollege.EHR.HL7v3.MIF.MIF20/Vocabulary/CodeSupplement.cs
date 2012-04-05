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
    /// A supplement to a specific code or mnemonic that is part of the code system
    /// </summary>
    [XmlType(TypeName = "CodeSupplement", Namespace = "urn:hl7-org:v3/mif2")]
    public class CodeSupplement
    {
        /// <summary>
        /// The mnemonic or id of the code being supplemented
        /// </summary>
        [XmlAttribute("code")]
        public string Code { get; set; }
        /// <summary>
        /// Additional alternaitve print names for this particular code in a specified language
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("printName")]
        public List<PrintName> PrintName { get; set; }
        /// <summary>
        /// An additional property of the concept expressed as a coded name with an associated value
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("supplementalCodeProperty")]
        public List<ConceptDomainProperty> SupplementalCodeProperty { get; set; }
    }
}