using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.Cadastros
{
    public partial class Produto_Cadastro : Form
    {
        public Int32 id { get; set; }
        public Produto_Cadastro()
        {
            InitializeComponent();
            popula_combobox_fornecedor();
            popula_combobox_grupo_produto();
            popula_combobox_unidade_medida();
        }

        public void preenche_campos()
        {
            Zenfox_Software_OO.Cadastros.Produto cmd = new Zenfox_Software_OO.Cadastros.Produto();
            Zenfox_Software_OO.Cadastros.Entidade_Produto item = cmd.seleciona_entidade(new Zenfox_Software_OO.Cadastros.Entidade_Produto() { id = this.id });

            cb_fornecedor.SelectedValue = item.fornecedor;
            cb_unidade_medida.SelectedValue = item.unidade_medida;

            cb_grupo_produto.SelectedValue = item.grupo_produto;
            txtData.Text = item.data_cadastro;
            txt_nome_produto.Text = item.nome_produto;
            txt_ean.Text = item.ean;

            txtValor_Compra.Text = item.valor_compra.ToString("0.00").Replace(',', '.');
            txtPreco_Venda.Text = item.valor_venda.ToString("0.00").Replace(',', '.');
            txtPreco_Atacado.Text = item.valor_venda_atacado.ToString("0.00").Replace(',', '.');
            txtPreco_Venda_Margem.Text = item.valor_venda_margem.ToString();
            //txtPreco_Atacado_Margem.Text = item.valor_venda_atacado_margem.ToString();

            txtNCM.Text = item.ncm;

            if (item.cfop == 5101)
                txtCFOP.SelectedIndex = txtCFOP.Items.IndexOf("5101 - Venda de produção do estabelecimento");

            if (item.cfop == 5102)
                txtCFOP.SelectedIndex = txtCFOP.Items.IndexOf("5102 - Venda de mercadoria adquirida ou recebida de terceiros");

            if (item.cfop == 5103)
                txtCFOP.SelectedIndex = txtCFOP.Items.IndexOf("5103 - Venda de produção do estabelecimento, efetuada fora do estabelecimento");

            if (item.cfop == 5405)
                txtCFOP.SelectedIndex = txtCFOP.Items.IndexOf("5405 - Venda de mercadoria adquirida ou recebida de terceiros em operação com mercadoria sujeita ao regime de substituição tributária, na condição de contribuinte substituído");

            txt_estoque.Text = item.estoque.ToString("0.00").Replace(',', '.');
            txt_estoque_maximo.Text = item.estoque_maximo.ToString();
            txt_estoque_minimo.Text = item.estoque_minimo.ToString();

        }

        public void popula_combobox_fornecedor()
        {
            Zenfox_Software_OO.sistema s = new Zenfox_Software_OO.sistema();
            s.preenche_combobox(cb_fornecedor, "fornecedor", "id", "fantasia_nome", "", "fantasia_nome");
        }

        public void popula_combobox_grupo_produto()
        {
            Zenfox_Software_OO.sistema s = new Zenfox_Software_OO.sistema();
            s.preenche_combobox(cb_grupo_produto, "grupo_produto", "id", "descricao", "status = " + Zenfox_Software_OO.tabelas_sistema.Grupo_produto.status.ativo.GetHashCode(), "descricao");
        }

        public void popula_combobox_unidade_medida()
        {
            Zenfox_Software_OO.sistema s = new Zenfox_Software_OO.sistema();
            s.preenche_combobox(cb_unidade_medida, "unidade_medida", "id", "descricao", "status = " + Zenfox_Software_OO.tabelas_sistema.Grupo_produto.status.ativo.GetHashCode(), "descricao");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Produto_Cadastro_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Zenfox_Software_OO.Cadastros.Entidade_Produto item = new Zenfox_Software_OO.Cadastros.Entidade_Produto();

             //Validando NCM
                        if (txtNCM.Text.Length >= 8)
           // if(true)
           {
                //Validando CFOP
                try
                {
                    try
                    {
                        if (Int32.Parse(txtCFOP.SelectedItem.ToString().Split('-')[0].Trim()) > 0)
                        //if (true)
                        {
                            //Validando EAN
                            if (txt_ean.Text != null && txt_ean.Text != "")
                            {
                                Zenfox_Software_OO.ACBR acbr = new Zenfox_Software_OO.ACBR();

                                // if (acbr.valida_ncm(txtNCM.Text))
                                // {


                                item.id = this.id;

                                //Dados Produto
                                try
                                {
                                    if (cb_fornecedor.SelectedValue.ToString() != null && cb_fornecedor.SelectedValue.ToString() != "")
                                        item.fornecedor = Int32.Parse(cb_fornecedor.SelectedValue.ToString());
                                    else
                                        item.fornecedor = 0;
                                }
                                catch
                                {
                                    item.fornecedor = 0;
                                }

                                // Grupo de Produto
                                try
                                {
                                    if (cb_grupo_produto.SelectedValue.ToString() != null && cb_grupo_produto.SelectedValue.ToString() != "")
                                        item.grupo_produto = Int32.Parse(cb_grupo_produto.SelectedValue.ToString());
                                    else
                                        item.grupo_produto = 0;
                                }
                                catch
                                {
                                    item.grupo_produto = 0;
                                }

                                // Unidade Medida
                                try
                                {
                                    if (cb_unidade_medida.SelectedValue.ToString() != null && cb_unidade_medida.SelectedValue.ToString() != "")
                                        item.unidade_medida = Int32.Parse(cb_unidade_medida.SelectedValue.ToString());
                                    else
                                        item.unidade_medida = 0;
                                }
                                catch
                                {
                                    item.unidade_medida = 0;
                                }


                                item.nome_produto = txt_nome_produto.Text;
                                if(item.nome_produto.Length <= 0)
                                {
                                    MessageBox.Show("Nome do produto inválido !");
                                    txt_nome_produto.Focus();
                                    return;
                                }

                                item.ean = txt_ean.Text;
                                #region Valores

                                if (txtValor_Compra.Text.Length > 0)
                                    item.valor_compra = Double.Parse(txtValor_Compra.Text);
                                else
                                    item.valor_compra = 0;


                                if (txtPreco_Venda.Text.Length > 0)
                                    item.valor_venda = Double.Parse(txtPreco_Venda.Text);
                                else
                                    item.valor_compra = 0;


                                if (txtPreco_Venda_Margem.Text.Length > 0)
                                    item.valor_venda_margem = Double.Parse(txtPreco_Venda_Margem.Text);
                                else
                                    item.valor_venda_margem = 0;

                                if (txtPreco_Atacado.Text.Length > 0)
                                    item.valor_venda_atacado = Double.Parse(txtPreco_Atacado.Text);
                                else
                                    item.valor_venda_atacado = 0;

                                // if (txtPreco_Atacado_Margem.Text.Length > 0)
                                //     item.valor_venda_atacado_margem = Double.Parse(txtPreco_Atacado_Margem.Text);
                                // else
                                //     item.valor_venda_atacado_margem = 0;
                                #endregion
                                try
                                {
                                    item.cfop = Int32.Parse(txtCFOP.SelectedItem.ToString().Split('-')[0].Trim());
                                }
                                catch
                                {
                                    item.cfop = 0;
                                }
                                item.ncm = txtNCM.Text;

                                #region Estoque

                                //if (txt_estoque_inicial.Text.Length > 0)
                                //    item.estoque_inicial = Double.Parse(txt_estoque_inicial.Text);
                                //else
                                    item.estoque_inicial = 0;

                                if (txt_estoque_minimo.Text.Length > 0)
                                    item.estoque_minimo = Double.Parse(txt_estoque_minimo.Text);
                                else
                                    item.estoque_minimo = 0;

                                if (txt_estoque_maximo.Text.Length > 0)
                                    item.estoque_maximo = Double.Parse(txt_estoque_maximo.Text);
                                else
                                    item.estoque_maximo = 0;

                                if (txt_estoque.Text.Length > 0)
                                    item.estoque = Double.Parse(txt_estoque.Text);
                                else
                                    item.estoque = 0;

                                #endregion

                                Zenfox_Software_OO.Cadastros.Produto cmd = new Zenfox_Software_OO.Cadastros.Produto();
                                try
                                {
                                    cmd.salva(item);
                                    MessageBox.Show("Cadastro realizado com sucesso !");
                                    this.Close();
                                }
                                catch (Exception ee)
                                {
                                    MessageBox.Show(ee.Message);
                                }

                                //}
                                //  else
                                //  {
                                //      MessageBox.Show("NCM Inválido !");
                                //  }
                            }
                            else
                            {
                                MessageBox.Show("Você precisa informar o Código Barras(EAN) deste produto");
                                tabControl1.SelectedTab = tabPage1;
                                txt_ean.Focus();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Você precisa informar o CFOP deste produto");
                            tabControl1.SelectedTab = tabPage2;
                            txtCFOP.Focus();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Você precisa informar o CFOP deste produto");
                        tabControl1.SelectedTab = tabPage2;
                        txtCFOP.Focus();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    // tabControl1.SelectedTab = tabPage2;
                    // txtNCM.Focus();
                }
            }
            else
            {
                MessageBox.Show("Você precisa informar o NCM válido deste produto");
                tabControl1.SelectedTab = tabPage2;
                txtNCM.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_nome_produto_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtData_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {
            Fornecedor_Cadastro f = new Fornecedor_Cadastro(0);
            f.ShowDialog();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cadastros.Fornecedor_Cadastro cmd = new Fornecedor_Cadastro(0);
            cmd.ShowDialog();
            Int32 id = cb_fornecedor.SelectedIndex;
            popula_combobox_fornecedor();
            cb_fornecedor.SelectedIndex = id;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Moeda(txtPreco_Venda);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Moeda(txtPreco_Atacado);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtValor_Compra_KeyPress(object sender, KeyPressEventArgs e)
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
                        if (!txtValor_Compra.Text.Contains(','))
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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCFOP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void txtPreco_Venda_KeyPress(object sender, KeyPressEventArgs e)
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
                        if (!txtValor_Compra.Text.Contains(','))
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

        private void txtPreco_Atacado_KeyPress(object sender, KeyPressEventArgs e)
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
                        if (!txtValor_Compra.Text.Contains(','))
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

        private void txtPreco_Venda_Margem_KeyPress(object sender, KeyPressEventArgs e)
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
                        if (!txtValor_Compra.Text.Contains(','))
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

        private void txtPreco_Atacado_Margem_KeyPress(object sender, KeyPressEventArgs e)
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
                        if (!txtValor_Compra.Text.Contains(','))
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

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txt_estoque_minimo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != (Char)13 && e.KeyChar != (Char)8))
                {
                    e.KeyChar = (Char)0;
                }
                else
                {
                    if (e.KeyChar == '.' || e.KeyChar == ',')
                    {
                        if (!txtValor_Compra.Text.Contains(','))
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

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != (Char)13 && e.KeyChar != (Char)8))
                {
                    e.KeyChar = (Char)0;
                }
                else
                {
                    if (e.KeyChar == '.' || e.KeyChar == ',')
                    {
                        if (!txtValor_Compra.Text.Contains(','))
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != (Char)13 && e.KeyChar != (Char)8))
                {
                    e.KeyChar = (Char)0;
                }
                else
                {
                    if (e.KeyChar == '.' || e.KeyChar == ',')
                    {
                        if (!txtValor_Compra.Text.Contains(','))
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

        private void txtValor_Compra_TextChanged(object sender, EventArgs e)
        {
            Moeda(txtValor_Compra);
        }

        private void button5_Click(object sender, EventArgs e)
        {

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

        private void button4_Click(object sender, EventArgs e)
        {
            Zenfox_Software.tabelas_sistema.Grupo_produto cmd = new Zenfox_Software.tabelas_sistema.Grupo_produto();
            cmd.ShowDialog();
            popula_combobox_grupo_produto();
        }

        private void txt_estoque_TextChanged(object sender, EventArgs e)
        {
            Moeda(txt_estoque);
        }
    }
}
