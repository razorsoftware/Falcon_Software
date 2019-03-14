using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software_OO
{
    public class helper
    {

        public class combobox
        {
            public String name { get; set; }
            public Int32 value { get; set; }
        }


        public static void Moeda(TextBox txt)
        {

            string m = string.Empty;
            Double v = 0;
            try
            {
                m = txt.Text.Replace(",", "").Replace(",", "");
                if (m.Equals(""))
                {
                    m = "";
                }
                m = m.PadLeft(3, '0');
                if (m.Length > 3 & m.Substring(0, 1) == "0")
                {
                    m = m.Substring(1, m.Length - 1);
                }
                v = Convert.ToDouble(m) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;

            }
            catch (Exception)
            {
                txt.Text = "0,00";
            }
        }

        public static Double Moeda_to_Double(String x)
        {
            x = x.Replace("R$","");
            Double xx = Double.Parse(x);
            return xx;
        }

        public static void executa_comando_sql(String comando)
        {
            Zenfox_Software_OO.data.bd_postgres bd = new Zenfox_Software_OO.data.bd_postgres();
            bd.localdb();
            bd.Comando = new Npgsql.NpgsqlCommand();
            bd.Comando.CommandText = comando;
            bd.AbrirConexao();
            bd.ExecutaComando_v2();
            bd.FechaConexao();
        }

        public static Int32 retorna_versao_sistema(String mac)
        {
            Int32 versao = 0;
            Zenfox_Software_OO.data.bd_postgres bd = new Zenfox_Software_OO.data.bd_postgres();
            bd.localdb();
            bd.Comando = new Npgsql.NpgsqlCommand();
            bd.Comando.CommandText = "select codigo_versao from razor_licencas ";
            //where mac_addres = '"+mac+"'";
            bd.AbrirConexao();
            IDataReader dr = bd.RetornaDados_v2();
            while (dr.Read()){
                if (!dr.IsDBNull(0))
                    versao = dr.GetInt32(0);
                else
                    versao = 0;
            }
            bd.FechaConexao();
            return versao;
        }

        public static String retorna_id_razor()
        {
            String id_razor = "";
            Zenfox_Software_OO.data.bd_postgres bd = new Zenfox_Software_OO.data.bd_postgres();
            bd.localdb();
            bd.Comando = new Npgsql.NpgsqlCommand();
            bd.Comando.CommandText = "select id_razor from empresa ";
            //where mac_addres = '"+mac+"'";
            bd.AbrirConexao();
            IDataReader dr = bd.RetornaDados_v2();
            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                    id_razor = dr.GetString(0);
                else
                    id_razor = "";
            }
            bd.FechaConexao();
            return id_razor;
        }

        public static Boolean retorna_fake()
        {
            Boolean fake = false;
            Zenfox_Software_OO.data.bd_postgres bd = new Zenfox_Software_OO.data.bd_postgres();
            bd.localdb();
            bd.Comando = new Npgsql.NpgsqlCommand();
            bd.Comando.CommandText = "select fake from empresa ";
            //where mac_addres = '"+mac+"'";
            bd.AbrirConexao();
            IDataReader dr = bd.RetornaDados_v2();
            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                    fake = dr.GetBoolean(0);
                else
                    fake = false;
            }
            bd.FechaConexao();
            return fake;
        }



        public static List<combobox> seleciona_combobox(String tabela,String propridade_id, String propriedade_display,String where) {
            List<combobox> list = new List<combobox>();


            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            sql.Comando = new Npgsql.NpgsqlCommand();

            StringBuilder sb = new StringBuilder();
            if(where.Length > 0)
                where = " where " + where;



            sql.Comando.CommandText = "select "+propridade_id+","+propriedade_display+" from "+tabela+" " + where + " order by 2 asc";
            sql.AbrirConexao();
            IDataReader dr = sql.RetornaDados_v2();

            while (dr.Read())
                list.Add(new combobox() { name = dr.GetString(1), value = dr.GetInt32(0) });
            
            sql.FechaConexao();
            
            return list;
        }

        public static Boolean envia_email(String destino,String mensagem)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = "smtp.umbler.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("sistema@razorsoft.com.br", "q2aw3@se4");

            MailMessage mail = new MailMessage(); //Instancio o Objeto do tipo MailMessage
            mail.Sender = new System.Net.Mail.MailAddress("sistema@razorsoft.com.br", "Sistema Falcon"); //Defino quem está enviado
            mail.From = new System.Net.Mail.MailAddress("sistema@razorsoft.com.br", "Sistema Falcon"); //Defino o return path
            mail.Subject = "Sistema Falcon - Envio de arquivos de fechamento"; //Assunto do e-mail
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true; //Avisamos que o e-mail é composto em HTML
            mail.Priority = System.Net.Mail.MailPriority.Low; //Definimos a prioridade do e-mail como alta

            mail.To.Add(new System.Net.Mail.MailAddress("razorsoftbr@gmail.com"));

         
            mail.Body += mensagem;

            Boolean status_envio = true;

            try
            {
                client.Send(mail);
            }
            catch
            {
                status_envio = false;
            }

            return status_envio;
        }

    }
}
