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
    public partial class Pesquisa_Produto_Balanca : Form
    {
        Zenfox_Software_OO.Cadastros.Produto cmd = new Zenfox_Software_OO.Cadastros.Produto();
        public String produto = "0";
        public Double valor_produto = 0;
        public Double valor = 0;
        public Boolean finalizado = false;
        public String nome_produto = "";
        public Double quantidade = 0;
        public Zenfox_Software_OO.Caixa.Configuracao.Entidade configuracao = new Zenfox_Software_OO.Caixa.Configuracao.Entidade();


        public Pesquisa_Produto_Balanca()
        {
            InitializeComponent();
            dgDados.DataSource = cmd.seleciona_listagem_lite(new Zenfox_Software_OO.Cadastros.Entidade_Produto() { search = txtProdutos.Text, unidade_medida = 2 });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Int32 x = Int32.Parse(peso_gramas.Text);

            if (x > 0 && this.produto != "" && this.produto != "0")
            {
                this.quantidade = (Double.Parse(peso_gramas.Text)) / 1000;
                this.finalizado = true;
                this.Dispose();
                //this.produto 

            }
            else
            {
                MessageBox.Show("Você precisa informar um produto e um peso em gramas para inserir o produto !");
            }
        }

        private void txtProdutos_TextChanged(object sender, EventArgs e)
        {
            if(this.configuracao.configuracao_balanca == Zenfox_Software_OO.Caixa.enum_caixa_configuracao.manual)
                dgDados.DataSource = cmd.seleciona_listagem_lite(new Zenfox_Software_OO.Cadastros.Entidade_Produto() { search = txtProdutos.Text, unidade_medida = 2 });

            if(this.configuracao.configuracao_balanca == Zenfox_Software_OO.Caixa.enum_caixa_configuracao.ler_ean13){
                if(txtProdutos.Text.Length >= 13){
                    String codigo_produto = Int32.Parse(txtProdutos.Text.Substring(1,6)).ToString();
                    String peso = txtProdutos.Text;


                    Zenfox_Software_OO.Caixa.Configuracao cmd_caixa = new Zenfox_Software_OO.Caixa.Configuracao();
                    peso = peso.Substring(7,cmd_caixa.seleciona().numero_caracteres_peso);
                    
                    Zenfox_Software_OO.Cadastros.Entidade_Produto produto = cmd.seleciona_entidade(new Zenfox_Software_OO.Cadastros.Entidade_Produto() { codigo_balanca = codigo_produto });
                    if (produto.id > 0)
                    {
                        this.produto = produto.id.ToString();
                        this.nome_produto = produto.nome_produto;
                        this.valor_produto = produto.valor_venda;
                        lbl_valor_kg.Text = "R$ " + produto.valor_venda;
                        peso_gramas.Text = Int32.Parse(peso).ToString();
                        button1_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Produto não cadastrado !");
                    }
                }
            }
        }

        private void dgDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.produto = dgDados.CurrentRow.Cells[0].Value.ToString();
                Zenfox_Software_OO.Cadastros.Produto cmd = new Zenfox_Software_OO.Cadastros.Produto();

                Zenfox_Software_OO.Cadastros.Entidade_Produto produto = cmd.seleciona_entidade(new Zenfox_Software_OO.Cadastros.Entidade_Produto() { id = Int32.Parse(this.produto) });


                this.nome_produto = produto.nome_produto;
                this.valor_produto = produto.valor_venda;
                lbl_valor_kg.Text = "R$ " + produto.valor_venda;
                peso_gramas.Text = "";
                peso_gramas.Focus();
            }
            catch (Exception ee)
            {
                String depura = "";
            }
        }

        private void peso_gramas_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 gramas = Int32.Parse(peso_gramas.Text);
                this.valor = (gramas * this.valor_produto) / 1000;
                lbl_valor_final.Text = "R$ " + Math.Round(this.valor, 2);
            }
            catch
            {
                
            }
        }

        private void peso_gramas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }

        private void Pesquisa_Produto_Balanca_Load(object sender, EventArgs e)
        {
            Zenfox_Software_OO.Caixa.Configuracao cmd = new Zenfox_Software_OO.Caixa.Configuracao();
            this.configuracao.configuracao_balanca = (Zenfox_Software_OO.Caixa.enum_caixa_configuracao)cmd.seleciona().configuracao_balanca;

            if (this.configuracao.configuracao_balanca == Zenfox_Software_OO.Caixa.enum_caixa_configuracao.ler_ean13){
                groupBox1.Text = "Insira o código de barras gerado pela balança";
            }
        }
    }
}
