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
    public partial class Orcamento : Form
    {
        public Int32 id = 0;

        public Orcamento(Boolean converter_em_venda)
        {
            InitializeComponent();
            seleciona_orcamento();
            btn_converter_em_venda.Enabled = converter_em_venda;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }


        public void seleciona_orcamento()
        {
            Zenfox_Software_OO.Cadastros.Vendas cmd = new Zenfox_Software_OO.Cadastros.Vendas();
            dataGridView1.DataSource = cmd.seleciona_orcamento_gerencia(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { data_inicial = "", data_final= ""});
        }

        private void btn_converter_em_venda_Click(object sender, EventArgs e)
        {
            try
            {
                this.id = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            }
            catch
            {
                MessageBox.Show("Você deve selecionar o orçamento a ser convertido em venda !");
            }
        }
    }
}
