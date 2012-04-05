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
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// Identifies the codes to be included by describing characteristics of the code mnemonic. Codes must 
    /// match all of the characteristics specified
    /// </summary>
    [XmlType(TypeName = "CodeFilterContentDefinition", Namespace = "urn:hl7-org:v3/mif2")]
    public class CodedFilterContentDefinition
    {
        /// <summary>
        /// Indicates the type of expression to be used to select the codes
        /// </summary>
        [XmlAttribute("expressionType")]
        public CodeFilterExpressionKind ExpressionType { get; set; }
        /// <summary>
        /// Defines a regular expression that must be true when applied to a code
        /// for it to be included in the allowed content. Content MUST be a value regular expression
        /// as used in the w3c schema definition.
        /// </summary>
        [XmlElement("expression")]
        public string Expression { get; set; }
    }
}