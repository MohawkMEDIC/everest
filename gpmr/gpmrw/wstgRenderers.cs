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
    public partial class wstgRenderers : UserControl, IWizardStage
    {

        private IWizardStage[] m_nextStages = new IWizardStage[] {
            new wstgCSRenderOptions(),
            new wstgJavaRenderOptions()
        };
        private string[] m_rendererNames = {
                                               "RIMBA_CS",
                                               "RIMBA_JA",
                                               "DEKI",
                                               "XSD"
                                           };

        private int m_nextStageIndex = 0;

        public wstgRenderers()
        {
            InitializeComponent();
        }

        #region IWizardStage Members

        public IWizard Host { get; set; }

        public string StageName
        {
            get { return "Configure Renderers"; }
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
            get { return this.m_nextStages[this.m_nextStageIndex]; }
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
            StringCollection sci = null;
            if (!Program.s_parameters.TryGetValue("renderer", out sci))
            {
                sci = new StringCollection();
                Program.s_parameters.Add("renderer", sci);
            }
            sci.Clear();
            sci.Add(this.m_rendererNames[this.m_nextStageIndex]);
        }

        #endregion

        private void rdoCS_CheckedChanged(object sender, EventArgs e)
        {
            this.m_nextStageIndex = 0;
        }

        private void rdoJava_CheckedChanged(object sender, EventArgs e)
        {
            this.m_nextStageIndex = 1;
        }
    }
}
