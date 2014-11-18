namespace SnowProCorp.Factory
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnGenerateData = new System.Windows.Forms.Button();
            this.btnItinitiateSystem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SnowProCorp.Factory.Properties.Resources._502px_Factory_svg;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(478, 365);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnGenerateData
            // 
            this.btnGenerateData.Location = new System.Drawing.Point(619, 46);
            this.btnGenerateData.Name = "btnGenerateData";
            this.btnGenerateData.Size = new System.Drawing.Size(267, 31);
            this.btnGenerateData.TabIndex = 1;
            this.btnGenerateData.Text = "Generate Data";
            this.btnGenerateData.UseVisualStyleBackColor = true;
            this.btnGenerateData.Click += new System.EventHandler(this.generateData_Click);
            // 
            // btnItinitiateSystem
            // 
            this.btnItinitiateSystem.Location = new System.Drawing.Point(619, 13);
            this.btnItinitiateSystem.Name = "btnItinitiateSystem";
            this.btnItinitiateSystem.Size = new System.Drawing.Size(267, 27);
            this.btnItinitiateSystem.TabIndex = 2;
            this.btnItinitiateSystem.Text = "Initiate System";
            this.btnItinitiateSystem.UseVisualStyleBackColor = true;
            this.btnItinitiateSystem.Click += new System.EventHandler(this.btnItinitiateSystem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 692);
            this.Controls.Add(this.btnItinitiateSystem);
            this.Controls.Add(this.btnGenerateData);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "SnowCorp Factory";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnGenerateData;
        private System.Windows.Forms.Button btnItinitiateSystem;
    }
}

