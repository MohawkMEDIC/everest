using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARC.Everest.VisualStudio.Wizards.Interfaces;
using System.IO;
using System.Reflection;
using MARC.Everest.VisualStudio.Wizards.Parameters;

namespace MARC.Everest.VisualStudio.Wizards.Stages
{
    /// <summary>
    /// Select a release
    /// </summary>
    public partial class wstgSelectRelease : UserControl, IWizardStage
    {

        public wstgSelectRelease()
        {
            InitializeComponent();
        }

        #region IWizardStage Members

        /// <summary>
        /// Features
        /// </summary>
        internal List<DisplayableKeyValuePair<String, String>> Features
        {
            get
            {
                List<DisplayableKeyValuePair<String,String>> retVal = new List<DisplayableKeyValuePair<string,string>>();
                foreach (DisplayableKeyValuePair<String, String> itm in cklFeatures.CheckedItems)
                    retVal.Add(itm);
                return retVal;
            }
        }

        /// <summary>
        /// The message type assembly file
        /// </summary>
        public string MessageTypeAssemblyFile
        {
            get
            {
                return (cboGPMR.SelectedItem as DisplayableKeyValuePair<String, String>).Value;
            }
        }


        /// <summary>
        /// Gets or sets the host wizard that this stage runs under
        /// </summary>
        public IWizard Host { get; set; }

        /// <summary>
        /// Select release DLL to use
        /// </summary>
        public string StageName
        {
            get { return "Select Everest Features"; }
        }

        /// <summary>
        /// Return false
        /// </summary>
        public bool IsTerminal
        {
            get { return true; }
        }

        /// <summary>
        /// Can exit the wizard from this stage
        /// </summary>
        public bool CanExit
        {
            get { return true; }
        }

        /// <summary>
        /// Identifies the next wizard stage
        /// </summary>
        public IWizardStage Next
        {
            get {
                return null;
            }
        }

        /// <summary>
        /// Identifies the previous wizard stage
        /// </summary>
        public IWizardStage Previous
        {
            get { return null; }
        }

        /// <summary>
        /// Fired when the wizard stage is shown
        /// </summary>
        public void Shown(IWizardStage previous, WizardDirection direction)
        {

            var installDir = Host.Parameters.Find(o => o.Name == "InstallDir") as ValueDataParameter;

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(CurrentDomain_ReflectionOnlyAssemblyResolve);
            // Scan the installation director
            foreach (var file in Directory.GetFiles(Path.Combine(installDir.Value.ToString(), "lib"), "*.dll"))
            {
                Assembly asm = Assembly.ReflectionOnlyLoadFrom(file);
                var cad = CustomAttributeData.GetCustomAttributes(asm);
                bool isGPMR = false;
                string desc = string.Empty,
                    iVersion = string.Empty ;
                foreach (var ad in cad)
                {
                    isGPMR |= (ad.ToString().Contains("AssemblyDescription") && ad.ConstructorArguments[0].Value.ToString().Contains("GPMR"));
                    if (ad.ToString().Contains("AssemblyTitleAttribute"))
                        desc = ad.ConstructorArguments[0].Value.ToString();
                    else if (ad.ToString().Contains("AssemblyInformationalVersion"))
                        iVersion = ad.ConstructorArguments[0].Value.ToString();

                }
                if (isGPMR)
                    cboGPMR.Items.Add(new DisplayableKeyValuePair<String, String>(String.Format("{0} ({1})", desc, iVersion), file));
                else if(!String.IsNullOrEmpty(desc))
                {
                    cklFeatures.Items.Add(new DisplayableKeyValuePair<String, String>(desc, file));
                    if (Path.GetFileNameWithoutExtension(file).Equals("MARC.Everest"))
                        cklFeatures.SetItemChecked(cklFeatures.Items.Count - 1, true);
                }
            }
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= new ResolveEventHandler(CurrentDomain_ReflectionOnlyAssemblyResolve);
            cboGPMR.SelectedIndex = 0;
        }

        /// <summary>
        /// Reflection assembly load
        /// </summary>
        Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.ReflectionOnlyLoad(args.Name);
        }

        /// <summary>
        /// Called when hiding
        /// </summary>
        public void Hiding()
        {
            
        }

        #endregion
    }
}
