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
using MARC.Everest.DataTypes;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Formatter helper for the REAL data type
    /// </summary>
    internal class REALFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {

            PDVFormatter qtyf = new PDVFormatter();
            qtyf.Host = this.Host;
            qtyf.Graph(s, o, result);

        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            PDVFormatter qtyf = new PDVFormatter();
            qtyf.Host = this.Host;
            return qtyf.Parse<REAL>(s, result);

        }

        /// <summary>
        /// Gets the type that this formatter helper graphs
        /// </summary>
        public string HandlesType
        {
            get { return "REAL"; }
        }

        /// <summary>
        /// Gets or sets the host formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the generic arguments to this formatter
        /// </summary>
        public Type[] GenericArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the list of supported properties
        /// </summary>
        /// <returns></returns>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            var retVal = new QTYFormatter().GetSupportedProperties();
            retVal.AddRange(new PropertyInfo[] {
                typeof(REAL).GetProperty("Value"),
                typeof(REAL).GetProperty("Expression"),
                typeof(REAL).GetProperty("OriginalText"),
                typeof(REAL).GetProperty("Uncertainty"),
                typeof(REAL).GetProperty("UncertainRange")
            });
            return retVal;
        }


        #endregion
    }
}
