/* 
 * Copyright 2010 Mohawk College of Applied Arts and Technology
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
 * User: Justin Fyfe
 * Date: 15-10-2010
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Identifies a name that can be retrieved using a key
    /// </summary>
    [XmlType("KeyedName", Namespace = "urn:hl7-org:v3/mif2")]
    public class KeyedName
    {
        /// <summary>
        /// The key that identifies the keyed name
        /// </summary>
        [XmlAttribute("key")]
        public string Key { get; set; }
        /// <summary>
        /// The name that identifies the keyed name
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
