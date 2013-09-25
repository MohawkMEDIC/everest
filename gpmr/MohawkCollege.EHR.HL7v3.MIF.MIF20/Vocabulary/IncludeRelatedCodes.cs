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
    /// Defines related codes to be included in a value set
    /// </summary>
    [XmlType(TypeName = "IncludeRelatedCodes", Namespace = "urn:hl7-org:v3/mif2")]
    public class IncludeRelatedCodes 
    {
        /// <summary>
        /// Identifies the type of relationship to trace when including codes
        /// </summary>
        [XmlAttribute("relationshipName")]
        public string RelationshipName { get; set; }
        /// <summary>
        /// Identifies what set of codes to include when traversing the relationship
        /// </summary>
        [XmlAttribute("relationshipTraversal")]
        public RelationshipTraversalKind RelationshipTraversal { get; set; }
    }
}