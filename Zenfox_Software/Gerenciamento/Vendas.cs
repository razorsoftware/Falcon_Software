using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.Gerenciamento
{
    public partial class Vendas : Form
    {
        public Vendas()
        {
            InitializeComponent();
        }

        private void Vendas_Load(object sender, EventArgs e){
           

            mes_atual();
            pesquisa();
        }
        public void mes_anterior()
        {
            var data = new DateTime(Int32.Parse(data_inicial.Text.Split('/')[2]), Int32.Parse(data_inicial.Text.Split('/')[1]), Int32.Parse(data_inicial.Text.Split('/')[0])); //pega a data que está no controle
            var mesAnterior = data.AddMonths(-1);
            var primeiroDia = new DateTime(mesAnterior.Year, mesAnterior.Month, 1);
            var ultimoDia = new DateTime(mesAnterior.Year, mesAnterior.Month,
                    DateTime.DaysInMonth(mesAnterior.Year, mesAnterior.Month));


            data_inicial.Text = primeiroDia.ToShortDateString();
            data_final.Text = ultimoDia.ToShortDateString();
        }

        public void mes_atual() {
            var data = DateTime.Now; //pega a data que está no controle
            var mesAnterior = data.AddMonths(0);
            var primeiroDia = new DateTime(mesAnterior.Year, mesAnterior.Month, 1);
            var ultimoDia = new DateTime(mesAnterior.Year, mesAnterior.Month,
                    DateTime.DaysInMonth(mesAnterior.Year, mesAnterior.Month));


            data_inicial.Text = primeiroDia.ToShortDateString();
            data_final.Text = ultimoDia.ToShortDateString();
        }

        public void pesquisa(){

            Zenfox_Software_OO.Cadastros.Vendas cmd = new Zenfox_Software_OO.Cadastros.Vendas();
            dataGridView1.DataSource = cmd.seleciona_vendas_gerencia(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { data_inicial = data_inicial.Value.ToString().Replace("00:00:00","").Trim(), data_final = data_final.Value.ToString().Replace("00:00:00", "").Trim() });

            Zenfox_Software_OO.Cadastros.Entidade_Vendas total_cancelado = cmd.seleciona_totais_cancelado(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { data_inicial = data_inicial.Value.ToString().Replace("00:00:00", "").Trim(), data_final = data_final.Value.ToString().Replace("00:00:00", "").Trim() });
            lbl_total_cancelado.Text = "R$ " + Math.Round(total_cancelado.valor_total, 2);

            Zenfox_Software_OO.Cadastros.Entidade_Vendas total = new Zenfox_Software_OO.Cadastros.Entidade_Vendas();
            total = cmd.seleciona_totais(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { data_inicial = data_inicial.Value.ToString().Replace("00:00:00", "").Trim(), data_final = data_final.Value.ToString().Replace("00:00:00", "").Trim() });

            lbl_total_dinheiro.Text = "R$ " + Math.Round(total.dinheiro, 2);
            lbl_total_credito.Text = "R$ " + Math.Round(total.credito, 2);
            lbl_total_debito.Text = "R$ " + Math.Round(total.debito, 2);
            lbl_total_descontos.Text = "R$ " + Math.Round(total.desconto, 2);
            lbl_total_crediario.Text = "R$ " + Math.Round(total.crediario, 2);
            Double xx = Math.Round(total.desconto + total_cancelado.valor_total,2);
            lbl_total_geral.Text = "R$ " + Math.Round(total.valor_total - xx, 2);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            pesquisa();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Int32 id = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            Zenfox_Software_OO.Cadastros.Vendas cmd = new Zenfox_Software_OO.Cadastros.Vendas();
            Zenfox_Software_OO.Cadastros.Entidade_Vendas item = cmd.seleciona(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { id = id });

            String xml = "SAT.ImprimirExtratoVenda(\"" + item.xml + "\");";
            System.IO.File.WriteAllText("C:/Rede_Sistema/ENT.txt", xml.Replace("\\\"", "'"));

            Thread.Sleep(5000);

            File.Delete("C:/Rede_Sistema/sai.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mes_anterior();
            pesquisa();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mes_atual();
            pesquisa();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Zenfox_Software_OO.Cadastros.Entidade_Vendas item = new Zenfox_Software_OO.Cadastros.Entidade_Vendas();
            Boolean flag = false;

            try
            {
                Int32 x = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                if (x > 0)
                {
                    item.id = x;
                    flag = true;
                }
                else
                    MessageBox.Show("Nenhuma linha foi selecionada !");

            }
            catch (Exception ee)
            {
                MessageBox.Show("Erro ao coletar ID da venda : " + ee.Message);
            }


            if (flag) {

                item.para_cancelamento = true;
                
                Zenfox_Software_OO.Cadastros.Vendas cmd = new Zenfox_Software_OO.Cadastros.Vendas();
                DataTable venda = cmd.seleciona_vendas(item);
                
                if (venda.Rows.Count > 0){

                    item.xml = cmd.seleciona(new Zenfox_Software_OO.Cadastros.Entidade_Vendas() { id = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()) }).xml;

                    if (item.xml != "nao_fiscal") { 
                        Zenfox_Software_OO.ACBR acbr = new Zenfox_Software_OO.ACBR();
                        item = acbr.cancela_cupom_fiscal(item);
                    }

                    if(item.xml != "")
                    {
                        cmd.cancela_venda(item);
                        MessageBox.Show("Venda cancelada com sucesso !");
                        pesquisa();
                    }
                }
                else
                    MessageBox.Show("Esta venda excedeu o período de 30 minutos para cancelamento !");

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
