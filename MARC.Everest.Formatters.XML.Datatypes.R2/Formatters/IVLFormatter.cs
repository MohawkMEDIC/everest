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
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;
using System.Reflection;
using MARC.Everest.Exceptions;
using MARC.Everest.DataTypes.Interfaces;
using System.Xml;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Represents a formatter that can represent the IVL type in Data Types R2
    /// </summary>
    internal class IVLFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members
        
        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {

            // Format base attributes
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, o, result);

            if ((o as ANY).NullFlavor != null) return; // no need to continue formatting

            // Get the values the formatter needs to represent in XML
            Type ivlType = o.GetType();
            object operatorValue = ivlType.GetProperty("Operator").GetValue(o, null),
                lowValue = ivlType.GetProperty("Low").GetValue(o, null),
                highValue = ivlType.GetProperty("High").GetValue(o, null),
                widthValue = ivlType.GetProperty("Width").GetValue(o, null),
                originalTextValue = ivlType.GetProperty("OriginalText").GetValue(o, null),
                lowClosedValue = ivlType.GetProperty("LowClosed").GetValue(o, null),
                highClosedValue = ivlType.GetProperty("HighClosed").GetValue(o, null),
                valueValue = ivlType.GetProperty("Value").GetValue(o, null);

            // Low / high closed
            if (lowClosedValue != null)
                s.WriteAttributeString("lowClosed", Util.ToWireFormat(lowClosedValue));
            if (highClosedValue != null)
                s.WriteAttributeString("highClosed", Util.ToWireFormat(highClosedValue));
            if (operatorValue != null)
                result.AddResultDetail(new UnsupportedDatatypeR2PropertyResultDetail(ResultDetailType.Warning, "Operator", "IVL", s.ToString()));

            // Original text
            if (originalTextValue != null)
            {
                s.WriteStartElement("originalText", null);
                var hostResult = this.Host.Graph(s, originalTextValue as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }

            bool invalidCombo = false;
            // Valid combinations
            if (lowValue != null)
            {
                s.WriteStartElement("low", null);

                // Low value XSI?
                if (lowValue.GetType() != GenericArguments[0])
                    s.WriteAttributeString("xsi", "type", DatatypeR2Formatter.NS_XSI, DatatypeR2Formatter.CreateXSITypeName(lowValue.GetType()));

                var hostResult = this.Host.Graph(s, lowValue as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
                if (highValue != null)
                {
                    s.WriteStartElement("high", null);

                    if (highValue.GetType() != GenericArguments[0])
                        s.WriteAttributeString("xsi", "type", DatatypeR2Formatter.NS_XSI, DatatypeR2Formatter.CreateXSITypeName(highValue.GetType()));

                    hostResult = this.Host.Graph(s, highValue as IGraphable);
                    result.Code = hostResult.Code;
                    result.AddResultDetail(hostResult.Details);
                    s.WriteEndElement();
                }
                invalidCombo |= valueValue != null || widthValue != null;
            }
            else if (highValue != null)
            {
                s.WriteStartElement("high", null);

                // High value XSI
                if (highValue.GetType() != GenericArguments[0])
                    s.WriteAttributeString("xsi", "type", DatatypeR2Formatter.NS_XSI, DatatypeR2Formatter.CreateXSITypeName(highValue.GetType()));
                
                var hostResult = this.Host.Graph(s, highValue as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
                invalidCombo |= valueValue != null || widthValue != null;
            }
            if (widthValue != null && ((lowValue == null) ^ (highValue == null) || lowValue == null && highValue == null ))
            {
                s.WriteStartElement("width", null);
                var hostResult = this.Host.Graph(s, widthValue as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
                if (valueValue != null)
                {
                    s.WriteStartElement("any", null);

                    // Value value xsi type
                    if (valueValue.GetType() != GenericArguments[0])
                        s.WriteAttributeString("xsi", "type", DatatypeR2Formatter.NS_XSI, DatatypeR2Formatter.CreateXSITypeName(valueValue.GetType()));

                    hostResult = this.Host.Graph(s, valueValue as IGraphable);
                    result.Code = hostResult.Code;
                    result.AddResultDetail(hostResult.Details);
                    s.WriteEndElement();
                }
                invalidCombo |= lowValue != null || highValue != null;
            }
            else if (valueValue != null)
            {
                s.WriteStartElement("any", null);

                // Value value xsi type
                if (valueValue.GetType() != GenericArguments[0])
                    s.WriteAttributeString("xsi", "type", DatatypeR2Formatter.NS_XSI, DatatypeR2Formatter.CreateXSITypeName(valueValue.GetType()));

                var hostResult = this.Host.Graph(s, valueValue as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
                invalidCombo |= lowValue != null || highValue != null;
            }
            else
                invalidCombo = true;

            // Invalid combination, warn
            if(invalidCombo)
                result.AddResultDetail(new NotSupportedChoiceResultDetail(ResultDetailType.Warning, "This IVL is not conformant to the ISO21090 data types specification. Only 'Value' and/or 'Width', or 'Low' and/or 'High' are permitted. All data has been serialized but may not be interpreted by a remote system", s.ToString(), null));


        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Create the types
            Type ivlType = typeof(IVL<>);
            Type ivlGenericType = ivlType.MakeGenericType(GenericArguments);

            // Create an instance of rto from the rtoType
            object instance = ivlGenericType.GetConstructor(Type.EmptyTypes).Invoke(null);

            // Parse an any
            ANYFormatter baseFormatter = new ANYFormatter();
            DatatypeR2Formatter.CopyBaseAttributes(baseFormatter.Parse(s, result) as ANY, instance as ANY);

            if ((instance as IAny).NullFlavor == null)
            {

                // Try get operator and value
                if (s.GetAttribute("operator") != null)
                    result.AddResultDetail(new NotPermittedDatatypeR2EntityResultDetail(ResultDetailType.Warning, "operator", "IVL", s.ToString()));
                if (s.GetAttribute("value") != null)
                    result.AddResultDetail(new NotPermittedDatatypeR2EntityResultDetail(ResultDetailType.Warning, "value", "IVL", s.ToString()));
                
                // Get property information
                PropertyInfo lowProperty = ivlGenericType.GetProperty("Low"),
                    highProperty = ivlGenericType.GetProperty("High"),
                    widthProperty = ivlGenericType.GetProperty("Width"),
                    valueProperty = ivlGenericType.GetProperty("Value"),
                    originalTextProperty = ivlGenericType.GetProperty("OriginalText"),
                    lowClosedProperty = ivlGenericType.GetProperty("LowClosed"),
                    highClosedProperty = ivlGenericType.GetProperty("HighClosed");

                // Attributes
                if(s.GetAttribute("lowClosed") != null)
                    lowClosedProperty.SetValue(instance, XmlConvert.ToBoolean(s.GetAttribute("lowClosed")), null);
                if(s.GetAttribute("highClosed") != null)
                    highClosedProperty.SetValue(instance, XmlConvert.ToBoolean(s.GetAttribute("highClosed")), null);

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
                            if(s.NodeType == System.Xml.XmlNodeType.Element)
                            {
                                PropertyInfo setProperty = null;
                                switch(s.LocalName)
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
                                    case "any":
                                        setProperty = valueProperty;
                                        break;
                                    case "width":
                                        setProperty = widthProperty;
                                        break;
                                    default:
                                        result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, String.Format("Element '{0}' is not supported in IVL", s.LocalName), s.ToString(), null));
                                        break;
                                }

                                // Set the property if we found something valid
                                if(setProperty != null)
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

                // Validate combination
                if(!((lowProperty.GetValue(instance, null) != null || highProperty.GetValue(instance, null) != null) ^ (valueProperty.GetValue(instance, null) != null || widthProperty.GetValue(instance, null) != null)))
                    result.AddResultDetail(new NotSupportedChoiceResultDetail(ResultDetailType.Warning, "IVL instance is not comformant to ISO21090 data types specification. 'Value' and/or 'Width', or 'Low' and/or 'High' are compliant representations.", s.ToString(), null));

            }

            // Validate
            ANYFormatter validation = new ANYFormatter();
            validation.Validate(instance as ANY, s.LocalName, result);

            return instance;

        }

        /// <summary>
        /// Return the handles type
        /// </summary>
        public string HandlesType
        {
            get { return "IVL"; }
        }

        /// <summary>
        /// Gets or sets the host formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets the generic arguments
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get the supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(20);
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            retVal.AddRange(typeof(IVL<>).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly));
            retVal.Add(typeof(IVL<>).GetProperty("Value"));
            return retVal;
        }

        #endregion
    }
}
