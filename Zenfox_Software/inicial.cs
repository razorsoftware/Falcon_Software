using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Zenfox_Software
{
    public partial class inicial : Form
    {

        public Boolean usuario = false; // Flag que identifica que ainda não será criado o usuario
        public Int32 id = 0;

        public inicial()
        {
            InitializeComponent();

            // Zenfox_Software_OO.Cadastros.Compras cmd = new Zenfox_Software_OO.Cadastros.Compras();
            // cmd.caminho_xml = "C:/2017-09-25-11-11-26-0-chv-NFe35170904443320000177550010000032551508001940-env-lot.xml";
            // cmd.realiza_leitura_nota_compra();

            //cmd.caminho_xml = 

            //Zenfox_Software_OO cmd = new 
            // Informações importantes do cabeçalho =====

            // Chave nota
            // Fornecedor

            // Informações Produtos =====================

            // Descrição
            // Código na fábrica
            // NCM
            // Unidade de Medida
            // Quantidade
            // Valor de Compra

            //String chave_nfe = "";
            //carrega o arquivo XML


            //chave_nfe =
            //       // Numero da Nota ============
            //       try { nf.nota = nodeList[0]["nNF"].InnerText; }
            //       catch { nf.nota = nodeList[0]["nCT"].InnerText; }
            //
            //       //DATA EMISSÃO ==============
            //       try { nf.data_emissao = nodeList[0]["dEmi"].InnerText; }
            //       catch { nf.data_emissao = nodeList[0]["dhEmi"].InnerText; }
            //
            //
            //       //CNPJ DESTINATARIO ==============
            //       nodeList = root.GetElementsByTagName("dest");
            //       try { nf.cnpj_destinatario = nodeList[0]["CNPJ"].InnerText; }
            //       catch { }
            //
            //       //CNPJ EMISSOR ==============
            //       nodeList = root.GetElementsByTagName("emit");
            //       try { nf.cnpj_emitente = nodeList[0]["CNPJ"].InnerText; }
            //       catch { }
            //
            //       //NUMERO PROTOCOLO ==================
            //       nodeList = root.GetElementsByTagName("infProt");
            //       nf.protocolo = nodeList[0]["nProt"].InnerText;




            // Zenfox_Software_OO.Cadastros.Empresa cmd = new Zenfox_Software_OO.Cadastros.Empresa();
            // cmd.verifica_primeiro_acesso();
            // txt_cnpj.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usuario)
            {

                if (txt_cnpj.Text.ToString().Length > 2)
                {
                    if (txt_senha.Text.ToString().Length >= 6)
                    {

                        if (txt_senha.Text == txt_confirmacao_senha.Text)
                        {

                            Zenfox_Software_OO.Cadastros.Usuario cmd_user = new Zenfox_Software_OO.Cadastros.Usuario();

                            Zenfox_Software_OO.Cadastros.Entidade_Usuario ent_usuario = new Zenfox_Software_OO.Cadastros.Entidade_Usuario();
                            ent_usuario.nome = txt_cnpj.Text;
                            ent_usuario.usuario = txt_cnpj.Text;
                            ent_usuario.senha = txt_senha.Text;
                            ent_usuario.adm = true;

                            cmd_user.insere_usuario(ent_usuario);
                            MessageBox.Show("Usuario administrador criado com sucesso !");

                            this.Visible = false;
                            //Sincronizacao cmd_sinc = new Sincronizacao();
                            //cmd_sinc.id = this.id;
                            //cmd_sinc.ShowDialog();

                            //Dashboard dash = new Dashboard(this.id);
                            //dash.Show();

                        }
                        else
                            MessageBox.Show("Senha e confirmação de senha são diferentes !");

                    }
                    else
                        MessageBox.Show("Sua senha deve conter pelo menos 6 digitos");
                }
                else
                    MessageBox.Show("Você precisa informar um usuario válido !");

                // UsuarioDAL cmd_usuario = new UsuarioDAL();
                // cmd_usuario.InserirUsuario(txt_cnpj.Text, txt_cnpj.Text, txt_senha.Text, "Ativo", 0, 0, "trend@trend.com.br");
                // MessageBox.Show("Cadastro Criado com sucesso !");
                // this.Close();
            }
            else
            {

                Boolean existe = false;
                Zenfox_Software_OO.data.bd_postgres sql_pg = new Zenfox_Software_OO.data.bd_postgres();
                sql_pg.onlinedb();
                //Trend_SAT_OO.data.bd_pg sql = new Trend_SAT_OO.data.bd_pg();
                //EmpresaBLL empresa_bll = new EmpresaBLL();

                sql_pg.AbrirConexao();
                IDataReader dr = sql_pg.RetornaDados("select * from cliente where cpf_cnpj = '" + txt_cnpj.Text.Replace(',', '.') + "'");

                id = dr.GetOrdinal("id");

                while (dr.Read())
                {
                    existe = true;
                    id = dr.GetInt32(id);
                }

                sql_pg.FechaConexao();


                if (existe)
                {
                    Zenfox_Software_OO.Cadastros.Empresa cmd_empresa = new Zenfox_Software_OO.Cadastros.Empresa();
                    cmd_empresa.vincula_empresa(id, txt_cnpj.Text.Replace(',', '.'));

                    label_titulo.Text = "Primeiro Usuario";
                    label_descricao.Text = "Informe o usuario e senha para o usuario principal do sistema";
                    //
                    label_campo1.Text = "Nome Usuario";
                    label_campo2.Text = "Senha";
                    //
                    txt_cnpj.Text = "";
                    txt_cnpj.Mask = "";
                    txt_senha.Text = "";
                    //
                    txt_senha2.Visible = true;
                    txt_senha.Visible = true;
                    txt_confirmacao_senha.Visible = true;

                    usuario = true;
                }
                else
                {
                    MessageBox.Show("CPF/CNPJ Invalido ou incorreto !");
                }

                //trend_cliente.ClienteClient cmd = new trend_cliente.ClienteClient();
                //Int32 x = cmd.seleciona_cliente(txt_cnpj.Text,txt_senha.Text);
                //
                //if(x > 0){
                //    MessageBox.Show("Vinculado");
                //}else{
                //    MessageBox.Show("Não vinculado");
                //} 
            }
        }

        private void inicial_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Zenfox_Software_OO.Cadastros.Empresa cmd_empresa = new Zenfox_Software_OO.Cadastros.Empresa();
            cmd_empresa.vincula_empresa(id,"111");

            label_titulo.Text = "Primeiro Usuario";
            label_descricao.Text = "Informe o usuario e senha para o usuario principal do sistema";
            //
            label_campo1.Text = "Nome Usuario";
            label_campo2.Text = "Senha";
            //
            txt_cnpj.Text = "";
            txt_cnpj.Mask = "";
            txt_senha.Text = "";
            //
            txt_senha2.Visible = true;
            txt_senha.Visible = true;
            txt_confirmacao_senha.Visible = true;

            usuario = true;

        }
    }
}
