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
            this.chkVocab = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxLiterals)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "C# / .NET Renderer Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 71);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Namespace:";
            // 
            // chkGenPhone
            // 
            this.chkGenPhone.AutoSize = true;
            this.chkGenPhone.Location = new System.Drawing.Point(39, 411);
            this.chkGenPhone.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkGenPhone.Name = "chkGenPhone";
            this.chkGenPhone.Size = new System.Drawing.Size(372, 24);
            this.chkGenPhone.TabIndex = 3;
            this.chkGenPhone.Text = "Generate Windows Phone / Silverlight Assembly";
            this.chkGenPhone.UseVisualStyleBackColor = true;
            // 
            // chkGenerateRim
            // 
            this.chkGenerateRim.AutoSize = true;
            this.chkGenerateRim.Location = new System.Drawing.Point(39, 305);
            this.chkGenerateRim.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkGenerateRim.Name = "chkGenerateRim";
            this.chkGenerateRim.Size = new System.Drawing.Size(285, 24);
            this.chkGenerateRim.TabIndex = 5;
            this.chkGenerateRim.Text = "Generate complete RIM structures ";
            this.chkGenerateRim.UseVisualStyleBackColor = true;
            // 
            // chkCompile
            // 
            this.chkCompile.AutoSize = true;
            this.chkCompile.Checked = true;
            this.chkCompile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCompile.Location = new System.Drawing.Point(39, 340);
            this.chkCompile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkCompile.Name = "chkCompile";
            this.chkCompile.Size = new System.Drawing.Size(349, 24);
            this.chkCompile.TabIndex = 6;
            this.chkCompile.Text = "Compile resultant code into a .NET assembly";
            this.chkCompile.UseVisualStyleBackColor = true;
            this.chkCompile.CheckedChanged += new System.EventHandler(this.chkCompile_CheckedChanged);
            // 
            // chkDllOnly
            // 
            this.chkDllOnly.AutoSize = true;
            this.chkDllOnly.Location = new System.Drawing.Point(39, 375);
            this.chkDllOnly.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDllOnly.Name = "chkDllOnly";
            this.chkDllOnly.Size = new System.Drawing.Size(432, 24);
            this.chkDllOnly.TabIndex = 7;
            this.chkDllOnly.Text = "After compiling, remove source files (just produce a DLL)";
            this.chkDllOnly.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 112);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Organization:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(78, 151);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 20);
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
            this.numMaxLiterals.Location = new System.Drawing.Point(72, 265);
            this.numMaxLiterals.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numMaxLiterals.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMaxLiterals.Name = "numMaxLiterals";
            this.numMaxLiterals.Size = new System.Drawing.Size(87, 26);
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
            this.label5.Location = new System.Drawing.Point(4, 192);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Default profile ID:";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Enabled = false;
            this.txtNamespace.Location = new System.Drawing.Point(147, 66);
            this.txtNamespace.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.ReadOnly = true;
            this.txtNamespace.Size = new System.Drawing.Size(464, 26);
            this.txtNamespace.TabIndex = 10;
            this.txtNamespace.Text = "MARC.Everest.RMIM.UV.R020402";
            // 
            // txtOrganization
            // 
            this.txtOrganization.Location = new System.Drawing.Point(147, 106);
            this.txtOrganization.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtOrganization.Name = "txtOrganization";
            this.txtOrganization.Size = new System.Drawing.Size(464, 26);
            this.txtOrganization.TabIndex = 0;
            this.txtOrganization.Text = "Copyright Owner\'s Name";
            // 
            // txtProfileId
            // 
            this.txtProfileId.Location = new System.Drawing.Point(147, 188);
            this.txtProfileId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtProfileId.Name = "txtProfileId";
            this.txtProfileId.Size = new System.Drawing.Size(148, 26);
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
            this.cbxLicense.Location = new System.Drawing.Point(147, 146);
            this.cbxLicense.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxLicense.Name = "cbxLicense";
            this.cbxLicense.Size = new System.Drawing.Size(259, 28);
            this.cbxLicense.TabIndex = 1;
            this.cbxLicense.Text = "UV";
            this.cbxLicense.SelectedIndexChanged += new System.EventHandler(this.cbxLicense_SelectedIndexChanged);
            this.cbxLicense.TextChanged += new System.EventHandler(this.txtProfileId_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(168, 269);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(374, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Maximum number of terms in an enumerated vocab.";
            // 
            // chkVocab
            // 
            this.chkVocab.AutoSize = true;
            this.chkVocab.Location = new System.Drawing.Point(42, 231);
            this.chkVocab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkVocab.Name = "chkVocab";
            this.chkVocab.Size = new System.Drawing.Size(437, 24);
            this.chkVocab.TabIndex = 15;
            this.chkVocab.Text = "Enumerate and generate Value Sets, and Code Systems";
            this.chkVocab.UseVisualStyleBackColor = true;
            this.chkVocab.CheckedChanged += new System.EventHandler(this.chkGenerateVocab_CheckedChanged);
            // 
            // wstgCSRenderOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkVocab);
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
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "wstgCSRenderOptions";
            this.Size = new System.Drawing.Size(663, 451);
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
        private System.Windows.Forms.CheckBox chkVocab;
    }
}
