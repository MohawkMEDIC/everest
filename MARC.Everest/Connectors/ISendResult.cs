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
    /// Result codes
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// The message was accepted for delivery
        /// </summary>
        Accepted, 
        /// <summary>
        /// The message was accepted however it did not pass conformance
        /// </summary>
        AcceptedNonConformant,
        /// <summary>
        /// The message was rejected for delivery due to validation failures
        /// in the formatter
        /// </summary>
        Rejected,
        /// <summary>
        /// An error occured while sending the message to the remote endpoint
        /// </summary>
        Error,
        /// <summary>
        /// The connection was not available
        /// </summary>
        NotAvailable,
        /// <summary>
        /// The Type was not available
        /// </summary>
        TypeNotAvailable
    }

    /// <summary>
    /// Represents a result from a sending connector
    /// </summary>
    /// <remarks>
    /// This interface permits connectors to pass contextual meta data about the send
    /// operation back to callers. For example, if a call failed, it would be possible
    /// for a sending connector to pass back an HTTP error code in a send result class.
    /// </remarks>
    public interface ISendResult
    {
        /// <summary>
        /// The result of the send operation
        /// </summary>
        ResultCode Code { get; }
        /// <summary>
        /// The details of how the result was attained.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        IEnumerable<IResultDetail> Details { get; }
    }
}