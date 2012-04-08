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
    /// Represents a <see cref="T:MARC.Everest.Connectors.ISendingConnector"/> that sends and receives messages over 
    /// a transport channel
    /// </summary>
    /// <seealso cref="T:MARC.Everest.Connectors.ISendingConnector"/>
    /// <seealso cref="T:MARC.Everest.Connectors.IReceivingConnector"/>
    public interface ISendReceiveConnector : ISendingConnector
    {
        /// <summary>
        /// Receive the response to a message that has been sent
        /// </summary>
        /// <param name="correlate">The <see cref="ISendResult"/> that represents the response from the send method</param>
        /// <returns>A receive result</returns>
        /// <example>
        /// <code lang="cs">
        /// <![CDATA[
        /// <summary>
        /// Sends <paramref name="request"/> via
        /// <paramref name="target"/>, waits for a 
        /// response and returns the result.
        /// </summary>
        /// <returns>The response message</returns>
        ///public static IGraphable SendAndWait(IGraphable request, ISendReceiveConnector target)
        ///{
        ///    if (!target.IsOpen())
        ///        target.Open();
        ///
        ///    // Send a request
        ///    ISendResult sendResult = target.Send(request);
        ///
        ///    // If the send was successful then block and wait the response
        ///    if (sendResult.Code == ResultCode.Accepted ||
        ///        sendResult.Code == ResultCode.AcceptedNonConformant)
        ///    {
        ///        // Block and wait for response
        ///        IReceiveResult receiveResult = target.Receive(sendResult);
        ///
        ///        // If the receive result was successful return the structure
        ///        if (receiveResult.Code == ResultCode.Accepted ||
        ///            receiveResult.Code == ResultCode.AcceptedNonConformant)
        ///            return receiveResult.Structure;
        ///    }
        ///    else
        ///        Console.WriteLine("There was a problem sending the message");
        ///    return null;
        ///}
        /// ]]>
        /// </code>
        /// </example>
        IReceiveResult Receive(ISendResult correlate);
        /// <summary>
        /// Start the receive operation
        /// </summary>
        /// <param name="correlate">The <see cref="ISendResult"/> that represents the response from the send operation</param>
        /// <param name="callback">A callback delegate that is executed with the receive operation completes</param>
        /// <param name="state">A user state</param>
        /// <returns>An <see cref="IAsyncResult"/> handle</returns>
        /// <example>
        /// <code lang="cs">
        /// <![CDATA[
        /// response and returns the result.
        /// </summary>
        /// <returns>The response message</returns>
        ///public static IGraphable SendAndWait(IGraphable request, ISendReceiveConnector target)
        ///{
        ///    if (!target.IsOpen())
        ///        target.Open();
        ///
        ///    // Send a request
        ///    IAsyncResult asyncResult = target.BeginSend(request, null, null);
        ///
        ///    // Wait for the message to be serialized and sent
        ///    while (!asyncResult.IsCompleted)
        ///        Console.WriteLine("Formatting and Sending...");
        ///
        ///    ISendResult sendResult = target.EndSend(asyncResult);
        ///
        ///    // If the send was successful then block and wait the response
        ///    if (sendResult.Code == ResultCode.Accepted ||
        ///        sendResult.Code == ResultCode.AcceptedNonConformant)
        ///    {
        ///        // Begin the receive operation
        ///        asyncResult = target.BeginReceive(sendResult, null, null);
        ///
        ///        // Wait for the message to be received and parsed
        ///        while (!asyncResult.IsCompleted)
        ///            Console.WriteLine("Receiving...");
        ///
        ///        // Get the receive result
        ///        IReceiveResult receiveResult = target.EndReceive(asyncResult);
        ///
        ///        // If the receive result was successful return the structure
        ///        if (receiveResult.Code == ResultCode.Accepted ||
        ///            receiveResult.Code == ResultCode.AcceptedNonConformant)
        ///            return receiveResult.Structure;
        ///    }
        ///    else
        ///        Console.WriteLine("There was a problem sending the message");
        ///    return null;
        ///}
        /// ]]>
        /// </code>
        /// </example>
        /// <seealso cref="M:EndReceive(IAsyncResult asyncResult)"/>
        IAsyncResult BeginReceive(ISendResult correlate, AsyncCallback callback, object state);
        /// <summary>
        /// End the receive operation and retrieve the result
        /// </summary>
        /// <param name="asyncResult">The result of the <see cref="BeginReceive"/> method</param>
        /// <returns>A receive result</returns>
        /// <seealso cref="M:BeginReceive(ISendResult correlate, AsyncCallback callback, object state)"/>
        IReceiveResult EndReceive(IAsyncResult asyncResult);
    }
}