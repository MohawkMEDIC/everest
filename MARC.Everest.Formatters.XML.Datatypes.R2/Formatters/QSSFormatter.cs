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
using System.Reflection;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Exceptions;
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;
using System.Collections;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Represents a formatter that can represent QSS in data types r2
    /// </summary>
    internal class QSSFormatter : IDatatypeFormatter
    {
        
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {

            // Graph the base
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, o, result);

            // Do not graph if null flavor is present
            if ((o as ANY).NullFlavor != null)
                return;

            var originalText = o as IOriginalText;

            // Write out the original text first
            if (originalText.OriginalText != null)
            {
                s.WriteStartElement("originalText", "urn:hl7-org:v3");
                var hostResult = this.Host.Graph(s, originalText.OriginalText);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }

            // Now the terms
            foreach (var term in (o as IEnumerable))
            {
                s.WriteStartElement("term", "urn:hl7-org:v3");

                var hostResult = this.Host.Graph(s, term as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }
        }

        /// <summary>
        /// Parse from wire level format
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {

            // Any formatter for the base attributes
            ANYFormatter baseFormatter = new ANYFormatter();
            // Create the types
            Type qsiType = typeof(QSS<>);
            Type qsiGenericType = qsiType.MakeGenericType(GenericArguments);

            // Create an instance of rto from the rtoType
            object instance = qsiGenericType.GetConstructor(Type.EmptyTypes).Invoke(null);
            DatatypeR2Formatter.CopyBaseAttributes(baseFormatter.Parse(s, result) as ANY, instance as ANY);

            // Read internal elements
            if ((instance as ANY).NullFlavor != null)
                return instance;

            // Now process the elements
            #region Elements
            if (!s.IsEmptyElement)
            {

                int sDepth = s.Depth;
                string sName = s.Name;

                List<IGraphable> termList = new List<IGraphable>(4);

                s.Read();
                // string Name
                while (!(s.NodeType == System.Xml.XmlNodeType.EndElement && s.Depth == sDepth && s.Name == sName))
                {
                    string oldName = s.Name; // Name
                    try
                    {
                        if (s.NodeType == System.Xml.XmlNodeType.Element)
                        {
                            switch (s.LocalName)
                            {
                                case "originalText":
                                    {
                                        var hostResult = this.Host.Parse(s, typeof(ED));
                                        result.Code = hostResult.Code;
                                        result.AddResultDetail(hostResult.Details);
                                        (instance as IOriginalText).OriginalText = hostResult.Structure as ED;
                                        break;
                                    }
                                case "term":
                                    {
                                        var hostResult = this.Host.Parse(s, GenericArguments[0]);
                                        result.Code = hostResult.Code;
                                        result.AddResultDetail(hostResult.Details);
                                        termList.Add(hostResult.Structure as IGraphable);
                                        break;
                                    }
                                default:
                                    result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null));
                                    break;
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

                // Set term list

                ((IListContainer)instance).ContainedList = termList;
            }
            #endregion

            // Validate
            baseFormatter.Validate(instance as ANY, s.ToString(), result);

            return instance;
        }

        /// <summary>
        /// Gets the type of handler
        /// </summary>
        public string HandlesType
        {
            get { return "QSS"; }
        }

        /// <summary>
        /// Gets or sets the host
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets the generic arguments for the formatter helper
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get the supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            retVal.Add(typeof(QSET<>).GetProperty("OriginalText"));
            retVal.Add(typeof(QSS<>).GetProperty("Terms"));
            return retVal;
        }

        #endregion
    }
}
