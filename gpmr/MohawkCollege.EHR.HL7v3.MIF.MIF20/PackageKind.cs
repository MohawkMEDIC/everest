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
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Identifies the level associated with a particular package
    /// </summary>
    [XmlType(TypeName = "PackageKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum PackageKind
    {
        /// <summary>
        /// Indicates the fundamental categorization of the package
        /// </summary>
        [XmlEnum("root")]
        Root,
        /// <summary>
        /// Indicates a broad categorization of the business area
        /// </summary>
        [XmlEnum("section")]
        Section,
        /// <summary>
        /// Indicates a more specific categorization of the business area
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "SubSection")]
        [XmlEnum("subSection")]
        SubSection,
        /// <summary>
        /// Indicates a specific business area
        /// </summary>
        [XmlEnum("domain")]
        Domain,
        /// <summary>
        /// Indicates the geographic area covered by the package
        /// </summary>
        [XmlEnum("realmNamespace")]
        Realm,
        /// <summary>
        /// Indicates the iteration or revision number of a set of content
        /// </summary>
        [XmlEnum("version")]
        Version,
        /// <summary>
        /// Indicates a type of business object stored in the package
        /// </summary>
        [XmlEnum("artifact")]
        Artifact,
        /// <summary>
        /// Indicates a sub-type of business object stored in the package
        /// </summary>
        [XmlEnum("subArtifact")]
        SubArtifact,
        /// <summary>
        /// A descriptive label for a particular artifact
        /// </summary>
        [XmlEnum("name")]
        Name, 
        /// <summary>
        /// A numeric label for a particular artifact
        /// </summary>
        [XmlEnum("id")]
        Id
    }
}