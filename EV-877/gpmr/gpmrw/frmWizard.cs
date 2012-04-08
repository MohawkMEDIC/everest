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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MARC.Everest.VisualStudio.Wizards.Interfaces;

namespace gpmrw
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "frm"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "frm")]
    public partial class frmWizard : Form, IWizard
    {
        public string StatusText
        {
            get
            {
                return lblStatus.Text;
            }
        }

        public frmWizard()
        {
            // Parameters
            Parameters = new List<WizardParameter>();
            InitializeComponent();

        }

        private void frmWizard_Load(object sender, EventArgs e)
        {

        }

        #region IWizard Members

        /// <summary>
        ///  The current stage that is shown
        /// </summary>
        public IWizardStage CurrentStage { get; private set; }

        /// <summary>
        /// Show a wizard stage
        /// </summary>
        /// <param name="stage">The stage to show</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void ShowStage(IWizardStage stage, WizardDirection dir)
        {
            System.Diagnostics.Trace.WriteLine(String.Format("Showing stage {0}...", stage.StageName));

            if (CurrentStage != null)
            {
                try
                {
                    CurrentStage.Hiding();
                }
                catch(Exception e)
                {
                    if (dir == WizardDirection.Forward)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }
                }
            }

            lblStatus.Text = stage.StageName;

            stage.Host = this;
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(stage as Control);

            // Setup buttons
            btnNextFinish.Text = stage.IsTerminal ? "Finish" : "Next";
            btnExitPrevious.Text = stage.CanExit ? "Cancel" : "Back";

            var prevStage = CurrentStage;
            CurrentStage = stage;
            UpdateNextPrev();

            CurrentStage.Shown(prevStage, dir); 

            // Select the stage
            SelectCurrentStage(trvStages.Nodes);
        }

        private void SelectCurrentStage(TreeNodeCollection nodes)
        {
            foreach (TreeNode trv in nodes)
                if (trv.Text == CurrentStage.StageName)
                {
                    trv.NodeFont = new Font(trvStages.Font, FontStyle.Underline);
                    trvStages.SelectedNode = trv;
                }
                else
                {
                    SelectCurrentStage(trv.Nodes);
                    trv.NodeFont = trvStages.Font;
                }
        }

        /// <summary>
        /// Parameters in the wizard
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<WizardParameter> Parameters { get; set; }

        #endregion

        private void btnNextFinish_Click(object sender, EventArgs e)
        {
            // Dialog result
            if (CurrentStage.IsTerminal)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                ShowStage(CurrentStage.Next, WizardDirection.Forward);
        }

        private void btnExitPrevious_Click(object sender, EventArgs e)
        {
            if (CurrentStage.CanExit)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
                ShowStage(CurrentStage.Previous, WizardDirection.Backward);
        }

        public void DisableNext()
        {
            btnNextFinish.Enabled = false;
        }

        public void EnableNext()
        {
            btnNextFinish.Enabled = true;
        }

        public void DisableBack()
        {
            btnExitPrevious.Enabled = false;
        }

        public void EnableBack()
        {
            btnExitPrevious.Enabled = true;
        }

        private void tmrUpdateUI_Tick(object sender, EventArgs e)
        {
            UpdateNextPrev();

        }

        private void UpdateNextPrev()
        {
            btnNextFinish.Enabled = CurrentStage != null && (CurrentStage.Next != null || CurrentStage.IsTerminal);
            btnExitPrevious.Enabled = CurrentStage != null && (CurrentStage.Previous != null || CurrentStage.CanExit);
        }

    }
}
