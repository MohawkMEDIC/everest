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
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// The allowed derivation relationships between two artifacts.
    /// </summary>
    [XmlType(TypeName = "DerivationRelationshipKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum DerivationRelationshipKind
    {
        /// <summary>
        /// The current element is a proper subset of the element it was derived from but is not
        /// identical
        /// </summary>
        [XmlEnum("restriction")]
        Restriction,
        /// <summary>
        /// The element derived from is a proper subset of this element but is not identical
        /// </summary>
        [XmlEnum("extension")]
        Extension, 
        /// <summary>
        /// Neither the element nor the element derived from are proper subsets of the other
        /// </summary>
        [XmlEnum("conflicting")]
        Conflicting,
        /// <summary>
        /// The element is unchanged from the element it is derived from with the exception of descriptive
        /// annotations
        /// </summary>
        [XmlEnum("annotated")]
        Annotated,
        /// <summary>
        /// The element is unchanged from the element it is derived from
        /// </summary>
        [XmlEnum("unchanged")]
        Unchanged
    }
}