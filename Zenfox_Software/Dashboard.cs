using ICSharpCode.SharpZipLib.Tar;
using Newtonsoft.Json;
using NUnrar.Archive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Compression;


// BUG A RESOLVER NA FUNÇÂO DO BOTÂO btn_abrir_caixa_Click

namespace Zenfox_Software
{
    public partial class Dashboard : Form
    {
        public Int32 id = 0;
        private Boolean atualizacao_disponivel = false;
        public String mac_addres = "";

        public Dashboard(Int32 id)
        {
            InitializeComponent();
            this.id = id;

            lbl_local_ip.Text = GetLocalIPAddress();
            lbl_server_ip.Text = Zenfox_Software_OO.data.bd_postgres.getip().Split(';')[0].Split('=')[1];
            // Cadastros.Configuracao cmd = new Cadastros.Configuracao();
            // cmd.ShowDialog();
            // Application.Exit();
            //btnprodutos_Click(new object(), new EventArgs());

            var left = (this.ClientRectangle.Width - pictureBox1.ClientRectangle.Width) / 2;
            var top = (this.ClientRectangle.Height - pictureBox1.ClientRectangle.Height) / 2;
            pictureBox1.Location = new Point(this.ClientRectangle.Location.X + left, this.ClientRectangle.Location.Y + top);
            background_att.RunWorkerAsync();

            String mac_addres = (from nic in NetworkInterface.GetAllNetworkInterfaces()
                                 where nic.OperationalStatus == OperationalStatus.Up
                                 select nic.GetPhysicalAddress().ToString()).FirstOrDefault();
            this.mac_addres = mac_addres;
            timer1.Enabled = true;

            btn_contabilidade.Visible = Zenfox_Software_OO.helper.retorna_fake();

        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }


        private void btnprodutos_Click(object sender, EventArgs e)
        {
            Cadastros.Produto cmd = new Cadastros.Produto();
            cmd.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            Cadastros.Fornecedor cmd = new Cadastros.Fornecedor();
            cmd.ShowDialog();
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            caixa.Caixa cmd = new caixa.Caixa(this.id);
            cmd.ShowDialog();
        }

        private void configuraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cadastros.Configuracao cmd = new Cadastros.Configuracao();
            cmd.ShowDialog();
        }

        private void btn_abrir_caixa_Click(object sender, EventArgs e)
        {
            try
            {
                caixa.Caixa cmd = new caixa.Caixa(this.id);
                cmd.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Um erro inesperado aconteceu : " + ee.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Gerenciamento.Vendas cmd = new Gerenciamento.Vendas();
            cmd.ShowDialog();
        }

        Int32 ct = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {

            //= DateTime.Now.ToShortTimeString();
            ct++;
            DateTime data = new DateTime(); //29/05/2009  
            data = DateTime.Now;
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            lbl_dia_semana.Text = dtfi.GetDayName(data.DayOfWeek);
            lbl_hora.Text = data.ToShortTimeString();
            lbl_data.Text = data.ToShortDateString();

            if (ct > 1000)
            {
                ct = 0;
                background_att.RunWorkerAsync();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cadastros.Produto cmd = new Cadastros.Produto();
            cmd.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                Suporte.Ticket_Detalhamento cmd = new Suporte.Ticket_Detalhamento();
                cmd.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Falha de comunicação com a internet !");
                // cmd.Close();
            }
        }

        private void subGrupoDeProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cadastros.Cliente cliente = new Cadastros.Cliente();
            cliente.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Gerenciamento.crediario cmd = new Gerenciamento.crediario(this.id);
            cmd.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cadastros.Fornecedor cmd = new Cadastros.Fornecedor();
            cmd.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo ainda não disponível");
            //MessageBox.Show("Módulo em desenvolvimento");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo ainda não disponível");
            //MessageBox.Show("Módulo em desenvolvimento");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Gerenciamento.Estoque cmd = new Gerenciamento.Estoque();
            cmd.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo ainda não disponível");
            //MessageBox.Show("Módulo em desenvolvimento");
        }


        private void btn_att_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Deseja realmente atualizar a versão do sistema ? Esse processo pode levar alguns minutos !", "Atualização de Sistema", MessageBoxButtons.YesNoCancel,
      MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
               

                // Verifica se pastas e arquivos existem ========================================
                if (!Directory.Exists("C:/Rede_Sistema"))
                    Directory.CreateDirectory("C:/Rede_Sistema");

                if (File.Exists("C:/Rede_sistema/razor_software.zip"))
                    File.Delete("C:/Rede_sistema/razor_software.zip");

                if (File.Exists("C:/Rede_sistema/atualizacao.msi"))
                    File.Delete("C:/Rede_sistema/atualizacao.msi");


                string arqui_bat = "C:/Rede_Sistema/start_updater.bat";
                if (!File.Exists(arqui_bat))
                {
                    File.Create(arqui_bat).Close();
                    TextWriter escrever = File.AppendText(arqui_bat);
                    //escrever.WriteLine("@echo off");
                    // escrever.WriteLine("START Instalador_Trend_SAT");

                    escrever.WriteLine("@echo off");
                    escrever.WriteLine("cd C:/Program Files (x86)/Razor/Razor_Instalador_Updater");
                    escrever.WriteLine("Start /b Updater.exe");

                    // File.Delete(arqui_bat);
                    escrever.Close();
                    //Process.Start .StartInfo.FileName = arqui_bat;
                    //Process.Start();
                }


