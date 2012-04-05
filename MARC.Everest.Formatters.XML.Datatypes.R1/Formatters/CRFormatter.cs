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
 * Date: 9/10/2009 12:43:15 PM
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using System.Reflection;
using MARC.Everest.DataTypes.Interfaces;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Concept Role formatter for DT R1
    /// </summary>
    public class CRFormatter : IDatatypeFormatter
    {

        #region IDatatypeFormatter Members

     
        /// <summary>
        /// Graph object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {

            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, o, result); // Graph the base data


            // If there is no null flavor then graph the rest of the object
            
            object invertedValue = o.GetType().GetProperty("Inverted").GetValue(o, null),
                nameValue = o.GetType().GetProperty("Name").GetValue(o, null),
                valueValue = o.GetType().GetProperty("Value").GetValue(o, null);

            // Graph the structural attributes
            s.WriteAttributeString("inverted", Util.ToWireFormat(invertedValue));

            // Graph the non-structural elements
            if (nameValue != null)
            {
                s.WriteStartElement("name");
                CVFormatter cdFormatter = new CVFormatter();
                cdFormatter.Host = this.Host;
                cdFormatter.GenericArguments = this.GenericArguments;
                cdFormatter.Graph(s, nameValue, result);
                s.WriteEndElement(); // end name
            }
            if (valueValue != null)
            {
                s.WriteStartElement("value");
                CDFormatter cdFormatter = new CDFormatter();
                cdFormatter.Host = this.Host;
                cdFormatter.GenericArguments = this.GenericArguments;
                cdFormatter.Graph(s, valueValue, result);
                s.WriteEndElement(); // end value
            }

        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse the object from</param>
        /// <returns>The constructed object</returns>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {

            Type crGenericType = typeof(CR<>).MakeGenericType(GenericArguments);
            ConstructorInfo ci = crGenericType.GetConstructor(Type.EmptyTypes);

            if (ci == null)
                throw new InvalidOperationException("Type being parsed must have parameterless constructor");
            object instance = ci.Invoke(null);
            
            // NullFlavor ? 
            if (s.GetAttribute("nullFlavor") != null)
                ((ANY)instance).NullFlavor = (NullFlavor)Util.FromWireFormat(s.GetAttribute("nullFlavor"), typeof(NullFlavor));
            else
            {
                
                // JF - Supported only in CA extensions to R1 data types
                if (s.GetAttribute("specializationType") != null && result.CompatibilityMode == DatatypeFormatterCompatibilityMode.Canadian)
                    ((ANY)instance).Flavor = s.GetAttribute("specializationType");
                // Inverted
                if (s.GetAttribute("inverted") != null)
                    crGenericType.GetProperty("Inverted").SetValue(instance, Util.FromWireFormat(s.GetAttribute("inverted"), typeof(bool)), null);

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
                            if (s.LocalName == "name") // Format using ED
                            {
                                CVFormatter cvFormatter = new CVFormatter();
                                cvFormatter.Host = this.Host;
                                cvFormatter.GenericArguments = this.GenericArguments;
                                crGenericType.GetProperty("Name").SetValue(instance, Util.FromWireFormat(cvFormatter.Parse(s, result), crGenericType.GetProperty("Name").PropertyType), null);
                            }
                            else if (s.LocalName == "value")
                            {
                                CDFormatter cdFormatter = new CDFormatter();
                                cdFormatter.Host = this.Host;
                                cdFormatter.GenericArguments = this.GenericArguments;
                                crGenericType.GetProperty("Value").SetValue(instance, Util.FromWireFormat(cdFormatter.Parse(s, result), crGenericType.GetProperty("Value").PropertyType), null);
                            }
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
            return instance;
        }

        /// <summary>
        /// Gets the type that this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "CR"; }
        }

        /// <summary>
        /// Get or set the host of this formatter
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets the generic type arguments of this formatter
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(CR<>).GetProperty("Inverted"));
            retVal.Add(typeof(CR<>).GetProperty("Name"));
            retVal.Add(typeof(CR<>).GetProperty("Value"));
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}