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
    public partial class Crediario : Form
    {
        public Crediario(Int32 id_operador)
        {

            InitializeComponent();

            dataGridView2.Columns.Add("parcela", "Parcela");
            dataGridView2.Columns.Add("vencimento", "Data Vencimento");
            dataGridView2.Columns.Add("valor", "Valor");


            this.id_operador = id_operador;
        }

        Int32 id_operador = 0;
        Int32 id_venda = 0;
        Int32 id_crediario = 0;
        Double total_parcelas = 0;
        Zenfox_Software_OO.Cadastros.Entidade_Vendas venda;
        public Boolean finalizado = false;


        public void setar_id_venda(Int32 id_venda)
        {
            this.id_venda = id_venda;
        }


        public void setar_id_crediario(Int32 id_crediarios)
        {
            this.id_crediario = id_crediarios;

            Zenfox_Software_OO.Caixa.Crediario cmd = new Zenfox_Software_OO.Caixa.Crediario();
            Zenfox_Software_OO.Caixa.Crediario.Entidade crediario = cmd.seleciona_crediario(new Zenfox_Software_OO.Caixa.Crediario.Entidade() { id = this.id_crediario });

            txt_data.Text = crediario.data_criacao;
            txt_descricao.Text = crediario.descricao;
            Zenfox_Software_OO.Cadastros.Cliente cmd_cliente = new Zenfox_Software_OO.Cadastros.Cliente();
            txt_cliente.Text = crediario.cliente + " - " + cmd_cliente.seleciona(new Zenfox_Software_OO.Cadastros.Cliente.Entidade() { id = crediario.cliente }).nome;

            if (crediario.status == Zenfox_Software_OO.Caixa.Crediario.status.aberto)
                lbl_status.Text = "Em Aberto";

            if (crediario.status == Zenfox_Software_OO.Caixa.Crediario.status.cancelado)
                lbl_status.Text = "Cancelado";

            if (crediario.status == Zenfox_Software_OO.Caixa.Crediario.status.pago)
                lbl_status.Text = "Pago";

            List<Zenfox_Software_OO.Caixa.Crediario.Entidade> list = cmd.seleciona_crediario_detalhamento(new Zenfox_Software_OO.Caixa.Crediario.Entidade() { crediario = this.id_crediario });

            Int32 qtd_aberto = 0;

            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("id", "ID");
            dataGridView2.Columns.Add("status", "Status");
            dataGridView2.Columns.Add("parcela", "Parcela");
            dataGridView2.Columns.Add("vencimento", "Data Vencimento");
            dataGridView2.Columns.Add("valor", "Valor");
            dataGridView2.Columns.Add("valor_pago", "Valor Pago");
            dataGridView2.Columns.Add("resta_pagar", "Resta Pagar");

            Double total = 0;
            foreach (var item in list)
            {


                string[] row1 = new string[] {item.id.ToString(), item.status.ToString(), "" + item.n_parcela, "" + item.vencimento, "R$ " + item.valor.ToString("0.00").Replace(',', '.'), "R$ " + item.parcial.ToString("f2"), "R$ " +(item.valor - item.parcial).ToString("f2") };
                dataGridView2.Rows.Add(row1);
                if (item.status == Zenfox_Software_OO.Caixa.Crediario.status.aberto)
                    qtd_aberto++;

                total += item.valor;
            }

            txt_valor_venda.Text = "R$ " + total.ToString("f2");
            lbl_valor_parcelas.Text = txt_valor_venda.Text;
            txt_qtd_parcelas.Text = list.Count.ToString();

            lbl_parcelas_aberto.Text = qtd_aberto.ToString();
            txt_qtd_parcelas.Text = list.Count.ToString();
            txt_qtd_parcelas.Enabled = false;
            dataGridView2.EditMode = DataGridViewEditMode.EditProgrammatically;
            btn_salvar.Visible = false;
            lbl_valor_parcelas.Size = new Size { Width = 367, Height = 37 };
            txt_descricao.Enabled = false;

            btn_imprimir.Visible = true;
            btn_dar_baixa.Visible = true;
            btn_baixa_parcial.Visible = true;
            btn_gerar_parcelas.Visible = false;
            btn_ver_parcelas.Visible = true;
            btn_ver_produtos.Visible = true;

            button2.Visible = false;
            button3.Visible = false;
        }

        public void carrega_novo_crediario()
        {

            Zenfox_Software_OO.Cadastros.Vendas vendas = new Zenfox_Software_OO.Cadastros.Vendas();
            this.venda = vendas.seleciona(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { id = this.id_venda });

            txt_valor_venda.Text = venda.valor_total.ToString("0.00").Replace(',', '.');

        }

        public void carrega_crediario()
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cadastros.Cliente_Cadastro cmd = new Cadastros.Cliente_Cadastro(0);
            cmd.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (total_parcelas == venda.valor_total)
                {
                    if (valida_datas())
                    {
                        if (txt_cliente.Text.Length > 0)
                        {


                            if (MessageBox.Show("Deseja realmente fechar esta venda ?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                            {
                                //SALVANDO CREDIARIO
                                Zenfox_Software_OO.Caixa.Crediario.Entidade item = new Zenfox_Software_OO.Caixa.Crediario.Entidade();
                                item.venda = this.id_venda;
                                item.cliente = Int32.Parse(txt_cliente.Text.Split('-')[0].Trim());
                                item.descricao = txt_descricao.Text;
                                item.parcelas = Int32.Parse(txt_qtd_parcelas.Text);

                                Zenfox_Software_OO.Caixa.Crediario cmd = new Zenfox_Software_OO.Caixa.Crediario();
                                Int32 id_crediario = cmd.salva(item);

                                // SALVANDO DETALHAMENTO CREDIARIO
                                for (int i = 0; i < (dataGridView2.Rows.Count - 1); i++)
                                {
                                    item = new Zenfox_Software_OO.Caixa.Crediario.Entidade();

                                    item.crediario = id_crediario;
                                    item.vencimento = dataGridView2.Rows[i].Cells[1].Value.ToString();
                                    item.valor = Double.Parse(dataGridView2.Rows[i].Cells[2].Value.ToString().Replace("R$", "").Replace('.',',').Trim());
                                    item.n_parcela = Int32.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString());

                                    cmd.salva_detalhamento(item);
                                }

                                // FINALIZANDO VENDA COMO CREDIARIO

                                Zenfox_Software_OO.Cadastros.Vendas cmd_venda = new Zenfox_Software_OO.Cadastros.Vendas();
                                cmd_venda.fecha_venda(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { origem = Zenfox_Software_OO.Cadastros.Origem_venda.crediario, crediario = this.total_parcelas, xml = "", id = this.id_venda });


                                if (MessageBox.Show("Deseja imprimir o crediário deste cliente ?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    Zenfox_Software_OO.Caixa.Crediario.imprimir_crediario(id_crediario, this.id_operador);
                                }

                                this.finalizado = true;
                                this.Close();
                            }
                        }
                        else
                            MessageBox.Show("Nenhum cliente selecionado !");
                    }
                    else { }
                }
                else
                {
                    MessageBox.Show("Valor total de parcelas difere do valor total da venda !");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Algum erro aconteceu : " + ee.Message);
            }
        }


        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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
                        if (!txt_qtd_parcelas.Text.Contains(','))
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

        private void btn_gerar_parcelas_Click(object sender, EventArgs e)
        {
            try
            {

                Int32 qtd_parcelas = Int32.Parse(txt_qtd_parcelas.Text);

                if (qtd_parcelas > 0)
                {

                    Double vl_parcela = this.venda.valor_total / qtd_parcelas;
                    dataGridView2.Rows.Clear();

                    DateTime dt = DateTime.Now;
                    dt = dt.AddMonths(+1);

                    for (int i = 1; i < (qtd_parcelas + 1); i++)
                    {
                        string[] row1 = new string[] { "" + i, "" + dt.ToShortDateString(), "R$ " + vl_parcela.ToString("0.00").Replace(',', '.') };
                        dataGridView2.Rows.Add(row1);
                        dt = dt.AddMonths(+1);
                    }

                    calcula_totais();
                }
                else
                {
                    MessageBox.Show("Quantidade de parcelas não pode ser 0 !");
                }

            }
            catch
            {
                MessageBox.Show("Quantidade de parcelas inválido");
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            valida_datas();
            calcula_totais();
        }


        public Boolean valida_datas()
        {
            try
            {
                for (int i = 0; i < (dataGridView2.Rows.Count - 1); i++)
                {
                    var test = Convert.ToDateTime(dataGridView2.Rows[i].Cells[1].Value.ToString());
                }
                return true;
            }
            catch
            {
                MessageBox.Show("Alguma data não é válida");
                return false;
            }
        }

        public Boolean calcula_totais()
        {
            try
            {
                total_parcelas = 0;
                for (int i = 0; i < (dataGridView2.Rows.Count - 1); i++)
                {
                    String aux = dataGridView2.Rows[i].Cells[2].Value.ToString().Replace("R$", "").Trim().Replace(".", ",");
                    total_parcelas += Double.Parse(aux);
                }
                lbl_valor_parcelas.Text = "R$ " + total_parcelas.ToString("0.00").Replace(",", ".");
                return true;
            }
            catch
            {
                MessageBox.Show("Algum valor não é válido");
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pesquisa_cliente cmd = new Pesquisa_cliente();
            cmd.ShowDialog();
            if (cmd.cliente.Length > 0)
                txt_cliente.Text = cmd.cliente.ToString();
        }

        private void txt_cliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_fechar_crediario_Click(object sender, EventArgs e)
        {

        }

        private void btn_dar_baixa_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows[0].Cells[0].Value.ToString() != "pago")

                if (MessageBox.Show("Deseja realmente dar baixa nesta parcela ? Esta ação não poderá ser desfeita !", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    Zenfox_Software_OO.Caixa.Crediario cmd = new Zenfox_Software_OO.Caixa.Crediario();

                    cmd.dar_baixa(new Zenfox_Software_OO.Caixa.Crediario.Entidade() { crediario = this.id_crediario, n_parcela = Int32.Parse(dataGridView2.SelectedRows[0].Cells[2].Value.ToString()) });

                    MessageBox.Show("Baixa realizada com sucesso !");
                    setar_id_crediario(this.id_crediario);

                    if (MessageBox.Show("Deseja imprimir o crediário deste cliente ?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Zenfox_Software_OO.Caixa.Crediario.imprimir_crediario(id_crediario, this.id_operador);
                    }
                }
                else
                {

                }
            else
                MessageBox.Show("Esta parcela já foi paga !");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Int32 id = Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                Crediario_Parcial cmd = new Crediario_Parcial(id);
                cmd.ShowDialog();
                setar_id_crediario(this.id_crediario);
            }
            catch
            {
                MessageBox.Show("Você precisa selecionar uma duplicata !");
            }
        }

        private void dataGridView2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                if (this.id_crediario > 0)
                {

                    DateTime data1;

                    String x = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();

                    if (x == "pago")
                        dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                    else
                    {
                        if (DateTime.TryParse(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString(), out data1).Equals(true))
                            if (data1.Date < DateTime.Now.Date)
                                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                            else if (data1.Date == DateTime.Now.Date)
                                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                    }

                    //  String x = 
                    //  
                }
            }
            catch { }
        }

        private void btn_imprimir_Click(object sender, EventArgs e)
        {
            Zenfox_Software_OO.Caixa.Crediario.imprimir_crediario(id_crediario,this.id_operador);
        }
    }
}
