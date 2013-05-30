using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARC.Everest.Connectors.WCF;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;

namespace Samples.Everest.Connectors.WcfClient.PdqClient
{
    public partial class frmClientRegistry : Form
    {

        // Search string
        private string m_searchString = String.Empty;

        // When true indicates the UI needs refresh
        private bool m_needsRefresh = false;

        // Background worker for client registry status
        private BackgroundWorker m_crStatus = new BackgroundWorker();

        // Background worker for populating the auto complete
        private BackgroundWorker m_populateAutoComplete = new BackgroundWorker();

        // Client connector
        private WcfClientConnector m_clientConnector;

        // ctor
        public frmClientRegistry()
        {
            InitializeComponent();
            InitializeConnector();

            m_crStatus.DoWork += new DoWorkEventHandler(m_crStatus_DoWork);
            m_crStatus.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_crStatus_RunWorkerCompleted);
            m_populateAutoComplete.DoWork += new DoWorkEventHandler(m_populateAutoComplete_DoWork);
            m_populateAutoComplete.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_populateAutoComplete_RunWorkerCompleted);
            m_crStatus.RunWorkerAsync();
        }

        /// <summary>
        /// Initialize the client connector
        /// </summary>
        private void InitializeConnector()
        {
            // Build connection string
            WcfConnectionStringBuilder csBuilder = new WcfConnectionStringBuilder();
            csBuilder.EndpointName = "pdqSupplier";

            // Create formatter
            XmlIts1Formatter fmtr = new XmlIts1Formatter()
            {
                ValidateConformance = false
            };
            fmtr.GraphAides.Add(new CanadianDatatypeFormatter() {
                ValidateConformance = false 
            });

            // Setup and open
            this.m_clientConnector = new WcfClientConnector(csBuilder.GenerateConnectionString());
            this.m_clientConnector.Formatter = fmtr;
            this.m_clientConnector.Open();
        }

        /// <summary>
        /// Get the status of the client registry
        /// </summary>
        void m_crStatus_DoWork(object sender, DoWorkEventArgs e)
        {
            PdqCommunications crc = new PdqCommunications();
            e.Result = crc.IsCrAvailable(this.m_clientConnector);
        }

        /// <summary>
        /// Update the status label
        /// </summary>
        void m_crStatus_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((bool)e.Result)
                lblStatus.Text = "Client Registry is available";
            else
                lblStatus.Text = "Client Registry not available";
            txtName.Enabled = (bool)e.Result;
        }

        /// <summary>
        /// Populate the auto-complete data worker thread
        /// </summary>
        void m_populateAutoComplete_DoWork(object sender, DoWorkEventArgs e)
        {
            PdqCommunications crc = new PdqCommunications();
            e.Result = crc.Filter(this.m_clientConnector, e.Argument.ToString());
        }

        /// <summary>
        /// Update auto-complete data
        /// </summary>
        void m_populateAutoComplete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lstResults.Items.Clear();
            lstResults.Items.AddRange(e.Result as String[]);
            lblStatus.Text = String.Format("{0} matches", lstResults.Items.Count);
            this.m_needsRefresh = txtName.Text != this.m_searchString;
        }

        /// <summary>
        /// Text has changed
        /// </summary>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this.m_needsRefresh = true;
        }

        private void tmrAutoCompleteRefresh_Tick(object sender, EventArgs e)
        {
            if (this.m_needsRefresh && !this.m_populateAutoComplete.IsBusy)
            {
                this.m_searchString = txtName.Text;
                lblStatus.Text = String.Format("Searching...");
                this.m_populateAutoComplete.RunWorkerAsync(txtName.Text);
            }
        }

    
  

    }
}
