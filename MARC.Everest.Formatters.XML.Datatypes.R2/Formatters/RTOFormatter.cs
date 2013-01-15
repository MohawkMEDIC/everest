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
 * Date: 01-30-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;
using MARC.Everest.Exceptions;
using MARC.Everest.Xml;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Formatter helper for the RTO type
    /// </summary>
    internal class RTOFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph object <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream being graphed to</param>
        /// <param name="o">The object being graphed</param>
        /// <param name="result">The result of the graph operation</param>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Graph the QTY portions
            QTYFormatter qty = new QTYFormatter();
            qty.Host = this.Host;
            qty.Graph(s, o, result);

            // Any null flavors
            IAny anyO = o as IAny;
            IPrimitiveDataValue ipdv = o as IPrimitiveDataValue;
            if (anyO.NullFlavor != null)
                return;

            // PDV Value does not support value
            if (ipdv.Value != null)
                result.AddResultDetail(new NotPermittedDatatypeR2EntityResultDetail(MARC.Everest.Connectors.ResultDetailType.Warning, "Value", "RTO", s.ToString()));

            // Get the numerator / denominator
            PropertyInfo numeratorProperty = o.GetType().GetProperty("Numerator"),
                denominatorProperty = o.GetType().GetProperty("Denominator");
            object numerator = numeratorProperty.GetValue(o, null),
                denominator = denominatorProperty.GetValue(o, null);

            // Serialize the numerator
            if (numerator != null)
            {
                s.WriteStartElement("numerator", "urn:hl7-org:v3");

                // Write the XSI type
                string xsiTypeName = DatatypeR2Formatter.CreateXSITypeName(numerator.GetType());

                // Write the type
                if (this.Host.Host == null)
                    s.WriteAttributeString("type", DatatypeR2Formatter.NS_XSI, xsiTypeName.ToString());
                else
                    s.WriteAttributeString("xsi", "type", DatatypeR2Formatter.NS_XSI, xsiTypeName.ToString());

                var hostResult = this.Host.Graph(s, (IGraphable)numerator);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }
            // Serialize Denominator
            if (denominator != null)
            {
                s.WriteStartElement("denominator", "urn:hl7-org:v3");

                // Write the XSI type
                string xsiTypeName = DatatypeR2Formatter.CreateXSITypeName(denominator.GetType());

                // Write the type
                if (this.Host.Host == null)
                    s.WriteAttributeString("type", DatatypeR2Formatter.NS_XSI, xsiTypeName.ToString());
                else
                    s.WriteAttributeString("xsi", "type", DatatypeR2Formatter.NS_XSI, xsiTypeName.ToString());

                var hostResult = this.Host.Graph(s, (IGraphable)denominator);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            } 
        }

        /// <summary>
        /// Parse the RTO
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Create the types
            Type rtoType = typeof(RTO<,>);

            QTYFormatter baseFormatter = (new QTYFormatter() { Host = this.Host});
            // Quantity value, just read into a REAL
            var qty = baseFormatter.ParseAttributes<REAL>(s, result);

            // Temporary values
            IGraphable denominator = null,
                numerator = null;
          
            // Elements
            // This type is a little different in R2, basically we can't determine the generic
            // parameters by the XSI Type so we have to parse the num/denom manually, and 
            // ensure that we have both, then we construct the object and set the properties
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
                        // Numerator
                        if (s.LocalName == "numerator")
                        {
                            var parseResult = Host.Parse(s, GenericArguments[0]);
                            result.Code = parseResult.Code;
                            result.AddResultDetail(parseResult.Details);
                            numerator = parseResult.Structure;
                        }
                        // Denominator
                        else if (s.LocalName == "denominator")
                        {
                            var parseResult = Host.Parse(s, GenericArguments[1]);
                            result.Code = parseResult.Code;
                            result.AddResultDetail(parseResult.Details);
                            denominator = parseResult.Structure;
                        }
                        else
                            baseFormatter.ParseElementsInline(s, qty, result);

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

            try
            {
                // Construct the generic type (if possible)
                Type rtoGenericType = rtoType.MakeGenericType(new Type[]
                {
                    numerator == null ? typeof(IQuantity) : numerator.GetType(),
                    denominator == null ? typeof(IQuantity) : denominator.GetType()
                });

                // Create an instance of rto from the rtoType
                object instance = rtoGenericType.GetConstructor(Type.EmptyTypes).Invoke(null);

                // Get the values from the QTY and copy
                IQuantity rtoQty = instance as IQuantity;
                DatatypeR2Formatter.CopyBaseAttributes(qty, rtoQty as ANY);
                rtoQty.Expression = qty.Expression;
                rtoQty.OriginalText = qty.OriginalText;
                rtoQty.UncertainRange = qty.UncertainRange;
                rtoQty.Uncertainty = qty.Uncertainty;
                rtoQty.UncertaintyType = qty.UncertaintyType;

                // rto properties
                PropertyInfo numeratorProperty = rtoGenericType.GetProperty("Numerator"),
                    denominatorProperty = rtoGenericType.GetProperty("Denominator");

                // Set the previously found properties
                numeratorProperty.SetValue(instance, Util.FromWireFormat(numerator, numeratorProperty.PropertyType), null);
                denominatorProperty.SetValue(instance, Util.FromWireFormat(denominator, denominatorProperty.PropertyType), null);

                // Validate
                ANYFormatter anyFormatter = new ANYFormatter();
                string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
                anyFormatter.Validate(instance as ANY, pathName, result);

                return instance;
            }
            catch (Exception e)
            {
                result.AddResultDetail(new ResultDetail(ResultDetailType.Error, e.Message, e));
                return null;
            }
        }

        /// <summary>
        /// Get the type that this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "RTO"; }
        }

        /// <summary>
        /// Gets or sets the host of the formatter
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
        /// Gets the list of supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>((new QTYFormatter()).GetSupportedProperties());
            retVal.Add(typeof(RTO<,>).GetProperty("Numerator"));
            retVal.Add(typeof(RTO<,>).GetProperty("Denominator"));
            return retVal;
        }

        #endregion
    }
}
