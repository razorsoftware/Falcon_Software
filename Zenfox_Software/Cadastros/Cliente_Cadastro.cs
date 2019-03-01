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
    public partial class Cliente_Cadastro : Form
    {

        Int32 id = 0;
        Int32 id_cidade = 0;

        public Cliente_Cadastro(Int32 id)
        {
            InitializeComponent();
            
            this.id = id;           

            cmb_estado.DataSource = Zenfox_Software_OO.helper.seleciona_combobox("estado", "id", "descricao", "");
            cmb_estado.DisplayMember = "name";
            cmb_estado.ValueMember = "value";

            lista();

            cmb_cidade.DataSource = Zenfox_Software_OO.helper.seleciona_combobox("cidade", "id", "nome_municipio", "fk_codigo_uf = " + cmb_estado.SelectedValue);
            cmb_cidade.DisplayMember = "name";
            cmb_cidade.ValueMember = "value";

            if (id_cidade > 0)
                cmb_cidade.SelectedValue = id_cidade;
        }


        public void lista()
        {
            if (id > 0)
            {
                Zenfox_Software_OO.Cadastros.Cliente cmd = new Zenfox_Software_OO.Cadastros.Cliente();
                Zenfox_Software_OO.Cadastros.Cliente.Entidade item = cmd.seleciona(new Zenfox_Software_OO.Cadastros.Cliente.Entidade() { id = this.id});

                txt_nome.Text = item.nome;
                txt_rg.Text = item.rg;
                txt_cpf.Text = item.cpf;
                txt_cep.Text = item.cep;
                txt_endereco.Text = item.endereco;
                txt_numero.Text = item.numero;
                txt_complemento.Text = item.complemento;
                txt_bairro.Text = item.bairro;
                txt_telefone.Text = item.telefone;
                txt_celular.Text = item.celular;
                txt_observacao.Text = item.observacao;
                cmb_estado.SelectedValue = item.estado;
                this.id_cidade = item.cidade;

            }
        }

        public Boolean valida_campos()
        {
            Boolean x = true;


            if(x && txt_nome.Text.Length < 3){
                x = false;
                MessageBox.Show("Nome do cliente é inválido");
                txt_nome.Focus();
            }

            if (x && txt_endereco.Text.Length < 3)
            {
                x = false;
                MessageBox.Show("Endereço é inválido");
                txt_endereco.Focus();
            }

            if (x && txt_numero.Text.Length == 0)
            {
                x = false;
                MessageBox.Show("Numero é inválido");
                txt_numero.Focus();
            }

            if (x && txt_bairro.Text.Length == 0)
            {
                x = false;
                MessageBox.Show("Bairro é inválido");
                txt_numero.Focus();
            }

            if (cmb_estado.SelectedItem == null && x)
            {
                x = false;
                MessageBox.Show("Campo estado é obrigatório !");
            }

            if (cmb_cidade.SelectedItem == null && x)
            {
                x = false;
                MessageBox.Show("Campo cidade é obrigatório !");
            }

            return x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (valida_campos())
            {
                Zenfox_Software_OO.Cadastros.Cliente.Entidade item = new Zenfox_Software_OO.Cadastros.Cliente.Entidade();
                item.id = this.id;

                item.nome = txt_nome.Text;

                item.cpf = txt_cpf.Text;
                item.rg = txt_rg.Text;
                item.endereco = txt_endereco.Text;
                item.cidade = (Int32)cmb_cidade.SelectedValue;
                item.estado = (Int32)cmb_estado.SelectedValue;
                item.cep = txt_cep.Text;
                item.bairro = txt_bairro.Text;
                item.telefone = txt_telefone.Text;
                item.celular = txt_celular.Text;
                item.observacao = txt_observacao.Text;
                item.status = true;
                item.numero = txt_numero.Text;
                item.complemento = txt_complemento.Text;

                Zenfox_Software_OO.Cadastros.Cliente cmd = new Zenfox_Software_OO.Cadastros.Cliente();
                cmd.salva(item);

                MessageBox.Show("Cliente cadastrado com sucesso !");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir este cliente ?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                Zenfox_Software_OO.Cadastros.Cliente cmd = new Zenfox_Software_OO.Cadastros.Cliente();
                cmd.apaga(this.id);
                MessageBox.Show("Cliente excluido com sucesso !");
                this.Close();
            }
        }
    }
}
