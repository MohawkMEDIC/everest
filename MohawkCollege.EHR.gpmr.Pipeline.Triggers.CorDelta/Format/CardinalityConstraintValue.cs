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
    /// Cardinality constriant value
    /// </summary>
    [XmlType("CardinalityConstraint", Namespace = "urn:infoway.ca/deltaSet")]
    public class CardinalityConstraintValue : ConstraintValueBase
    {
        /// <summary>
        /// Original minimum value
        /// </summary>
        [XmlIgnore]
        public int? OriginalMinValue { get; set; }
        /// <summary>
        /// Original maximum value
        /// </summary>
        [XmlIgnore]
        public int? OriginalMaxValue { get; set; }
        /// <summary>
        /// New minimum value
        /// </summary>
        [XmlIgnore]
        public int? NewMinValue { get; set; }
        /// <summary>
        /// New maximum value
        /// </summary>
        [XmlIgnore]
        public int? NewMaxValue { get; set; }
        /// <summary>
        /// Gets or sets the original minimum cardinality
        /// </summary>
        [XmlAttribute("originalMinValue")]
        public int OriginalMinValueXml { get { return this.OriginalMinValue.Value; } set { this.OriginalMinValue = value; } }
        /// <summary>
        /// Gets or sets the original maximum cardinality
        /// </summary>
        [XmlAttribute("originalMaxValue")]
        public int OriginalMaxValueXml { get { return this.OriginalMaxValue.Value; } set { this.OriginalMaxValue = value; } }
        /// <summary>
        /// Gets or sets the new minimum cardinality
        /// </summary>
        [XmlAttribute("newMinValue")]
        public int NewMinValueXml { get { return this.NewMinValue.Value; } set { this.NewMinValue = value; } }
        /// <summary>
        /// Gets or sets the new maximum cardinality
        /// </summary>
        [XmlAttribute("newMaxValue")]
        public int NewMaxValueXml { get { return this.NewMaxValue.Value; } set { this.NewMaxValue = value; } }

    }
}
