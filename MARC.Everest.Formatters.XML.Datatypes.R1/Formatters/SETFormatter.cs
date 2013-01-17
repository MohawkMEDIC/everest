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
using System.Reflection;
using System.Collections;
using MARC.Everest.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using MARC.Everest.Xml; 
using MARC.Everest.Attributes;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Data types R1 formatter for SET
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SET")]
    public class SETFormatter : IDatatypeFormatter
    {

        #region IDatatypeFormatter Members

       
        /// <summary>
        /// Graph object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The XmlWriter to graph to</param>
        /// <param name="o">The object to graph</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public virtual void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {

            // Has this formatter found the elemScopeStack yet?
            string currentElementName = null;

            // TODO: Make all parameters XmlStateWriters, next release of Everest
            // TODO: Don't have enough time to test what this may change upstream
            if (s is XmlStateWriter)
                currentElementName = (s as XmlStateWriter).CurrentElement.Name;
            else
            {
                result.AddResultDetail(new ResultDetail(ResultDetailType.Error, "Can't represent a SET or LIST using this Xml Stream as it does not inherit from an XmlStateWriter", s.ToString(), null));
                return;
            }

            // Write the array
            IEnumerable instance = (IEnumerable)o;

            int count = 0;

            // Get each element in the instance
            IEnumerator enumerator = instance.GetEnumerator();

            if (!enumerator.MoveNext()) return; // No elements

            // Loop through elements
            while (enumerator.Current != null)
            {
                if (count != 0) // Not the first element, write the element name again
                    s.WriteStartElement(currentElementName, "urn:hl7-org:v3");

                // JF: Output XSI:Type
                if (GenericArguments != null && !enumerator.Current.GetType().Equals(GenericArguments[0]))
                {
                    string xsiTypeName = Util.CreateXSITypeName(enumerator.Current.GetType());
                    s.WriteAttributeString("xsi", "type", DatatypeFormatter.NS_XSI, xsiTypeName);
                }

                var hostResult = Host.Graph(s, (IGraphable)enumerator.Current);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                if (!enumerator.MoveNext()) break; // No further objects
                s.WriteEndElement();
                count++;
            }
        }
        
        
        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to read from</param>
        public virtual object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Get the current element name
            string currentElementName = s.LocalName;

            LIST<IGraphable> retVal = new LIST<IGraphable>();

            // Read until the current name is exhausted
            while (s.LocalName == currentElementName && !s.EOF)
            {
                if (s.NodeType == System.Xml.XmlNodeType.Element)
                {
                    // Correct the XSI attribute
                    //if (Util.CreateXSITypeName(GenericArguments[0]) != s.GetAttribute("type", DatatypeFormatter.NS_XSI) &&
                    //    s is XmlStateReader && !String.IsNullOrEmpty(s.GetAttribute("type", DatatypeFormatter.NS_XSI)))
                    //    (s as XmlStateReader).AddFakeAttribute("type", Util.CreateXSITypeName(GenericArguments[0]));
                    
                    var hostResult = Host.Parse(s, GenericArguments[0]);
                    result.Code = hostResult.Code;
                    result.AddResultDetail(hostResult.Details);
                    retVal.Add(hostResult.Structure);
                }

                // Read until the next element
                s.Read();
                while (s.NodeType != System.Xml.XmlNodeType.Element && !s.EOF)
                {
                    if (s.NodeType == System.Xml.XmlNodeType.EndElement &&
                        s.LocalName != currentElementName)
                        return retVal;
                    s.Read();
                }
            }

            // Return the array
            return retVal;
        }

        /// <summary>
        /// Get the type that this formatter supports
        /// </summary>
        public virtual string HandlesType
        {
            get { return "SET"; }
        }

        /// <summary>
        /// Reference to the hosting formatter
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

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
            retVal.Add(typeof(SET<>).GetProperty("Items"));
            return retVal;
        }
        #endregion
    }
}