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
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Entity name expression part formatter
    /// </summary>
    internal class ENXPFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            
            // XP is special, it does not extend anything so we have to graph from scratch
            var instance = o as ENXP;

            // Null flavor?
            if (instance.NullFlavor != null)
                s.WriteAttributeString("nullFlavor", Util.ToWireFormat(instance.NullFlavor));
            else
            {
                // Validate 
                DatatypeR2FormatterParseResult tResult = new DatatypeR2FormatterParseResult(result.ValidateConformance);
                new ANYFormatter().Validate(instance, s.ToString(), tResult);
                result.AddResultDetail(tResult.Details);
                
                // Qualifiers (copy for modification)
                SET<CS<EntityNamePartQualifier>> qualifiers = new SET<CS<EntityNamePartQualifier>>();
                if(instance.Qualifier != null)
                    foreach (var qlf in instance.Qualifier)
                        qualifiers.Add(qlf.Clone() as CS<EntityNamePartQualifier>);

                // Unsupported properties
                if (instance.ControlActExt != null)
                    result.AddResultDetail(new UnsupportedDatatypeR2PropertyResultDetail(ResultDetailType.Warning, "ControlActExt", "ENXP", s.ToString()));
                if (instance.ControlActRoot != null)
                    result.AddResultDetail(new UnsupportedDatatypeR2PropertyResultDetail(ResultDetailType.Warning, "ControlActRoot", "ENXP", s.ToString()));
                if (instance.ValidTimeHigh != null)
                    result.AddResultDetail(new UnsupportedDatatypeR2PropertyResultDetail(ResultDetailType.Warning, "ValidTimeHigh", "ENXP", s.ToString()));
                if (instance.ValidTimeLow != null)
                    result.AddResultDetail(new UnsupportedDatatypeR2PropertyResultDetail(ResultDetailType.Warning, "ValidTimeLow", "ENXP", s.ToString()));
                if (instance.Flavor != null)
                    result.AddResultDetail(new UnsupportedDatatypeR2PropertyResultDetail(ResultDetailType.Warning, "Flavor", "ENXP", s.ToString()));
                if (instance.UpdateMode != null)
                    result.AddResultDetail(new UnsupportedDatatypeR2PropertyResultDetail(ResultDetailType.Warning, "UpdateMode", "ENXP", s.ToString()));

                // Output the supported properties
                if (instance.Value != null)
                    s.WriteAttributeString("value", instance.Value);
                if (instance.Code != null)
                    s.WriteAttributeString("code", instance.Code);
                if (instance.CodeSystem != null)
                    s.WriteAttributeString("codeSystem", instance.CodeSystem);
                if (instance.CodeSystemVersion != null)
                    s.WriteAttributeString("codeSystemVersion", instance.CodeSystemVersion);
                if (instance.Type != null)
                {
                    // Qualifiers that count as TITLE
                    EntityNamePartQualifier[] titleQualifiers = new EntityNamePartQualifier[] {
                        EntityNamePartQualifier.Professional,
                        EntityNamePartQualifier.Nobility,
                        EntityNamePartQualifier.Academic ,
                        EntityNamePartQualifier.LegalStatus
                    };

                    // If type is not SFX or PFX then output the type,
                    // if it is either SFX or PFX then don't output the type
                    // but do modify the qualifier
                    switch(instance.Type.Value)
                    {
                        case EntityNamePartType.Prefix:
                            if (instance.Qualifier == null)
                                instance.Qualifier = new SET<CS<EntityNamePartQualifier>>();
                            if(!qualifiers.Contains(EntityNamePartQualifier.Prefix))
                                qualifiers.Add(EntityNamePartQualifier.Prefix);

                            // Change the instance type
                            if(Array.Exists(titleQualifiers, q => qualifiers.Contains(q)))
                                s.WriteAttributeString("type", "TITLE");

                            break;
                        case EntityNamePartType.Suffix:
                            if (instance.Qualifier == null)
                                instance.Qualifier = new SET<CS<EntityNamePartQualifier>>();
                            if (!qualifiers.Contains(EntityNamePartQualifier.Suffix))
                                qualifiers.Add(EntityNamePartQualifier.Suffix);
                            
                            // Change the instance type
                            if (Array.Exists(titleQualifiers, q => qualifiers.Contains(q)))
                                s.WriteAttributeString("type", "TITLE");

                            break;
                        default:
                            s.WriteAttributeString("type", Util.ToWireFormat(instance.Type));
                            break;
                    }
                }
                if (!qualifiers.IsEmpty)
                    s.WriteAttributeString("qualifier", Util.ToWireFormat(qualifiers));

            }
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Parse result data
            var retVal = new ENXP();

            // Parse the supported data
            if (s.GetAttribute("nullFlavor") != null)
                retVal.NullFlavor = Util.Convert<NullFlavor>(s.GetAttribute("nullFlavor"));
            if (s.GetAttribute("value") != null)
                retVal.Value = s.GetAttribute("value");
            if (s.GetAttribute("code") != null)
                retVal.Code = s.GetAttribute("code");
            if (s.GetAttribute("codeSystem") != null)
                retVal.CodeSystem = s.GetAttribute("codeSystem");
            if (s.GetAttribute("codeSystemVersion") != null)
                retVal.CodeSystemVersion = s.GetAttribute("codeSystemVersion");
            if (s.GetAttribute("type") != null)
                retVal.Type = Util.Convert<EntityNamePartType>(s.GetAttribute("type"));
            if (s.GetAttribute("qualifier") != null)
                retVal.Qualifier = Util.Convert<SET<CS<EntityNamePartQualifier>>>(s.GetAttribute("qualifier"));
            if (s.GetAttribute("language") != null)
                result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, "@language", null, s.ToString(), null));

            // Return & validate
            new ANYFormatter().Validate(retVal, s.ToString(), result);
            return retVal;
        }

        /// <summary>
        /// Gets the type this handles
        /// </summary>
        public string HandlesType
        {
            get { return "ENXP"; }
        }

        /// <summary>
        /// Gets or sets the host instance
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
        /// Gets a list of the supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            var retVal = new List<PropertyInfo>(
                new PropertyInfo[] {
                    typeof(ANY).GetProperty("NullFlavor"),
                    typeof(ENXP).GetProperty("Value"),
                    typeof(ENXP).GetProperty("Code"),
                    typeof(ENXP).GetProperty("CodeSystem"),
                    typeof(ENXP).GetProperty("CodeSystemVersion"),
                    typeof(ENXP).GetProperty("Type"),
                    typeof(ENXP).GetProperty("Qualifier")
                }
            );
            return retVal;

        }

        #endregion
    }
}
