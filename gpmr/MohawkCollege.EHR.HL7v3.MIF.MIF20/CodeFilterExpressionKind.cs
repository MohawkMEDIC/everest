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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Identifes a type of expression used to select a set of codes from a code system
    /// </summary>
    [XmlType(TypeName = "CodeFilterExpressionKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum CodeFilterExpressionKind
    {
        /// <summary>
        /// The expression is a regular expression that must be true when applied to a code for it
        /// to be included. Expression must be a regular expression as used in w3c schema definition
        /// </summary>
        [XmlEnum("regex")]
        RegularExpression,
        /// <summary>
        /// The expression is a terminology query language expression as defined in 
        /// <seealso href="http://www.amia.org/pubs/symposia/D200562.pdf"/>
        /// </summary>
        [XmlEnum("tql")]
        TerminologyQueryLanguage

    }
}
