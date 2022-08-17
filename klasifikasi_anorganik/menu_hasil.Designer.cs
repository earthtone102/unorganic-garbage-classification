namespace klasifikasi_anorganik
{
    partial class menu_hasil
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
            this.label1 = new System.Windows.Forms.Label();
            this.pbRgb = new System.Windows.Forms.PictureBox();
            this.txtMaterial = new System.Windows.Forms.TextBox();
            this.pbGraysScale = new System.Windows.Forms.PictureBox();
            this.pbContrastS = new System.Windows.Forms.PictureBox();
            this.lvGlrlm = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lvGlcm = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.pbRgb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraysScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbContrastS)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(185, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hasil Klasifikasi";
            // 
            // pbRgb
            // 
            this.pbRgb.Location = new System.Drawing.Point(12, 42);
            this.pbRgb.Name = "pbRgb";
            this.pbRgb.Size = new System.Drawing.Size(162, 110);
            this.pbRgb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRgb.TabIndex = 1;
            this.pbRgb.TabStop = false;
            // 
            // txtMaterial
            // 
            this.txtMaterial.Enabled = false;
            this.txtMaterial.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaterial.Location = new System.Drawing.Point(152, 170);
            this.txtMaterial.Name = "txtMaterial";
            this.txtMaterial.ReadOnly = true;
            this.txtMaterial.Size = new System.Drawing.Size(234, 28);
            this.txtMaterial.TabIndex = 2;
            this.txtMaterial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pbGraysScale
            // 
            this.pbGraysScale.Location = new System.Drawing.Point(180, 42);
            this.pbGraysScale.Name = "pbGraysScale";
            this.pbGraysScale.Size = new System.Drawing.Size(162, 110);
            this.pbGraysScale.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbGraysScale.TabIndex = 4;
            this.pbGraysScale.TabStop = false;
            // 
            // pbContrastS
            // 
            this.pbContrastS.Location = new System.Drawing.Point(348, 42);
            this.pbContrastS.Name = "pbContrastS";
            this.pbContrastS.Size = new System.Drawing.Size(162, 110);
            this.pbContrastS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbContrastS.TabIndex = 5;
            this.pbContrastS.TabStop = false;
            // 
            // lvGlrlm
            // 
            this.lvGlrlm.Location = new System.Drawing.Point(269, 228);
            this.lvGlrlm.Name = "lvGlrlm";
            this.lvGlrlm.Size = new System.Drawing.Size(241, 128);
            this.lvGlrlm.TabIndex = 7;
            this.lvGlrlm.UseCompatibleStateImageBehavior = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Nilai Ciri GLCM :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(272, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Nilai Ciri GLRLM :";
            // 
            // lvGlcm
            // 
            this.lvGlcm.Location = new System.Drawing.Point(12, 228);
            this.lvGlcm.Name = "lvGlcm";
            this.lvGlcm.Size = new System.Drawing.Size(241, 128);
            this.lvGlcm.TabIndex = 10;
            this.lvGlcm.UseCompatibleStateImageBehavior = false;
            // 
            // menu_hasil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(522, 368);
            this.Controls.Add(this.lvGlcm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lvGlrlm);
            this.Controls.Add(this.pbContrastS);
            this.Controls.Add(this.pbGraysScale);
            this.Controls.Add(this.txtMaterial);
            this.Controls.Add(this.pbRgb);
            this.Controls.Add(this.label1);
            this.Name = "menu_hasil";
            this.Text = "Menu Hasil";
            this.Load += new System.EventHandler(this.menu_hasil_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbRgb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraysScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbContrastS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbRgb;
        private System.Windows.Forms.TextBox txtMaterial;
        private System.Windows.Forms.PictureBox pbGraysScale;
        private System.Windows.Forms.PictureBox pbContrastS;
        private System.Windows.Forms.ListView lvGlrlm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lvGlcm;
    }
}