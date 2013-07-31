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
 * Date: 02-09-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Formatter helper for the EN data type
    /// </summary>
    public class ENFormatter :IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graphs <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public virtual void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Graph the EN
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Host = this.Host;
            baseFormatter.GenericArguments = this.GenericArguments;
            baseFormatter.Graph(s, o, result);

            // EN instance
            var enInstance = o as EN;
 
            // Format the properties if not nul
            if (enInstance.NullFlavor != null)
                return;

            // Format the use
            if (enInstance.Use != null)
                s.WriteAttributeString("use", Util.ToWireFormat(enInstance.Use));

            // Parts
            foreach (var enxp in enInstance.Part)
            {
                s.WriteStartElement("part", "urn:hl7-org:v3");

                // Output the type
                var hostResult = this.Host.Graph(s, enxp);
                result.AddResultDetail(hostResult.Details);

                s.WriteEndElement(); // end part
            }
        }

        /// <summary>
        /// Parse an object instance from <paramref name="s"/>
        /// </summary>
        public virtual object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Parse the base attributes
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Host = this.Host;
            baseFormatter.GenericArguments = this.GenericArguments;
            EN retVal = baseFormatter.Parse<EN>(s);

            // Use
            if (s.GetAttribute("use") != null)
                retVal.Use = Util.Convert<SET<CS<EntityNameUse>>>(s.GetAttribute("use"));

            // Process the parts
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
                        if (s.LocalName == "part")
                        {
                            var parseResult = Host.Parse(s, typeof(ENXP));
                            result.Code = parseResult.Code;
                            result.AddResultDetail(parseResult.Details);
                            retVal.Part.Add(parseResult.Structure as ENXP);
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

            // Validate
            baseFormatter.Validate(retVal, s.ToString(), result);
            return retVal;
        }

        /// <summary>
        /// Gets the type that this handles
        /// </summary>
        public virtual string HandlesType
        {
            get { return "EN"; }
        }

        /// <summary>
        /// Gets or sets the host formatter
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
        /// Gets a list of supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            var retVal = new ANYFormatter().GetSupportedProperties();
            retVal.AddRange(new PropertyInfo[] {
                typeof(EN).GetProperty("Use"),
                typeof(EN).GetProperty("Part")
            });
            return retVal;
        }

        #endregion
    }
}
