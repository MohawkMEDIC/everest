namespace DataTypeExplorer
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intanceIdentifierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encapsulatedDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentDescriptorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.provisionedQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.booleanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dataTypesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(470, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // dataTypesToolStripMenuItem
            // 
            this.dataTypesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.intanceIdentifierToolStripMenuItem,
            this.stringToolStripMenuItem,
            this.encapsulatedDataToolStripMenuItem,
            this.contentDescriptorToolStripMenuItem,
            this.provisionedQuantityToolStripMenuItem,
            this.addressToolStripMenuItem,
            this.booleanToolStripMenuItem});
            this.dataTypesToolStripMenuItem.Name = "dataTypesToolStripMenuItem";
            this.dataTypesToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.dataTypesToolStripMenuItem.Text = "Data Types";
            // 
            // intanceIdentifierToolStripMenuItem
            // 
            this.intanceIdentifierToolStripMenuItem.Name = "intanceIdentifierToolStripMenuItem";
            this.intanceIdentifierToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.intanceIdentifierToolStripMenuItem.Text = "Intance Identifier";
            this.intanceIdentifierToolStripMenuItem.Click += new System.EventHandler(this.intanceIdentifierToolStripMenuItem_Click);
            // 
            // stringToolStripMenuItem
            // 
            this.stringToolStripMenuItem.Name = "stringToolStripMenuItem";
            this.stringToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.stringToolStripMenuItem.Text = "String";
            this.stringToolStripMenuItem.Click += new System.EventHandler(this.stringToolStripMenuItem_Click);
            // 
            // encapsulatedDataToolStripMenuItem
            // 
            this.encapsulatedDataToolStripMenuItem.Name = "encapsulatedDataToolStripMenuItem";
            this.encapsulatedDataToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.encapsulatedDataToolStripMenuItem.Text = "Encapsulated Data";
            this.encapsulatedDataToolStripMenuItem.Click += new System.EventHandler(this.encapsulatedDataToolStripMenuItem_Click);
            // 
            // contentDescriptorToolStripMenuItem
            // 
            this.contentDescriptorToolStripMenuItem.Name = "contentDescriptorToolStripMenuItem";
            this.contentDescriptorToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.contentDescriptorToolStripMenuItem.Text = "Content Descriptor";
            this.contentDescriptorToolStripMenuItem.Click += new System.EventHandler(this.contentDescriptorToolStripMenuItem_Click);
            // 
            // provisionedQuantityToolStripMenuItem
            // 
            this.provisionedQuantityToolStripMenuItem.Name = "provisionedQuantityToolStripMenuItem";
            this.provisionedQuantityToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.provisionedQuantityToolStripMenuItem.Text = "Provisioned Quantity";
            this.provisionedQuantityToolStripMenuItem.Click += new System.EventHandler(this.provisionedQuantityToolStripMenuItem_Click);
            // 
            // addressToolStripMenuItem
            // 
            this.addressToolStripMenuItem.Name = "addressToolStripMenuItem";
            this.addressToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.addressToolStripMenuItem.Text = "Address";
            this.addressToolStripMenuItem.Click += new System.EventHandler(this.addressToolStripMenuItem_Click);
            // 
            // booleanToolStripMenuItem
            // 
            this.booleanToolStripMenuItem.Name = "booleanToolStripMenuItem";
            this.booleanToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.booleanToolStripMenuItem.Text = "Boolean";
            this.booleanToolStripMenuItem.Click += new System.EventHandler(this.booleanToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 524);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Data Types Explorer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intanceIdentifierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encapsulatedDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentDescriptorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem provisionedQuantityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem booleanToolStripMenuItem;
    }
}

