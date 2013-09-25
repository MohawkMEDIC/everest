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
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// Used to reference a coded concept property, possibly with a specific value or matching a given expression.
    /// If neither value nor expression are specified the reference will match all concepts
    /// with the specified property regardless of value.
    /// </summary>
    [XmlType(TypeName = "ConceptPropertyRef", Namespace = "urn:hl7-org:v3/mif2")]
    public class ConceptPropertyRef
    {
        /// <summary>
        /// Identifies the name of the property being referenced
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        /// <summary>
        /// Identifies the value the referenced property must have
        /// </summary>
        [XmlAttribute("value")]
        public string Value { get; set; }
        /// <summary>
        /// Identifies a regular expression that must be matched by the referenced property
        /// </summary>
        [XmlAttribute("expression")]
        public string Expression { get; set; }
    }
}