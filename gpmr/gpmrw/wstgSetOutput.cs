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
    public partial class wstgSetOutput : UserControl, IWizardStage
    {

        private wstgVerifyParameters m_nextStage = new wstgVerifyParameters();

        public wstgSetOutput()
        {
            InitializeComponent();
            cbxProcessingMode.SelectedIndex = 1;
        }

        #region IWizardStage Members

        public IWizard Host { get; set; }

        public string StageName
        {
            get { return "Set Output"; }
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
            get
            {
                if (txtOutput.Text.Length > 0)
                    return this.m_nextStage;
                else
                    return null;
            }
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
            if (txtOutput.Text.Length == 0)
                throw new InvalidOperationException("Output directory must be selected!");

            StringCollection sci = null;
            if (!Program.s_parameters.TryGetValue("o", out sci))
            {
                sci = new StringCollection();
                Program.s_parameters.Add("o", sci);
            }
            sci.Clear();
            sci.Add(txtOutput.Text);

            string[] modes = { "strict", null, "quirks" };
            foreach (var s in modes)
                if (s != null && Program.s_parameters.ContainsKey(s))
                    Program.s_parameters.Remove(s);
            if (modes[cbxProcessingMode.SelectedIndex] != null)
            {
                Program.s_parameters.Add(modes[cbxProcessingMode.SelectedIndex], null);
            }

        }

        #endregion

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (dlgFolderBrowse.ShowDialog() != DialogResult.OK)
                return;
            txtOutput.Text = dlgFolderBrowse.SelectedPath;
        }
    }
}
