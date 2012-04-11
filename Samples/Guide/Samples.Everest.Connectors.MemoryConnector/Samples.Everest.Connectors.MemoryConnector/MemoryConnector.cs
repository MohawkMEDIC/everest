/**
 * Copyright (c) 2008/2009, Mohawk College of Applied Arts and Technology
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that 
 * the following conditions are met:
 *
 *    * Redistributions of source code must retain the above copyright notice, this list of conditions and 
 *      the following disclaimer.
 *    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions 
 *      and the following disclaimer in the documentation and/or other materials provided with the distribution.
 *    * Neither the name of the Mohawk College of Applied Arts and Technology nor the names of its contributors 
 *      may be used to endorse or promote products derived from this software without specific prior written 
 *      permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED 
 * WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
 * PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR 
 * ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED 
 * TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;
using System.IO;
using System.Threading;
using MARC.Everest.Exceptions;
using MARC.Everest.Interfaces;

namespace Samples.Everest.Connectors.MemoryConnector
{
public class MemoryConnector : IConnector, IFormattedConnector, ISendingConnector
{
    #region Constructors

    /// <summary>
    /// Create a new instance of the memory connector
    /// </summary>
    public MemoryConnector() { }
    /// <summary>
    /// Create a new instance of the memory connector with the 
    /// specified connectionString
    /// </summary>
    /// <param name="connectionString">The connection string</param>
    public MemoryConnector(string connectionString) 
    {
        this.ConnectionString = connectionString;
    }

    #endregion

    #region IConnector Members

    /// <summary>
    /// Close the connection
    /// </summary>
    public void Close()
    {
        // No need to close memory
    }

    /// <summary>
    /// Get or set the connection string
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Returns true if the connection is open
    /// </summary>
    public bool IsOpen()
    {
        return true; // No need to check if memory is open
    }

    /// <summary>
    /// Open the connector
    /// </summary>
    public void Open()
    {
        // No need to open the memory stream
    }

    #endregion

    #region IFormattedConnector Members

    /// <summary>
    /// Get or set the formatter this connector is using
    /// </summary>
    public IStructureFormatter Formatter { get; set; }

    #endregion

    /// <summary>
    /// Good practice to have a worker class to do the 
    /// actual work
    /// </summary>
    private class Worker
    {
        /// <summary>
        /// The formatter to use
        /// </summary>
        public IStructureFormatter Formatter { get; set; }

        /// <summary>
        /// The result of the worker
        /// </summary>
        public SendResult Result { get; set; }

        /// <summary>
        /// The callback of the worker
        /// </summary>
        public event WaitCallback Completed;

        /// <summary>
        /// Perform the work
        /// </summary>
        /// <param name="state">A user state. In our case, the thing
        /// to be sent</param>
        public void Work(object state)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                Result = new SendResult();

                // Format and set result details
                var result = Formatter.Graph(ms, (IGraphable)state);
                Result.Code = result.Code;
                Result.Details = result.Details;
            }
            catch (Exception e)
            {
                // Append the error
                Result.Code = ResultCode.Error;
                Result.Details = new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) };
            }

            // Callback
            if (Completed != null) Completed(this);
        }
    }

    #region ISendingConnector Members

    /// <summary>
    /// The results
    /// </summary>
    private Dictionary<IAsyncResult, SendResult> results = new Dictionary<IAsyncResult, SendResult>();

    /// <summary>
    /// Asynchronous send operation
    /// </summary>
    /// <param name="data">The data being sent</param>
    /// <param name="callback">A callback to execute when the method is complete</param>
    /// <param name="state">A user state</param>
    /// <returns>An IAsyncResult representing the asynchronous operation</returns>
    public IAsyncResult BeginSend(MARC.Everest.Interfaces.IGraphable data, AsyncCallback callback, object state)
    {

        // Good practice to check if the connector is open
        if (!IsOpen())
            throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen, null);

        // Create a new worker
        Worker w = new Worker();

        // Always clone the current formatter
        w.Formatter = (IStructureFormatter)Formatter.Clone();

        // Create the async result
        IAsyncResult result = new SendResultAsyncResult(state, new AutoResetEvent(false));

        // Set the callback
        w.Completed +=  new WaitCallback(delegate(object sender)
        {
            Worker sWorker = sender as Worker; 

            // Set the result in the dictionary
            lock (this.results)
                this.results.Add(result, sWorker.Result);

            // Notify any listeners
            (result.AsyncWaitHandle as AutoResetEvent).Set();
            if (callback != null) callback(result);
        });

        // Execute
        ThreadPool.QueueUserWorkItem(w.Work, data);

        return result;
    }

    /// <summary>
    /// Complete the asynchronous send operation
    /// </summary>
    /// <param name="asyncResult">The asynchronous reference to retrieve the result for</param>
    /// <returns>The result of the send</returns>
    public ISendResult EndSend(IAsyncResult asyncResult)
    {
        SendResult result = null;
        if (results.TryGetValue(asyncResult, out result)) // There is a response
        {
            lock (this.results) // Remove the result
                this.results.Remove(asyncResult);

            return result; // return the result
        }
        else // Result is not available
            return null;
    }

    /// <summary>
    /// Synchronous send operation
    /// </summary>
    /// <param name="data">The data to send</param>
    /// <returns>The result of the send operation</returns>
    public ISendResult Send(MARC.Everest.Interfaces.IGraphable data)
    {
        // Good practice to check if the connector is open
        if (!IsOpen())
            throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen, null);

        // Create a new worker
        Worker w = new Worker();

        // Always clone the current formatter
        w.Formatter = (IStructureFormatter)Formatter.Clone();

        // Perform the work
        w.Work(data);

        return w.Result;

    }

    #endregion

    #region IDisposable Members

    /// <summary>
    /// Dispose the connector
    /// </summary>
    public void Dispose()
    {
        return;
    }

    #endregion
}
}
