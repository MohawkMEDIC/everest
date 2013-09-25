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
using MARC.Everest.Interfaces;

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Represents a <see cref="T:MARC.Everest.Connectors.IListenWait"/> connector that can send responses back to the
    /// solicitor
    /// </summary>
    public interface IListenWaitRespondConnector : IListenWaitConnector
    {
        /// <summary>
        /// Send <paramref name="data"/> to the remote endpoint
        /// </summary>
        /// <param name="data">The data to send to the remote endpoint</param>
        /// <param name="correlate">The result of the receive function that this response should be correlated with</param>
        /// <returns>Response code from the sending handler</returns>
        ISendResult Send(IGraphable data, IReceiveResult correlate);
        /// <summary>
        /// Starts an asynchronous send of the data to the remote endpoint
        /// </summary>
        /// <param name="data">The data to send</param>
        /// <param name="callback">The delegate to call when the message is confirmed sent</param>
        /// <param name="state">An object representing the state of the request</param>
        /// <param name="correlate">The result of the receive function that this response should be correlated with</param>
        /// <returns>A callback information class</returns>
        IAsyncResult BeginSend(IGraphable data, IReceiveResult correlate, AsyncCallback callback, object state);
        /// <summary>
        /// Get the send result of an asynchronous call
        /// </summary>
        /// <param name="asyncResult">A pointer to the async result returned from the begin send method</param>
        /// <returns>Response code from the sending handler</returns>
        ISendResult EndSend(IAsyncResult asyncResult);
        /// <summary>
        /// Fires if the response to a message did not pass conformance. This allows the user to suggest a new 
        /// structure to send in place of the invalid one
        /// </summary>
        event EventHandler<MessageEventArgs> InvalidResponse;
        
    }
}