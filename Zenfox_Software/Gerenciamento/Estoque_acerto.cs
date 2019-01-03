using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.Gerenciamento
{
    public partial class Estoque_acerto : Form
    {
        public Int32 id_produto = 0;

        public Estoque_acerto(Int32 id)
        {
            InitializeComponent();
            this.id_produto = id;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Zenfox_Software_OO.Cadastros.Produto cmd = new Zenfox_Software_OO.Cadastros.Produto();
                cmd.acerta_estoque(new Zenfox_Software_OO.Cadastros.Entidade_Produto() { id = this.id_produto, estoque = Int32.Parse(textBox1.Text) });

                MessageBox.Show("Estoque acertado com sucesso !");
                this.Close();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
