using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.Gerenciamento
{
    public partial class crediario : Form
    {
        public crediario(Int32 id_usuario)
        {
            InitializeComponent();
            mes_atual();
            pesquisa();

            dataGridView1.Rows[0].Selected = false;
            this.id_usuario = id_usuario;
        }


        public Boolean normal = true;
        public Int32 id_usuario = 0;

        public void pesquisa()
        {
            normal = true;
            Int32 id_cliente = 0;
            if (txt_cliente.Text.Length > 0)
                id_cliente = Int32.Parse(txt_cliente.Text.ToString().Split('-')[0].ToString());

            Zenfox_Software_OO.Caixa.Crediario cmd = new Zenfox_Software_OO.Caixa.Crediario();
            dataGridView1.DataSource = cmd.seleciona_vendas_gerencia(new Zenfox_Software_OO.Caixa.Crediario.Entidade() { data_inicial = data_inicial.Value.ToString().Replace("00:00:00", "").Trim(), data_final = data_final.Value.ToString().Replace("00:00:00", "").Trim(), pesquisa_atrasado = false ,cliente = id_cliente, pesquisa_todas_do_cliente =false});


            dataGridView1.Rows[0].Selected = false;
        }

        public void pesquisa_atrasados()
        {
            normal = true;
            Zenfox_Software_OO.Caixa.Crediario cmd = new Zenfox_Software_OO.Caixa.Crediario();
            dataGridView1.DataSource = cmd.seleciona_vendas_gerencia(new Zenfox_Software_OO.Caixa.Crediario.Entidade() { data_inicial = data_inicial.Value.ToString().Replace("00:00:00", "").Trim(), data_final = data_final.Value.ToString().Replace("00:00:00", "").Trim(), pesquisa_atrasado = true, pesquisa_todas_do_cliente = false });

            dataGridView1.Rows[0].Selected = false;
        }

        public void pesquisa_do_cliente()
        {
            normal = false;
            Int32 id_cliente = 0;
            if (txt_cliente.Text.Length > 0)
                id_cliente = Int32.Parse(txt_cliente.Text.ToString().Split('-')[0].ToString());

            if (id_cliente > 0) {

                Zenfox_Software_OO.Caixa.Crediario cmd = new Zenfox_Software_OO.Caixa.Crediario();
                dataGridView1.DataSource = cmd.seleciona_vendas_gerencia(new Zenfox_Software_OO.Caixa.Crediario.Entidade() { data_inicial = "", data_final="", pesquisa_atrasado = false, cliente = id_cliente, pesquisa_todas_do_cliente = true});

                dataGridView1.Rows[0].Selected = false;
            }
            else
            {
                MessageBox.Show("Você precisa selecionar o cliente !");
            }

        }


        public void mes_atual()
        {
            var data = DateTime.Now; //pega a data que está no controle
            var mesAnterior = data.AddMonths(0);
            var primeiroDia = new DateTime(mesAnterior.Year, mesAnterior.Month, 1);
            var ultimoDia = new DateTime(mesAnterior.Year, mesAnterior.Month,
                    DateTime.DaysInMonth(mesAnterior.Year, mesAnterior.Month));


            data_inicial.Text = primeiroDia.ToShortDateString();
            data_final.Text = ultimoDia.ToShortDateString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pesquisa();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 id = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            caixa.Crediario cmd = new caixa.Crediario(this.id_usuario);
            cmd.setar_id_crediario(id);
            cmd.dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cmd.dataGridView2.EditMode = DataGridViewEditMode.EditProgrammatically;
            cmd.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mes_atual();
            pesquisa();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            caixa.Pesquisa_cliente cmd = new caixa.Pesquisa_cliente();
            cmd.ShowDialog();
            if (cmd.cliente.Length > 0)
                txt_cliente.Text = cmd.cliente.ToString();
        }

        private void txt_cliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            mes_anterior();
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

        public void proximo_mes()
        {
            var data = new DateTime(Int32.Parse(data_inicial.Text.Split('/')[2]), Int32.Parse(data_inicial.Text.Split('/')[1]), Int32.Parse(data_inicial.Text.Split('/')[0])); //pega a data que está no controle
            var mesAnterior = data.AddMonths(+1);
            var primeiroDia = new DateTime(mesAnterior.Year, mesAnterior.Month, 1);
            var ultimoDia = new DateTime(mesAnterior.Year, mesAnterior.Month,
                    DateTime.DaysInMonth(mesAnterior.Year, mesAnterior.Month));


            data_inicial.Text = primeiroDia.ToShortDateString();
            data_final.Text = ultimoDia.ToShortDateString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            proximo_mes();
            pesquisa();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pesquisa_atrasados();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {

                if (normal)
                {

                    DateTime data1;

                    String x = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                    if (x == "Pago")
                    {
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        x = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                        if (DateTime.TryParse(x, out data1).Equals(true))
                            if (data1.Date < DateTime.Now.Date)
                                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                            else if (data1.Date == DateTime.Now.Date)
                                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
                else
                {
                    String x = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                    if (x == "Pago")
                    {
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
            catch { }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            txt_cliente.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pesquisa_do_cliente();
        }
    }
}
