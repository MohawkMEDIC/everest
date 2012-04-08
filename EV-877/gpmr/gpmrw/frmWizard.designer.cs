namespace gpmrw
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWizard));
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Welcome");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Select Source Files");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Optimizations");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Configure Renderers");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Set Output");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Verify");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Convert");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Complete");
            this.pnlTopImage = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnNextFinish = new System.Windows.Forms.Button();
            this.btnExitPrevious = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.grpTitle = new System.Windows.Forms.GroupBox();
            this.pnlSplit = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.trvStages = new System.Windows.Forms.TreeView();
            this.tmrUpdateUI = new System.Windows.Forms.Timer(this.components);
            this.pnlTopImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.grpTitle.SuspendLayout();
            this.pnlSplit.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlContent.Location = new System.Drawing.Point(132, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(442, 293);
            this.pnlContent.TabIndex = 4;
            // 
            // grpTitle
            // 
            this.grpTitle.Controls.Add(this.btnExitPrevious);
            this.grpTitle.Controls.Add(this.btnNextFinish);
            this.grpTitle.Location = new System.Drawing.Point(-3, 347);
            this.grpTitle.Name = "grpTitle";
            this.grpTitle.Size = new System.Drawing.Size(577, 58);
            this.grpTitle.TabIndex = 5;
            this.grpTitle.TabStop = false;
            // 
            // pnlSplit
            // 
            this.pnlSplit.Controls.Add(this.panel1);
            this.pnlSplit.Controls.Add(this.pnlContent);
            this.pnlSplit.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSplit.Location = new System.Drawing.Point(0, 61);
            this.pnlSplit.Name = "pnlSplit";
            this.pnlSplit.Size = new System.Drawing.Size(574, 293);
            this.pnlSplit.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.trvStages);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(134, 293);
            this.panel1.TabIndex = 5;
            // 
            // trvStages
            // 
            this.trvStages.BackColor = System.Drawing.SystemColors.Control;
            this.trvStages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvStages.HideSelection = false;
            this.trvStages.Location = new System.Drawing.Point(0, 0);
            this.trvStages.Name = "trvStages";
            treeNode9.Name = "Node0";
            treeNode9.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode9.Text = "Welcome";
            treeNode10.Name = "Node1";
            treeNode10.Text = "Select Source Files";
            treeNode11.Name = "Node2";
            treeNode11.Text = "Optimizations";
            treeNode12.Name = "ndRenderers";
            treeNode12.Text = "Configure Renderers";
            treeNode13.Name = "Node5";
            treeNode13.Text = "Set Output";
            treeNode14.Name = "Node4";
            treeNode14.Text = "Verify";
            treeNode15.Name = "Node6";
            treeNode15.Text = "Convert";
            treeNode16.Name = "Node0";
            treeNode16.Text = "Complete";
            this.trvStages.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16});
            this.trvStages.Scrollable = false;
            this.trvStages.ShowLines = false;
            this.trvStages.ShowPlusMinus = false;
            this.trvStages.ShowRootLines = false;
            this.trvStages.Size = new System.Drawing.Size(134, 293);
            this.trvStages.TabIndex = 0;
            this.trvStages.TabStop = false;
            // 
            // tmrUpdateUI
            // 
            this.tmrUpdateUI.Enabled = true;
            this.tmrUpdateUI.Interval = 500;
            this.tmrUpdateUI.Tick += new System.EventHandler(this.tmrUpdateUI_Tick);
            // 
            // frmWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 389);
            this.Controls.Add(this.pnlSplit);
            this.Controls.Add(this.grpTitle);
            this.Controls.Add(this.pnlTopImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "General Purpose MIF Renderer Wizard";
            this.Load += new System.EventHandler(this.frmWizard_Load);
            this.pnlTopImage.ResumeLayout(false);
            this.pnlTopImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.grpTitle.ResumeLayout(false);
            this.pnlSplit.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.Panel pnlSplit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView trvStages;
        private System.Windows.Forms.Timer tmrUpdateUI;

    }
}