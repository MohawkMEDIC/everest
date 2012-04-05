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
using System.Reflection;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Exceptions;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// A formatter that can represent an EIVL in data types R2 format
    /// </summary>
    public class EIVLFormatter : IDatatypeFormatter
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
            Type eivlType = o.GetType();
            object operatorValue = eivlType.GetProperty("Operator").GetValue(o, null),
                valueValue = eivlType.GetProperty("Value").GetValue(o, null),
                originalTextValue = eivlType.GetProperty("OriginalText").GetValue(o, null),
                offsetValue = eivlType.GetProperty("Offset").GetValue(o, null),
                eventValue = eivlType.GetProperty("Event").GetValue(o, null)
                ;

            // Low / high closed
            if (operatorValue != null)
                result.AddResultDetail(new UnsupportedDatatypeR2PropertyResultDetail(ResultDetailType.Warning, "Operator", "EIVL", s.ToString()));
            if (valueValue != null)
                result.AddResultDetail(new UnsupportedDatatypeR2PropertyResultDetail(ResultDetailType.Warning, "Value", "EIVL", s.ToString()));
            if (eventValue != null)
                s.WriteAttributeString("event", Util.ToWireFormat(eventValue));

            // Original text
            if (originalTextValue != null)
            {
                s.WriteStartElement("originalText", "urn:hl7-org:v3");
                var hostResult = this.Host.Graph(s, originalTextValue as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }
            // Valid combinations
            if (offsetValue != null)
            {
                s.WriteStartElement("offset", "urn:hl7-org:v3");
                var hostResult = this.Host.Graph(s, offsetValue as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }

        }

        /// <summary>
        /// Parse an instance of EIVL from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Create the types
            Type eivlType = typeof(EIVL<>);
            Type eivlGenericType = eivlType.MakeGenericType(GenericArguments);

            // Create an instance of rto from the rtoType
            object instance = eivlGenericType.GetConstructor(Type.EmptyTypes).Invoke(null);

            // Parse an any
            ANYFormatter baseFormatter = new ANYFormatter();
            DatatypeR2Formatter.CopyBaseAttributes(baseFormatter.Parse(s, result) as ANY, instance as ANY);

            if ((instance as IAny).NullFlavor == null)
            {
                // Try get operator and value
                if (s.GetAttribute("operator") != null)
                    result.AddResultDetail(new NotPermittedDatatypeR2EntityResultDetail(ResultDetailType.Warning, "operator", "PIVL", s.ToString()));
                if (s.GetAttribute("value") != null)
                    result.AddResultDetail(new NotPermittedDatatypeR2EntityResultDetail(ResultDetailType.Warning, "value", "PIVL", s.ToString()));
                
                // Get property information
                PropertyInfo eventProperty = eivlGenericType.GetProperty("Event"),
                    offsetProperty = eivlGenericType.GetProperty("Offset"),
                    originalTextProperty = eivlGenericType.GetProperty("OriginalText");

                // Attributes
                if(s.GetAttribute("event") != null)
                    eventProperty.SetValue(instance, Util.FromWireFormat(s.GetAttribute("event"), eventProperty.PropertyType), null);

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
                                    case "offset":
                                        setProperty = offsetProperty;
                                        break;
                                    default:
                                        result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null));
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

            }

            // Validate
            ANYFormatter validation = new ANYFormatter();
            validation.Validate(instance as ANY, s.LocalName, result);

            return instance;
        }

        /// <summary>
        /// Gets the type that this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "EIVL"; }
        }

        /// <summary>
        /// Gets or sets the host of this 
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the generic type arguments for this formatter
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get a list of supported properties
        /// </summary>
        /// <returns></returns>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            retVal.Add(typeof(EIVL<>).GetProperty("Offset"));
            retVal.Add(typeof(EIVL<>).GetProperty("Event"));
            retVal.Add(typeof(EIVL<>).GetProperty("OriginalText"));
            return retVal;
        }

        #endregion
    }
}
