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
    public partial class Caixa_Abertura : Form
    {
        public Boolean fechou = false;
        private Int32 id_usuario = 0;

        public Caixa_Abertura(Int32 id_usuario)
        {
            InitializeComponent();
            this.id_usuario = id_usuario;
            textBox1.Focus();
        }

        private void Caixa_Abertura_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar < '0' || e.KeyChar > '9') &&
              (e.KeyChar != ',' && e.KeyChar != '.' &&
               e.KeyChar != (Char)13 && e.KeyChar != (Char)8))
                {
                    e.KeyChar = (Char)0;
                }
                else
                {
                    if (e.KeyChar == '.' || e.KeyChar == ',')
                    {
                        if (!textBox1.Text.Contains(','))
                        {
                            e.KeyChar = ',';
                        }
                        else
                        {
                            e.KeyChar = (Char)0;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Moeda(textBox1);

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter){


                Double valor = 0;

                if (textBox1.Text.Length > 0)
                    valor = double.Parse(textBox1.Text.ToString().Replace(".",""));    

                if (MessageBox.Show("Deseja realmente abrir o caixa com valor de R$ "+ valor +" ?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes){
                    Zenfox_Software_OO.Caixa.Caixa cmd = new Zenfox_Software_OO.Caixa.Caixa();
                    cmd.abrir_caixa(new Zenfox_Software_OO.Caixa.Entidade_Caixa() { usuario = this.id_usuario,valor_abertura = valor });
                    this.fechou = true;

                    if (MessageBox.Show("Deseja imprimir o cupom de abertura de caixa ?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes){
                        //Impressão 
                        Zenfox_Software_OO.Impressora impressora = new Zenfox_Software_OO.Impressora();
                        impressora.total_abertura_caixa = valor;
                        impressora.operador = Zenfox_Software_OO.Cadastros.Usuario.seleciona_nome(new Zenfox_Software_OO.Cadastros.Entidade_Usuario() { id = this.id_usuario });
                        impressora.imprime_abertura_caixa();
                    }

                    this.Close();
                }

            }
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



    }
}
