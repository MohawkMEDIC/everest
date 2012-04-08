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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10.Repository
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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2229:ImplementSerializationConstructors"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [Serializable]
    public class PackageRepository : Dictionary<String, Package>, IComparer<Package>
    {

        /// <summary>
        /// Find a package by its reference
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])")]
        public Package Find(PackageRef refName)
        {

            string name = String.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}",
                                        refName.Realm ?? "Universal",
                                        refName.Root,
                                        refName.Section,
                                        refName.SubSection,
                                        refName.Domain,
                                        refName.Artifact,
                                        refName.Id);
            // First, attempt via name

            if (this.ContainsKey(name))
                return this[name];

            return null;
        }

        /// <summary>
        /// Add a package to this repository
        /// </summary>
        /// <param name="pkg">The package to add</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "pkg"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void Add(Package pkg)
        {
            pkg.Initialize();

            pkg.MemberOfRepository = this;
            try
            {
                base.Add(String.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}",
                                            pkg.PackageLocation.Realm,
                                            pkg.PackageLocation.Root,
                                            pkg.PackageLocation.Section,
                                            pkg.PackageLocation.SubSection,
                                            pkg.PackageLocation.Domain,
                                            pkg.PackageLocation.Artifact,
                                            pkg.PackageLocation.Id ?? pkg.Name), pkg);
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
                                            pkg.PackageLocation.Id ?? pkg.Name)), e);
            }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Build output
            StringBuilder output = new StringBuilder();
            output.AppendFormat("Package Repository ({0} Packages)\r\n", this.Count);
            foreach (KeyValuePair<String, Package> record in this)
                output.AppendFormat("{0} ({1})\r\n", record.Key, record.Value.GetType().Name);

            return output.ToString();
        }

        #region IComparer<Package> Members
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Package x, Package y)
        {
            return ((int)x.PackageLocation.Artifact).CompareTo((int)y.PackageLocation.Artifact);
        }

        #endregion
    }
}