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
    public partial class menu_awal : Form
    {
        public menu_awal()
        {
            InitializeComponent();
        }

        private void start_Click(object sender, EventArgs e)
        {
            menu_input menu_input = new menu_input();
            menu_input.Show();
            this.Hide();
        }
    }
}
