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
using System.Xml;
using MARC.Everest.DataTypes;
using MARC.Everest.Exceptions;
using MARC.Everest.Connectors;
using MARC.Everest.Xml;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Formats an II instance on the wire according to various data-types R1
    /// and derivativ
    /// </summary>
    public class IIFormatter : ANYFormatter, IDatatypeFormatter
    {
        /// <summary>
        /// Host context
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Get or set the generic arguments to this type (if applicable)
        /// </summary>
        public Type[] GenericArguments { get; set; }

        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph this datatype to the stream
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public void Graph(XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            II instance = o as II;

            // Base graph
            base.Graph(s, o as ANY, result);

            // Handle II graphing
            if (instance.IsNull)
                return;
            if(instance.Root != null) // root
                s.WriteAttributeString("root", instance.Root);
            if (instance.Extension != null) // extension
                s.WriteAttributeString("extension", instance.Extension);
            if (instance.AssigningAuthorityName != null) // assigning authority
                s.WriteAttributeString("assigningAuthorityName", instance.AssigningAuthorityName);
            
            if(instance.Displayable != null)
                s.WriteAttributeString("displayable", instance.Displayable.ToString().ToLower());

            // JF - Use is not permitted
            if (instance.Scope != null && instance.Scope.HasValue)
            {
                if (result.CompatibilityMode == DatatypeFormatterCompatibilityMode.Canadian)
                    switch (instance.Scope.Value)
                    {
                        case IdentifierScope.VersionIdentifier:
                            s.WriteAttributeString("use", "VER");
                            break;
                        case IdentifierScope.BusinessIdentifier:
                            s.WriteAttributeString("use", "BUS");
                            break;
                        default:
                            result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Scope", "II", s.ToString()));
                            break;
                    }
                else
                    result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Scope", "II", s.ToString()));
            }

            // Non supported features
            if (instance.IdentifierName != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "IdentifierName", "II", s.ToString()));
            if(instance.Reliability != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Reliability", "II", s.ToString()));

        }

        /// <summary>
        /// Parse this object
        /// </summary>
        public object Parse(XmlReader s, DatatypeFormatterParseResult result)
        {
           
            // Parse CS
            II retVal = base.Parse<II>(s, result);

            // Now parse our data out... Attributes
            if (s.GetAttribute("root") != null)
                retVal.Root = s.GetAttribute("root");
            if (s.GetAttribute("extension") != null)
                retVal.Extension = s.GetAttribute("extension");
            if (s.GetAttribute("displayable") != null)
                retVal.Displayable = (bool)Util.FromWireFormat(s.GetAttribute("displayable"), typeof(bool));
            if (s.GetAttribute("use") != null)
                switch (s.GetAttribute("use"))
                {
                    case "VER":
                        retVal.Scope = IdentifierScope.VersionIdentifier;
                        break;
                    case "BUS":
                        retVal.Scope = IdentifierScope.BusinessIdentifier;
                        break;
                }

            // Validate
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            base.Validate(retVal, pathName, result);


            return retVal;
        }

        /// <summary>
        /// Returns the datatype this formatter handles
        /// </summary>
        public override string HandlesType { get { return "II"; }}



        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public override List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(II).GetProperty("Root"));
            retVal.Add(typeof(II).GetProperty("Extension"));
            retVal.Add(typeof(II).GetProperty("Displayable"));
            retVal.Add(typeof(II).GetProperty("Use"));
            retVal.AddRange(base.GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}