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
 * User: computc
 * Date: 9/16/2009 11:08:53 AM
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using System.Reflection;
using MARC.Everest.Xml;
using System.Globalization;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Data type R1 formatter for the REAL data type
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "REAL")]
    public class REALFormatter : PDVFormatter, IDatatypeFormatter
    {

        #region IDatatypeFormatter Members

     

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
		// We don't use base.graph() here because we want to control the value property
            ANYFormatter baseFormatter = new ANYFormatter();

            baseFormatter.Graph(s, o, result);
            REAL instance = o as REAL;

            if (instance.NullFlavor != null) return; // Don't graph anymore

            // Precision
            if (instance.Value.HasValue && instance.Precision != 0)
                s.WriteAttributeString("value", instance.Value.Value.ToString(String.Format("0.{0}", new String('0', instance.Precision), DatatypeFormatter.FormatterCulture.NumberFormat.NumberDecimalSeparator), DatatypeFormatter.FormatterCulture));
            else if (instance.Value.HasValue)
                s.WriteAttributeString("value", instance.Value.Value.ToString(DatatypeFormatter.FormatterCulture));

            // Unsupported properties
            if (instance.Expression != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Expression", "REAL", s.ToString()));
            if (instance.OriginalText != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "OriginalText", "REAL", s.ToString()));
            if (instance.Uncertainty != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Uncertainty", "REAL", s.ToString()));
            if (instance.UncertaintyType != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "UncertaintyType", "REAL", s.ToString()));
            if (instance.UncertainRange != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "UncertainRange", "REAL", s.ToString()));

             
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse from</param>
        /// <returns>The parsed object</returns>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            REAL retVal = base.Parse<REAL>(s, result);

            // Precision is not supported in R1, but is still useful to have so 
            // we will report the precision of the data that was on the wire
            string valStr = s.GetAttribute("value");
            if (valStr != null && valStr.Contains(DatatypeFormatter.FormatterCulture.NumberFormat.NumberDecimalSeparator))
                retVal.Precision = valStr.Length - valStr.IndexOf(DatatypeFormatter.FormatterCulture.NumberFormat.NumberDecimalSeparator) - 1;
            else
                retVal.Precision = 0;

            
            // Validate
            base.Validate(retVal, s.ToString(), result);

            return retVal;
        }

        /// <summary>
        /// Get the data type that this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "REAL"; }
        }

        /// <summary>
        /// Get or set the host of this formatter
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// The generic type arguments to the formatter
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.AddRange(new PDVFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion


    }
}