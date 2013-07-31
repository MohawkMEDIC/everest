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
 * User: fyfej
 * Date: 9/22/2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using System.Reflection;
using MARC.Everest.Interfaces;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Formatter for the GTS datatype
    /// </summary>
    public class GTSFormatter : ANYFormatter, IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph the object <paramref name="o"/> to <paramref name="s"/>
        /// </summary>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            
            // Cast the object to GTS
            GTS instance = o as GTS;

            // Hull instance corrections
            if (instance.Hull != null)
            {
                if (instance.Hull.NullFlavor != null)
                {
                    instance.NullFlavor = instance.NullFlavor ?? instance.Hull.NullFlavor;
                    instance.Hull.NullFlavor = null;
                    result.AddResultDetail(new PropertyValuePropagatedResultDetail(ResultDetailType.Warning, "Hull.NullFlavor", "NullFlavor", instance.NullFlavor, s.ToString()));
                }
                if (instance.Hull.Flavor != null)
                {
                    instance.Flavor = instance.Flavor ?? instance.Hull.Flavor;
                    instance.Hull.Flavor = null;
                    result.AddResultDetail(new PropertyValuePropagatedResultDetail(ResultDetailType.Warning, "Hull.Flavor", "Flavor", instance.Flavor, s.ToString()));
                }
            }


            // Graph the base
            base.Graph(s, o as ANY, result);


            // Determine what type of hull we have
            if (instance.NullFlavor != null) // Null flavor specified, no hull will be graphed
            {
                return;
            }
            else if(instance.Hull == null)
            {
                result.AddResultDetail(new MandatoryElementMissingResultDetail(ResultDetailType.Error, "Cannot graph a GTS with a Null Hull", s.ToString()));
                return;
            }

            object instanceHull = instance.Hull;

            if (instanceHull.GetType().Name.StartsWith("QS"))
                instanceHull = instanceHull.GetType().GetMethod("TranslateToSXPR").Invoke(instanceHull, null);
            
            // Not for CDA:
            if (result.CompatibilityMode == DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture && (instanceHull is SXCM<TS>))
                ;
            else
            {
                string xsiTypeName = Util.CreateXSITypeName(instanceHull.GetType());
                s.WriteAttributeString("xsi", "type", DatatypeFormatter.NS_XSI, xsiTypeName);
            }

            // Output the formatting
            var hostResult = this.Host.Graph(s, (IGraphable)instanceHull);
            result.Code = hostResult.Code;
            result.AddResultDetail(hostResult.Details);

        }

        /// <summary>
        /// Parse the object from <paramref name="s"/>
        /// </summary>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Parse
            GTS retVal = base.Parse<GTS>(s, result);

            // Is there any need to continue?
            if (retVal.NullFlavor != null)
            {
                return retVal;
            }

            // Now determine the type of GTS
            string typeName = s.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
            if (String.IsNullOrEmpty(typeName) && result.CompatibilityMode == DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture)
                typeName = "SXCM_TS";

            IDatatypeFormatter formatter;

            // Parse the type
            switch (typeName)
            {
                case "IVL_TS":
                    formatter = new IVLFormatter();
                    break;
                case "PIVL_TS":
                    formatter = new PIVLFormatter();
                    break;
                case "EIVL_TS":
                    formatter = new EIVLFormatter();
                    break;
                case "SXPR_TS":
                    formatter = new SXPRFormatter();
                    break;
                case "SXCM_TS":
                    formatter = new SXCMFormatter();
                    break;
                default:
                    result.AddResultDetail(new NotSupportedChoiceResultDetail(ResultDetailType.Error, String.Format("Cannot parse a GTS Hull of type '{0}'", typeName), s.ToString(), null));
                    return null;
            }

            // Graph the Hull
            formatter.Host = this.Host;
            formatter.GenericArguments = new Type[] { typeof(TS) };
            retVal.Hull = formatter.Parse(s, result) as SXCM<TS>;
            
            // Correct the flavor, the flavor of the hull becomes the flavor of the object
            retVal.Flavor = retVal.Flavor ?? retVal.Hull.Flavor;
            retVal.Hull.Flavor = null;
            retVal.NullFlavor = retVal.NullFlavor ?? (retVal.Hull.NullFlavor != null ? retVal.Hull.NullFlavor.Clone() as CS<NullFlavor> : null);
            retVal.Hull.NullFlavor = null;

            // Set the details
            
            return retVal;
        }

        /// <summary>
        /// Get the type that this handles
        /// </summary>
        public override string HandlesType
        {
            get { return "GTS"; }
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(1);
            retVal.Add(typeof(GTS).GetProperty("Hull"));
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            return retVal;

        }
        #endregion
    }
}
