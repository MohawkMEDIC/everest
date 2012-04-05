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
 * Date: 9/17/2009 11:48:28 AM
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
    /// Data types R1 formatter for RTO type
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RTO")]
    public class RTOFormatter : IDatatypeFormatter
    {

        #region IDatatypeFormatter Members

      
        /// <summary>
        /// Graph object <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {

            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, o, result);

            // Since this is a generic type, we'll need to get the values we're interested in
            // for the formatting operation
            Type oType = o.GetType();
            object numerator = oType.GetProperty("Numerator").GetValue(o, null),
                denominator = oType.GetProperty("Denominator").GetValue(o, null);

            // Check for the QTY members
            QTY<Nullable<Double>> qtyPortion = o as QTY<Nullable<Double>>;

            // Check for non-representable properties
            if (qtyPortion.Expression != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Expression", "RTO", s.ToString()));
            if (qtyPortion.OriginalText != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "OriginalText", "RTO", s.ToString()));
            if (qtyPortion.Uncertainty != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Uncertainty", "RTO", s.ToString()));
            if (qtyPortion.UncertaintyType != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "UncertaintyType", "RTO", s.ToString()));
            if (qtyPortion.UncertainRange != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "UncertainRange", "RTO", s.ToString()));
            if (qtyPortion.Value != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Value", "RTO", s.ToString()));
            

            IXmlStructureFormatter serFormatter = Host;

            // Serialize the data-type
            if (numerator != null)
            {
                s.WriteStartElement("numerator", "urn:hl7-org:v3");

                // Write the XSI type
                string xsiTypeName = Util.CreateXSITypeName(numerator.GetType());

                // Write the type
                if (this.Host.Host == null)
                    s.WriteAttributeString("type", DatatypeFormatter.NS_XSI, xsiTypeName.ToString());
                else
                    s.WriteAttributeString("xsi", "type", DatatypeFormatter.NS_XSI, xsiTypeName.ToString());

                var hostResult = serFormatter.Graph(s, (IGraphable)numerator);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }
            if (denominator != null)
            {
                s.WriteStartElement("denominator", "urn:hl7-org:v3");

                // Write the XSI type
                string xsiTypeName = Util.CreateXSITypeName(denominator.GetType());

                // Write the type
                if (this.Host.Host == null)
                    s.WriteAttributeString("type", DatatypeFormatter.NS_XSI, xsiTypeName.ToString());
                else
                    s.WriteAttributeString("xsi", "type", DatatypeFormatter.NS_XSI, xsiTypeName.ToString());

                var hostResult = serFormatter.Graph(s, (IGraphable)denominator);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            } 

            // Append details
            //details.AddRange(serFormatter.Details);
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse from</param>
        /// <returns>The parsed object</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            
            // Create the types
            Type rtoType = typeof(RTO<,>);
            Type rtoGenericType = rtoType.MakeGenericType(GenericArguments);

            // Create an instance of rto from the rtoType
            object instance = rtoGenericType.GetConstructor(Type.EmptyTypes).Invoke(null);

            if (s.GetAttribute("nullFlavor") != null)
                ((ANY)instance).NullFlavor = (NullFlavor)Util.FromWireFormat(s.GetAttribute("nullFlavor"), typeof(NullFlavor));
            else
            {
                PropertyInfo numeratorProperty= rtoGenericType.GetProperty("Numerator"),
                    denominatorProperty = rtoGenericType.GetProperty("Denominator");

                if (s.GetAttribute("specializationType") != null)
                    ((ANY)instance).Flavor = s.GetAttribute("specializationType");

                // Get the values
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
                            if (s.LocalName == "numerator")
                            {
                                var parseResult = Host.Parse(s, GenericArguments[0]);
                                result.Code = parseResult.Code;
                                result.AddResultDetail(parseResult.Details);
                                numeratorProperty.SetValue(instance, parseResult.Structure, null);
                            }
                            else if (s.LocalName == "denominator")
                            {
                                var parseResult = Host.Parse(s, GenericArguments[1]);
                                result.Code = parseResult.Code;
                                result.AddResultDetail(parseResult.Details);
                                denominatorProperty.SetValue(instance, parseResult.Structure, null);
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

            }

            // Validate
            ANYFormatter anyFormatter = new ANYFormatter();
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            anyFormatter.Validate(instance as ANY, pathName, result);

            return instance;
        }

        /// <summary>
        /// Get the type that this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "RTO"; }
        }

        /// <summary>
        /// Get or set the host of this formatter
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Generic arguments
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(RTO<,>).GetProperty("Numerator"));
            retVal.Add(typeof(RTO<,>).GetProperty("Denominator"));
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}