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
 * Date: 02-06-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Interfaces;
using MARC.Everest.DataTypes;
using MARC.Everest.Exceptions;
using MARC.Everest.Connectors;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Formatter helper for the COLL data type
    /// </summary>
    public class COLLFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/> storing 
        /// details in <paramref name="result"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Cast to strongly typed data
            IColl collInstance = o as IColl;
            IAny anyInstance = o as IAny;

            // Base formatter
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Host = this.Host;
            baseFormatter.Graph(s, o, result);

            // Graphing a COLL is easy, we just iterate over the items 
            if(anyInstance.NullFlavor == null)
                foreach (var itm in collInstance)
                {

                    // Start the item tag
                    s.WriteStartElement("item", null);

                    // Output an XSI type if the type does not match the generic parameter
                    if (itm.GetType() != GenericArguments[0])
                    {
                        var xsiName = DatatypeR2Formatter.CreateXSITypeName(itm.GetType());
                        s.WriteAttributeString("xsi", "type", DatatypeR2Formatter.NS_XSI, xsiName);
                    }

                    // Format the item
                    var hostResult = this.Host.Graph(s, itm as IGraphable);
                    result.Code = hostResult.Code;
                    result.AddResultDetail(hostResult.Details);

                    // End of item tag
                    s.WriteEndElement();
                }

        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Parse the base data
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Host = this.Host;
            LIST<IGraphable> retVal = baseFormatter.Parse<LIST<IGraphable>>(s); 

            // parse items
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
                        if (s.LocalName == "item")
                        {
                            var parseResult = Host.Parse(s, GenericArguments[0]);
                            result.Code = parseResult.Code;
                            result.AddResultDetail(parseResult.Details);
                            retVal.Add(parseResult.Structure);
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
        /// Gets the type that this type handles
        /// </summary>
        public virtual string HandlesType
        {
            get { return "COLL"; }
        }

        /// <summary>
        /// Gets or sets the host of this formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the generic type arguments
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
            retVal.Add(typeof(COLL<>).GetProperty("Item"));
            return retVal;
        }

        #endregion
    }
}
