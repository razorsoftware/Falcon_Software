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
    public partial class Fornecedor_Cadastro : Form
    {

        Int32 id_cidade = 0;

        public Fornecedor_Cadastro(Int32 id)
        {
            InitializeComponent();
            this.id = id;
            lista();


            cmb_estado.DataSource=  Zenfox_Software_OO.helper.seleciona_combobox("estado", "id", "descricao", "");
            cmb_estado.DisplayMember = "name";
            cmb_estado.ValueMember = "value";


            cmb_cidade.DataSource = Zenfox_Software_OO.helper.seleciona_combobox("cidade", "id", "nome_municipio", "fk_codigo_uf = " + cmb_estado.SelectedValue );
            cmb_cidade.DisplayMember = "name";
            cmb_cidade.ValueMember = "value";

            if(id_cidade > 0)
                cmb_cidade.SelectedValue = id_cidade;

        }

        Int32 id = 0;

        public void lista()
        {
            if (id > 0)
            {
                Zenfox_Software_OO.Cadastros.Fornecedor cmd = new Zenfox_Software_OO.Cadastros.Fornecedor();
                Zenfox_Software_OO.Cadastros.Entidade_Fornecedor item = cmd.seleciona(new Zenfox_Software_OO.Cadastros.Entidade_Fornecedor());

                txt_fantasia.Enabled = true;
                txt_fantasia.Text = item.fantasia;
                if (item.tipo_pessoa == Zenfox_Software_OO.Cadastros.tipo.pessoa_fisica){
                    cmb_tipo_pessoa.SelectedItem = "Pessoa Fisica";
                }
                else{
                    cmb_tipo_pessoa.SelectedItem = "Pessoa Jurídica";
                }

                txt_razao_social.Text = item.razao_social;
                txt_cnpj.Text = item.cpf_cnpj;
                txt_ie.Text = item.ie;
                txt_endereco.Text = item.endereco;
                txt_bairro.Text = item.bairro;
                txt_numero.Text = item.n;
                txt_complemento.Text = item.complemento;
                txt_cep.Text = item.cep;
                //cmb_estado.SelectedValue = item.estado;
                id_cidade = item.cidade;

            }
        }

        public Boolean valida_campos()
        {
            Boolean x = false;

            try
            {
                if (cmb_tipo_pessoa.SelectedItem.ToString() != "" && cmb_tipo_pessoa.SelectedItem.ToString() != null)
                    x = true;
                
                    
            }
            catch
            {
                MessageBox.Show("Você precisa selecionar o tipo de cliente !");
            }

            if (x)
            {
                
                if (txt_fantasia.Text.Length <= 3 && x)
                {
                    x = false;
                    MessageBox.Show("O Campo fantasia/nome é um campo obrigatório e deve conter mais do que 3 digitos !");
                    txt_fantasia.Focus();
                }


                if (txt_endereco.Text.Length <= 3 && x)
                {
                    x = false;
                    MessageBox.Show("Campo endereço inválido !");
                    txt_endereco.Focus();
                }

                if (txt_numero.Text.Length == 0 && x)
                {
                    x = false;
                    MessageBox.Show("Campo numero inválido !");
                    txt_numero.Focus();
                }

            
                if (txt_bairro.Text.Length <= 2 && x)
                {
                    x = false;
                    MessageBox.Show("Campo bairro inválido !");
                    txt_bairro.Focus();
                }

                if(cmb_estado.SelectedItem == null && x)
                {
                    x = false;
                    MessageBox.Show("Campo estado é obrigatório !");
                }

                if (cmb_cidade.SelectedItem == null && x)
                {
                    x = false;
                    MessageBox.Show("Campo cidade é obrigatório !");
                }


            }

            return x;
        }

        private void Fornecedor_Cadastro_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (valida_campos())
            {
                Zenfox_Software_OO.Cadastros.Entidade_Fornecedor item = new Zenfox_Software_OO.Cadastros.Entidade_Fornecedor();
                item.id = this.id;
                if (cmb_tipo_pessoa.SelectedItem == "Pessoa Fisica")
                    item.tipo_pessoa = Zenfox_Software_OO.Cadastros.tipo.pessoa_fisica;
                else
                {
                    item.tipo_pessoa = Zenfox_Software_OO.Cadastros.tipo.pessoa_juridica;
                    item.razao_social = txt_razao_social.Text;
                    item.ie = txt_ie.Text;
                }
                item.fantasia = txt_fantasia.Text;
                item.cpf_cnpj = txt_cnpj.Text;
                item.estado = (Int32)cmb_estado.SelectedValue;
                item.cidade = (Int32)cmb_cidade.SelectedValue;
                item.endereco = txt_endereco.Text;
                item.bairro = txt_bairro.Text;
                item.n = txt_numero.Text;
                item.complemento = txt_complemento.Text;
                if (item.complemento.Length == 0)
                    item.complemento = "";
                item.cep = txt_cep.Text;

                Zenfox_Software_OO.Cadastros.Fornecedor cmd = new Zenfox_Software_OO.Cadastros.Fornecedor();
                cmd.salva(item);

                MessageBox.Show("Fornecedor cadastrado com sucesso !");
                this.Close();
            }
        }

        private void txt_fantasia_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_tipo_pessoa.SelectedItem.ToString() == "Pessoa Fisica")
            {
                txt_razao_social.Enabled = false;
                txt_fantasia.Enabled = true;
                txt_cnpj.Enabled = true;
                txt_ie.Enabled = false;


            }
            else
            {
                txt_razao_social.Enabled = true;
                txt_fantasia.Enabled = true;
                txt_cnpj.Enabled = true;
                txt_ie.Enabled = true;

            }

            txt_endereco.Enabled = true;
            txt_bairro.Enabled = true;
            txt_numero.Enabled = true;
            txt_complemento.Enabled = true;
            txt_cep.Enabled = true;
            cmb_estado.Enabled = true;
            cmb_cidade.Enabled = true;
            txt_telefone.Enabled = true;
            txt_representante.Enabled = true;
            txt_email.Enabled = true;
        }

        private void txt_cep_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
