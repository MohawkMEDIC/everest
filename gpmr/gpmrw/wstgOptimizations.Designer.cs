namespace gpmrw
{
    partial class wstgOptimizations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wstgOptimizations));
            this.label1 = new System.Windows.Forms.Label();
            this.chkCombine = new System.Windows.Forms.CheckBox();
            this.chkCollapse = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select optimizations that you would like GPMR to perform:";
            // 
            // chkCombine
            // 
            this.chkCombine.AutoSize = true;
            this.chkCombine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCombine.Location = new System.Drawing.Point(44, 48);
            this.chkCombine.Name = "chkCombine";
            this.chkCombine.Size = new System.Drawing.Size(224, 17);
            this.chkCombine.TabIndex = 1;
            this.chkCombine.Text = "Class Combination (Recommended)";
            this.chkCombine.UseVisualStyleBackColor = true;
            // 
            // chkCollapse
            // 
            this.chkCollapse.AutoSize = true;
            this.chkCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCollapse.Location = new System.Drawing.Point(44, 155);
            this.chkCollapse.Name = "chkCollapse";
            this.chkCollapse.Size = new System.Drawing.Size(237, 17);
            this.chkCollapse.TabIndex = 2;
            this.chkCollapse.Text = "Class Collapsing (Not Recommended)";
            this.chkCollapse.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(64, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 84);
            this.label2.TabIndex = 3;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(64, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(330, 97);
            this.label3.TabIndex = 4;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // wstgOptimizations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkCollapse);
            this.Controls.Add(this.chkCombine);
            this.Controls.Add(this.label1);
            this.Name = "wstgOptimizations";
            this.Size = new System.Drawing.Size(442, 293);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkCombine;
        private System.Windows.Forms.CheckBox chkCollapse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
