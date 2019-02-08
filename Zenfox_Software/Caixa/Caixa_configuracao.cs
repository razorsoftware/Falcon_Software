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
    public partial class Caixa_configuracao : Form
    {
        public Caixa_configuracao()
        {
            InitializeComponent();

            combo_tipo_funcionamento_impressora.Items.Add("1 - Balança Manual");
            combo_tipo_funcionamento_impressora.Items.Add("2 - Ler códigos de barras da etiqueta gerado pela balança");

            cmb_qtd_caracteres_peso.Items.Add("1");
            cmb_qtd_caracteres_peso.Items.Add("2");
            cmb_qtd_caracteres_peso.Items.Add("3");
            cmb_qtd_caracteres_peso.Items.Add("4");
            cmb_qtd_caracteres_peso.Items.Add("5");
            cmb_qtd_caracteres_peso.Items.Add("6");

        }

        private void atualiza()
        {
            Zenfox_Software_OO.Caixa.Configuracao.Entidade item = new Zenfox_Software_OO.Caixa.Configuracao.Entidade();
            Int32 aux = Int32.Parse(combo_tipo_funcionamento_impressora.SelectedItem.ToString().Split('-')[0].Trim().ToString());

            item.configuracao_balanca = (Zenfox_Software_OO.Caixa.enum_caixa_configuracao)aux;
            item.exibir_balanca_pdv = cb_exibir_balanca_pdv.Checked;

            if(cmb_qtd_caracteres_peso.SelectedItem != null)
                item.numero_caracteres_peso = Int32.Parse(cmb_qtd_caracteres_peso.SelectedItem.ToString());
            Zenfox_Software_OO.Caixa.Configuracao cmd = new Zenfox_Software_OO.Caixa.Configuracao();
            cmd.atualiza(item);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            atualiza();
        }

        private void Caixa_configuracao_Load(object sender, EventArgs e)
        {

            Zenfox_Software_OO.Caixa.Configuracao cmd = new Zenfox_Software_OO.Caixa.Configuracao();
            Zenfox_Software_OO.Caixa.Configuracao.Entidade item = cmd.seleciona();

            if (item.configuracao_balanca == Zenfox_Software_OO.Caixa.enum_caixa_configuracao.manual)
                combo_tipo_funcionamento_impressora.SelectedItem = "1 - Balança Manual";
            else
                combo_tipo_funcionamento_impressora.SelectedItem = "2 - Ler códigos de barras da etiqueta gerado pela balança";

            cmb_qtd_caracteres_peso.SelectedItem = item.numero_caracteres_peso.ToString();
            cb_exibir_balanca_pdv.Checked = item.exibir_balanca_pdv;

        }

        private void cb_exibir_balanca_pdv_CheckedChanged(object sender, EventArgs e)
        {
            atualiza();
        }

        private void cmb_qtd_caracteres_peso_SelectedIndexChanged(object sender, EventArgs e)
        {
            atualiza();
        }
    }
}
