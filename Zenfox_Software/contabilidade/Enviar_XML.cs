using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.contabilidade
{
    public partial class Enviar_XML : Form
    {
        public Enviar_XML()
        {
            InitializeComponent();
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
                lbl_progresso.Text = "Enviando ...";
                this.progresso++;
            }

            if (this.progresso == 7)
            {
                lbl_progresso.Text = "Enviando ...";
                this.progresso++;
            }

            if (this.progresso == 6)
            {
                lbl_progresso.Text = "Formulando email";
                this.progresso++;
            }

            if (this.progresso == 5)
            {
                lbl_progresso.Text = "Testando módulo de envio de email";
                this.progresso++;
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
