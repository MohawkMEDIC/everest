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
using MARC.Everest.Connectors.WCF.Core;
using MARC.Everest.Connectors.WCF.Serialization;
using System.Threading;
using System.Configuration;
using System.ServiceModel.Channels;
using MARC.Everest.Interfaces;
using System.IO;
using MARC.Everest.Exceptions;
using System.ServiceModel;
using System.ComponentModel;

#if WINDOWS_PHONE
using MARC.Everest.Phone;
#else
using MARC.Everest.Connectors.WCF.Configuration;
#endif

namespace MARC.Everest.Connectors.WCF
{
    /// <summary>
    /// The WcfClient connector allows applications to communicate with remote endpoints using
    /// Windows Communication Foundation technologies. 
    /// </summary>
    /// <example>
    /// Send message in variable (instance) to <a href="http://shr.marc-hi.ca:8080/shr">http://shr.marc-hi.ca:8080/shr</a> host:
    /// <code lang="cs" title="main.cs">
    /// <![CDATA[
    ///using System;
    ///using MARC.Everest.Formatters.XML.Datatypes.R1;
    ///using MARC.Everest.Formatters.XML.ITS1;
    ///using MARC.Everest.Connectors;
    ///using MARC.Everest.DataTypes;
    ///using MARC.Everest.Connectors.WCF;
    ///using MARC.Everest.RMIM.CA.R020402.Interactions;
    ///using MARC.Everest.RMIM.CA.R020402.Vocabulary;
    ///using MARC.Everest.RMIM.CA.R020402.MCCI_MT002200CA;
    ///using MARC.Everest.RMIM.CA.R020402.REPC_MT000008CA;
    ///using MARC.Everest.RMIM.CA.R020402.QUQI_MT120006CA;
    ///using MARC.Everest.RMIM.CA.R020402.MCAI_MT700210CA;
    ///using MARC.Everest.RMIM.CA.R020402.QUQI_MT120008CA;
    ///
    ///namespace SharedHealthRecordConsumer
    ///{
    ///    public class Program
    ///    {
    ///        static void Main(string[] args)
    ///        {
    ///
    ///            // Connection string builder
    ///            WcfConnectionStringBuilder csBuilder = new WcfConnectionStringBuilder();
    ///            csBuilder.EndpointName = "exampleClient";
    ///
    ///            // Create the formatter
    ///            // NB: This can be skipped if using the configuration file's
    ///            // marc.everest.connectors.wcf section
    ///            XmlIts1Formatter formatter = new XmlIts1Formatter()
    ///            {
    ///                ValidateConformance = false
    ///            };
    ///            formatter.GraphAides.Add(new DatatypeFormatter()
    ///            {
    ///                ValidateConformance = true,
    ///                CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian
    ///            });
    ///
    ///            // Create the connector
    ///            WcfClientConnector client = new WcfClientConnector(csBuilder.GenerateConnectionString());
    ///            try
    ///            {
    ///                // Set the formatter
    ///                client.Formatter = formatter;
    ///
    ///                // Open the underlying channel
    ///                client.Open();
    ///
    ///                // Construct a request
    ///                COMT_IN100000CA instance = new COMT_IN100000CA(
    ///                  Guid.NewGuid(),
    ///                  DateTime.Now,
    ///                  ResponseMode.Immediate,
    ///                  COMT_IN100000CA.GetInteractionId(),
    ///                  COMT_IN100000CA.GetProfileId(),
    ///                  ProcessingID.Debugging,
    ///                  AcknowledgementCondition.Always,
    ///                  new Receiver()
    ///                  {
    ///                      Telecom = new TEL("http://shr.marc-hi.ca:8080/shr"),
    ///                      Device = new Device2(
    ///                        new II("1.3.6.1.4.1.33349.3.1.1.20.4", "SHR"),
    ///                        "MARC-HI Shared Health Record",
    ///                        null
    ///                      )
    ///                  },
    ///                  new Sender()
    ///                  {
    ///                      Telecom = new TEL() { NullFlavor = NullFlavor.NoInformation },
    ///                      Device = new Device1(
    ///                        new II("1.2.3.4.5.4", "Sample"),
    ///                        "Everest Guide Sample",
    ///                        "Example #107",
    ///                        null,
    ///                        null,
    ///                        "An Example"
    ///                      )
    ///                  },
    ///                  new MARC.Everest.RMIM.CA.R020402.QUQI_MT020000CA.ControlActEvent<ParameterList>(
    ///                    Guid.NewGuid(),
    ///                    COMT_IN100000CA.GetTriggerEvent(),
    ///                    new RecordTarget(),
    ///                    new Author(DateTime.Now),
    ///                    new QueryByParameter<ParameterList>(
    ///                      Guid.NewGuid(),
    ///                      new ParameterList(
    ///                        new MARC.Everest.RMIM.CA.R020402.REPC_MT220004CA.MostRecentByTypeIndicator(false)
    ///                      )
    ///                    )
    ///                  )
    ///                  {
    ///                      EffectiveTime = new IVL<TS>(DateTime.Now)
    ///                  }
    ///                );
    ///
    ///                // Set the patient
    ///                instance.controlActEvent.RecordTarget.SetPatient1(
    ///                  SET<II>.CreateSET(new II("1.3.6.1.4.1.33349.3.1.2.2.3.0", "369")
    ///                  {
    ///                      Scope = IdentifierScope.BusinessIdentifier
    ///                  }),
    ///                  new MARC.Everest.RMIM.CA.R020402.COCT_MT050207CA.Person(
    ///                    new PN(EntityNameUse.Legal,
    ///                      new ENXP[] { 
    ///                    new ENXP("Brown", EntityNamePartType.Family),
    ///                    new ENXP("Jennifer", EntityNamePartType.Given)
    ///                }
    ///                    ),
    ///                    AdministrativeGender.Female,
    ///                    new TS(new DateTime(1984, 07, 12), DatePrecision.Day)
    ///                  )
    ///                );
    ///
    ///                // Set the author
    ///                instance.controlActEvent.Author.SetAuthorPerson(
    ///                  SET<II>.CreateSET(new II("1.3.6.1.4.1.21367.2010.3.2.202", "0008")),
    ///                  new MARC.Everest.RMIM.CA.R020402.COCT_MT090108CA.Person(
    ///                    new PN(EntityNameUse.License,
    ///                      new ENXP[] {
    ///                    new ENXP("Birth", EntityNamePartType.Family),
    ///                    new ENXP("John", EntityNamePartType.Given)
    ///                }
    ///                    ),
    ///                    null
    ///                  )
    ///                );
    ///
    ///                // Code truncated to save space
    ///
    ///                // Send the data
    ///                ISendResult sendResult = client.Send(instance);
    ///
    ///                // Did we actually send the message?
    ///                if (sendResult.Code != ResultCode.Accepted &&
    ///                  sendResult.Code != ResultCode.AcceptedNonConformant)
    ///                {
    ///                    Console.WriteLine("Couldn't send message!");
    ///                    return;
    ///                }
    ///
    ///                // Like any ISendReceiveConnector we wait for a response
    ///                IReceiveResult receiveResult = client.Receive(sendResult);
    ///
    ///                // Output the receive result
    ///                if (receiveResult.Structure != null)
    ///                    Console.WriteLine(receiveResult.Structure.GetType().Name);
    ///                else
    ///                {
    ///                    Console.WriteLine("Error interpreting result:");
    ///                    foreach (var itm in receiveResult.Details)
    ///                        if (itm.Type == ResultDetailType.Error)
    ///                            Console.WriteLine(itm.Message);
    ///                }
    ///
    ///                // Wait for a key press
    ///                Console.ReadKey();
    ///            }
    ///            finally
    ///            {
    ///                // Clean up the connector
    ///                client.Close();
    ///                client.Dispose();
    ///            }
    ///
    ///        }
    ///
    ///    }
    ///}
    /// ]]>
    /// </code>
    /// <code lang="xml" title="app.config"> 
    /// <![CDATA[
    /// <configSections>
    ///     <section type="MARC.Everest.Connectors.WCF.Configuration.ConfigurationSection, MARC.Everest.Connectors.WCF, Version=1.1.0.0" name="marc.everest.connectors.wcf"/>
    /// </configSections>
    ///<system.serviceModel>
    ///  <client>
    ///    <endpoint binding="basicHttpBinding" address="http://shr.marc-hi.ca:8080/shr" name="sharedHealthRecord" contract="MARC.Everest.Connectors.WCF.Core.IConnectorContract" bindingConfiguration="exampleBindingConfig"/>
    ///  </client>
    ///  <bindings>
    ///    <basicHttpBinding>
    ///      <binding name="exampleBindingConfig" maxReceivedMessageSize="10485760"/>
    ///    </basicHttpBinding>
    ///  </bindings>
    ///</system.serviceModel>
    /// 
    /// <marc.everest.connectors.wcf 
    ///     formatter="MARC.Everest.Formatters.XML.ITS1.Formatter, MARC.Everest.Formatters.XML.ITS1, Version=1.0.0.0" aide="MARC.Everest.Formatters.XML.Datatypes.R1.Formatter, MARC.Everest.Formatters.XML.Datatypes.R1, Version=1.0.0.0">
    ///         <action type="MARC.Everest.RMIM.CA.R020402.Interactions.COMT_IN100000CA" action="urn:hl7-org:v3:COMT_IN100000CA_I"/>
    /// </marc.everest.connectors.wcf>
    /// ]]>
    /// </code>
    /// </example>
    /// <remarks>
    /// <para>
    /// In order to properly communicate with an endpoint, it is important that you add the configuration
    /// section MARC.Everest.Connectors.wcf to the web.config file (see example). 
    /// </para>
    /// <para>
    /// <b>Note:</b> Everest for Windows Phone cannot use the Send method as the Silverlight framework
    /// does not permit synchronous calls to the WCF client base. 
    /// </para>
    /// <code lang="cs" title="Sending on Windows Phone Platform">
    /// <![CDATA[
    /// 
    /// private WcfClientConnector m_connector;
    /// delegate void MessageReceived(IReceiveResult message);
    /// 
    /// public void fooMethod() {
    ///     // Create
    ///     this.m_connector = new WcfClientConnector("endpointname=pds");
    ///     this.m_connector.Formatter = new XmlIts1Formatter();
    ///     this.m_connector.Formatter.GraphAides.Add(new DatatypeFormatter());
    ///     this.m_connector.Open();
    ///     this.m_connector.BeginSend(instance, this.SendCompleted, null);
    ///     // .. Continue UI ..
    /// }
    /// 
    /// private void SendCompleted(IAsyncResult sendAsyncResult)
    /// {
    ///      var sendResult = this.m_clientConnector.EndSend(sendAsyncResult);
    ///      if(sendResult.Code != ResultCode.Accepted &&
    ///         sendResult.Code != ResultCode.AcceptedNonConformant)
    ///             return; // bail out
    ///      
    ///     // receive the response message
    ///     var receiveResult = this.m_connector.Receive(sendResult);
    ///     
    ///     Dispatcher.BeginInvoke(new MessageReceived(this.OnMessageReceived), receiveResult);
    /// }
    /// 
    /// private void OnMessageReceived(IReceiveResult result)
    /// {
    ///     // Do UI stuff here...
    /// }
    /// ]]>
    /// </code>
    /// <para>
    /// The WcfClient must use a connector that is an IInputChannel and IOutputChannel.
    /// </para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [Description("WCF Client Connector")]
    public class WcfClientConnector : ISendReceiveConnector, IFormattedConnector, IDisposable
    {

