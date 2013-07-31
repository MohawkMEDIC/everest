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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Descriptive information about the containing model element
    /// </summary>
    [XmlType(TypeName = "Annotation", Namespace = "urn:hl7-org:v3/mif2")]
    public class Annotation
    {
        /// <summary>
        /// Documentation related to the model element
        /// </summary>
        [XmlElement("documentation")]
        public Documentation Documentation { get; set; }
        /// <summary>
        /// Supporting programmatic information related to the model element
        /// </summary>
        [XmlElement("appInfo")]
        public AppInfo AppInfo { get; set; }

        /// <summary>
        /// Allows us to easily find features that won't be loaded
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), XmlAnyElement]
        public XmlElement[] NotImplementedElements
        {
            get
            {
                return null;
            }
            set
            {
                if (value == null) return;
                int ur = 0;
                foreach (XmlElement e in value)
                {
                    if (!e.Name.Contains("graphic") && e.LocalName != "figure" && e.LocalName != "historyItem")
                    {
                        System.Diagnostics.Trace.WriteLine(String.Format("fixme: Element {0} not recognized in type {1}", e.Name, this.GetType().FullName), "debug");
                        ur++;
                    }
                }
                if (ur > 10)
                    throw new OperationCanceledException("Too many elements missing to reliably perform a transform");
            }
        }
    }
}