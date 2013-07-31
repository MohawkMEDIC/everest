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
    /// Vocabulary constraint value
    /// </summary>
    [XmlType("VocabularyConstraint", Namespace = "urn:infoway.ca/deltaSet")]
    public class VocabularyConstraintValue : ConstraintValueBase
    {
        /// <summary>
        /// Original type of the vocab
        /// </summary>
        [XmlAttribute("originalType")]
        public string OriginalDomain { get; set; }
        /// <summary>
        /// New type of the vocab
        /// </summary>
        [XmlAttribute("newType")]
        public string NewDomain { get; set; }
        /// <summary>
        /// Original source type of the vocab
        /// </summary>
        [XmlAttribute("originalSource")]
        public DomainSourceType OriginalSourceType { get; set; }
        /// <summary>
        /// New source type of the of the vocab
        /// </summary>
        [XmlAttribute("newSource")]
        public DomainSourceType NewSourceType { get; set; }
    }
}
