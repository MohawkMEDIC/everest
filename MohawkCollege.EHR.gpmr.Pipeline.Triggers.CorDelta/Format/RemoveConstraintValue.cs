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
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Format
{
    /// <summary>
    /// Removes the constraint
    /// </summary>
    [XmlType("RemoveConstraint", Namespace = "urn:infoway.ca/deltaSet")]
    public class RemoveConstraintValue : ConstraintValueBase
    {
        /// <summary>
        /// Owned entry point
        /// </summary>
        [XmlAttribute("ownedEntryPoint")]
        public string OwnedEntryPoint { get; set; }
        /// <summary>
        /// Name of the class that is being removed from
        /// </summary>
        [XmlAttribute("className")]
        public string ClassName { get; set; }
        /// <summary>
        /// The name of the relationship to be removed
        /// </summary>
        [XmlAttribute("relationshipName")]
        public string RelationshipName { get; set; }
    }
}
