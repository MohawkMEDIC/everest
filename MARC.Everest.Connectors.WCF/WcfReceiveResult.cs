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
using System.ServiceModel.Channels;

namespace MARC.Everest.Connectors.WCF
{
    
    /// <summary>
    /// The WCF Receive result is an implementation of the <see cref="IReceiveResult"/> 
    /// </summary>
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class WcfReceiveResult : IReceiveResult
    {
        #region IReceiveResult Members

        /// <summary>
        /// Gets or sets the result code from the receive operation. Any result other than Accepted or AcceptedNonConformant indicates the entire message may not be available.
        /// </summary>
        public ResultCode Code { get; set; }
        /// <summary>
        /// Gets or sets the details of the result. If the <see cref="ResultCode">ResultCode</see> is not Accepted, this list will contain at least one detail item that is an error.
        /// </summary>
        public IEnumerable<IResultDetail> Details { get; set; }
        /// <summary>
        /// Gets or sets the actual result.
        /// </summary>
        public MARC.Everest.Interfaces.IGraphable Structure { get; set; }
        /// <summary>
        /// Gets or sets the id of the message to respond to.
        /// </summary>
        internal Guid MessageIdentifier { get; set; }
        
        /// <summary>
        /// Gets or sets the version of the original message. This is for correlation purposes.
        /// </summary>
        internal MessageVersion MessageVersion { get { return messageVersion; } set { messageVersion = value; } }
#if !WINDOWS_PHONE
        [NonSerialized]
#endif
        private MessageVersion messageVersion;
        /// <summary>
        /// Gets the headers that were sent from the soliciting applciation
        /// </summary>
        public MessageHeaders Headers { get; internal set; }
        /// <summary>
        /// Gets or sets a series of message headers that should be included in responses to 
        /// clients
        /// </summary>
        /// <remarks>This property is intended to be used by users of the <see cref="T:MARC.HI.Everest.Connectors.WCF.WcfServerConnector"/> connector. For
        /// sending headers via the client connector, see the <see cref="F:MARC.HI.Everest.Connectors.WCF.WcfReceiveConnector.Send(MARC.Everest.Interfaces.IGraphable, System.ServiceModel.MessageHeaders)"/> method</remarks>
        public MessageHeaders ResponseHeaders { get; set; }
        #endregion
    }
}