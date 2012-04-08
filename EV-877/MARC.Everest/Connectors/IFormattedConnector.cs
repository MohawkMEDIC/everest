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
 * Author: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Represents a <see cref="T:MARC.Everest.Connectors.IConnector"/> that sends or receives data "over the wire" using a 
    /// <see cref="T:MARC.Everest.Connectors.IStructureFormatter"/> for serialization 
    /// of the message.
    /// </summary>
    /// <remarks>
    /// <para>This interface is implemented by connectors that exchange data with external systems
    /// using HL7v3. If a connector implementation does not need to send data using V3 (ie: does not 
    /// need a v3 Formatter instance) then it will most likely not need to implement this interface</para>
    /// </remarks>
    /// <example>
    /// <code lang="cs">
    /// <![CDATA[
    /// // Create the formatter
    ///XmlIts1Formatter itsFormatter = new XmlIts1Formatter();
    ///itsFormatter.ValidateConformance = false;
    ///itsFormatter.GraphAides.Add(new DatatypeFormatter()
    ///    {
    ///        ValidateConformance = false,
    ///        CompatibilityMode = DatatypeFormatterCompatibilityMode.Universal
    ///    }
    ///);
    ///
    /// // Setup connector
    /// IFormattedConnector connector = new WcfClientConnector();
    /// connector.Formatter = itsFormatter;
    /// ]]>
    /// </code>
    /// </example>
    public interface IFormattedConnector : IConnector
    {
        /// <summary>
        /// Get or set the formatter this connector uses to format the message.
        /// </summary>
        IStructureFormatter Formatter { get; set; }
        
    }
}
