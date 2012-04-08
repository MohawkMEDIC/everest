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
using MohawkCollege.EHR.gpmr.COR;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Format
{
    /// <summary>
    /// Represents a constraint
    /// </summary>
    [XmlType("Constraint", Namespace = "urn:infoway.ca/deltaSet")]
    public class Constraint : BaseXmlType
    {

        /// <summary>
        /// Represents the value of the constraint
        /// </summary>
        [XmlElement("annotationValue", typeof(AnnotationConstraintValue))]
        [XmlElement("numericValue", typeof(ConstraintValue<Int32>))]
        [XmlElement("stringListValue", typeof(ListConstraintValue))]
        [XmlElement("stringValue", typeof(ConstraintValue<String>))]
        [XmlElement("vocabularyValue", typeof(VocabularyConstraintValue))]
        [XmlElement("cardinalityValue", typeof(CardinalityConstraintValue))]
        [XmlElement("removeValue", typeof(RemoveConstraintValue))]
        public ConstraintValueBase Value  { get; set; }

        /// <summary>
        /// Identifies the type of constraint
        /// </summary>
        [XmlAttribute("type")]
        public ConstraintDeltaType Type { get; set; }

    }
}
