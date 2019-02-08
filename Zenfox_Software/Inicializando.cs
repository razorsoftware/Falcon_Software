using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Zenfox_Software
{
    public partial class Inicializando : Form
    {

        Int32 codigo_atualizacao = 0;
        String versao = "1.0 BETA";
        String mac = "";
        Boolean inicializado = false;


        // static async void GetLicenca(string path)
        // {
        //
        //     HttpClient client = new HttpClient();
        //     var json = new WebClient().DownloadString(path);
        //     var results = JsonConvert.DeserializeObject<String>(json);
        //     String x = results.Split('T')[0];
        //
        //     DateTime myDate = DateTime.ParseExact(x, "yyyy-dd-MM",
        //                                System.Globalization.CultureInfo.InvariantCulture);
        //     
        // }

        public Inicializando()
        {
            InitializeComponent();

            //  GetLicenca("http://localhost:5000/clientes/seleciona_licenca/5bd086313fa56e692003cd74");

            var macAddr = (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();

            //            Zenfox_Software_OO.Impressora cmd = new Zenfox_Software_OO.Impressora();
            //            cmd.imprime_crediario();

            this.mac = macAddr;

            try
            {
                codigo_atualizacao = Zenfox_Software_OO.helper.retorna_versao_sistema(macAddr);
                verifica_atualizacao_sql();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Ocorreu um erro durante a inicialização do sistema : " + ee.Message);
                verifica_atualizacao_sql();
            }

            this.inicializado = true;

        }

        public void verifica_atualizacao_sql()
        {

            // VERSÂO 1.0.0 
            if (codigo_atualizacao < 1)
            {
                // Criando tabelas iniciais do sistema
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("CREATE TABLE public.atualizacao(id SERIAL PRIMARY KEY,  versao character varying(20),  arquivo bytea);");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
                sb.Clear();

                //Criando Caixa venda
                sb.AppendLine("CREATE TABLE public.caixa" +
                "(" +
                "id SERIAL PRIMARY KEY, " +
                "usuario integer," +
                "data_abertura timestamp with time zone DEFAULT now()," +
                "data_fechamento timestamp with time zone," +
                "valor_abertura double precision" +
                "); ");

                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
                sb.Clear();

                //Criando Caixa venda
                sb.AppendLine(" CREATE TABLE public.caixa_venda " +
                "( " +
                 " id SERIAL PRIMARY KEY, " +
                 " data_venda timestamp with time zone DEFAULT now(), " +
                 " usuario integer," +
                 " caixa integer" +
                "); ");

                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
                sb.Clear();
                // Criando Cidade
                sb.AppendLine("CREATE TABLE public.cidade( id SERIAL PRIMARY KEY, " +
                "codigo_municipio integer, fk_codigo_uf integer, nome_municipio character varying(60)); ");

                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
                sb.Clear();
                // Criando configuracao 
                sb.AppendLine("CREATE TABLE public.configuracao( " +
                "validar_ncm boolean DEFAULT false,  versao_banco integer DEFAULT 0,  sat boolean DEFAULT false); ");

                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
                sb.Clear();
                // Criando Empresa
                sb.AppendLine("CREATE TABLE public.empresa " +
                "( id SERIAL PRIMARY KEY, " +
                "status boolean DEFAULT true, " +
                "fantasia character varying(60), " +
                "cpf_cnpj character varying(30), " +
                "endereco character varying(80), " +
                "numero character varying(20), " +
                "complemento character varying(20), " +
                "bairro character varying(60), " +
                "cep character varying(12), " +
                "pais integer, " +
                "estado integer, " +
                "cidade integer, " +
                "email character varying(200), " +
                "senha character varying(32), " +
                "data_cadastro timestamp with time zone DEFAULT now(), " +
                "codigo_versao integer DEFAULT 0); ");

                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
                sb.Clear();
                // Criando estado
                sb.AppendLine("CREATE TABLE public.estado " +
                "( id SERIAL PRIMARY KEY, " +
                "  uf character varying(2), " +
                "  descricao character varying(60), " +
                "  codigouf integer); ");

                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
                sb.Clear();
                // Criando fornecedor
                sb.AppendLine("CREATE TABLE public.fornecedor " +
                "( id SERIAL PRIMARY KEY, " +
                "  razao_social character varying(200), " +
                "  fantasia_nome character varying(80), " +
                "  cpf_cnpj character varying(18), " +
                "  tipo_pessoa integer, " +
                "  ie character varying(15), " +
                "  endereco character varying(80), " +
                "  cidade integer, " +
                "  estado integer, " +
                "  cep character varying(9), " +
                "  bairro character varying(80), " +
                "  telefone character varying(15), " +
                "  representante character varying(60), " +
                "  email character varying(120), " +
                "  observacao character varying(500), " +
                "  status boolean, " +
                "  numero character varying(10), " +
                "  complemento character varying(30)); ");

                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
                sb.Clear();
                // Criando Produto
                sb.AppendLine("CREATE TABLE public.produto " +
                "  (  id SERIAL PRIMARY KEY, " +
                "  nome character varying(60), " +
                "  empresa integer, " +
                "  fornecedor integer, " +
                "  ean character varying(100), " +
                "  tipo_produto integer, " +
                "  valor_compra double precision, " +
                "  valor_venda double precision, " +
                "  status boolean DEFAULT true, " +
                "  ncm character varying(20), " +
                "  cfop integer, " +
                "  estoque integer, " +
                "  estoque_minimo integer, " +
                "  estoque_maximo integer, " +
                "  sat_cfop integer DEFAULT 0, " +
                "  sat_ncm character varying(20), " +
                "  data_cadastro date DEFAULT ('now'::text)::date, " +
                "  valor_atacado double precision, " +
                "  margem double precision, " +
                "  margem_atacado double precision); ");

                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
                sb.Clear();
                // Razor licencas
                sb.AppendLine("CREATE TABLE public.razor_licencas " +
                "(  id SERIAL PRIMARY KEY, " +
                "  expiracao_licenca timestamp with time zone, " +
                "  mac_addres character varying(120), " +
                "  codigo_versao integer);");

                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
                sb.Clear();
                // cria usuario
                sb.AppendLine("CREATE TABLE public.usuario( " +
                  "id SERIAL PRIMARY KEY, " +
                  "nome character varying(60), " +
                  "usuario character varying(60), " +
                  "senha character varying(32), " +
                  "ativo boolean DEFAULT true, " +
                  "adm boolean); ");

                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
                sb.Clear();
                // cria Vendas
                sb.AppendLine(" CREATE TABLE public.vendas( " +
                  "id SERIAL PRIMARY KEY, " +
                  "status integer DEFAULT 0, " +
                  "data timestamp with time zone, " +
                  "valor_total double precision, " +
                  "desconto double precision, " +
                  "usuario integer, " +
                  "vendedor integer, " +
                  "origem smallint, " +
                  "xml text, " +
                  "xml_cancelamento text, " +
                  "dinheiro double precision, " +
                  "cheque double precision, " +
                  "credito double precision, " +
                  "debito double precision); ");

                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
                sb.Clear();
                // Vendas itens
                sb.AppendLine("CREATE TABLE public.vendas_itens " +
                "( id SERIAL PRIMARY KEY, " +
                "  venda integer, " +
                "  produto integer, " +
                "  valor double precision, " +
                "  quantidade integer); ");

                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
                sb.Clear();
                // cria versão
                sb.AppendLine("CREATE TABLE public.versao( " +
                "id SERIAL PRIMARY KEY, " +
                "codigo_versao character varying(20), " +
                "arquivo bytea, " +
                "data_coleta_versao timestamp with time zone DEFAULT now()); ");

                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // CADASTRANDO SÂO PAULO
                sb.Clear();
                sb.AppendLine("INSERT INTO estado(id,uf, descricao, codigouf)VALUES(35,'SP','SÂO PAULO',35);");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // CADASTRANDO 2 CIDADES DE SÂO PAULO
                sb.Clear();
                sb.AppendLine("INSERT INTO public.cidade(id, codigo_municipio, fk_codigo_uf, nome_municipio)VALUES(3523107,3523107,35,'Itaquaquecetuba');");
                sb.AppendLine("INSERT INTO public.cidade(id, codigo_municipio, fk_codigo_uf, nome_municipio)VALUES(3518800,3518800,35,'Guarulhos');");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Atualiza versão
                sb.Clear();
                sb.AppendLine("insert into razor_licencas (codigo_versao,mac_addres,expiracao_licenca) values(1,'" + this.mac + "','08/02/1995')");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

            }

            // VERSAO 1.0.1
            if (codigo_atualizacao < 2)
            {
                StringBuilder sb = new StringBuilder();

                // Criando crediário
                sb.AppendLine("CREATE TABLE public.crediario( ");
                sb.AppendLine(" id SERIAL PRIMARY KEY, ");
                sb.AppendLine("   status integer, ");
                sb.AppendLine("   venda integer, ");
                sb.AppendLine("   data timestamp with time zone, ");
                sb.AppendLine("   cliente integer, ");
                sb.AppendLine("   descricao text, ");
                sb.AppendLine("   parcelas integer ");
                sb.AppendLine(" ) ");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Criando detalhamento crediário
                sb.Clear();
                sb.AppendLine("CREATE TABLE public.crediario_detalhamento ");
                sb.AppendLine("( ");
                sb.AppendLine("  id SERIAL PRIMARY KEY, ");
                sb.AppendLine("  status integer, ");
                sb.AppendLine("  crediario integer, ");
                sb.AppendLine("  n_parcela integer, ");
                sb.AppendLine("  vencimento timestamp with time zone, ");
                sb.AppendLine("  valor double precision ");
                sb.AppendLine(")");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Criando cadastro de cliente 
                sb.Clear();
                sb.AppendLine("CREATE TABLE public.cliente ");
                sb.AppendLine("( ");
                sb.AppendLine("  id SERIAL PRIMARY KEY, ");
                sb.AppendLine("  nome character varying(200), ");
                sb.AppendLine("  cpf character varying(18),  rg character varying(20), ");
                sb.AppendLine("  endereco character varying(80), ");
                sb.AppendLine("  cidade integer, ");
                sb.AppendLine("  estado integer, ");
                sb.AppendLine("  cep character varying(9), ");
                sb.AppendLine("  bairro character varying(80), ");
                sb.AppendLine("  telefone character varying(20), ");
                sb.AppendLine("  celular character varying(20), ");
                sb.AppendLine("  observacao character varying(500), ");
                sb.AppendLine("  status boolean, ");
                sb.AppendLine("  numero character varying(10), ");
                sb.AppendLine("  complemento character varying(30) ");
                sb.AppendLine(") ");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Atualiza versão
                sb.Clear();
                sb.AppendLine("update razor_licencas set codigo_versao = 2 where mac_addres = '" + this.mac + "'");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Adiciona coluna crediario a vendas
                sb.Clear();
                sb.AppendLine("ALTER TABLE vendas add column crediario double precision; ");
                sb.AppendLine("update vendas set crediario = 0; ");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Adiciona columa versao do cupom fiscal
                sb.Clear();
                sb.AppendLine("ALTER TABLE empresa add column versao_cupom character varying(10); ");
                sb.AppendLine("ALTER TABLE empresa add column aliquota_icms double precision; ");
                sb.AppendLine("ALTER TABLE empresa add column ie character varying(20); ");
                sb.AppendLine("ALTER TABLE empresa add column codigo_vinculacao character varying(500); ");
                sb.AppendLine("ALTER TABLE empresa add column software_house character varying(30); ");
                sb.AppendLine("update empresa set versao_cupom = '7.0', aliquota_icms = 0,codigo_vinculacao = ''; ");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
            }


            // VERSAO 1.0.2
            if (codigo_atualizacao < 3)
            {
                StringBuilder sb = new StringBuilder();

                // Adicionando coluna para impressora
                sb.AppendLine("ALTER TABLE configuracao add column impressora character varying(50); ");
                sb.AppendLine("update configuracao set impressora = ''; ");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Coluna para status do caixa
                sb.Clear();
                sb.AppendLine("alter table caixa add column status integer;");
                sb.AppendLine("delete from caixa;");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Atualiza Vendas para receber id do caixa aberto
                sb.Clear();
                sb.AppendLine("alter table vendas add column caixa integer;");
                sb.AppendLine("update vendas set caixa = 0;");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Atualiza Crediario valor de baixa parcial
                sb.Clear();
                sb.AppendLine("alter table crediario_detalhamento add column parcial double precision;");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Adiciona coluna para download da versão
                sb.Clear();
                sb.AppendLine("alter table atualizacao add column codigo_versao integer;");
                sb.AppendLine("update atualizacao set codigo_versao = 0;");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());


                // Cofiguração fiscal = true
                sb.Clear();
                sb.AppendLine("update configuracao set sat = true;");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Cria tabela para operação do caixa (Acrescento e Retirada do caixa)
                sb.Clear();
                sb.AppendLine("CREATE TABLE caixa_operacoes ");
                sb.AppendLine("( ");
                sb.AppendLine("  id SERIAL PRIMARY KEY, ");
                sb.AppendLine("  tipo_operacao integer, ");
                sb.AppendLine("  caixa integer, ");
                sb.AppendLine("  descricao character varying(300), ");
                sb.AppendLine("  valor double precision ");
                sb.AppendLine(") ");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Atualiza versão
                sb.Clear();
                sb.AppendLine("update razor_licencas set codigo_versao = 3");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

            }

            // VERSÂO 1.0.3
            if (codigo_atualizacao < 4)
            {
                StringBuilder sb = new StringBuilder();

                // Coluna para status do caixa
                sb.Clear();
                sb.AppendLine("alter table configuracao add column impressora_pula_impressao Boolean;");
                sb.AppendLine("update configuracao set impressora_pula_impressao = false;");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());



                // Atualiza versão
                sb.Clear();
                sb.AppendLine("update razor_licencas set codigo_versao = 4");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
            }

            // VERSÂO 1.0.4
            if (codigo_atualizacao < 5)
            {
                StringBuilder sb = new StringBuilder();

                // Cria tabela grupo de Produtos
                sb.Clear();
                sb.AppendLine("CREATE TABLE grupo_produto ");
                sb.AppendLine("( ");
                sb.AppendLine("  id SERIAL PRIMARY KEY, ");
                sb.AppendLine("  status integer, ");
                sb.AppendLine("  descricao character varying(300) ");
                sb.AppendLine(") ");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Cria tabela Unidade de medida
                sb.Clear();
                sb.AppendLine("CREATE TABLE unidade_medida ");
                sb.AppendLine("( ");
                sb.AppendLine("  id SERIAL PRIMARY KEY, ");
                sb.AppendLine("  status integer, ");
                sb.AppendLine("  descricao character varying(80), ");
                sb.AppendLine("  sigla character varying(3) ");
                sb.AppendLine(") ");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Adiciona coluna grupo_produto e unidade de medida ao produto
                sb.Clear();
                sb.AppendLine("alter table produto add column grupo_produto integer; update produto set grupo_produto = 0;");
                sb.AppendLine("alter table produto add column unidade_medida integer;");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Insere unidades de medida
                sb.Clear();
                sb.AppendLine("insert into unidade_medida (status,descricao,sigla) values(1,'Unidade','UN');");
                sb.AppendLine("insert into unidade_medida (status,descricao,sigla) values(1,'Kilograma','KG');");
                sb.AppendLine("update produto set unidade_medida = 1");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Atualiza estoque para Double
                sb.Clear();
                sb.AppendLine("ALTER TABLE produto ALTER COLUMN estoque TYPE double precision;");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Criar columna para cadastro do path de arquivos do acbr
                sb.Clear();
                sb.AppendLine("alter table configuracao add column acbr character varying(1000);");
                sb.AppendLine("alter table empresa add column fake boolean;");
                sb.AppendLine("update empresa set fake = false;");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Atualiza versão
                sb.Clear();
                sb.AppendLine("update razor_licencas set codigo_versao = 5");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());
            }

            // VERSÂO 1.0.5
            if (codigo_atualizacao < 6){

                StringBuilder sb = new StringBuilder();

                // Cria id de controle Razor
                sb.Clear();
                sb.AppendLine("alter table empresa add column id_razor character varying(60);");
                sb.AppendLine("update empresa  set id_razor = '';");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Atualiza versão
                sb.Clear();
                sb.AppendLine("update razor_licencas set codigo_versao = 6");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

            }

            // VERSÂO 1.0.6
            if (codigo_atualizacao < 7)
            {

                StringBuilder sb = new StringBuilder();

                // vinculação de detalhamento do crediario ao caixa no momento da baixa
                sb.Clear();
                sb.AppendLine("alter table crediario_detalhamento add column caixa_recebido integer;");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Atualiza versão
                sb.Clear();
                sb.AppendLine("update razor_licencas set codigo_versao = 7");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

            }

        }




        //15 - Pará / PA
        //16 - Amapá / AP
        //17 - Tocantins / TO
        //Região Nordeste
        //21 - Maranhão / MA
        //22 - Piauí / PI
        //23 - Ceará / CE
        //24 - Rio Grande do Norte / RN
        //  25 - Paraíba / PB
        //26 - Pernambuco / PE
        //27 - Alagoas / AL
        //28 - Sergipe / SE
        //29 - Bahia / BA
        //Região Sudeste
        //31 - Minas Gerais / MG
        //32 - Espírito Santo / ES
        //33 - Rio de Janeiro/ RJ
        //35 - São Paulo / SP
        //Região Sul
        //41 - Paraná / PR
        //42 - Santa Catarina / SC
        //43 - Rio Grande do Sul / RS
        //  Região Centro - Oeste
        //50 - Mato Grosso do Sul / MS
        //  51 - Mato Grosso / MT
        //52 - Goiás / GO
        //53 - Distrito Federal / DF

        //      }


        //        }


        /*
         * 
status: [pago,cancelado]
tipo [Despesa fixas(valor fixo cobrado todos os mêses), despesa variavel (valor variavel)]
tipo_emissor:[Cliente, fornecedor]
id_emissor: 
descrição: 
data_criação
data_vencimento: 


# detalhamento 

id_conta
n_duplicata
status [aberto,pago]
data_vencimento
valor










CREATE TABLE configuracao_caixa
(
configuracao_balanca smallint DEFAULT 0,
exibir_balanca_pdv boolean,
quantidade_caracteres_peso smallint
);

insert into configuracao_caixa (configuracao_balanca,exibir_balanca_pdv,quantidade_caracteres_peso) values(1,false,5);

alter table vendas_itens alter column quantidade type Double precision;

alter table produto add column codigo_balanca integer;

drop table configuracao_caixa




         * */


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (inicializado)
            {
                timer1.Enabled = false;

                try
                {
                    Zenfox_Software_OO.data.bd_postgres bd = new Zenfox_Software_OO.data.bd_postgres();
                    bd.localdb();

                    Zenfox_Software_OO.Cadastros.Empresa cmd = new Zenfox_Software_OO.Cadastros.Empresa();
                    if (cmd.verifica_primeiro_acesso())
                    {
                        inicial inicial = new inicial();
                        inicial.ShowDialog();
                        timer1.Enabled = true;
                    }
                    else
                    {
                        this.Visible = false;
                        atualizacao.RunWorkerAsync();
                        Autenticacao autenticacao = new Autenticacao();
                        autenticacao.Show();
                    }

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    Configura_Database c_database = new Configura_Database();
                    c_database.ShowDialog();
                    timer1.Enabled = true;
                }
            }

        }

        private void atualizacao_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //  Zenfox_Software_OO.atualizacao atualizacao = new Zenfox_Software_OO.atualizacao();
                //  Zenfox_Software_OO.atualizacao.Entidade entidade_att = atualizacao.seleciona();
                //
                //  if (entidade_att.versao != null && entidade_att.versao != "")
                //  {
                //
                //  }
                //  else
                //  {
                //      entidade_att = atualizacao.pega_ultima_atualizacao();
                //      atualizacao.insere_atualizacao(entidade_att);
                //      //  System.IO.File.WriteAllBytes("C:\\Rede_Sistema", entidade_att.arquivo);
                //  }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }


        }
    }
}
