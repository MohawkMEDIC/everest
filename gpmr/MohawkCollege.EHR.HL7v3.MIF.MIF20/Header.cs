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
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Defines common content for all major artifact types. It should always be present in the root element of a document 
    /// </summary>
    /// <remarks>
    /// This class is only partially implemented to save time. Most of the elements are placed into the XmlAny
    /// elements if they are required later they can be developed at that time.
    /// <para>Class is incomplete</para>
    /// </remarks>
    [XmlType(TypeName = "Header", Namespace = "urn:hl7-org:v3/mif2")]
    public class Header
    {

        private XmlElement[] nonImplemented;
        private Lagalese copyright;

        /// <summary>
        /// Legal information of the mif
        /// </summary>
        [XmlElement("legalese")]
        public Lagalese Copyright
        {
            get { return copyright; }
            set { copyright = value; }
        }

        /// <summary>
        /// Primary repository
        /// </summary>
        [XmlAttribute("primaryRepository")]
        public string PrimaryRepository { get; set; }

        /// <summary>
        /// Responsible group
        /// </summary>
        [XmlElement("responsibleGroup")]
        public List<ResponsibleGroup> ResponsibleGroup { get; set; }


        /// <summary>
        /// Identifies contributors to the current document
        /// </summary>
        [XmlElement("contributor")]
        public List<Contributor> Contributor { get; set; }

        
        /// <summary>
        /// Non implemented elements in the header class
        /// </summary>
        /// HACK: Just to get header information which most renderers will ignore anyways
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays"), XmlAnyElement()]
        public XmlElement[] NonImplemented
        {
            get { return nonImplemented; }
            set 
            {
                //System.Diagnostics.Trace.WriteLine("Functionality for these elements is not implemented");
                nonImplemented = value; 
            }
        }
	
	
    }
}