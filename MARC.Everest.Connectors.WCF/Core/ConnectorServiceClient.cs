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
 * WARRANTIES OR CONDITIONS OF ANY KIND, either exopress or implied. See the 
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
using System.Diagnostics;

namespace MARC.Everest.Connectors.WCF.Core
{
    
    /// <summary>
    /// The connector service client is used by the WCF Client to perform WCF operations.
    /// </summary>
    public class ConnectorServiceClient : System.ServiceModel.ClientBase<IConnectorContract>, IConnectorContract
    {
        #region Service Base Methods
        /// <summary>
        /// Creates a new instance of ConnectorServiceClient.
        /// </summary>
        public ConnectorServiceClient() { }

        /// <summary>
        /// Creates a new instance of the ConnectorServiceClient using the specified <paramref name="endpointConfigurationName"/>.
        /// </summary>
        /// <param name="endpointConfigurationName">The name of the endpoint configuration.</param>
        public ConnectorServiceClient(string endpointConfigurationName) : base(endpointConfigurationName) {
            
        }
        //DOC: Shouldn't this be local address
        /// <summary>
        /// Creates a new instance of the ConnectorServiceClient using the specified <paramref name="endpointConfigurationName"/> and 
        /// <paramref name="remoteAddress"/>.
        /// </summary>
        /// <param name="endpointConfigurationName">The name of the endpoint configuration.</param>
        /// <param name="remoteAddress">The fully qualified uri resource name that this service will bind to. Example: http://localhost:1234/service/ .</param>
        public ConnectorServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress) { }
        /// <summary>
        /// Creates a new instance of the ConnectorServiceClient using the specified <paramref name="endpointConfigurationName"/> and
        /// <paramref name="endpointAddress"/>.
        /// </summary>
        /// <param name="endpointConfigurationName">The name of the endpoint configuration.</param>
        /// <param name="endpointAddress">An EndpointAddress object that specifies the address this service should bind to.</param>
        public ConnectorServiceClient(string endpointConfigurationName, EndpointAddress endpointAddress) : base(endpointConfigurationName, endpointAddress) { }
        /// <summary>
        /// Creates a new instance of the ConncetorServiceClient using the specified <paramref name="binding"/> and <paramref name="endpointAddress"/>.
        /// </summary>
        /// <param name="binding">The binding object this service should use to bind to the endpointAddress.</param>
        /// <param name="endpointAddress">An EndpointAddress object that specifies the address this service should bind to using the specified binding.</param>
        public ConnectorServiceClient(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress) { }
        #endregion

#if WINDOWS_PHONE

        /// <summary>
        /// Create the underlying channel for communications
        /// </summary>
        /// <returns></returns>
        protected override IConnectorContract CreateChannel()
        {
            return new ConnectorServiceChannel(this);
        }

        #region IConnectorContract Members

        /// <summary>
        /// Begin inbound message process
        /// </summary>
        IAsyncResult IConnectorContract.BeginProcessInboundMessage(Message m, AsyncCallback callback, object state)
        {
            return base.Channel.BeginProcessInboundMessage(m, callback, state);
        }

        /// <summary>
        /// End inbound process
        /// </summary>
        Message IConnectorContract.EndProcessInboundMessage(IAsyncResult result)
        {
            return base.Channel.EndProcessInboundMessage(result);
        }

        #endregion

        /// <summary>
        /// Service channel
        /// </summary>
        private class ConnectorServiceChannel : System.ServiceModel.ClientBase<IConnectorContract>.ChannelBase<IConnectorContract>, IConnectorContract
        {
            // Lock object
            private Object m_lockObject = new Object();

            /// <summary>
            /// Constructs a new instance of the object
            /// </summary>
            /// <param name="client"></param>
            public ConnectorServiceChannel(System.ServiceModel.ClientBase<IConnectorContract> client) : 
                    base(client) {
            }

            #region IConnectorContract Members

            /// <summary>
            /// Begin process inbound message
            /// </summary>
            public IAsyncResult BeginProcessInboundMessage(Message m, AsyncCallback callback, object state)
            {
                object[] _args = new object[] { m };

                return base.BeginInvoke("ProcessInboundMessage", _args, callback, state);

            }

            /// <summary>
            /// End process inbound message
            /// </summary>
            public Message EndProcessInboundMessage(IAsyncResult result)
            {
                return (Message)base.EndInvoke("ProcessInboundMessage", new object[0], result);
            }
            #endregion
        }

        /// <summary>
        /// Process an inbound message in a synchronous manner
        /// </summary>
        public Message ProcessInboundMessage(Message m)
        {
            
            // This is a synchronous method (for now) because we need
            // to maintain compatiblity with the Everest Framework.
            // TODO: Make this async. 
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            Message result = null;
            BeginOperationDelegate bod = new BeginOperationDelegate(this.OnBeginProcessInboundMessage);
            EndOperationDelegate eod = new EndOperationDelegate(this.OnEndProcessMessage);
            SendOrPostCallback sopcb = new SendOrPostCallback(
                delegate(object state)
                {
                    InvokeAsyncCompletedEventArgs e = state as InvokeAsyncCompletedEventArgs;
                    result = e.Results[0] as Message;
                    // Pulse the waiting async to let it know its been set
                    resetEvent.Set();
                }
                );
            base.InvokeAsync(bod, new object[] { m }, eod, sopcb, null);

            resetEvent.WaitOne();
            return result;
        }

        /// <summary>
        /// Begin process message
        /// </summary>
        private System.IAsyncResult OnBeginProcessInboundMessage(object[] inValues, System.AsyncCallback callback, object asyncState)
        {
            System.ServiceModel.Channels.Message request = ((System.ServiceModel.Channels.Message)(inValues[0]));
            return ((IConnectorContract)(this)).BeginProcessInboundMessage(request, callback, asyncState);
        }

        /// <summary>
        /// Processing is completed
        /// </summary>
        private void OnProcessMessageCompleted(object state)
        {
            // TODO:
            //if ((this.ProvideAndRegisterDocumentSetCompleted != null))
            //{
            //    InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
            //    this.ProvideAndRegisterDocumentSetCompleted(this, new ProvideAndRegisterDocumentSetCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            //}
        }

        /// <summary>
        /// End process message
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private object[] OnEndProcessMessage(System.IAsyncResult result)
        {
            System.ServiceModel.Channels.Message retVal = ((IConnectorContract)(this)).EndProcessInboundMessage(result);

            return new object[] {retVal};
        }
#else
        #region IConnectorContract Members
        //DOC: Let's beef this up a bit here.
        /// <summary>
        /// Process the message.
        /// </summary>
        /// <param name="m">The message to send.</param>
        /// <returns>The resulting message.</returns>
        public System.ServiceModel.Channels.Message ProcessInboundMessage(System.ServiceModel.Channels.Message m)
        {
#if DEBUG
            Trace.WriteLine("Processing inbound channel message");
#endif
            return base.Channel.ProcessInboundMessage(m);
#if DEBUG
            Trace.WriteLine("Done Processing inbound channel message");
#endif        
        }

        #endregion
#endif


    }
}