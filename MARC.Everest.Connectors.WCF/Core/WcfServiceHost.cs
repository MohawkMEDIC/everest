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
using System.ServiceModel.Activation;
using System.ServiceModel.Description;

namespace MARC.Everest.Connectors.WCF.Core
{
    /// <summary>
    /// Service host class that is used for creating instances.
    /// </summary>
    public class WcfServiceHost : ServiceHost
    {

        /// <summary>
        /// Creates a new instance of the WcfServiceHost.
        /// </summary>
        /// <param name="serviceType">The type of service that this WcfServiceHost should host.</param>
        /// <param name="baseAddresses">A list of addresses that this service should bind to.</param>
        public WcfServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses) { }

        //TODO: Why is this set only, and not gettable.
        /// <summary>
        /// Sets the service name of the object.
        /// </summary>
        internal string ServiceName
        {
            set
            {
                this.Description.ConfigurationName = value;
                ApplyConfiguration();
            }
        }

        /// <summary>
        /// Gets or sets the connector that is expecting the WCF service to contact when a message is received.
        /// </summary>
        public WcfServerConnector ConnectorHost { get; set; }

    }
}