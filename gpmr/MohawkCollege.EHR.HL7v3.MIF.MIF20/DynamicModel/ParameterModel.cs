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
    /// Identifies a parameter and the static model it is bound to
    /// </summary>
    [XmlType(TypeName = "ParameterModel", Namespace = "urn:hl7-org:v3/mif2")]
    public class ParameterModel : BoundStaticModel
    {
        private string parameterName;
        private string traversalName;
        private List<AssociationEndSpecialization> specialization;

        /// <summary>
        /// For specializers that are choices or CMETS whose root is 
        /// a choice, identifies the classes within the choice and the association names tied 
        /// to those classes.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("specialization")]
        public List<AssociationEndSpecialization> Specialization
        {
            get { return specialization; }
            set { specialization = value; }
        }
        
        /// <summary>
        /// Name of the element when traversing from the association end directly to
        /// the specialized class
        /// </summary>
        [XmlAttribute("traversalName")]
        public string TraversalName
        {
            get { return traversalName; }
            set { traversalName = value; }
        }
	
        /// <summary>
        /// The name of the stub parameter being bound
        /// </summary>
        [XmlAttribute("parameterName")]
        public string ParameterName
        {
            get { return parameterName; }
            set { parameterName = value; }
        }
	
    }
}