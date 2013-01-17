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
 * Date: 06-27-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using MARC.Everest.Exceptions;
using MARC.Everest.Xml;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Formatter that can graph and interpret the ED datatype
    /// </summary>
    internal class EDFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Get an instance ref
            ED instance_ed = (ED)o;

            // Do a base format
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, o, result);

            // Null flavor
            if (((ANY)o).NullFlavor != null)
            {
                return;
            }

            // Attributes
            if (instance_ed.MediaType != null && instance_ed.Representation != EncapsulatedDataRepresentation.TXT)
                s.WriteAttributeString("mediaType", instance_ed.MediaType);
            if (instance_ed.Language != null)
                s.WriteAttributeString("language", instance_ed.Language);
            if (instance_ed.Compression != null)
                s.WriteAttributeString("compression", Util.ToWireFormat(instance_ed.Compression));
            if (instance_ed.IntegrityCheckAlgorithm != null)
                s.WriteAttributeString("integrityCheckAlgorithm", Util.ToWireFormat(instance_ed.IntegrityCheckAlgorithm));

            Encoding textEncoding = System.Text.Encoding.UTF8;

            // Representation of data
            if(instance_ed.Data != null && instance_ed.Data.Length > 0)
                switch(instance_ed.Representation)
                {
                    case EncapsulatedDataRepresentation.TXT:
                        s.WriteAttributeString("value", textEncoding.GetString(instance_ed.Data));
                        break;
                    case EncapsulatedDataRepresentation.B64:
                        s.WriteStartElement("data", "urn:hl7-org:v3");
                        s.WriteBase64(instance_ed.Data, 0, instance_ed.Data.Length);
                        s.WriteEndElement();// data
                        break;
                    case EncapsulatedDataRepresentation.XML:
                        s.WriteStartElement("xml", "urn:hl7-org:v3");
                        s.WriteRaw(instance_ed.XmlData.OuterXml);
                        s.WriteEndElement(); // xml
                        break;
                }

            // Elements
            if (instance_ed.Reference != null)
            {
                s.WriteStartElement("reference", "urn:hl7-org:v3");
                var hostResult = Host.Graph(s, instance_ed.Reference);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }
            if (instance_ed.IntegrityCheck != null && instance_ed.IntegrityCheck.Length > 0)
            {
                s.WriteStartElement("integrityCheck", "urn:hl7-org:v3");
                s.WriteBase64(instance_ed.IntegrityCheck, 0, instance_ed.IntegrityCheck.Length);
                s.WriteEndElement(); // intcheck
            }
            if (instance_ed.Thumbnail != null)
            {
                s.WriteStartElement("thumbnail", "urn:hl7-org:v3");
                var hostResult = Host.Graph(s, instance_ed.Thumbnail);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);

                s.WriteEndElement();
            }
            if (instance_ed.Description != null)
            {
                s.WriteStartElement("description", "urn:hl7-org:v3");
                var hostResult = Host.Graph(s, instance_ed.Description);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }
            if (instance_ed.Translation != null && !instance_ed.Translation.IsNull)
            {
                foreach (var trans in instance_ed.Translation)
                {
                    s.WriteStartElement("translation", "urn:hl7-org:v3");
                    var hostResult = Host.Graph(s, trans);
                    result.Code = hostResult.Code;
                    result.AddResultDetail(hostResult.Details);
                    s.WriteEndElement(); // translation
                }
            }

        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Parse base (ANY) from the stream
            ANYFormatter baseFormatter = new ANYFormatter();
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;

            // Parse ED
            ED retVal = baseFormatter.Parse<ED>(s);

            // Now parse our data out... Attributes
            if (s.GetAttribute("mediaType") != null)
                retVal.MediaType = s.GetAttribute("mediaType");
            if (s.GetAttribute("language") != null)
                retVal.Language = s.GetAttribute("language");
            if (s.GetAttribute("compression") != null)
                retVal.Compression = (EncapsulatedDataCompression?)Util.FromWireFormat(s.GetAttribute("compression"), typeof(EncapsulatedDataCompression));
            if (s.GetAttribute("integrityCheckAlgorithm") != null)
                retVal.IntegrityCheckAlgorithm = Util.Convert<EncapsulatedDataIntegrityAlgorithm>(s.GetAttribute("integrityCheckAlgorithm"));
            if (s.GetAttribute("value") != null)
            {
                retVal.Representation = EncapsulatedDataRepresentation.TXT;
                if(retVal.MediaType == null)
                    retVal.MediaType = "text/plain";
                retVal.Value = s.GetAttribute("value");
            }

            // Elements and inner data
            #region Elements
            if (!s.IsEmptyElement)
            {
                // Exit markers
                int sDepth = s.Depth;
                string sName = s.Name;

                Encoding textEncoding = System.Text.Encoding.UTF8;
                s.Read();
                // Read until exit condition is fulfilled
                while (!(s.NodeType == System.Xml.XmlNodeType.EndElement && s.Depth == sDepth && s.Name == sName))
                {
                    string oldName = s.Name; // Name
                    try
                    {
                        if (s.LocalName == "thumbnail") // Format using ED
                        {
                            var hostResult = Host.Parse(s, typeof(ED));
                            result.Code = hostResult.Code;
                            result.AddResultDetail(hostResult.Details);
                            retVal.Thumbnail = (ED)hostResult.Structure;
                        }
                        else if (s.LocalName == "reference") // Format using TEL
                        {
                            var hostResult = Host.Parse(s, typeof(TEL));
                            result.Code = hostResult.Code;
                            result.AddResultDetail(hostResult.Details);
                            retVal.Reference = (TEL)hostResult.Structure;
                        }
                        else if (s.LocalName == "translation") // Translation
                        {
                            var hostResult = Host.Parse(s, typeof(ED));
                            result.Code = hostResult.Code;
                            result.AddResultDetail(hostResult.Details);
                            if (retVal.Translation == null)
                                retVal.Translation = new SET<ED>();
                            retVal.Translation.Add((ED)hostResult.Structure);
                        }
                        else if (s.LocalName == "data") // Data
                        {
                            retVal.Representation = EncapsulatedDataRepresentation.B64;
                            retVal.Data = Convert.FromBase64String(s.ReadString());
                        }
                        else if (s.LocalName == "xml") // data
                        {
                            retVal.Representation = EncapsulatedDataRepresentation.XML;
                            retVal.Data = textEncoding.GetBytes(s.ReadInnerXml());
                        }
                        else if (s.LocalName == "integrityCheck")
                        {
                            retVal.IntegrityCheck = Convert.FromBase64String(s.ReadString());
                        }
                    }
                    catch (MessageValidationException e)
                    {
                        result.AddResultDetail(new MARC.Everest.Connectors.ResultDetail(MARC.Everest.Connectors.ResultDetailType.Error, e.Message, s.ToString(), e));
                    }
                    finally
                    {
                        if (s.Name == oldName) s.Read();
                    }
                }
            }
            #endregion

         
            // Finally, the hash, this will validate the data
            if (!retVal.ValidateIntegrityCheck())
                result.AddResultDetail(new ResultDetail(ResultDetailType.Warning,
                    string.Format("Encapsulated data with content starting with '{0}' failed integrity check!", retVal.ToString().PadRight(10, ' ').Substring(0, 10)),
                    s.ToString(),
                    null));

            // Validate
            baseFormatter.Validate(retVal, pathName, result);

            return retVal;
        }

        /// <summary>
        /// Gets the type that this formatter can handle
        /// </summary>
        public string HandlesType
        {
            get { return "ED"; }
        }

        /// <summary>
        /// Gets or sets the host of the formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets the generic arguments of the formatter
        /// </summary>
        public Type[] GenericArguments { get; set; }

        #endregion

        #region IDatatypeFormatter Members


        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            return new List<PropertyInfo>(typeof(ED).GetProperties(BindingFlags.Public | BindingFlags.Instance));
        }

        #endregion
    }
}
