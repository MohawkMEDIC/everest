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
using System.Xml;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10
{
    /// <summary>
    /// Allows complex markup to identify the language in which it is expressed
    /// </summary>
    /// <remarks>
    /// Stripped the last translated on attribute
    /// </remarks>
    [XmlType(TypeName = "ComplexMarkupWithLanguage", Namespace = "urn:hl7-org:v3/mif")]
    public class ComplexMarkupWithLanguage
    {
        private string lang;
        private string markup;
        private XmlElement[] markupElements;

        /// <summary>
        /// The other elements in the markup
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays"), XmlAnyElement()]
        public XmlElement[] MarkupElements
        {
            get { return markupElements; }
            set { markupElements = value; }
        }

        /// <summary>
        /// Identifies the content of this markup
        /// </summary>
        [XmlText()]
        public string MarkupText
        {
            get { return markup; }
            set { markup = value; }
        }
	
        /// <summary>
        /// Identifies the language that this markup is represented in  
        /// </summary>
        [XmlAttribute("lang")]
        public string Language
        {
            get { return lang; }
            set { lang = value; }
        }
	

    }
}