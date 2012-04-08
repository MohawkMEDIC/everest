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
 * Date: November 22, 2008
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Serialized
{
    /// <summary>
    /// A static model in standard notation. Used for CIM, LIM, etc.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance"), XmlRoot(ElementName = "serializedStaticModel", Namespace = "urn:hl7-org:v3/mif2")]
    [XmlType(TypeName = "SerializedStaticModel", Namespace = "urn:hl7-org:v3/mif2")]
    public class SerializedStaticModel : StaticModelBase
    {

        private SerializedEntryPoint ownedEntryPoint;

        /// <summary>
        /// Identifies a class within the model that may be used as the initial class in a serialized 
        /// representation of this model
        /// </summary>
        // JF: Not sure why this is ownedEntryPoint here...
        //[XmlElement(ElementName = "ownedEntryPoint")]
        [XmlElement(ElementName = "entryPoint")]
        public SerializedEntryPoint OwnedEntryPoint
        {
            get { return ownedEntryPoint; }
            set { ownedEntryPoint = value; }
        }


        /// <summary>
        /// Initialize the static model
        /// </summary>
        internal override void Initialize()
        {
            if (OwnedEntryPoint.SpecializedClass.Item is SerializedClass)
                (OwnedEntryPoint.SpecializedClass.Item as SerializedClass).Container = this;
        }
    }
}