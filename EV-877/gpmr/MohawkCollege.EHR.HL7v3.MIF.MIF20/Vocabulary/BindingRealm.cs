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
using System.Xml;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// Identifies a particular binding realm
    /// </summary>
    [XmlType(TypeName = "BindingRealm", Namespace = "urn:hl7-org:v3/mif2")]
    public class BindingRealm : ModelElement
    {
        /// <summary>
        /// A formal name for the binding realm
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        /// <summary>
        /// The affiliate responsible for the binding realm
        /// </summary>
        [XmlAttribute("owningAffiliate")]
        public AffiliateKind OwningAffiliate { get; set; }
        /// <summary>
        /// A description of the purpose of the binding realm
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays"), XmlElement("description")]
        public XmlNode[] Description { get; set; }

    }
}