namespace DataTypeExplorer
{
    partial class frmEncapsulatedData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRender = new System.Windows.Forms.Button();
            this.btnBrowseThumbnail = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.cbxRepresentation = new System.Windows.Forms.ComboBox();
            this.cbxIntegrityCheck = new System.Windows.Forms.ComboBox();
            this.cbxCompression = new System.Windows.Forms.ComboBox();
            this.txtContentType = new System.Windows.Forms.TextBox();
            this.txtThumbnailFileName = new System.Windows.Forms.TextBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtXml = new System.Windows.Forms.TextBox();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnRender);
            this.splitContainer1.Panel1.Controls.Add(this.btnBrowseThumbnail);
            this.splitContainer1.Panel1.Controls.Add(this.btnBrowse);
            this.splitContainer1.Panel1.Controls.Add(this.cbxRepresentation);
            this.splitContainer1.Panel1.Controls.Add(this.cbxIntegrityCheck);
            this.splitContainer1.Panel1.Controls.Add(this.cbxCompression);
            this.splitContainer1.Panel1.Controls.Add(this.txtContentType);
            this.splitContainer1.Panel1.Controls.Add(this.txtThumbnailFileName);
            this.splitContainer1.Panel1.Controls.Add(this.txtFileName);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtXml);
            this.splitContainer1.Size = new System.Drawing.Size(362, 479);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Integrity Check";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(72, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "File";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Compression";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Thumbnail";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Representation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Content Type";
            // 
            // btnRender
            // 
            this.btnRender.Location = new System.Drawing.Point(278, 172);
            this.btnRender.Name = "btnRender";
            this.btnRender.Size = new System.Drawing.Size(75, 23);
            this.btnRender.TabIndex = 8;
            this.btnRender.Text = "Render";
            this.btnRender.UseVisualStyleBackColor = true;
            this.btnRender.Click += new System.EventHandler(this.btnRender_Click);
            // 
            // btnBrowseThumbnail
            // 
            this.btnBrowseThumbnail.Location = new System.Drawing.Point(321, 117);
            this.btnBrowseThumbnail.Name = "btnBrowseThumbnail";
            this.btnBrowseThumbnail.Size = new System.Drawing.Size(29, 23);
            this.btnBrowseThumbnail.TabIndex = 7;
            this.btnBrowseThumbnail.Text = "...";
            this.btnBrowseThumbnail.UseVisualStyleBackColor = true;
            this.btnBrowseThumbnail.Click += new System.EventHandler(this.btnBrowseThumbnail_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(324, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(29, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // cbxRepresentation
            // 
            this.cbxRepresentation.FormattingEnabled = true;
            this.cbxRepresentation.Items.AddRange(new object[] {
            "Text",
            "Base64",
            "Xml"});
            this.cbxRepresentation.Location = new System.Drawing.Point(101, 92);
            this.cbxRepresentation.Name = "cbxRepresentation";
            this.cbxRepresentation.Size = new System.Drawing.Size(121, 21);
            this.cbxRepresentation.TabIndex = 5;
            // 
            // cbxIntegrityCheck
            // 
            this.cbxIntegrityCheck.FormattingEnabled = true;
            this.cbxIntegrityCheck.Items.AddRange(new object[] {
            "None",
            "SHA1",
            "SHA256"});
            this.cbxIntegrityCheck.Location = new System.Drawing.Point(101, 65);
            this.cbxIntegrityCheck.Name = "cbxIntegrityCheck";
            this.cbxIntegrityCheck.Size = new System.Drawing.Size(121, 21);
            this.cbxIntegrityCheck.TabIndex = 4;
            // 
            // cbxCompression
            // 
            this.cbxCompression.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCompression.FormattingEnabled = true;
            this.cbxCompression.Items.AddRange(new object[] {
            "None",
            "GZip",
            "Deflate"});
            this.cbxCompression.Location = new System.Drawing.Point(101, 38);
            this.cbxCompression.Name = "cbxCompression";
            this.cbxCompression.Size = new System.Drawing.Size(121, 21);
            this.cbxCompression.TabIndex = 3;
            // 
            // txtContentType
            // 
            this.txtContentType.Location = new System.Drawing.Point(101, 145);
            this.txtContentType.Name = "txtContentType";
            this.txtContentType.Size = new System.Drawing.Size(217, 20);
            this.txtContentType.TabIndex = 2;
            this.txtContentType.Text = "text/plain";
            // 
            // txtThumbnailFileName
            // 
            this.txtThumbnailFileName.AcceptsReturn = true;
            this.txtThumbnailFileName.Location = new System.Drawing.Point(101, 119);
            this.txtThumbnailFileName.Name = "txtThumbnailFileName";
            this.txtThumbnailFileName.Size = new System.Drawing.Size(217, 20);
            this.txtThumbnailFileName.TabIndex = 1;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(101, 12);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(217, 20);
            this.txtFileName.TabIndex = 0;
            // 
            // txtXml
            // 
            this.txtXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtXml.Location = new System.Drawing.Point(0, 0);
            this.txtXml.Multiline = true;
            this.txtXml.Name = "txtXml";
            this.txtXml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtXml.Size = new System.Drawing.Size(362, 265);
            this.txtXml.TabIndex = 0;
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.Filter = "All Files (*.*)|*.*";
            // 
            // frmEncapsulatedData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 479);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmEncapsulatedData";
            this.Text = "Encapsulated Data";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnBrowseThumbnail;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ComboBox cbxRepresentation;
        private System.Windows.Forms.ComboBox cbxIntegrityCheck;
        private System.Windows.Forms.ComboBox cbxCompression;
        private System.Windows.Forms.TextBox txtContentType;
        private System.Windows.Forms.TextBox txtThumbnailFileName;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRender;
        private System.Windows.Forms.TextBox txtXml;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
    }
}