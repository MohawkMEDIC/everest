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
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Interfaces
{
    /// <summary>
    /// Summary of StubDefinition
    /// </summary>
    [XmlType(TypeName = "StubDefinition", Namespace = "urn:hl7-org:v3/mif2")]
    public class StubDefinition : ClassifierBase
    {

        private Package container;

        /// <summary>
        /// The identifier of the stub
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        /// <summary>
        /// Identifies the means by which the stub can be entered
        /// </summary>
        [XmlAttribute("entryKind")]
        public CMETEntryKind EntryKind { get; set; }
        /// <summary>
        /// Identifies the RIM type of other class serving as the base of the CMET
        /// </summary>
        [XmlAttribute("otherClassName")]
        public string OtherClassName { get; set; }
        /// <summary>
        /// Descriptive information about the containing element
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations { get; set; }
        /// <summary>
        /// The ID of the model that represents the outer boundaries of the elements that can bind to this stub
        /// </summary>
        [XmlElement("typeStaticModel")]
        public PackageRef TypeStaticModel { get; set; }
        /// <summary>
        /// The ID of the model that represents the minimum of the elements that can bind to the stub
        /// </summary>
        [XmlElement("constraintStaticModel")]
        public PackageRef ConstraintStaticModel { get; set; }
        /// <summary>
        /// The name of the class from the constraint static model
        /// </summary>
        [XmlElement("entryClass")]
        public String EntryClass { get; set; }


        /// <summary>
        /// The container package
        /// </summary>
        [XmlIgnore()]
        public Package Container
        {
            get { return container; }
            set { container = value; }
        }
    }
}