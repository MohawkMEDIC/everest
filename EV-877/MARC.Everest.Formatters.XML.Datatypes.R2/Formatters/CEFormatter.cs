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
 * User: Justin Fyfe
 * Date: 11-14-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.DataTypes;
using System.Reflection;
using MARC.Everest.Connectors;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Represents a formatter that can represent CE in DT R2
    /// </summary>
    public class CEFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph object <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {

            var any = o as IAny;
            // Output flavor id
            if (any.Flavor == null && (any.NullFlavor == null || ((NullFlavor)any.NullFlavor).IsChildConcept(NullFlavor.Other)))
                s.WriteAttributeString("flavorId", "CD.CE");

            // Do a base format
            CDFormatter realFormatter = new CDFormatter();
            realFormatter.Host = this.Host;
            realFormatter.Graph(s, o, result);
            
        }

        /// <summary>
        /// Parse a CE instance from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            CDFormatter realFormatter = new CDFormatter();

            if (String.IsNullOrEmpty(s.GetAttribute("flavorId")) ||
                s.GetAttribute("flavorId") != "CD.CE")
                result.AddResultDetail(new FixedValueMisMatchedResultDetail(s.GetAttribute("flavorId"), "CD.CE", s.ToString()));

            realFormatter.Host = this.Host;
            var retVal = realFormatter.Parse<CE<String>>(s, result);

            retVal.Flavor = null;

            return retVal;
        }

        /// <summary>
        /// Gets the type that this formatter helper can handle
        /// </summary>
        public string HandlesType
        {
            get { return "CE"; }
        }

        /// <summary>
        /// Gets or sets the host context of the formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets the generic type arguments
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Gets the properties supported by this formatter
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(
                new CVFormatter().GetSupportedProperties());
            retVal.Add(typeof(CE<>).GetProperty("Translation"));
            return retVal;
        }

        #endregion
    }
}
