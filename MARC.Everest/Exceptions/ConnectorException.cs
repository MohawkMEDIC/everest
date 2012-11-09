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
using MARC.Everest.Connectors;

namespace MARC.Everest.Exceptions
{
    /// <summary>
    /// Summary of ConnectorException
    /// </summary>
    /// <remarks>
    /// <para>
    /// The ConnectorException is thrown whenever an exception related to the sending of an instance to an
    /// endpoint fails due to an exceptional circumstance.
    /// </para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly")]
#if WINDOWS_PHONE
    public class ConnectorException : Exception
#else
    [Serializable]
    public class ConnectorException : ApplicationException
#endif
    {
        /// <summary>
        /// Reason why the exception is being thrown
        /// </summary>
        public enum ReasonType
        {
            /// <summary>
            /// Occurs when the connector's "Open" method has not been called prior to executing
            /// the action
            /// </summary>
            NotOpen,
            /// <summary>
            /// The connector could not send or receive data as the URI of the endpoint to receive on 
            /// is invalid
            /// </summary>
            InvalidUri,
            /// <summary>
            /// The connector could not send or receive data as there was an error with the message. This usually
            /// occurs when a soap fault is thrown
            /// </summary>
            MessageError, 
            /// <summary>
            /// The connector cannot format the message because no formatter has been assigned
            /// </summary>
            NullFormatter,
            /// <summary>
            /// The connector cannot be opened because the connection string is null
            /// </summary>
            NullConnectionString
        }

        /// <summary>
        /// Invalid state message
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STATE")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MSG")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "INVALID")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public const string MSG_INVALID_STATE = "This connector is not in a valid state that permits this operation";
        /// <summary>
        /// Formatter is null
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MSG")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FORMATTER")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public const string MSG_NULL_FORMATTER = "A formatter must be assigned to this formatter prior to performing this operation";
        /// <summary>
        /// Connection string is null
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STRING")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MSG")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CONNECTION")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public const string MSG_NULL_CONNECTION_STRING = "A connection string must be assigned to this formatter before performing this operation";

        /// <summary>
        /// Details of the operation
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public IResultDetail[] Details { get; set; }

        /// <summary>
        /// Reason for the exception
        /// </summary>
        public ReasonType Reason { get; set; }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public ConnectorException() : base() { }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ConnectorException(string message) : base(message) { }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ConnectorException(string message, Exception innerException) : base(message, innerException) { }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="reason"></param>
        public ConnectorException(string message, ReasonType reason) : this(message) { Reason = reason; }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="reason"></param>
        /// <param name="innerException"></param>
        public ConnectorException(string message, ReasonType reason, Exception innerException) : this(message, innerException) { Reason = reason; }

    }
}