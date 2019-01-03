using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.correcoes.produtos
{
    public partial class Produtos_sem_categoria : Form
    {
        public Produtos_sem_categoria()
        {
            InitializeComponent();
            atualiza();
        }

        public void atualiza()
        {
            Zenfox_Software_OO.Cadastros.Produto cmd = new Zenfox_Software_OO.Cadastros.Produto();
            
            dataGridView1.DataSource = cmd.seleciona_correcao(true);
            label1.Text = "Total " + (dataGridView1.Rows.Count - 1);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cadastros.Produto_Cadastro cmd = new Cadastros.Produto_Cadastro();
                cmd.id = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd.preenche_campos();
                cmd.ShowDialog();
                atualiza();
            }
            catch
            {

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
