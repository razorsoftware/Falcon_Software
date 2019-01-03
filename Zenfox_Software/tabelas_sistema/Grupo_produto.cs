using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.tabelas_sistema
{
    public partial class Grupo_produto : Form
    {
        Int32 id = 0;

        public Grupo_produto()
        {
            InitializeComponent();
            atualiza_grid();
        }

        public void atualiza_grid()
        {
            Zenfox_Software_OO.tabelas_sistema.Grupo_produto cmd = new Zenfox_Software_OO.tabelas_sistema.Grupo_produto();
            dataGridView1.DataSource = cmd.seleciona_grid("");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir este grupo de produto ? Esta ação não poderá ser desfeita !", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    Int32 id = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    Zenfox_Software_OO.tabelas_sistema.Grupo_produto cmd = new Zenfox_Software_OO.tabelas_sistema.Grupo_produto();
                    cmd.delete(new Zenfox_Software_OO.tabelas_sistema.Grupo_produto.Entidade() { id = id});
                    MessageBox.Show("Grupo de Produto excluido com sucesso !");
                    atualiza_grid();
                }
                catch
                {
                    MessageBox.Show("Nenhuma linha selecionada !");

                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (descricao.Text.Length > 0)
            {
                Zenfox_Software_OO.tabelas_sistema.Grupo_produto cmd = new Zenfox_Software_OO.tabelas_sistema.Grupo_produto();
                cmd.salva(new Zenfox_Software_OO.tabelas_sistema.Grupo_produto.Entidade() { id = this.id, descricao = descricao.Text });
                MessageBox.Show("Grupo de Produto salvo com sucesso !");
                descricao.Text = "";
                atualiza_grid();
            }
            else
            {
                MessageBox.Show("Você precisa digitar uma descrição !");
            }
        }
    }
}
