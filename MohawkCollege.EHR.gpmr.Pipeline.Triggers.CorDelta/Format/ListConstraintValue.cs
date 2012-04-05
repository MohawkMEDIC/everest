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
 * User: Justin Fyfe
 * Date: 09-26-2011
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Format
{
    /// <summary>
    /// A constraint value which is represented as a list
    /// </summary>
    [XmlType("StringListValue", Namespace = "urn:infoway.ca/deltaSet")]
    public class ListConstraintValue : ConstraintValueBase
    {
        /// <summary>
        /// The original list of strings
        /// </summary>
        [XmlElement("original")]
        public List<String> Original { get; set; }
        /// <summary>
        /// The new list of strings
        /// </summary>
        [XmlElement("new")]
        public List<String> New { get; set; }

    }
}
