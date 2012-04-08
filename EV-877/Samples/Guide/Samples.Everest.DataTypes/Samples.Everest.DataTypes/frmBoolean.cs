using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARC.Everest.DataTypes;

namespace DataTypeExplorer
{
    public partial class frmBoolean : Form
    {
        public frmBoolean()
        {
            InitializeComponent();
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            //Create a Boolean
            BL bl = new BL();

            //Set the boolean to the selected radio option
            if (radTrue.Checked)
                bl.Value = true;
            else if (radFalse.Checked)
                bl.Value = false;
            else
            {
                bl.Value = null;
                //Add in the Null Flavor information
                if (cbxNullFlavor.SelectedItem == null)
                    //A Null Flavor must be attached in order to have a BL with a Value of Null
                    return;
                else
                    switch (cbxNullFlavor.SelectedItem.ToString())
                    {
                        case "NAV":
                            bl.NullFlavor = NullFlavor.Unavailable;
                            break;
                        case "OTH":
                            bl.NullFlavor = NullFlavor.Other;
                            break;
                        case "NI":
                            bl.NullFlavor = NullFlavor.NoInformation;
                            break;
                        default:
                            return;
                    }
            }

            // Get the BL in XML format using the FormatterHelper class.
            string blXmlStr = FormatterHelper.FormatDataType(bl, "BL");

            // Display the generated xml.
            txtXml.Text = blXmlStr;

        }

        private void radNull_CheckedChanged(object sender, EventArgs e)
        {
            //Enable the null flavor if the null radio option has been checked
            if (radNull.Checked)
                cbxNullFlavor.Enabled = true;
            else
                cbxNullFlavor.Enabled = false;
        }
    }
}
