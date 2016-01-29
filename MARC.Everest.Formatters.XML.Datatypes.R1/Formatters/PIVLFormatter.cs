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
 * User: computc
 * Date: 9/24/2009 12:14:39 PM
 **/
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
    /// Summary Documentation
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PIVL")]
    public class PIVLFormatter : ANYFormatter, IDatatypeFormatter
    {

        #region IDatatypeFormatter Members


        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            base.Graph(s, o, result);

            // Now graph the attributes
            Type pivlType = o.GetType();
            object valueValue = pivlType.GetProperty("Value").GetValue(o, null),
                operatorValue = pivlType.GetProperty("Operator").GetValue(o, null),
                alignmentValue = pivlType.GetProperty("Alignment").GetValue(o, null),
                phaseValue = pivlType.GetProperty("Phase").GetValue(o, null),
                periodValue = pivlType.GetProperty("Period").GetValue(o, null),
                institutionSpecifiedValue = pivlType.GetProperty("InstitutionSpecified").GetValue(o, null),
                countValue = pivlType.GetProperty("Count").GetValue(o, null),
                frequencyValue = pivlType.GetProperty("Frequency").GetValue(o, null);

            // Append the attributes to the writer
            if ((o as ANY).NullFlavor != null)
                return; // Nothing to report
            if (valueValue != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Value", "PIVL", s.ToString()));
                //s.WriteAttributeString("value", Util.ToWireFormat(valueValue));
            if (operatorValue != null)
                s.WriteAttributeString("operator", Util.ToWireFormat(operatorValue));
            if (alignmentValue != null)
                s.WriteAttributeString("alignment", Util.ToWireFormat(alignmentValue));
            if (institutionSpecifiedValue != null)
                s.WriteAttributeString("institutionSpecified", Util.ToWireFormat(institutionSpecifiedValue));

            if (countValue != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Count", "PIVL", s.ToString()));

            // Write elements
            if (phaseValue != null)
            {
                s.WriteStartElement("phase", "urn:hl7-org:v3");
                var hostResult = Host.Graph(s, (IGraphable)phaseValue);
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }

            if (frequencyValue != null)
            {
                // JF - Frequency is not supported by UV R1
                if (result.CompatibilityMode == DatatypeFormatterCompatibilityMode.Canadian)
                {
                    s.WriteStartElement("frequency", "urn:hl7-org:v3");
                    var hostResult = this.Host.Graph(s, (IGraphable)frequencyValue);
                    result.AddResultDetail(hostResult.Details);
                    result.Code = hostResult.Code;
                    s.WriteEndElement();
                }
                else
                {
                    RTO<INT, PQ> rto = frequencyValue as RTO<INT, PQ>;
                    periodValue = rto.Denominator / rto.Numerator;
                    result.AddResultDetail(new PropertyValuePropagatedResultDetail(ResultDetailType.Warning, "Frequency", "Period", periodValue, s.ToString()));
                    s.WriteStartElement("period", "urn:hl7-org:v3");
                    var hostResult = Host.Graph(s, (IGraphable)periodValue);
                    result.AddResultDetail(hostResult.Details);
                    result.Code = hostResult.Code;
                    s.WriteEndElement();
                }
            }
            else if (periodValue != null)
            {
                s.WriteStartElement("period", "urn:hl7-org:v3");
                var hostResult = Host.Graph(s, (IGraphable)periodValue);
                result.AddResultDetail(hostResult.Details);
                result.Code = hostResult.Code;
                s.WriteEndElement();
            }

            
        }

        /// <summary>
        /// Parse an instance from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse from</param>
        /// <returns>The parsed object</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Create the types
            Type pivlType = typeof(PIVL<>);
            Type pivlGenericType = pivlType.MakeGenericType(GenericArguments);

            // For the phase
            Type ivlType = typeof(IVL<>);
            Type phaseIvlType = ivlType.MakeGenericType(GenericArguments);

            // Create an instance of rto from the rtoType
            object instance = pivlGenericType.GetConstructor(Type.EmptyTypes).Invoke(null);

            if (s.GetAttribute("specializationType") != null && result.CompatibilityMode == DatatypeFormatterCompatibilityMode.Canadian)
                ((ANY)instance).Flavor = s.GetAttribute("specializationType");

            if (s.GetAttribute("nullFlavor") != null)
                ((ANY)instance).NullFlavor = (NullFlavor)Util.FromWireFormat(s.GetAttribute("nullFlavor"), typeof(NullFlavor));

            // Try get operator and value
            if (s.GetAttribute("operator") != null)
                pivlGenericType.GetProperty("Operator").SetValue(instance, Util.FromWireFormat(s.GetAttribute("operator"), typeof(SetOperator?)), null);
            if (s.GetAttribute("value") != null)
                result.AddResultDetail(new NotSupportedChoiceResultDetail(
                    ResultDetailType.Warning, "The 'value' attribute of a SXCM does not interpretable in this context, and has been ignored", s.ToString(), null));

            //    pivlGenericType.GetProperty("Value").SetValue(instance, Util.FromWireFormat(s.GetAttribute("value"), GenericArguments[0]), null);
            if (s.GetAttribute("institutionSpecified") != null)
                pivlGenericType.GetProperty("InstitutionSpecified").SetValue(instance, Util.FromWireFormat(s.GetAttribute("institutionSpecified"), typeof(bool?)), null);
            if (s.GetAttribute("alignment") != null)
                pivlGenericType.GetProperty("Alignment").SetValue(instance, Util.FromWireFormat(s.GetAttribute("alignment"), typeof(CalendarCycle?)), null);

            // Get property information
            PropertyInfo phaseProperty = pivlGenericType.GetProperty("Phase"),
               periodProperty = pivlGenericType.GetProperty("Period"),
               frequencyProperty = pivlGenericType.GetProperty("Frequency");

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
                        if (s.LocalName == "phase") // low value
                        {
                            var hostResult = Host.Parse(s, phaseIvlType);
                            result.AddResultDetail(hostResult.Details);
                            phaseProperty.SetValue(instance, hostResult.Structure, null);
                        }
                        else if (s.LocalName == "period") // high value
                        {
                            var hostResult = Host.Parse(s, typeof(PQ));
                            result.AddResultDetail(hostResult.Details);
                            periodProperty.SetValue(instance, hostResult.Structure, null);
                        }
                        else if (s.LocalName == "frequency" && result.CompatibilityMode == DatatypeFormatterCompatibilityMode.Canadian) // frequency
                        {
                            var hostResult = Host.Parse(s, typeof(RTO<INT, PQ>));
                            result.AddResultDetail(hostResult.Details);
                            frequencyProperty.SetValue(instance, hostResult.Structure, null);
                        }
                        else if (s.NodeType == System.Xml.XmlNodeType.Element)
                            result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning,
                                s.LocalName, s.NamespaceURI, s.ToString(), null));


                    }
                    catch (MessageValidationException e) // Message validation error
                    {
                        result.AddResultDetail(new MARC.Everest.Connectors.ResultDetail(MARC.Everest.Connectors.ResultDetailType.Error, e.Message, s.ToString(), e));
                    }
                    finally
                    {
                        if (s.Name == oldName) s.Read();
                    }
                }
            }
            else
                ;
            #endregion


            // Validate
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            base.Validate(instance as ANY, pathName, result);

            return instance;
        }

        /// <summary>
        /// Gets the type that this formatter handles
        /// </summary>
        public override string HandlesType
        {
            get { return "PIVL"; }
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.AddRange(new PropertyInfo[] {
                typeof(PIVL<>).GetProperty("Operator"),
                typeof(PIVL<>).GetProperty("Alignment"),
                typeof(PIVL<>).GetProperty("Phase"),
                typeof(PIVL<>).GetProperty("Period"),
                typeof(PIVL<>).GetProperty("InstitutionSpecified")
            });
            retVal.AddRange(base.GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}