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
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using MARC.Everest.Exceptions;
using MARC.Everest.Xml;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Data types R1 formatter for TEL
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL")]
    public class TELFormatter : IDatatypeFormatter
    {
        /// <summary>
        /// Host context
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph the object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The XmlWriter to graph object to</param>
        /// <param name="o">The object to graph</param>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            // Get an instance ref
            TEL instance_tel = (TEL)o;

            // Do a base format
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, o, result);

            // Null flavor
            if (((ANY)o).NullFlavor != null)
                return;

            // Attributes
            if (instance_tel.Value != null)
                s.WriteAttributeString("value", instance_tel.Value);
            if (instance_tel.Use != null && instance_tel.Use.Items != null
                && instance_tel.Use.Items.Count > 0)
                s.WriteAttributeString("use", Util.ToWireFormat(instance_tel.Use));
            if (instance_tel.Capabilities != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(
                    ResultDetailType.Warning, "Capabilities", "TEL", s.ToString()));

            // Elements
            if (instance_tel.UseablePeriod != null)
            {
                s.WriteStartElement("useablePeriod", "urn:hl7-org:v3");
                GTSFormatter formatterHelper = new GTSFormatter();
                formatterHelper.Host = this.Host;
                formatterHelper.Graph(s, instance_tel.UseablePeriod, result);
                s.WriteEndElement(); // usable period
            }
                //foreach (SXCM<TS> up in instance_tel.UseablePeriod)
                //{
                //    // Start element
                //    s.WriteStartElement("useablePeriod", "urn:hl7-org:v3");

                //    // Attributes for SXCM
                //    if(up.Operator != null)
                //        s.WriteAttributeString("operator", Util.ToWireFormat(up.Operator));
                    
                //    // Timestamp portion
                //    TSFormatter tsFormatter = new TSFormatter();
                //    tsFormatter.Graph(s, (TS)up, result); 
                //    s.WriteEndElement(); // end usablePeriod

                //}
             
        }

        /// <summary>
        /// Parse the object from stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The XmlReader to parse from</param>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Parse base (ANY) from the stream
            ANYFormatter baseFormatter = new ANYFormatter();

            // Parse TS
            TEL retVal = baseFormatter.Parse<TEL>(s, result);

            // Now parse our data out... Attributes
            if (s.GetAttribute("value") != null)
                retVal.Value = s.GetAttribute("value");
            if (s.GetAttribute("use") != null)
                retVal.Use = (SET<CS<TelecommunicationAddressUse>>)(Util.FromWireFormat(s.GetAttribute("use"), typeof(SET<CS<TelecommunicationAddressUse>>)));

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
                        if (s.LocalName == "useablePeriod") // Usable Period, since this is an SXCM
                        {
                            // Useable period doesn't exist
                            //if (retVal.UseablePeriod == null) retVal.UseablePeriod = new SET<SXCM<TS>>();
                            //retVal.UseablePeriod.Add(this.Host.ParseObject(s, typeof(SXCM<TS>)) as SXCM<TS>);
                            GTSFormatter formatter = new GTSFormatter();
                            formatter.Host = this.Host;
                            retVal.UseablePeriod = formatter.Parse(s, result) as GTS;
                        }
                    }
                    catch (MessageValidationException e)
                    {
                        result.AddResultDetail(new MARC.Everest.Connectors.ResultDetail(MARC.Everest.Connectors.ResultDetailType.Error, e.Message, s.ToString(), e));
                    }
                    finally
                    {
                        if(s.Name == oldName) s.Read();
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
        /// Gets the name of the structure this formatter handles
        /// </summary>
        public string HandlesType
        {
            get { return "TEL"; }
        }

        /// <summary>
        /// Get or set the generic arguments to this type (if applicable)
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(TEL).GetProperty("Value"));
            retVal.Add(typeof(TEL).GetProperty("Use"));
            retVal.Add(typeof(TEL).GetProperty("UseablePeriod"));
            retVal.AddRange(new ANYFormatter().GetSupportedProperties());
            return retVal;
        }
        #endregion
    }
}