        /// <summary>
        /// Creates a new instance of the WcfClientConnector.
        /// </summary>
        public WcfClientConnector() { }
        /// <summary>
        /// Creates a new instance of the WcfClientConnector with the specified connection string.
        /// </summary>
        /// <param name="connectionString">The string that dictates the connection parameters (<see cref="ConnectionString"/>).</param>
        public WcfClientConnector(string connectionString) { this.ConnectionString = connectionString; }

        #region ISendReceiveConnector Members

        /// <summary>
        /// Allows custom SOAP headers to be added to the oubound messages prior to sending the message
        /// </summary>
        /// <remarks>This array should not be used to append WS-RM or WS-SEC data to your message as these are handled
        /// by the WCF layer automatically and should be done at the Configuration level. 
        /// <para>This array exists to facilitate the addition of custom SOAP headers required by some jurisidictions. This can include 
        /// custom authentication tokens, test ticket identifiers, etc...</para></remarks>
        //public MessageHeaders Headers { get; set; }

        /// <summary>
        /// The client that is servicing requests.
        /// </summary>
        private ConnectorServiceClient wcfClient;
        /// <summary>
        /// WCF Configuration section reference.
        /// </summary>
#if WINDOWS_PHONE
        // Backing field for actions
        private Dictionary<Type, string> m_actions = new Dictionary<Type, string>();
        /// <summary>
        /// Gets the actions that is registered for the specified message type
        /// </summary>
        public string GetAction(Type messageType)
        {
            String retVal = null;
            if (m_actions.TryGetValue(messageType, out retVal))
                return retVal;
            return null;
        }
        /// <summary>
        /// Adds an action to the registered message types, replaces an existing one if 
        /// it already exists
        /// </summary>
        public void AddAction(Type messageType, string wsaAction)
        {
            if (this.m_actions.ContainsKey(messageType))
                this.m_actions[messageType] = wsaAction;
            else
                this.m_actions.Add(messageType, wsaAction);
        }
#else
        private MARC.Everest.Connectors.WCF.Configuration.ConfigurationSection wcfConfiguration;
#endif
        /// <summary>
        /// Dictionary of pending Results.
        /// </summary>
        private Dictionary<ISendResult, Message> results = new Dictionary<ISendResult, Message>();
        /// <summary>
        /// Dictionary of Async results.
        /// </summary>
        private Dictionary<IAsyncResult, object> asyncResults = new Dictionary<IAsyncResult, object>();

