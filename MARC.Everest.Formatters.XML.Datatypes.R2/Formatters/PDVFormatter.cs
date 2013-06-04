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
 * Date: 16-12-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using System.Globalization;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Formatter for the PDV types
    /// </summary>
    public class PDVFormatter 
    {

        /// <summary>
        /// Graph object <paramref name="o"/> onto the stream
        /// </summary>
        public void Graph(XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Graph ANY
            QTYFormatter baseFormatter = new QTYFormatter();
            baseFormatter.Host = this.Host;

            // Null ?
            IAny anyInstance = o as IAny;
            IPrimitiveDataValue pdv = o as IPrimitiveDataValue;
            IRealValue rv = o as IRealValue;

            // Format
            if (pdv.Value != null && anyInstance.NullFlavor == null)
            {
                if(rv == null || rv.Precision == 0)
                    s.WriteAttributeString("value", Util.ToWireFormat(pdv.Value));
                else if (rv != null)
                    s.WriteAttributeString("value", String.Format(DatatypeR2Formatter.FormatterCulture, String.Format("{{0:0.{0}}}", new String('0', rv.Precision), DatatypeR2Formatter.FormatterCulture.NumberFormat.NumberDecimalSeparator), rv.Value));
            }

            // Format the base
            baseFormatter.Graph(s, o, result);

        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public T Parse<T>(XmlReader r, DatatypeR2FormatterParseResult result) where T : ANY, IPrimitiveDataValue, IQuantity, new()
        {

            // Prepare QTY formatting
            QTYFormatter baseFormatter = new QTYFormatter();
            baseFormatter.Host = this.Host;

            // Parse attributes part of QTY
            T retVal = this.ParseAttributes<T>(r, result);

            // Parse the elements 
            baseFormatter.ParseElements<T>(r, retVal, result);

            // Validate
            new ANYFormatter().Validate(retVal, r.ToString(), result);

            return retVal;
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public T ParseAttributes<T>(XmlReader r, DatatypeR2FormatterParseResult result) where T : ANY, IPrimitiveDataValue, IQuantity, new()
        {

            string val = null;
            // Get the value
            if (r.GetAttribute("value") != null)
                val = r.GetAttribute("value");

            // Parse the QTY part
            QTYFormatter baseFormatter = new QTYFormatter();
            baseFormatter.Host = this.Host;

            // Parse attributes part of QTY
            T retVal = baseFormatter.ParseAttributes<T>(r, result);
            
            IRealValue rvRetVal = retVal as IRealValue;

            // If the value was interpreted
            if (!String.IsNullOrEmpty(val))
            {
                try
                {
                    retVal.Value = Util.FromWireFormat(val, retVal.GetType().GetProperty("Value").PropertyType);
                    if (rvRetVal != null && val.Contains(DatatypeR2Formatter.FormatterCulture.NumberFormat.NumberDecimalSeparator))
                        rvRetVal.Precision = val.Length - val.IndexOf(DatatypeR2Formatter.FormatterCulture.NumberFormat.NumberDecimalSeparator) - 1;
                }
                catch (Exception e)
                {
                    result.Code = ResultCode.Error;
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Error, e.Message, r.ToString(), e));
                }
            }

            // REturn the retVal
            return retVal;
        }
        /// <summary>
        /// Gets or sets the host
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }
    }
}
