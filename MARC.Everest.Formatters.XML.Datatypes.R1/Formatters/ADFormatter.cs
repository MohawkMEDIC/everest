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
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using MARC.Everest.Xml;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// 
    /// </summary>
    public class ADFormatter : ANYFormatter, IDatatypeFormatter
    {
       

        /// <summary>
        /// Mapping to/from the Data Types R1 "each element has its own name"
        /// </summary>
        private static Dictionary<AddressPartType?, string> mapping = new Dictionary<AddressPartType?, string>()
        {
            { AddressPartType.AdditionalLocator, "additionalLocator" },
            { AddressPartType.AddressLine, "streetAddressLine" },
           { AddressPartType.BuildingNumber, "houseNumber" },
           { AddressPartType.Delimiter, "delimeter" },
           { AddressPartType.Country, "country" },
           { AddressPartType.State, "state" },
           { AddressPartType.County, "county" },
           { AddressPartType.City, "city" },
           { AddressPartType.PostalCode, "postalCode" },
           { AddressPartType.StreetAddressLine, "streetAddressLine" },
           { AddressPartType.BuildingNumberNumeric, "houseNumberNumeric" },
           { AddressPartType.Direction, "direction" },
           { AddressPartType.StreetName, "streetName" },
           { AddressPartType.StreetNameBase, "streetNameBase" },
           { AddressPartType.StreetType, "streetNameType" },
           { AddressPartType.UnitIdentifier, "unitID" },
           { AddressPartType.UnitDesignator, "unitType" },
           { AddressPartType.CareOf, "careOf" },
           { AddressPartType.CensusTract, "censusTract" },
           { AddressPartType.DeliveryAddressLine, "deliveryAddressLine" },
           { AddressPartType.DeliveryInstallationType, "deliveryInstallationType" },
           { AddressPartType.DeliveryInstallationArea, "deliveryInstallationArea" },
           { AddressPartType.DeliveryInstallationQualifier, "deliveryInstallationQualifier" },
           { AddressPartType.DeliveryMode, "deliveryMode" },
           { AddressPartType.DeliveryModeIdentifier, "deliveryModeIdentifier" },
           { AddressPartType.BuildingNumberSuffix, "buildingNumberSuffix" },
           { AddressPartType.PostBox, "postBox" },
           { AddressPartType.Precinct, "precinct" }
        };
        private static Dictionary<string, AddressPartType?> reverseMapping = new Dictionary<string, AddressPartType?>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ADFormatter()
        {
            #region Mappings

            foreach (KeyValuePair<AddressPartType?, string> kv in mapping)
                if(!reverseMapping.ContainsKey(kv.Value))
                    reverseMapping.Add(kv.Value, kv.Key);
            #endregion
        }

        #region IDatatypeFormatter Members

        
        /// <summary>
        /// Graph object <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            base.Graph(s, o as ANY, result);
            AD instance = o as AD;

            // Null flavor
            if (instance == null || instance.NullFlavor != null)
                return;

            // use
            if (instance.Use != null)
                s.WriteAttributeString("use", Util.ToWireFormat(instance.Use));
            
            if(instance.IsNotOrdered != null)
                s.WriteAttributeString("isNotOrdered", instance.IsNotOrdered.ToString().ToLower());

            // parts
            if(instance.Part != null)
                foreach (ADXP part in instance.Part)
                {
                    if (mapping.ContainsKey(part.Type ?? AddressPartType.AddressLine))
                        s.WriteStartElement(mapping[part.Type ?? AddressPartType.AddressLine], "urn:hl7-org:v3");
                    else
                        throw new MessageValidationException(string.Format("Can't represent address part '{0}' in datatypes R1 at '{1}'", part.Type, (s as XmlStateWriter).CurrentPath));

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
                    s.WriteEndElement(); // useable
            }

        }
        
        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse</param>
        /// <returns>The parsed object</returns>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Parse the address parts
            // Parse base (ANY) from the stream
            // Parse CS
            AD retVal = base.Parse<AD>(s, result);

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
                // string Name
                while (!(s.NodeType == System.Xml.XmlNodeType.EndElement && s.Depth == sDepth && s.Name == sName))
                {
                    string oldName = s.Name; // Name
                    try
                    {
                        AddressPartType? adxpType; // Address part type

                        // JF - This is a Canadian extension
                        if (s.LocalName == "useablePeriod") // Usable Period, since this is an SXCM we'll need to read manually
                        {
                            // Useable period doesn't exist
                            GTSFormatter sxcmFormatter = new GTSFormatter();
                            sxcmFormatter.Host = this.Host;
                            retVal.UseablePeriod = sxcmFormatter.Parse(s, result) as GTS;
                        }
                        else if (reverseMapping.TryGetValue(s.LocalName, out adxpType)) // Reverse map exists, so this is a part
                        {
                            ADXPFormatter adxpFormatter = new ADXPFormatter(); // ADXP Formatter
                            adxpFormatter.Host = this.Host;
                            ADXP part = (ADXP)adxpFormatter.Parse(s, result); // Parse
                            part.Type = adxpType;
                            retVal.Part.Add(part); // Add to AD
                        }
                        else if(s.NodeType == System.Xml.XmlNodeType.Element)
                            result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null));
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
            base.Validate(retVal, pathName, result);
            return retVal;
        }

        /// <summary>
        /// What types does this formatter handle
        /// </summary>
        public override string HandlesType
        {
            get { return "AD"; }
        }


        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public override List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>()
            {
                typeof(AD).GetProperty("Use"),
                typeof(AD).GetProperty("IsNotOrdered"),
                typeof(AD).GetProperty("Part"),
                typeof(AD).GetProperty("UseablePeriod")
            };
            retVal.AddRange(base.GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}