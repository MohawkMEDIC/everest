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
 * Author: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using MARC.Everest.Interfaces;

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Structured formatter that supports xml data
    /// </summary>
    /// <remarks>
    /// This interface appends the necessary functionality to call a formatter
    /// using <see cref="T:System.Xml.XmlWriter"/> and <see cref="T:System.Xml.XmlReader"/>
    /// instances. For the best results, it is strongly recommended that callers 
    /// of these functions use the <see cref="T:MARC.Everest.Xml.XmlStateWriter"/> and
    /// <see cref="T:MARC.Everest.Xml.XmlStateReader"/> classes rather than <see cref="T:System.Xml.XmlWriter"/>
    /// and <see cref="T:System.Xml.XmlReader"/> classes respectively.
    /// </remarks>
    public interface IXmlStructureFormatter : IStructureFormatter
    {


        /// <summary>
        /// Graph object <paramref name="o"/> onto XML Writer <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream (XML Writer) to graph the object</param>
        /// <param name="o">The object to be graphed</param>
        /// <returns>A structure containing the results of the graph operation</returns>
        IFormatterGraphResult Graph(XmlWriter s, IGraphable o);

        /// <summary>
        /// Parse an object from an XMLReader
        /// </summary>
        /// <param name="r">The reader to read from</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "r")]
        IFormatterParseResult Parse(XmlReader r);

        /// <summary>
        /// Parse an object from <paramref name="r"/> using type <paramref name="t"/>
        /// </summary>
        /// <param name="r">The reader to read from</param>
        /// <param name="t">The type to use for formatting</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "r")]
        IFormatterParseResult Parse(XmlReader r, Type t);

    }
}