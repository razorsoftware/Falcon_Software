using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            

            //SmtpClient client = new SmtpClient();
            //
            //
            //// Configurando as credenciais do email
            //System.Net.NetworkCredential credenciais = new System.Net.NetworkCredential();
            //credenciais.Password = "q2aw3@se4";
            //credenciais.UserName = "sistema@razorsoft.com.br";
            //credenciais.Domain = "razorsoft.com.br";
            //
            //client.Credentials = credenciais;
            //client.Host = "smtp.umbler.com";
            //client.Port = 587;
            //client.EnableSsl = true;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = true;
            //
            //MailAddress remetente = new MailAddress("sistema@razorsoft.com.br", "Sistema Falcon",System.Text.Encoding.UTF8);
            //MailAddress destinatario = new MailAddress("natanniel.alves@outlook.com");
            //
            //MailMessage mail = new MailMessage(remetente, destinatario);
            //mail.Subject = "Sistema Falcon - ";
            //mail.Body = "Teste ";
            //
            //client.Send(mail);
            //



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Inicializando());

            
            
            //Application.Run(new caixa.Fechamento_Caixa());
            //Application.Run(new Cadastros.Produto());
            //   Application.Run(new Suporte.Ticket_Detalhamento());
            //   String x = "";
            // Application.Run(new contabilidade.Enviar_XML());
        }
    }
}
