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
    public partial class Fechamento_Caixa : Form
    {
        public Double valor_abertura = 0;
        public Double valor_liquido = 0;
        public Double valor_total = 0;
        public Double valor_dinheiro = 0;
        public Double valor_debito = 0;
        public Double valor_credito = 0;
        public Double valor_cheque = 0;
        public Double valor_retirada_de_caixa = 0;
        public Double valor_suprimento_caixa = 0;
        public Double valor_desconto = 0;
        public Double valor_cancelado = 0;
        public Double crediario_vendido = 0;
        public Double crediario_recebido = 0;

        public Boolean fechou = false;
        Int32 id_caixa = 0;

        public Fechamento_Caixa()
        {
            InitializeComponent();
            lista_totais();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente fechar este caixa ? esta ação não poderá ser desfeita !", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                Zenfox_Software_OO.Caixa.Caixa cmd = new Zenfox_Software_OO.Caixa.Caixa();
                cmd.fechar_caixa(new Zenfox_Software_OO.Caixa.Entidade_Caixa());


                if (MessageBox.Show("Deseja imprimir o cupom de fechamento de caixa ?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes){
                    Zenfox_Software_OO.Impressora impressora = new Zenfox_Software_OO.Impressora();

                    impressora.total_abertura_caixa = this.valor_abertura;
                   
                    //impressora.operador = Zenfox_Software_OO.Cadastros.Usuario.seleciona_nome(new Zenfox_Software_OO.Cadastros.Entidade_Usuario() { id });
                    impressora.total_sangria = this.valor_retirada_de_caixa;
                    impressora.total_suprimento = this.valor_suprimento_caixa;
                    impressora.total_reais = this.valor_dinheiro;
                    impressora.total_credito = this.valor_credito;
                    impressora.total_debito = this.valor_debito;
                    impressora.total_cheque = this.valor_cheque;
                    impressora.total_geral = (this.valor_dinheiro + this.valor_credito + this.valor_debito + this.valor_cheque );

                    impressora.total_desconto = 0;
                    impressora.total_desconto = 0;
                    impressora.total_cancelado = this.valor_cancelado;

                    impressora.imprime_fechamento_caixa();
                }

                MessageBox.Show("Caixa fechado com sucesso !");
                this.fechou = true;
                this.Close();
            }
        }

        public void lista_totais()
        {
            // Coletando o ID do caixa
            Zenfox_Software_OO.Cadastros.Vendas cmd = new Zenfox_Software_OO.Cadastros.Vendas();
            this.id_caixa = Zenfox_Software_OO.Caixa.Caixa.seleciona_id_caixa();

            // Listando as vendas deste caixa=======================
            grid.DataSource = cmd.seleciona_vendas_fechamento(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { caixa = this.id_caixa, data_inicial = "", data_final = "" });

            // Coletando valor total de abertura do caixa=========
            Zenfox_Software_OO.Caixa.Caixa cmd_caixa = new Zenfox_Software_OO.Caixa.Caixa();
            Zenfox_Software_OO.Caixa.Entidade_Caixa caixa = cmd_caixa.seleciona(new Zenfox_Software_OO.Caixa.Entidade_Caixa());

            if (caixa.valor_abertura > 0)
                lbl_abertura_caixa.Text = "R$ " + caixa.valor_abertura.ToString("F2");
            this.valor_liquido = caixa.valor_abertura;
            this.valor_abertura = caixa.valor_abertura;

            // Puxando totais de dinheiro,credito,debito,cheque===========
            Zenfox_Software_OO.Cadastros.Vendas cmd_vendas = new Zenfox_Software_OO.Cadastros.Vendas();
            Zenfox_Software_OO.Cadastros.Entidade_Vendas vendas = cmd_vendas.seleciona_totais(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { caixa = this.id_caixa, data_inicial = "", data_final = "", ver_todos_status = 1 });

            if (vendas.dinheiro > 0)
                lbl_dinheiro.Text = "R$ " + vendas.dinheiro.ToString("F2");
            valor_liquido += vendas.dinheiro;
            this.valor_dinheiro = vendas.dinheiro;

            if (vendas.debito > 0)
                lbl_debito.Text = "R$ " + vendas.debito.ToString("F2");
            valor_liquido += vendas.debito;
            this.valor_debito = vendas.debito;

            if (vendas.credito > 0)
                lbl_credito.Text = "R$ " + vendas.credito.ToString("F2");
            valor_liquido += vendas.credito;
            this.valor_credito = vendas.credito;

            if (vendas.cheque > 0)
                lbl_cheque.Text = "R$ " + vendas.cheque.ToString("F2");
            valor_liquido += vendas.cheque;
            this.valor_cheque = vendas.cheque;

            if (vendas.desconto > 0)
                lbl_total_desconto.Text = "R$ " + vendas.desconto.ToString("F2");
            this.valor_desconto = vendas.desconto;

            Double total_cancelado = cmd_vendas.seleciona_totais_cancelado(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { caixa = this.id_caixa, data_inicial = "", data_final = "" }).valor_total;
            //valor_liquido += total_cancelado;

            if (total_cancelado > 0)
                lbl_total_cancelamento.Text = total_cancelado.ToString("F2");
            this.valor_cancelado = total_cancelado;

            // Puxando crediario vendido ========================
            this.crediario_vendido = cmd_vendas.seleciona_totais_crediario_vendido(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { caixa = this.id_caixa});
            lbl_crediario_vendido.Text = "R$ " + this.crediario_vendido.ToString("f2");
            // Puxando crediario Recebido =======================

            // Retirada e acrescento ao caixa ====================
            this.valor_retirada_de_caixa = Zenfox_Software_OO.Caixa.Caixa.seleciona_retirada_caixa();
            lbl_total_retirada.Text = "R$ " +this.valor_retirada_de_caixa.ToString("F2");

            this.valor_suprimento_caixa = Zenfox_Software_OO.Caixa.Caixa.seleciona_acrescento_caixa();
            lbl_total_suprimento.Text = "R$ " + this.valor_suprimento_caixa.ToString("F2");

            // Totais Gerais =====================================

            lbl_total_vendas.Text = "R$ " + (this.valor_dinheiro + this.valor_debito + this.valor_credito + this.valor_cheque + this.crediario_recebido).ToString("F2");

            if (this.valor_liquido > 0)
                lbl_total_bruto.Text = "R$ " + valor_liquido.ToString("F2");

            lbl_total_liquido.Text = "R$ " + ((this.valor_liquido + this.valor_suprimento_caixa) - (this.valor_retirada_de_caixa + this.valor_desconto)).ToString("F2");

            lbl_total_geral.Text = lbl_total_liquido.Text;


        }

        private void Fechamento_Caixa_Load(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }
    }
}
