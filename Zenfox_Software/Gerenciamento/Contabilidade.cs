using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.Gerenciamento
{
    public partial class Contabilidade : Form
    {
        public Contabilidade()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 3)
            {

            }
            else
            {
                MessageBox.Show("Você precisa informar um email válido !");
            }
        }
    }
}
