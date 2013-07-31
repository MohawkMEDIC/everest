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
using MARC.Everest.Interfaces;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Concept Role formatter for DT R1
    /// </summary>
    public class CRFormatter : ANYFormatter, IDatatypeFormatter
    {

        #region IDatatypeFormatter Members

     
        /// <summary>
        /// Graph object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {

            base.Graph(s, o, result); // Graph the base data


            // If there is no null flavor then graph the rest of the object
            if (((ANY)o).IsNull)
                return;

            object invertedValue = o.GetType().GetProperty("Inverted").GetValue(o, null),
                nameValue = o.GetType().GetProperty("Name").GetValue(o, null),
                valueValue = o.GetType().GetProperty("Value").GetValue(o, null);

            // Graph the structural attributes
            s.WriteAttributeString("inverted", Util.ToWireFormat(invertedValue));

            // Graph the non-structural elements
            if (nameValue != null)
            {
                s.WriteStartElement("name");
                var hostResult = this.Host.Graph(s, nameValue as IGraphable);
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement(); // end name
            }
            if (valueValue != null)
            {
                s.WriteStartElement("value");
                var hostResult = this.Host.Graph(s, valueValue as IGraphable);
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement(); // end value
            }

        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse the object from</param>
        /// <returns>The constructed object</returns>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {

            Type crGenericType = typeof(CR<>).MakeGenericType(GenericArguments);
            ConstructorInfo ci = crGenericType.GetConstructor(Type.EmptyTypes);

            if (ci == null)
                throw new InvalidOperationException("Type being parsed must have parameterless constructor");
            object instance = ci.Invoke(null);
            
            // NullFlavor ? 
            if (s.GetAttribute("nullFlavor") != null)
                ((ANY)instance).NullFlavor = (NullFlavor)Util.FromWireFormat(s.GetAttribute("nullFlavor"), typeof(NullFlavor));
                
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
                            else
                                result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null));
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

            base.Validate((ANY)instance, s.ToString(), result);

            return instance;
        }

        /// <summary>
        /// Gets the type that this formatter handles
        /// </summary>
        public override string HandlesType
        {
            get { return "CR"; }
        }

       
        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public override List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(){
                typeof(CR<>).GetProperty("Inverted"),
                typeof(CR<>).GetProperty("Name"),
                typeof(CR<>).GetProperty("Value")
            };
            retVal.AddRange(base.GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}