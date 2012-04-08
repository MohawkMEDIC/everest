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
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using MARC.Everest.Interfaces;
using System.Reflection;
using System.Xml;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Quantity formatter
    /// </summary>
    public class QTYFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            
            // Quantity formatter
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, o, result);

            // Graph the others
            IQuantity qty = o as IQuantity;

            // Uncertainty type
            if (qty.NullFlavor == null && qty.UncertaintyType != null && qty.UncertaintyType.HasValue)
                s.WriteAttributeString("uncertaintyType", Util.ToWireFormat(qty.UncertaintyType.Value));
            
            // Elements
            // Expression
            if (qty.Expression != null)
            {
                s.WriteStartElement("expression", "urn:hl7-org:v3");
                this.Host.Graph(s, qty.Expression as IGraphable);
                s.WriteEndElement();
            }
            if (qty.OriginalText != null)
            {
                s.WriteStartElement("originalText", "urn:hl7-org:v3");
                this.Host.Graph(s, qty.OriginalText as IGraphable);
                s.WriteEndElement();
            }
            if (qty.NullFlavor == null && qty.Uncertainty != null)
            {
                s.WriteStartElement("uncertainty", "urn:hl7-org:v3");
                s.WriteAttributeString("xsi", "type", DatatypeR2Formatter.NS_XSI, DatatypeR2Formatter.CreateXSITypeName(qty.Uncertainty.DataType));
                this.Host.Graph(s, qty.Uncertainty as IGraphable);
                s.WriteEndElement();
            }
            if (qty.UncertainRange != null)
            {
                s.WriteStartElement("uncertainRange", "urn:hl7-org:v3");
                this.Host.Graph(s, qty.UncertainRange);
                s.WriteEndElement();
            }

        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public T ParseAttributes<T>(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result) where T : ANY, IQuantity, new()
        {

            ANYFormatter baseFormatter = new ANYFormatter();
            T retVal = baseFormatter.Parse<T>(s);

            // attributes
            if (s.GetAttribute("uncertaintyType") != null)
                retVal.UncertaintyType = Util.Convert<QuantityUncertaintyType>(s.GetAttribute("uncertaintyType"));

            return retVal;
        }

        /// <summary>
        /// Parse elements
        /// </summary>
        public void ParseElements<T>(XmlReader s, T retVal, DatatypeR2FormatterParseResult result) where T : ANY, IQuantity, new()
        {

            if (retVal == null)
                throw new ArgumentNullException("retVal");

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
                        ParseElementsInline(s, retVal, result);
                    }
                    catch (VocabularyException e)
                    {
                        result.AddResultDetail(new VocabularyIssueResultDetail(ResultDetailType.Error, e.Message, e));
                    }
                    catch (MessageValidationException e)
                    {
                        result.AddResultDetail(new MARC.Everest.Connectors.ResultDetail(MARC.Everest.Connectors.ResultDetailType.Error, e.Message, s.ToString(), e));
                    }
                    finally
                    {
                        if (s.Name == oldName) s.Read();
                    }
                }

            }
            #endregion

        }

        /// <summary>
        /// Parse elements that are inline in the read loop
        /// </summary>
        public void ParseElementsInline(XmlReader s, IQuantity retVal, DatatypeR2FormatterParseResult result)
        {
            if (s.LocalName == "expression") // Format using ED
            {
                var hostResult = this.Host.Parse(s, typeof(ED));
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                retVal.Expression = hostResult.Structure as ED;
            }
            else if (s.LocalName == "originalText") // display name
            {
                var hostResult = this.Host.Parse(s, typeof(ED));
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                retVal.OriginalText = hostResult.Structure as ED;
            }
            else if (s.LocalName == "uncertainty") // Translation
            {
                var hostResult = Host.Parse(s, retVal.GetType());
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                retVal.Uncertainty = hostResult.Structure as IQuantity;
            }
            else if (s.LocalName == "uncertainRange") // Uncertainty range
            {
                var hostResult = Host.Parse(s, typeof(IVL<IQuantity>));
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                retVal.UncertainRange = hostResult.Structure as IVL<IQuantity>;
            }
            else if (s.NodeType == System.Xml.XmlNodeType.Element)
                result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null));
        }
        
        /// <summary>
        /// Host of this formatter
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }
        #endregion

        /// <summary>
        /// Supported properties
        /// </summary>
        internal List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            return new ANYFormatter().GetSupportedProperties();
        }
    }


}
