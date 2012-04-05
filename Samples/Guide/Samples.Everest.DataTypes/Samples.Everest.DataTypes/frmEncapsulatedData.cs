using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARC.Everest.DataTypes;
using System.IO;
using MARC.Everest.DataTypes.Interfaces;

namespace DataTypeExplorer
{
    public partial class frmEncapsulatedData : Form
    {
        public frmEncapsulatedData()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
                txtFileName.Text = dlgOpenFile.FileName;

        }

        private void btnBrowseThumbnail_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
                txtThumbnailFileName.Text = dlgOpenFile.FileName;

        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            ED ed = new ED();     // Set the content type
            ed.MediaType = txtContentType.Text;     // Set a file if possible
            if (txtFileName.Text != String.Empty)
                ed.Data = File.ReadAllBytes(txtFileName.Text);
            // Set the integrity check. This is done before the compressing as we want the hash to 
            // represent the data prior to compression
            if (cbxIntegrityCheck.SelectedItem != null && !cbxIntegrityCheck.SelectedItem.Equals("None"))
                switch (cbxIntegrityCheck.SelectedItem.ToString())
                {
                    case "SHA1":
                        ed.IntegrityCheckAlgorithm = EncapsulatedDataIntegrityAlgorithm.SHA1;
                        ed.IntegrityCheck = ed.ComputeIntegrityCheck(); // Set the integrity check
                        break;
                    case "SHA256":
                        ed.IntegrityCheckAlgorithm = EncapsulatedDataIntegrityAlgorithm.SHA256;
                        ed.IntegrityCheck = ed.ComputeIntegrityCheck(); // Set the integrity check
                        break;

                }
            // Set the representation
            if (cbxRepresentation.SelectedItem != null)
                switch (cbxRepresentation.SelectedItem.ToString())
                {
                    case "Text":
                        ed.Representation = EncapsulatedDataRepresentation.TXT;
                        break;
                    case "Base64":
                        ed.Representation = EncapsulatedDataRepresentation.B64;
                        break;
                    case "Xml":
                        ed.Representation = EncapsulatedDataRepresentation.XML;
                        break;
                }

            // Set the compression
            if (cbxCompression.SelectedItem != null && !cbxCompression.SelectedItem.Equals("None"))
                switch (cbxCompression.SelectedItem.ToString())
                {
                    case "GZip":
                        ed.Data = ed.Compress(EncapsulatedDataCompression.GZ); // Compress the data
                        ed.Representation = EncapsulatedDataRepresentation.B64; // must be set to B64
                        break;
                    case "Deflate":
                        ed.Data = ed.Compress(EncapsulatedDataCompression.DF); // compress the data
                        ed.Representation = EncapsulatedDataRepresentation.B64; // must be set to B64
                        break;
                }


            // Set the thumbnail
            if (txtThumbnailFileName.Text != string.Empty)
                ed.Thumbnail = new ED(File.ReadAllBytes(txtThumbnailFileName.Text), txtContentType.Text);

            txtXml.Text = FormatterHelper.FormatDataType(ed, "ED");


        }
    }
}
