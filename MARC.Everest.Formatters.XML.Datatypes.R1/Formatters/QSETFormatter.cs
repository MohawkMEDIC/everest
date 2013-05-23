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
using MARC.Everest.Connectors;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// A qset formatter must emulate the old SXPR functionality
    /// </summary>
    public class QSETFormatter : SXPRFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph to the stream
        /// </summary>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
             
            result.AddResultDetail(new NotImplementedResultDetail(ResultDetailType.Warning, String.Format("{0} cannot be graphed by the R1 formatter directly, processing as SXPR", this.HandlesType), s.ToString(), null));

            // Convert o as SXPR
            base.Graph(s, o.GetType().GetMethod("TranslateToSXPR").Invoke(o, null), result);


        }

        /// <summary>
        /// Parse from the stream
        /// </summary>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            result.AddResultDetail(new NotImplementedResultDetail(ResultDetailType.Warning, "QSET and derivatives cannot be parsed by the R1 formatter directly, processing as SXPR", s.ToString(), null));
            object retval = base.Parse(s, result);
            return retval;

        }

        /// <summary>
        /// Gets the type that this handles
        /// </summary>
        public override string HandlesType
        {
            get { return "QSET"; }
        }

        /// <summary>
        /// Gets supported properties from the QSET type
        /// </summary>
        public override List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            return new ANYFormatter().GetSupportedProperties();
        }

        #endregion
    }
}
