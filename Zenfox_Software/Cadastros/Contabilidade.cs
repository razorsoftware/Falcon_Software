using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.Cadastros
{
    public partial class Contabilidade : Form
    {

        Int32 id = 0;

        public Contabilidade()
        {
            InitializeComponent();
        }

        private void Contabilidade_Load(object sender, EventArgs e)
        {
            Zenfox_Software_OO.Cadastros.Contabilidade cmd = new Zenfox_Software_OO.Cadastros.Contabilidade();
            Zenfox_Software_OO.Cadastros.Contabilidade.Entidade item = cmd.seleciona(new Zenfox_Software_OO.Cadastros.Contabilidade.Entidade());

            this.id = item.id;
            txt_email.Text = item.email;
            txt_nome.Text = item.nome;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Zenfox_Software_OO.Cadastros.Contabilidade.Entidade item = new Zenfox_Software_OO.Cadastros.Contabilidade.Entidade();

            if(txt_nome.Text.Length == 0)
            {
                MessageBox.Show("Você precisa informar o nome da contabilidade");
                return;
            }
            
            if(txt_email.Text.Length < 5)
            {
                MessageBox.Show("Você precisa informar um email válido");
                return;
            }

            item.id = this.id;
            item.nome = txt_nome.Text;
            item.email = txt_email.Text;

            Zenfox_Software_OO.Cadastros.Contabilidade cmd = new Zenfox_Software_OO.Cadastros.Contabilidade();
            cmd.salva(item);
            MessageBox.Show("Contabilidade salva com sucesso !");
            this.Close();
        }
    }
}
