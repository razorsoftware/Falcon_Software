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
    public partial class Suprimento_caixa : Form
    {
        public Suprimento_caixa()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Suprimento_caixa_Load(object sender, EventArgs e)
        {

        }


        public void aplica_suprimento()
        {

            Double x = Zenfox_Software_OO.helper.Moeda_to_Double(txt_valor.Text);

            if (x > 0)
            {

                if (txt_descricao.Text.Length > 3)
                {

                    Zenfox_Software_OO.Caixa.Caixa caixa = new Zenfox_Software_OO.Caixa.Caixa();
                    caixa.adiciona_dinheiro_caixa(new Zenfox_Software_OO.Caixa.Entidade_Caixa() { valor_abertura = x, descricao = txt_descricao.Text });

                    MessageBox.Show("Retirada de caixa realizada com sucesso !");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Você deve informar uma descrição válida");
                }
            }
            else
            {
                MessageBox.Show("Você deve informar um valor maior que 0.");
            }
        }

        private void txt_valor_TextChanged(object sender, EventArgs e)
        {

            Zenfox_Software_OO.helper.Moeda(txt_valor);
        }

        private void txt_valor_KeyPress(object sender, KeyPressEventArgs e)
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
                        if (!txt_valor.Text.Contains(','))
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

        Boolean qtd_enter = false;

        private void txt_valor_KeyUp(object sender, KeyEventArgs e)
        {
            if (qtd_enter == false)
            {

                qtd_enter = true;
                if (e.KeyCode == Keys.Enter)
                {
                    aplica_suprimento();
                    qtd_enter = false;
                }
            }
            else
            {
                qtd_enter = false;
            }
        }

        private void txt_descricao_KeyUp(object sender, KeyEventArgs e)
        {
            if (qtd_enter == false)
            {
                qtd_enter = true;
                if (e.KeyCode == Keys.Enter)
                {
                    aplica_suprimento();
                    qtd_enter = false;

                }
            }
            else
            {
                qtd_enter = false;
            }
        }

    }
}
