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
 * Date: 11-16-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Xml;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// A formatter that can represent a CS in Data types R2
    /// </summary>
    public class CSFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph object <paramref name="o"/> to <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Get an instance ref 
            ICodedSimple instance_ics = (ICodedSimple)o;

            // Do a base format
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, o as ANY, result);

            // Format the coded simple
            if (instance_ics.CodeValue != null && ((ANY)o).NullFlavor == null)
                s.WriteAttributeString("code", Util.ToWireFormat(instance_ics.CodeValue));
        }

        /// <summary>
        /// Parse object from the wire
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Parse base (ANY) from the stream
            ANYFormatter baseFormatter = new ANYFormatter();

            // Parse CS
            CS<String> retVal = baseFormatter.Parse<CS<String>>(s);

            // Now parse our data out... Attributes
            if (retVal.NullFlavor == null && s.GetAttribute("code") != null)
                retVal.Code = s.GetAttribute("code");

            // Validate
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            baseFormatter.Validate(retVal, pathName, result);


            return retVal;
        }

        /// <summary>
        /// Gets the type that this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "CS"; }
        }

        /// <summary>
        /// Gets or sets the host of the formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets the generic arguments
        /// </summary>
        public Type[] GenericArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(
                new ANYFormatter().GetSupportedProperties()
            );
            retVal.Add(typeof(CS<>).GetProperty("Code"));
            return retVal;
        }

        #endregion
    }
}
