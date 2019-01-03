using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.Suporte
{
    public partial class Ticket_Detalhamento : Form
    {
        public Ticket_Detalhamento()
        {
            InitializeComponent();
            String x = GetMensagens(this.id_cliente, this.index_ultima_msg, this.ultima_resposta).Result;
            this.index_ultima_msg = int.Parse(x.Split('|')[0].ToString());
            this.ultima_resposta = x.Split('|')[1];

            this.id_cliente = Zenfox_Software_OO.helper.retorna_id_razor();
        }

        public class Person
        {
            public String cliente { get; set; }
            public String data { get; set; }
            public String resposta { get; set; }
            public String mensagem { get; set; }
        }

        public String id_cliente = "5b9c18c544deaf3cbc08ff13";
        public int index_ultima_msg = 0;
        public String ultima_resposta = "";

        async Task<String> GetMensagens(String id_cliente,Int32 index_ultima_msg,String ultima_resposta)
        {

            HttpClient client = new HttpClient();
            var json = new WebClient().DownloadString("http://api.agenciarazor.com.br/suporte/seleciona/" + id_cliente);

            //  JavaScriptSerializer js = new JavaScriptSerializer();
            //  Person[] persons = js.Deserialize<Person[]>(json)

            Person[] persons = JsonConvert.DeserializeObject<Person[]>(json);

         

            
            Int32 index = 0;
            Boolean x = false;

            Int32 atual = persons.Length;

            foreach (var item in persons)
            {
                if (index >= index_ultima_msg)
                {

                   

                    if (ultima_resposta != "" && item.resposta != ultima_resposta)
                    {
                        this.conversa.AppendText(System.Environment.NewLine.ToString());
                        this.conversa.AppendText(System.Environment.NewLine.ToString());
                    }

                    this.conversa.SelectionFont = new Font(this.conversa.Font, FontStyle.Bold);
                    if (item.resposta == "1" && ultima_resposta != "1")
                    {
                        this.conversa.AppendText("Razor disse:");
                    }
                    else
                    {
                        if (item.resposta != ultima_resposta)
                            this.conversa.AppendText("Você disse:");
                    }

                    this.conversa.SelectionFont = new Font(this.conversa.Font, FontStyle.Regular);
                    this.conversa.AppendText(System.Environment.NewLine.ToString());
                    this.conversa.AppendText(item.mensagem);

                  

                    ultima_resposta = item.resposta;
                    this.conversa.ScrollToCaret();
                    x = true;
                }
                index++;
            }

          
            
            return index+ "|" + ultima_resposta;

            //var results = JsonConvert.DeserializeObject<String>(json);
            //String x = results.Split('T')[0];
            //
            //DateTime myDate = DateTime.ParseExact(x, "yyyy-dd-MM",
            //                           System.Globalization.CultureInfo.InvariantCulture);

        }

        async Task<int> SetMensagensAsync(String id_cliente)
        {
            if (mensagem.Text.Length > 0)
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.agenciarazor.com.br/suporte/salvar");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        String json = JsonConvert.SerializeObject(new Person() { cliente = id_cliente, resposta = "0", mensagem = mensagem.Text });

                        streamWriter.Write(json);
                        // streamWriter.Flush();
                        // streamWriter.Close();
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                    mensagem.Text = "";
                    return 1;
                }
                catch (Exception err) { MessageBox.Show(err.Message); return 0; }




            }

            return 0;

        }

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            SetMensagensAsync(this.id_cliente);
            String x = GetMensagens(this.id_cliente, this.index_ultima_msg, this.ultima_resposta).Result;
            this.index_ultima_msg = int.Parse(x.Split('|')[0].ToString());
            this.ultima_resposta = x.Split('|')[1];


        }




        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                String x = GetMensagens(this.id_cliente, this.index_ultima_msg, this.ultima_resposta).Result;
                this.index_ultima_msg = int.Parse(x.Split('|')[0].ToString());
                this.ultima_resposta = x.Split('|')[1];
            }
            catch { }

        }

        private void mensagem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetMensagensAsync(this.id_cliente);
                String x = GetMensagens(this.id_cliente, this.index_ultima_msg, this.ultima_resposta).Result;
                this.index_ultima_msg = int.Parse(x.Split('|')[0].ToString());
                this.ultima_resposta = x.Split('|')[1];

            }

        }

        private void mensagem_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

     
    }
}
