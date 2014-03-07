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
 * User: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;

namespace MARC.Everest.Formatters.XML.ITS1
{
    /// <summary>
    /// Summary of DatatypeFormatter
    /// </summary>
    public interface ITypeFormatter
    {
        /// <summary>
        /// The host of the formatter
        /// </summary>
        XmlIts1Formatter Host { get; set; }
        /// <summary>
        /// Graph object <paramref name="o"/> to stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        void Graph(XmlWriter s, object o, IGraphable context, XmlIts1FormatterGraphResult resultContext);
        /// <summary>
        /// Parse object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse from</param>
        /// <returns>The parsed object</returns>
        /// <param name="currentInteractionType">The current interaction that is being rendered. This is used for interaction cases of traversal names</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        object Parse(XmlReader s, Type useType, Type currentInteractionType, XmlIts1FormatterParseResult resultContext);
        /// <summary>
        /// The type this formatter handles
        /// </summary>
        Type HandlesType { get; }
        /// <summary>
        /// Validate all mandatory elements are populated
        /// </summary>
        /// <param name="o">The object to check</param>
        /// <returns>True if the object is valid, false otherwise</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        IEnumerable<IResultDetail> Validate(IGraphable o, string location);
        /// <summary>
        /// Parse element content from the current position
        /// </summary>
        /// <remarks>This method is used to process the elements from the current position into the specified instance</remarks>
        object ParseElementContent(XmlReader r, Object instance, String terminationElement, int terminationDepth, Type interactionType, XmlIts1FormatterParseResult resultContext);

    }
}