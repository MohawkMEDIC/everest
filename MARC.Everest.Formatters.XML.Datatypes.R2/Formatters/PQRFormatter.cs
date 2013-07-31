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
 * Date: 02-06-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// PQR formatter helper for the PQR data type
    /// </summary>
    internal class PQRFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/> saving
        /// results in <paramref name="result"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Base formatter is the CD type
            CDFormatter baseFormatter = new CDFormatter();
            baseFormatter.Host = this.Host;

            // Graph the value if the null flavor of the object is not null
            PQR opqr = o as PQR;
            if (opqr.NullFlavor == null && opqr.Value.HasValue)
            {
                // Precision
                if (opqr.Precision == null)
                    s.WriteAttributeString("value", Util.ToWireFormat(opqr.Value));
                else
                    s.WriteAttributeString("value", String.Format(String.Format("{{0:0.{0}}}", new String('0', opqr.Precision)), opqr.Value));
            }

            // Graph the base stuff
            baseFormatter.Graph(s, o, result);
        }

        /// <summary>
        /// Parse the string
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Temporary holding value for value
            string tValue = null;
            if (s.GetAttribute("value") != null)
                tValue = s.GetAttribute("value");

            // Parse the CD stuff
            CDFormatter baseFormatter = new CDFormatter();
            baseFormatter.Host = this.Host;
            var retVal = baseFormatter.Parse<PQR>(s, result);

            // Interpret the value
            // If the value was interpreted
            if (!String.IsNullOrEmpty(tValue))
            {
                try
                {
                    retVal.Value = Util.Convert<decimal>(tValue);
                    if (tValue.Contains("."))
                        retVal.Precision = tValue.Length - tValue.IndexOf(".") - 1;
                }
                catch (Exception e)
                {
                    result.Code = ResultCode.Error;
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Error, e.Message, s.ToString(), e));
                }
            }

            // Validate
            new ANYFormatter().Validate(retVal, s.ToString(), result);

            return retVal;
        }

        /// <summary>
        /// Get the type this handles
        /// </summary>
        public string HandlesType
        {
            get { return "PQR"; }
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
        /// Gets or sets the generic arguments
        /// </summary>
        public Type[] GenericArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the supported properties
        /// </summary>
        /// <returns></returns>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(new CDFormatter().GetSupportedProperties());
            retVal.Add(typeof(PQR).GetProperty("Value"));
            return retVal;
        }

        #endregion
    }
}
