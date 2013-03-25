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
 * Date: 9/16/2009 11:12:26 AM
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
    /// UVP Formatter
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "UVP")]
    public class UVPFormatter : ANYFormatter, IDatatypeFormatter
    {

        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            // Graph this UVP to the stream
            base.Graph(s, o, result);
            
            // Add probability 
            if ((o as ANY).NullFlavor != null)
                return; // No need for this

            object probValue = o.GetType().GetProperty("Probability").GetValue(o, null),
                valueValue = o.GetType().GetProperty("Value").GetValue(o, null);

            // Output XSI:TYPE
            s.WriteAttributeString("xsi","type", DatatypeFormatter.NS_XSI, Util.CreateXSITypeName(o.GetType()));

            if (probValue != null)
                s.WriteAttributeString("probability", Util.ToWireFormat(probValue));
            
            // Graph the value
            ANY anyValue = valueValue as ANY;
            if (anyValue == null)
                return;

            var hostResult = this.Host.Graph(s, anyValue);
            result.Code = hostResult.Code;
            result.AddResultDetail(hostResult.Details);

        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The reader to parse from</param>
        /// <returns>The parsed object</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            
            // parse PDV portion
            Type uvpType = typeof(UVP<>).MakeGenericType(GenericArguments);
            ConstructorInfo ci = uvpType.GetConstructor(Type.EmptyTypes);
            ANY retVal = ci.Invoke(null) as ANY;

            // Property information
            PropertyInfo probabilityProperty = uvpType.GetProperty("Probability"),
                valueProperty = uvpType.GetProperty("Value");

            // Clean the 
            if (s.GetAttribute("type", DatatypeFormatter.NS_XSI) != null && s is XmlStateReader)
            {
                (s as XmlStateReader).AddFakeAttribute("type", Util.CreateXSITypeName(GenericArguments[0]));
            }

            // Probability
            if (s.GetAttribute("probability") != null) 
            {
                decimal prob = (decimal)0.0f;
                if (!Decimal.TryParse(s.GetAttribute("probability"), out prob)) // Try to parse
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Warning, string.Format("Value '{0}' can't be processed into 'Probability' on data type UVP", s.GetAttribute("probability")), s.ToString(), null));
                else // Success, so assign
                    probabilityProperty.SetValue(retVal, prob, null);
            }

            // Set value
            var hostResult = Host.Parse(s, GenericArguments[0]);
            result.Code = hostResult.Code;
            result.AddResultDetail(hostResult.Details);
            valueProperty.SetValue(retVal, hostResult.Structure, null);

            // Move null flavors and flavors up
            ANY resultAny = hostResult.Structure as ANY;
            retVal.NullFlavor = resultAny.NullFlavor;
            resultAny.NullFlavor = null;
            retVal.Flavor = resultAny.Flavor;
            resultAny.Flavor = null;

            // Validate the data type
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            base.Validate(retVal, pathName, result);
            return retVal;

        }

        /// <summary>
        /// Get the type that this formatter handles
        /// </summary>
        public override string HandlesType
        {
            get { return "UVP"; }
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = base.GetSupportedProperties();
            retVal.Add(typeof(UVP<>).GetProperty("Probability"));
            return retVal;
        }
        #endregion
    }
}