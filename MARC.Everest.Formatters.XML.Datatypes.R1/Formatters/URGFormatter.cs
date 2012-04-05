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
 * Date: 9/17/2009 9:51:29 AM
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using MARC.Everest.Interfaces;
using MARC.Everest.Xml;
using MARC.Everest.DataTypes.Surrogates;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Formatter for the DT R1 URG data type
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG")]
    public class URGFormatter : IDatatypeFormatter
    {


        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            // Format the UVP to the wire
            UVPFormatter uvpFormatter = new UVPFormatter();
            uvpFormatter.Host = this.Host;
            uvpFormatter.Graph(s, o, result);

            if ((o as ANY).NullFlavor != null)
                return;

            // Get the value of the elements, since it is a generic type 
            Type ivlType = o.GetType();
            object lowValue = ivlType.GetProperty("Low").GetValue(o, null),
                highValue = ivlType.GetProperty("High").GetValue(o, null),
                widthValue = ivlType.GetProperty("Width").GetValue(o, null),
                originalTextValue = ivlType.GetProperty("OriginalText").GetValue(o, null),
                lowClosedValue = ivlType.GetProperty("LowClosed").GetValue(o, null),
                highClosedValue = ivlType.GetProperty("HighClosed").GetValue(o, null),
                valueValue = ivlType.GetProperty("Value").GetValue(o, null);

            // Warn the developer if there are any properties that can't be represented in R1
            if (originalTextValue != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "OriginalText", "URG", s.ToString()));
            if (lowClosedValue != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "LowClosed", "URG", s.ToString()));
            if (highClosedValue != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "HighClosed", "URG", s.ToString()));
            
            if (lowValue != null && highValue != null) // low & high
            {
                s.WriteStartElement("low", "urn:hl7-org:v3");
                var hostResult = Host.Graph(s, (IGraphable)lowValue);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
                s.WriteStartElement("high", "urn:hl7-org:v3");
                hostResult = Host.Graph(s, (IGraphable)highValue);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();

                #region Warnings
                if (valueValue != null)
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Warning,
                        "low, high, value can't be represented together in an URG data type in R1. Only formatting low and high", s.ToString(), null));
                if (widthValue != null)
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Warning,
                        "low, high, width can't be represented together in an URG data type in R1. Only formatting low and high", s.ToString(), null));
                #endregion
            }
            else if (lowValue != null && widthValue != null) // Low & width
            {
                s.WriteStartElement("low", "urn:hl7-org:v3");
                var hostResult = Host.Graph(s, (IGraphable)lowValue);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);

                s.WriteEndElement();
                s.WriteStartElement("width", "urn:hl7-org:v3");
                hostResult = Host.Graph(s, (IGraphable)widthValue);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);

                s.WriteEndElement();

                #region Warnings
                if (valueValue != null)
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Warning,
                        "low, width, value can't be represented together in an URG data type in R1. Only formatting low and width", s.ToString(), null));
                if (highValue != null)
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Warning,
                        "low, width, high can't be represented together in an URG data type in R1. Only formatting low and width", s.ToString(), null));
                #endregion
            }
            else if (highValue != null && widthValue != null) // high & width
            {
                s.WriteStartElement("width", "urn:hl7-org:v3");
                var hostResult = Host.Graph(s, (IGraphable)widthValue);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);

                s.WriteEndElement();
                s.WriteStartElement("high", "urn:hl7-org:v3");
                hostResult = Host.Graph(s, (IGraphable)highValue);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);

                s.WriteEndElement();

                #region Warnings
                if (valueValue != null)
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Warning,
                        "high, width, value can't be represented together in an URG data type in R1. Only formatting width and high", s.ToString(), null));
                if (lowValue != null)
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Warning,
                        "high, width, low can't be represented together in an URG data type in R1. Only formatting width and high", s.ToString(), null));
                #endregion
            }
            else if (lowValue != null) // low only
            {
                s.WriteStartElement("low", "urn:hl7-org:v3");
                var hostResult = Host.Graph(s, (IGraphable)lowValue);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }
            else if (highValue != null) // High only
            {
                s.WriteStartElement("high", "urn:hl7-org:v3");
                var hostResult = Host.Graph(s, (IGraphable)highValue);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);

                s.WriteEndElement();
            }
            else if (widthValue != null) // width only
            {
                s.WriteStartElement("width", "urn:hl7-org:v3");
                var hostResult = Host.Graph(s, (IGraphable)widthValue);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }
            else if (valueValue != null) ;
                //DOC: The next comment makes no sence, further documentation required.
            // No need for this ;
            else
                result.AddResultDetail(new ResultDetail(ResultDetailType.Error,
                    "Can't create a valid representation of URG using the supplied data for data types R1. Valid IVL must have [low & [high | width]] | [high & width] | high | value to satisfy data type R1 constraints",
                    s.ToString(), null));

        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse from</param>
        /// <returns>The parsed object</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Parse the base PDV first
            PDVFormatter pdvFormatter = new PDVFormatter();
            URG<Object> retVal = pdvFormatter.Parse<URG<Object>>(s, result);

            if (retVal.NullFlavor != null) // Null, no longer process
                return retVal;

            if (s.GetAttribute("probability") != null) // Probability
            {
                decimal prob = (decimal)0.0f;
                if (!Decimal.TryParse(s.GetAttribute("probability"), out prob)) // Try to parse
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Warning, string.Format("Value '{0}' can't be processed into 'Probability' on data type UVP", s.GetAttribute("probability")), s.ToString(), null));
                else // Success, so assign
                    retVal.Probability = prob;
            }

            // Serialization
            IXmlStructureFormatter serHost = this.Host;
            
            // Elements
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
                        if (s.LocalName == "low") // Low , parse using the proper type
                        {
                            var hostResult = serHost.Parse(s, GenericArguments[0]);
                            result.Code = hostResult.Code;
                            result.AddResultDetail(hostResult.Details);
                            retVal.Low = hostResult.Structure;
                        }
                        else if (s.LocalName == "width") // Width
                        {
                            var hostResult = serHost.Parse(s, typeof(PQ));
                            result.Code = hostResult.Code;
                            result.AddResultDetail(hostResult.Details);
                            retVal.Width = hostResult.Structure as PQ;
                        }
                        else if (s.LocalName == "high") // High
                        {
                            var hostResult = serHost.Parse(s, GenericArguments[0]);
                            result.Code = hostResult.Code;
                            result.AddResultDetail(hostResult.Details);
                            retVal.High = hostResult.Structure;
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

            // Validate the data type
            ANYFormatter validator = new ANYFormatter();
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            validator.Validate(retVal, pathName, result);

            return retVal;
        }

        /// <summary>
        /// Returns the type that this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "URG"; }
        }

        /// <summary>
        /// Get or set the host of this formatter
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
            retVal.AddRange(new PropertyInfo[] {
                typeof(URG<>).GetProperty("Low"),
                typeof(URG<>).GetProperty("High"),
                typeof(URG<>).GetProperty("Width"),
                typeof(URG<>).GetProperty("Value")
            });
            retVal.AddRange(new UVPFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}