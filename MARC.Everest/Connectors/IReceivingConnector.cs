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
using MARC.Everest.Interfaces;

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Represents a <see cref="T:MARC.Everest.Connectors.IConnector"/> that receives data from a remote endpoint
    /// </summary>
    /// <remarks>
    /// This type of connector is not to be confused with an <see cref="T:MARC.Everest.Connectors.IListenWaitConnector"/> 
    /// as it does not actively try to receive messages from an endpoint. Rather, it serves
    /// as a marker for the Send/Receive pattern.
    /// </remarks>
    /// <seealso cref="T:MARC.Everest.Connectors.ISendingConnector"/>
    public interface IReceivingConnector : IConnector
    {
        /// <summary>
        /// Receive data from the connector
        /// </summary>
        /// <returns>The data received from the connector interface</returns>
        IReceiveResult Receive();
        /// <summary>
        /// Start an asynchronous receive operation
        /// </summary>
        /// <param name="callback">The delegate to call when the data is fully received</param>
        /// <param name="state">An object representing the status of the receive operation</param>
        /// <returns>An IAsyncResult point that can be used to track the request</returns>
        IAsyncResult BeginReceive(AsyncCallback callback, object state);
        /// <summary>
        /// Receive the contents of an asynchronous receive operation
        /// </summary>
        /// <param name="result">A pointer to the BeingReceive return value</param>
        /// <returns>The data received from the connector interface</returns>
        IReceiveResult EndReceive(IAsyncResult result);
        /// <summary>
        /// True if the receive connector has new data waiting
        /// </summary>
        bool HasData { get; }
    }
}