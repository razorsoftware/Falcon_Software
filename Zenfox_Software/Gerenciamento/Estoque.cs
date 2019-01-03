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
    public partial class Estoque : Form
    {
        Int32 produtos_sem_estoque, produtos_abaixo_estoque = 0;
        Zenfox_Software_OO.Cadastros.Produto cmd_produto = new Zenfox_Software_OO.Cadastros.Produto();

        public Estoque()
        {
            InitializeComponent();
            pesquisa();

            Zenfox_Software_OO.sistema s = new Zenfox_Software_OO.sistema();
            s.preenche_combobox(cb_grupo_produto, "grupo_produto", "id", "descricao", "status = " + Zenfox_Software_OO.tabelas_sistema.Grupo_produto.status.ativo.GetHashCode(), "descricao");
        }

        public void pesquisa()
        {
            Int32 grupo_produto = 0;
            try { grupo_produto = Int32.Parse(cb_grupo_produto.SelectedValue.ToString()); } catch{  }

            dataGridView1.DataSource = cmd_produto.seleciona_listagem_lite(new Zenfox_Software_OO.Cadastros.Entidade_Produto() { nome_produto = txt_pesquisa.Text, estoque_zerado = this.produtos_sem_estoque, estoque_abaixo_minimo = this.produtos_abaixo_estoque, grupo_produto = grupo_produto });
        

            Zenfox_Software_OO.Cadastros.Entidade_Produto totais = cmd_produto.seleciona_totais_estoque(new Zenfox_Software_OO.Cadastros.Entidade_Produto() { nome_produto = txt_pesquisa.Text, estoque_zerado = this.produtos_sem_estoque, estoque_abaixo_minimo = this.produtos_abaixo_estoque, grupo_produto = grupo_produto });
            lbl_quantidade.Text = totais.estoque.ToString();
            lbl_valor_estoque.Text = "R$ " + totais.valor_venda.ToString();

            this.produtos_abaixo_estoque = 0;
            this.produtos_sem_estoque = 0;
        }

        private void txt_pesquisa_TextChanged(object sender, EventArgs e)
        {
            pesquisa();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.produtos_sem_estoque = 1;
            pesquisa();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.produtos_abaixo_estoque = 1;
            pesquisa();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 id_produto = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                Estoque_acerto cmd = new Estoque_acerto(id_produto);
                cmd.ShowDialog();
                pesquisa();
            }
            catch
            {
                MessageBox.Show("Você precisa selecionar uma linha para acertar o estoque !");
            }
        }

        private void cb_grupo_produto_SelectedIndexChanged(object sender, EventArgs e)
        {
            pesquisa();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txt_pesquisa.Text = "";
            pesquisa();
        }
    }
}
