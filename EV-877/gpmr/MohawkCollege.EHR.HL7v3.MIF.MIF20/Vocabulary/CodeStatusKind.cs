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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// Defines whether the concept is intended to be currently used
    /// </summary>
    [XmlType(TypeName = "CodeStatusKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum CodeStatusKind
    {
        /// <summary>
        /// The code is currently available for use
        /// </summary>
        [XmlEnum("active")]
        Active,
        /// <summary>
        /// The code is under consideration for approval but it should not be used in production systems
        /// </summary>
        [XmlEnum("proposed")]
        Proposed,
        /// <summary>
        /// The code should no longer be used by compliant systems except in transitional situations
        /// </summary>
        [XmlEnum("retired")]
        Retired
    }
}