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
 * Date: 02-12-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using MARC.Everest.Exceptions;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Formatter for the TEL data type
    /// </summary>
    internal class TELFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            ANYFormatter baseFormatter = new ANYFormatter();

            // Graph 
            baseFormatter.Graph(s, o, result);

            // Null ?
            IAny anyInstance = o as IAny;
            TEL tel = o as TEL;

            // Null flavor
            if (anyInstance.NullFlavor != null)
                return;

            // Format attributes
            if (tel.Value != null) // Value
                s.WriteAttributeString("value", tel.Value);
            if (tel.Use != null) // Use
                s.WriteAttributeString("use", Util.ToWireFormat(tel.Use));
            if (tel.Capabilities != null) // Capabilities
                s.WriteAttributeString("capabilities", Util.ToWireFormat(tel.Capabilities));
            
            // format elements
            if (tel.UseablePeriod != null)
            {
                s.WriteStartElement("useablePeriod", "urn:hl7-org:v3");
                var hostResult = this.Host.Graph(s, tel.UseablePeriod);
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }

        }

        /// <summary>
        /// Parse <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Parse base (ANY) from the stream
            ANYFormatter baseFormatter = new ANYFormatter();

            // Parse TEL
            TEL retVal = baseFormatter.Parse<TEL>(s);

            // Get the value
            if (s.GetAttribute("value") != null)
                retVal.Value = s.GetAttribute("value");
            if (s.GetAttribute("use") != null)
                retVal.Use = Util.Convert<SET<CS<TelecommunicationAddressUse>>>(s.GetAttribute("use"));
            if (s.GetAttribute("capabilities") != null)
                retVal.Capabilities = Util.Convert<SET<CS<TelecommunicationCabability>>>(s.GetAttribute("capabilities"));

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
                        // Numerator
                        if (s.LocalName == "useablePeriod")
                        {
                            var parseResult = this.Host.Parse(s, typeof(GTS));
                            result.Code = parseResult.Code;
                            result.AddResultDetail(parseResult.Details);
                            retVal.UseablePeriod = Util.Convert<GTS>(parseResult.Structure);
                        }
                        else if (s.NodeType == System.Xml.XmlNodeType.Element)
                            result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null));

                    }
                    catch (MessageValidationException e)
                    {
                        result.AddResultDetail(new ResultDetail(MARC.Everest.Connectors.ResultDetailType.Error, e.Message, e));
                    }
                    finally
                    {
                        if (s.Name == oldName) s.Read();
                    }
                }
            }
            #endregion

            // Base formatter
            baseFormatter.Validate(retVal, s.ToString(), result);

            return retVal;
        }

        /// <summary>
        /// Gets the type that this formatter helper handles
        /// </summary>
        public string HandlesType
        {
            get { return "TEL"; }
        }

        /// <summary>
        /// Gets or sets the host
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
        /// Gets or sets the supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            var retVal = new ANYFormatter().GetSupportedProperties();
            retVal.AddRange(new PropertyInfo[] {
                typeof(TEL).GetProperty("Value"),
                typeof(TEL).GetProperty("Use"),
                typeof(TEL).GetProperty("Capabilities"),
                typeof(TEL).GetProperty("UseablePeriod")
            });
            return retVal;
        }

        #endregion
    }
}
