/* 
 * Copyright 2008/2009 Mohawk College of Applied Arts and Technology
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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10.StaticModel.Flat
{
    /// <summary>
    /// Choice
    /// </summary>
    [XmlType(TypeName = "ClassElement", Namespace = "urn:hl7-org:v3/mif")]
    public class ClassElement
    {
        private ModelElement choice;

        /// <summary>
        /// <para>Class - A set of attributes and associations representing a single instance</para>
        /// <para>CommonModelElementRef - A reference to an external model intended to be imported and re-used at this point</para>
        /// <para>TemplateParameter - A point in the mode at which a sub-model may be placed that corresponds to the identified contentType. 
        /// The specific mode to be included may varied and is deteremined at runtim</para>
        /// </summary>
        [XmlElement(ElementName = "class", Type = typeof(Class))]
        [XmlElement(ElementName = "commonModelElementRef", Type = typeof(CommonModelElementRef))]
        [XmlElement(ElementName = "templateParameter", Type = typeof(StaticModelClassTemplateParameter))]
        public ModelElement Choice
        {
            get { return choice; }
            set { choice = value; }
        }
	
    }
}