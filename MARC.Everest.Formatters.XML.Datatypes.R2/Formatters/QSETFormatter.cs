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
 * Date: 11-12-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// A formatter that can represent an instance of one of the QSET classes on the wire in DT R2
    /// </summary>
    public class QSETFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // First off, QSET cannot be instantiated so the formatter should find out
            // which of the QSET classes are actually instantiated and output the appropriate
            // xsi type information before passing off to the appropriate formatter
            s.WriteAttributeString("xsi", "type", DatatypeR2Formatter.NS_XSI, DatatypeR2Formatter.CreateXSITypeName(o.GetType()));
            var hostResult = this.Host.Graph(s, o as IGraphable);
            result.Code = hostResult.Code;
            result.AddResultDetail(hostResult.Details);

        }

        /// <summary>
        /// Parse a QS* class from a QSET formatter
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // First off, there is no way to determine which of the classes it is..
           
            // Try using the xsi type
            var fmtr = new DatatypeR2Formatter().GetFormatter(s.GetAttribute("type", DatatypeR2Formatter.NS_XSI));
            if (fmtr == null) // No formatter available, add a result
            {
                result.AddResultDetail(new MandatoryElementMissingResultDetail(ResultDetailType.Error, "Cannot determine type of QSET to parse, use xsi:type to specify a type", s.ToString()));
                return null;
            }

            // Parse using the formatter
            // Fix: EV-876
            fmtr.GenericArguments = this.GenericArguments;
            fmtr.Host = this.Host; 
            return fmtr.Parse(s, result);

        }

        /// <summary>
        /// Get the type that this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "QSET"; }
        }

        /// <summary>
        /// Gets or sets the host of the formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets the generic arguments for this formatter
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get supported properties of QSET. Since QSET is abstract, none are supported
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            return null;
        }

        #endregion
    }
}
