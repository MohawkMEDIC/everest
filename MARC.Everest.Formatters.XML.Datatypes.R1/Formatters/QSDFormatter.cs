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
 * Date: 9/22/2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using System.Collections;
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// A qset formatter must emulate the old SXPR functionality
    /// </summary>
    public class QSDFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph to the stream
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
             
            result.AddResultDetail(new NotImplementedResultDetail(ResultDetailType.Warning, "QSD cannot be graphed by the R1 formatter directly, processing as SXPR", s.ToString(), null));
            SXPRFormatter formatter = new SXPRFormatter();
            formatter.Host = this.Host;
            formatter.GenericArguments = this.GenericArguments;
            
            // Convert o as SXPR
            formatter.Graph(s, o.GetType().GetMethod("TranslateToSXPR").Invoke(o, null), result);

        }

        /// <summary>
        /// Parse from the stream
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            result.AddResultDetail(new NotImplementedResultDetail(ResultDetailType.Warning, "QSD cannot be parsed by the R1 formatter directly, processing as SXPR", s.ToString(), null));
            SXPRFormatter formatter = new SXPRFormatter();
            formatter.Host = this.Host;
            formatter.GenericArguments = this.GenericArguments;
            object retval = formatter.Parse(s, result);
            return retval;


        }

        /// <summary>
        /// Gets the type that this handles
        /// </summary>
        public string HandlesType
        {
            get { return "QSD"; }
        }

        /// <summary>
        /// Gets the host of this type
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets the generic arguments
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Gets supported properties from the QSET type
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            return new ANYFormatter().GetSupportedProperties();
        }

        #endregion
    }
}
