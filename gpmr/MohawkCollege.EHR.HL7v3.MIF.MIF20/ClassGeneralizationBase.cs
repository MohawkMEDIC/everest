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
    /// Corresponds to a generalization
    /// </summary>
    /// <remarks>
    /// Graphical data has been removed
    /// </remarks>
    [XmlType(TypeName = "ClassGeneralizationBase", Namespace = "urn:hl7-org:v3/mif2")]
    public class ClassGeneralizationBase : Relationship
    {

        private bool isMandatory;
        private ConformanceKind conformance = ConformanceKind.Optional;
        private Annotation annotations;

        /// <summary>
        /// Descriptive information about the containing element
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations
        {
            get { return annotations; }
            set { annotations = value; }
        }
	
        /// <summary>
        /// Identifies the element must be supported by implementers or not
        /// </summary>
        [XmlAttribute("conformance")]
        public ConformanceKind Conformance
        {
            get { return conformance; }
            set { conformance = value; }
        }
	

        /// <summary>
        /// If true, null values may not be sent for this element
        /// </summary>
        [XmlAttribute("isMandatory")]
        public bool IsMandatory
        {
            get { return isMandatory; }
            set { isMandatory = value; }
        }
	

    }
}