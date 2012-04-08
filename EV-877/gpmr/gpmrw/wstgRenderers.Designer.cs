namespace gpmrw
{
    partial class wstgRenderers
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rdoCS = new System.Windows.Forms.RadioButton();
            this.rdoJava = new System.Windows.Forms.RadioButton();
            this.rdoWiki = new System.Windows.Forms.RadioButton();
            this.rdoXML = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the renderer that you would like GPMR to use when rendering your MIF files" +
                "";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(65, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(353, 33);
            this.label2.TabIndex = 5;
            this.label2.Text = "The C# renderer can be used to generate Visual Studio projects, .NET Assemblies a" +
                "nd C# files that represent the structures in the source MIFs";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(65, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(353, 33);
            this.label3.TabIndex = 6;
            this.label3.Text = "The Java renderer can be used to generate Eclipse projects, JAR files and Java so" +
                "urce that represent the structures in the source MIFs";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(65, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(353, 33);
            this.label4.TabIndex = 7;
            this.label4.Text = "The DekiWiki and HTML renderer can be used to generate documentation which can be" +
                " published to a Mindtouch Wiki server";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(65, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(353, 46);
            this.label5.TabIndex = 8;
            this.label5.Text = "The XML renderer can be used to generate XSD files describing an optimized set of" +
                " structures as well as XSLT files to translate between optimized and non-optimiz" +
                "ed instances";
            // 
            // rdoCS
            // 
            this.rdoCS.AutoSize = true;
            this.rdoCS.Checked = true;
            this.rdoCS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCS.Location = new System.Drawing.Point(41, 42);
            this.rdoCS.Name = "rdoCS";
            this.rdoCS.Size = new System.Drawing.Size(183, 17);
            this.rdoCS.TabIndex = 0;
            this.rdoCS.TabStop = true;
            this.rdoCS.Text = "RIMBA C# / .NET Renderer";
            this.rdoCS.UseVisualStyleBackColor = true;
            this.rdoCS.CheckedChanged += new System.EventHandler(this.rdoCS_CheckedChanged);
            // 
            // rdoJava
            // 
            this.rdoJava.AutoSize = true;
            this.rdoJava.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoJava.Location = new System.Drawing.Point(41, 98);
            this.rdoJava.Name = "rdoJava";
            this.rdoJava.Size = new System.Drawing.Size(151, 17);
            this.rdoJava.TabIndex = 1;
            this.rdoJava.Text = "RIMBA Java Renderer";
            this.rdoJava.UseVisualStyleBackColor = true;
            this.rdoJava.CheckedChanged += new System.EventHandler(this.rdoJava_CheckedChanged);
            // 
            // rdoWiki
            // 
            this.rdoWiki.AutoSize = true;
            this.rdoWiki.Enabled = false;
            this.rdoWiki.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoWiki.Location = new System.Drawing.Point(41, 154);
            this.rdoWiki.Name = "rdoWiki";
            this.rdoWiki.Size = new System.Drawing.Size(195, 17);
            this.rdoWiki.TabIndex = 2;
            this.rdoWiki.Text = "Wiki Documentation Renderer";
            this.rdoWiki.UseVisualStyleBackColor = true;
            // 
            // rdoXML
            // 
            this.rdoXML.AutoSize = true;
            this.rdoXML.Enabled = false;
            this.rdoXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoXML.Location = new System.Drawing.Point(41, 210);
            this.rdoXML.Name = "rdoXML";
            this.rdoXML.Size = new System.Drawing.Size(165, 17);
            this.rdoXML.TabIndex = 3;
            this.rdoXML.Text = "Optimized XML Renderer";
            this.rdoXML.UseVisualStyleBackColor = true;
            // 
            // wstgRenderers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rdoXML);
            this.Controls.Add(this.rdoWiki);
            this.Controls.Add(this.rdoJava);
            this.Controls.Add(this.rdoCS);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "wstgRenderers";
            this.Size = new System.Drawing.Size(442, 293);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdoCS;
        private System.Windows.Forms.RadioButton rdoJava;
        private System.Windows.Forms.RadioButton rdoWiki;
        private System.Windows.Forms.RadioButton rdoXML;
    }
}
