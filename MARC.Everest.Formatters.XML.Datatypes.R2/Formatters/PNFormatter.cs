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
 * Date: 02-09-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Person name formatter
    /// </summary>
    public class PNFormatter : ENFormatter
    {

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Cast to any
            var any = o as IAny;
            // Output flavor id
            if (any.Flavor == null && any.NullFlavor == null)
                s.WriteAttributeString("flavorId", "EN.PN");

            // Do a base format
            base.Graph(s, o, result);
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public override object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Validate flavor
            if (String.IsNullOrEmpty(s.GetAttribute("flavorId")) ||
                s.GetAttribute("flavorId") != "EN.PN")
                result.AddResultDetail(new FixedValueMisMatchedResultDetail(s.GetAttribute("flavorId"), "EN.PN", s.ToString()));

            var retVal = base.Parse(s, result);
            (retVal as IAny).Flavor = "EN.PN";
            return retVal;
        }

        /// <summary>
        /// Gets the type that this formatter handles
        /// </summary>
        public override string HandlesType
        {
            get
            {
                return "PN";
            }
        }
    }
}
