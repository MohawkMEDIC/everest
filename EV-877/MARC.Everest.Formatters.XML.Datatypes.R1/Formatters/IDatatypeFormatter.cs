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
using MARC.Everest.Connectors;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Summary of DatatypeFormatter
    /// </summary>
    internal interface IDatatypeFormatter
    {

        /// <summary>
        /// Graph object <paramref name="o"/> to stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        void Graph(XmlWriter s, object o, DatatypeFormatterGraphResult result);
        /// <summary>
        /// Parse object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse from</param>
        /// <returns>The parsed object</returns>
        object Parse(XmlReader s, DatatypeFormatterParseResult result);
        /// <summary>
        /// The type this formatter handles
        /// </summary>
        string HandlesType { get; }
        /// <summary>
        /// Get or set a host context
        /// </summary>
        IXmlStructureFormatter Host { get; set; }
        /// <summary>
        /// The generic arguments to the type being formatted
        /// </summary>
        Type[] GenericArguments { get; set; }
        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        List<PropertyInfo> GetSupportedProperties();
    }
}