using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataTypeExplorer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void intanceIdentifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInstanceIdentifier iiForm = new frmInstanceIdentifier();
            iiForm.MdiParent = this;
            iiForm.Show();
        }

        private void stringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmString stForm = new frmString();
            stForm.MdiParent = this;
            stForm.Show();

        }

        private void encapsulatedDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEncapsulatedData edForm = new frmEncapsulatedData();
            edForm.MdiParent = this;
            edForm.Show();

        }

        private void contentDescriptorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConceptDescriptor cdForm = new frmConceptDescriptor();
            cdForm.MdiParent = this;
            cdForm.Show();

        }

        private void provisionedQuantityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProvisionedQuantity pqForm = new frmProvisionedQuantity();
            pqForm.MdiParent = this;
            pqForm.Show();
        }

        private void addressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddress adForm = new frmAddress();
            adForm.MdiParent = this;
            adForm.Show();
        }

        private void booleanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBoolean blForm = new frmBoolean();
            blForm.MdiParent = this;
            blForm.Show();
        }

    }
}
