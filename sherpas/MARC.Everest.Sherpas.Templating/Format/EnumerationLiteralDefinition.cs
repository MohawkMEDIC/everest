/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
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
 * User: fyfej
 * Date: 4-2-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a literal within a templated enumeration
    /// </summary>
    [XmlType("EnumerationLiteralDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class EnumerationLiteralDefinition
    {
        /// <summary>
        /// Represents the literal in the enumeration
        /// </summary>
        [XmlAttribute("literalName")]
        public string Literal { get; set; }

        /// <summary>
        /// Represents the domain from which code is drawn
        /// </summary>
        [XmlAttribute("supplierDomain")]
        public string CodeSystem { get; set; }

        /// <summary>
        /// Represents the code 
        /// </summary>
        [XmlAttribute("code")]
        public string Code { get; set; }

        /// <summary>
        /// Represents the human name of the code
        /// </summary>
        [XmlAttribute("displayName")]
        public string DisplayName { get; set; }

    }
}
