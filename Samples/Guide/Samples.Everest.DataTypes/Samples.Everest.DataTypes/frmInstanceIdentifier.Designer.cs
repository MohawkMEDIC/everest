namespace DataTypeExplorer
{
    partial class frmInstanceIdentifier
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
            this.txtRoot = new System.Windows.Forms.TextBox();
            this.txtExtension = new System.Windows.Forms.TextBox();
            this.txtXml = new System.Windows.Forms.TextBox();
            this.cbxFlavor = new System.Windows.Forms.ComboBox();
            this.cbxNullFlavor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRender = new System.Windows.Forms.Button();
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
            this.splitContainer1.Panel1.Controls.Add(this.btnRender);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cbxNullFlavor);
            this.splitContainer1.Panel1.Controls.Add(this.cbxFlavor);
            this.splitContainer1.Panel1.Controls.Add(this.txtExtension);
            this.splitContainer1.Panel1.Controls.Add(this.txtRoot);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtXml);
            this.splitContainer1.Size = new System.Drawing.Size(207, 273);
            this.splitContainer1.SplitterDistance = 139;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtRoot
            // 
            this.txtRoot.Location = new System.Drawing.Point(77, 57);
            this.txtRoot.Name = "txtRoot";
            this.txtRoot.Size = new System.Drawing.Size(121, 20);
            this.txtRoot.TabIndex = 0;
            // 
            // txtExtension
            // 
            this.txtExtension.Location = new System.Drawing.Point(77, 82);
            this.txtExtension.Name = "txtExtension";
            this.txtExtension.Size = new System.Drawing.Size(121, 20);
            this.txtExtension.TabIndex = 1;
            // 
            // txtXml
            // 
            this.txtXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtXml.Location = new System.Drawing.Point(0, 0);
            this.txtXml.Multiline = true;
            this.txtXml.Name = "txtXml";
            this.txtXml.ReadOnly = true;
            this.txtXml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtXml.Size = new System.Drawing.Size(207, 130);
            this.txtXml.TabIndex = 0;
            // 
            // cbxFlavor
            // 
            this.cbxFlavor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFlavor.FormattingEnabled = true;
            this.cbxFlavor.Items.AddRange(new object[] {
            "BUS",
            "PUBLIC",
            "TOKEN"});
            this.cbxFlavor.Location = new System.Drawing.Point(77, 3);
            this.cbxFlavor.Name = "cbxFlavor";
            this.cbxFlavor.Size = new System.Drawing.Size(121, 21);
            this.cbxFlavor.TabIndex = 2;
            // 
            // cbxNullFlavor
            // 
            this.cbxNullFlavor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxNullFlavor.FormattingEnabled = true;
            this.cbxNullFlavor.Items.AddRange(new object[] {
            "None",
            "NAV",
            "NI",
            "OTH"});
            this.cbxNullFlavor.Location = new System.Drawing.Point(77, 30);
            this.cbxNullFlavor.Name = "cbxNullFlavor";
            this.cbxNullFlavor.Size = new System.Drawing.Size(121, 21);
            this.cbxNullFlavor.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Flavor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Null Flavor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Root";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Extension";
            // 
            // btnRender
            // 
            this.btnRender.Location = new System.Drawing.Point(123, 108);
            this.btnRender.Name = "btnRender";
            this.btnRender.Size = new System.Drawing.Size(75, 23);
            this.btnRender.TabIndex = 8;
            this.btnRender.Text = "Render";
            this.btnRender.UseVisualStyleBackColor = true;
            this.btnRender.Click += new System.EventHandler(this.btnRender_Click);
            // 
            // frmInstanceIdentifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(207, 273);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmInstanceIdentifier";
            this.Text = "Instance Identifier";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnRender;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxNullFlavor;
        private System.Windows.Forms.ComboBox cbxFlavor;
        private System.Windows.Forms.TextBox txtExtension;
        private System.Windows.Forms.TextBox txtRoot;
        private System.Windows.Forms.TextBox txtXml;
    }
}