                WebClient wc = new WebClient();
                wc.DownloadFile("http://api.razorsoft.com.br/att", "C:/rede_sistema/razor_software.zip");

                ZipFile.ExtractToDirectory("C:/rede_sistema/razor_software.zip", "C:/rede_sistema");
                
                Process proc = new Process();
                proc.StartInfo.FileName = "start_updater.bat";
                proc.StartInfo.WorkingDirectory = "C:/Rede_Sistema";
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                proc.WaitForExit();
                int ExitCode = proc.ExitCode;
                proc.Close();

                Application.Exit();




                // FAZ DOWNLOAD DA NOVA VERSÂO ==================================================



                //Zenfox_Software_OO.atualizacao cmd = new Zenfox_Software_OO.atualizacao();
                //List<Zenfox_Software_OO.atualizacao.Entidade> list = new List<Zenfox_Software_OO.atualizacao.Entidade>();//cmd.verifica_existencia_nova_atualizacao(new Zenfox_Software_OO.atualizacao.Entidade() { mac_addres = this.mac_addres });


                //File.WriteAllBytes("C:\\rede_sistema\\razor.rar", list[0].arquivo);

                //FileStream fs = new FileStream("C:\\rede_Sistema\\atualizacao.rar", FileMode.Create);
                //fs.Write(list[0].arquivo, 0, 10000000);
                //fs.Close();
                //
                //RarArchive archive = RarArchive.Open("C:\\Rede_Sistema\\atualizacao.rar");
                //
                //foreach (RarArchiveEntry item in archive.Entries)
                //{
                //    string path = Path.Combine("C:\\rede_sistema", Path.GetFileName(item.FilePath));
                //    item.WriteToFile(path);
                //}
                //

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }


        private void background_att_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                var json = new WebClient().DownloadString("http://api.razorsoft.com.br/versao");

                if (json != "1.3.2")
                    this.atualizacao_disponivel = true;
                else
                    this.atualizacao_disponivel = false;
            }
            catch { }
        }


            private void background_att_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                if (this.atualizacao_disponivel)
                {
                    btn_att.Visible = true;
                }
                else
                {
                    btn_att.Visible = false;
                }
            }

            private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Cadastros.Produto cmd = new Cadastros.Produto();
                cmd.ShowDialog();
            }

            private void fornecedorToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Cadastros.Fornecedor cmd = new Cadastros.Fornecedor();
                cmd.ShowDialog();
            }

            private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

            private void pictureBox2_Click(object sender, EventArgs e)
            {
                Zenfox_Software_OO.Impressora imp = new Zenfox_Software_OO.Impressora();
                imp.imprime_crediario();
            }

            private void grupoDeProdutoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Zenfox_Software.tabelas_sistema.Grupo_produto cmd = new tabelas_sistema.Grupo_produto();
                cmd.Show();
            }

            private void btn_contabilidade_Click(object sender, EventArgs e)
            {
                contabilidade.Enviar_XML cmd = new contabilidade.Enviar_XML();
                cmd.ShowDialog();
                btn_contabilidade.Visible = Zenfox_Software_OO.helper.retorna_fake();
            }

            private void calcularXMLToolStripMenuItem_Click(object sender, EventArgs e)
            {
                relatorios.contabilidade.Calcular_XML cmd = new relatorios.contabilidade.Calcular_XML();
                cmd.ShowDialog();
            }

            private void produtosSemCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                correcoes.produtos.Produtos_sem_categoria cmd = new correcoes.produtos.Produtos_sem_categoria();
                cmd.ShowDialog();
            }

            private void alert_message_Click(object sender, EventArgs e)
            {

            }


            private void timer_pisca_alerta_Tick(object sender, EventArgs e)
            {

            }


            private void timer3_Tick(object sender, EventArgs e)
            {
                if (alert_message.Visible)
                    alert_message.Visible = false;
                else
                    alert_message.Visible = true;
            }


            async Task<Boolean> GetMensagens_nao_lidas()
            {

                try
                {
                    Boolean x = false;
                    HttpClient client = new HttpClient();
                    var json = new WebClient().DownloadString("http://api.agenciarazor.com.br/suporte/nao_lido");

                    Person[] persons = JsonConvert.DeserializeObject<Person[]>(json);
                    if (persons.Length > 0)
                    {
                        timer3.Enabled = true;
                    }
                    else
                    {
                        timer3.Enabled = false;
                        alert_message.Visible = false;
                    }

                    return x;
                }
                catch
                {
                    return false;
                }
            }

            private void timer4_Tick(object sender, EventArgs e)
            {
                GetMensagens_nao_lidas();
            }

            private void pictureBox3_Click(object sender, EventArgs e)
            {

            }

            private void alert_message_Click_1(object sender, EventArgs e)
            {

            }
        }
        public class Person
        {
            public String cliente { get; set; }
            public String data { get; set; }
            public String resposta { get; set; }
            public String mensagem { get; set; }
        }
    }
