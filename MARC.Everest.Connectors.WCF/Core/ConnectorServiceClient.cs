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
        public ConnectorServiceClient(string endpointConfigurationName) : base(endpointConfigurationName) { }
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

        #region IConnectorContract Members
        //DOC: Let's beef this up a bit here.
        /// <summary>
        /// Process the message.
        /// </summary>
        /// <param name="m">The message to send.</param>
        /// <returns>The resulting message.</returns>
        public System.ServiceModel.Channels.Message ProcessInboundMessage(System.ServiceModel.Channels.Message m)
        {
            return base.Channel.ProcessInboundMessage(m);
        }

        #endregion
    }
}