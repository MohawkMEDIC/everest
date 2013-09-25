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
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.DynamicModel
{
    /// <summary>
    /// A single exchange of data between systems for a particular reason with a set of expected response
    /// behaviors
    /// </summary>
    [XmlType(TypeName = "Interaction", Namespace = "urn:hl7-org:v3/mif2")]
    public abstract class Interaction : PackageArtifact
    {

        private InteractionKind interactionKind;
        private Annotation annotations;
        private PackageRef invokingTriggerEvent;
        private BoundStaticModel argumentMessage;
        private List<ReceiverResponsibility> receiverResponsibilities;
        private List<PackageRef> sendingApplication;
        private List<PackageRef> receivingApplication;

        /// <summary>
        /// A type of system which is capable of receiving the interaction
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("receivingApplication")]
        public List<PackageRef> ReceivingApplication
        {
            get { return receivingApplication; }
            set { receivingApplication = value; }
        }
	
        /// <summary>
        /// A type of system that is capable of sending the interaction
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("sendingApplication")]
        public List<PackageRef> SendingApplication
        {
            get { return sendingApplication; }
            set { sendingApplication = value; }
        }
	
        /// <summary>
        /// Identifies the behaviors the receiver has the choice to choose between
        /// when receiving this interaction. One an only one of the listed receiver 
        /// responsibilites must be execited
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("receiverResponsibilities")]
        public List<ReceiverResponsibility> ReceiverResponsibilities
        {
            get { return receiverResponsibilities; }
            set { receiverResponsibilities = value; }
        }

        /// <summary>
        /// Indicates the fully bound static model which defines the type for the content
        /// transmitted by the interaction
        /// </summary>
        [XmlElement("argumentMessage")]
        public BoundStaticModel ArgumentMessage
        {
            get { return argumentMessage; }
            set { argumentMessage = value; }
        }
	
        /// <summary>
        /// Identifies the trigger event which causes the interaction to be sent
        /// </summary>
        [XmlElement("invokingTriggerEvent")]
        public PackageRef InvokingTriggerEvent
        {
            get { return invokingTriggerEvent; }
            set { invokingTriggerEvent = value; }
        }
	
        /// <summary>
        /// Comments associated with an interaction
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations
        {
            get { return annotations; }
            set { annotations = value; }
        }
	
        /// <summary>
        /// Identifies the general pattern this interaction conforms to
        /// </summary>
        [XmlAttribute("interactionType")]
        public InteractionKind InteractionKind
        {
            get { return interactionKind; }
            set { interactionKind = value; }
        }
	
    }
}