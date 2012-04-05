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
    public partial class frmString : Form
    {
        public frmString()
        {
            InitializeComponent();
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            ST st = new ST();
            // Determine if null flavor should be attached to the object
            if (cbxNullFlavor.SelectedItem != null)
                switch (cbxNullFlavor.SelectedItem.ToString())
                {
                    case "NAV":
                        st.NullFlavor = NullFlavor.Unavailable;
                        break;
                    case "OTH":
                        st.NullFlavor = NullFlavor.Other;
                        break;
                    case "NI":
                        st.NullFlavor = NullFlavor.NoInformation;
                        break;
                    default:
                        st.NullFlavor = null;
                        break;
                }
            // Set the language
            if (txtLanguage.Text != String.Empty)
                st.Language = txtLanguage.Text;
            // Set the text
            st.Value = txtText.Text;
            // Represent in txtXml
            txtXml.Text = FormatterHelper.FormatDataType(st, "ST");
        }
    }
}
