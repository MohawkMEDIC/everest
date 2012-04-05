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
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Reflection;

namespace gpmrw
{
    public partial class wstgSources : UserControl, IWizardStage
    {

        private wstgOptimizations m_nextStage = new wstgOptimizations();

        public wstgSources()
        {
            InitializeComponent();
            pbStatus.Image = imlNotes.Images[1];
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dlgOpenMif.ShowDialog() != DialogResult.OK)
                return;

            foreach (var fil in dlgOpenMif.FileNames)
            {
                Cursor = Cursors.WaitCursor;
                AddAndVerifySource(fil);
                Cursor = Cursors.Default;
            }
            Verify();
        }

        private bool Verify()
        {
            foreach(ListViewItem lvi in lsvMifs.Items)
                if (lvi.ImageIndex != 1)
                {
                    pbStatus.Image = imlNotes.Images[0];
                    lblState.Text = "Some source files are invalid";
                    return false;
                }
            pbStatus.Image = imlNotes.Images[1];
            lblState.Text = "All files appear to be valid";

            return true;
        }

        /// <summary>
        /// Add and verify source file(s)
        /// </summary>
        private void AddAndVerifySource(string file)
        {
            // Don't add
            if (lsvMifs.Items.ContainsKey(file))
                return;

            ListViewItem lvi = lsvMifs.Items.Add(file, Path.GetFileNameWithoutExtension(file), 1);
            try
            {
                lvi.Tag = file;
                XmlDocument xd = new XmlDocument();

                // Validate XML
                xd.Load(file);
                var node = xd.SelectSingleNode("//*[local-name() = 'schemaVersion'] | //@*[local-name() = 'schemaVersion']");

                // Validate MIF
                if (node == null)
                    throw new Exception("This file doesn't appear to be a valid MIF!");
                string schemaVersion = node.Value;

                // Validate XSLT is available
                if (!File.Exists(Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "xsl"), String.Format("{0}.xslt", schemaVersion))))
                    throw new Exception(String.Format("GPMR doesn't understand MIF version {0}", schemaVersion));

                lvi.SubItems.Add(schemaVersion);

            }
            catch(Exception e)
            {
                lvi.ImageIndex = 0;
                lvi.ToolTipText = e.Message;
            }
        }

        #region IWizardStage Members

        /// <summary>
        /// Gets or sets the host
        /// </summary>
        public IWizard Host { get; set; }

        /// <summary>
        /// Gets the stage name
        /// </summary>
        public string StageName
        {
            get { return "Select Source Files"; }
        }

        /// <summary>
        /// True if is terminal
        /// </summary>
        public bool IsTerminal
        {
            get { return false; }
        }

        /// <summary>
        /// True if can exit
        /// </summary>
        public bool CanExit
        {
            get { return false; }
        }

        /// <summary>
        /// Next wizard stage
        /// </summary>
        public IWizardStage Next
        {
            get {
                if (lsvMifs.Items.Count > 0)
                    return m_nextStage;
                else
                    return null;
            }
        }

        /// <summary>
        /// Previous wizard stage
        /// </summary>
        public IWizardStage Previous
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Shown
        /// </summary>
        public void Shown(IWizardStage previous, WizardDirection direction)
        {
            if (direction == WizardDirection.Forward)
                this.Previous = previous;
        }

        /// <summary>
        /// Set the parameters
        /// </summary>
        public void Hiding()
        {

            if (lsvMifs.Items.Count == 0)
                throw new InvalidOperationException("At least one source file must be selected");

            StringCollection parms = null;
            if (!Program.s_parameters.TryGetValue("s", out parms))
            {
                parms = new StringCollection();
                Program.s_parameters.Add("s", parms);
            }
            parms.Clear();
            foreach (ListViewItem lvi in lsvMifs.Items)
                parms.Add(lvi.Tag.ToString());
        }

        #endregion

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lsvMifs.SelectedItems == null)
                return;

            foreach (ListViewItem itm in lsvMifs.SelectedItems)
                lsvMifs.Items.Remove(itm);
            Verify();
        }
    }
}
