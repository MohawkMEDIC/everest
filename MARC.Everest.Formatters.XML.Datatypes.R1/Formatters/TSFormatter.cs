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
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using MARC.Everest.Xml;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// TimeStamp formatter
    /// </summary>
    public class TSFormatter : IDatatypeFormatter
    {
        /// <summary>
        /// Host context
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Get or set the generic arguments to this type (if applicable)
        /// </summary>
        public Type[] GenericArguments { get; set; }

        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph the object <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        /// <param name="s">The xmlwriter to write to</param>
        /// <param name="o">The object to graph</param>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            // Get an instance ref
            TS instance_ts = (TS)o;

            // Do a base format
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, o, result);

            // Null flavor
            if (((ANY)o).NullFlavor != null)
                return;
            
            // Timestamp
            if (instance_ts.Value != null)
                s.WriteAttributeString("value", o.ToString());

        }
         
        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The XmlReader to parse from</param>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Parse base (ANY) from the stream
            ANYFormatter baseFormatter = new ANYFormatter();

            // Parse TS
            TS retVal = baseFormatter.Parse<TS>(s, result);

            // Now parse our data out... Attributes
            if (s.GetAttribute("value") != null)
                retVal.Value = s.GetAttribute("value");

            // Validate
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            baseFormatter.Validate(retVal, pathName, result);


            return retVal;
        }

        /// <summary>
        /// Gets the type that this datatype formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "TS"; }
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(TS).GetProperty("DateValue"));
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}