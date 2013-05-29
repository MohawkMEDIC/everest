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
 * Date: 10/22/2010
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.VisualStudio.Wizards.Parameters;
using MARC.Everest.VisualStudio.Wizards.Util;
using Microsoft.VisualStudio.TemplateWizard;
using System.Windows.Forms;
using MARC.Everest.VisualStudio.Wizards.Stages;
using Microsoft.Win32;
using MARC.Everest.VisualStudio.Wizards.Interfaces;
using System.Reflection;
using System.IO;

namespace MARC.Everest.VisualStudio.Wizards
{
    public class ProjectWizard : Microsoft.VisualStudio.TemplateWizard.IWizard
    {

        private bool shouldAdd = false;

        #region IWizard Members
    
        public void BeforeOpeningFile(EnvDTE.ProjectItem projectItem)
        {

        }

        public void ProjectFinishedGenerating(EnvDTE.Project project)
        {
        }

        public void ProjectItemFinishedGenerating(EnvDTE.ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {

            try
            {
                // Installation directory
                RegistryKey rkSoftware = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{A21E1269-8CDE-43CD-B179-2B6674413081}_is1", false);
                string installDir = String.Empty;
                if (rkSoftware != null)
                {
                    installDir = rkSoftware.GetValue("InstallLocation").ToString();
                    //Assembly.LoadFrom(Path.Combine(Path.Combine(installDir, "lib"), "MARC.Everest.dll"));
                }
                else
                {
                    MessageBox.Show("Could not locate Everest Framework 1.0 on this machine, please reinstall");
                    return;
                }

                // Wizard Creation
                var wzrd = new frmWizard();
                ValueDataParameter wp = new ValueDataParameter();
                wp.Name = "InstallDir";
                wp.Value = installDir;
                wzrd.Parameters.Add(wp);
                var wStg = new wstgSelectRelease();
                wzrd.Text = "Setup Everest Project";
                wzrd.ShowStage(wStg, WizardDirection.Forward);
                shouldAdd = true;
                wzrd.ShowDialog();

                // Get the namespaces
                replacementsDictionary.Add("$rmimusings$", Path.GetFileNameWithoutExtension(wStg.MessageTypeAssemblyFile));
                Assembly rmim = Assembly.ReflectionOnlyLoadFrom(wStg.MessageTypeAssemblyFile);
                replacementsDictionary.Add("$rmiminclude$", String.Format("<Reference Include=\"{0}\"><SpecificVersion>False</SpecificVersion><HintPath>{1}</HintPath></Reference>", rmim.FullName, wStg.MessageTypeAssemblyFile));
                replacementsDictionary.Add("$featureinclude$", "");
                replacementsDictionary.Add("$featureusings$", "");
                foreach (var ns in wStg.Features)
                {
                    Assembly asm = Assembly.ReflectionOnlyLoadFrom(ns.Value);
                    replacementsDictionary["$featureinclude$"] += String.Format("<Reference Include=\"{0}\"><SpecificVersion>False</SpecificVersion><HintPath>{1}</HintPath></Reference>", asm.FullName, ns.Value);
                    replacementsDictionary["$featureusings$"] += String.Format("using {0};\r\n", Path.GetFileNameWithoutExtension(ns.Value));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error Occurred");
            }


        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return shouldAdd;
        }

        #endregion
    }
}
