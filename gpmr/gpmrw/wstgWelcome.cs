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

namespace gpmrw
{
    public partial class wstgWelcome : UserControl, IWizardStage
    {

        // Next stage is always source selection
        private wstgSources m_nextStage = new wstgSources();

        // CTOR
        public wstgWelcome()
        {
            InitializeComponent();
        }

        #region IWizardStage Members

        /// <summary>
        /// Gets or sets the host of this wizard
        /// </summary>
        public IWizard Host { get; set; }

        /// <summary>
        /// Gets the stage name
        /// </summary>
        public string StageName
        {
            get { return "Welcome"; }
        }

        /// <summary>
        /// True if terminal
        /// </summary>
        public bool IsTerminal
        {
            get { return false; }
        }

        /// <summary>
        /// Can cancel the wizard?
        /// </summary>
        public bool CanExit
        {
            get { return true; }
        }

        /// <summary>
        /// Get the next stage
        /// </summary>
        public IWizardStage Next
        {
            get { return this.m_nextStage; }
        }

        /// <summary>
        /// Get the previous stage
        /// </summary>
        public IWizardStage Previous
        {
            get { return null; }
        }

        /// <summary>
        /// Called when shown
        /// </summary>
        public void Shown(IWizardStage previous, WizardDirection direction)
        {
            
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
