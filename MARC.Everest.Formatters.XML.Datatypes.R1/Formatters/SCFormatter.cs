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
 * User: Jaspinder Singh
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using System.Reflection;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes.Primitives;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// SC Formatter.
    /// </summary>
    public class SCFormatter : STFormatter

    {

        #region IDatatypeFormatter Members

        /// <summary>
        /// Graphs the object <paramref name="o"/> onto the stream
        /// <paramref name="s"/>.
        /// </summary>
        /// <param name="s">The XmlWriter stream to graph to.</param>
        /// <param name="o">The object to graph.</param>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            // Serialize CS
            SC cs = (SC)o;

            // Serialize ST 
            if (((ANY)o).NullFlavor != null)
                return;
            else if(cs.Code != null && !cs.Code.IsNull)
            {
                if (cs.Code.Code != null)
                    s.WriteAttributeString("code", Util.ToWireFormat(cs.Code));
                if (cs.Code.CodeSystem != null)
                    s.WriteAttributeString("codeSystem", Util.ToWireFormat(cs.Code.CodeSystem));
                if (cs.Code.CodeSystemName != null)
                    s.WriteAttributeString("codeSystemName", Util.ToWireFormat(cs.Code.CodeSystemName));
                if (cs.Code.CodeSystemVersion != null)
                    s.WriteAttributeString("codeSystemVersion", Util.ToWireFormat(cs.Code.CodeSystemVersion));
                if (cs.Code.DisplayName != null)
                    s.WriteAttributeString("displayName", Util.ToWireFormat(cs.Code.DisplayName));
                
                // Not supported properties
                if (cs.Code.ValueSet != null)
                    result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Code.ValueSet", "SC", s.ToString()));
                if (cs.Code.ValueSetVersion != null)
                    result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Code.ValueSetVersion", "SC", s.ToString()));
                if(cs.Code.CodingRationale != null)
                    result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Code.CodingRationale", "SC", s.ToString()));
                if (cs.Code.OriginalText != null)
                    result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Code.OriginalText", "SC", s.ToString()));
                if (cs.Code.Qualifier != null)
                    result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Code.Qualifier", "SC", s.ToString()));
                if (cs.Code.Translation != null)
                    result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Code.Translation", "SC", s.ToString()));
            }

            ST st = (ST)o;
            base.Graph(s, st, result);
            
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse from</param>
        /// <returns>The parsed object</returns>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // The SC to return.
            SC sc = base.Parse<SC>(s, result);

            if (sc.NullFlavor != null)
                return sc;

            if (s.GetAttribute("code") != null || s.GetAttribute("codeSystem") != null ||
                s.GetAttribute("codeSystemVersion") != null || s.GetAttribute("codeSystemName") != null ||
                s.GetAttribute("displayName") != null)
                sc.Code = new CD<string>();

            if (s.GetAttribute("code") != null)
                sc.Code.Code = Util.Convert<CodeValue<String>>(s.GetAttribute("code"));
            if (s.GetAttribute("codeSystem") != null)
                sc.Code.CodeSystem = s.GetAttribute("codeSystem");
            if (s.GetAttribute("codeSystemVersion") != null)
                sc.Code.CodeSystemVersion = s.GetAttribute("codeSystemVersion");
            if (s.GetAttribute("codeSystemName") != null)
                sc.Code.CodeSystemName = s.GetAttribute("codeSystemName");
            if (s.GetAttribute("displayName") != null)
                sc.Code.DisplayName = s.GetAttribute("displayName");

            // Read the ST parts
            ST st = (ST)base.Parse(s, result);
            sc.Language = st.Language;
            sc.Value = st.Value;

            return sc;
           
        }

        /// <summary>
        /// Returns the type that this formater handles.
        /// </summary>
        public override string HandlesType
        {
            get { return "SC"; }
        }

      
        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = base.GetSupportedProperties();
            retVal.Add(typeof(SC).GetProperty("Code"));
            return retVal;
        }
        #endregion
    }
}
