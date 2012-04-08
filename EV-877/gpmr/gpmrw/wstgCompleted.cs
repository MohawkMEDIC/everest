using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARC.Everest.VisualStudio.Wizards.Interfaces;

namespace gpmrw
{
    public partial class wstgCompleted : UserControl, IWizardStage
    {
        public wstgCompleted()
        {
            InitializeComponent();
        }

        #region IWizardStage Members

        public IWizard Host { get; set; }

        public string StageName
        {
            get { return "Complete"; }
        }

        public bool IsTerminal
        {
            get { return true; }
        }

        public bool CanExit
        {
            get { return false; }
        }

        public IWizardStage Next
        {
            get { return null; }
        }

        public IWizardStage Previous
        {
            get { return null; }
        }

        public void Shown(IWizardStage previous, WizardDirection direction)
        {
        }

        public void Hiding()
        {
        }

        #endregion
    }
}
