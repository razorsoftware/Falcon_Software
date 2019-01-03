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
    public partial class Produto : Form
    {
        Zenfox_Software_OO.Cadastros.Produto cmd_produto = new Zenfox_Software_OO.Cadastros.Produto();

        public Produto()
        {
            InitializeComponent();
            pesquisa();
        }


        public void pesquisa()
        {
            dataGridView1.DataSource = cmd_produto.seleciona_listagem_lite(new Zenfox_Software_OO.Cadastros.Entidade_Produto() { search = txt_pesquisa.Text});
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Produto_Cadastro cmd = new Produto_Cadastro();
            cmd.ShowDialog();
            dataGridView1.DataSource = cmd_produto.seleciona_listagem_lite(new Zenfox_Software_OO.Cadastros.Entidade_Produto());
        }

        private void Produto_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pesquisa();
        }

 
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Produto_Cadastro cmd = new Produto_Cadastro();
                cmd.id = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd.preenche_campos();
                cmd.ShowDialog();
                dataGridView1.DataSource = cmd_produto.seleciona_listagem(new Zenfox_Software_OO.Cadastros.Entidade_Produto());
            }
            catch
            {

            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
