namespace DataTypeExplorer
{
    partial class frmBoolean
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
            this.radTrue = new System.Windows.Forms.RadioButton();
            this.radFalse = new System.Windows.Forms.RadioButton();
            this.radNull = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtXml = new System.Windows.Forms.TextBox();
            this.btnRender = new System.Windows.Forms.Button();
            this.cbxNullFlavor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.cbxNullFlavor);
            this.splitContainer1.Panel1.Controls.Add(this.btnRender);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.radNull);
            this.splitContainer1.Panel1.Controls.Add(this.radFalse);
            this.splitContainer1.Panel1.Controls.Add(this.radTrue);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtXml);
            this.splitContainer1.Size = new System.Drawing.Size(199, 316);
            this.splitContainer1.SplitterDistance = 141;
            this.splitContainer1.TabIndex = 0;
            // 
            // radTrue
            // 
            this.radTrue.AutoSize = true;
            this.radTrue.Checked = true;
            this.radTrue.Location = new System.Drawing.Point(71, 12);
            this.radTrue.Name = "radTrue";
            this.radTrue.Size = new System.Drawing.Size(47, 17);
            this.radTrue.TabIndex = 0;
            this.radTrue.TabStop = true;
            this.radTrue.Text = "True";
            this.radTrue.UseVisualStyleBackColor = true;
            // 
            // radFalse
            // 
            this.radFalse.AutoSize = true;
            this.radFalse.Location = new System.Drawing.Point(71, 35);
            this.radFalse.Name = "radFalse";
            this.radFalse.Size = new System.Drawing.Size(50, 17);
            this.radFalse.TabIndex = 1;
            this.radFalse.Text = "False";
            this.radFalse.UseVisualStyleBackColor = true;
            // 
            // radNull
            // 
            this.radNull.AutoSize = true;
            this.radNull.Location = new System.Drawing.Point(71, 58);
            this.radNull.Name = "radNull";
            this.radNull.Size = new System.Drawing.Size(43, 17);
            this.radNull.TabIndex = 2;
            this.radNull.Text = "Null";
            this.radNull.UseVisualStyleBackColor = true;
            this.radNull.CheckedChanged += new System.EventHandler(this.radNull_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Value";
            // 
            // txtXml
            // 
            this.txtXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtXml.Location = new System.Drawing.Point(0, 0);
            this.txtXml.Multiline = true;
            this.txtXml.Name = "txtXml";
            this.txtXml.ReadOnly = true;
            this.txtXml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtXml.Size = new System.Drawing.Size(199, 171);
            this.txtXml.TabIndex = 0;
            // 
            // btnRender
            // 
            this.btnRender.Location = new System.Drawing.Point(117, 108);
            this.btnRender.Name = "btnRender";
            this.btnRender.Size = new System.Drawing.Size(75, 23);
            this.btnRender.TabIndex = 4;
            this.btnRender.Text = "Render";
            this.btnRender.UseVisualStyleBackColor = true;
            this.btnRender.Click += new System.EventHandler(this.btnRender_Click);
            // 
            // cbxNullFlavor
            // 
            this.cbxNullFlavor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxNullFlavor.Enabled = false;
            this.cbxNullFlavor.FormattingEnabled = true;
            this.cbxNullFlavor.Items.AddRange(new object[] {
            "NAV",
            "NI",
            "OTH"});
            this.cbxNullFlavor.Location = new System.Drawing.Point(71, 81);
            this.cbxNullFlavor.Name = "cbxNullFlavor";
            this.cbxNullFlavor.Size = new System.Drawing.Size(121, 21);
            this.cbxNullFlavor.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Null Flavor";
            // 
            // frmBoolean
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 316);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmBoolean";
            this.Text = "Boolean";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radNull;
        private System.Windows.Forms.RadioButton radFalse;
        private System.Windows.Forms.RadioButton radTrue;
        private System.Windows.Forms.TextBox txtXml;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxNullFlavor;
    }
}