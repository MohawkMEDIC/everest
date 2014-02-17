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
 * Date: 12-13-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using System.Reflection;
using MARC.Everest.Exceptions;
using System.Xml;
using MARC.Everest.Connectors;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Formatter helper for the ST data type
    /// </summary>
    public class STFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public virtual void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Base formatter
            ANYFormatter anyFormatter = new ANYFormatter();

            // Format the base
            anyFormatter.Graph(s, o, result);

            // Nullflavor?
            ST instance = o as ST;
            if (instance.NullFlavor != null)
                return;

            // format properties
            if (instance.Value != null)
                s.WriteAttributeString("value", instance.Value);
            if (instance.Language != null)
                s.WriteAttributeString("language", instance.Language);

            // format translation
            if(instance.Translation != null)
                foreach (var tx in instance.Translation)
                {
                    s.WriteStartElement("translation", null);
                    var hostResult = this.Host.Graph(s, tx);
                    result.Code = hostResult.Code;
                    result.AddResultDetail(hostResult.Details);
                    s.WriteEndElement();
                }

        }

        /// <summary>
        /// Parse only the attributes
        /// </summary>
        protected T ParseAttributes<T>(XmlReader s, DatatypeR2FormatterParseResult result) where T : ST, new()
        {
            // Base formatter
            ANYFormatter baseFormatter = new ANYFormatter();
            var retVal = baseFormatter.Parse<T>(s);

            // Attributes
            if (s.GetAttribute("value") != null)
                retVal.Value = s.GetAttribute("value");
            if (s.GetAttribute("language") != null)
                retVal.Language = s.GetAttribute("language");
            return retVal;
        }

        /// <summary>
        /// Parse an object instance from <paramref name="s"/>
        /// </summary>
        public virtual object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            var retVal = ParseAttributes<ST>(s, result);

            // Elements
            if (!s.IsEmptyElement)
            {
                // Exit markers
                int sDepth = s.Depth;
                string sName = s.Name;

                Encoding textEncoding = System.Text.Encoding.UTF8;
                s.Read();
                // Read until exit condition is fulfilled
                while (!(s.NodeType == System.Xml.XmlNodeType.EndElement && s.Depth == sDepth && s.Name == sName))
                {
                    string oldName = s.Name; // Name
                    try
                    {

                        ParseElementsInline(s, retVal, result);
                        
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

            // Validate
            new ANYFormatter().Validate(retVal, s.ToString(), result);
            return retVal;
        }

        /// <summary>
        /// Parse elements inline (meaning the element exit criteria has already been handled
        /// </summary>
        protected void ParseElementsInline(XmlReader s, ST retVal, DatatypeR2FormatterParseResult result)
        {
            if (s.LocalName == "translation") // Translation
            {
                var hostResult = Host.Parse(s, typeof(ED));
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                if (retVal.Translation == null)
                    retVal.Translation = new SET<ST>();
                retVal.Translation.Add((ST)hostResult.Structure);
            }
            else if (s.NodeType == System.Xml.XmlNodeType.Element)
                result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null));

        }

        /// <summary>
        /// Gets the type that this formatter handles
        /// </summary>
        public virtual string HandlesType
        {
            get { return "ST"; }
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
        /// Gets or sets the generic type arguments
        /// </summary>
        public Type[] GenericArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the supported properties for the ST class
        /// </summary>
        /// <returns></returns>
        public virtual List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(
                typeof(PDV<String>).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            );
            retVal.Add(typeof(ST).GetProperty("Language"));
            retVal.Add(typeof(ST).GetProperty("Translation"));
            return retVal;
        }

        #endregion
    }
}
