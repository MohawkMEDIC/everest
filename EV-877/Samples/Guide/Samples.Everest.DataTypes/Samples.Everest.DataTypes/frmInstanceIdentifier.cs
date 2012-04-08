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
    public partial class frmInstanceIdentifier : Form
    {
        public frmInstanceIdentifier()
        {
            InitializeComponent();
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            II ii = new II();
            // Determine if null flavor should be attached to the object
            if (cbxNullFlavor.SelectedItem != null)
                switch (cbxNullFlavor.SelectedItem.ToString())
                {
                    case "NAV":
                        ii.NullFlavor = NullFlavor.Unavailable;
                        break;
                    case "OTH":
                        ii.NullFlavor = NullFlavor.Other;
                        break;
                    case "NI":
                        ii.NullFlavor = NullFlavor.NoInformation;
                        break;
                    default:
                        ii.NullFlavor = null;
                        break;
                }

            // Determine if a flavor should be attached
            if (cbxFlavor.SelectedItem != null &&
                cbxFlavor.SelectedItem.ToString() != "None")
                ii.Flavor = cbxFlavor.SelectedItem.ToString();

            // Assign the root and extension
            ii.Root = txtRoot.Text;
            ii.Extension = txtExtension.Text;


            // Represent in txtXml
            txtXml.Text = FormatterHelper.FormatDataType(ii, "II");
        }
    }
}
