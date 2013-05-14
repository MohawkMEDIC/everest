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
using System.Configuration;
using System.IO;
using MARC.Everest.Interfaces;
using MARC.Everest.Connectors.WCF.Configuration;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.Xml.XPath;
using MARC.Everest.Connectors.WCF.Serialization;
using System.Xml;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;

namespace MARC.Everest.Connectors.WCF.Core
{
    
    /// <summary>
    /// The listen connector service behavior for WCF service host.
    /// </summary>
    [ServiceBehavior(ValidateMustUnderstand=true, InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ConnectorService : IConnectorContract, IPolicyAccessContract
    {

        /// <summary>
        /// The configuration settings for this particular contract.
        /// </summary>
        private static MARC.Everest.Connectors.WCF.Configuration.ConfigurationSection settings;

        /// <summary>
        /// Gets or sets the instance of the WCF listen connector when running in process mode.
        /// This value will be null when running in hosted mode (for example, when running in IIS).
        /// </summary>
        internal WcfServerConnector ListenConnector { get; set; }

        #region IConnectorContract Members

        /// <summary>
        /// Process any message from the channel
        /// </summary>
        /// <param name="m">The message to be processed</param>
        /// <returns>Any message</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.ServiceModel.FaultReason.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public System.ServiceModel.Channels.Message ProcessInboundMessage(System.ServiceModel.Channels.Message m)
        {

            #if DEBUG
            Trace.TraceInformation("Received message on transport...");
            #endif

            if (ListenConnector == null && OperationContext.Current.Host is WcfServiceHost)
                ListenConnector = (OperationContext.Current.Host as WcfServiceHost).ConnectorHost;

            if (ListenConnector != null) // In process
            {
                // Is this channel one way or two way?
                if (!(OperationContext.Current.Channel is IOutputChannel)) // Input only
                    return null;

                #if DEBUG
                Trace.TraceInformation("Message handoff to WcfServerConnector completed");
                #endif

                WcfSendResult processResult = ListenConnector.ProcessMessage(m);
                Message retVal = null;

                // There is an error, so the return value must be a fault!
                if (processResult == null || processResult.Code != ResultCode.Accepted && processResult.Code != ResultCode.AcceptedNonConformant)
                {
                    // Web based context?
                    if (WebOperationContext.Current != null)
                    {
                        WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                        WebOperationContext.Current.OutgoingResponse.StatusDescription = "Internal Server Error";
                    }

                    List<String> details = new List<string>();
                    if (processResult != null)
                    {
                        details = new List<String>();
                        foreach(var dtl in processResult.Details)
                            details.Add(dtl.Message); // Append details
                    }

                    if (processResult == null)
                        retVal = Message.CreateMessage(m.Version, MessageFault.CreateFault(FaultCode.CreateReceiverFaultCode(processResult.Code.ToString(), "http://marc.mohawkcollege.ca/hi"), new FaultReason("The receiver has constructed an invalid response that cannot be sent to the sender"), details), m.Headers.Action);
                    else
                        retVal = Message.CreateMessage(m.Version, MessageFault.CreateFault(FaultCode.CreateSenderFaultCode("EPIC", "http://marc.mohawkcollege.ca/hi"), new FaultReason("Catastrophic failure occurred in the WcfServer send pipeline. This usually occurs when the connector does not receive a message in the allocated amount of time"), details), m.Headers.Action);
                }
                else
                {
                    retVal = processResult.Message;

                    if (processResult.Headers != null)
                    {
                        retVal.Headers.Clear();
                        retVal.Headers.CopyHeadersFrom(processResult.Headers);
                    }
                }
                                
                #if DEBUG
                Trace.TraceInformation("Message sent to client");
                #endif

                return retVal;
            }
            else
            {
                // Get settings
                if(settings == null) settings = System.Web.Configuration.WebConfigurationManager.GetSection("marc.everest.connectors.wcf") as MARC.Everest.Connectors.WCF.Configuration.ConfigurationSection;

                // Now format the message and pass it on ...
                MemoryStream ms = new MemoryStream();
                System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(ms); // Write to message memory stream for classification matching
                m.WriteMessage(xw);
                xw.Flush(); // Flush the Xml Writer

                ms.Seek(0, SeekOrigin.Begin);                  // Seek to start
                XPathDocument xpd = new XPathDocument(ms); // load xpath document
                XPathNavigator xpn = xpd.CreateNavigator();

                IMessageReceiver receiver = null; // The receiver to use

                // Determine the receiver
                foreach (KeyValuePair<String, IMessageReceiver> kv in settings.Receiver)
                    if (xpn.SelectSingleNode(kv.Key) != null)
                    {
                        receiver = kv.Value;
                        break;
                    }

                // Was a receiver found?
                if (receiver == null)
                { 
                    // Create a not implemented exception
                    if(WebOperationContext.Current != null)
                        WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotImplemented;
                    return Message.CreateMessage(m.Version, MessageFault.CreateFault(FaultCode.CreateSenderFaultCode(
                        "NotImplemented", "http://marc.mohawkcollege.ca/hi"), new FaultReason("No receiver understands the request message.")),
                        m.Headers.Action);
                }

                // Create a streams for deserialization
                ms = new MemoryStream();
                XmlWriterSettings xws = new XmlWriterSettings();
                xws.Indent = true;
                xw = XmlWriter.Create(ms, xws); 

                // Deserialize body
                WcfReceiveResult rcv = new WcfReceiveResult();
                try
                {
                    // Because of classification, we need to process this in a wierd way, 
                    // Basically the formatter will classify the message based on the root element name
                    // it receives. Because a SOAP message's root isn't what we really want to process, 
                    // the first child node under the 'body' must be passed to the xml writer
                    xpn.SelectSingleNode("//*[local-name() = 'Body']/child::node()").WriteSubtree(xw);
                    xw.Flush();
                    ms.Seek(0, SeekOrigin.Begin);

                    var serResult = settings.Formatter.Parse(ms);
                    rcv.Structure = serResult.Structure;
                    rcv.Details = serResult.Details;
                    if (rcv.Details.Count() == 0)
                        rcv.Code = ResultCode.Accepted;
                    else
                        rcv.Code = ResultCode.AcceptedNonConformant;
                }
                catch (Exception e)
                {
                    rcv.Code = ResultCode.Error;
                    rcv.Details = new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) };
                }

                // Process on the receiver
                IGraphable obj = receiver.MessageReceived(rcv.Structure, rcv.Code, rcv.Details);

                // Graph this back
                XmlSerializerSurrogate surrogate = new XmlSerializerSurrogate((IXmlStructureFormatter)settings.Formatter);

                // Serialize the response
                Message result = Message.CreateMessage(m.Version, m.Headers.Action, obj, surrogate);
                
                // Validate
                surrogate.WriteObject(new MemoryStream(), obj);

                // Surrogate result code is acceptable?
                if (surrogate.ResultCode != ResultCode.Accepted && surrogate.ResultCode != ResultCode.AcceptedNonConformant)
                {
                    if (WebOperationContext.Current != null)
                    {
                        WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                        WebOperationContext.Current.OutgoingResponse.StatusDescription = "Internal Server Error";
                    }

                    List<string> details = new List<String>();
                    foreach(var itm in surrogate.Details)
                        details.Add(itm.Message); // Append details
                    return Message.CreateMessage(m.Version, MessageFault.CreateFault(FaultCode.CreateReceiverFaultCode(surrogate.ResultCode.ToString(), "http://marc.mohawkcollege.ca/hi"), new FaultReason("The receiver has constructed an invalid response that cannot be returned to the sender"), details), m.Headers.Action);
                }
                else
                    return result;

            }
        }

        #endregion

        #region IPolicyAccessContract Members

        /// <summary>
        /// Get client access policy
        /// </summary>
        public Stream GetClientAccessPolicy()
        {
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/xml";
            return new MemoryStream(Encoding.UTF8.GetBytes(@"<?xml version=""1.0"" encoding=""utf-8""?>
            <access-policy>
            <cross-domain-access>
             <policy>
                <allow-from http-request-headers=""*"">
                <domain uri=""*""/>
            </allow-from>
            <grant-to>
                <resource path=""/"" include-subpaths=""true""/>
            </grant-to>
        </policy>
        </cross-domain-access>
        </access-policy>"));
        }

        #endregion
    }
}