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
 * Date: $date$
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE80;
using MARC.Everest.VisualStudio.Wizards.Parameters;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
using MARC.Everest.VisualStudio.Wizards.Proxy;
using System.Runtime.InteropServices.ComTypes;

namespace MARC.Everest.VisualStudio.Wizards.Util
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Util")]
    public class ProjectUtil
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dte")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static DTE2 dteSolution;
        
        #region Ugly COM Stuff
        [DllImport("ole32.dll")]
        static extern int GetRunningObjectTable(uint reserved, out IRunningObjectTable pprot);

        [DllImport("ole32.dll")]
        static extern int CreateBindCtx(uint reserved, out IBindCtx pctx);

        /// <summary>
        /// From : Leonard Jiang 
        /// On : http://social.msdn.microsoft.com/forums/en-US/vsx/thread/3120db69-a89c-4545-874f-2d61c9317c8a/
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.Compare(System.String,System.String)")]
        private static DTE2 SeekDTE2InstanceFromROT(String moniker)
        {
            //
            IRunningObjectTable prot = null;
            IEnumMoniker pmonkenum = null;
            IntPtr pfeteched = IntPtr.Zero;

            DTE2 ret = null;

            try
            {
                //get rot
                if ((GetRunningObjectTable(0, out prot) != 0) || (prot == null)) return ret;
                prot.EnumRunning(out pmonkenum);
                pmonkenum.Reset();
                IMoniker[] monikers = new IMoniker[1];
                while (pmonkenum.Next(1, monikers, pfeteched) == 0)
                {
                    String insname;
                    IBindCtx pctx;
                    CreateBindCtx(0, out pctx);
                    monikers[0].GetDisplayName(pctx, null, out insname);
                    Marshal.ReleaseComObject(pctx);
                    if (string.Compare(insname, moniker) == 0) //lookup by item moniker
                    {
                        Object obj;
                        prot.GetObject(monikers[0], out obj);
                        ret = (DTE2)obj;
                    }
                }
            }
            finally
            {
                if (prot != null) Marshal.ReleaseComObject(prot);
                if (pmonkenum != null) Marshal.ReleaseComObject(pmonkenum);
            }
            return ret;
        }

        #endregion

 
        /// <summary>
        /// Get all reference structures
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public static InteractionDataParameter GetAllReferenceStructures()
        {
            if (dteSolution == null) // Get the current instance of VS
                dteSolution = SeekDTE2InstanceFromROT(String.Format("!VisualStudio.DTE.9.0:{0}", Process.GetCurrentProcess().Id)) as DTE2;

            // Get the active project
            Array activeProjects = (Array)dteSolution.ActiveSolutionProjects;
            EnvDTE.Project currentProject = activeProjects.GetValue(0) as EnvDTE.Project;

            
            // Get a reference to the current Visual Studio Project
            VSLangProj.VSProject vsProject = currentProject.Object as VSLangProj.VSProject;
            
            // Create an app domain for loading new assemblies, load the assemblies, 
            // and get all interations unsing the interaction list proxy class
            AppDomain dllDomain = AppDomain.CreateDomain("MessageFactoryWizard");
            dllDomain.Load(Assembly.GetExecutingAssembly().FullName);
            StructureListProxyClass ilpc = dllDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, "MARC.Everest.VisualStudio.Wizards.Proxy.StructureListProxyClass") as StructureListProxyClass;

            // Load references
            foreach (VSLangProj.Reference reference in vsProject.References)
                if (reference.Type == VSLangProj.prjReferenceType.prjReferenceTypeAssembly) // Project Assembly, search AppDomain
                    ilpc.LoadAssembly(reference.Path);

            // Get the strucutres in the referenced assemblies then copy them to the
            // parameters
            InteractionDataParameter parm = new InteractionDataParameter();
            parm.Structures = ilpc.GetStructures();
            parm.Name = "structures";
       
            if (parm.Structures == null)
                throw new InvalidOperationException("You must make a reference to the MARC.Everest assembly before adding a new structure");

            // Unload the app domain
            AppDomain.Unload(dllDomain);

            return parm;
        }
    }
}
