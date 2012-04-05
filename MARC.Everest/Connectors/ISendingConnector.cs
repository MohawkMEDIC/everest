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
    /// Represents a <see cref="T:MARC.Everest.Connectors.IConnector"/> that can send data to a remote system
    /// </summary>
    /// <seealso cref="T:MARC.Everest.Connectors.IReceivingConnector"/>
    public interface ISendingConnector : IConnector
    {
        /// <summary>
        /// Send <paramref name="data"/> to the remote endpoint
        /// </summary>
        /// <param name="data">The data to send to the remote endpoint</param>
        /// <returns>Response code from the sending handler</returns>
        /// <example>
        /// <code lang="cs">
        /// <![CDATA[
        /// public static void Publish(IGraphable instance, ISendingConnector target)
        ///{
        ///    // Check target is open
        ///    if (!target.IsOpen())
        ///        target.Open();
        ///
        ///    ISendResult result = target.Send(instance);
        ///    // Verify data was sent
        ///    if (result.Code != ResultCode.Accepted &&
        ///        result.Code != ResultCode.AcceptedNonConformant)
        ///        Console.WriteLine("There was a problem sending the message");
        ///}
        ///
        /// ]]>
        /// </code>
        /// </example>
        ISendResult Send(IGraphable data);
        /// <summary>
        /// Starts an asynchronous send of the data to the remote endpoint
        /// </summary>
        /// <param name="data">The data to send</param>
        /// <param name="callback">The delegate to call when the message is confirmed sent</param>
        /// <param name="state">An object representing the state of the request</param>
        /// <returns>A callback information class</returns>
        /// <example>
        /// <code lang="cs">
        /// <![CDATA[
        ///public static void Publish(IGraphable instance, ISendingConnector target)
        ///{
        ///    // Check target is open
        ///    if (!target.IsOpen())
        ///        target.Open();
        ///
        ///    IAsyncResult asyncResult = target.BeginSend(instance, null, null);
        ///
        ///    // Print "Doing something else" while the formatter formats
        ///    while (!asyncResult.IsCompleted)
        ///        Console.WriteLine("Doing something else....");
        ///
        ///    // Get the send result
        ///    ISendResult result = target.EndSend(asyncResult);
        ///
        ///    // Verify data was sent
        ///    if (result.Code != ResultCode.Accepted &&
        ///        result.Code != ResultCode.AcceptedNonConformant)
        ///        Console.WriteLine("There was a problem sending the message");
        ///}
        /// ]]>
        /// </code>
        /// </example>
        /// <seealso cref="M:EndSend(IAsyncResult asyncResult)"/>
        IAsyncResult BeginSend(IGraphable data, AsyncCallback callback, object state);
        /// <summary>
        /// Get the send result of an asynchronous call
        /// </summary>
        /// <param name="asyncResult">A pointer to the async result returned from the begin send method</param>
        /// <returns>Response code from the sending handler</returns>
        /// <seealso cref="M:BeginSend(IGraphable data, AsyncCallback callback, object state)"/>
        ISendResult EndSend(IAsyncResult asyncResult);

    }
}