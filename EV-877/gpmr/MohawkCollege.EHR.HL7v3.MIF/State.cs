/* 
 * Copyright 2008/2009 Mohawk College of Applied Arts and Technology
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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10
{
    /// <summary>
    /// Represents both simple states and composite states
    /// </summary>
    [XmlType(TypeName = "State", Namespace = "urn:hl7-org:v3/mif")]
    public class State : ModelElement
    {

        private string name;
        private string parentStateName;
        private List<BusinessName> businessName;
        private Annotation annotations;
        private List<StateDerivation> derivationSupplier;

        /// <summary>
        /// Identifies the corresponding state in a model from which the current model has been
        /// derived
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement(ElementName = "derivationSupplier")]
        public List<StateDerivation> DerivationSupplier
        {
            get { return derivationSupplier; }
            set { derivationSupplier = value; }
        }
	
        /// <summary>
        /// Descriptive name about the containing information
        /// </summary>
        [XmlElement(ElementName = "annotations")]
        public Annotation Annotations
        {
            get { return annotations; }
            set { annotations = value; }
        }
	
        /// <summary>
        /// Business names associated with the element
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement(ElementName = "businessName")]
        public List<BusinessName> BusinessName
        {
            get { return businessName; }
            set { businessName = value; }
        }
	
        /// <summary>
        /// Identifies the name of the state of which this is a sub state
        /// </summary>
        [XmlAttribute("parentStateName")]
        public string ParentStateName
        {
            get { return parentStateName; }
            set { parentStateName = value; }
        }
	
        /// <summary>
        /// the formal name for the state
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
	
    }
}