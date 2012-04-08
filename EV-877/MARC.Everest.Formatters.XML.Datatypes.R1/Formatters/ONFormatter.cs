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
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Formatter for the ON Type
    /// </summary>
    public class ONFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

       

        /// <summary>
        /// Graph this object to the specified stream
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            ENFormatter formatter = new ENFormatter() { Host = this.Host };


            // Validate and remove any incriminating data
            ON instance = o as ON;
                for(int i = instance.Part.Count - 1; i >= 0; i--)
                    if (instance.Part[i].Type == EntityNamePartType.Family ||
                        instance.Part[i].Type == EntityNamePartType.Given)
                    {
                        result.AddResultDetail(new VocabularyIssueResultDetail(ResultDetailType.Warning,
                            String.Format("Part name '{0}' in ON instance will be removed. ON Parts cannot have FAM or GIV parts", instance.Part[i]),
                            s.ToString(),
                            null));
                        instance.Part.RemoveAt(i);
                    }
            formatter.Graph(s, o, result);
        }

        /// <summary>
        /// Parse the object from the specified stream
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {

            ENFormatter formatter = new ENFormatter()  { Host = this.Host };
            var instance = formatter.Parse(s, result) as EN;

            ON retVal = new ON(EntityNameUse.Alphabetic, instance.Part) { Use = instance.Use };
            
            // Remove non-allowed parts
                for (int i = retVal.Part.Count - 1; i >= 0; i--)
                    if (retVal.Part[i].Type == EntityNamePartType.Family ||
                        retVal.Part[i].Type == EntityNamePartType.Given)
                    {
                        result.AddResultDetail(new VocabularyIssueResultDetail(ResultDetailType.Warning,
                            String.Format("Part name '{0}' in ON instance will be removed. ON Parts cannot have FAM or GIV parts", retVal.Part[i]),
                            s.ToString(),
                            null));
                        retVal.Part.RemoveAt(i);
                    }

            retVal.NullFlavor = instance.NullFlavor;
            retVal.Flavor = instance.Flavor;


            return retVal;
        }

        /// <summary>
        /// Gets the type that this formatter can handle
        /// </summary>
        public string HandlesType
        {
            get { return "ON"; }
        }

        /// <summary>
        /// Gets or sets the host
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the generic arguments
        /// </summary>
        public Type[] GenericArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.AddRange(new ENFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}
