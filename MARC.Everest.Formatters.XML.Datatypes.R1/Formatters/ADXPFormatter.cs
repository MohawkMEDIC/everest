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
using MARC.Everest.Connectors;
using MARC.Everest.Xml;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Responsible for formatting ADXP members "to the wire"
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ADXP")]
    public class ADXPFormatter : ANYFormatter, IDatatypeFormatter
    {
        /// <summary>
        /// Host context
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Get or set the generic arguments to this type (if applicable)
        /// </summary>
        public Type[] GenericArguments { get; set; }

        #region IDatatypeFormatter Members

        /// <summary>
        /// Grap the object to a stream
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {

            ADXP instance = o as ADXP;

            // Start with part type and code attributes
            base.Graph(s, o, result);

            if(instance == null || instance.NullFlavor != null)
                return;

            // Now format our data
            if (instance.Type != null && result.CompatibilityMode != DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture)
                s.WriteAttributeString("partType", Util.ToWireFormat(instance.Type));
            if (instance.Code != null)
            {
                if (result.CompatibilityMode == DatatypeFormatterCompatibilityMode.Canadian)
                    s.WriteAttributeString("code", instance.Code);
                else
                    result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Code", "ADXP", s.ToString()));
            }
            if (instance.Value != null)
                s.WriteValue(instance.Value);
            if (instance.CodeSystem != null) // Warn if there is no way to represent this in R1
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "CodeSystem", "ADXP", s.ToString()));
            if(instance.CodeSystemVersion != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "CodeSystemVersion", "ADXP", s.ToString()));


        }

        /// <summary>
        /// Parse an ADXP from stream <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            
            // Parse CS
            ADXP retVal = base.Parse<ADXP>(s, result);

            // Now parse our data out... 
            if (!s.IsEmptyElement)
            {
                if (s.GetAttribute("code") != null && result.CompatibilityMode == DatatypeFormatterCompatibilityMode.Canadian)
                    retVal.Code = s.GetAttribute("code");
                else if(s.GetAttribute("code") != null)
                    result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "code", "ADXP", s.ToString()));

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
            base.Validate(retVal, pathName, result);


            return retVal;
        }

        /// <summary>
        /// Determines which types this grapher can understand
        /// </summary>
        public string HandlesType
        {
            get { return "ADXP"; }
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>()
            {
                typeof(ADXP).GetProperty("Type"),
                typeof(ADXP).GetProperty("Code"),
                typeof(ADXP).GetProperty("Value")
            };
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}