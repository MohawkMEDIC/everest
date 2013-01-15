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
 * Date: 11-11-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using MARC.Everest.Xml;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// A data types R2 formatter for the AD data type
    /// </summary>
    internal class ADFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph the object <paramref name="o"/> to <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            AD instance = o as AD;
 
            // Base formatting
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, instance, result);

            // Use 
            if (instance.Use != null && !instance.Use.IsNull)
                s.WriteAttributeString("use", Util.ToWireFormat(instance.Use));

            // No need to format 
            if (instance.NullFlavor != null)
                return;

            // Is not ordered
            if (instance.IsNotOrdered != null && instance.IsNotOrdered.HasValue)
                s.WriteAttributeString("isNotOrdered", Util.ToWireFormat(instance.IsNotOrdered));

            // Parts
            if(instance.Part != null)
                foreach (var part in instance.Part)
                {
                    s.WriteStartElement("part", "urn:hl7-org:v3");
                    ADXPFormatter adFormatter = new ADXPFormatter();
                    adFormatter.Graph(s, part, result);
                    s.WriteEndElement();
                }

            // Useable period
            if (instance.UseablePeriod != null)
            {
                s.WriteStartElement("useablePeriod", "urn:hl7-org:v3");
                GTSFormatter gtsFormatter = new GTSFormatter();
                gtsFormatter.Host = this.Host;
                gtsFormatter.Graph(s, instance.UseablePeriod, result);
                s.WriteEndElement();
            }
        }


        /// <summary>
        /// Parse an AD instance from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Create a base formatter so it can construct the object
            ANYFormatter baseFormatter = new ANYFormatter(); 

            // Parse AD
            AD retVal = baseFormatter.Parse<AD>(s);

            // Now parse our data out... Attributes
            if (s.GetAttribute("use") != null)
                retVal.Use = (SET<CS<PostalAddressUse>>)Util.FromWireFormat(s.GetAttribute("use"), typeof(SET<CS<PostalAddressUse>>));
            if (s.GetAttribute("isNotOrdered") != null)
                retVal.IsNotOrdered = (bool)Util.FromWireFormat(s.GetAttribute("isNotOrdered"), typeof(bool));

            // Loop through content
            // Elements
            #region Elements
            if (!s.IsEmptyElement)
            {

                int sDepth = s.Depth;
                string sName = s.Name;

                s.Read();

                // Read all internal elements
                while (!(s.NodeType == System.Xml.XmlNodeType.EndElement && s.Depth == sDepth && s.Name == sName))
                {
                    string oldName = s.Name; // Name
                    try
                    {
                        AddressPartType? adxpType; // Address part type

                        if (s.LocalName == "useablePeriod") // Usable Period, since this is a GTS
                        {
                            // Useable period doesn't exist
                            GTSFormatter sxcmFormatter = new GTSFormatter();
                            sxcmFormatter.Host = this.Host;
                            retVal.UseablePeriod = sxcmFormatter.Parse(s, result) as GTS;
                        }
                        else if (s.LocalName == "part") // Reverse map exists, so this is a part
                        {
                            ADXPFormatter adxpFormatter = new ADXPFormatter(); // ADXP Formatter
                            adxpFormatter.Host = this.Host;
                            ADXP part = (ADXP)adxpFormatter.Parse(s, result); // Parse
                            retVal.Part.Add(part); // Add to AD
                        }
                    }
                    catch (MessageValidationException e)
                    {
                        result.AddResultDetail(new ResultDetail(ResultDetailType.Error, e.Message, s.ToString(), e)); // Append details
                    }
                    finally
                    {
                        if (oldName == s.Name) s.Read(); // Read if we need to
                    }
                }
            }
            #endregion

            // Validate
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            baseFormatter.Validate(retVal, pathName, result);
            return retVal;
        }

        /// <summary>
        /// Handles type of AD
        /// </summary>
        public string HandlesType
        {
            get { return "AD"; }
        }

        /// <summary>
        /// Gets or sets the host of this formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// GEts or sets the generic arguments
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get the supported properties
        /// </summary>
        /// <returns></returns>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            retVal.Add(typeof(AD).GetProperty("Use"));
            retVal.Add(typeof(AD).GetProperty("IsNotOrdered"));
            retVal.Add(typeof(AD).GetProperty("Part"));
            retVal.Add(typeof(AD).GetProperty("UseablePeriod"));
            return retVal;
        }

        #endregion
    }
}
