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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// The possible relationships between two mapped elements
    /// </summary>
    [XmlType(TypeName = "MapRelationshipKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum MapRelationshipKind
    {
        /// <summary>
        /// The first concept is at a more abstract level then the second concept
        /// </summary>
        /// <example>
        /// Hepatitis is broader than Hepatitis A and endocrine disease is broader than Diabetes Mellitus
        /// </example>
        [XmlEnum("BT")]
        Broader,
        /// <summary>
        /// The two concepts have identical meaning
        /// </summary>
        [XmlEnum("E")]
        Exact,
        /// <summary>
        /// The first concept is at a more detailed level than the second concept.
        /// </summary>
        /// <example>
        /// Pennicillin G is narrower than Pennicillin and vellus hair is narrower than hair
        /// </example>
        [XmlEnum("NT")]
        Narrower,
        /// <summary>
        /// The fist concept cannot be mapped in any way to the second
        /// </summary>
        /// <example>
        /// Blue cannot be mapped to orange
        /// </example>
        [XmlEnum("NC")]
        NotComparable
    }
}