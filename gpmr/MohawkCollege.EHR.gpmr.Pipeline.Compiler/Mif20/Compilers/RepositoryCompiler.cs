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
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository;
using MohawkCollege.EHR.HL7v3.MIF.MIF20;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Compilers
{
    /// <summary>
    /// Responsible for converting a repository of MIF structures to a repository of COR structures
    /// </summary>
    internal class RepositoryCompiler : ICompiler
    {
        #region ICompiler Members

        private PackageRepository repository;
        
        /// <summary>
        /// Get or set the class repository that results from this compilation
        /// </summary>
        public MohawkCollege.EHR.gpmr.COR.ClassRepository ClassRepository
        {
            get; set;
        }

        /// <summary>
        /// Create a new instance of the repository compiler
        /// </summary>
        public RepositoryCompiler(PackageRepository pkrep)
        {
            this.repository = pkrep;
        }

        /// <summary>
        /// Compile the source package repository passed by parameter into a COR repository
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public void Compile()
        {
            
            // Get compilers that we can use to compile data
            Dictionary<Type, Type> compilers = new Dictionary<Type, Type>();

            foreach(Type t in this.GetType().Assembly.GetTypes())
                if (t.GetInterface("MohawkCollege.EHR.gpmr.Pipeline.Compiler.IPackageCompiler") != null &&
                    t.Namespace == "MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Compilers")
                {
                    IPackageCompiler c = (IPackageCompiler)this.GetType().Assembly.CreateInstance(t.FullName);
                    compilers.Add(c.PackageType, t);
                }

            // Prepare for compile
            repository.Sort(new PackageArtifact.Comparator());

            // Parse the sorted list
            foreach (PackageArtifact pkg in repository)
            {
                if (compilers.ContainsKey(pkg.GetType()))
                {
                    IPackageCompiler pkc = (IPackageCompiler)this.GetType().Assembly.CreateInstance(compilers[pkg.GetType()].FullName);
                    pkc.ClassRepository = this.ClassRepository;
                    pkc.Package = pkg;
                    pkc.PackageRepository = repository;
                    pkc.Compile();
                }
                else
                    System.Diagnostics.Trace.WriteLine(string.Format("Can't compile '{0}', no compiler understands {1}", pkg.PackageLocation.ToString(MifCompiler.NAME_FORMAT), pkg.GetType().Name), "warn");
            }
        }

        #endregion
    }
}
