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
    public partial class Retirada_caixa : Form
    {
        public Retirada_caixa()
        {
            InitializeComponent();
            txt_descricao.Focus();
        }

        private void Retirada_caixa_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Zenfox_Software_OO.helper.Moeda(txt_valor);
        }

        public void aplica_retirada(){
            
            Double x =  Zenfox_Software_OO.helper.Moeda_to_Double(txt_valor.Text);

            if (x > 0)
            {

                if(txt_descricao.Text.Length > 3)
                {

                    Zenfox_Software_OO.Caixa.Caixa caixa = new Zenfox_Software_OO.Caixa.Caixa();
                    caixa.retirar_dinheiro_caixa(new Zenfox_Software_OO.Caixa.Entidade_Caixa() { valor_abertura = x, descricao = txt_descricao.Text});

                    MessageBox.Show("Retirada de caixa realizada com sucesso !");
                    this.Close();
                }else{
                    MessageBox.Show("Você deve informar uma descrição válida");
                }
            }else{
                MessageBox.Show("Você deve informar um valor maior que 0.");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (qtd_enter == false){

                qtd_enter = true;
                if (e.KeyCode == Keys.Enter)
                {
                    aplica_retirada();
                    qtd_enter = false;
                }
            }
            else
            {
                qtd_enter = false;
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (qtd_enter == false)
            {
                qtd_enter = true;
                if (e.KeyCode == Keys.Enter)
                {
                    aplica_retirada();
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
