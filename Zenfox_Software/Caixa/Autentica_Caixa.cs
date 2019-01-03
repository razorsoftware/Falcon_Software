using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.caixa
{
    public partial class Autentica_Caixa : Form
    {
        public Int32 id = 0;
        public Boolean autentica = false;
        public Boolean finaliza = false;

        public Autentica_Caixa()
        {
            InitializeComponent();
        }

        private void Autentica_Caixa_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Zenfox_Software_OO.Cadastros.Usuario cmd = new Zenfox_Software_OO.Cadastros.Usuario();
            Int32 id = cmd.autenticacao(new Zenfox_Software_OO.Cadastros.Entidade_Usuario() { usuario = txt_usuario.Text, senha = txt_senha.Text });

            if (id > 0)
            {
                this.id = id;
                this.autentica = true;
                this.Close();
            }
            else
                MessageBox.Show("Usuario ou senha inválidos");

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txt_senha_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txt_usuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.finaliza = true;
            this.Close();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void txt_senha_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(new object(), new EventArgs());
        }


    }
}
