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
using System.IO;

namespace gpmrw
{
    public partial class wstgVerifyParameters : UserControl, IWizardStage
    {
        public wstgVerifyParameters()
        {
            InitializeComponent();
            this.Next = new wstgRun();
        }

        #region IWizardStage Members

        public IWizard Host { get; set; }

        public string StageName
        {
            get { return "Verify"; }
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
            get;
            private set;
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

            string renderName = "";
            switch (Program.s_parameters["renderer"][0])
            {
                case "RIMBA_CS":
                    renderName = "RIM Based C#";
                    break;
                case "RIMBA_JA":
                    renderName = "RIM Based Java";
                    break;
                case "DEKI":
                    renderName = "DEKI Wiki / HTML";
                    break;
                case "XSD":
                    renderName = "XML Renderer";
                    break;
            }

            // Assemble information
            StringBuilder options = new StringBuilder();
            options.AppendFormat("Mode: {0}\r\n", Program.s_parameters.ContainsKey("quirks") ? "Quirks" : Program.s_parameters.ContainsKey("strict") ? "Strict" : "Normal");
            options.AppendFormat("Renderer: {0}\r\n", renderName);
            options.AppendFormat("Output Directory: {0}\r\n", Program.s_parameters["o"][0]);
            options.Append("Files:\r\n");
            foreach (var f in Program.s_parameters["s"])
                options.AppendFormat("\t{0}\r\n", Path.GetFileName(f));

            txtParameters.Text = options.ToString();

            StringBuilder cmdBuilder = new StringBuilder("gpmr -c");
            foreach (var kv in Program.s_parameters)
            {

                if (kv.Value == null)
                    cmdBuilder.AppendFormat(" --{0}", kv.Key);
                else
                    foreach (var itm in kv.Value)
                    {
                        if (kv.Key.Length == 1)
                            cmdBuilder.AppendFormat(" -{0} \"{1}\"", kv.Key, itm);
                        else
                            cmdBuilder.AppendFormat(" --{0}=\"{1}\"", kv.Key, itm);
                    }
            }
            txtCommandLine.Text = cmdBuilder.ToString();
        }

        public void Hiding()
        {
            
        }

        #endregion
    }
}
