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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10.StaticModel.Flat
{
    /// <summary>
    /// Defines a flat static model
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance"), XmlRoot(ElementName = "staticModel", Namespace = "urn:hl7-org:v3/mif")]
    [XmlType(TypeName = "StaticModel", Namespace = "urn:hl7-org:v3/mif")]
    public class StaticModel : StaticModelBase
    {

        private List<EntryPoint> ownedEntryPoint;
        private List<ClassElement> ownedClass;
        private List<Association> ownedAssociation;

        /// <summary>
        /// The associations that are part of this static model
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("ownedAssociation")]
        public List<Association> OwnedAssociation
        {
            get { return ownedAssociation; }
            set { ownedAssociation = value; }
        }

        /// <summary>
        /// The classes that are part of this model
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("ownedClass")]
        public List<ClassElement> OwnedClass
        {
            get { return ownedClass; }
            set { ownedClass = value; }
        }
	
        /// <summary>
        /// Identifies a class within the model that may be used as the initial class in a serializable
        /// representation of the model 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("ownedEntryPoint")]
        public List<EntryPoint> OwnedEntryPoint
        {
            get { return ownedEntryPoint; }
            set { ownedEntryPoint = value; }
        }

        /// <summary>
        /// Initialize the owned entry point
        /// </summary>
        internal override void Initialize()
        {

            foreach (ClassElement p in OwnedClass)
                if (p.Choice is Class)
                    (p.Choice as Class).Container = this;
        }
    }
}