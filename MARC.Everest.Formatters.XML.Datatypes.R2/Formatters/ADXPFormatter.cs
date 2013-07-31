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
 * Date: 11-11-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// A formatter helper that renders ADXP in data types R2
    /// </summary>
    internal class ADXPFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            ADXP instance = o as ADXP;

            // Start with the part type and code attributes
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, instance, result);

            // Format data
            if (instance.Type != null && instance.Type.HasValue)
                s.WriteAttributeString("type", Util.ToWireFormat(instance.Type));
            if (instance.Value != null)
                s.WriteAttributeString("value", instance.Value);
            if (instance.Code != null)
                s.WriteAttributeString("code", instance.Code);
            if (instance.CodeSystem != null)
                s.WriteAttributeString("codeSystem", instance.CodeSystem);
            if (instance.CodeSystemVersion != null)
                s.WriteAttributeString("codeSystemVersion", instance.CodeSystemVersion);
            
        }

        /// <summary>
        /// Parse <paramref name="s"/> to an instance of ADXP
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Create base
            ANYFormatter baseFormatter = new ANYFormatter();
            ADXP retVal = baseFormatter.Parse<ADXP>(s);

            // Read data
            if (s.GetAttribute("type") != null)
                retVal.Type = Util.Convert<AddressPartType>(s.GetAttribute("type"));
            if (s.GetAttribute("code") != null)
                retVal.Code = s.GetAttribute("code");
            if (s.GetAttribute("codeSystem") != null)
                retVal.CodeSystem = s.GetAttribute("codeSystem");
            if (s.GetAttribute("codeSystemVersion") != null)
                retVal.CodeSystemVersion = s.GetAttribute("codeSystemVersion");
            if (s.GetAttribute("language") != null)
                result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, "@language", s.NamespaceURI, s.ToString(), null));
            if (s.GetAttribute("value") != null)
                retVal.Value = s.GetAttribute("value");
            
            // Validate
            baseFormatter.Validate(retVal, s.ToString(), result);
            return retVal;
        }

        /// <summary>
        /// Handles type
        /// </summary>
        public string HandlesType
        {
            get { return "ADXP"; }
        }

        /// <summary>
        /// Gets or sets the host 
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets generic type arguments
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Gets a list of supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            retVal.AddRange(typeof(ADXP).GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance));
            return retVal;
        }

        #endregion
    }
}
