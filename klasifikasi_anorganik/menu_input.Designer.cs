namespace klasifikasi_anorganik
{
    partial class menu_input
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
            this.pb1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnKlasifikasi = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnPilihGambar = new System.Windows.Forms.Button();
            this.txtNamaFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPilihMetode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPilihSudut = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).BeginInit();
            this.SuspendLayout();
            // 
            // pb1
            // 
            this.pb1.Location = new System.Drawing.Point(194, 12);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(383, 269);
            this.pb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb1.TabIndex = 1;
            this.pb1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nama File";
            // 
            // btnKlasifikasi
            // 
            this.btnKlasifikasi.Location = new System.Drawing.Point(15, 235);
            this.btnKlasifikasi.Name = "btnKlasifikasi";
            this.btnKlasifikasi.Size = new System.Drawing.Size(173, 46);
            this.btnKlasifikasi.TabIndex = 5;
            this.btnKlasifikasi.Text = "Klasifikasi";
            this.btnKlasifikasi.UseVisualStyleBackColor = true;
            this.btnKlasifikasi.Click += new System.EventHandler(this.btnKlasifikasi_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnPilihGambar
            // 
            this.btnPilihGambar.Location = new System.Drawing.Point(15, 17);
            this.btnPilihGambar.Name = "btnPilihGambar";
            this.btnPilihGambar.Size = new System.Drawing.Size(75, 23);
            this.btnPilihGambar.TabIndex = 8;
            this.btnPilihGambar.Text = "Pilih Gambar";
            this.btnPilihGambar.UseVisualStyleBackColor = true;
            this.btnPilihGambar.Click += new System.EventHandler(this.btnPilihGambar_Click);
            // 
            // txtNamaFile
            // 
            this.txtNamaFile.Enabled = false;
            this.txtNamaFile.Location = new System.Drawing.Point(15, 69);
            this.txtNamaFile.Name = "txtNamaFile";
            this.txtNamaFile.ReadOnly = true;
            this.txtNamaFile.Size = new System.Drawing.Size(173, 20);
            this.txtNamaFile.TabIndex = 10;
            this.txtNamaFile.Text = "Nama file";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Pilih Metode";
            // 
            // cbPilihMetode
            // 
            this.cbPilihMetode.FormattingEnabled = true;
            this.cbPilihMetode.Items.AddRange(new object[] {
            "GLCM",
            "GLRLM",
            "GLCM dan GLRLM"});
            this.cbPilihMetode.Location = new System.Drawing.Point(14, 135);
            this.cbPilihMetode.Name = "cbPilihMetode";
            this.cbPilihMetode.Size = new System.Drawing.Size(135, 21);
            this.cbPilihMetode.TabIndex = 12;
            this.cbPilihMetode.Text = "Pilih Metode";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Tentukan Sudut";
            // 
            // cbPilihSudut
            // 
            this.cbPilihSudut.FormattingEnabled = true;
            this.cbPilihSudut.Items.AddRange(new object[] {
            "0",
            "45",
            "90",
            "135"});
            this.cbPilihSudut.Location = new System.Drawing.Point(15, 187);
            this.cbPilihSudut.Name = "cbPilihSudut";
            this.cbPilihSudut.Size = new System.Drawing.Size(86, 21);
            this.cbPilihSudut.TabIndex = 14;
            this.cbPilihSudut.Tag = "";
            this.cbPilihSudut.Text = "0";
            // 
            // menu_input
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(587, 293);
            this.Controls.Add(this.cbPilihSudut);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbPilihMetode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNamaFile);
            this.Controls.Add(this.btnPilihGambar);
            this.Controls.Add(this.btnKlasifikasi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pb1);
            this.Name = "menu_input";
            this.Text = "Menu Masukan";
            this.Load += new System.EventHandler(this.menu_input_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pb1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnKlasifikasi;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnPilihGambar;
        private System.Windows.Forms.TextBox txtNamaFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPilihMetode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbPilihSudut;
    }
}