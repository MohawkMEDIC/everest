using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MARC.Everest.VisualStudio.Wizards.Interfaces;

namespace MARC.Everest.VisualStudio.Wizards
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
        public void ShowStage(IWizardStage stage, WizardDirection direction)
        {
            System.Diagnostics.Trace.WriteLine(String.Format("Showing stage {0}...", stage.StageName));

            lblStatus.Text = stage.StageName;

            stage.Host = this;
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(stage as Control);

            // Setup buttons
            btnNextFinish.Text = stage.IsTerminal ? "Finish" : "Next";
            btnExitPrevious.Text = stage.CanExit ? "Cancel" : "Back";

            if (CurrentStage != null)
                CurrentStage.Hiding();

            stage.Shown(CurrentStage, direction); 

            CurrentStage = stage;
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

    }
}
