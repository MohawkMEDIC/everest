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
using MARC.Everest.Interfaces;
using System.Reflection;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// A formatter that can represent the QSP data type in R2 format
    /// </summary>
    internal class QSPFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph object <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Graph the base data
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, o, result);

            // Is null flavor set?
            if ((o as ANY).NullFlavor != null)
                return;

            // Original text
            IOriginalText originalText = o as IOriginalText;
            if (originalText.OriginalText != null)
            {
                s.WriteStartElement("originalText", null);
                var hostResult = this.Host.Graph(s, originalText.OriginalText);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }

            // Get properties we'll be graphing
            object lowValue = o.GetType().GetProperty("Low").GetValue(o, null),
                highValue = o.GetType().GetProperty("High").GetValue(o, null);
            

            // Graph low / hi
            if (lowValue != null)
            {
                s.WriteStartElement("low", null);
                s.WriteAttributeString("xsi", "type", DatatypeR2Formatter.NS_XSI, DatatypeR2Formatter.CreateXSITypeName(lowValue.GetType()));
                var hostResult = this.Host.Graph(s, lowValue as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }
            if (highValue != null)
            {
                s.WriteStartElement("high", null);
                s.WriteAttributeString("xsi", "type", DatatypeR2Formatter.NS_XSI, DatatypeR2Formatter.CreateXSITypeName(lowValue.GetType()));
                var hostResult = this.Host.Graph(s, highValue as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }


        }

        /// <summary>
        /// Parse an object from <paramref name="s"/> into an instance of QSP
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Create the types
            Type qspType = typeof(QSP<>);
            Type qspGenericType = qspType.MakeGenericType(GenericArguments);

            // Create an instance of rto from the rtoType
            object instance = qspGenericType.GetConstructor(Type.EmptyTypes).Invoke(null);

            // Parse an any
            ANYFormatter baseFormatter = new ANYFormatter();
            DatatypeR2Formatter.CopyBaseAttributes(baseFormatter.Parse(s, result) as ANY, instance as ANY);

            if ((instance as IAny).NullFlavor == null)
            {

                // Get property information
                PropertyInfo lowProperty = qspGenericType.GetProperty("Low"),
                    highProperty = qspGenericType.GetProperty("High"),
                    originalTextProperty = qspGenericType.GetProperty("OriginalText");

                // Now process the elements
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
                            if (s.NodeType == System.Xml.XmlNodeType.Element)
                            {
                                PropertyInfo setProperty = null;
                                switch (s.LocalName)
                                {
                                    case "originalText":
                                        setProperty = originalTextProperty;
                                        break;
                                    case "low":
                                        setProperty = lowProperty;
                                        break;
                                    case "high":
                                        setProperty = highProperty;
                                        break;
                                    default:
                                        result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null));
                                        break;
                                }

                                // Set the property if we found something valid
                                if (setProperty != null)
                                {
                                    var hostResult = this.Host.Parse(s, setProperty.PropertyType);
                                    result.Code = hostResult.Code;
                                    result.AddResultDetail(hostResult.Details);
                                    setProperty.SetValue(instance, Util.FromWireFormat(hostResult.Structure, setProperty.PropertyType), null);
                                }
                            }
                        }
                        catch (MessageValidationException e) // Message validation error
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
            ANYFormatter validation = new ANYFormatter();
            validation.Validate(instance as ANY, s.LocalName, result);

            return instance;
        }

        /// <summary>
        /// Gets the name of the type that this represents
        /// </summary>
        public string HandlesType
        {
            get { return "QSP"; }
        }

        /// <summary>
        /// Gets or sets the host context of the formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets the generic type arguments for the formatters
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Gets the supported properties
        /// </summary>
        /// <returns></returns>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            var retVal = new List<PropertyInfo>(10);
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            retVal.AddRange(typeof(QSP<>).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public));
            retVal.Add(typeof(QSET<>).GetProperty("OriginalText"));
            return retVal;
        }

        #endregion
    }
}
