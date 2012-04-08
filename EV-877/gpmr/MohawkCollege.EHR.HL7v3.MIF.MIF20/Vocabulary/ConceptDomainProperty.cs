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
    /// A property of a concept domain expressed as a coded name with an associated value
    /// </summary>
    [XmlType(TypeName = "ConceptDomainProperty", Namespace = "urn:hl7-org:v3/mif2")]
    public class ConceptDomainProperty
    {
        /// <summary>
        /// Identifies the name of the property being specified
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Identifies the value of the property for this concept domain
        /// </summary>
        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}