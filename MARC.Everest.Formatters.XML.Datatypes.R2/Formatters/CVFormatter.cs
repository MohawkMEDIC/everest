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
 * Date: 11-14-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using System.Reflection;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// A data types formatter that can represent CV in data types R2
    /// </summary>
    internal class CVFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {

            var any = o as IAny;
            // Output flavor id
            if(any.Flavor == null && (any.NullFlavor == null || ((NullFlavor)any.NullFlavor).IsChildConcept(NullFlavor.Other)))
                s.WriteAttributeString("flavorId", "CD.CV");

            // Do a base format
            CDFormatter realFormatter = new CDFormatter();
            realFormatter.Host = this.Host;
            realFormatter.Graph(s, o, result);

        }

        /// <summary>
        /// Parse an instance of CV from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            CDFormatter realFormatter = new CDFormatter();

            // Validate flavor
            if (String.IsNullOrEmpty(s.GetAttribute("flavorId")) ||
                s.GetAttribute("flavorId") != "CD.CV")
                result.AddResultDetail(new FixedValueMisMatchedResultDetail(s.GetAttribute("flavorId"), "CD.CV", s.ToString()));

            realFormatter.Host = this.Host;
            var retVal = realFormatter.Parse<CV<String>>(s, result);

            retVal.Flavor = null;

            return retVal;
        }

        /// <summary>
        /// Gets the type that this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "CV"; }
        }

        /// <summary>
        /// Gets or sets the host of this formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets the generic type arguments for the formatter
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Gets a list of properties that this formatter handles
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(
                new ANYFormatter().GetSupportedProperties()
            );
            retVal.AddRange(
                new PropertyInfo[] {
                    typeof(CV<>).GetProperty("Code"),
                    typeof(CV<>).GetProperty("CodeSystem"),
                    typeof(CV<>).GetProperty("CodeSystemName"),
                    typeof(CV<>).GetProperty("CodeSystemVersion"),
                    typeof(CV<>).GetProperty("CodingRationale"),
                    typeof(CV<>).GetProperty("ValueSet"),
                    typeof(CV<>).GetProperty("ValueSetVersion"),
                    typeof(CV<>).GetProperty("OriginalText"),
                    typeof(CV<>).GetProperty("DisplayName")
                }
            );
            return retVal;
        }

        #endregion
    }
}
