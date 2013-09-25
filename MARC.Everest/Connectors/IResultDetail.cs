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

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Identifies the type of send result detail
    /// </summary>
    public enum ResultDetailType
    {
        /// <summary>
        /// The detail represents a fatal error
        /// </summary>
        Error,
        /// <summary>
        /// The detail represents a warning
        /// </summary>
        Warning,
        /// <summary>
        /// The detail represents an informational message
        /// </summary>
        Information
    }

    /// <summary>
    /// Represents details about the send result
    /// </summary>
    /// <remarks>
    /// The IResultDetail interface provides mechanisms for retrieving errors from the 
    /// process of formatting an instance. 
    /// </remarks>
    /// <example lang="cs" title="Printing Error Details">
    /// <code lang="cs" title="Printing Error Details">
    /// // This example assumes instance is an IGraphable object
    /// // and that formatter is a formatter
    /// IStructureFormatter isf = (IStructureFormatter)formatter;
    /// IFormatterGraphResult result = isf.Graph(Console.OpenStandardOutput(), instance);
    /// foreach(IResultDetail dtl in result.Details)
    ///     Console.WriteLine("{0} at {1}", dtl.Message, dtl.Location);
    /// </code>
    /// </example>
    public interface IResultDetail
    {
        /// <summary>
        /// The type of detail this result detail line represents
        /// </summary>
        ResultDetailType Type { get; }
        /// <summary>
        /// The textual message of the result detail
        /// </summary>
        String Message { get; }
        /// <summary>
        /// The exception that may have caused this result detail to fail.
        /// </summary>
        Exception Exception { get; }
        /// <summary>
        /// Identifies the location of the error
        /// </summary>
        String Location { get; set; }
    }
}