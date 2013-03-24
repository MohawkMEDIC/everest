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
 * Date: 9/17/2009 9:09:43 AM
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using System.Reflection;
using MARC.Everest.Xml;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Datatype formatter for the MO data type
    /// </summary>
    public class MOFormatter : PDVFormatter, IDatatypeFormatter
    {

        #region IDatatypeFormatter Members

        
        /// <summary>
        /// Graph object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            // Want to control the output of value
            ANYFormatter baseFormatter = new ANYFormatter();

            baseFormatter.Graph(s, o, result);
            MO instance = o as MO;

            if (instance.NullFlavor != null) return; // Don't graph anymore

            if (instance.Expression != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Expression", "PQ", s.ToString()));
            if (instance.OriginalText != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "OriginalText", "PQ", s.ToString()));
            if (instance.Uncertainty != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Uncertainty", "PQ", s.ToString()));
            if (instance.UncertaintyType != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "UncertaintyType", "PQ", s.ToString()));
            if (instance.UncertainRange != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "UncertainRange", "PQ", s.ToString()));
            if (instance.Currency != null)
                s.WriteAttributeString("currency", instance.Currency);

            // Precision
            if (instance.Precision != null && instance.Precision != 0 && instance.Value.HasValue)
                s.WriteAttributeString("value", instance.Value.Value.ToString(String.Format("0.{0}", new String('0', instance.Precision))));
            else if (instance.Value.HasValue)
                s.WriteAttributeString("value", instance.Value.Value.ToString());

            
        }

        /// <summary>
        /// Parse an object from stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to read from</param>
        /// <returns>The parsed object</returns>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            MO retVal = base.Parse<MO>(s, result);

            if (s.GetAttribute("currency") != null)
                retVal.Currency = s.GetAttribute("currency");

            // Precision is not supported in R1, but is still useful to have so 
            // we will report the precision of the data that was on the wire
            string valStr = s.GetAttribute("value");
            if (valStr != null && valStr.Contains("."))
                retVal.Precision = valStr.Length - valStr.IndexOf(".") - 1;
            else
                retVal.Precision = 0;

     
            // Validate
            ANYFormatter fmtr = new ANYFormatter();
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            fmtr.Validate(retVal, pathName, result);

            return retVal;
        }

        /// <summary>
        /// Get the type this formatter handles
        /// </summary>
        public override string HandlesType
        {
            get { return "MO"; }
        }


        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = base.GetSupportedProperties();
            retVal.Add(typeof(MO).GetProperty("Currency"));
            return retVal;
        }
        #endregion
    }
}