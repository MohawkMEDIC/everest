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
using MohawkCollege.EHR.gpmr.COR;
using System.Collections.Specialized;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository;
using MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Compilers;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler
{
    /// <summary>
    /// This class is responsible for converting the MIF1.0/2.0 Structured data from the MIF repository
    /// to the internal common representation of the MIF
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mif")]
    public class MifCompiler : IPipelineComponent
    {
        /// <summary>
        /// The pipeline this component is running in
        /// </summary>
        internal static Pipeline hostContext;
        /// <summary>
        /// Bindings for the CMET
        /// </summary>
        internal static Dictionary<String, String> cmetBindings = new Dictionary<string,string>();
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static string Language = "en";

        /// <summary>
        /// No longer required
        /// </summary>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        //public static string BindingRealm = "Canada";

        /// <summary>
        /// Format for package locations : sd_Air = RE PC _ IN 000076 CA
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NAME")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FORMAT")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public const string NAME_FORMAT = "%s%%d%_%A%%i%%r%%v%";

        #region Non Pipeline Compilation

        /// <summary>
        /// Compile MIF 2.0 Repository <paramref name="pkr"/> adding the contents to class repository
        /// <paramref name="rep"/>
        /// </summary>
        /// <param name="pkr">The MIF 2.0 repository to compile</param>
        /// <param name="rep">The repository to add to</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void CompileMif(MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository.PackageRepository pkr, ClassRepository rep)
        {
            
            MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Compilers.RepositoryCompiler util = new MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Compilers.RepositoryCompiler(pkr);
            util.ClassRepository = rep;
            util.Compile();
        }

        /// <summary>
        /// Compile MIF 2.0 repository <paramref name="pkr"/> into the common object represntation (COR) format
        /// </summary>
        /// <param name="pkr">The MIF2.0 package repository to compile</param>
        /// <returns>A ClassRepository representating the MIF classes in COR format</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "pkr"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mif")]
        public MohawkCollege.EHR.gpmr.COR.ClassRepository CompileMif(MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository.PackageRepository pkr)
        {
            ClassRepository retVal = new ClassRepository();
            CompileMif(pkr, retVal);
            return retVal;
        }

        #endregion

        #region IPipelineComponent Members

        /// <summary>
        /// This compiler should run first, so this returns -1
        /// </summary>
        public int ExecutionOrder
        {
            get { return -1; }
        }

        /// <summary>
        /// This is a compiler so it returns a compiler component type
        /// </summary>
        public PipelineComponentType ComponentType
        {
            get { return PipelineComponentType.Compiler; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        public void Init(Pipeline Context)
        {
            hostContext = Context;
            System.Diagnostics.Trace.WriteLine("Mohawk College MIF 1.0/2.0 to COR Converter\r\nCopyright(C) 2008-2012 Mohawk College of Applied Arts and Technology", "information");

        }

        /// <summary>
        /// Execute this pipeline object
        /// </summary>
        public void Execute()
        {

            System.Diagnostics.Trace.WriteLine("\r\nStarting MIF to COR Compilation Process", "information");

            // Grab the MifRepository"
            ClassRepository classRep = new ClassRepository();

            // Process the CMET Maps
            ProcessCmetBindings(classRep);

            // Compile the Mif 1 repository
            if (hostContext.Data.ContainsKey("Mif10Repository"))
                CompileMif(hostContext.Data["Mif10Repository"] as PackageRepository, classRep);

            // Compile mif 2 repository
            if (hostContext.Data.ContainsKey("MIF20Repository"))
                CompileMif(hostContext.Data["MIF20Repository"] as MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository.PackageRepository, classRep);


            // Push our rendered classes back onto the pipeline
            hostContext.Data.Add("SourceCR", classRep);
        }

        // Add CMET Bindings
        private void ProcessCmetBindings(ClassRepository classRep)
        {
            // Get the parameters
            Dictionary<String, StringCollection> parameters = hostContext.Data["CommandParameters"] as Dictionary<String, StringCollection>;
            StringCollection cmetBinding = null;
            
            // Parameter was not provided
            if(!parameters.TryGetValue("cmet-binding", out cmetBinding))
                return;
            
            // Create type references for the cmets that have been bound
            foreach (var binding in cmetBinding)
            {
                string[] bindingData = binding.Split(';');
                cmetBindings.Add(bindingData[0], bindingData[1]);
            }

        }

        #endregion
    }
}
