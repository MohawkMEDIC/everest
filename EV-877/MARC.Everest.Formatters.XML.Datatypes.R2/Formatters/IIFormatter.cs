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
 * Date: 02-09-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Represents an II formatter
    /// </summary>
    public class IIFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graphs <paramref name="o"/> to <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {

            // Any formatter
            ANYFormatter anyFormatter = new ANYFormatter();
            anyFormatter.GenericArguments = this.GenericArguments;
            anyFormatter.Host = this.Host;

            // Graph base
            anyFormatter.Graph(s, o, result);

            // Instance
            II iiInstance = o as II;

            // Other nullFlavor
            if (iiInstance.NullFlavor != null)
                return;

            // Properties
            if (iiInstance.Root != null)
                s.WriteAttributeString("root", iiInstance.Root);
            if (iiInstance.Extension != null)
                s.WriteAttributeString("extension", iiInstance.Extension);
            if (iiInstance.IdentifierName != null)
                s.WriteAttributeString("identifierName", iiInstance.IdentifierName);
            if (iiInstance.Displayable != null)
                s.WriteAttributeString("displayable", Util.ToWireFormat(iiInstance.Displayable));
            if (iiInstance.Scope != null)
                s.WriteAttributeString("scope", Util.ToWireFormat(iiInstance.Scope));
            if (iiInstance.Reliability != null)
                s.WriteAttributeString("reliability", Util.ToWireFormat(iiInstance.Reliability));
            if (iiInstance.AssigningAuthorityName != null)
                result.AddResultDetail(new UnsupportedDatatypeR2PropertyResultDetail(
                    ResultDetailType.Warning, "AssigningAuthorityName", "II", s.ToString()
                ));

        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Parse the base
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Host = this.Host;
            baseFormatter.GenericArguments = this.GenericArguments;
            var retVal = baseFormatter.Parse<II>(s);

            // Parse II specific data
            if (s.GetAttribute("root") != null)
                retVal.Root = s.GetAttribute("root");
            if (s.GetAttribute("extension") != null)
                retVal.Extension = s.GetAttribute("extension");
            if (s.GetAttribute("identifierName") != null)
                retVal.IdentifierName = s.GetAttribute("identifierName");
            if (s.GetAttribute("displayable") != null)
                retVal.Displayable = Util.Convert<bool>(s.GetAttribute("displayable"));
            if (s.GetAttribute("scope") != null)
                retVal.Scope = Util.Convert<IdentifierScope>(s.GetAttribute("scope"));
            if (s.GetAttribute("reliability") != null)
                retVal.Reliability = Util.Convert<IdentifierReliability>(s.GetAttribute("reliability"));

            // Validate
            baseFormatter.Validate(retVal, s.ToString(), result);

            return retVal;
        }

        /// <summary>
        /// Gets the type that this handles
        /// </summary>
        public string HandlesType
        {
            get { return "II"; }
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
        /// Gets the supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            var retVal = new ANYFormatter().GetSupportedProperties();
            retVal.AddRange(new PropertyInfo[]
            {
                typeof(II).GetProperty("Root"),
                typeof(II).GetProperty("Extension"),
                typeof(II).GetProperty("IdentifierName"),
                typeof(II).GetProperty("Displayable"),
                typeof(II).GetProperty("Scope"),
                typeof(II).GetProperty("Reliability")
            });
            return retVal;
        }

        #endregion
    }
}
