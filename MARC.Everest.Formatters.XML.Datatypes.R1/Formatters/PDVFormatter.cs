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
 * Date: 9/16/2009 10:39:03 AM
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using System.Xml;
using System.Reflection;
using MARC.Everest.DataTypes.Interfaces;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Primitive Data Value Container formatter. This formatter is used by a multitude of other formatters
    /// to parse and graph PDV data
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PDV")]
    public class PDVFormatter
    {
       
        /// <summary>
        /// Graph a PDV object onto the stream
        /// </summary>
        /// <param name="xw">The writer to graph to</param>
        /// <param name="o">The object to parse</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "xw"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public void Graph(XmlWriter xw, object o, DatatypeFormatterGraphResult result)
        {
            // Graph this object to the stream
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(xw, o, result); // Graph the any part of this

            if (o.GetType().GetProperty("NullFlavor").GetValue(o, null) != null)
                return; // No further graphing

            // Now comes the fun part .. we'll need to graph the Value property onto the stream, 
            object valueValue = o.GetType().GetProperty("Value").GetValue(o, null);

            if (valueValue != null)
                xw.WriteAttributeString("value", Util.ToWireFormat(valueValue));
        }
      
        /// <summary>
        /// Parse an instance of T from the XmlReader
        /// </summary>
        /// <typeparam name="T">The type of object to read</typeparam>
        /// <param name="xr">The Xml Reader to read from</param>
        /// <returns>The parsed instance</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "xr")]
        public T Parse<T>(XmlReader xr, DatatypeFormatterParseResult result) where T : ANY, new()
        {
            ANYFormatter baseFormatter = new ANYFormatter(); // Base formatter
            T retVal = baseFormatter.Parse<T>(xr, result);

            // If it is null return the null flavor
            if (retVal.NullFlavor != null) return retVal;

            PropertyInfo pi = typeof(T).GetProperty("Value");

            try
            {
                // Value
                if (xr.GetAttribute("value") != null)
                    pi.SetValue(retVal, Util.FromWireFormat(xr.GetAttribute("value"), pi.PropertyType), null);
            }
            catch (Exception e)
            {
                result.AddResultDetail(new ResultDetail(ResultDetailType.Error, e.Message, xr.ToString(), e));
            }
            return retVal;
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(PDV<>).GetProperty("Value"));
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            return retVal;
        }
    }
}