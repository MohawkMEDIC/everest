namespace MARC.Everest.VisualStudio.Wizards
{
    partial class frmWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWizard));
            this.pnlTopImage = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnNextFinish = new System.Windows.Forms.Button();
            this.btnExitPrevious = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.grpTitle = new System.Windows.Forms.GroupBox();
            this.pnlTopImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.grpTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopImage
            // 
            this.pnlTopImage.BackColor = System.Drawing.Color.White;
            this.pnlTopImage.Controls.Add(this.lblStatus);
            this.pnlTopImage.Controls.Add(this.pbImage);
            this.pnlTopImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopImage.Location = new System.Drawing.Point(0, 0);
            this.pnlTopImage.Name = "pnlTopImage";
            this.pnlTopImage.Size = new System.Drawing.Size(574, 61);
            this.pnlTopImage.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(12, 24);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(43, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Status";
            // 
            // pbImage
            // 
            this.pbImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbImage.Image = ((System.Drawing.Image)(resources.GetObject("pbImage.Image")));
            this.pbImage.Location = new System.Drawing.Point(507, 0);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(67, 61);
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // btnNextFinish
            // 
            this.btnNextFinish.Location = new System.Drawing.Point(489, 13);
            this.btnNextFinish.Name = "btnNextFinish";
            this.btnNextFinish.Size = new System.Drawing.Size(76, 23);
            this.btnNextFinish.TabIndex = 2;
            this.btnNextFinish.Text = "Next";
            this.btnNextFinish.UseVisualStyleBackColor = true;
            this.btnNextFinish.Click += new System.EventHandler(this.btnNextFinish_Click);
            // 
            // btnExitPrevious
            // 
            this.btnExitPrevious.Location = new System.Drawing.Point(408, 13);
            this.btnExitPrevious.Name = "btnExitPrevious";
            this.btnExitPrevious.Size = new System.Drawing.Size(76, 23);
            this.btnExitPrevious.TabIndex = 3;
            this.btnExitPrevious.Text = "Cancel";
            this.btnExitPrevious.UseVisualStyleBackColor = true;
            this.btnExitPrevious.Click += new System.EventHandler(this.btnExitPrevious_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlContent.Location = new System.Drawing.Point(0, 61);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(574, 295);
            this.pnlContent.TabIndex = 4;
            // 
            // grpTitle
            // 
            this.grpTitle.Controls.Add(this.btnExitPrevious);
            this.grpTitle.Controls.Add(this.btnNextFinish);
            this.grpTitle.Location = new System.Drawing.Point(-3, 347);
            this.grpTitle.Name = "grpTitle";
            this.grpTitle.Size = new System.Drawing.Size(592, 106);
            this.grpTitle.TabIndex = 5;
            this.grpTitle.TabStop = false;
            // 
            // frmWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 389);
            this.Controls.Add(this.grpTitle);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTopImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWizard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wizard";
            this.Load += new System.EventHandler(this.frmWizard_Load);
            this.pnlTopImage.ResumeLayout(false);
            this.pnlTopImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.grpTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopImage;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button btnNextFinish;
        private System.Windows.Forms.Button btnExitPrevious;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.GroupBox grpTitle;

    }
}