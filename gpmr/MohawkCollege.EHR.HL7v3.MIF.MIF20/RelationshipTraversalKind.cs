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
using System.ComponentModel;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Identifies the filter to apply when selecting codes by traversing a relationship
    /// </summary>
    [XmlType(TypeName = "RelationshipTraversalKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum RelationshipTraversalKind
    {
        /// <summary>
        /// All codes walking the transitive closure of the specified relationship are included
        /// </summary>
        [Description("All codes walking the transitive closure of the specified relationship are included")]
        TransitiveClosure,
        /// <summary>
        /// All codes walking the transitive closure of the specified relationship
        /// where the code has no outbound relationship of the specified type.
        /// </summary>
        [Description("All codes walking the transitive closure of the specified relationship where the code has no outbound relationship of the specified type.")]
        TransitiveClosureLeaves,
        /// <summary>
        /// Included to support the MIF vocab models
        /// </summary>
        [Description("Included to support the MIF vocab models")]
        DirectRelationsOnly
    }
}