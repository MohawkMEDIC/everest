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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.DynamicModel
{
    /// <summary>
    /// Identifies the allowed types of interactions
    /// </summary>
    [XmlType(TypeName = "InteractionKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum InteractionKind
    {
        /// <summary>
        /// Solicits data from the reciever
        /// </summary>
        Query,
        /// <summary>
        /// Returns requested data to the query initiator or an indication that requested
        /// data is unavailable.
        /// </summary>
        QueryResponse,
        /// <summary>
        /// Informs the receiver about the occurence of a trigger event, along with 
        /// full or partial data related to that trigger event.
        /// </summary>
        EventNotification,
        /// <summary>
        /// Solicits a specific action(trigger event) from the receiver. Must be an action the receiving role is 
        /// theoretically capable of
        /// </summary>
        RequestForAction,
        /// <summary>
        /// Notification of the agreement of conditional agreement to perform the requested action
        /// </summary>
        RequestResponseAccept,
        /// <summary>
        /// Notification of the refusal to perform the requested action
        /// </summary>
        RequestResponseRefuse,
        /// <summary>
        /// Transmission of data that is independent of the occurence of any state-transition event or 
        /// other interaction.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Untriggered")]
        UntriggeredNotification
    }
}