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
    public partial class frmConceptDescriptor : Form
    {
        public frmConceptDescriptor()
        {
            InitializeComponent();
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            // Create an empty CD object.
            CD<String> cd = new CD<String>();

            // Set properties provided by form input.
            if (!String.IsNullOrEmpty(txtCode.Text))
                cd.Code = txtCode.Text;

            if (!String.IsNullOrEmpty(txtCodeSystem.Text))
                cd.CodeSystem = txtCodeSystem.Text;

            if (!String.IsNullOrEmpty(txtCodeSystemName.Text))
                cd.CodeSystemName = txtCodeSystemName.Text;

            if (!String.IsNullOrEmpty(txtCodeSystemVersion.Text))
                cd.CodeSystemVersion = txtCodeSystemVersion.Text;

            if (!String.IsNullOrEmpty(txtDisplayName.Text))
                cd.DisplayName = txtDisplayName.Text;

            if (!String.IsNullOrEmpty(txtOriginalText.Text))
                cd.OriginalText = txtOriginalText.Text;


            // Get the CD in XML format using the FormatterHelper class.
            string cdXmlStr = FormatterHelper.FormatDataType(cd, "CD");

            // Display the generated xml.
            txtXml.Text = cdXmlStr;

        }
    }
}
