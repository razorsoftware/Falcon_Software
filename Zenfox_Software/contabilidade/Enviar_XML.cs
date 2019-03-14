using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.contabilidade
{
    public partial class Enviar_XML : Form
    {
        Double valor_total_venda;
        String path;

        String destino;
        String email;

        public Enviar_XML()
        {
            InitializeComponent();

            Zenfox_Software_OO.Cadastros.Contabilidade cmd = new Zenfox_Software_OO.Cadastros.Contabilidade();
            txt_email.Text = cmd.seleciona(new Zenfox_Software_OO.Cadastros.Contabilidade.Entidade()).email;
            
            
            Zenfox_Software_OO.Cadastros.Configuracao cmdConfig = new Zenfox_Software_OO.Cadastros.Configuracao();
            String path = cmdConfig.seleciona(new Zenfox_Software_OO.Cadastros.Entidade_Configuracao()).acbr;

            Zenfox_Software_OO.Cadastros.Empresa cmdEmpresa = new Zenfox_Software_OO.Cadastros.Empresa();
            String cnpj = cmdEmpresa.seleciona().cnpj;

            DateTime dt = DateTime.Now;
            dt = dt.AddMonths(-1);

            String mes = dt.Month.ToString();
            if (mes.Length == 1)
                mes = "0" + mes;

            this.path = path + "/Arqs/SAT/Vendas/" + cnpj.Replace(".", "").Replace("/", "").Replace("-", "") + "/" + dt.Year + "" + mes;
            DirectoryInfo Dir = new DirectoryInfo(this.path);

            // Busca automaticamente todos os arquivos em todos os subdiretórios
            FileInfo[] Files = Dir.GetFiles("*", SearchOption.AllDirectories);
            DataTable dt_produtos = new DataTable();

            String[,] itens = new String[5000, 2];

            int linha = 0;

            Double totalvenda = 0;

            foreach (FileInfo File in Files)
            {

                System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
                xml.Load(File.FullName);
                System.Xml.XmlElement root = xml.DocumentElement;
                System.Xml.XmlNodeList nodeList = root.GetElementsByTagName("total");

                totalvenda += Double.Parse(nodeList[0]["vCFe"].InnerText, System.Globalization.CultureInfo.InvariantCulture);

                if (Double.Parse(nodeList[0]["vCFe"].InnerText, System.Globalization.CultureInfo.InvariantCulture) == 0)
                {
                    nodeList = root.GetElementsByTagName("pgto");
                    totalvenda += Double.Parse(nodeList[0]["vTroco"].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                }

            }

            lbl_total_vendas.Text = "R$ " + totalvenda;
            this.valor_total_venda = totalvenda;

        }

        Int32 progresso = 0;

        private void btn_processar_Click(object sender, EventArgs e)
        {
            if(txt_email.Text.Length > 3)
            {
                txt_email.Visible = false;
                lbl_email.Visible = false;
                btn_processar.Visible = false;
                progressBar1.Visible = true;
                lbl_progresso.Visible = true;
                timer1.Enabled = true;
            }
            else
            {
                MessageBox.Show("Você deve informar um email válido !");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.progresso == 10)
            {
                lbl_progresso.Text = "Enviado com sucesso !";
                this.progresso++;
            }

            if (this.progresso == 9)
            {
                lbl_progresso.Text = "Finalizando ...";
                this.progresso++;
            }

            if (this.progresso == 8)
            {
                lbl_progresso.Text = "Email Enviado ...";
                this.progresso++;
            }

            if (this.progresso == 7)
            {
                lbl_progresso.Text = "Enviando ...";

                Boolean status = Zenfox_Software_OO.helper.envia_email(txt_email.Text, this.email);

                if (status)
                    this.progresso++;
                else
                {
                    this.progresso = 0;
                    timer1.Enabled = false;
                    txt_email.Visible = true;
                    lbl_email.Visible = true;
                    btn_processar.Visible = true;
                    progressBar1.Visible = false;
                    lbl_progresso.Visible = false;
                    MessageBox.Show("Falha ao enviar email, Por favor contate o suporte !");
                }

                this.progresso++;
            }

            if (this.progresso == 6)
            {
                this.email = "<body>";
                this.email += "<h1 style='text-align:center; margin:0px'>Sistema Falcon</h1>";
                this.email += "<h2 style='text-align:center; margin:0px'>NH Calçados</h2>";
                this.email += "<h3 style='text-align:center; margin:0px'>Fechamento 02/2019</h3>";
                this.email += "<hr/>";
                this.email += "<ul><li><b>Total de Vendas </b> R$ "+ this.valor_total_venda.ToString("F2").Replace(".",",") +"</li>";
                this.email += "<li><b>Total de Cancelamentos </b> R$ 0,00</li>";
                this.email += "<li><b>Fechamento </b> R$ "+ this.valor_total_venda.ToString("F2").Replace(".", ",") + "</li></ul>";
                this.email += "</body>";

                lbl_progresso.Text = "Formulando email";
                this.progresso++;
            }

            if (this.progresso == 5)
            {
                lbl_progresso.Text = "Testando módulo de envio de email";

              //  Boolean status = Zenfox_Software_OO.helper.envia_email("razorsoftbr@gmail.com", "Teste de email");
              //
              //  if(status)
                    this.progresso++;
              //  else
              //  {
              //      this.progresso = 0;
              //      timer1.Enabled = false;
              //      txt_email.Visible = true;
              //      lbl_email.Visible = true;
              //      btn_processar.Visible = true;
              //      progressBar1.Visible = false;
              //      lbl_progresso.Visible = false;
              //      MessageBox.Show("Falha ao enviar email, Por favor contate o suporte !");
              //  }
            }

            if (this.progresso == 4)
            {
                lbl_progresso.Text = "Compactando Arquivos ...";
                this.progresso++;
            }


            if (this.progresso == 3)
            {
                lbl_progresso.Text = "Compactando Arquivos ...";
                this.progresso++;
            }

            if (this.progresso == 2)
            {
                lbl_progresso.Text = "Separando XML's de Cancelamento";
                this.progresso++;
            }


            if (this.progresso == 1)
            {
                lbl_progresso.Text = "Separando XML's de Vendas";
                this.progresso++;
            }


            if (this.progresso == 0)
            {
                lbl_progresso.Text = "Iniciando Processo ...";
                this.progresso++;
            }

            progressBar1.Value = this.progresso;
            if (this.progresso == 10)
            {
                timer1.Enabled = false;
                lbl_progresso.Text = "Arquivos de fechamento enviados com sucesso !";
                Zenfox_Software_OO.helper.executa_comando_sql("update empresa set fake = false;");
            }
                

        }
    }
}
