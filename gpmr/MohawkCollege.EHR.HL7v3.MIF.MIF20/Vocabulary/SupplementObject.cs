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
 * User: Justin Fyfe
 * Date: 02-28-2012
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// A reference to an object that is being supplemented
    /// </summary>
    [XmlType("SupplementObject", Namespace = "urn:hl7-org:v3/mif2")]
    public class SupplementObject
    {

        /// <summary>
        /// Gets or sets the name of the object
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Parameters to locate the object. 
        /// </summary>
        /// <remarks>
        /// This doesn't seem like a really good way of doing this, but it is the spec 
        /// that we must follow
        /// </remarks>
        [XmlElement("param", Namespace = "http://www.w3.org/1999/xhtml")]
        public List<Param> Params { get; set; }
    }
}
