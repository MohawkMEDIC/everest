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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10.DynamicModel
{
    /// <summary>
    /// Represents an HL7 interaction definition
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Hl"), XmlType(TypeName = "Hl7Interaction", Namespace = "urn:hl7-org:v3/mif")]
    public class Hl7Interaction
    {
        
        private string name;
        private string title;
        private List<BusinessName> businessName;
        private PackageRef packageLocation;
        private Annotation annotations;
        private PackageRef callAction;
        private ParameterTypeModel parameterTypeModel;
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("parameterTypeModel")]
        public ParameterTypeModel ParameterTypeModel
        {
            get { return parameterTypeModel; }
            set { parameterTypeModel = value; }
        }
	
        /// <summary>
        /// Appears to be the allowed trigger event or call action for an element
        /// </summary>
        [XmlElement("callAction")]
        public PackageRef CallAction
        {
            get { return callAction; }
            set { callAction = value; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations
        {
            get { return annotations; }
            set { annotations = value; }
        }

        /// <summary>
        /// Location of the package this interaction references as its root name
        /// </summary>
        [XmlElement("packageLocation")]
        public PackageRef PackageLocation
        {
            get { return packageLocation; }
            set { packageLocation = value; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("businessName")]
        public List<BusinessName> BusinessName
        {
            get { return businessName; }
            set { businessName = value; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("title")]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
	
    }
}