        /// <summary>
        /// The worker class is responsible for parsing and consuming messages. This is done for code-reusability
        /// in the synchronous and asynchronous methods.
        /// </summary>
        private class Worker
        {
#if WINDOWS_PHONE
            /// <summary>
            /// Actions dictionary for the worker class
            /// </summary>
            public Dictionary<Type, string> Actions { get; set; }
#else
            /// <summary>
            /// Gets or sets a reference to the WCF configuration section.
            /// </summary>
            public MARC.Everest.Connectors.WCF.Configuration.ConfigurationSection WcfConfiguration { get; set; }
#endif
            /// <summary>
            /// Gets or sets the formatter to use.
            /// </summary>
            public IXmlStructureFormatter Formatter { get; set; }
            /// <summary>
            /// Occurs when the operation is complete.
            /// </summary>
            public event WaitCallback Completed;
            /// <summary>
            /// Gets or sets the <see cref="MARC.Everest.Connectors.WCF.WcfSendResult">Result</see> from the worker's Work() method.
            /// </summary>
            public WcfSendResult SendResult { get; set; }
            /// <summary>
            /// Gets or sets the <see cref="MARC.Everest.Connectors.WCF.WcfSendResult">Result</see> of the receive operation.
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
            /// Occurs when an invalid message is received.
            /// </summary>
            internal EventHandler<MessageEventArgs> InvalidMessage;
            /// <summary>
            /// Gets or sets the custom headers
            /// </summary>
            public MessageHeaders CustomHeaders { get; set; }
            /// <summary>
            /// Performs work.
            /// </summary>
            /// <param name="state">A state object for providing state to the work method.</param>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
            public void WorkSend(object state)
            {
                // Prepare stream
                SendResult = new WcfSendResult();
                IGraphable data = null;
                ReceiveResult = new WcfReceiveResult();
                try { data = (IGraphable)state; }
                catch { }


                // Prepare the serializer surrogate
                XmlSerializerSurrogate surrogate = new XmlSerializerSurrogate(Formatter);

                try
                {
                    if (data == null)
                        throw new MessageValidationException("Invalid message structure passed to the connector", data);

                    // Graph the object
                    string soapAction = "";
#if WINDOWS_PHONE
                    SendResult.Message = Message.CreateMessage(MessageVersion, Actions != null && Actions.TryGetValue(state.GetType(), out soapAction) ? soapAction : "none", data, surrogate);
#else
                    SendResult.Message = Message.CreateMessage(MessageVersion, WcfConfiguration != null && WcfConfiguration.Actions.TryGetValue(state.GetType().FullName, out soapAction) ? soapAction : "none", data, surrogate);
#endif
                    if(CustomHeaders != null)
                        SendResult.Message.Headers.CopyHeadersFrom(CustomHeaders);
                    // Validate
#if !WINDOWS_PHONE
                    var graphResult = this.Formatter.Graph(new MemoryStream(), data);
                    SendResult.Code = graphResult.Code;

                    // Did the operation fail?
                    if (graphResult.Code != ResultCode.Accepted && graphResult.Code != ResultCode.AcceptedNonConformant)
                    {
                        if (InvalidMessage != null)
                        {
                            MessageEventArgs me = new MessageEventArgs(SendResult.Code, surrogate.Details);
                            InvalidMessage(this, me);
                            if (me.Alternate != null)
                                SendResult.Message = Message.CreateMessage(MessageVersion, soapAction, me.Alternate, surrogate);
                        }
                        else
                        {
                            SendResult.Details = surrogate.Details;
                            SendResult.Message = null;
                        }
                    }
#endif
                }
                catch (MessageValidationException e)
                {
                    SendResult.Code = ResultCode.Rejected;
                    List<IResultDetail> dtl = new List<IResultDetail>(new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) });
                    dtl.AddRange(surrogate.Details ?? new IResultDetail[0]);
                    SendResult.Details = dtl.ToArray();
                }
                catch (FormatException e)
                {
                    SendResult.Code = ResultCode.Rejected;
                    SendResult.Details = new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) };
                }
                catch (Exception e)
                {
                    SendResult.Code = ResultCode.Error;
                    SendResult.Details = new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) };
                }
                finally
                {
                }

                // Fire completed event
                if (Completed != null) Completed(this);
            }

            /// <summary>
            /// Worker method.
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
            public void WorkReceive(object state)
            {
                ReceiveResult = new WcfReceiveResult();

                // Create the surrogate
                XmlSerializerSurrogate surrogate = new XmlSerializerSurrogate(Formatter);
                try
                {

                    // Parse the object
                    ReceiveResult.MessageVersion = (state as Message).Version;
                    ReceiveResult.Structure = (state as Message).GetBody<IGraphable>(surrogate);
                    ReceiveResult.Details = surrogate.Details;
                    ReceiveResult.Code = ResultCode.Accepted;
                    ReceiveResult.Headers = ReceiveResult.ResponseHeaders = (state as Message).Headers;
#if WINDOWS_PHONE
                    if (ReceiveResult.Details.Find(o => o.Type == ResultDetailType.Error) != null)
#else
                    if (Array.Find<IResultDetail>(ReceiveResult.Details, o => o.Type == ResultDetailType.Error) != null)
#endif
                        ReceiveResult.Code = ResultCode.AcceptedNonConformant;
                    else if (ReceiveResult.Structure == null)
                        ReceiveResult.Code = ResultCode.TypeNotAvailable;
                }
                catch (MessageValidationException e)
                {
                    ReceiveResult.Code = ResultCode.Rejected;
                    List<IResultDetail> dtl = new List<IResultDetail>(new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) });
                    dtl.AddRange(surrogate.Details ?? new IResultDetail[0]);
                    ReceiveResult.Details = dtl.ToArray();
                }
                catch (FormatException e)
                {
                    ReceiveResult.Code = ResultCode.Rejected;
                    ReceiveResult.Details = new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) };
                }
                catch (Exception e)
                {
                    ReceiveResult.Code = ResultCode.Error;
                    ReceiveResult.Details = new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) };
                }

                // Fire completed event
                if (Completed != null) Completed(this);
            }
        }

        /// <summary>
        /// Receive the result from the connector.
        /// </summary>
        /// <param name="correlate">The send result. Used to correlate the response with the request.</param>
        /// <returns>The receive result.</returns>
        /// <remarks>Performs a blocking receive operation. If you use this method after a BeginSend()
        /// this method will block and wait for a response.</remarks>
        public IReceiveResult Receive(ISendResult correlate)
        {
            // Receive the result from the connector
            if (!IsOpen())
                throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen);

            // wait for a message
            Message responseMessage = null;
            DateTime startTime = DateTime.Now;

            while (!results.TryGetValue(correlate, out responseMessage) && DateTime.Now.Subtract(startTime).CompareTo(wcfClient.Endpoint.Binding.ReceiveTimeout) == -1)
                //TODO: Should use a WaitHandle
                Thread.Sleep(100);

            if (responseMessage == null)
                throw new TimeoutException("Receive() operation did not complete in specified amount of time");

            // process the message 
            Worker w = new Worker();
            w.Formatter = (IXmlStructureFormatter)Formatter;
