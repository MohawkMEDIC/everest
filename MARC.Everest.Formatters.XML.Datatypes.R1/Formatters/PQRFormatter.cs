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
 * Date: 9/22/2009 10:52:05 AM
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
    /// Data Type R1 Formatter 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PQR")]
    public class PQRFormatter : IDatatypeFormatter
    {

        #region IDatatypeFormatter Members

       

        /// <summary>
        /// Graph object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            // Since PQR is a CV , we can use the CD formatter to graph the basic attributes onto the stream
            CVFormatter formatter = new CVFormatter();
            PQR instance = o as PQR;

            if (instance.NullFlavor != null) return; // Null, no need to format
            
            // Watch out for nargles
            if (instance.Precision != null && instance.Precision != 0 && instance.Value.HasValue)
                s.WriteAttributeString("value", instance.Value.Value.ToString(String.Format("0.{0}", new String('0', instance.Precision))));
            else if (instance.Value.HasValue)
                s.WriteAttributeString("value", instance.Value.Value.ToString());
            

            formatter.Graph(s, o as PQR, result);

           
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse from</param>
        /// <returns>The parsed object</returns>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {

            // Read value before we lose context
            string valStr = s.GetAttribute("value");

            // Create retval
            PQR retVal = CDFormatter.Parse<PQR>(s, Host, result);

            // Precision is not supported in R1, but is still useful to have so 
            // we will report the precision of the data that was on the wire
            if (valStr != null && valStr.Contains("."))
                retVal.Precision = valStr.Length - valStr.IndexOf(".") - 1;
            else
                retVal.Precision = 0;

            retVal.Value = (decimal?)Util.FromWireFormat(valStr, typeof(decimal?));

            // Validate
            ANYFormatter fmtr = new ANYFormatter();
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            fmtr.Validate(retVal, pathName, result);

            return retVal;
        }

        /// <summary>
        /// Gets the type of structure that this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "PQR"; }
        }

        /// <summary>
        /// Gets or sets the host of this formatter
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets the generic arguments supplied to this formatter
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(PQR).GetProperty("Value"));
            retVal.AddRange(new CVFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}