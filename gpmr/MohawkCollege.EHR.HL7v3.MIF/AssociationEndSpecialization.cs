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
    /// Indeitifes a class that specialize the participant class for this association end
    /// </summary>
    [XmlType(TypeName = "AssociationEndSpecialization", Namespace = "urn:hl7-org:v3/mif")]
    public class AssociationEndSpecialization
    {
        private string className;
        private string traversalName;
        private List<AssociationEndSpecialization> specialization;

        /// <summary>
        /// For specialization that are choices or CMETs whose root is a choice, identifies the classes within
        /// the choice and the association names tied to those classes
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("specialization")]
        public List<AssociationEndSpecialization> Specialization
        {
            get { return specialization; }
            set { specialization = value; }
        }
	
        /// <summary>
        /// Name of the element when traversing from the association end directly to the specialized class
        /// </summary>
        [XmlAttribute("traversalName")]
        public string TraversalName
        {
            get { return traversalName; }
            set { traversalName = value; }
        }
	
        /// <summary>
        /// Name of the class
        /// </summary>
        [XmlAttribute("className")]
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }
	
    }
}