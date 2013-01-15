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
 * Date: 21-12-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MARC.Everest.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Formatter helper for SXPR
    /// </summary>
    internal class SXPRFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graphs <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Translate the object to a QSET
            Type t = o.GetType();
            MethodInfo mi = t.GetMethod("TranslateToQSET");
            object qsetInstance = mi.Invoke(o, null);

            // Now pass to the qset formatter
            this.Host.Graph(s, qsetInstance as IGraphable);
        }

        /// <summary>
        /// Parse from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Shouldn't really be here, but call the 
            result.AddResultDetail(new NotImplementedResultDetail(ResultDetailType.Warning, "SXPR cannot be parsed by the R2 formatter directly, processing as QSET", s.ToString(), null));
            QSETFormatter formatter = new QSETFormatter();
            formatter.Host = this.Host;
            formatter.GenericArguments = this.GenericArguments;
            object retval = formatter.Parse(s, result);
            return retval;
        }

        /// <summary>
        /// Gets the type that this formater handles
        /// </summary>
        public string HandlesType
        {
            get { return "SXPR"; }
        }

        /// <summary>
        /// Gets or sets the host of the formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets generic arguments
        /// </summary>
        public Type[] GenericArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Get the supported properties
        /// </summary>
        /// <returns></returns>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            return new ANYFormatter().GetSupportedProperties();
        }

        #endregion
    }
}
