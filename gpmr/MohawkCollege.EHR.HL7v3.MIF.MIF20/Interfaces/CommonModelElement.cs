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
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Interfaces
{
    /// <summary>
    /// Used to define CMETS
    /// </summary>
    [XmlType(TypeName = "CommonModelElement", Namespace = "urn:hl7-org:v3/mif2")]
    public class CommonModelElement : ClassifierBase
    {

        private Package container;
        private string name;
        private string attributionLevel;
        private CMETEntryKind entryKind;
        private Annotation annotations;
        private PackageRef specializationChildStaticModel;
        private List<StaticModel.StaticModelClassTemplateParameter> templateParameter;
        private SpecializationClass specializationChildEntryClass;

        /// <summary>
        /// Indicates the name of the root class of the CMETS
        /// </summary>
        [XmlElement("entryClass")]
        public SpecializationClass EntryClass
        {
            get { return specializationChildEntryClass; }
            set { specializationChildEntryClass = value; }
        }
	
        /// <summary>
        /// Indentifies the parameters associated with the CMET that must be bound if the CMET
        /// is referenced
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("templateParameter")]
        public List<StaticModel.StaticModelClassTemplateParameter> TemplateParameter
        {
            get { return templateParameter; }
            set { templateParameter = value; }
        }

        /// <summary>
        /// The ID of the model that implements the common model element
        /// </summary>
        [XmlElement("boundStaticModel")]
        public PackageRef BoundStaticModel
        {
            get { return specializationChildStaticModel; }
            set { specializationChildStaticModel = value; }
        }
	
        /// <summary>
        /// Descriptive information about the containing element
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations
        {
            get { return annotations; }
            set { annotations = value; }
        }
	
        /// <summary>
        /// Identifies the means by which the CMET can be entered
        /// </summary>
        [XmlAttribute("entryKind")]
        public CMETEntryKind EntryKind
        {
            get { return entryKind; }
            set { entryKind = value; }
        }

        /// <summary>
        /// Identifies the level of detail associated with the CMET
        /// </summary>
        [XmlAttribute("attributionLevel")]
        public string AttributionLevel
        {
            get { return attributionLevel; }
            set { attributionLevel = value; }
        }

        /// <summary>
        /// The identifier of the model
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
	
        /// <summary>
        /// The container package
        /// </summary>
        [XmlIgnore()]
        public Package Container
        {
            get { return container; }
            set { container = value; }
        }
	
    }
}