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

namespace Zenfox_Software.relatorios.contabilidade
{
    public partial class Calcular_XML : Form
    {
        public Calcular_XML()
        {
            InitializeComponent();

            Zenfox_Software_OO.Cadastros.Configuracao cmd = new Zenfox_Software_OO.Cadastros.Configuracao();
            
            String path = cmd.seleciona(new Zenfox_Software_OO.Cadastros.Entidade_Configuracao()).acbr;
            DirectoryInfo dir = new DirectoryInfo(path);
          
            foreach (DirectoryInfo file in dir.GetDirectories()){

                String diretorio = file.Name.Substring(4, 2) + "/" + file.Name.Substring(0, 4);
                cb_periodo.Items.Add(diretorio);

                // aqui no caso estou guardando o nome completo do arquivo em em controle ListBox
                // voce faz como quiser
                //                   lbxResultado.Items.Add(file.FullName);                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Double totalvenda = 0;
                Double totalimposto = 0;


                Zenfox_Software_OO.Cadastros.Configuracao cmd = new Zenfox_Software_OO.Cadastros.Configuracao();
                String path = cmd.seleciona(new Zenfox_Software_OO.Cadastros.Entidade_Configuracao()).acbr;

                DirectoryInfo Dir = new DirectoryInfo(path + "\\" + cb_periodo.SelectedItem.ToString().Split('/')[1] + cb_periodo.SelectedItem.ToString().Split('/')[0]);
                //DirectoryInfo Dir = new DirectoryInfo(@"C:\Users\Natanniel\Desktop\cancelamentos_201708");
                // Busca automaticamente todos os arquivos em todos os subdiretórios
                FileInfo[] Files = Dir.GetFiles("*", SearchOption.AllDirectories);
                DataTable dt_produtos = new DataTable();

                String[,] itens = new String[5000, 2];

                int linha = 0;

                foreach (FileInfo File in Files)
                {

                    System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
                    xml.Load(File.FullName);
                    System.Xml.XmlElement root = xml.DocumentElement;
                    System.Xml.XmlNodeList nodeList = root.GetElementsByTagName("total");

                    totalvenda += Double.Parse(nodeList[0]["vCFe"].InnerText, System.Globalization.CultureInfo.InvariantCulture);

                    if(Double.Parse(nodeList[0]["vCFe"].InnerText, System.Globalization.CultureInfo.InvariantCulture) == 0){
                        nodeList = root.GetElementsByTagName("pgto");
                        totalvenda += Double.Parse(nodeList[0]["vTroco"].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                    }

                }

                lbl_total.Text = "R$ " + totalvenda;

                //dt_produtos = new DataTable();
                //dt_produtos.Columns.Add("Código Produto", typeof(string));
                //dt_produtos.Columns.Add("Quantidade Produto", typeof(string));
                //
                //for (int x = 0; x < linha; x++)
                //{
                //    dt_produtos.Rows.Add(itens[x, 0], itens[x, 1]);
                //}
                //
                //super_grid spg = new super_grid(dt_produtos);
                //spg.ShowDialog();
            }
            catch { }
        }
    }
}
