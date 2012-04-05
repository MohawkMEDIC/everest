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
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// UML package stereotype
    /// </summary>
    [XmlType(TypeName = "Package", Namespace = "urn:hl7-org:v3/mif2")]
    public abstract class Package : PackageArtifact
    {

        private string hashCode;
        private PackageKind packageKind;
        private string secondaryId;
        private Header header;
        private List<PackageRef> replaces;


        /// <summary>
        /// An object identifier issued to the package or model as a supplementary
        /// id used for locating the object in registries, etc.
        /// </summary>
        [XmlAttribute("secondaryId")]
        public string SecondaryId
        {
            get { return secondaryId; }
            set { secondaryId = value; }
        }
       
        /// <summary>
        /// Indicates the package and versions (or packges) that are intended to be superseded by this package
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement(ElementName = "replaces")]
        public List<PackageRef> Replaces
        {
            get { return replaces; }
            set { replaces = value; }
        }

        /// <summary>
        /// General meta-data information about the package
        /// </summary>
        [XmlElement("header")]
        public Header Header
        {
            get { return header; }
            set { header = value; }
        }

	
        /// <summary>
        /// The level or variety of package being expressed
        /// </summary>
        [XmlAttribute("packageKind")]
        public PackageKind PackageKind
        {
            get { return packageKind; }
            set { packageKind = value; }
        }


        /// <summary>
        /// A base64 encoded 160 bit SHA-1 hashcode. The hashcode will be calculated upon the full 
        /// canonicalized content of the package transformed to exclude the hashcode.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods"), XmlAttribute("hashCode")]
        public string HashCode
        {
            get { return hashCode; }
            set { hashCode = value; }
        }

    }
}