namespace MARC.Everest.VisualStudio.Wizards.Stages
{
    partial class wstgSelectRelease
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboGPMR = new System.Windows.Forms.ComboBox();
            this.cklFeatures = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the Everest Framework features that your project will use";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Message Types";
            // 
            // cboGPMR
            // 
            this.cboGPMR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGPMR.FormattingEnabled = true;
            this.cboGPMR.Location = new System.Drawing.Point(170, 62);
            this.cboGPMR.Name = "cboGPMR";
            this.cboGPMR.Size = new System.Drawing.Size(332, 21);
            this.cboGPMR.TabIndex = 2;
            // 
            // cklFeatures
            // 
            this.cklFeatures.FormattingEnabled = true;
            this.cklFeatures.Location = new System.Drawing.Point(74, 118);
            this.cklFeatures.Name = "cklFeatures";
            this.cklFeatures.Size = new System.Drawing.Size(428, 154);
            this.cklFeatures.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Everest Features";
            // 
            // wstgSelectRelease
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cklFeatures);
            this.Controls.Add(this.cboGPMR);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "wstgSelectRelease";
            this.Size = new System.Drawing.Size(595, 295);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboGPMR;
        private System.Windows.Forms.CheckedListBox cklFeatures;
        private System.Windows.Forms.Label label3;
    }
}
