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
using MohawkCollege.EHR.HL7v3.MIF.MIF10.StaticModel.Serialized;
using MohawkCollege.EHR.HL7v3.MIF.MIF10.StaticModel;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10
{

    /// <summary>
    /// A full blown class, or a reference to an already described class
    /// </summary>
    [XmlType(TypeName = "ClassOrReference", Namespace = "urn:hl7-org:v3/mif")]
    public class ClassOrReference
    {

        private object content;

        /// <summary>
        /// What is being defined in this class
        /// </summary>
        [XmlElement(ElementName = "class", Type = typeof(SerializedClass))]
        [XmlElement(ElementName = "commonModelElementRef", Type = typeof(SerializedCommonModelElementRef))]
        [XmlElement(ElementName = "templateParameter", Type = typeof(StaticModelClassTemplateParameter))]
        [XmlElement(ElementName = "reference", Type = typeof(LocalClassReference))]
        public object Content
        {
            get { return content; }
            set { content = value; }
        }
	

    }
}