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
 * Date: 02-06-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Interfaces;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Datatype formatter for SLIST
    /// </summary>
    internal class SLISTFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graphs <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Host = this.Host;
            baseFormatter.GenericArguments = this.GenericArguments;
            baseFormatter.Graph(s, o, result);

            // Graph elements
            ISampledList instance = o as ISampledList;
            if (instance.NullFlavor != null)
                return;

            // Origin
            if (instance.Origin != null)
            {
                s.WriteStartElement("origin", "urn:hl7-org:v3");
                var hostResult = this.Host.Graph(s, instance.Origin as IGraphable);
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }
            // Scaling
            if (instance.Scale != null)
            {
                s.WriteStartElement("scale", "urn:hl7-org:v3");

                // Output xsi type
                s.WriteAttributeString("xsi", "type", DatatypeR2Formatter.NS_XSI, DatatypeR2Formatter.CreateXSITypeName(instance.Scale.GetType()));

                var hostResult = this.Host.Graph(s, instance.Scale);
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }
            // Digits
            if (instance.Items != null)
            {
                foreach (var itm in instance.Items)
                {
                    s.WriteStartElement("digit", "urn:hl7-org:v3");
                    var hostResult = this.Host.Graph(s, itm);
                    result.AddResultDetail(hostResult.Details);
                    s.WriteEndElement();
                }
            }

        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Host = this.Host;
            baseFormatter.GenericArguments = this.GenericArguments;
            ANY tRetVal = baseFormatter.Parse<ANY>(s);

            // Construct an instance of the SLIST
            var slistType = typeof(SLIST<>).MakeGenericType(this.GenericArguments);
            var constructor = slistType.GetConstructor(Type.EmptyTypes);
            ISampledList retVal = constructor.Invoke(null) as ISampledList;

            DatatypeR2Formatter.CopyBaseAttributes(tRetVal, retVal as ANY);

            // Parse the return value
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
                        // Origin
                        if (s.LocalName == "origin" && retVal.Origin == null)
                        {
                            var parseResult = Host.Parse(s, GenericArguments[0]);
                            result.Code = parseResult.Code;
                            result.AddResultDetail(parseResult.Details);
                            retVal.Origin = parseResult.Structure as IQuantity;
                        }
                        else if (s.LocalName == "origin")
                            result.AddResultDetail(new NotImplementedResultDetail(ResultDetailType.Warning, "origin may only be supplied once", s.ToString()));
                        // Scale
                        else if (s.LocalName == "scale" && retVal.Scale == null)
                        {
                            var parseResult = Host.Parse(s, typeof(IQuantity));
                            result.Code = parseResult.Code;
                            result.AddResultDetail(parseResult.Details);
                            retVal.Scale = parseResult.Structure as IQuantity;
                        }
                        else if (s.LocalName == "scale")
                            result.AddResultDetail(new NotImplementedResultDetail(ResultDetailType.Warning, "scale may only be supplied once", s.ToString()));
                        // Digit
                        else if (s.LocalName == "digit")
                        {
                            var parseResult = Host.Parse(s, typeof(INT));
                            result.Code = parseResult.Code;
                            result.AddResultDetail(parseResult.Details);
                            retVal.Items.Add(parseResult.Structure as INT);
                        }
                        else if (s.NodeType == System.Xml.XmlNodeType.Element)
                            result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null));

                    }
                    catch (MessageValidationException e)
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

            // Validate
            baseFormatter.Validate(retVal as ANY, s.ToString(), result);
            return retVal;
        }

        /// <summary>
        /// Gets the type this handles
        /// </summary>
        public string HandlesType
        {
            get { return "SLIST"; }
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
        /// Generic arguments
        /// </summary>
        public Type[] GenericArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Get supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            var retVal = new ANYFormatter().GetSupportedProperties();
            retVal.AddRange(new PropertyInfo[] {
                typeof(SLIST<>).GetProperty("Items"),
                typeof(SLIST<>).GetProperty("Origin"),
                typeof(SLIST<>).GetProperty("Scale")
            });
            return retVal;
        }

        #endregion
    }
}
