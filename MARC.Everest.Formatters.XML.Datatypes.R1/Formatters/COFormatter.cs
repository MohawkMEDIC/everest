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
 * User: fyfej
 * Date: 9/10/2009 11:45:10 AM
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
    /// Formatter for the CO Datatype
    /// </summary>
    public class COFormatter : IDatatypeFormatter
    {

        #region IDatatypeFormatter Members

       
        /// <summary>
        /// Graph function
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            CO instance = (o as ANY).Clone() as CO; // We'll be modifying data 
            instance.Code = (instance.Code ?? new CD<String>()).Clone() as CD<String>;
            
            // CO is identical on the wire as a cv in dt r1
            CVFormatter formatter = new CVFormatter();
            formatter.Host = this.Host;

            // First, is the instance null?
            if (instance == null)
                return;
            else if (instance.IsNull)
            {
                if (instance.Code == null) instance.Code = new CD<string>();
                else if (instance.Code.IsNull)
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Warning, "NullFlavor from CO instance will overwrite NullFlavor from Code Property", s.ToString(), null));
                instance.Code.NullFlavor = instance.NullFlavor;
            }
            else if (instance.Flavor != null)
            {
                if (instance.Code == null) instance.Code = new CD<string>();
                else if (instance.Code.Flavor != null)
                    result.AddResultDetail(new ResultDetail(ResultDetailType.Warning, "Flavor from CO instance will overwrite Flavor from Code Property", s.ToString(), null));
                instance.Code.Flavor = instance.Flavor;
            }

            // Graph
            formatter.Graph(s, instance.Code, result);

            
            // Append details
            if(instance.Value != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Value", "CO", s.ToString()));
            else if(instance.Code != null && instance.Code.Qualifier != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Code.Qualifier", "CO", s.ToString()));
            else if (instance.Code != null && instance.Code.Translation != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Code.Translation", "CO", s.ToString()));


        }

        /// <summary>
        /// Parse function
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            var retVal = CDFormatter.Parse<CD<String>>(s, Host, result);
            CO instance = new CO();
            
            instance.Code = retVal;

            // Propogate
            if (instance.Code != null)
            {
                instance.Flavor = instance.Code.Flavor;
                instance.NullFlavor = instance.Code.NullFlavor;
                instance.Code.NullFlavor = null;
                instance.Code.Flavor = null;
            }

            ANYFormatter fmtr = new ANYFormatter();
            fmtr.Validate(instance, s.ToString(), result);
            return instance;
        }

        /// <summary>
        /// Gets the types this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "CO"; }
        }

        /// <summary>
        /// Gets or sets the host of this formatlet
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets the generic arguments to this type
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            retVal.Add(typeof(CO).GetProperty("Code"));
            return retVal;
        }
        #endregion
    }
}