using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Connectors.WCF
{
    /// <summary>
    /// Abstracts the task of building the WCF connection string to be 
    /// passed to the constructor of a <see cref="T:MARC.Everest.Connectors.WCF.WcfServerConnector"/>
    /// or <see cref="T:MARC.Everest.Connectors.WCF.WcfClientConnector"/>
    /// </summary>
    public class WcfConnectionStringBuilder
    {

        /// <summary>
        /// Gets or sets the name of the service as defined in the application
        /// configuration file
        /// </summary>
        /// <remarks>When this property is set, the Wcf connectors will ignore the
        /// other potential parameters.</remarks>
        public string ServiceName { get; set; }
        /// <summary>
        /// Specifies the endpoint address on which the connector should listen or send data. This 
        /// allows for the creation of endpoints without the use of a configuration file.
        /// </summary>
        /// <remarks>When ServiceName is used in a server connector, this parameter is ignored. 
        /// <para>This parameter modifies the behavior of the client connector to change
        /// the endpoint configuration's target address</para></remarks>
        public string EndpointAddress { get; set; }
        /// <summary>
        /// Specifies the binding type of a custom WcfEndpoint. This 
        /// allows for the creation of endpoints without the use of a configuration file.
        /// </summary>
        /// <remarks>When ServiceName is used, this parameter is ignored.
        /// <para>The WCF Server connector supports the following binding types for runtime created 
        /// services:</para>
        /// <list style="table">
        ///     <listheader><term>BindingType</term><description>Binding</description></listheader>
        ///     <item><term>basicHttpBinding</term><description>SOAP 1.1 over HTTP</description></item>
        ///     <item><term>wsHttpBinding</term><description>SOAP 1.2 over HTTP</description></item>
        /// </list>
        /// </remarks>
        public string BindingType { get; set; }
        /// <summary>
        /// Specifies the binding configuration of the WcfEndpoint. This 
        /// allows for the creation of endpoints without the use of a configuration file.
        /// </summary>
        /// <remarks>When ServiceName is used, this parameter is ignored.
        /// </remarks>
        public string BindingConfiguration { get; set; }
        /// <summary>
        /// When using the connection string with a client connector, specifies
        /// the name of the endpoint in the configuration file with which to communicate
        /// </summary>
        public string EndpointName { get; set; }


        /// <summary>
        /// Generate the connection string
        /// </summary>
        public string GenerateConnectionString()
        {
            StringBuilder conn = new StringBuilder();
            bool isClient = false;
            // Cannot mix serivcename and endpoint name
            if (!String.IsNullOrEmpty(this.ServiceName))
            {
                conn.AppendFormat("servicename={0};", this.ServiceName);
                return conn.ToString();
            }
            else if (!String.IsNullOrEmpty(this.EndpointName))
            {
                conn.AppendFormat("endpointName={0};", this.EndpointName);
                isClient = true;
            }

            // Endpoint address
            if (!String.IsNullOrEmpty(this.EndpointAddress))
            {
                if (!isClient && String.IsNullOrEmpty(this.BindingType) || String.IsNullOrEmpty(this.BindingConfiguration))
                    throw new InvalidOperationException("When endpointAddress is specified, bindingType and bindingConfiguration must also be supplied");
                conn.AppendFormat("endpointAddress={0};", this.EndpointAddress);
            }
            if (!String.IsNullOrEmpty(this.BindingType))
            {
                if (this.BindingType != "basicHttpBinding" && this.BindingType != "wsHttpBinding")
                    throw new InvalidOperationException("Only basicHttpBinding or wsHttpBinding are supported for this parameter");
                else if (isClient)
                    throw new InvalidOperationException("Cannot set a bindingType on a client connector");
                conn.AppendFormat("bindingtype={0};", this.BindingType);
            }
            if (!String.IsNullOrEmpty(this.BindingConfiguration))
            {
                if (isClient)
                    throw new InvalidOperationException("Cannot set a bindingType on a client connector");

                conn.AppendFormat("bindingConfiguration={0};", this.BindingConfiguration);
            }
            return conn.ToString();
        }
    }
}
