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
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// CO Formatter
    /// </summary>
    internal class COFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {

            PDVFormatter qtyf = new PDVFormatter();
            qtyf.Host = this.Host;
            qtyf.Graph(s, o, result);

            CO instance = o as CO;

            // Code property
            if (instance.NullFlavor != null)
                return;
            if (instance.Code != null)
            {
                s.WriteStartElement("code", "urn:hl7-org:v3");
                var hostResult = this.Host.Graph(s, instance.Code);
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }
           
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            PDVFormatter baseFormatter = new PDVFormatter();
            baseFormatter.Host = this.Host;
            var retVal = baseFormatter.ParseAttributes<CO>(s, result);

            // Process the parts
            #region Elements
            if (!s.IsEmptyElement)
            {

                int sDepth = s.Depth;
                string sName = s.Name;
                s.Read();

                QTYFormatter qtyf = new QTYFormatter();

                // string Name
                while (!(s.NodeType == System.Xml.XmlNodeType.EndElement && s.Depth == sDepth && s.Name == sName))
                {
                    string oldName = s.Name; // Name
                    try
                    {
                        // Numerator
                        if (s.LocalName == "code" && retVal.Code == null)
                        {
                            var parseResult = Host.Parse(s, typeof(CD<String>));
                            result.Code = parseResult.Code;
                            result.AddResultDetail(parseResult.Details);
                            retVal.Code = parseResult.Structure as CD<String>;
                        }
                        else if (s.LocalName == "code")
                            result.AddResultDetail(new NotImplementedResultDetail(ResultDetailType.Warning, "Code can only be assigned once", s.ToString()));
                        else
                            qtyf.ParseElementsInline(s, retVal, result);
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
            new ANYFormatter().Validate(retVal, s.ToString(), result);
            return retVal;
        }

        /// <summary>
        /// Gets the type that this formatter helper graphs
        /// </summary>
        public string HandlesType
        {
            get { return "CO"; }
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
        /// Gets or sets the generic arguments to this formatter
        /// </summary>
        public Type[] GenericArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the list of supported properties
        /// </summary>
        /// <returns></returns>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            var retVal = new QTYFormatter().GetSupportedProperties();
            retVal.AddRange(new PropertyInfo[] {
                typeof(CO).GetProperty("Code"),
                typeof(CO).GetProperty("Value"),
            });
            return retVal;
        }


        #endregion
    }
}
