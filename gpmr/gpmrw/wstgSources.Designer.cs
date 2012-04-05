namespace gpmrw
{
    partial class wstgSources
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wstgSources));
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lsvMifs = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.imlNotes = new System.Windows.Forms.ImageList(this.components);
            this.btnRemove = new System.Windows.Forms.Button();
            this.dlgOpenMif = new System.Windows.Forms.OpenFileDialog();
            this.lblState = new System.Windows.Forms.Label();
            this.pbStatus = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(344, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the MIF files that you wish to process and click Next to continue.";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(367, 48);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(61, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add...";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lsvMifs
            // 
            this.lsvMifs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lsvMifs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvMifs.HideSelection = false;
            this.lsvMifs.Location = new System.Drawing.Point(20, 48);
            this.lsvMifs.Name = "lsvMifs";
            this.lsvMifs.ShowItemToolTips = true;
            this.lsvMifs.Size = new System.Drawing.Size(341, 211);
            this.lsvMifs.SmallImageList = this.imlNotes;
            this.lsvMifs.TabIndex = 1;
            this.lsvMifs.UseCompatibleStateImageBehavior = false;
            this.lsvMifs.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File";
            this.columnHeader1.Width = 276;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Version";
            // 
            // imlNotes
            // 
            this.imlNotes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlNotes.ImageStream")));
            this.imlNotes.TransparentColor = System.Drawing.Color.Magenta;
            this.imlNotes.Images.SetKeyName(0, "Serious.bmp");
            this.imlNotes.Images.SetKeyName(1, "SuccessComplete.bmp");
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(367, 77);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(61, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // dlgOpenMif
            // 
            this.dlgOpenMif.Filter = "Model Interchange Files (*.mif;*.coremif)|*.mif;*.coremif";
            this.dlgOpenMif.Multiselect = true;
            this.dlgOpenMif.Title = "Add MIF Files";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(39, 266);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(127, 13);
            this.lblState.TabIndex = 5;
            this.lblState.Text = "All files appear to be valid";
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(20, 265);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(16, 16);
            this.pbStatus.TabIndex = 6;
            this.pbStatus.TabStop = false;
            // 
            // wstgSources
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lsvMifs);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Name = "wstgSources";
            this.Size = new System.Drawing.Size(442, 293);
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListView lsvMifs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ImageList imlNotes;
        private System.Windows.Forms.OpenFileDialog dlgOpenMif;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.PictureBox pbStatus;
    }
}
