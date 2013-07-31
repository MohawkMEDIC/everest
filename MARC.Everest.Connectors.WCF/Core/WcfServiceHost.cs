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
        /// WCF Service host
        /// </summary>
        public WcfServiceHost()
        {
        }

        /// <summary>
        /// Creates a new instance of the WcfServiceHost.
        /// </summary>
        /// <param name="serviceType">The type of service that this WcfServiceHost should host.</param>
        /// <param name="baseAddresses">A list of addresses that this service should bind to.</param>
        public WcfServiceHost(String serviceName, Type serviceType, params Uri[] baseAddresses)
        {
            this.ServiceName = serviceName;
            this.InitializeDescription(serviceType, new UriSchemeKeyedCollection(baseAddresses));
            
        }

        //TODO: Why is this set only, and not gettable.
        /// <summary>
        /// Sets the service name of the object.
        /// </summary>
        internal string ServiceName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the connector that is expecting the WCF service to contact when a message is received.
        /// </summary>
        public WcfServerConnector ConnectorHost { get; set; }

        /// <summary>
        /// Apply configuration
        /// </summary>
        protected override void ApplyConfiguration()
        {

            this.Description.ConfigurationName = this.ServiceName;
            base.ApplyConfiguration();

            ServiceMetadataBehavior mexBehavior = this.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (mexBehavior == null)
            {
                mexBehavior = new ServiceMetadataBehavior();
                this.Description.Behaviors.Add(mexBehavior);
            }
            else
            {
                //Metadata behavior has already been configured, 
                //so we don't have any work to do.
                return;
            }

            //Add a metadata endpoint at each base address
            //using the "/mex" addressing convention
            foreach (Uri baseAddress in this.BaseAddresses)
            {
                if (baseAddress.Scheme == Uri.UriSchemeHttp)
                {
                    mexBehavior.HttpGetEnabled = true;
                    this.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                            MetadataExchangeBindings.CreateMexHttpBinding(),
                                            "mex");
                }
                else if (baseAddress.Scheme == Uri.UriSchemeHttps)
                {
                    mexBehavior.HttpsGetEnabled = true;
                    this.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                            MetadataExchangeBindings.CreateMexHttpsBinding(),
                                            "mex");
                }
                else if (baseAddress.Scheme == Uri.UriSchemeNetPipe)
                {
                    this.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                            MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                                            "mex");
                }
                else if (baseAddress.Scheme == Uri.UriSchemeNetTcp)
                {
                    this.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                            MetadataExchangeBindings.CreateMexTcpBinding(),
                                            "mex");
                }
            }

        }
   
    }
}