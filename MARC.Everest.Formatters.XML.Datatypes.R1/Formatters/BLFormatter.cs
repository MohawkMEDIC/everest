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
 * Date: 06-24-2011
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Datatype R1 formatter for the BL datatype
    /// </summary>
    public class BLFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

       
        /// <summary>
        /// Graph the object
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, o, result);

            // Null ?
            BL instance = o as BL;
            if (instance.NullFlavor != null)
                return;

            // Format
            if (instance.Value != null)
                s.WriteAttributeString("value", string.Format("{0}", instance.Value).ToLower());
            
        }

        /// <summary>
        /// Parse the object
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToBoolean(System.String)")]
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Parse base (ANY) from the stream
            ANYFormatter baseFormatter = new ANYFormatter();

            // Parse TS
            BL retVal = baseFormatter.Parse<BL>(s, result);

            // Now parse our data out... Attributes
            if (s.GetAttribute("value") != null)
                retVal.Value = Convert.ToBoolean(s.GetAttribute("value"));

            return retVal;
        }

        /// <summary>
        /// Get the type that this handles
        /// </summary>
        public string HandlesType
        {
            get { return "BL"; }
        }

        /// <summary>
        /// Host 
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Get generic arguments
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(BL).GetProperty("Value"));
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}