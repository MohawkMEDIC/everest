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
 * Date: 13-10-2011
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// ITS1 formatter for the NPPD datatype
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LIST")]
    public class NPPDFormatter : ANYFormatter, IDatatypeFormatter
    {
        #region IDatatypeFormatter Members


        /// <summary>
        /// Graph object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream</param>
        /// <param name="o">The object</param>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            SETFormatter formatter = new SETFormatter();
            formatter.Host = this.Host;

            // Create new generic arguments
            var uvpType = typeof(UVP<>);
            var genType = uvpType.MakeGenericType(GenericArguments);

            formatter.GenericArguments = new Type[] { genType };

            formatter.Graph(s, o, result);
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            SETFormatter formatter = new SETFormatter();
            formatter.Host = this.Host;

            // Create new generic arguments
            var uvpType = typeof(UVP<>);
            var genType = uvpType.MakeGenericType(GenericArguments);

            formatter.GenericArguments = new Type[] { genType };
            object retval = formatter.Parse(s, result);
            base.Validate((ANY)retval, s.ToString(), result);
            return retval;
        }

        /// <summary>
        /// Get the name of the type this handles
        /// </summary>
        public override string HandlesType
        {
            get { return "NPPD"; }
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            return new SETFormatter().GetSupportedProperties();
        }
        #endregion
    }
}