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
 * User: computc
 * Date: 9/14/2009 10:43:44 AM
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using MARC.Everest.Xml;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Formatter for ENXP
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ENXP")]
    public class ENXPFormatter : IDatatypeFormatter
    {
        /// <summary>
        /// Host context
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

      
        #region IDatatypeFormatter Members

        /// <summary>
        /// Gets or sets the generic arguments to the formatter
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Grap the object to a stream
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {

            ENXP instance = o as ENXP;

            // Start with part type and code attributes
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, o, result);

            IResultDetail[] partTypeDetails = new IResultDetail[0];

            if (instance.NullFlavor != null)
                return;
            
            // Now format our data
            if (instance.Type != null && result.CompatibilityMode != DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture)
                s.WriteAttributeString("partType", Util.ToWireFormat(instance.Type));
            if (instance.Qualifier != null && !instance.Qualifier.IsEmpty)
                s.WriteAttributeString("qualifier", Util.ToWireFormat(instance.Qualifier));
            if (instance.Code != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Code", "ADXP", s.ToString()));
                //s.WriteAttributeString("code", instance.Code);
            if (instance.Value != null)
                s.WriteValue(instance.Value);
            if (instance.CodeSystem != null) // Warn if there is no way to represent this in R1
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "CodeSystem", "ENXP", s.ToString()));
            if(instance.CodeSystemVersion != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "CodeSystemVersion", "ENXP", s.ToString()));

        }

        /// <summary>
        /// Parse an ENXP from stream <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Parse base (ANY) from the stream
            ANYFormatter baseFormatter = new ANYFormatter();

            // Parse CS
            ENXP retVal = baseFormatter.Parse<ENXP>(s, result);

            // Part Type is ignored by this formatter but qualifier is not
            if (s.GetAttribute("qualifier") != null)
                retVal.Qualifier = Util.Convert<SET<CS<EntityNamePartQualifier>>>(s.GetAttribute("qualifier"));

            // Now parse our data out... 
            if (!s.IsEmptyElement)
            {
                // Read next for text elemnt
                string sName = s.Name;
                s.Read();
                while (!(s.NodeType == System.Xml.XmlNodeType.EndElement && sName == s.Name))
                {
                    if (s.NodeType == System.Xml.XmlNodeType.Text)
                        retVal.Value = s.Value;
                    s.Read();
                }

            }

            // Validate
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            baseFormatter.Validate(retVal, pathName, result);


            return retVal;
        }

        /// <summary>
        /// Determines which types this grapher can understand
        /// </summary>
        public string HandlesType
        {
            get { return "ENXP"; }
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(ENXP).GetProperty("Type"));
            retVal.Add(typeof(ENXP).GetProperty("Code"));
            retVal.Add(typeof(ENXP).GetProperty("Value"));
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}