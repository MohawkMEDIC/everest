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
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using MARC.Everest.Exceptions;
using System.IO;
using System.Xml;
using MARC.Everest.Interfaces;
using MARC.Everest.Connectors.WCF.Serialization;
using MARC.Everest.Connectors.WCF.Core;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;

namespace MARC.Everest.Connectors.WCF
{
    /// <summary>
    /// The WcfServerConnector provides a gateway to the Windows Communications Foundataion (WCF) channels.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class allows developers to easily host in-process WCF services. It permits receive and 
    /// recevie/respond channel patterns. 
    /// </para>
    /// <para>
    /// This connector requires that a service be defined in the applications app.config file. The connection
    /// string ServiceName should be used to "link" the app.config defined service with the connector. For
    /// the best performance, it is recommended that the developer use the Begin/EndReceive.
    /// </para>
    /// <para>
    /// This class implements the <see cref="T:MARC.Everest.Connectors.IListenWaitRespondConnector"/> interface. This
    /// means that on a receive/respond channel (ie: net.tcp, or ws.http) a response MUST BE SENT back to the WCF system. If 
    /// not, the thread will wait for a response for up to five minutes. See the example for details on how to properly 
    /// respond to a message.
    /// </para>
    /// </remarks>
    /// <example>
    /// A simple "echo" service:
    /// <code lang="cs">
    /// public void Main()
    /// {
    ///     WcfServerConnector conn = new WcfServerConnector("ServiceName=ApplicationService");
    ///     conn.Formatter = new MARC.Everest.Formatters.XML.ITS1.Formatter();
    ///     conn.Formatter.GraphAides.Add(new MARC.Everest.Formatters.XML.ITS1.
    ///     // NB: The service ApplicationService must appear in the app.config
    ///     conn.Open();
    ///     conn.Start();
    ///     
    ///     // Message available is fired when a new message is received from the server
    ///     conn.MessageAvailable += new EventHandler&lt;UnsolicitedDataEventArgs&gt;(message_available);
    ///     
    ///     // Can't guarantee that the outgoing message is conformant, the InvalidResponse event
    ///     // is fired when the connector tries to send an invalid message back to the requestor
    ///     conn.InvalidResponse += new EventHandler&lt;MessageEventArgs&gt;(invalid_response);
    ///     
    ///     Console.WriteLine("Server is listening... Press any key to stop");
    ///     Console.ReadKey();
    ///     conn.Close();
    /// }
    /// 
    /// // Invalid response handler
    /// public void invalid_response(object sender, MessageEventArgs evt)
    /// {
    ///     Console.WriteLine("Tried to echo back a non-conformant message...");
    ///     evt.Alternate = CreateAlternativeMessage();
    /// }
    /// 
    /// // Create a valid message
    /// public IGraphable CreateAlternativeMessage()
    /// {
    ///    // Not relevant to example
    /// }
    /// 
    /// // Message available handler
    /// public void message_available(object sender, UnsolicitedDataEventArgs evt)
    /// {
    ///     // Process the message
    ///     IAsyncResult iar = conn.BeginReceive(null, null);
    ///     iar.AsyncWaitHandle.WaitOne();
    ///     WcfReceiveResult rcvResult = (WcfReceiveResult)conn.EndReceive(iar);
    ///     Console.WriteLine("Received a message...");
    ///     
    ///     // Send a response
    ///     iar = conn.BeginSend(rcvResult.Result, rcvResult, null, null);
    ///     iar.AsyncWaitHandle.WaitOne();
    ///     WcfSendResult sndResult = (WcfSendResult)conn.EndSend(iar);
    ///     Console.WriteLine("Sent response...");
    /// }
    /// </code>
    /// </example>
    [Description("WCF Server Connector")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class WcfServerConnector : IListenWaitRespondConnector, IFormattedConnector, IDisposable
    {

        /// <summary>
        /// Creates a new instance of the WcfServerConnector.
        /// </summary>
        public WcfServerConnector() { }
        /// <summary>
        /// Creates a new instance of the WcfServerConnector with the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string that dictates the connection (<see cref="ConnectionString"/>)</param>
        public WcfServerConnector(string connectionString) { this.ConnectionString = connectionString; }

        /// <summary>
        /// Worker class.
        /// </summary>
        private class Worker
        {

            /// <summary>
            /// The message id.
            /// </summary>
            public Guid MessageId;
            /// <summary>
            /// Gets or sets the formatter to use.
            /// </summary>
            public IXmlStructureFormatter Formatter { get; set; }
            /// <summary>
            /// Occurs when the operation is complete.
            /// </summary>
            public event WaitCallback Completed;
            /// <summary>
            /// Gets or sets the result from parsing.
            /// </summary>
            public WcfSendResult SendResult { get; set; }
            /// <summary>
            /// Gets or sets the result of the receive operation.
            /// </summary>
            public WcfReceiveResult ReceiveResult { get; set; }
            /// <summary>
            /// Gets or sets the message version.
            /// </summary>
            public MessageVersion MessageVersion { get; set; }
            /// <summary>
            /// Gets or sets the Message that is processed.
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public Message MessageResult { get; set; }
            /// <summary>
            /// Occurs when an invalid response is received. This gives a handler the ability to correct the message.
            /// </summary>
            internal EventHandler<MessageEventArgs> InvalidResponse;
            /// <summary>
            /// Headers to include in the result
            /// </summary>
            public MessageHeaders ResponseHeaders { get; set; }

            /// <summary>
            /// Performs a send operation.
            /// </summary>
            /// <param name="state">User state.</param>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
            public void WorkSend(object state)
            {
                // Prepare stream
                WcfSendResult result = new WcfSendResult();
                IGraphable data = (IGraphable)state;

                // Create the surrogate
                XmlSerializerSurrogate surrogate = new XmlSerializerSurrogate(Formatter); 
                
                try
                {
                    // Graph the object
                    result.Message = Message.CreateMessage(MessageVersion, "", data, surrogate);
                    result.MessageId = this.MessageId;
                    result.Headers = this.ResponseHeaders;

                    // Validate
                    surrogate.WriteObject(new MemoryStream(), data);
                    result.Code = surrogate.ResultCode;
                    result.Details = surrogate.Details;

                    if ((result.Code != ResultCode.Accepted && result.Code != ResultCode.AcceptedNonConformant)
                        && InvalidResponse != null)
                    {
                        MessageEventArgs mea = new MessageEventArgs(result.Code, result.Details);
                        InvalidResponse(this, mea);
                        InvalidResponse = null; // Don't call retry again! NOTE: This is a single use class.
                        if (mea.Alternate != null)
                        { // An alternate was suggested
                            if (Formatter is IValidatingStructureFormatter) // Turn of validation for fallback
                                (Formatter as IValidatingStructureFormatter).ValidateConformance = false;
                            WorkSend(mea.Alternate);

                        }
                        return;
                    }

                    // Did the operation succeed?
                    if (result.Code != ResultCode.Accepted && result.Code != ResultCode.AcceptedNonConformant)
                    {
                        result.Details = surrogate.Details;
                        result.Message = null;
                    }
                }
                catch (MessageValidationException e)
                {
                    result.Code = ResultCode.Rejected;
                    List<IResultDetail> dtl = new List<IResultDetail>(new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) });
                    dtl.AddRange(surrogate.Details ?? new IResultDetail[0]);
                    result.Details = dtl.ToArray();
                }
                catch (FormatException e)
                {
                    result.Code = ResultCode.Rejected;
                    result.Details = new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) };
                }
                catch (Exception e)
                {
                    result.Code = ResultCode.Error;
                    result.Details = new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) };
                }
                finally
                {
                }

                // Set the result
                this.SendResult = result;

                // Fire completed event
                if (Completed != null) Completed(this);
            }

            /// <summary>
            /// Worker method that receives and deserializes the data
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
            public void WorkReceive(object state)
            {
                WcfReceiveResult result = new WcfReceiveResult();

                // Create the surrogate
                XmlSerializerSurrogate surrogate = new XmlSerializerSurrogate(Formatter);
                try
                {


                    // Parse the object
                    result.MessageIdentifier = MessageId;
                    
                    result.MessageVersion = (state as Message).Version;
                    result.Structure = (state as Message).GetBody<IGraphable>(surrogate);
                    result.Headers = (state as Message).Headers;
                    result.Details = surrogate.Details;
                    result.Code = ResultCode.Accepted;
                    if (Array.Find<IResultDetail>(result.Details, o => o.Type == ResultDetailType.Error) != null)
                        result.Code = ResultCode.AcceptedNonConformant;
                    else if (result.Structure == null)
                        result.Code = ResultCode.TypeNotAvailable;
                }
                catch (MessageValidationException e)
                {
                    result.Code = ResultCode.Rejected;
                    List<IResultDetail> dtl = new List<IResultDetail>(new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) });
                    dtl.AddRange(surrogate.Details ?? new IResultDetail[0]);
                    result.Details = dtl.ToArray();
                }
                catch (FormatException e)
                {
                    result.Code = ResultCode.Rejected;
                    result.Details = new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) };
                }
                catch (Exception e)
                {
                    result.Code = ResultCode.Error;
                    result.Details = new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) };
                }


                // Set the result
                this.ReceiveResult = result;

                // Fire completed event
                if (Completed != null) Completed(this);
            }

        }

        /// <summary>
        /// The service host for this connector
        /// </summary>
        private WcfServiceHost svcHost;
        /// <summary>
        /// The name of the service to host
        /// </summary>
        private string serviceName;
        /// <summary>
        /// Waiting messages
        /// </summary>
        Queue<KeyValuePair<Message, Guid>> waitingMessages = new Queue<KeyValuePair<Message, Guid>>();
        /// <summary>
        /// Waiting results
        /// </summary>
        Dictionary<Guid, WaitHandle> waitHandles = new Dictionary<Guid, WaitHandle>();
        /// <summary>
        /// Results for messages
        /// </summary>
        Dictionary<Guid, WcfSendResult> results = new Dictionary<Guid, WcfSendResult>();

        #region WCF Communications

        /// <summary>
        /// Begin a notification of message received, returns a correlation.
        /// </summary>
        /// <returns>A guid that represents the unique identifier for the message in the receiver.</returns>
        internal WcfSendResult ProcessMessage(Message m)
        {

            Guid rGuid = Guid.NewGuid(); // Unique identifier
            #if DEBUG
            Trace.TraceInformation("Enqueuing message...");
            #endif 

            // Start notification
            lock (waitingMessages)
                waitingMessages.Enqueue(new KeyValuePair<Message, Guid>(m, rGuid));

            // Publish a wait handle
            WaitHandle wait = new AutoResetEvent(false);
            lock (waitHandles)
                waitHandles.Add(rGuid, wait);

            // Create unsolicited data event args
            var rep = (OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty);

            // JF - To handle IPv6 processing
            Uri uriFrom = m.Headers.From != null ? m.Headers.From.Uri : null;
            if(uriFrom == null)
            {
                // Attempt to do reverse lookup
                try
                {
                    var address = IPAddress.Parse(rep.Address);
                    uriFrom = new Uri(String.Format("http://{0}:{1}", rep.Address, rep.Port));
                }
                catch 
                { 
                    #if DEBUG    
                    Trace.TraceInformation("Performance: Exception while resolving host name");
                    #endif

                    uriFrom = new Uri("http://anonymous"); 
                }
            }
            
            UnsolicitedDataEventArgs usdea = new UnsolicitedDataEventArgs(m.Headers.To, DateTime.Now, uriFrom);

            
            // Message is available
            if (MessageAvailable != null)
            {
                #if DEBUG
                Trace.TraceInformation("Raising MessageAvailable event");
                #endif
                MessageAvailable(this, usdea); // Start to invoke on another thread... we don't care what the result it
            }

            wait.WaitOne(new TimeSpan(0, 1, 0)); // TODO: How to reliably detect no one is going to send a result

            // Get the result
            WcfSendResult retVal;
            if (results.TryGetValue(rGuid, out retVal))
            {
                lock (results)
                    results.Remove(rGuid);

                return retVal;
            }
            //
            //return null;
            throw new InvalidAsynchronousStateException(
                String.Format("Response message for msg '{0}' recevied by WcfServerConnector, but couldn't be found in the results", rGuid));
        }

        /// <summary>
        /// Get the result of a StartProcessMessage operation.
        /// </summary>
        /// <param name="messageId">The unqiue id of the message.</param>
        internal void PublishResult(Guid messageId)
        {
            WaitHandle resetEvent;
            if (this.waitHandles.TryGetValue(messageId, out resetEvent))
            {
                (resetEvent as AutoResetEvent).Set(); // Notify listener
                lock (waitHandles)
                    waitHandles.Remove(messageId);
            }
        }

        //TODO: Should this be removed? - Should be commented as to why it is not currently active if left in

        ///// <summary>
        ///// End processing of message
        ///// </summary>
        //internal WcfSendResult EndProcessMessage(Guid result)
        //{
        //    WcfSendResult retVal;
        //    if (results.TryGetValue(result, out retVal))
        //    {
        //        lock(results)
        //            results.Remove(result);
        //        return retVal;
        //    }

        //    return null;
        //}
        #endregion

        #region IListenWaitConnector Members

        /// <summary>
        /// Starts the service host and initializes the listen process.
        /// </summary>
        public void Start()
        {
            if (!IsOpen())
                throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen);
            svcHost.Open();
            
        }

        /// <summary>
        /// Stops the running service host and tears down the listening process.
        /// </summary>
        public void Stop()
        {
            if (IsOpen())
                svcHost.Close();
        }

        /// <summary>
        /// Fired when a new message is available on the listen connector interface.
        /// </summary>
        public event EventHandler<UnsolicitedDataEventArgs> MessageAvailable;

        #endregion

        #region IReceivingConnector Members

        /// <summary>
        /// Synchronously receives a message from the connector interface.
        /// </summary>
        /// <returns>An <see cref="IReceiveResult"/> object with the results of the receive operation. If an error occured, this value is null.</returns>
        public IReceiveResult Receive()
        {

            // Formatter check
            if (!IsOpen())
                throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen);

            // Create the work that will perform the operations
            Worker w = new Worker();
            w.Formatter = (IXmlStructureFormatter)Formatter;

            // If there is no data in the waiting data queue, block
            while (waitingMessages.Count == 0)
                Thread.Sleep(100);

            KeyValuePair<Message, Guid> state;
            lock (waitingMessages) { state = waitingMessages.Dequeue(); }
            w.MessageId = state.Value;
            w.WorkReceive(state.Key);

            // Return the result
            return w.ReceiveResult;
        }

        // Async results
        private Dictionary<IAsyncResult, object> asyncResults = new Dictionary<IAsyncResult, object>();

        /// <summary>
        /// Start an asynchronous receive operation.
        /// </summary>
        public IAsyncResult BeginReceive(AsyncCallback callback, object state)
        {

            // Formatter check
            if (!IsOpen())
                throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen);

            if (waitingMessages.Count == 0) // Can't start as no messages exist!
                return null;

            // Setup wroker
            Worker w = new Worker();

            // Create a new instance of the formatter
            w.Formatter = (IXmlStructureFormatter)Formatter;

            // Set async result
            IAsyncResult Result = new ReceiveResultAsyncResult(state, new AutoResetEvent(false));

            // Completed delegate
            w.Completed += delegate(object Sender)
            {
                Worker sWorker = Sender as Worker; // Strong type sender
                // Lookup the result in the dictionary
                if (!asyncResults.ContainsKey(Result))
                    lock (asyncResults) { asyncResults.Add(Result, sWorker.ReceiveResult); }
                (Result as ReceiveResultAsyncResult).SetComplete(); // Set completed
                (Result.AsyncWaitHandle as AutoResetEvent).Set(); // send signal
                if (callback != null) callback(Result); // callback
            };

            // Add to thread pool
            KeyValuePair<Message, Guid> mData;
            lock (waitingMessages) { mData = waitingMessages.Dequeue(); }
            w.MessageId = mData.Value;
            ThreadPool.QueueUserWorkItem(new WaitCallback(w.WorkReceive), mData.Key);

            return Result;

        }

        /// <summary>
        /// Returns a result from an asynchronous receive operation.
        /// </summary>
        /// <param name="result">The <see cref="System.IAsyncResult" /> that was attained from the <seealso cref="BeginReceive"/> operation.</param>
        /// <returns>The result of the receive.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public IReceiveResult EndReceive(IAsyncResult result)
        {
            //TODO: This should block until the result is avialable (Need a check)
            object r;
            if (!asyncResults.TryGetValue(result, out r) || !(r is WcfReceiveResult))
                return null;
            else
            {
                // Remove the result
                lock (asyncResults)
                    asyncResults.Remove(result);
                return r as WcfReceiveResult;
            }
        }

        /// <summary>
        /// Gets a value indicating if this connector has data that can be consumed.
        /// </summary>
        public bool HasData
        {
            get { return waitingMessages.Count > 0; }
        }

        #endregion

        #region IConnector Members

        /// <summary>
        /// Gets or sets the connection string. 
        /// </summary>
        /// <remarks>
        // <list type="table">
        /// <listheader><term>Key</term><description>Description</description></listheader>
        /// <item><term>ServiceName</term>The name of the service to use</item>
        /// <item><term>EndpointAddress</term>The address of the listener</item>
        /// <item><term>BindingType</term>The type of binding</item>
        /// <item><term>BindingContract</term>The binding contract</item>
        /// </list>
        /// </remarks>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Prepares the parameters for this connector and ensures that resources exist so the <seealso cref="Start"/> operation
        /// succeeds.
        /// </summary>
        public void Open()
        {

            // Connection string
            if (ConnectionString == null)
                throw new ConnectorException(ConnectorException.MSG_NULL_CONNECTION_STRING, ConnectorException.ReasonType.NullConnectionString);
            else if (Formatter == null)
                throw new ConnectorException(ConnectorException.MSG_NULL_FORMATTER, ConnectorException.ReasonType.NullFormatter);

            Dictionary<string, List<string>> conf = ConnectionStringParser.ParseConnectionString(ConnectionString);

            string epAddress = string.Empty, epBinding = string.Empty, epContract = string.Empty,
                epBindingConfig = string.Empty;

            // Load 
            List<string> tmpValues;
            if (conf.TryGetValue("servicename", out tmpValues))
                serviceName = tmpValues[0];
            if (conf.TryGetValue("endpointaddress", out tmpValues))
                epAddress = tmpValues[0];
            if (conf.TryGetValue("bindingtype", out tmpValues))
                epBinding = tmpValues[0];
            if (conf.TryGetValue("bindingconfig", out tmpValues))
                epBindingConfig = tmpValues[0];

            epContract = typeof(IConnectorContract).FullName;

            // Service host
            svcHost = null;

            if (String.IsNullOrEmpty(epAddress))
            {
                svcHost = new WcfServiceHost(serviceName, typeof(ConnectorService));
            }
            else
            {
                svcHost = new WcfServiceHost(null, typeof(ConnectorService));
                if (!String.IsNullOrEmpty(epBindingConfig))
                    svcHost.AddServiceEndpoint(
                        epContract,
                        epBinding == "basicHttpBinding" ? (Binding)(new BasicHttpBinding(epBindingConfig)) : epBinding == "wsHttpBinding" ? (Binding)(new WSHttpBinding(epBindingConfig)) : null,
                        epAddress
                    );
                else
                    svcHost.AddServiceEndpoint(
                        epContract,
                        epBinding == "basicHttpBinding" ? (Binding)(new BasicHttpBinding()) : epBinding == "wsHttpBinding" ? (Binding)(new WSHttpBinding()) : null,
                        epAddress
                    );
            }
            
            svcHost.ConnectorHost = this;
        }

        /// <summary>
        /// Close the WCF connector.
        /// </summary>
        public void Close()
        {
            Stop();
            svcHost = null;
        }

        /// <summary>
        /// Gets a value indicating if the Open() method has been called.
        /// </summary>
        public bool IsOpen()
        {
            return svcHost != null;
        }

        #endregion

        #region IFormattedConnector Members

        private IXmlStructureFormatter formatter;

        /// <summary>
        /// Gets or sets the formatter this connector should use.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public IStructureFormatter Formatter
        {
            get
            { return formatter; }
            set
            {
                if (!(value is IXmlStructureFormatter))
                    throw new ArgumentException("Formatter must be set to an IXmlStructureFormatter!");
                formatter = (IXmlStructureFormatter)value;
            }
        }

        #endregion

        #region IListenWaitResponseConnector Members

        /// <summary>
        /// Send a response back to the remote endpoint.
        /// </summary>
        /// <param name="data">The data to send back.</param>
        /// <param name="correlate">The result to correlate this response with.</param>
        /// <returns>A send result structure that details the result of the send operation.</returns>
        public ISendResult Send(MARC.Everest.Interfaces.IGraphable data, IReceiveResult correlate)
        {

            if (!IsOpen())
                throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen);

            Worker w = new Worker();
            var cresult = correlate as WcfReceiveResult;
            w.MessageId = cresult.MessageIdentifier;
            w.MessageVersion = cresult.MessageVersion; // Prepare
            w.Formatter = (IXmlStructureFormatter)Formatter;
            w.ResponseHeaders = cresult.ResponseHeaders;
            w.InvalidResponse = InvalidResponse;

            w.WorkSend(data); // Work

            // Publish so WCF connector can send a result
            lock (results)
                results.Add(w.MessageId, w.SendResult);

            // Notify
            PublishResult(w.MessageId);

            // Return result
            return w.SendResult;
        }

        /// <summary>
        /// Start an asynchronous send operation.
        /// </summary>
        /// <param name="data">The data to send to the remote endpoint.</param>
        /// <param name="correlate">The result to correlate this response with.</param>
        /// <param name="callback">A callback handle that can be used to execute code when the operation completes.</param>
        /// <param name="state">A user state.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public IAsyncResult BeginSend(MARC.Everest.Interfaces.IGraphable data, IReceiveResult correlate, AsyncCallback callback, object state)
        {
            // Formatter check
            if (!IsOpen())
                throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen);

            // Setup wroker
            Worker w = new Worker();

            // Create a new instance of the formatter
            w.Formatter = (IXmlStructureFormatter)Formatter;
            var cresult = correlate as WcfReceiveResult;
            w.MessageId = (correlate as WcfReceiveResult).MessageIdentifier;
            w.MessageVersion = (correlate as WcfReceiveResult).MessageVersion;
            w.InvalidResponse = InvalidResponse;
            w.ResponseHeaders = cresult.ResponseHeaders;

            // Set async result
            IAsyncResult Result = new SendResultAsyncResult(state, new AutoResetEvent(false));

            // Completed delegate
            w.Completed += delegate(object Sender)
            {
                Worker sWorker = Sender as Worker; // Strong type sender
                // Lookup the result in the dictionary
                if (!asyncResults.ContainsKey(Result))
                    lock (asyncResults) { asyncResults.Add(Result, sWorker.SendResult); }
                (Result as SendResultAsyncResult).SetComplete(); // Set completed
                (Result.AsyncWaitHandle as AutoResetEvent).Set(); // send signal
                if (callback != null) callback(Result); // callback
            };

            // Add to thread pool
            ThreadPool.QueueUserWorkItem(new WaitCallback(w.WorkSend), data);

            return Result;
        }

        /// <summary>
        /// End a send operation and retrieve results.
        /// </summary>
        /// <param name="asyncResult">The <see cref="System.IAsyncResult" /> to get the result for.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public ISendResult EndSend(IAsyncResult asyncResult)
        {
            //TODO: Should block until result is available.
            object r;
            if (!asyncResults.TryGetValue(asyncResult, out r) || !(r is WcfSendResult))
                return null;
            else
            {
                WcfSendResult result = r as WcfSendResult;

                // Publish so WCF connector can send a result
                lock (results)
                    results.Add(result.MessageId, result);

                // Notify
                PublishResult(result.MessageId);

                // Remove the result
                lock (asyncResults)
                    asyncResults.Remove(asyncResult);

                return r as WcfSendResult;
            }
        }

        #endregion

        #region IListenWaitResponseConnector Members

        /// <summary>
        /// Occurs when the response provided is invalid and cannot be sent to the remote endpoint.
        /// </summary>
        public event EventHandler<MessageEventArgs> InvalidResponse;

        #endregion

        #region IDisposable Members
        //DOC: Documentation Required
        /// <summary>
        /// Releases all resources held by this object and suppresses finalization
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

                this.Close();

                try
                {
                    if (this.svcHost.State != CommunicationState.Closed)
                        this.svcHost.Close();
                }
                catch
                {
                    this.svcHost.Abort();
                }

                foreach (var item in waitHandles)
                    item.Value.Close();
            }
        }


        #endregion
    }
}
