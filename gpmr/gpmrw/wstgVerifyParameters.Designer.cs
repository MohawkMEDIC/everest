namespace gpmrw
{
    partial class wstgVerifyParameters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wstgVerifyParameters));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCommandLine = new System.Windows.Forms.TextBox();
            this.txtParameters = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(428, 47);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(400, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "For your reference, the command line to run GPMR with the specified parameters is" +
                ":";
            // 
            // txtCommandLine
            // 
            this.txtCommandLine.Location = new System.Drawing.Point(6, 205);
            this.txtCommandLine.Multiline = true;
            this.txtCommandLine.Name = "txtCommandLine";
            this.txtCommandLine.ReadOnly = true;
            this.txtCommandLine.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCommandLine.Size = new System.Drawing.Size(425, 85);
            this.txtCommandLine.TabIndex = 2;
            // 
            // txtParameters
            // 
            this.txtParameters.Location = new System.Drawing.Point(6, 70);
            this.txtParameters.Multiline = true;
            this.txtParameters.Name = "txtParameters";
            this.txtParameters.ReadOnly = true;
            this.txtParameters.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtParameters.Size = new System.Drawing.Size(425, 105);
            this.txtParameters.TabIndex = 3;
            // 
            // wstgVerifyParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtParameters);
            this.Controls.Add(this.txtCommandLine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "wstgVerifyParameters";
            this.Size = new System.Drawing.Size(442, 293);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCommandLine;
        private System.Windows.Forms.TextBox txtParameters;
    }
}
