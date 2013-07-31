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
    /// Identifies a possible set of actions to be taken in response to the receipt of an interaction
    /// </summary>
    [XmlType(TypeName = "ReceiverResponsibility", Namespace = "urn:hl7-org:v3/mif2")]
    public class ReceiverResponsibility 
    {
        private PackageRef invokeTriggerEvent;
        private PackageRef invokeInteraction;
        private ComplexMarkupWithLanguage reason;

        /// <summary>
        /// Indicates the conditions under which this receiver responsibility should be chosen.
        /// Should be mutually exclusive with the conditions of all other receiver responsibilities
        /// for this interaction. Also, the combined reasons for all receiver responsibilities
        /// should be complete. IE: There should be no circumstance that doesn't fit into the
        /// reason of one and one receiver responsibility
        /// </summary>
        [XmlElement("reason")]
        public ComplexMarkupWithLanguage Reason
        {
            get { return reason; }
            set { reason = value; }
        }
	
        /// <summary>
        /// Indicates an interaction that should be returned to the sender of the original 
        /// interaction as an application acknowledgement
        /// </summary>
        [XmlElement("invokeInteraction")]
        public PackageRef InvokeInteraction
        {
            get { return invokeInteraction; }
            set { invokeInteraction = value; }
        }

        /// <summary>
        /// Indicates that the specified trigger event should be fired. Along with any 
        /// interactions associated with that trigger event and supported by any of the receiving
        /// system's application roles.
        /// </summary>
        [XmlElement("invokeTriggerEvent")]
        public PackageRef InvokeTriggerEvent
        {
            get { return invokeTriggerEvent; }
            set { invokeTriggerEvent = value; }
        }
	
    }
}