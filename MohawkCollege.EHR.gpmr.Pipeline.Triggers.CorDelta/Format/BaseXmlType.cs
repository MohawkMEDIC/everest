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
using System.Xml;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Format
{
    /// <summary>
    /// Base XML type for all classes in the DeltaSet structure,
    /// this is used to capture unsupported elements
    /// </summary>
    [XmlType("baseXmlType", Namespace = "urn:infoway.ca/deltaSet", IncludeInSchema = false)]
    public abstract class BaseXmlType
    {

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
                        System.Diagnostics.Trace.WriteLine(String.Format("fixme: Element {0} not recognized in type {1}", e.Name, this.GetType().Name), "debug");
                        ur++;
                    }
                }
                if (ur > 10)
                    throw new OperationCanceledException("Too many elements missing to reliably process the delta set");
            }
        }

        /// <summary>
        /// Allows us to easily find features that won't be loaded
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), XmlAnyAttribute]
        public XmlAttribute[] NotImplementedAttributes
        {
            get
            {
                return null;
            }
            set
            {
                if (value == null) return;
                foreach (XmlAttribute e in value)
                    if (!e.Name.Contains("graphic") && e.Prefix != "xsi") System.Diagnostics.Trace.WriteLine(String.Format("fixme: Attribute {0} not recognized in type {1}", e.Name, this.GetType().Name), "debug");
                if (value.Length > 10)
                    throw new OperationCanceledException("Too many elements missing to reliably process the delta set");
            }
        }


    }
}
