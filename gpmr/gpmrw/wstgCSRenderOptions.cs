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
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Util;
using MARC.Everest.VisualStudio.Wizards.Interfaces;
using System.Collections.Specialized;

namespace gpmrw
{
    public partial class wstgCSRenderOptions : UserControl, IWizardStage
    {

        private wstgSetOutput m_nextStage = new wstgSetOutput();

        public wstgCSRenderOptions()
        {
            InitializeComponent();
        }

        private void chkGenerateVocab_CheckedChanged(object sender, EventArgs e)
        {
            numMaxLiterals.Enabled = chkGenerateVocab.Checked;
        }

        private void cbxLicense_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateNamespace();
        }

        private void GenerateNamespace()
        {
            string realmPart = Util.MakeFriendly(cbxLicense.Text);
            string profilePart = Util.MakeFriendly(txtProfileId.Text);
            txtNamespace.Text = String.Format("MARC.Everest.RMIM.{0}.{1}", realmPart, profilePart);
        }

        private void txtProfileId_TextChanged(object sender, EventArgs e)
        {
            GenerateNamespace();
        }

        #region IWizardStage Members

        public IWizard Host { get; set; }

        public string StageName
        {
            get { return "RIM Based Application : C# / .NET Renderer"; }
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

            Dictionary<String, String> parameters = new Dictionary<string, string>()
            {
                { "rimbapi-gen-vocab", chkGenerateVocab.Checked.ToString().ToLower() },
                { "rimbapi-gen-rim", chkGenerateRim.Checked.ToString().ToLower() },
                { "rimbapi-target-ns", txtNamespace.Text },
                { "rimbapi-profileid", txtProfileId.Text },
                { "rimbapi-max-literals", numMaxLiterals.Value.ToString() },
                { "rimbapi-realm-pref", cbxLicense.Text },
                { "rimbapi-dllonly", (chkCompile.Checked && chkDllOnly.Checked).ToString().ToLower() },
                { "rimbapi-compile", chkCompile.Checked.ToString().ToLower() },
                { "rimbapi-org", txtOrganization.Text }
            };

            foreach (var kv in parameters)
            {
                StringCollection sci = null;
                if (!Program.s_parameters.TryGetValue(kv.Key, out sci))
                {
                    sci = new StringCollection();
                    Program.s_parameters.Add(kv.Key, sci);
                }
                sci.Clear();
                sci.Add(kv.Value);
            }
        }

        #endregion

        private void chkCompile_CheckedChanged(object sender, EventArgs e)
        {
            chkDllOnly.Enabled = chkCompile.Checked;
        }
    }
}
