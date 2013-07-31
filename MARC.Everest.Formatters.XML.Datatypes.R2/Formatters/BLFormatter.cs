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
 * Date: 11-12-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// A formatter that graphs and interprets the <see cref="T:MARC.Everest.DataTypes.BL"/> class
    /// </summary>
    internal class BLFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members
        /// <summary>
        /// Graphs object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            ANYFormatter baseFormatter = new ANYFormatter();

            // Graph 
            baseFormatter.Graph(s, o, result);

            // Null ?
            IAny anyInstance = o as IAny;
            IPrimitiveDataValue pdv = o as IPrimitiveDataValue;

            // Null flavor
            if (anyInstance.NullFlavor != null)
                return;

            // Format
            if (pdv.Value != null)
            {
                s.WriteAttributeString("value", Util.ToWireFormat(pdv.Value));
            }

        }

        /// <summary>
        /// Parses an object from stream <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Parse base (ANY) from the stream
            ANYFormatter baseFormatter = new ANYFormatter();

            // Parse BL
            BL retVal = baseFormatter.Parse<BL>(s);

            // Get the value
            if (s.GetAttribute("value") != null)
                try
                {
                    retVal.Value = Util.Convert<Boolean>(s.GetAttribute("value"));
                }
                catch (Exception e)
                {
                    result.Code = ResultCode.Error;
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Error, e.Message, s.ToString(), e));
                }

            // Base formatter
            baseFormatter.Validate(retVal, s.ToString(), result);

            return retVal;
        }

        /// <summary>
        /// Gets the type that this datatype formatter can graph
        /// </summary>
        public string HandlesType
        {
            get { return "BL"; }
        }

        /// <summary>
        /// Gets or sets the formatting host that this formatter operates within
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets the generic arguments to the formatter
        /// </summary>
        public Type[] GenericArguments { get; set; }

        #endregion

        #region IDatatypeFormatter Members

        /// <summary>
        /// Get supported properties 
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            var retVal = new List<System.Reflection.PropertyInfo>();
            retVal.Add(typeof(BL).GetProperty("Value"));
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            return retVal;
        }

        #endregion

    }
}
