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
 * User: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Used to make an absolute reference to other packages
    /// </summary>
    [XmlType(TypeName = "PackageRef", Namespace = "urn:hl7-org:v3/mif2")]
    public class PackageRef
    {
        private string root;
        private string section;
        private SubSectionKind subSection;
        private DomainKind domain;
        private string realm;
        private string version;
        private ArtifactKind artifact;
        private string subArtifact;
        private string name;
        private string id;
        private string creatorId;

        /// <summary>
        /// An oid distinguishing the namespace of a specific artifact creator
        /// </summary>
        [XmlAttribute("creatorId")]
        public string CreatorId
        {
            get { return creatorId; }
            set { creatorId = value; }
        }
	
        /// <summary>
        /// Indicates the identifier of the package
        /// </summary>
        [XmlAttribute("id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Indicates the name of the package
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
	
        /// <summary>
        /// Indicates the sub-category of the artifacts in the package
        /// </summary>
        [XmlAttribute("subArtifact")]
        public string SubArtifact
        {
            get { return subArtifact; }
            set { subArtifact = value; }
        }
	
        /// <summary>
        /// Identifies the category of the artifacts in the package
        /// </summary>
        [XmlAttribute("artifact")]
        public ArtifactKind Artifact
        {
            get { return artifact; }
            set { artifact = value; }
        }
	
        /// <summary>
        /// Identifies the name of the version of the package
        /// </summary>
        [XmlAttribute("version")]
        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        /// <summary>
        /// Indicates the geographic area covered by the package
        /// </summary>
        [XmlAttribute("realmNamespace")]
        public string Realm
        {
            get { return realm; }
            set { realm = value; }
        }
	
        /// <summary>
        /// Indicates the name of the domain package
        /// </summary>
        [XmlAttribute("domain")]
        public DomainKind Domain
        {
            get { return domain; }
            set { domain = value; }
        }
	
        /// <summary>
        /// Indicates the name of the sub-section package
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "SubSection"), XmlAttribute("subSection")]
        public SubSectionKind SubSection
        {
            get { return subSection; }
            set { subSection = value; }
        }
	
        /// <summary>
        /// Identifies the name of the section package
        /// </summary>
        [XmlAttribute("section")]
        public string Section
        {
            get { return section; }
            set { section = value; }
        }
	
        /// <summary>
        /// Indicates the fundamental categorization of the package
        /// </summary>
        [XmlAttribute("root")]
        public string Root
        {
            get { return root; }
            set { root = value; }
        }


        /// <summary>
        /// Represent this reference as a string    
        /// </summary>
        /// <param name="format">
        /// The format, valid characters are:
        /// <list>
        /// <item>%d% -   Domain</item>
        /// <item>%s% -   SubSection</item>
        /// <item>%S% -   Section</item>
        /// <item>%r% -   Realm</item>
        /// <item>%i% -   Id</item>
        /// <item>%A% -   Artifact</item>
        /// <item>%a% -   Subartifact</item>
        /// </list>
        /// </param>
        /// <returns></returns>
        public string ToString(String format)
        {

            string retString = format;

            retString = retString.Replace("%d%", Domain.ToString());
            retString = retString.Replace("%s%", SubSection.ToString());
            retString = retString.Replace("%r%", Realm);
            retString = retString.Replace("%i%", Id);
            retString = retString.Replace("%S%", Section);
            retString = retString.Replace("%A%", Artifact.ToString());
            retString = retString.Replace("%a%", SubArtifact);
            retString = retString.Replace("%v%", Version);
            return retString;
        }

        /// <summary>
        /// Determine if this package reference is equal to object <paramref name="obj"/>
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <param name="version">True if version information should be included</param>
        /// <returns>True if the two are equal</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "version"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj")]
        public bool Equals(object obj, bool version)
        {
            if (obj is PackageRef)
            {
                PackageRef ro = obj as PackageRef;
                return ro.Artifact == Artifact && ro.Domain == Domain && ro.Id == Id && ro.Name == Name &&
                    (ro.Realm ?? "UV") == (Realm ?? "UV") && ro.Root == Root && ro.Section == Section && ro.SubArtifact == SubArtifact &&
                    ro.SubSection == SubSection && (!version || ro.Version == "00" || (ro.Version == Version));
            }
            return base.Equals(obj);
        }

    }
}