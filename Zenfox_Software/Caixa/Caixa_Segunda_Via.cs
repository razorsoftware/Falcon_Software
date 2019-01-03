using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.caixa
{
    public partial class Caixa_Segunda_Via : Form
    {
        public Caixa_Segunda_Via()
        {
            InitializeComponent();
            Zenfox_Software_OO.Cadastros.Vendas cmd = new Zenfox_Software_OO.Cadastros.Vendas();
            dataGridView1.DataSource = cmd.seleciona_vendas(new Zenfox_Software_OO.Cadastros.Entidade_Vendas());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 id = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

                Zenfox_Software_OO.Cadastros.Vendas cmd = new Zenfox_Software_OO.Cadastros.Vendas();
                Zenfox_Software_OO.Cadastros.Entidade_Vendas item = cmd.seleciona(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { id = id });

                String xml = "SAT.ImprimirExtratoVenda(\"" + item.xml + "\");";
                System.IO.File.WriteAllText("C:/Rede_Sistema/ENT.txt", xml.Replace("\\\"", "'"));

                File.Delete("C:/Rede_Sistema/sai.txt");
            }
            catch (Exception ee){
                MessageBox.Show(ee.Message);
            }
        }
    }
}
