using System;
using System.Collections.Generic;
using System.Linq;
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
