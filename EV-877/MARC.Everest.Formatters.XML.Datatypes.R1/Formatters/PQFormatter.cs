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
 * Date: 9/22/2009 9:17:04 AM
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using MARC.Everest.Xml;
using MARC.Everest.Interfaces;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Formatter for PQ data type
    /// </summary>
    public class PQFormatter : IDatatypeFormatter
    {

        #region IDatatypeFormatter Members

       

        /// <summary>
        /// Graph object <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            ANYFormatter baseFormatter = new ANYFormatter();

            baseFormatter.Graph(s, o, result);
            PQ instance = o as PQ;

            if (instance.NullFlavor != null) return; // Don't graph anymore

            if (instance.CodingRationale != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "CodingRationale", "PQ", s.ToString()));
            if (instance.Expression != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Expression", "PQ", s.ToString()));
            if (instance.OriginalText != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "OriginalText", "PQ", s.ToString()));
            if (instance.Uncertainty != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Uncertainty", "PQ", s.ToString()));
            if (instance.UncertaintyType != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "UncertaintyType", "PQ", s.ToString()));
            if (instance.UncertainRange != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "UncertainRange", "PQ", s.ToString()));
            if (instance.Unit != null)
                s.WriteAttributeString("unit", instance.Unit);
            
            // Precision
            if (instance.Precision != null && instance.Precision != 0 && instance.Value.HasValue)
                s.WriteAttributeString("value", instance.Value.Value.ToString(String.Format("0.{0}", new String('0', instance.Precision))));
            else if(instance.Value.HasValue)
                s.WriteAttributeString("value", instance.Value.Value.ToString());

            
            if (instance.Translation != null)
                foreach (var trans in instance.Translation)
                {
                    s.WriteStartElement("translation", "urn:hl7-org:v3");
                    PQRFormatter pqrFormatter = new PQRFormatter();
                    pqrFormatter.Graph(s, trans, result);
                    s.WriteEndElement();
                }

            
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse</param>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            PDVFormatter pdvFormatter = new PDVFormatter();
            PQ retVal = pdvFormatter.Parse<PQ>(s, result);

            if (s.GetAttribute("unit") != null)
                retVal.Unit = s.GetAttribute("unit");

            // Precision is not supported in R1, but is still useful to have so 
            // we will report the precision of the data that was on the wire
            string valStr = s.GetAttribute("value");
            if (valStr != null && valStr.Contains("."))
                retVal.Precision = valStr.Length - valStr.IndexOf(".") - 1;
            else
                retVal.Precision = 0;

            #region Elements
            if (!s.IsEmptyElement)
            {

                int sDepth = s.Depth;
                string sName = s.Name;

                s.Read();
                // string Name
                while (!(s.NodeType == System.Xml.XmlNodeType.EndElement && s.Depth == sDepth && s.Name == sName))
                {
                    string oldName = s.Name; // Name
                    try
                    {
                        if (s.LocalName == "translation") // Format using ED
                        {
                            SETFormatter pqrFormatter = new SETFormatter();
                            pqrFormatter.GenericArguments = new Type[] { typeof(PQR) };
                            pqrFormatter.Host = Host.Clone() as IXmlStructureFormatter;
                            retVal.Translation = new SET<PQR>((LIST<IGraphable>)pqrFormatter.Parse(s, result)); // Parse ED
                            //details.AddRange(pqrFormatter.Details);
                        }
                    }
                    catch (MessageValidationException e)
                    {
                        result.AddResultDetail(new MARC.Everest.Connectors.ResultDetail(MARC.Everest.Connectors.ResultDetailType.Error, e.Message, e));
                    }
                    finally
                    {
                        if (s.Name == oldName) s.Read();
                    }
                }
            }
            #endregion

            // Validate
            ANYFormatter fmtr = new ANYFormatter();
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            fmtr.Validate(retVal, pathName, result);

            return retVal;
        }

        /// <summary>
        /// Handles type
        /// </summary>
        public string HandlesType
        {
            get { return "PQ"; }
        }

        /// <summary>
        /// Get or set the host
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Get or set generic arguments
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(PQ).GetProperty("Unit"));
            retVal.Add(typeof(PQ).GetProperty("Precision"));
            retVal.Add(typeof(PQ).GetProperty("Value"));
            retVal.Add(typeof(PQ).GetProperty("Translation"));
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}