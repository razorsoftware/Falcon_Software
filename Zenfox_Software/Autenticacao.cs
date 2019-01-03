using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software
{
    public partial class Autenticacao : Form
    {
        public String data_hoje;
        public Boolean licenca = false;

        public Autenticacao()
        {
            InitializeComponent();


            macaddress.Text = (
      from nic in NetworkInterface.GetAllNetworkInterfaces()
      where nic.OperationalStatus == OperationalStatus.Up
      select nic.GetPhysicalAddress().ToString()
    ).FirstOrDefault();

        }

        private Boolean verifica_licenca()
        {
            String mac_addres = (
   from nic in NetworkInterface.GetAllNetworkInterfaces()
   where nic.OperationalStatus == OperationalStatus.Up
   select nic.GetPhysicalAddress().ToString()
).FirstOrDefault();

            //Verificado se computador possui licença
            Zenfox_Software_OO.Cadastros.Licenca licenca = new Zenfox_Software_OO.Cadastros.Licenca();
            List<Zenfox_Software_OO.Cadastros.Licenca.Entidade> list = licenca.seleciona(new Zenfox_Software_OO.Cadastros.Licenca.Entidade() { mac_addres = mac_addres });

            if (list.Count > 0)
            {

                DateTime dt_licenca = new DateTime(Int32.Parse(list[0].expiracao_licenca.Split('/')[2]), Int32.Parse(list[0].expiracao_licenca.Split('/')[1]), Int32.Parse(list[0].expiracao_licenca.Split('/')[0]));
                this.data_hoje = dt_licenca.ToShortDateString();
                DateTime dt_hoje = DateTime.Now;

                if (dt_licenca > dt_hoje)
                {
                    return true;
                }

                else if ((dt_licenca.AddDays(5)) >= dt_hoje)
                {
                    MessageBox.Show("Sua licença esta expirando, contate o desenvolvedor !");
                    return true;
                }
                else
                {
                    MessageBox.Show("Sua licença expirou, contate o desenvolvedor !");
                    return false;
                }

            }
            else
            {
                MessageBox.Show("Este computador não possui nenhuma licença, contate o desenvolvedor !");
                return false;
            }
        }


        private void btn_entrar_Click(object sender, EventArgs e)
        {

            if (verifica_licenca())
            {
                Zenfox_Software_OO.Cadastros.Entidade_Usuario item = new Zenfox_Software_OO.Cadastros.Entidade_Usuario();
                item.usuario = txt_usuario.Text;
                item.senha = txt_senha.Text;

                Zenfox_Software_OO.Cadastros.Usuario cmd = new Zenfox_Software_OO.Cadastros.Usuario();
                Int32 id = cmd.autenticacao(item);

                if (id > 0)
                {
                    this.Visible = false;
                    Dashboard dashboard = new Dashboard(id);
                    dashboard.Show();
                }
                else
                {
                    MessageBox.Show("Usuario ou senha invalidos !");
                    txt_senha.Text = "";
                }
            }
            else
            {
                verificando_licenca.Visible = true;
                verificar_licenca.RunWorkerAsync();
            }

        }

        private void verificar_licenca_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //Pegar o CNPJ do cliente
                Zenfox_Software_OO.Cadastros.Empresa cmd = new Zenfox_Software_OO.Cadastros.Empresa();
                Zenfox_Software_OO.Cadastros.Empresa.Entidade empresa = cmd.seleciona();


                Zenfox_Software_OO.data.bd_postgres sql_pg = new Zenfox_Software_OO.data.bd_postgres();
                sql_pg.onlinedb();
                //Trend_SAT_OO.data.bd_pg sql = new Trend_SAT_OO.data.bd_pg();
                //EmpresaBLL empresa_bll = new EmpresaBLL();

                sql_pg.AbrirConexao();

                String comando = "";

                comando += " select c.data_expiracao from cliente as a";
                comando += " inner join contrato as b on a.id = b.cliente and b.produto = 3";
                comando += " inner join licenca as c on c.contrato = b.id";
                comando += " where a.cpf_cnpj = '" + empresa.cnpj + "'";
                comando += " limit 1";

                IDataReader dr = sql_pg.RetornaDados(comando);
                String data = "";
                while (dr.Read())
                {
                    data = dr.GetDateTime(0).ToShortDateString();
                    this.licenca = true;
                }

                sql_pg.FechaConexao();


                if (this.licenca && data != this.data_hoje)
                {
                    Zenfox_Software_OO.Cadastros.Licenca cmd_licenca = new Zenfox_Software_OO.Cadastros.Licenca();
                    cmd_licenca.atualiza_licenca(data);
                }
                else
                    this.licenca = false;


            }
            catch (Exception ee)
            {
                MessageBox.Show("Falha ao verificar licença, verifique se você esta conectado a internet ! , " + ee.Message);
            }
        }

        private void verificar_licenca_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.licenca)
                MessageBox.Show("Licença atualizada com sucesso, tente fazer login novamente !");

            this.licenca = false;
            verificando_licenca.Visible = false;
        }

        private void Autenticacao_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        Boolean enter = true;

        private void txt_senha_KeyUp(object sender, KeyEventArgs e)
        {
            if (enter)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    enter = false;
                    btn_entrar_Click(new object(), new EventArgs());
                }
            }
            else
                enter = true;
        }

        private void txt_usuario_KeyUp(object sender, KeyEventArgs e)
        {
            if (enter)
            {

                if (e.KeyCode == Keys.Enter)
                {
                    enter = false;
                    btn_entrar_Click(new object(), new EventArgs());
                }
            }
            else
                enter = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }




        private void txt_usuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (enter)
            {
                if (e.KeyData == Keys.Enter)
                {
                    enter = false;
                    btn_entrar_Click(sender, e);
                }
            }
            else
                enter = false;
        }

        private void txt_senha_KeyDown(object sender, KeyEventArgs e)
        {
            if (enter)
            {
                if (e.KeyData == Keys.Enter)
                {
                    enter = false;
                    btn_entrar_Click(sender, e);
                }
            }
            else
                enter = false;
        }

        private void macaddress_Click(object sender, EventArgs e)
        {

        }

        private void Autenticacao_Load(object sender, EventArgs e)
        {

        }
    }
}