#if WINDOWS_PHONE
            w.Actions = this.m_actions;
#else
            w.WcfConfiguration = wcfConfiguration;
#endif
            w.WorkReceive(responseMessage);

            return w.ReceiveResult;
        }

        /// <summary>
        /// Begin an asynchronous receive event. 
        /// </summary>
        /// <param name="correlate">The result of the send method.</param>
        /// <param name="callback">A callback.</param>
        /// <param name="state">User state.</param>
        public IAsyncResult BeginReceive(ISendResult correlate, AsyncCallback callback, object state)
        {
            // Formatter check
            if (!IsOpen())
                throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen);

            // Create the work that will perform the operations
            Worker w = new Worker();
            w.Formatter = (IXmlStructureFormatter)Formatter;
#if WINDOWS_PHONE
            w.Actions = this.m_actions;
#else
            w.WcfConfiguration = wcfConfiguration;
#endif
            w.MessageVersion = wcfClient.Endpoint.Binding.MessageVersion;

            Message data = null;

            if (!results.TryGetValue(correlate, out data))
                return null;

            // The asynchronous result, used to correlate results when EndReceive is called
            IAsyncResult result = new ReceiveResultAsyncResult(state, new AutoResetEvent(false));

            // Work
            w.Completed += new WaitCallback(delegate(object sender)
            {
                lock (asyncResults)
                    asyncResults.Add(result, (sender as Worker).ReceiveResult);
                (result.AsyncWaitHandle as AutoResetEvent).Set();
            });

            ThreadPool.QueueUserWorkItem(w.WorkReceive, data);

            return result;
        }

        /// <summary>
        /// Finish asynchronous receive result.
        /// </summary>
        /// <param name="asyncResult">The result of the BeginReceive event.</param>
        /// <returns>The result of the receive.</returns>
        public IReceiveResult EndReceive(IAsyncResult asyncResult)
        {
            //TODO: This should block if result is not available.
            object result = null;
            if (asyncResults.TryGetValue(asyncResult, out result))
            {
                lock (asyncResults) asyncResults.Remove(asyncResult);
                return (IReceiveResult)result;
            }
            return null;
        }

        #endregion

        #region ISendingConnector Members

        /// <summary>
        /// Send a request to the remote endpoint.
        /// </summary>
        /// <param name="data">The IGraphable data to send.</param>
        public ISendResult Send(MARC.Everest.Interfaces.IGraphable data)
        {
            return Send(data, null);
        }

        /// <summary>
        /// Send a request to the remote endpoint with the specified message headers
        /// </summary>
        /// <param name="data">The data to be sent to the remote endpoint</param>
        /// <param name="headers">The SOAP headers to append to the request</param>
        /// <returns>A <see cref="T:MARC.Everest.Connectors.WCF.WcfSendResult"/> instance containing the result of the send operation</returns>
        public ISendResult Send(MARC.Everest.Interfaces.IGraphable data, MessageHeaders headers)
        {
            //TODO: Convert this into a method call for reuability.
            // Formatter check
            if (!IsOpen())
                throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen);

            // Create the work that will perform the operations
            Worker w = new Worker();
            w.Formatter = (IXmlStructureFormatter)Formatter;
