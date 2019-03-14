using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.caixa
{
    public partial class Caixa_Fechamento : Form
    {
        Zenfox_Software_OO.Cadastros.Entidade_Vendas venda = new Zenfox_Software_OO.Cadastros.Entidade_Vendas();
        Zenfox_Software_OO.Cadastros.Vendas cmd_venda = new Zenfox_Software_OO.Cadastros.Vendas();
        public Boolean vendido = false;
        String cpf = "";
        Int32 id_cliente = 0;
        Int32 id_usuario = 0;
        Boolean orcamento = false;

        public void verifica_key_up_atalho(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F2)
                button2_Click(sender, e);

            //  if (e.KeyCode == Keys.F1)
            //     button5_Click(sender, e);
        }


        public Caixa_Fechamento(Int32 id, Int32 id_usuario, String cpf, Int32 id_cliente)
        {
            InitializeComponent();

            this.venda.id = id;
            this.id_usuario = id_usuario;

            venda = cmd_venda.seleciona(venda);

            lbl_subtotal.Text = "R$ " + venda.valor_total;
            //lbl_subtotal.Text = "R$ " + decimal.Parse(item.valor_total.ToString());

            System.IO.File.WriteAllText(@"C:\Rede_Sistema\ENT.txt", "SAT.Inicializar");

            txt_dinheiro.Select();
            lbl_nome_operador.Text = Zenfox_Software_OO.Cadastros.Usuario.seleciona_nome(new Zenfox_Software_OO.Cadastros.Entidade_Usuario() { id = this.id_usuario });
            this.cpf = cpf;
            this.id_cliente = id_cliente;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string retornoSAT(string chave)
        {
            string retorno = "";
            try
            {
                string[] lines = File.ReadAllLines("C:/Rede_Sistema/sai.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(chave + "="))
                    {
                        retorno = lines[i].Substring(chave.Length + 1);

                        if (retorno.Length <= 100 && chave == "XML" || chave == "xml")
                        {
                            retorno += lines[i + 1].ToString();
                        }

                    }
                }
            }
            catch { retorno = "Erro Monitor"; }

            return retorno;
        }


        Boolean vendendo = true;
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente fechar esta venda ?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {

                //Aqui vai ficar a validação da Venda
                if (vendendo)
                {
                    vendendo = false;

                    try
                    {

                        String x = "";

                        // Sistema esta com módulo fiscal habilitado ?
                        Zenfox_Software_OO.Cadastros.Configuracao cmd_config = new Zenfox_Software_OO.Cadastros.Configuracao();
                        Zenfox_Software_OO.Cadastros.Entidade_Configuracao item_config = cmd_config.seleciona(new Zenfox_Software_OO.Cadastros.Entidade_Configuracao());

                        if (item_config.sat && this.orcamento == false)
                        {
                            String string_sat = "";


                            double aliquota_icms = 0.02;

                            Zenfox_Software_OO.ACBR acbr = new Zenfox_Software_OO.ACBR();

                            /* 
                             *   1 - Iniciar Montagem
                             *   2 - Identificação
                             *   3 - Emitente
                             *   4 - Destinatario
                             *   5 - Adiciona Produto
                             *   6 - Total
                             *   7 - Pagamentos
                             *   8 - Dados Adicionais
                             *   9 - Gera Cupom
                            */

                            Zenfox_Software_OO.Cadastros.Empresa cmd_empresa = new Zenfox_Software_OO.Cadastros.Empresa();
                            Zenfox_Software_OO.Cadastros.Empresa.Entidade empresa = cmd_empresa.seleciona();

                            string_sat += acbr.inicia_montagem(empresa.versao_cupom);
                            string_sat += acbr.identificacao(empresa.software_house, empresa.codigo_vinculacao, "1");
                            string_sat += acbr.emitente(empresa.cnpj.Replace(".", "").Replace(" / ", "").Replace(" - ", "").ToString(), empresa.ie, "");
                            string_sat += acbr.destinatario(this.cpf, "");

                            Zenfox_Software_OO.Cadastros.Vendas cmd_vendas = new Zenfox_Software_OO.Cadastros.Vendas();
                            List<Zenfox_Software_OO.Cadastros.Entidade_Vendas> list = cmd_vendas.seleciona_detalhamento(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { id = this.venda.id });

                            Int32 produto = 1;
                            Double total_venda = 0;

                            Double aliquota_total = 0;
                            Double aliquota = 0;
                            foreach (var item in list)
                            {
                                aliquota = item.valor_total * item.quantidade;
                                aliquota = aliquota - (aliquota * (0 / 100));
                                aliquota = aliquota * (empresa.aliquota_icms / 100);
                                string_sat += acbr.adiciona_produto(produto, item.id.ToString(), "", item.ean, item.nome, item.ncm, item.cfop.ToString(), item.quantidade, item.valor_total, 0, 0, aliquota);
                                produto++;
                                aliquota_total += aliquota;
                            }

                            Double desconto = 0;
                            if (txt_desconto_percentual.Text.Length > 0)
                            {
                                desconto = venda.valor_total * (Convert.ToDouble(txt_desconto_percentual.Text) / 100);
                                desconto = Math.Floor(desconto * 100) / 100;
                            }
                            if (txt_desconto.Text.Length > 0)
                                desconto = Convert.ToDouble(txt_desconto.Text);

                            if (desconto > 0)
                                aliquota_total = aliquota_total - ((venda.valor_total - desconto) * (empresa.aliquota_icms / 100));

                            string_sat += acbr.total(aliquota_total, desconto);
                            Int32 pagamento = 1;


                            // Pagamento em dinheiro ====================================
                            if (txt_dinheiro.Text.Length > 0)
                                if (Double.Parse(txt_dinheiro.Text) > 0)
                                {
                                    string_sat += acbr.formas_pagamento(pagamento, 1, Double.Parse(txt_dinheiro.Text));
                                    pagamento++;
                                }

                            // Pagamento Cartão de Crédito =============================
                            if (txt_cartao_credito.Text.Length > 0)
                                if (Double.Parse(txt_cartao_credito.Text) > 0)
                                {
                                    string_sat += acbr.formas_pagamento(pagamento, 3, Double.Parse(txt_cartao_credito.Text));
                                    pagamento++;
                                }

                            // Pagamento Cartão de Débito =============================
                            if (txt_cartao_debito.Text.Length > 0)
                                if (Double.Parse(txt_cartao_debito.Text) > 0)
                                {
                                    string_sat += acbr.formas_pagamento(pagamento, 4, Double.Parse(txt_cartao_debito.Text));
                                    pagamento++;
                                }

                            // Pagamento Cheque =============================
                            if (txt_cheque.Text.Length > 0)
                                if (Double.Parse(txt_cheque.Text) > 0)
                                {
                                    string_sat += acbr.formas_pagamento(pagamento, 2, Double.Parse(txt_cheque.Text));
                                    pagamento++;
                                }


                            // CODIGO 5 CREDITO EM LOJA

                            string_sat += acbr.dados_adicionais("");
                            x = acbr.emite_sat(venda.id, string_sat);


                        }
                        else
                        {
                            if (this.orcamento)
                                x = "orçamento";
                            else
                                x = "nao_fiscal";
                        }

                        if (x != "")
                        {
                            Zenfox_Software_OO.Cadastros.Entidade_Vendas _venda = new Zenfox_Software_OO.Cadastros.Entidade_Vendas();

                            _venda.id = venda.id;
                            _venda.xml = x;

                            if (txt_dinheiro.Text.Length > 0)
                                _venda.dinheiro = Double.Parse(txt_dinheiro.Text);

                            if (txt_cartao_debito.Text.Length > 0)
                                _venda.debito = Double.Parse(txt_cartao_debito.Text);

                            if (txt_cartao_credito.Text.Length > 0)
                                _venda.credito = Double.Parse(txt_cartao_credito.Text);

                            if (txt_cheque.Text.Length > 0)
                                _venda.cheque = Double.Parse(txt_cheque.Text);

                            Double desconto = 0;
                            if (txt_desconto_percentual.Text.Length > 0)
                            {
                                desconto = venda.valor_total * (Convert.ToDouble(txt_desconto_percentual.Text) / 100);
                                desconto = Math.Floor(desconto * 100) / 100;
                            }
                            if (txt_desconto.Text.Length > 0)
                            {
                                desconto = Convert.ToDouble(txt_desconto.Text);
                            }
                            _venda.desconto = desconto;
                            _venda.id_cliente = this.id_cliente;
                            Zenfox_Software_OO.Cadastros.Vendas cmd = new Zenfox_Software_OO.Cadastros.Vendas();

                            // Verifica se a venda é um orçamento
                            if (!this.orcamento)
                            {
                                cmd.fecha_venda(_venda);
                                dar_baixa_produtos();
                                MessageBox.Show("Venda realizada com sucesso !");
                            }
                            else
                            {
                                if (_venda.id_cliente > 0)
                                {
                                    cmd.fecha_venda_orcamento(_venda);
                                    MessageBox.Show("Orçamento realizado com sucesso !");
                                }
                                else
                                {
                                    MessageBox.Show("Você precisa selecionar um cliente para associar ele ao orçamento !");
                                    return;
                                }
                            }
                            
                            vendido = true;
                            this.Close();
                        }

                    }
                    catch (Exception ee) { MessageBox.Show("Ocorreu um erro sem excessão, contate o desenvolvedor : " + ee.Message); }


                    vendendo = true;
                    this.orcamento = false;
                }
                else
                {

                }
            }


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Moeda(txt_dinheiro);
            calcula_totais();
        }

        public void dar_baixa_produtos()
        {
            Zenfox_Software_OO.Cadastros.Vendas cmd_vendas = new Zenfox_Software_OO.Cadastros.Vendas();
            List<Zenfox_Software_OO.Cadastros.Entidade_Vendas> list = cmd_vendas.seleciona_detalhamento(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { id = this.venda.id });

            Int32 quantidade = 0;

            foreach (var item in list)
            {
                Zenfox_Software_OO.Cadastros.Produto cmd = new Zenfox_Software_OO.Cadastros.Produto();
                Zenfox_Software_OO.Cadastros.Entidade_Produto produto = cmd.seleciona_entidade(new Zenfox_Software_OO.Cadastros.Entidade_Produto() { id = item.id_produto });

                produto.estoque = produto.estoque - item.quantidade;

                Zenfox_Software_OO.gerenciamento.Estoque.dar_baixa(new Zenfox_Software_OO.gerenciamento.Estoque.Entidade() { id_produto = item.id_produto, quantidade = Double.Parse(produto.estoque.ToString()) });
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





        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txt_dinheiro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
                e.Handled = true;

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            Moeda(txt_cartao_debito);
            calcula_totais();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
                e.Handled = true;
        }

        private void txt_cheque_TextChanged(object sender, EventArgs e)
        {
            Moeda(txt_cheque);
            calcula_totais();
        }

        private void txt_cheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
                e.Handled = true;
        }

        private void txt_cartao_credito_TextChanged(object sender, EventArgs e)
        {
            Moeda(txt_cartao_credito);
            calcula_totais();
        }

        private void txt_cartao_credito_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
                e.Handled = true;
        }

        public void calcula_totais()
        {
            Double troco = 0;
            Double desconto = 0;
            Double total = 0;

            if (txt_dinheiro.Text.Length > 0)
                total += Convert.ToDouble(txt_dinheiro.Text);
            if (txt_cartao_debito.Text.Length > 0)
                total += Convert.ToDouble(txt_cartao_debito.Text);
            if (txt_cartao_credito.Text.Length > 0)
                total += Convert.ToDouble(txt_cartao_credito.Text);
            if (txt_cheque.Text.Length > 0)
                total += Convert.ToDouble(txt_cheque.Text);

            if (txt_desconto_percentual.Text.Length > 0)
            {
                desconto = venda.valor_total * (Convert.ToDouble(txt_desconto_percentual.Text) / 100);
                desconto = Math.Floor(desconto * 100) / 100;
            }
            if (txt_desconto.Text.Length > 0)
                desconto = Convert.ToDouble(txt_desconto.Text);

            troco = total - (venda.valor_total - desconto);
            if (troco <= 0)
                troco = 0;

            lbl_desconto.Text = "R$ " + desconto;
            lbl_troco.Text = "R$ " + Convert.ToDouble(troco).ToString("f2");
            lbl_pagamento.Text = "R$ " + Convert.ToDouble(total).ToString("f2");
            lbl_total_pagar.Text = "R$ " + (venda.valor_total - desconto).ToString("f2");



        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
                e.Handled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txt_desconto.Text = "";
            txt_desconto_percentual.Text = txt_desconto_percentual.Text.Replace(",", "").Replace(".", "");
            calcula_totais();
        }

        private void txt_desconto_TextChanged(object sender, EventArgs e)
        {
            txt_desconto_percentual.Text = "";
            if (txt_desconto.Text.Length > 0)
                Moeda(txt_desconto);
            calcula_totais();
        }

        private void txt_desconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
                e.Handled = true;
        }

        private void txt_dinheiro_KeyUp(object sender, KeyEventArgs e)
        {
            verifica_key_up_atalho(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Crediario c = new Crediario(this.id_usuario);
            c.setar_id_venda(this.venda.id);
            c.carrega_novo_crediario();
            c.ShowDialog();

            if (c.finalizado)
            {
                dar_baixa_produtos();
                MessageBox.Show("Crediário realizado com sucesso !");
                vendido = true;
                this.Close();
            }
        }

        private void txt_cartao_debito_KeyUp(object sender, KeyEventArgs e)
        {
            verifica_key_up_atalho(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.orcamento = true;
            button2_Click(sender, e);

        }
    }
}
