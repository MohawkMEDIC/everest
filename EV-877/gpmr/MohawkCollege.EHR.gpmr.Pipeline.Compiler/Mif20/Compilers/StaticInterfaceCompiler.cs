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
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Interfaces;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository;
using MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Compilers
{
    /// <summary>
    /// Summary of StaticInterfaceCompiler
    /// </summary>
    public class StaticInterfaceCompiler : IPackageCompiler
    {
        #region IPackageCompiler Members

        private CommonModelElementPackage staticModel;
        private PackageRepository packageRepository;

        /// <summary>
        /// The type of package this compiler will process
        /// </summary>
        public Type PackageType
        {
            get { return typeof(CommonModelElementPackage); }
        }

        /// <summary>
        /// Set the package that is being processed
        /// </summary>
        public object Package
        {
            set { staticModel = value as CommonModelElementPackage; }
        }

        /// <summary>
        /// The package repository being processed
        /// </summary>
        public object PackageRepository
        {
            get
            {
                return packageRepository;
            }
            set
            {
                packageRepository = value as PackageRepository;
            }
        }

        #endregion

        #region ICompiler Members

        /// <summary>
        /// The class repository that contains all of the compiled COR classes
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public MohawkCollege.EHR.gpmr.COR.ClassRepository ClassRepository { get; set; }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void Compile()
        {

            System.Diagnostics.Trace.WriteLine(string.Format("Compiling static model interface package '{0}'...", staticModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT)), "debug");

            // Check if the package has already been "compiled"
            if (ClassRepository.ContainsKey(staticModel.Name))
                return; // Already compiled

            // Process each element
            foreach (CommonModelElement cme in staticModel.CommonModelElements)
                CommonTypeReferenceParser.Parse(cme, ClassRepository);

            if (staticModel.Stubs != null)
                foreach (StubDefinition stub in staticModel.Stubs)
                    CommonTypeReferenceParser.Parse(stub, ClassRepository); ;

        }

        #endregion
    }
}