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
using System.IO;
using System.Xml;
using System.Collections;
using MohawkCollege.EHR.gpmr.Pipeline;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// The base type of all semantic elements in the MIF
    /// </summary>
    [XmlType(TypeName = "ModelElement", Namespace = "urn:hl7-org:v3/mif2")]
    public abstract class ModelElement
    {

        private string sortKey;

        /// <summary>
        /// A name used in determining the sort order of the model element within its siblings
        /// </summary>
        [XmlAttribute("sortKey")]
        public string SortKey
        {
            get { return sortKey; }
            set { sortKey = value; }
        }

        /// <summary>
        /// Allows us to easily find features that won't be loaded
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), XmlAnyElement]
        public XmlElement[] NotImplementedElements { 
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
                if (ur > 1)
                    throw new OperationCanceledException("Too many elements missing to reliably perform a transform");
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
                    if (!e.Name.Contains("graphic") && e.Prefix != "xsi") System.Diagnostics.Trace.WriteLine(String.Format("fixme: Attribute {0} not recognized in type {1}", e.Name, this.GetType().FullName), "debug");
                if (value.Length > 1)
                    throw new OperationCanceledException("Too many elements missing to reliably perform a transform");
            }
        }

        /// <summary>
        /// Convert the model element to a string
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor")]
        public override string ToString()
        {
            try
            {
                StringWriter sw = new StringWriter();
                XmlWriterSettings xws = new XmlWriterSettings();
                xws.Indent = true;
                XmlWriter xw = XmlWriter.Create(sw, xws);
                XmlSerializer xsz = new XmlSerializer(this.GetType());
                xsz.Serialize(xw, this);
                sw.Close();
                return sw.ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.ToString(), "error");
                return e.Message;
            }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class Comparator : IComparer
        {
            #region IComparer<ModelElement> Members
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.CompareTo(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
            public int Compare(Object x, Object y)
            {
                if ((x as ModelElement).sortKey != null && (y as ModelElement).sortKey != null)
                    return (x as ModelElement).sortKey.CompareTo((y as ModelElement).sortKey);
                else if ((x as ModelElement).sortKey != null) // X is not null so Y comes first
                    return -1;
                else if ((y as ModelElement).sortKey != null)
                    return 1;
                else
                    return 0;

            }

            #endregion
        }
    }
}