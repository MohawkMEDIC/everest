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
 * Date: 09-26-2011
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Format
{
    /// <summary>
    /// Identifies an entry point that is being modified 
    /// </summary>
    [XmlType("ownedEntryPoint", Namespace = "urn:infoway.ca/deltaSet")]
    public class OwnedEntryPoint : BaseXmlType
    {
        /// <summary>
        /// Identifies the message artifact that is being
        /// constrained
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }
        /// <summary>
        /// A descriptive title for the delta
        /// </summary>
        [XmlAttribute("title")]
        public string Title { get; set; }
        /// <summary>
        /// A list of classes within the container model that
        /// are being constrained.
        /// </summary>
        [XmlElement("classDelta")]
        public List<ClassDeltaData> Delta { get; set; }
    }
}
