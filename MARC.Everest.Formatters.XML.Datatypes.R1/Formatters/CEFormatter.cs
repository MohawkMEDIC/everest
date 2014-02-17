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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.DataTypes;
using MARC.Everest.Interfaces;
using MARC.Everest.Connectors;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Data types R1 formatter for the CE data type
    /// </summary>
    public class CEFormatter : CVFormatter, IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        
        /// <summary>
        /// Graph object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            // Get an instance ref
            ICodedEquivalents instance_ics = (ICodedEquivalents)o;

            // Do a base format 
            base.Graph(s, o, result);

            // Format the coded simple
            if (instance_ics.Translation != null) // Original Text
            {
                foreach (IGraphable ig in instance_ics.Translation)
                {
                    s.WriteStartElement("translation", null);
                    var hostResult = Host.Graph(s, ig);
                    result.Code = hostResult.Code;
                    result.AddResultDetail(hostResult.Details);
                    s.WriteEndElement();
                }
            }

        }

        /// <summary>
        /// Parse an object from stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse from</param>
        /// <remarks>This need to be implemented in this manner (duplicating code) because of the way that 
        /// the XMLReader object functions</remarks>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            IResultDetail[] details = null;
            CE<String> retVal = CDFormatter.Parse<CE<String>>(s, Host, result);
            return retVal;
        }

        /// <summary>
        /// Get the type that this formatter handles
        /// </summary>
        public override string HandlesType
        {
            get { return "CE"; }
        }

       
        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public override List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(CE<>).GetProperty("Translation"));
            retVal.AddRange(base.GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}