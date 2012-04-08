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
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.DynamicModel
{
    /// <summary>
    /// Extends interaction for use as a stand-alone document instance
    /// </summary>
    [XmlRoot(ElementName = "interaction", Namespace = "urn:hl7-org:v3/mif2")]
    [XmlType(TypeName = "GlobalInteraction", Namespace = "urn:hl7-org:v3/mif2")]
    public class GlobalInteraction : Interaction
    {

        private string schemaVersion;

        /// <summary>
        /// Identifies the version of the schema that this interaction is claiming conformance against
        /// </summary>
        [XmlAttribute("schemaVersion")]
        public string SchemaVersion
        {
            get { return schemaVersion; }
            set { schemaVersion = value; }
        }

        /// <summary>
        /// Initialize the owned entry point
        /// </summary>
        internal override void Initialize()
        {
        }
    }
}