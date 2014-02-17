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
 * Date: 02-13-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Exceptions;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// String with code formatter
    /// </summary>
    public class SCFormatter : STFormatter, IDatatypeFormatter
    {

        /// <summary>
        /// Graphs <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Output base properties
            base.Graph(s, o, result);

            // Output code
            SC instance = o as SC;
            if (instance.NullFlavor != null)
                return;

            // Code 
            if (instance.Code != null)
            {
                s.WriteStartElement("code", null);

                // Code
                var hostResult = this.Host.Graph(s, instance.Code);
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }
        }

        /// <summary>
        /// Parse an object instance from <paramref name="s"/>
        /// </summary>
        public override object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Parse the base (SC)
            var retVal = base.ParseAttributes<SC>(s, result);
            
            // Parse the code element
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

                        if (s.LocalName == "code") // Translation
                        {
                            var hostResult = Host.Parse(s, typeof(CD<String>));
                            result.Code = hostResult.Code;
                            result.AddResultDetail(hostResult.Details);
                            retVal.Code = result.Structure as CD<String>;
                        }
                        else
                            base.ParseElementsInline(s, retVal, result);
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
        /// Gets the type that this formatter handles
        /// </summary>
        public override string HandlesType
        {
            get
            {
                return "SC";
            }
        }

        /// <summary>
        /// Get a list of supported properties for the SC type
        /// </summary>
        public override List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            var retVal = base.GetSupportedProperties();
            retVal.Add(typeof(SC).GetProperty("Code"));
            return retVal;
        }
    }
}
