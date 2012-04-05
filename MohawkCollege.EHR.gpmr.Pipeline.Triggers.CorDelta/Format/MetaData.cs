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
    /// Identifies meta-data about the delta set
    /// </summary>
    [XmlType("metaData", Namespace = "urn:infoway.ca/deltaSet")]
    public class MetaData : BaseXmlType
    {

        /// <summary>
        /// Describes the contents of the deltaset
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }
        /// <summary>
        /// Identifies the rationale for creating the delta
        /// set
        /// </summary>
        [XmlElement("rationale")]
        public string Rationale { get; set; }
        /// <summary>
        /// Identifies the source of the delta-set
        /// </summary>
        [XmlElement("source")]
        public string Source { get; set; }
        /// <summary>
        /// Identifies the version of the standards artifact that the
        /// delta set was created against
        /// </summary>
        [XmlElement("specificationVersion")]
        public string SpecificationVersion { get; set; }

    }
}
