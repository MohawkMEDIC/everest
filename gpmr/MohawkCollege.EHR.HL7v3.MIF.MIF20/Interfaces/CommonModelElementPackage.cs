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
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Interfaces
{
    /// <summary>
    /// A package of a common model element interface that can be
    /// imported into a static model for reference
    /// </summary>
    //[XmlRoot(ElementName = "commonModelElementPackage", Namespace = "urn:hl7-org:v3/mif2")]
    [XmlRoot(ElementName = "staticModelInterfacePackage", Namespace = "urn:hl7-org:v3/mif2")]
    [XmlType(TypeName = "CommonModelElementPackage", Namespace = "urn:hl7-org:v3/mif2")]
    public class CommonModelElementPackage : Package
    {

        private string schemaVersion;

        /// <summary>
        /// Identifies the version of the schema that this interaction is claiming conformance against
        /// </summary>
        [XmlAttribute("schemaVersion")]
        public string SchemaVersion
        {
            get { return schemaVersion; }
            set { schemaVersion = value; }
        }

        private List<CommonModelElement> ownedModelElements;
        
        /// <summary>
        /// The vocabulary model that is used in defining the interfaces by this model
        /// </summary>
        [XmlElement("importedVocabularyModelPackage")]
        public PackageRef ImportedVocabularyModelPackage { get; set; }

        /// <summary>
        /// The other static model interface packages that are being included in the current one. Packages are processed
        /// in the order listed with duplicate context interfaces in subsequent packages overriding
        /// interfaces imported from prior packages.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<PackageRef> ImportedStaticModelInterfacePackage { get; set; }

        /// <summary>
        /// One of the CMET definitions included in the common model element
        /// interface package
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("commonModelElementDefinition")]
        public List<CommonModelElement> CommonModelElements
        {
            get { return ownedModelElements; }
            set { ownedModelElements = value; }
        }

        /// <summary>
        /// One of the stub definitions included in the static model interface package.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("stubDefinition")]
        public List<StubDefinition> Stubs { get; set; }

        /// <summary>
        /// Initialize the common model element package 
        /// </summary>
        internal override void Initialize()
        {
            foreach (CommonModelElement cmet in CommonModelElements)
                cmet.Container = this;
            foreach (StubDefinition stub in Stubs ?? new List<StubDefinition>())
                stub.Container = this;
        }
    }
}