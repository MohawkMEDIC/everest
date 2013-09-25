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
    public class TSFormatter : ANYFormatter
    {

        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph the object <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        /// <param name="s">The xmlwriter to write to</param>
        /// <param name="o">The object to graph</param>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            // Get an instance ref
            TS instance_ts = (TS)o;

            // Do a base format
            base.Graph(s, o, result);

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
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Parse TS
            TS retVal = base.Parse<TS>(s, result);

            // Now parse our data out... Attributes
            if (s.GetAttribute("value") != null)
                retVal.Value = s.GetAttribute("value");

            // Validate
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            base.Validate(retVal, pathName, result);


            return retVal;
        }

        /// <summary>
        /// Gets the type that this datatype formatter handles
        /// </summary>
        public override string HandlesType
        {
            get { return "TS"; }
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public override List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = base.GetSupportedProperties();
            retVal.Add(typeof(TS).GetProperty("DateValue"));
            return retVal;
        }
        #endregion
    }
}