#if WINDOWS_PHONE
            w.Actions = this.m_actions;
#else
            w.WcfConfiguration = wcfConfiguration;
#endif
            w.MessageVersion = wcfClient.Endpoint.Binding.MessageVersion;
            w.CustomHeaders = headers;
            // Work
            w.WorkSend(data);

            // Send over the channel
            if (w.SendResult.Code == ResultCode.Accepted ||
                w.SendResult.Code == ResultCode.AcceptedNonConformant)
                lock (results)
                    results.Add(w.SendResult, wcfClient.ProcessInboundMessage(w.SendResult.Message));

            // Return the result
            return w.SendResult;
        }

        /// <summary>
        /// Start an asynchronous send operation.
        /// </summary>
        /// <param name="data">The data to send to the server.</param>
        /// <param name="callback">A callback delegate to execute when the operation is complete.</param>
        /// <param name="state">A user state object.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public IAsyncResult BeginSend(MARC.Everest.Interfaces.IGraphable data, AsyncCallback callback, object state)
        {
            // Formatter check
            if (!IsOpen())
                throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen);

            // Create the work that will perform the operations
            Worker w = new Worker();
            w.Formatter = (IXmlStructureFormatter)Formatter;

#if !WINDOWS_PHONE
            w.WcfConfiguration = wcfConfiguration;
