using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace gpmrw
{
    static class Program
    {

        public static Dictionary<String, StringCollection> s_parameters = new Dictionary<string, StringCollection>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmWizard wzrd = new frmWizard();
            wzrd.ShowStage(new wstgWelcome(), MARC.Everest.VisualStudio.Wizards.Interfaces.WizardDirection.Forward);
            var result = wzrd.ShowDialog();
        }
    }
}
