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
 * User: computc
 * Date: 9/14/2009 11:19:24 AM
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// XML ITS 1.0 PN Formatter
    /// </summary>
    public class PNFormatter : IDatatypeFormatter
    {

        #region IDatatypeFormatter Members

        
        /// <summary>
        /// Graph object <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse from</param>
        /// <param name="o">The object to parse</param>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {

            ENFormatter baseFormatter = new ENFormatter();
            baseFormatter.Host = this.Host;
            baseFormatter.GenericArguments = this.GenericArguments;
            
            // Format
            baseFormatter.Graph(s, o, result);

        }

        /// <summary>
        /// Parse an instance from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse from</param>
        /// <returns>The parsed object</returns>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Parse the object
            ENFormatter baseFormatter = new ENFormatter();
            baseFormatter.Host = this.Host;
            baseFormatter.GenericArguments = this.GenericArguments;

            // Parse
            PN retVal = baseFormatter.Parse<PN>(s, result);
            return retVal;
        }

        /// <summary>
        /// Get the type that this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "PN"; }
        }

        /// <summary>
        /// Get or set the host value
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Get or set the generic arguments
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.AddRange(new ENFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}