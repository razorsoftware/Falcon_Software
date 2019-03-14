using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.caixa
{
    public partial class Pesquisa_cliente : Form
    {

        public String cliente = "";
        public String cpf = "";

        public Pesquisa_cliente()
        {
            InitializeComponent();
            pesquisa();
        }

        public void pesquisa()
        {
            Zenfox_Software_OO.Cadastros.Cliente cmd = new Zenfox_Software_OO.Cadastros.Cliente();
            dataGridView1.DataSource = cmd.seleciona_grid(txt_pesquisa.Text);
        }


        private void txt_pesquisa_TextChanged(object sender, EventArgs e)
        {
            pesquisa();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.cliente = dataGridView1.CurrentRow.Cells[0].Value.ToString() + " - " + dataGridView1.CurrentRow.Cells[1].Value.ToString();
            this.cpf = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            this.Close();
        }
    }
}
