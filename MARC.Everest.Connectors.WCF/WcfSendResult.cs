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
using System.ServiceModel.Channels;

namespace MARC.Everest.Connectors.WCF
{
    /// <summary>
    /// Result of the WCF Send operation
    /// </summary>
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class WcfSendResult : ISendResult
    {
        /// <summary>
        /// New WcfSendResult
        /// </summary>
        public WcfSendResult()
        {
        }

        #region ISendResult Members

        /// <summary>
        /// Gets or sets the result code of the send operation. Any value other than Accepted and AcceptedNonConformant indicates the message may not have been sent.
        /// </summary>
        public ResultCode Code { get; set; }

        /// <summary>
        /// Gets or sets the details of the operation. If the <see cref="ResultCode"/> is not Accepted, this list will contain at least one error item.
        /// </summary>
        public IEnumerable<IResultDetail> Details { get; set; }

        /// <summary>
        /// Gets or sets the message to be sent via the WCF connector.
        /// </summary>
        internal Message Message { get; set; }

        /// <summary>
        /// Gets or sets the id of the message.
        /// </summary>
        internal Guid MessageId { get; set; }
        #endregion

        /// <summary>
        /// Gets or sets message headers to send back to the client
        /// </summary>
        internal MessageHeaders Headers { get; set; }
    }
}