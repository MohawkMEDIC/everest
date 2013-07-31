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
 **/
using System;
using System.Collections.Generic;
using System.Text;
using MohawkCollege.EHR.HL7v3.MIF.MIF20;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat;
using MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Compilers;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Serialized;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers
{
    /// <summary>
    /// Summary of PackageParser
    /// </summary>
    internal class PackageParser
    {
        /// <summary>
        /// Parse a package based on its name
        /// </summary>
        /// <param name="PackageName">The name of the package to parse</param>
        /// <param name="mifRepo">The MIF repository</param>
        /// <param name="repo">The class repository to store classes in</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        internal static void Parse(string PackageName, PackageRepository mifRepo, ClassRepository repo)
        {
            // Find the package that contains the class we want
            Package pkg = mifRepo.Find(p => p.PackageLocation.ToString(MifCompiler.NAME_FORMAT) == PackageName) as Package;
            
            if (pkg == null) // Check if package was found
            {
                System.Diagnostics.Trace.WriteLine(string.Format("Could not find static model '{0}'. Any dependent models may not be rendered correctly!", PackageName), "warn");
                return;
            }

            // Process the package
            IPackageCompiler comp = null;
            if (pkg is GlobalStaticModel)
                comp = new StaticModelCompiler();
            else if (pkg is SerializedStaticModel)
                comp = new SerializedStaticModelCompiler();
            else
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Can't find an appropriate compiler for package '{0}'... Package will not be parsed", PackageName), "error");
                return;
            }
            comp.ClassRepository = repo;
            comp.Package = pkg;
            comp.PackageRepository = pkg.MemberOfRepository;
            comp.Compile();
        }

        /// <summary>
        /// Parse a package based on the class that is in that package
        /// </summary>
        /// <param name="ClassName">Full name of the class</param>
        /// <param name="mifRepo">The MIF repository</param>
        /// <param name="repo">The class repository to store classes in</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)")]
        internal static void ParseClassFromPackage(string ClassName, PackageRepository mifRepo, ClassRepository repo)
        {
            // Class and package location
            string classNamePart = ClassName.Substring(ClassName.IndexOf(".") + 1);
            string packageLocationPart = ClassName.Substring(0, ClassName.IndexOf("."));

            // Find the package that contains the class we want
            GlobalStaticModel pkg = mifRepo.Find(p => (p is GlobalStaticModel) &&
                (p as GlobalStaticModel).OwnedClass.Find(c => c.Choice is MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat.Class &&
                    (c.Choice as MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat.Class).Name == classNamePart) != null &&
                p.PackageLocation.ToString(MifCompiler.NAME_FORMAT) == packageLocationPart) as GlobalStaticModel;


            // Process the package
            if (pkg == null)
                throw new InvalidOperationException(string.Format("Can't find '{0}' in the package repository, cannot continue processing", ClassName));

            StaticModelCompiler comp = new StaticModelCompiler();
            comp.ClassRepository = repo;
            comp.Package = pkg;
            comp.PackageRepository = pkg.MemberOfRepository;
            comp.Compile();
        }
    }
}