using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.Cadastros
{
    public partial class Cliente : Form
    {
        public Cliente()
        {
            InitializeComponent();

            Int32 largura = Screen.PrimaryScreen.Bounds.Width;
            Int32 altura = Screen.PrimaryScreen.Bounds.Height;

            this.Width = largura - 150;
            this.Height = altura - (altura / 3);

            seleciona();
        }

        public void seleciona()
        {
            Zenfox_Software_OO.Cadastros.Cliente cmd = new Zenfox_Software_OO.Cadastros.Cliente();
            dataGridView1.DataSource = cmd.seleciona_grid(txt_pesquisa.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cliente_Cadastro cmd = new Cliente_Cadastro(0);
            cmd.ShowDialog();
            seleciona();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int32 id = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                Cliente_Cadastro cmd = new Cliente_Cadastro(id);
                cmd.ShowDialog();
                seleciona();
            }
            catch (Exception ee)
            {

            }
        }

        private void txt_pesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            seleciona();
        }
    }
}
