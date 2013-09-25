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
    /// The IConnector interface responsible for defining connector interfaces to/from external systems.
    /// </summary>
    /// <remarks>
    /// IConnectors follow one of the following patterns:
    /// <list type="table">
    ///     <listheader>
    ///         <term>Pattern</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><see cref="T:MARC.Everest.Connectors.IListenWaitConnector"/></term>    
    ///         <description>Represents a connector that can actively listen for messages
    ///         from solicitors and processes the message. No assumption is made that the 
    ///         connector can (or should) send a response. An example of a Listen/Wait connector
    ///         is a Queue listener</description>
    ///         
    ///     </item>
    ///     <item>
    ///         <term><see cref="T:MARC.Everest.Connectors.IListenWaitRespondConnector"/></term>    
    ///         <description>Represents a connector that can actively listen for messages
    ///         from solicitors, process the message and send a response to the solicitor. 
    ///         An example of a Listen/Wait/Respond connector is an Http connector</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="T:MARC.Everest.Connectors.ISendingConnector"/></term>    
    ///         <description>Represents a connector that can broadcast message instances to
    ///         the destination. This connector provides no facility to solicit (ie: get a response)
    ///         from the destination</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="T:MARC.Everest.Connectors.ISendReceiveConnector"/></term>    
    ///         <description>Represents a connector that can solicit data from the remote 
    ///         endpoint. This connector provides mechanisms for sending, waiting and receiving
    ///         messages in asynchronous and synchronous modes</description>
    ///     </item>
    /// </list>
    /// </remarks>
    public interface IConnector : IDisposable
    {
        /// <summary>
        /// Gets or sets the connection string property.
        /// </summary>
        string ConnectionString { get; set; }
        /// <summary>
        /// Opens or validates a connection with the remote system as defined by <paramref name="connectionString"/>. 
        /// </summary>
        /// <remarks>
        /// Note that in some connector infrastructures this will merely verify the connection can be opened, and 
        /// the actual opening of the connection may be withheld until a send or receive is executed.
        /// </remarks>
        void Open();
        /// <summary>
        /// Closes any active connection with the remote system
        /// </summary>
        void Close();
        /// <summary>
        /// Returns true if a channel is currently opened
        /// </summary>
        bool IsOpen();
    }
}