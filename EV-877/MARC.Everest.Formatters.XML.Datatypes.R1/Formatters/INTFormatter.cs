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
 * Date: 9/16/2009 11:05:54 AM
 **/
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Data Type R1 formatter for the INT data type
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "INT")]
    public class INTFormatter : IDatatypeFormatter
    {

        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            PDVFormatter pdvFormatter = new PDVFormatter();
            var instance = o as INT;
            pdvFormatter.Graph(s, instance, result);

            // Unsupported properties
            if (instance.Expression != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Expression", "INT", s.ToString()));
            if (instance.OriginalText != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "OriginalText", "INT", s.ToString()));
            if (instance.Uncertainty != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Uncertainty", "INT", s.ToString()));
            if (instance.UncertaintyType != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "UncertaintyType", "INT", s.ToString()));
            if (instance.UncertainRange != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "UncertaintyRange", "INT", s.ToString()));

        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse from</param>
        /// <returns>The parsed object</returns>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            PDVFormatter pdvFormatter = new PDVFormatter();
            INT retVal = pdvFormatter.Parse<INT>(s, result);
            return retVal;
        }

        /// <summary>
        /// Get the data type that this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "INT"; }
        }

        /// <summary>
        /// Get or set the host of the formatter
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// The generic type arguments
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