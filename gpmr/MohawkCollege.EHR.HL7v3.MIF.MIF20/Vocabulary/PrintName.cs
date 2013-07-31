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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// A preferred or alternative designation for a concept in a specified language
    /// </summary>
    [XmlType(TypeName = "PrintName", Namespace = "urn:hl7-org:v3/mif2")]
    public class PrintName
    {
        /// <summary>
        /// Identifies the language in which the print name is expressed
        /// </summary>
        [XmlAttribute("language")]
        public string Language { get; set; }
        /// <summary>
        /// Identifies whether this is the preferred print name for the specified language
        /// </summary>
        [XmlAttribute("preferredForLanguage")]
        public bool PreferredForLanguage { get; set; }
        /// <summary>
        /// The text representation of the print name
        /// </summary>
        [XmlAttribute("text")]
        public string Text { get; set; }

        /// <summary>
        /// Communicates an icon representation of the code
        /// </summary>
        /// <remarks>
        /// This is not implemented
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays"), XmlAnyElement()]
        public XmlElement[] Icon
        {
            get { return null;  }
            set
            {
                //System.Diagnostics.Trace.Write("ICON Element of PrintName is not supported!", "error");
            }
        }
    }
}