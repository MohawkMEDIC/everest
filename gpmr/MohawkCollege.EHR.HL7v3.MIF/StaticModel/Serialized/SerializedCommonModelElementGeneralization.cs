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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10.StaticModel.Serialized
{
    /// <summary>
    /// The generalization for a common model element stereotype in a serialized static model
    /// </summary>
    [XmlType(TypeName = "SerializedCommonModelElementGeneralization", Namespace = "urn:hl7-org:v3/mif")]
    public class SerializedCommonModelElementGeneralization : CommonModelElementGeneralizationBase
    {
        private List<SerializedClassBindingArgument> supplierBindingArgument;
        
        /// <summary>
        /// Identifies the classes that are to be bound to a parameterized common model element
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("supplierBindingArgument")]
        public List<SerializedClassBindingArgument> SupplierBindingArgument
        {
            get { return supplierBindingArgument; }
            set { supplierBindingArgument = value; }
        }
	
    }
}