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
 * Date: 02-06-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using MARC.Everest.Xml;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Formatter helper for the MO data type
    /// </summary>
    internal class MOFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graphs <paramref name="o"/> onto <paramref name="s"/> storing result
        /// in <paramref name="result"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Get a strongly typed instance of o
            MO omo = o as MO;

            // Output additional attributes
            // Since a MO with null flavor derived can still have a currency
            // we only want to check for null flavors not derived
            if (omo.NullFlavor == null || omo.NullFlavor.Equals(NullFlavor.Derived))
            {
                if (omo.Currency != null)
                    s.WriteAttributeString("currency", Util.ToWireFormat(omo.Currency));
            }

            // Output the PDV and QTY formatter helper data
            PDVFormatter pdvFormatter = new PDVFormatter();
            pdvFormatter.Host = this.Host;
            pdvFormatter.Graph(s, o, result);

        }

        /// <summary>
        /// Parse the PQ back into a structure
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Create the base formatter
            PDVFormatter baseFormatter = new PDVFormatter();
            baseFormatter.Host = this.Host;

            // Read temporary values
            string tCurrency = String.Empty;
            if(s.GetAttribute("currency") != null)
                tCurrency = s.GetAttribute("currency");

            // Parse PDV content 
            var retVal = baseFormatter.Parse<MO>(s, result);

            // Set PDV content
            retVal.Currency = tCurrency;

            // Validate
            ANYFormatter anyFormatter = new ANYFormatter();
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            anyFormatter.Validate(retVal as ANY, pathName, result);
            
            // Return instance
            return retVal;
        }

        /// <summary>
        /// Gets the type that this handler accepts
        /// </summary>
        public string HandlesType
        {
            get { return "MO"; }
        }

        /// <summary>
        /// Gets or sets the host of this formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the generic arguments for the type
        /// </summary>
        public Type[] GenericArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the list of supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(new QTYFormatter().GetSupportedProperties());
            retVal.Add(typeof(PQ).GetProperty("Value"));
            retVal.Add(typeof(MO).GetProperty("Currency"));
            return retVal;
        }

        #endregion
    }
}
