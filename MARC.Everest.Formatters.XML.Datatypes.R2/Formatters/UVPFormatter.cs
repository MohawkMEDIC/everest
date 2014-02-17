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
 * Date: 02-14-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;
using MARC.Everest.DataTypes;
using MARC.Everest.Exceptions;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// UVP Formatter for R2
    /// </summary>
    internal class UVPFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Host = this.Host;
            baseFormatter.GenericArguments = this.GenericArguments;

            // Graph
            baseFormatter.Graph(s, o, result);

            // Instance
            IProbability instance = o as IProbability;

            if (instance.NullFlavor != null)
                return;

            // Graph UVP properties
            if (instance.Probability.HasValue)
                s.WriteAttributeString("probability", Util.ToWireFormat(instance.Probability.Value));

            // Value
            if (instance.Value != null)
            {
                s.WriteStartElement("value", "urn:hl7-org:v3");
                var hostResult = this.Host.Graph(s, instance.Value as IGraphable);
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
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

            // Graph
            var tRetVal = baseFormatter.Parse<ANY>(s);
            var uvpType = typeof(UVP<>);
            var genType = uvpType.MakeGenericType(this.GenericArguments);

            // Construct
            var constructor = genType.GetConstructor(Type.EmptyTypes);
            IProbability retVal = constructor.Invoke(null) as IProbability;

            // Copy base attributes
            DatatypeR2Formatter.CopyBaseAttributes(tRetVal, retVal as ANY);

            // Parse UVP properties
            if (s.GetAttribute("probability") != null)
            {
                string tProb = s.GetAttribute("probability");
                try
                {
                    retVal.Probability = Decimal.Parse(tProb);
                }
                catch (Exception e)
                {
                    result.Code = ResultCode.Error;
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Error, e.Message, s.ToString(), e));
                }
            }

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
                        if (s.LocalName == "value" && retVal.Value == null)
                        {
                            var hostResult = this.Host.Parse(s, GenericArguments[0]);
                            result.AddResultDetail(hostResult.Details);
                            retVal.Value = hostResult.Structure;
                        }
                        else if(s.LocalName == "value")
                            result.AddResultDetail(new NotImplementedResultDetail(ResultDetailType.Warning, "Value can only be set once on an instance of UVP", s.ToString()));
                        else if (s.NodeType == System.Xml.XmlNodeType.Element)
                            result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null));
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

            // validate
            baseFormatter.Validate(retVal as ANY, s.ToString(), result);
            return retVal;
        }

        /// <summary>
        /// Gets the type that this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "UVP"; }
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
        /// Gets a list of supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            var retVal = new ANYFormatter().GetSupportedProperties();
            retVal.AddRange(new PropertyInfo[] {
                typeof(UVP<>).GetProperty("Value"),
                typeof(UVP<>).GetProperty("Probability")
            });
            return retVal;
        }

        #endregion
    }
}
