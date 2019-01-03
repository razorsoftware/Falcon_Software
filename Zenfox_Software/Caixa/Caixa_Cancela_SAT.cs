using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.caixa
{
    public partial class Caixa_Cancela_SAT : Form
    {
        public Caixa_Cancela_SAT()
        {
            InitializeComponent();

            Zenfox_Software_OO.Cadastros.Vendas cmd = new Zenfox_Software_OO.Cadastros.Vendas();
            dataGridView1.DataSource = cmd.seleciona_vendas(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { para_cancelamento = true });

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 id = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

                Zenfox_Software_OO.Cadastros.Vendas cmd = new Zenfox_Software_OO.Cadastros.Vendas();
                Zenfox_Software_OO.Cadastros.Entidade_Vendas item = cmd.seleciona(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { id = id });

                String xml = "SAT.CancelarCFe(\"" + item.xml + "\");";



                System.IO.File.WriteAllText("C:/Rede_Sistema/ENT.txt", xml.Replace("\\\"", "'"));


                #region verificando se arquivo existe
                Boolean arquivo_existe = false;

                Thread.Sleep(1000);
                if (File.Exists("C:/Rede_Sistema/sai.txt"))
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(1000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(2000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(2000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(3000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(3000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(5000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(5000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(10000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(10000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                #endregion

                string[] lines = File.ReadAllLines("C:/Rede_Sistema/sai.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("codigoDeRetorno="))
                    {
                        xml = lines[i].Substring(4);
                    }
                }

                if (xml.Contains("7000"))
                {
                    //excluirsat(ven_id);
                    xml = "SAT.ImprimirExtratoCancelamento(\"" + item.xml + "\");";
                    System.IO.File.WriteAllText(@"C:\Rede_Sistema\ENT.txt", xml);
                    //return ok;
                }
                else
                {
                    MessageBox.Show("Não foi possivel cancelas esse cupom fiscal, verifique se ele esta dentro do período de cancelamento !");
                }

                File.Delete("C:/Rede_Sistema/sai.txt");
            }
            catch (Exception ee){
                MessageBox.Show(ee.Message); 
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
