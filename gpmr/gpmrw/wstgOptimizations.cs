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
 * Date: 09-26-2011
 **/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARC.Everest.VisualStudio.Wizards.Interfaces;
using System.Collections.Specialized;

namespace gpmrw
{
    public partial class wstgOptimizations : UserControl, IWizardStage
    {

        private wstgRenderers m_nextStage = new wstgRenderers();

        public wstgOptimizations()
        {
            InitializeComponent();
        }

        #region IWizardStage Members

        public IWizard Host { get; set; }

        public string StageName
        {
            get { return "Optimizations"; }
        }

        public bool IsTerminal
        {
            get { return false; }
        }

        public bool CanExit
        {
            get { return false; }
        }

        public IWizardStage Next
        {
            get { return this.m_nextStage; }
        }

        /// <summary>
        /// Previous stage
        /// </summary>
        public IWizardStage Previous
        {
            get;
            private set;
        }

        public void Shown(IWizardStage previous, WizardDirection direction)
        {
            if (direction == WizardDirection.Forward)
                this.Previous = previous;
        }

        public void Hiding()
        {
            StringCollection parm = null;

            if (!Program.s_parameters.TryGetValue("combine", out parm))
            {
                parm = new StringCollection();
                Program.s_parameters.Add("combine", parm);
            }
            parm.Clear();
            parm.Add(chkCombine.Checked.ToString().ToLower());

            if (!Program.s_parameters.TryGetValue("collapse", out parm))
            {
                parm = new StringCollection();
                Program.s_parameters.Add("collapse", parm);
            }
            parm.Clear();
            parm.Add(chkCollapse.Checked.ToString().ToLower());

            if (!Program.s_parameters.TryGetValue("optimize", out parm))
            {
                parm = new StringCollection();
                Program.s_parameters.Add("optimize", parm);
            }
            parm.Clear();
            parm.Add((chkCollapse.Checked || chkCombine.Checked).ToString().ToLower());

            // Collapse?
            if (chkCollapse.Checked)
            {
                if (!Program.s_parameters.ContainsKey("collapse-ignore-fixed"))
                    Program.s_parameters.Add("collapse-ignore-fixed", new StringCollection() { "true" });
                if (!Program.s_parameters.ContainsKey("collapse-useless-name"))
                    Program.s_parameters.Add("collapse-useless-name", new StringCollection() { 
                        "component",
                        "section",
                        "structuredBody"
                    });
                if(!Program.s_parameters.ContainsKey("collapse-important-name"))
                    Program.s_parameters.Add("collapse-important-name", new StringCollection() {
                        "subject",
                        "componentOf"
                    });
                if (!Program.s_parameters.ContainsKey("collapse-adv-naming"))
                    Program.s_parameters.Add("collapse-adv-naming", new StringCollection() { "true" });
                if (!Program.s_parameters.ContainsKey("collapse-ignore"))
                    Program.s_parameters.Add("collapse-ignore", new StringCollection() { 
                        "nullFlavor",
                        "updateMode",
                        "contextConductionInd",
                        "contextControlCode"
                    });
            }
        }

        #endregion
    }
}
