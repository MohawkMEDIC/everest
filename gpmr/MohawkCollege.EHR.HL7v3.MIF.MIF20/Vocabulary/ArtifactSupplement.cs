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
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// Defines additional characters that add or supplement objects
    /// </summary>
    [XmlType("ArtifactSupplement", Namespace = "urn:hl7-org:v3/mif2")]
    public class ArtifactSupplement
    {

        /// <summary>
        /// Supplemented object
        /// </summary>
        [XmlElement("supplementedObject")]
        public SupplementObject SupplementedObject { get; set; }
        /// <summary>
        /// Business name supplement
        /// </summary>
        [XmlElement("businessName")]
        public List<BusinessName> BusinessName { get; set; }
        /// <summary>
        /// Annotations for the supplemented object
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations { get; set; }

    }
}
