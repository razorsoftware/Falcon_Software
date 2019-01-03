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
    public partial class Crediario_Parcial : Form
    {
        public Int32 id = 0;
        public Crediario_Parcial(Int32 id_duplicata)
        {
            InitializeComponent();
            this.id = id_duplicata;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Zenfox_Software_OO.helper.Moeda(textBox1);
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

        Boolean qtd_enter = false;
        Boolean valendo = true;
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (qtd_enter == false && valendo)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    qtd_enter = true;

                    if (MessageBox.Show("Deseja realmente dar baixa parcial nesta parcela ? Esta ação não poderá ser desfeita !", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Double valor = 0;
                        valor = Zenfox_Software_OO.helper.Moeda_to_Double(textBox1.Text);
                        Zenfox_Software_OO.Caixa.Crediario.baixa_parcial(this.id,valor);
                        MessageBox.Show("Baixa parcial realizada com sucesso !");
                        valendo = false;
                        qtd_enter = false;
                        this.Close();
                    }
                    else
                    {
                        qtd_enter = false;
                    }
                }
            }
            
           
        }
    }
}