#endif
            w.MessageVersion = wcfClient.Endpoint.Binding.MessageVersion;

            // Result
            IAsyncResult result = new SendResultAsyncResult(state, new AutoResetEvent(false));

            // Work
            w.Completed += new WaitCallback(delegate(object sender)
                {
                    lock (results)
                        results.Add((sender as Worker).SendResult, wcfClient.ProcessInboundMessage((sender as Worker).SendResult.Message));
                    lock (asyncResults)
                        asyncResults.Add(result, (sender as Worker).SendResult);
                    (result.AsyncWaitHandle as AutoResetEvent).Set();
                    if (callback != null)
                        callback(result);
                });

            ThreadPool.QueueUserWorkItem(w.WorkSend, data);

            // Return the result
            return result;
        }

        /// <summary>
        /// End an asynchronous send operation.
        /// </summary>
        /// <param name="asyncResult">The asyncResult to retrieve the result.</param>
        public ISendResult EndSend(IAsyncResult asyncResult)
        {
            object result = null;
            if (asyncResults.TryGetValue(asyncResult, out result))
            {
                lock (asyncResults) asyncResults.Remove(asyncResult);
                return (ISendResult)result;
            }
            return null;
        }

        #endregion

        #region IConnector Members

        /// <summary>
        /// Gets or sets the connection string for the connector.
        /// </summary>
        /// <remarks>
        /// Valid connection string parameters are:
        /// <list type="table">
        ///     <listheader>
        ///         <term>Key</term>    
        ///         <description>Description</description>
        ///     </listheader>
        ///     <item><term>endpointName</term><description>The name of the endpoint.</description></item>
        ///     <item><term>endpointAddress</term><description>Address of the remote endpoint.</description></item>
        /// </list>
        /// </remarks>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Sets up the connector so it is able to open connections to the remote endpoint, and opens the channel.
        /// </summary>
        public void Open()
        {
            //TODO: Check if the connector is already open, or if the connector is in a faulted state.

            // Connection string
            if (ConnectionString == null)
                throw new ConnectorException(ConnectorException.MSG_NULL_CONNECTION_STRING, ConnectorException.ReasonType.NullConnectionString);
            else if (Formatter == null)
                throw new ConnectorException(ConnectorException.MSG_NULL_FORMATTER, ConnectorException.ReasonType.NullFormatter);

            Dictionary<string, List<string>> parameters = ConnectionStringParser.ParseConnectionString(ConnectionString);
            string endpointName = null, endpointAddress = null;

            // Parameters
            foreach (KeyValuePair<string, List<string>> parm in parameters)
                switch (parm.Key)
                {
                    case "endpointname":
                        endpointName = parm.Value[0];
                        break;
                    case "endpointaddress":
                        endpointAddress = parm.Value[0];
                        break;
                }

            // Create the client
            if (endpointName == null)
                throw new InvalidOperationException("The connection string must include the 'endpointName' attribute!");
            else if (endpointAddress == null)
                wcfClient = new ConnectorServiceClient(endpointName);
            else
                wcfClient = new ConnectorServiceClient(endpointName, endpointAddress);


#if WINDOWS_PHONE
            ((ICommunicationObject)wcfClient).Open();
#else
            wcfClient.Open();
            // Using the app.config
            wcfConfiguration = ConfigurationManager.GetSection("marc.everest.connectors.wcf") as MARC.Everest.Connectors.WCF.Configuration.ConfigurationSection;
            if (Formatter == null)
                Formatter = (wcfConfiguration.Formatter as ICloneable).Clone() as IStructureFormatter;
#endif
        }

        /// <summary>
        /// Close the connection
        /// </summary>
        public void Close()
        {
#if WINDOWS_PHONE
            wcfClient.Abort();
#else
            wcfConfiguration = null;
            wcfClient.Close();
#endif
        }

        /// <summary>
        /// Returns true if the connection can send
        /// </summary>
        public bool IsOpen()
        {
            return wcfClient != null && wcfClient.State == CommunicationState.Opened;
        }

        #endregion

        #region IFormattedConnector Members

        /// <summary>
        /// Gets or sets the formatter that is to be used when creating instances to/from the 
        /// the remote endpoint
        /// </summary>
        public IStructureFormatter Formatter { get; set; }

        #endregion

        #region IDisposable Members
        /// <summary>
        /// Releases unmanaged resources and marks the object disposed for the Garbage Collector
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
        /// <param name="disposing">True to release all resources, false to only release unmanaged resources.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.results.Clear();
            }

            this.Close();

            try
            {
                if (this.wcfClient.State != CommunicationState.Closed)
#if WINDOWS_PHONE
                    ((ICommunicationObject)wcfClient).Close();
#else
                    wcfClient.Close();
#endif
            }
            catch
            {
                wcfClient.Abort();
            }

        }

        #endregion
    }
}
