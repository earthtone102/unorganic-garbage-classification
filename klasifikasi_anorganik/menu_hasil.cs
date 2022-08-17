using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace klasifikasi_anorganik
{
    public partial class menu_hasil : Form
    {
        public menu_hasil(Bitmap rgb, Bitmap grayscale, Bitmap contrastS, double[]glcm, double[] glrlm, string material)
        {
            InitializeComponent();

            pbRgb.Image = (Bitmap)rgb;
            pbGraysScale.Image = (Bitmap)grayscale;
            pbContrastS.Image = (Bitmap)contrastS;

            lvGlcm.View = View.Details;
            lvGlcm.FullRowSelect = true;
            lvGlcm.GridLines = true;
            lvGlcm.Columns.Add("Nama Fitur", 100, HorizontalAlignment.Right);
            lvGlcm.Columns.Add("Nilai", 100, HorizontalAlignment.Right);
            lvGlcm.Items.Add("ASM");
            lvGlcm.Items[0].SubItems.Add(glcm[0].ToString());
            lvGlcm.Items.Add("Kontras");
            lvGlcm.Items[1].SubItems.Add(glcm[1].ToString());
            lvGlcm.Items.Add("IDM");
            lvGlcm.Items[2].SubItems.Add(glcm[2].ToString());
            lvGlcm.Items.Add("Korelasi");
            lvGlcm.Items[3].SubItems.Add(glcm[3].ToString());


            lvGlrlm.View = View.Details;
            lvGlrlm.FullRowSelect = true;
            lvGlrlm.GridLines = true;
            lvGlrlm.Columns.Add("Nama Fitur", 100, HorizontalAlignment.Right);
            lvGlrlm.Columns.Add("Nilai", 100, HorizontalAlignment.Right);
            lvGlrlm.Items.Add("SRE");
            lvGlrlm.Items[0].SubItems.Add(glrlm[0].ToString());
            lvGlrlm.Items.Add("LRE");
            lvGlrlm.Items[1].SubItems.Add(glrlm[1].ToString());
            lvGlrlm.Items.Add("GLU");
            lvGlrlm.Items[2].SubItems.Add(glrlm[2].ToString());
            lvGlrlm.Items.Add("RLU");
            lvGlrlm.Items[3].SubItems.Add(glrlm[3].ToString());
            lvGlrlm.Items.Add("RP");
            lvGlrlm.Items[4].SubItems.Add(glrlm[4].ToString());

            if (material == "cardboard")
                txtMaterial.Text = "Cardboard/Karton/Kardus";
            else if (material == "glass")
                txtMaterial.Text = "Glass / Kaca";
            else if (material == "plastic")
                txtMaterial.Text = "Plastik";
            else if (material == "paper")
                txtMaterial.Text = "Paper / Kertas";
            if (material == "metal")
                txtMaterial.Text = "Metal / Logam";
        }

        private void menu_hasil_Load(object sender, EventArgs e)
        {
            
        }
    }
}
