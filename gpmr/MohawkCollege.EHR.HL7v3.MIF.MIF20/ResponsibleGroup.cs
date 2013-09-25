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
 * User: Justin Fyfe
 * Date: 02-28-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Identifies organizations that are responsible for the maintenance of a vocabulary
    /// </summary>
    [XmlType("ResponsibleGroup", Namespace = "urn:hl7-org:v3/mif2")]
    public class ResponsibleGroup
    {

        /// <summary>
        /// Identifies the technical committee that is responsible for the vocabulary item
        /// </summary>
        [XmlAttribute("groupName")]
        public string GroupName { get; set; }

        /// <summary>
        /// Identifies the name of the organization that is responsible for maintaining the vocabulary
        /// </summary>
        [XmlAttribute("organizationName")]
        public string OrganizationName { get; set; }

    }
}
