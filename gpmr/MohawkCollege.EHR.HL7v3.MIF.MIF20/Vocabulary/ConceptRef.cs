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
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// Used to reference one concept from another
    /// </summary>
    [XmlType(TypeName = "ConceptRef", Namespace = "urn:hl7-org:v3/mif2")]
    public class ConceptRef
    {
        /// <summary>
        /// Identifies one of the codes associated with the concept being referenced
        /// </summary>
        [XmlAttribute("code")]
        public string Code { get; set; }
        /// <summary>
        /// Identifies the code system of the code associated with the concept. If not 
        /// present, the code system is assumed to be the code system within the reference that
        /// occurs.
        /// </summary>
        [XmlAttribute("codeSystem")]
        public string CodeSystem { get; set; }
        /// <summary>
        /// Indicates a property the code must have to be considered a match for the reference. Used
        /// when a code system has homonym and the code value alone is not sufficient to reference the 
        /// concept. If multiple code properties are specified all must be true to be considered a match.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("codeProperty")]
        public List<ConceptDomainProperty> CodeProperty { get; set; }
    }
}