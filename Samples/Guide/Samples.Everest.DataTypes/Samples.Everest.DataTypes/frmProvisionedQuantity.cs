using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARC.Everest.DataTypes;
using System.Text.RegularExpressions;

namespace DataTypeExplorer
{
    public partial class frmProvisionedQuantity : Form
    {
        public frmProvisionedQuantity()
        {
            InitializeComponent();
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            //A double to store the amount in.
            decimal amount;

            //Ensure that a valid amount and unit type exist.
            if (cbxUnit.SelectedItem == null || !Decimal.TryParse(txtValue.Text, out amount))
                return;

            //Create the PQ.
            PQ pq = new PQ(amount, cbxUnit.SelectedItem.ToString());

            // Get the PQ in XML format using the FormatterHelper class.
            string pqXmlStr = FormatterHelper.FormatDataType(pq, "PQ");
            
            // Display the generated xml.
            txtXml.Text = pqXmlStr;
        }
    }
}
