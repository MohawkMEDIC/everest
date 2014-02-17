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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using MARC.Everest.Exceptions;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Datatypes R1 formatter for the CV data type
    /// </summary>
    public class CVFormatter : CSFormatter, IDatatypeFormatter
    {
       
        #region IDatatypeFormatter Members

      
        /// <summary>
        /// Graph object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The XmlWriter to graph to</param>
        /// <param name="o">The object to graph</param>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            // Get an instance ref
            ICodedValue instance_ics = (ICodedValue)o;

            // Do a base format
            base.Graph(s, o, result);

            // Format the coded simple
            if (instance_ics.CodeSystem != null) 
                s.WriteAttributeString("codeSystem", instance_ics.CodeSystem);
            if (instance_ics.CodeSystemName != null)
                s.WriteAttributeString("codeSystemName", instance_ics.CodeSystemName);
            if (instance_ics.CodeSystemVersion != null)
                s.WriteAttributeString("codeSystemVersion", instance_ics.CodeSystemVersion);
            if (instance_ics.DisplayName != null)
                s.WriteAttributeString("displayName", instance_ics.DisplayName);
            if (instance_ics.OriginalText != null) // Original Text
            {
                EDFormatter edFormatter = new EDFormatter();
                s.WriteStartElement("originalText", null);
                edFormatter.Graph(s, instance_ics.OriginalText, result);
                s.WriteEndElement();
            }
            if (!String.IsNullOrEmpty(instance_ics.ValueSet))
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "ValueSet", "CV", s.ToString()));
            if (!String.IsNullOrEmpty(instance_ics.ValueSetVersion))
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "ValueSetVersion", "CV", s.ToString()));


        }

        /// <summary>
        /// Parse object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The XmlReader to graph from</param>
        public virtual object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            IResultDetail[] details = null;
            CV<String> retVal = CDFormatter.Parse<CV<String>>(s, Host, result);
            return retVal;
        }

        /// <summary>
        /// Get the type that this formatter handles
        /// </summary>
        public override string HandlesType
        {
            get { return "CV"; }
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public override List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(){
                typeof(CV<>).GetProperty("CodeSystem"),
                typeof(CV<>).GetProperty("CodeSystemName"),
                typeof(CV<>).GetProperty("CodeSystemVersion"),
                typeof(CV<>).GetProperty("DisplayName"),
                typeof(CV<>).GetProperty("OriginalText")
            };
            retVal.AddRange(new CSFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}