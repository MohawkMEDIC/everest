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
 * User: Justin Fyfe
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// State machine
    /// </summary>
    [XmlType(TypeName = "StateMachine", Namespace = "urn:hl7-org:v3/mif2")]
    public class StateMachine
    {

        private string supplierStateAttributeName;
        private Annotation annotations;
        private List<State> subState;
        private List<Transition> transition;

        /// <summary>
        /// Defines a permitted movement between states.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement(ElementName = "transition")]
        public List<Transition> Transition
        {
            get { return transition; }
            set { transition = value; }
        }
	
        /// <summary>
        /// Identifies a mode in which instantiations of the class can exist
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement(ElementName = "subState")]
        public List<State> SubState
        {
            get { return subState; }
            set { subState = value; }
        }
	
        /// <summary>
        /// Descriptive information about the containing element
        /// </summary>
        [XmlElement(ElementName = "annotations")]
        public Annotation Annotations
        {
            get { return annotations; }
            set { annotations = value; }
        }
	
        /// <summary>
        /// The name of the class attribute which represents the state of the class (and whose value is
        /// drawn from the list of states in the classes state engine)
        /// </summary>
        [XmlAttribute("stateAttributeName")]
        public string SupplierStateAttributeName
        {
            get { return supplierStateAttributeName; }
            set { supplierStateAttributeName = value; }
        }
	
    }
}