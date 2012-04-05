namespace gpmrw
{
    partial class wstgRun
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.pgMain = new System.Windows.Forms.ProgressBar();
            this.tmrStatus = new System.Windows.Forms.Timer(this.components);
            this.bwStatus = new System.ComponentModel.BackgroundWorker();
            this.lnkLogLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(22, 91);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(73, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Please Wait...";
            // 
            // pgMain
            // 
            this.pgMain.Location = new System.Drawing.Point(25, 114);
            this.pgMain.Maximum = 5;
            this.pgMain.Name = "pgMain";
            this.pgMain.Size = new System.Drawing.Size(387, 23);
            this.pgMain.TabIndex = 1;
            // 
            // tmrStatus
            // 
            this.tmrStatus.Interval = 250;
            this.tmrStatus.Tick += new System.EventHandler(this.tmrStatus_Tick);
            // 
            // bwStatus
            // 
            this.bwStatus.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwStatus_DoWork);
            this.bwStatus.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwStatus_RunWorkerCompleted);
            // 
            // lnkLogLabel
            // 
            this.lnkLogLabel.AutoSize = true;
            this.lnkLogLabel.Location = new System.Drawing.Point(22, 140);
            this.lnkLogLabel.Name = "lnkLogLabel";
            this.lnkLogLabel.Size = new System.Drawing.Size(107, 13);
            this.lnkLogLabel.TabIndex = 2;
            this.lnkLogLabel.TabStop = true;
            this.lnkLogLabel.Text = "Log File Of Operation";
            this.lnkLogLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLogLabel_LinkClicked);
            // 
            // wstgRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkLogLabel);
            this.Controls.Add(this.pgMain);
            this.Controls.Add(this.lblStatus);
            this.Name = "wstgRun";
            this.Size = new System.Drawing.Size(442, 293);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar pgMain;
        private System.Windows.Forms.Timer tmrStatus;
        private System.ComponentModel.BackgroundWorker bwStatus;
        private System.Windows.Forms.LinkLabel lnkLogLabel;
    }
}
