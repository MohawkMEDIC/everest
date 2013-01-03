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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Xml;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Formatter for the CS datatype
    /// </summary>
    public class CSFormatter : ANYFormatter, IDatatypeFormatter
    {
      

        #region IDatatypeFormatter Members

      
        /// <summary>
        /// Graph the object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            // Get an instance ref 
            ICodedSimple instance_ics = (ICodedSimple)o;

           base.Graph(s, o as ANY, result);

            // Format the coded simple
            if (instance_ics.CodeValue != null && ((ANY)o).NullFlavor == null)
                s.WriteAttributeString("code", Util.ToWireFormat(instance_ics.CodeValue));
            //if ((o is CS<String> || o.GetType().GetGenericTypeDefinition() == typeof(CS<>)))
            //{
            //    if (instance_ics.CodeSystem != null) // Warn if there is no way to represent this in R1
            //        result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "CodeSystem", "CS", s.ToString()));
            //    else if (instance_ics.CodeSystemName != null)
            //        result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "CodeSystemName", "CS", s.ToString()));
            //    else if (instance_ics.CodeSystemVersion != null)
            //        result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "CodeSystemVersion", "CS", s.ToString()));
            //}
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse from</param>
        /// <returns>The parsed object</returns>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            
            // Parse CS
            CS<String> retVal = base.Parse<CS<String>>(s, result);
            
            // Now parse our data out... Attributes
            if (retVal.NullFlavor == null && s.GetAttribute("code") != null)
                retVal.Code = s.GetAttribute("code");

            // Validate
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            base.Validate(retVal, pathName, result);


            return retVal;
        }

        /// <summary>
        /// Gets the types that this formatter handles
        /// </summary>
        public override string HandlesType
        {
            get { return "CS"; }
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public override List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(CS<>).GetProperty("Code"));
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}