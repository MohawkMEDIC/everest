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
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;
using MARC.Everest.DataTypes.Interfaces;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// A formatter helper that formats GTS to data types r2
    /// </summary>
    public class GTSFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph the object <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Cast the object to GTS
            GTS instance = o as GTS;

            // Hull instance corrections
            if (instance.Hull != null)
            {
                if (instance.NullFlavor != null)
                {
                    instance.Hull.NullFlavor = instance.Hull.NullFlavor ?? instance.NullFlavor;
                    result.AddResultDetail(new PropertyValuePropagatedResultDetail(ResultDetailType.Warning, "NullFlavor", "Hull.NullFlavor", instance.Hull.NullFlavor, s.ToString()));
                }
                if (instance.Flavor != null)
                {
                    instance.Hull.Flavor = instance.Hull.Flavor ?? instance.Flavor;
                    result.AddResultDetail(new PropertyValuePropagatedResultDetail(ResultDetailType.Warning, "Flavor", "Hull.Flavor", instance.Hull.Flavor, s.ToString()));
                }
            }
            else
            {
                result.AddResultDetail(new ResultDetail(ResultDetailType.Error, "Cannot graph a GTS instance with a null Hull", s.ToString()));
                return;
            }

            // Map QSET/SXPR/SXCM
            object instanceHull = instance.Hull;
            if (instanceHull is SXPR<TS>)
                instanceHull = (instanceHull as SXPR<TS>).TranslateToQSET();

            // Emit the type name
            string xsiTypeName = Util.CreateXSITypeName(instanceHull.GetType());
            s.WriteAttributeString("xsi", "type", DatatypeR2Formatter.NS_XSI, xsiTypeName);

            // Host results
            var hostResult = this.Host.Graph(s, (IGraphable)instanceHull);
            result.Code = hostResult.Code;
            result.AddResultDetail(hostResult.Details);
        }

        /// <summary>
        /// Parse an instance of GTS from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Now determine the type of GTS
            string typeName = s.GetAttribute("type", DatatypeR2Formatter.NS_XSI);
            IDatatypeFormatter formatter;

            object value = null;

            // Parse the type
            switch (typeName)
            {
                case "IVL_TS":
                    formatter = new IVLFormatter();
                    break;
                case "PIVL_TS":
                    formatter = new PIVLFormatter();
                    break;
                case "EIVL_TS":
                    formatter = new EIVLFormatter();
                    break;
                case "QSP_TS":
                case "QSS_TS":
                case "QSD_TS":
                case "QSU_TS":
                case "QSI_TS":
                    formatter = new QSETFormatter();
                    break;
                default:
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Error, String.Format("Cannot parse a GTS Hull of type '{0}'", typeName), s.ToString()));
                    return null;
            }

            // Graph the Hull 
            // Fix: EV-876
            formatter.Host = this.Host;
            formatter.GenericArguments = new Type[] { typeof(TS) };
            return Util.Convert<GTS>(formatter.Parse(s, result));

        }

        /// <summary>
        /// Gets the type that this formatter "Handles"
        /// </summary>
        public string HandlesType
        {
            get { return "GTS"; }
        }

        /// <summary>
        /// Gets or sets the host of this formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets the generic arguments to the formatter
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Gets or sets the supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            retVal.Add(typeof(GTS).GetProperty("Hull"));
            return retVal;
        }

        #endregion
    }
}
