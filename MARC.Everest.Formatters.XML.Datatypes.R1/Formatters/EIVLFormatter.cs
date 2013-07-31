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
 * User: fyfej
 * Date: 4/27/2010 12:14:39 PM
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using MARC.Everest.Interfaces;
using System.Reflection;
using MARC.Everest.Exceptions;
using MARC.Everest.Xml;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// EIVL formatter
    /// </summary>
    public class EIVLFormatter : ANYFormatter, IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        
        /// <summary>
        /// Graph object <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            base.Graph(s, o, result);
            
            // Now graph the attributes
            Type eivlType = o.GetType();
            object eventValue = eivlType.GetProperty("Event").GetValue(o, null),
                offsetValue = eivlType.GetProperty("Offset").GetValue(o, null),
                operatorValue = eivlType.GetProperty("Operator").GetValue(o, null);

            // Append the attributes to the writer
            if ((o as ANY).NullFlavor != null)
                return; // Nothing to report
            if (operatorValue != null)
                s.WriteAttributeString("operator", Util.ToWireFormat(operatorValue));

            // Write elements
            if (eventValue != null)
            {
                s.WriteStartElement("event", "urn:hl7-org:v3");
                var hostResult = Host.Graph(s, (IGraphable)eventValue);
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }
            if (offsetValue != null)
            {
                s.WriteStartElement("offset", "urn:hl7-org:v3");
                var hostResult = Host.Graph(s, (IGraphable)offsetValue);
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }

        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Create the types
            Type eivlType = typeof(EIVL<>);
            Type eivlGenericType = eivlType.MakeGenericType(GenericArguments);

            // For the phase
            Type ivlType = typeof(IVL<PQ>);

            // Create an instance of rto from the rtoType
            object instance = eivlGenericType.GetConstructor(Type.EmptyTypes).Invoke(null);

            if (s.GetAttribute("nullFlavor") != null)
                ((ANY)instance).NullFlavor = (NullFlavor)Util.FromWireFormat(s.GetAttribute("nullFlavor"), typeof(NullFlavor));
                // Try get operator and value
                if (s.GetAttribute("operator") != null)
                    eivlGenericType.GetProperty("Operator").SetValue(instance, Util.FromWireFormat(s.GetAttribute("operator"), typeof(SetOperator?)), null);
                if (s.GetAttribute("value") != null)
                    result.AddResultDetail(new NotSupportedChoiceResultDetail(
                        ResultDetailType.Warning, "The 'value' attribute of a SXCM does not interpretable in this context, and has been ignored", s.ToString(), null));

                // JF - Spec type is a CA extension
                if (s.GetAttribute("specializationType") != null && result.CompatibilityMode == DatatypeFormatterCompatibilityMode.Canadian)
                    ((ANY)instance).Flavor = s.GetAttribute("specializationType");

                // Get property information
                PropertyInfo eventProperty = eivlGenericType.GetProperty("Event"),
                   offsetProperty = eivlGenericType.GetProperty("Offset");

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
                            if (s.LocalName == "offset") // low value
                            {
                                var hostResult = Host.Parse(s, typeof(IVL<PQ>));
                                result.AddResultDetail(hostResult.Details);
                                offsetProperty.SetValue(instance, hostResult.Structure, null);
                            }
                            else if (s.LocalName == "event") // high value
                            {
                                var hostResult = Host.Parse(s, typeof(CS<DomainTimingEventType>));
                                result.AddResultDetail(hostResult.Details);
                                eventProperty.SetValue(instance, Util.Convert<CS<DomainTimingEventType>>(hostResult.Structure), null);
                            }
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
                #endregion


            // Validate
            ANYFormatter validation = new ANYFormatter();
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            validation.Validate(instance as ANY, pathName, result);

            return instance;
        }

        /// <summary>
        /// Gets the type that this instance handles
        /// </summary>
        public override string HandlesType
        {
            get { return "EIVL"; }
        }
        
        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(){
            typeof(EIVL<>).GetProperty("Event"),
            typeof(EIVL<>).GetProperty("Offset"),
            typeof(EIVL<>).GetProperty("Operator")
            };
            retVal.AddRange(base.GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}
