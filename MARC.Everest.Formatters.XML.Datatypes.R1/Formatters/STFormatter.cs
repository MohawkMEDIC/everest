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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes;
using System.Reflection;
using MARC.Everest.Connectors;
using MARC.Everest.Xml;
using MARC.Everest.Exceptions;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// string Formatter
    /// </summary>
    public class STFormatter : ANYFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph the object
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            // Represent the ST as an ED and serialize the ED
            base.Graph(s, o, result);
            
            ST instance = o as ST;

            // Get rid of these attributes
            if (result.CompatibilityMode != DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture)
            {
                // In R1 data types an ST is a restriction of an ED with these attributes fixed
                s.WriteAttributeString("mediaType", "text/plain");
                s.WriteAttributeString("representation", "TXT");
            }

            // Language
            if (instance.Language != null)
                s.WriteAttributeString("language", instance.Language);
            
            // Content
            s.WriteString(instance.Value);

            // Translation
            if (instance.Translation != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Translation", "ST", s.ToString()));
            
        }

        /// <summary>
        /// Parse the object
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Parse base (ANY) from the stream

            // Parse ED
            ST retVal = base.Parse<ST>(s, result);

            // Now parse our data out... Attributes
            if (s.GetAttribute("mediaType") != null && s.GetAttribute("mediaType") != "text/plain")
                result.AddResultDetail(new FixedValueMisMatchedResultDetail(s.GetAttribute("mediaType"), "text/plain", String.Format("{0}/@mediaType", pathName)));
            if (s.GetAttribute("language") != null)
                retVal.Language = s.GetAttribute("language");

            // Elements and inner data
            #region Elements
            string innerData = "";
            if (!s.IsEmptyElement)
            {
                // Exit markers
                int sDepth = s.Depth;
                string sName = s.Name;

                s.Read();
                // Read until exit condition is fulfilled
                while (!(s.NodeType == System.Xml.XmlNodeType.EndElement && s.Depth == sDepth && s.Name == sName))
                {
                    string oldName = s.Name; // Name
                    try
                    {

                        if (s.NodeType == System.Xml.XmlNodeType.Text ||
                            s.NodeType == System.Xml.XmlNodeType.CDATA)
                            innerData += s.Value;
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
            else
                innerData = s.Value;
            #endregion

            retVal.Value = innerData;

            // Validate
            base.Validate(retVal, s.ToString(), result);

            return retVal;
        }

        /// <summary>
        /// What types does this formatter handle
        /// </summary>
        public string HandlesType
        {
            get { return "ST"; }
        }

        /// <summary>
        /// Get or set the host
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Generic arguments
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(2);
            retVal.Add(typeof(ST).GetProperty("Value"));
            retVal.Add(typeof(ST).GetProperty("Language"));
            retVal.AddRange(base.GetSupportedProperties());
            return retVal;

        }
        #endregion
    }
}