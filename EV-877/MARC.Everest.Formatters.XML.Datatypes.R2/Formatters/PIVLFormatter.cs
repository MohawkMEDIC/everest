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
 * Date: 11-12-2011 (Yes, that is a Saturday)
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;
using MARC.Everest.DataTypes;
using System.Reflection;
using MARC.Everest.Exceptions;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// A formatter helper that can represent an instance of PIVL in DT R2
    /// </summary>
    public class PIVLFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        /// <remarks>For some reason this type is not defined in the ISO Data types XSD</remarks>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {

            // Format the base
            ANYFormatter anyFormatter = new ANYFormatter();
            anyFormatter.Graph(s, o, result);

            // If null flavor is not
            if ((o as IAny).NullFlavor != null)
                return;

            // Get the property information
            Type pivlType = o.GetType();
            object valueValue = pivlType.GetProperty("Value").GetValue(o, null),
                operatorValue = pivlType.GetProperty("Operator").GetValue(o, null),
                alignmentValue = pivlType.GetProperty("Alignment").GetValue(o, null),
                phaseValue = pivlType.GetProperty("Phase").GetValue(o, null),
                periodValue = pivlType.GetProperty("Period").GetValue(o, null),
                institutionSpecifiedValue = pivlType.GetProperty("InstitutionSpecified").GetValue(o, null),
                countValue = pivlType.GetProperty("Count").GetValue(o, null),
                frequencyValue = pivlType.GetProperty("Frequency").GetValue(o, null),
                originalTextValue = pivlType.GetProperty("OriginalText").GetValue(o, null);

            // Attributes
            if (institutionSpecifiedValue != null)
                s.WriteAttributeString("isFlexible", institutionSpecifiedValue.ToString().ToLower());
            if (alignmentValue != null)
                s.WriteAttributeString("alignment", Util.ToWireFormat(alignmentValue));
            if (countValue != null)
                s.WriteAttributeString("count", Util.ToWireFormat(countValue));
            if (operatorValue != null)
                result.AddResultDetail(new UnsupportedDatatypeR2PropertyResultDetail(ResultDetailType.Warning, "Operator", "PIVL", s.ToString()));
            if (valueValue != null)
                result.AddResultDetail(new UnsupportedDatatypeR2PropertyResultDetail(ResultDetailType.Warning, "Value", "PIVL", s.ToString()));

            // Original text
            if (originalTextValue != null)
            {
                s.WriteStartElement("originalText", "urn:hl7-org:v3");
                var hostResult = this.Host.Graph(s, originalTextValue as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(result.Details);
                s.WriteEndElement();
            }

            // Phase
            if (phaseValue != null)
            {
                s.WriteStartElement("phase", "urn:hl7-org:v3");
                var hostResult = this.Host.Graph(s, phaseValue as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(result.Details);
                s.WriteEndElement();
            }
            
            // Frequency or period
            if(frequencyValue != null)
            {
                s.WriteStartElement("frequency", "urn:hl7-org:v3");
                var hostResult = this.Host.Graph(s, frequencyValue as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(result.Details);
                s.WriteEndElement();
            }
            else if (periodValue != null)
            {
                s.WriteStartElement("period", "urn:hl7-org:v3");
                var hostResult = this.Host.Graph(s, periodValue as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(result.Details);
                s.WriteEndElement();
                if (frequencyValue != null)
                    result.AddResultDetail(new NotSupportedChoiceResultDetail(ResultDetailType.Warning, "'Period' and 'Frequency' specified, this is not permitted, either 'Period' xor 'Frequency' may be represented. Will only represent 'Period' property", s.ToString(), null));
            }
        }

        /// <summary>
        /// Parse an instance of PIVL from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Create the types
            Type pivlType = typeof(PIVL<>);
            Type ivlGenericType = pivlType.MakeGenericType(GenericArguments);

            // Create an instance of rto from the rtoType
            object instance = ivlGenericType.GetConstructor(Type.EmptyTypes).Invoke(null);

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
                PropertyInfo alignmentProperty = ivlGenericType.GetProperty("Alignment"),
                    phaseProperty = ivlGenericType.GetProperty("Phase"),
                    periodProperty = ivlGenericType.GetProperty("Period"),
                    institutionSpecifiedProperty = ivlGenericType.GetProperty("InstitutionSpecified"),
                    countProperty = ivlGenericType.GetProperty("Count"),
                    frequencyProperty = ivlGenericType.GetProperty("Frequency"),
                    originalTextProperty = ivlGenericType.GetProperty("OriginalText");

                // Attributes
                if(s.GetAttribute("isFlexible") != null)
                    institutionSpecifiedProperty.SetValue(instance, Convert.ToBoolean(s.GetAttribute("isFlexible")), null);
                if(s.GetAttribute("alignment") != null)
                    alignmentProperty.SetValue(instance, Util.FromWireFormat(s.GetAttribute("alignment"), alignmentProperty.PropertyType), null);
                if(s.GetAttribute("count") != null)
                    countProperty.SetValue(instance, Util.Convert<INT>(s.GetAttribute("count")), null);

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
                                    case "phase":
                                        setProperty = phaseProperty;
                                        break;
                                    case "frequency":
                                        setProperty = frequencyProperty;
                                        break;
                                    case "period":
                                        setProperty = periodProperty;
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
            get { return "PIVL"; }
        }

        /// <summary>
        /// Gets or sets the host of this formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; } 

        /// <summary>
        /// Gets or sets the generic type arguments to this formatter
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Gets a list of properties that are handled by this formatter
        /// </summary>
        /// <returns></returns>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            retVal.Add(typeof(PIVL<>).GetProperty("Phase"));
            retVal.Add(typeof(PIVL<>).GetProperty("Period"));
            retVal.Add(typeof(PIVL<>).GetProperty("Frequency"));
            retVal.Add(typeof(PIVL<>).GetProperty("OriginalText"));
            retVal.Add(typeof(PIVL<>).GetProperty("InstitutionSpecified"));
            retVal.Add(typeof(PIVL<>).GetProperty("Count"));
            retVal.Add(typeof(PIVL<>).GetProperty("Alignment"));
            return retVal;
        }

        #endregion
    }
}
