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
 * Date: 02-14-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// NPPD formatter helper
    /// </summary>
    internal class NPPDFormatter : IDatatypeFormatter
    {

        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            SETFormatter formatter = new SETFormatter();
            formatter.Host = this.Host;

            // Create new generic arguments
            var uvpType = typeof(UVP<>);
            var genType = uvpType.MakeGenericType(GenericArguments);

            formatter.GenericArguments = new Type[] { genType };
            formatter.Graph(s, o, result);
        }

        /// <summary>
        /// Parse <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            SETFormatter formatter = new SETFormatter();
            formatter.Host = this.Host;

            // Create new generic arguments
            var uvpType = typeof(UVP<>);
            var genType = uvpType.MakeGenericType(GenericArguments);

            formatter.GenericArguments = new Type[] { genType };
            object retval = formatter.Parse(s, result);

            return retval;
        }

        /// <summary>
        /// Gets the type that this formatter handles
        /// </summary>
        public string HandlesType
        {
            get
            {
                return "NPPD";
            }
        }

        /// <summary>
        /// Gets or sets the host
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the generic parameters
        /// </summary>
        public Type[] GenericArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a list of supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            return new SETFormatter().GetSupportedProperties();
        }

        #endregion
    }
}
