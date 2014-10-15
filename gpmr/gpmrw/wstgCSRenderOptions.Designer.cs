namespace gpmrw
{
    partial class wstgCSRenderOptions
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
            this.chkGenPhone = new System.Windows.Forms.CheckBox();
            this.chkGenerateRim = new System.Windows.Forms.CheckBox();
            this.chkCompile = new System.Windows.Forms.CheckBox();
            this.chkDllOnly = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numMaxLiterals = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.txtOrganization = new System.Windows.Forms.TextBox();
            this.txtProfileId = new System.Windows.Forms.TextBox();
            this.cbxLicense = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxLiterals)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "C# / .NET Renderer Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Namespace:";
            // 
            // chkGenPhone
            // 
            this.chkGenPhone.AutoSize = true;
            this.chkGenPhone.Location = new System.Drawing.Point(26, 267);
            this.chkGenPhone.Name = "chkGenPhone";
            this.chkGenPhone.Size = new System.Drawing.Size(254, 17);
            this.chkGenPhone.TabIndex = 3;
            this.chkGenPhone.Text = "Generate Windows Phone / Silverlight Assembly";
            this.chkGenPhone.UseVisualStyleBackColor = true;
            this.chkGenPhone.CheckedChanged += new System.EventHandler(this.chkGenerateVocab_CheckedChanged);
            // 
            // chkGenerateRim
            // 
            this.chkGenerateRim.AutoSize = true;
            this.chkGenerateRim.Location = new System.Drawing.Point(26, 198);
            this.chkGenerateRim.Name = "chkGenerateRim";
            this.chkGenerateRim.Size = new System.Drawing.Size(191, 17);
            this.chkGenerateRim.TabIndex = 5;
            this.chkGenerateRim.Text = "Generate complete RIM structures ";
            this.chkGenerateRim.UseVisualStyleBackColor = true;
            // 
            // chkCompile
            // 
            this.chkCompile.AutoSize = true;
            this.chkCompile.Checked = true;
            this.chkCompile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCompile.Location = new System.Drawing.Point(26, 221);
            this.chkCompile.Name = "chkCompile";
            this.chkCompile.Size = new System.Drawing.Size(236, 17);
            this.chkCompile.TabIndex = 6;
            this.chkCompile.Text = "Compile resultant code into a .NET assembly";
            this.chkCompile.UseVisualStyleBackColor = true;
            this.chkCompile.CheckedChanged += new System.EventHandler(this.chkCompile_CheckedChanged);
            // 
            // chkDllOnly
            // 
            this.chkDllOnly.AutoSize = true;
            this.chkDllOnly.Location = new System.Drawing.Point(26, 244);
            this.chkDllOnly.Name = "chkDllOnly";
            this.chkDllOnly.Size = new System.Drawing.Size(291, 17);
            this.chkDllOnly.TabIndex = 7;
            this.chkDllOnly.Text = "After compiling, remove source files (just produce a DLL)";
            this.chkDllOnly.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Organization:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Realm:";
            // 
            // numMaxLiterals
            // 
            this.numMaxLiterals.Enabled = false;
            this.numMaxLiterals.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numMaxLiterals.Location = new System.Drawing.Point(48, 172);
            this.numMaxLiterals.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMaxLiterals.Name = "numMaxLiterals";
            this.numMaxLiterals.Size = new System.Drawing.Size(58, 20);
            this.numMaxLiterals.TabIndex = 4;
            this.numMaxLiterals.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Default profile ID:";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Enabled = false;
            this.txtNamespace.Location = new System.Drawing.Point(98, 43);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.ReadOnly = true;
            this.txtNamespace.Size = new System.Drawing.Size(311, 20);
            this.txtNamespace.TabIndex = 10;
            this.txtNamespace.Text = "MARC.Everest.RMIM.UV.R020402";
            // 
            // txtOrganization
            // 
            this.txtOrganization.Location = new System.Drawing.Point(98, 69);
            this.txtOrganization.Name = "txtOrganization";
            this.txtOrganization.Size = new System.Drawing.Size(311, 20);
            this.txtOrganization.TabIndex = 0;
            this.txtOrganization.Text = "Copyright Owner\'s Name";
            // 
            // txtProfileId
            // 
            this.txtProfileId.Location = new System.Drawing.Point(98, 122);
            this.txtProfileId.Name = "txtProfileId";
            this.txtProfileId.Size = new System.Drawing.Size(100, 20);
            this.txtProfileId.TabIndex = 2;
            this.txtProfileId.Text = "R02.04.02";
            this.txtProfileId.TextChanged += new System.EventHandler(this.txtProfileId_TextChanged);
            // 
            // cbxLicense
            // 
            this.cbxLicense.FormattingEnabled = true;
            this.cbxLicense.Items.AddRange(new object[] {
            "UV",
            "CA"});
            this.cbxLicense.Location = new System.Drawing.Point(98, 95);
            this.cbxLicense.Name = "cbxLicense";
            this.cbxLicense.Size = new System.Drawing.Size(174, 21);
            this.cbxLicense.TabIndex = 1;
            this.cbxLicense.Text = "UV";
            this.cbxLicense.SelectedIndexChanged += new System.EventHandler(this.cbxLicense_SelectedIndexChanged);
            this.cbxLicense.TextChanged += new System.EventHandler(this.txtProfileId_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(112, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(250, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Maximum number of terms in an enumerated vocab.";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(76, 138);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(291, 17);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "After compiling, remove source files (just produce a DLL)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // wstgCSRenderOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbxLicense);
            this.Controls.Add(this.txtProfileId);
            this.Controls.Add(this.txtOrganization);
            this.Controls.Add(this.txtNamespace);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numMaxLiterals);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkDllOnly);
            this.Controls.Add(this.chkCompile);
            this.Controls.Add(this.chkGenerateRim);
            this.Controls.Add(this.chkGenPhone);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "wstgCSRenderOptions";
            this.Size = new System.Drawing.Size(442, 293);
            this.Load += new System.EventHandler(this.wstgCSRenderOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxLiterals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkGenPhone;
        private System.Windows.Forms.CheckBox chkGenerateRim;
        private System.Windows.Forms.CheckBox chkCompile;
        private System.Windows.Forms.CheckBox chkDllOnly;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numMaxLiterals;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.TextBox txtOrganization;
        private System.Windows.Forms.TextBox txtProfileId;
        private System.Windows.Forms.ComboBox cbxLicense;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
