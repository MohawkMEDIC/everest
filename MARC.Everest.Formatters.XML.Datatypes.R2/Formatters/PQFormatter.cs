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
 * Date: 02-06-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using MARC.Everest.Xml;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Formatter helper for the PQ data type
    /// </summary>
    internal class PQFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graphs <paramref name="o"/> onto <paramref name="s"/> storing result
        /// in <paramref name="result"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Get a strongly typed instance of o
            PQ opq = o as PQ;

            // Output additional attributes
            // Since a PQ with null flavor derived can still have a unit 
            // we only want to check for null flavors not derived
            if (opq.NullFlavor == null || opq.NullFlavor.Equals(NullFlavor.Derived))
            {
                if (opq.Unit != null)
                    s.WriteAttributeString("unit", Util.ToWireFormat(opq.Unit));
                if (opq.CodingRationale != null)
                    s.WriteAttributeString("codingRationale", Util.ToWireFormat(opq.CodingRationale));
            }

            // Output the PDV and QTY formatter helper data
            PDVFormatter pdvFormatter = new PDVFormatter();
            pdvFormatter.Host = this.Host;
            pdvFormatter.Graph(s, o, result);

            // Output the translations (if they exist)
            if(opq.Translation != null && (opq.NullFlavor == null || opq.NullFlavor.Equals(NullFlavor.Derived)))
                foreach (var trans in opq.Translation)
                {
                    s.WriteStartElement("translation", "urn:hl7-org:v3");
                    
                    // Graph
                    var hostResult = this.Host.Graph(s, trans);
                    result.Code = hostResult.Code;
                    result.AddResultDetail(hostResult.Details);
                    
                    s.WriteEndElement();
                }
        }

        /// <summary>
        /// Parse the PQ back into a structure
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            // Create the base formatter
            PDVFormatter baseFormatter = new PDVFormatter();
            baseFormatter.Host = this.Host;

            // Read temporary values
            string tUnit = null;
            if(s.GetAttribute("unit") != null)
                tUnit = s.GetAttribute("unit");
            SET<CodingRationale> tRationale = null;
            if (s.GetAttribute("codingRationale") != null)
                tRationale = Util.Convert<SET<CodingRationale>>(s.GetAttribute("codingRationale"));

            // Parse PDV content (only attributes)
            var retVal = baseFormatter.ParseAttributes<PQ>(s, result);

            // Set PDV content
            retVal.Unit = tUnit;
            retVal.CodingRationale = tRationale;

            // Process elements 
            // This requires a QTY formatter as QTY elements may be 
            // in the stream as well
            #region Elements
            if (!s.IsEmptyElement)
            {
                // Prepare a formatter to process QTY elements
                QTYFormatter qtyFormatter = new QTYFormatter();
                qtyFormatter.Host = this.Host;

                // Exit markers
                int sDepth = s.Depth;
                string sName = s.Name;

                // Translations
                SET<PQR> translations = new SET<PQR>();

                // Read the next element
                s.Read();

                // Read until exit condition is fulfilled
                while (!(s.NodeType == System.Xml.XmlNodeType.EndElement && s.Depth == sDepth && s.Name == sName))
                {
                    string oldName = s.Name; // Name
                    try
                    {
                        if (s.LocalName == "translation") // Format using ED
                        {
                            var hostResult = Host.Parse(s, typeof(PQR));
                            result.Code = hostResult.Code;
                            result.AddResultDetail(hostResult.Details);
                            translations.Add(hostResult.Structure as PQR);
                        }
                        else
                            qtyFormatter.ParseElementsInline(s, retVal, result);

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

                // Set translations
                if (!translations.IsEmpty)
                    retVal.Translation = translations;
            }
            #endregion

            // Validate
            ANYFormatter anyFormatter = new ANYFormatter();
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            anyFormatter.Validate(retVal as ANY, pathName, result);
            
            // REturn instance
            return retVal;
        }

        /// <summary>
        /// Gets the type that this handler accepts
        /// </summary>
        public string HandlesType
        {
            get { return "PQ"; }
        }

        /// <summary>
        /// Gets or sets the host of this formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the generic arguments for the type
        /// </summary>
        public Type[] GenericArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the list of supported properties
        /// </summary>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(new QTYFormatter().GetSupportedProperties());
            retVal.Add(typeof(PQ).GetProperty("Value"));
            retVal.Add(typeof(PQ).GetProperty("Unit"));
            retVal.Add(typeof(PQ).GetProperty("CodingRationale"));
            return retVal;
        }

        #endregion
    }
}
