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
using MARC.Everest.Interfaces;

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Event arguments for unsolicited data received
    /// </summary>
    /// <remarks>
    /// <para>
    /// This event arguments class is passed by the MessageReceived event found on
    /// <see cref="T:MARC.Everest.Connectors.IListenWaitConnector"/>. When a new message
    /// is ready to be processed by the <see cref="T:MARC.Everest.Connectors.IListenWaitConnector"/>, 
    /// it will fire the MessageReceived method. This event args class supplies data to the 
    /// subscribed event handlers for that event.
    /// </para>
    /// </remarks>
    public class UnsolicitedDataEventArgs : EventArgs
    {
        /// <summary>
        /// The remote enpoint address where the data was received from
        /// </summary>
        public Uri ReceiveEndpoint { get; private set; }
        /// <summary>
        /// The time the data was received
        /// </summary>
        public DateTime Timestamp { get; private set; }
        /// <summary>
        /// The endpoint of the solicitor
        /// </summary>
        public Uri SolicitorEndpoint { get; private set; }
        /// <summary>
        /// Create a new instance of the UnsolicitedDataEventArgs
        /// </summary>
        /// <param name="receiveEndpoint">The endpoint the data was received from</param>
        /// <param name="timestamp">The time the data was received</param>
        /// <param name="solicitorEndpoint">The solicitor of the data</param>
        public UnsolicitedDataEventArgs(Uri receiveEndpoint, DateTime timestamp, Uri solicitorEndpoint)
        {
            this.ReceiveEndpoint = receiveEndpoint; 
            this.Timestamp = timestamp;
            this.SolicitorEndpoint = solicitorEndpoint;
        }

    }
}