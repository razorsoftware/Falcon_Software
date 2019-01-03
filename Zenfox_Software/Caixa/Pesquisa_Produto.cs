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
    public partial class Pesquisa_Produto : Form
    {
        public Int32 id = 0;
        public String codigo = "";

        public Pesquisa_Produto()
        {
            InitializeComponent();
            txtProdutos.Focus();

            Zenfox_Software_OO.Cadastros.Produto cmd = new Zenfox_Software_OO.Cadastros.Produto();
            DataTable tb = cmd.seleciona_listagem_lite(new Zenfox_Software_OO.Cadastros.Entidade_Produto() { nome_produto = txtProdutos.Text });
            dgDados.DataSource = tb;
            //dgDados.Columns[2].Width = 350;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Pesquisa_Produto_Load(object sender, EventArgs e)
        {

        }

        private void txtProdutos_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void txtProdutos_KeyUp(object sender, KeyEventArgs e)
        {
            Zenfox_Software_OO.Cadastros.Produto cmd = new Zenfox_Software_OO.Cadastros.Produto();
            DataTable tb = cmd.seleciona_listagem_lite(new Zenfox_Software_OO.Cadastros.Entidade_Produto() { nome_produto = txtProdutos.Text });
            dgDados.DataSource = tb;
            dgDados.Columns[2].Width = 350;
        }

        private void dgDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtProdutos_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.id = Int32.Parse(dgDados.CurrentRow.Cells[0].Value.ToString());
                this.codigo = dgDados.CurrentRow.Cells[7].Value.ToString();

                if (id > 0)
                    this.Close();


            }
            catch
            {

            }
        }
    }
}
