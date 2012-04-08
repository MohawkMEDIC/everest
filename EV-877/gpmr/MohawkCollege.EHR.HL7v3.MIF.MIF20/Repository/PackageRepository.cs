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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository
{
    /// <summary>
    /// The PackageRepository contains a complete reference of all packages loaded into memory that
    /// other packages can reference. It is really a dictionary whereby the Key is the unique name
    /// of the package.
    /// </summary>
    /// <remarks>
    /// Repository key is :
    /// Realm/Root/Section/SubSection/Domain/Artifact/Id
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public class PackageRepository : List<PackageArtifact>, IComparer<PackageArtifact>
    {

        /// <summary>
        /// Find a package by its reference
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public PackageArtifact Find(PackageRef refName)
        {

            // First, attempt via name
            PackageArtifact pkg = this.Find(o => o.PackageLocation.Equals(refName, true));

            if (null != pkg)
                return pkg;
            else             // Warn and then try forgetting the Version
            {
                System.Diagnostics.Trace.Write(String.Format("Can't locate package '{0}', attempting to locate older version", refName.ToString("%s%%d%_%A%%i%%r%%v%")), "warn1");
                return this.Find(o => o.PackageLocation.Equals(refName, false));
            }
        }

        /// <summary>
        /// Add a package to this repository
        /// </summary>
        /// <param name="pkg">The package to add</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "pkg"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void Add(PackageArtifact pkg)
        {
            pkg.Initialize();

            // HACK: JF, the UV Mifs don't necessarily place their ID in the package location, so we do it for them
            if(pkg is Package && (pkg as Package).PackageKind == PackageKind.Id)
                pkg.PackageLocation.Id = pkg.PackageLocation.Id ?? pkg.Name;

            pkg.MemberOfRepository = this;
            try
            {
                // Get an existing package
                PackageArtifact p = this.Find(o => o.PackageLocation.Equals(pkg.PackageLocation, true));

                if (p == null)
                    base.Add(pkg);
                else
                    System.Diagnostics.Trace.Write("Package has already been processed. Please use a unique name (" + pkg.PackageLocation.ToString("%s%%d%_%A%%i%%r%%v%") + ", " + pkg.Name + ")", "warn");

            }
            catch (Exception e)
            {
                throw new InvalidOperationException(String.Format("Can't add '{0}' to repository", String.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}",
                                            pkg.PackageLocation.Realm,
                                            pkg.PackageLocation.Root,
                                            pkg.PackageLocation.Section,
                                            pkg.PackageLocation.SubSection,
                                            pkg.PackageLocation.Domain,
                                            pkg.PackageLocation.Artifact,
                                            pkg.PackageLocation.Id)), e);
            }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])")]
        public override string ToString()
        {
            // Build output
            StringBuilder output = new StringBuilder();
            output.AppendFormat("Package Repository ({0} Packages)\r\n", this.Count);
            foreach (PackageArtifact pkg in this)
            {
                string key = String.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}",
                                            pkg.PackageLocation.Realm,
                                            pkg.PackageLocation.Root,
                                            pkg.PackageLocation.Section,
                                            pkg.PackageLocation.SubSection,
                                            pkg.PackageLocation.Domain,
                                            pkg.PackageLocation.Artifact,
                                            pkg.PackageLocation.Id,
                                            pkg.PackageLocation.Version);
                output.AppendFormat("{0} ({1})\r\n", key, pkg.GetType().Name);
            }
            return output.ToString();
        }

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
            return ((int)x.PackageLocation.Artifact).CompareTo((int)y.PackageLocation.Artifact);
        }

        #endregion
    }
}