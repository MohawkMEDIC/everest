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
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Abstract parent of artifacts that have general characteristics of packages but aren't 
    /// actually packages
    /// </summary>
    [XmlType(TypeName = "PackageArtifact", Namespace = "urn:hl7-org:v3/mif2")]
    public abstract class PackageArtifact : PackageBase
    {
        private string title;
        private PackageRef packageLocation;
        private PackageRepository memberOf;

        /// <summary>
        /// Identifies the package repository this package is a member of
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlIgnore()]
        public PackageRepository MemberOfRepository
        {
            get { return memberOf; }
            set { memberOf = value; }
        }
	

        /// <summary>
        /// Identifies where (within the repository package hierarchy) this package resides
        /// </summary>
        [XmlElement("packageLocation")]
        public PackageRef PackageLocation
        {
            get { return packageLocation; }
            set { packageLocation = value; }
        }
	
        /// <summary>
        /// The descriptive name for the package in circumstances where the name is more of an identifier
        /// </summary>
        [XmlAttribute("title")]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }


        /// <summary>
        /// Initialize all child members of this package
        /// </summary>
        internal abstract void Initialize();

        /// <summary>
        /// Merge a package artifact with another package artifact
        /// </summary>
        //internal abstract void Merge(PackageArtifact pkg);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class Comparator : IComparer<PackageArtifact>
        {
            #region IComparer<PackageArtifact> Members
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(PackageArtifact x, PackageArtifact y)
            {
                int xval = (int)x.packageLocation.Artifact;
                int yval = (int)y.packageLocation.Artifact;

                return xval.CompareTo(yval);

                //if (x.packageLocation.Artifact == ArtifactKind.VO && y.packageLocation.Artifact
                //if (x.packageLocation.Artifact == ArtifactKind.VO || x.packageLocation.Artifact == ArtifactKind.IFC ||
                //    x.packageLocation.Artifact == ArtifactKind.RIM)
                //    return -1;
                //return 0;
            }

            #endregion
        }
    }
}
