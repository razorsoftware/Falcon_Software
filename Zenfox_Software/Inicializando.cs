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
            if (codigo_atualizacao < 6)
            {

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

            // VERSÂO 1.0.7
            if (codigo_atualizacao < 8)
            {

                StringBuilder sb = new StringBuilder();

                // Criando tabela de configucarao do caixa
                sb.Clear();
                sb.AppendLine("    CREATE TABLE configuracao_caixa ");
                sb.AppendLine("( ");
                sb.AppendLine("configuracao_balanca smallint DEFAULT 0, ");
                sb.AppendLine("exibir_balanca_pdv boolean, ");
                sb.AppendLine("quantidade_caracteres_peso smallint ");
                sb.AppendLine("); ");

                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Atualiza versão
                sb.Clear();
                sb.AppendLine("insert into configuracao_caixa(configuracao_balanca, exibir_balanca_pdv, quantidade_caracteres_peso) values(1, false, 5); ");
                sb.AppendLine("alter table vendas_itens alter column quantidade type Double precision; ");
                sb.AppendLine("alter table produto add column codigo_balanca integer; ");
                sb.AppendLine("update razor_licencas set codigo_versao = 8");
                Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

            }


            // VERSÂO 1.2
            if(codigo_atualizacao < 10)
            {
                StringBuilder sb = new StringBuilder();

                // Criando tabela de configucarao do caixa
                sb.Clear();
                sb.AppendLine("CREATE TABLE contabilidade ");
                sb.AppendLine("( ");
                sb.AppendLine("  id serial NOT NULL, ");
                sb.AppendLine("  nome character varying(80), ");
                sb.AppendLine("  email character varying(200) ");
                sb.AppendLine(") ");


                //Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Atualiza versão
                sb.Clear();
                sb.AppendLine("update razor_licencas set codigo_versao = 9");
                //Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

                // Atualiza Cidades de São paulo
                sb.Clear();
                #region Atualizando Cidades de são paulo 

                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3500105, 'Adamantina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3500204, 'Adolfo', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3500303, 'Aguaí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3500402, 'Águas da Prata', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3500501, 'Águas de Lindóia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3500550, 'Águas de Santa Bárbara ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3500600, 'Águas de São Pedro ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3500709, 'Agudos', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3500758, 'Alambari', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3500808, 'Alfredo Marcondes  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3500907, 'Altair', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3501004, 'Altinópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3501103, 'Alto Alegre', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3501152, 'Alumínio', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3501202, 'Álvares Florence   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3501301, 'Álvares Machado', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3501400, 'Álvaro de Carvalho', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3501509, 'Alvinlândia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3501608, 'Americana', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3501707, 'Américo Brasiliense', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3501806, 'Américo de Campos', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3501905, 'Amparo', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3502002, 'Analândia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3502101, 'Andradina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3502200, 'Angatuba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3502309, 'Anhembi', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3502408, 'Anhumas', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3502507, 'Aparecida', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3502606, 'Aparecida d´Oeste', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3502705, 'Apiaí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3502754, 'Araçariguama', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3502804, 'Araçatuba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3502903, 'Araçoiaba da Serra', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3503000, 'Aramina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3503109, 'Arandu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3503158, 'Arapeí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3503208, 'Araraquara', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3503307, 'Araras', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3503356, 'Arco-Íris', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3503406, 'Arealva', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3503505, 'Areias', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3503604, 'Areiópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3503703, 'Ariranha', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3503802, 'Artur Nogueira ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3503901, 'Arujá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3503950, 'Aspásia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3504008, 'Assis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3504107, 'Atibaia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3504206, 'Auriflama', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3504305, 'Avaí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3504404, 'Avanhandava', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3504503, 'Avaré', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3504602, 'Bady Bassitt   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3504701, 'Balbinos', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3504800, 'Bálsamo', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3504909, 'Bananal', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3505005, 'Barão de Antonina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3505104, 'Barbosa', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3505203, 'Bariri', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3505302, 'Barra Bonita   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3505351, 'Barra do Chapéu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3505401, 'Barra do Turvo', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3505500, 'Barretos', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3505609, 'Barrinha', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3505708, 'Barueri', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3505807, 'Bastos', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3505906, 'Batatais', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3506003, 'Bauru', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3506102, 'Bebedouro', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3506201, 'Bento de Abreu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3506300, 'Bernardino de Campos', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3506359, 'Bertioga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3506409, 'Bilac', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3506508, 'Birigui', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3506607, 'Biritiba-Mirim', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3506706, 'Boa Esperança do Sul', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3506805, 'Bocaina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3506904, 'Bofete', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3507001, 'Boituva', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3507100, 'Bom Jesus dos Perdões  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3507159, 'Bom Sucesso de Itararé ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3507209, 'Borá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3507308, 'Boracéia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3507407, 'Borborema', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3507456, 'Borebi', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3507506, 'Botucatu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3507605, 'Bragança Paulista  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3507704, 'Braúna', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3507753, 'Brejo Alegre   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3507803, 'Brodowski', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3507902, 'Brotas', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3508009, 'Buri', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3508108, 'Buritama', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3508207, 'Buritizal', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3508306, 'Cabrália Paulista  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3508405, 'Cabreúva', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3508504, 'Caçapava', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3508603, 'Cachoeira Paulista ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3508702, 'Caconde', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3508801, 'Cafelândia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3508900, 'Caiabu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3509007, 'Caieiras', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3509106, 'Caiuá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3509205, 'Cajamar', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3509254, 'Cajati', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3509304, 'Cajobi', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3509403, 'Cajuru', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3509452, 'Campina do Monte Alegre', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3509502, 'Campinas', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3509601, 'Campo Limpo Paulista', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3509700, 'Campos do Jordão', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3509809, 'Campos Novos Paulista', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3509908, 'Cananéia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3509957, 'Canas', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3510005, 'Cândido Mota   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3510104, 'Cândido Rodrigues  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3510153, 'Canitar', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3510203, 'Capão Bonito   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3510302, 'Capela do Alto', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3510401, 'Capivari', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3510500, 'Caraguatatuba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3510609, 'Carapicuíba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3510708, 'Cardoso', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3510807, 'Casa Branca', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3510906, 'Cássia dos Coqueiros', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3511003, 'Castilho', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3511102, 'Catanduva', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3511201, 'Catiguá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3511300, 'Cedral', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3511409, 'Cerqueira César', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3511508, 'Cerquilho', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3511607, 'Cesário Lange  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3511706, 'Charqueada', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3557204, 'Chavantes', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3511904, 'Clementina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3512001, 'Colina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3512100, 'Colômbia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3512209, 'Conchal', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3512308, 'Conchas', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3512407, 'Cordeirópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3512506, 'Coroados', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3512605, 'Coronel Macedo ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3512704, 'Corumbataí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3512803, 'Cosmópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3512902, 'Cosmorama', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3513009, 'Cotia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3513108, 'Cravinhos', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3513207, 'Cristais Paulista  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3513306, 'Cruzália', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3513405, 'Cruzeiro', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3513504, 'Cubatão', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3513603, 'Cunha', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3513702, 'Descalvado', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3513801, 'Diadema', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3513850, 'Dirce Reis ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3513900, 'Divinolândia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3514007, 'Dobrada', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3514106, 'Dois Córregos  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3514205, 'Dolcinópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3514304, 'Dourado', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3514403, 'Dracena', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3514502, 'Duartina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3514601, 'Dumont', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3514700, 'Echaporã', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3514809, 'Eldorado', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3514908, 'Elias Fausto   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3514924, 'Elisiário', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3514957, 'Embaúba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515103, 'Embu-Guaçu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515004, 'Embu das Artes', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515129, 'Emilianópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515152, 'Engenheiro Coelho  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515186, 'Espírito Santo do Pinhal', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515194, 'Espírito Santo do Turvo', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3557303, 'Estiva Gerbi   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515202, 'Estrela d´Oeste', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515301, 'Estrela do Norte', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515350, 'Euclides da Cunha Paulista ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515400, 'Fartura', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515608, 'Fernando Prestes   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515509, 'Fernandópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515657, 'Fernão', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515707, 'Ferraz de Vasconcelos', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515806, 'Flora Rica ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3515905, 'Floreal', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3516002, 'Flórida Paulista   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3516101, 'Florínia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3516200, 'Franca', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3516309, 'Francisco Morato   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3516408, 'Franco da Rocha', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3516507, 'Gabriel Monteiro   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3516606, 'Gália', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3516705, 'Garça', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3516804, 'Gastão Vidigal ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3516853, 'Gavião Peixoto ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3516903, 'General Salgado', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3517000, 'Getulina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3517109, 'Glicério', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3517208, 'Guaiçara', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3517307, 'Guaimbê', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3517406, 'Guaíra', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3517505, 'Guapiaçu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3517604, 'Guapiara', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3517703, 'Guará', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3517802, 'Guaraçaí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3517901, 'Guaraci', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3518008, 'Guarani d´Oeste', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3518107, 'Guarantã', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3518206, 'Guararapes', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3518305, 'Guararema', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3518404, 'Guaratinguetá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3518503, 'Guareí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3518602, 'Guariba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3518701, 'Guarujá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3518859, 'Guatapará', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3518909, 'Guzolândia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3519006, 'Herculândia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3519055, 'Holambra', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3519071, 'Hortolândia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3519105, 'Iacanga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3519204, 'Iacri', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3519253, 'Iaras', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3519303, 'Ibaté', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3519402, 'Ibirá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3519501, 'Ibirarema', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3519600, 'Ibitinga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3519709, 'Ibiúna', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3519808, 'Icém', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3519907, 'Iepê', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3520004, 'Igaraçu do Tietê', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3520103, 'Igarapava', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3520202, 'Igaratá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3520301, 'Iguape', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3520426, 'Ilha Comprida  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3520442, 'Ilha Solteira  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3520400, 'Ilhabela', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3520509, 'Indaiatuba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3520608, 'Indiana', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3520707, 'Indiaporã', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3520806, 'Inúbia Paulista', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3520905, 'Ipaussu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3521002, 'Iperó', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3521101, 'Ipeúna', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3521150, 'Ipiguá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3521200, 'Iporanga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3521309, 'Ipuã', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3521408, 'Iracemápolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3521507, 'Irapuã', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3521606, 'Irapuru', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3521705, 'Itaberá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3521804, 'Itaí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3521903, 'Itajobi', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3522000, 'Itaju', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3522109, 'Itanhaém', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3522158, 'Itaóca', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3522208, 'Itapecerica da Serra', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3522307, 'Itapetininga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3522406, 'Itapeva', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3522505, 'Itapevi', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3522604, 'Itapira', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3522653, 'Itapirapuã Paulista', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3522703, 'Itápolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3522802, 'Itaporanga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3522901, 'Itapuí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3523008, 'Itapura', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3523206, 'Itararé', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3523305, 'Itariri', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3523404, 'Itatiba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3523503, 'Itatinga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3523602, 'Itirapina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3523701, 'Itirapuã', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3523800, 'Itobi', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3523909, 'Itu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3524006, 'Itupeva', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3524105, 'Ituverava', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3524204, 'Jaborandi', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3524303, 'Jaboticabal', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3524402, 'Jacareí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3524501, 'Jaci', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3524600, 'Jacupiranga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3524709, 'Jaguariúna', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3524808, 'Jales', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3524907, 'Jambeiro', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3525003, 'Jandira', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3525102, 'Jardinópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3525201, 'Jarinu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3525300, 'Jaú', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3525409, 'Jeriquara', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3525508, 'Joanópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3525607, 'João Ramalho   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3525706, 'José Bonifácio ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3525805, 'Júlio Mesquita ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3525854, 'Jumirim', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3525904, 'Jundiaí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3526001, 'Junqueirópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3526100, 'Juquiá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3526209, 'Juquitiba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3526308, 'Lagoinha', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3526407, 'Laranjal Paulista  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3526506, 'Lavínia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3526605, 'Lavrinhas', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3526704, 'Leme', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3526803, 'Lençóis Paulista   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3526902, 'Limeira', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3527009, 'Lindóia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3527108, 'Lins', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3527207, 'Lorena', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3527256, 'Lourdes', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3527306, 'Louveira', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3527405, 'Lucélia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3527504, 'Lucianópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3527603, 'Luís Antônio   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3527702, 'Luiziânia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3527801, 'Lupércio', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3527900, 'Lutécia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3528007, 'Macatuba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3528106, 'Macaubal', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3528205, 'Macedônia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3528304, 'Magda', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3528403, 'Mairinque', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3528502, 'Mairiporã', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3528601, 'Manduri', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3528700, 'Marabá Paulista', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3528809, 'Maracaí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3528858, 'Marapoama', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3528908, 'Mariápolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3529005, 'Marília', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3529104, 'Marinópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3529203, 'Martinópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3529302, 'Matão', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3529401, 'Mauá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3529500, 'Mendonça', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3529609, 'Meridiano', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3529658, 'Mesópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3529708, 'Miguelópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3529807, 'Mineiros do Tietê', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3530003, 'Mira Estrela   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3529906, 'Miracatu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3530102, 'Mirandópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3530201, 'Mirante do Paranapanema', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3530300, 'Mirassol', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3530409, 'Mirassolândia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3530508, 'Mococa', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3530607, 'Mogi das Cruzes', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3530706, 'Mogi Guaçu ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3530805, 'Mogi Mirim ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3530904, 'Mombuca', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3531001, 'Monções', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3531100, 'Mongaguá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3531209, 'Monte Alegre do Sul', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3531308, 'Monte Alto ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3531407, 'Monte Aprazível', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3531506, 'Monte Azul Paulista', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3531605, 'Monte Castelo  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3531803, 'Monte Mor  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3531704, 'Monteiro Lobato', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3531902, 'Morro Agudo', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532009, 'Morungaba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532058, 'Motuca', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532108, 'Murutinga do Sul', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532157, 'Nantes', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532207, 'Narandiba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532306, 'Natividade da Serra', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532405, 'Nazaré Paulista', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532504, 'Neves Paulista ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532603, 'Nhandeara', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532702, 'Nipoã', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532801, 'Nova Aliança   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532827, 'Nova Campina   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532843, 'Nova Canaã Paulista', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532868, 'Nova Castilho  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3532900, 'Nova Europa', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3533007, 'Nova Granada   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3533106, 'Nova Guataporanga  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3533205, 'Nova Independência ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3533304, 'Nova Luzitânia ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3533403, 'Nova Odessa', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3533254, 'Novais', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3533502, 'Novo Horizonte ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3533601, 'Nuporanga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3533700, 'Ocauçu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3533809, 'Óleo', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3533908, 'Olímpia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3534005, 'Onda Verde ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3534104, 'Oriente', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3534203, 'Orindiúva', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3534302, 'Orlândia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3534401, 'Osasco', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3534500, 'Oscar Bressane ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3534609, 'Osvaldo Cruz   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3534708, 'Ourinhos', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3534807, 'Ouro Verde ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3534757, 'Ouroeste', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3534906, 'Pacaembu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3535002, 'Palestina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3535101, 'Palmares Paulista  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3535200, 'Palmeira d´Oeste', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3535309, 'Palmital', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3535408, 'Panorama', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3535507, 'Paraguaçu Paulista ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3535606, 'Paraibuna', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3535705, 'Paraíso', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3535804, 'Paranapanema', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3535903, 'Paranapuã', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3536000, 'Parapuã', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3536109, 'Pardinho', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3536208, 'Pariquera-Açu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3536257, 'Parisi', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3536307, 'Patrocínio Paulista', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3536406, 'Paulicéia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3536505, 'Paulínia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3536570, 'Paulistânia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3536604, 'Paulo de Faria', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3536703, 'Pederneiras', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3536802, 'Pedra Bela ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3536901, 'Pedranópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3537008, 'Pedregulho', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3537107, 'Pedreira', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3537156, 'Pedrinhas Paulista ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3537206, 'Pedro de Toledo', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3537305, 'Penápolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3537404, 'Pereira Barreto', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3537503, 'Pereiras', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3537602, 'Peruíbe', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3537701, 'Piacatu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3537800, 'Piedade', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3537909, 'Pilar do Sul', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3538006, 'Pindamonhangaba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3538105, 'Pindorama', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3538204, 'Pinhalzinho', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3538303, 'Piquerobi', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3538501, 'Piquete', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3538600, 'Piracaia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3538709, 'Piracicaba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3538808, 'Piraju', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3538907, 'Pirajuí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3539004, 'Pirangi', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3539103, 'Pirapora do Bom Jesus  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3539202, 'Pirapozinho', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3539301, 'Pirassununga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3539400, 'Piratininga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3539509, 'Pitangueiras', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3539608, 'Planalto', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3539707, 'Platina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3539806, 'Poá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3539905, 'Poloni', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3540002, 'Pompéia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3540101, 'Pongaí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3540200, 'Pontal', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3540259, 'Pontalinda', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3540309, 'Pontes Gestal  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3540408, 'Populina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3540507, 'Porangaba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3540606, 'Porto Feliz', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3540705, 'Porto Ferreira ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3540754, 'Potim', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3540804, 'Potirendaba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3540853, 'Pracinha', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3540903, 'Pradópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3541000, 'Praia Grande   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3541059, 'Pratânia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3541109, 'Presidente Alves   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3541208, 'Presidente Bernardes   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3541307, 'Presidente Epitácio', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3541406, 'Presidente Prudente', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3541505, 'Presidente Venceslau   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3541604, 'Promissão', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3541653, 'Quadra', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3541703, 'Quatá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3541802, 'Queiroz', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3541901, 'Queluz', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3542008, 'Quintana', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3542107, 'Rafard', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3542206, 'Rancharia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3542305, 'Redenção da Serra', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3542404, 'Regente Feijó  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3542503, 'Reginópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3542602, 'Registro', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3542701, 'Restinga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3542800, 'Ribeira', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3542909, 'Ribeirão Bonito', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3543006, 'Ribeirão Branco', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3543105, 'Ribeirão Corrente  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3543204, 'Ribeirão do Sul', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3543238, 'Ribeirão dos Índios', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3543253, 'Ribeirão Grande', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3543303, 'Ribeirão Pires ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3543402, 'Ribeirão Preto ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3543600, 'Rifaina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3543709, 'Rincão', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3543808, 'Rinópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3543907, 'Rio Claro  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3544004, 'Rio das Pedras', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3544103, 'Rio Grande da Serra', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3544202, 'Riolândia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3543501, 'Riversul', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3544251, 'Rosana', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3544301, 'Roseira', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3544400, 'Rubiácea', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3544509, 'Rubinéia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3544608, 'Sabino', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3544707, 'Sagres', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3544806, 'Sales', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3544905, 'Sales Oliveira ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3545001, 'Salesópolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3545100, 'Salmourão', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3545159, 'Saltinho', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3545209, 'Salto', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3545308, 'Salto de Pirapora', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3545407, 'Salto Grande   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3545506, 'Sandovalina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3545605, 'Santa Adélia   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3545704, 'Santa Albertina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3545803, 'Santa Bárbara d´Oeste', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3546009, 'Santa Branca   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3546108, 'Santa Clara d´Oeste', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3546207, 'Santa Cruz da Conceição', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3546256, 'Santa Cruz da Esperança', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3546306, 'Santa Cruz das Palmeiras   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3546405, 'Santa Cruz do Rio Pardo', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3546504, 'Santa Ernestina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3546603, 'Santa Fé do Sul', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3546702, 'Santa Gertrudes', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3546801, 'Santa Isabel   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3546900, 'Santa Lúcia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3547007, 'Santa Maria da Serra   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3547106, 'Santa Mercedes ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3547403, 'Santa Rita d´Oeste', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3547502, 'Santa Rita do Passa Quatro ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3547601, 'Santa Rosa de Viterbo  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3547650, 'Santa Salete   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3547205, 'Santana da Ponte Pensa ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3547304, 'Santana de Parnaíba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3547700, 'Santo Anastácio', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3547809, 'Santo André', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3547908, 'Santo Antônio da Alegria   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3548005, 'Santo Antônio de Posse ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3548054, 'Santo Antônio do Aracanguá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3548104, 'Santo Antônio do Jardim', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3548203, 'Santo Antônio do Pinhal', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3548302, 'Santo Expedito ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3548401, 'Santópolis do Aguapeí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3548500, 'Santos', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3548609, 'São Bento do Sapucaí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3548708, 'São Bernardo do Campo', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3548807, 'São Caetano do Sul', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3548906, 'São Carlos ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3549003, 'São Francisco  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3549102, 'São João da Boa Vista', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3549201, 'São João das Duas Pontes', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3549250, 'São João de Iracema', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3549300, 'São João do Pau d´Alho', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3549409, 'São Joaquim da Barra   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3549508, 'São José da Bela Vista', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3549607, 'São José do Barreiro', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3549706, 'São José do Rio Pardo  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3549805, 'São José do Rio Preto  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3549904, 'São José dos Campos', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3549953, 'São Lourenço da Serra  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3550001, 'São Luís do Paraitinga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3550100, 'São Manuel ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3550209, 'São Miguel Arcanjo', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3550308, 'São Paulo  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3550407, 'São Pedro  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3550506, 'São Pedro do Turvo', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3550605, 'São Roque  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3550704, 'São Sebastião  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3550803, 'São Sebastião da Grama ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3550902, 'São Simão  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3551009, 'São Vicente', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3551108, 'Sarapuí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3551207, 'Sarutaiá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3551306, 'Sebastianópolis do Sul', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3551405, 'Serra Azul ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3551603, 'Serra Negra', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3551504, 'Serrana', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3551702, 'Sertãozinho', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3551801, 'Sete Barras', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3551900, 'Severínia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3552007, 'Silveiras', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3552106, 'Socorro', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3552205, 'Sorocaba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3552304, 'Sud Mennucci   ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3552403, 'Sumaré', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3552551, 'Suzanápolis', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3552502, 'Suzano', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3552601, 'Tabapuã', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3552700, 'Tabatinga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3552809, 'Taboão da Serra', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3552908, 'Taciba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3553005, 'Taguaí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3553104, 'Taiaçu', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3553203, 'Taiúva', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3553302, 'Tambaú', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3553401, 'Tanabi', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3553500, 'Tapiraí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3553609, 'Tapiratiba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3553658, 'Taquaral', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3553708, 'Taquaritinga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3553807, 'Taquarituba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3553856, 'Taquarivaí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3553906, 'Tarabai', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3553955, 'Tarumã', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3554003, 'Tatuí', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3554102, 'Taubaté', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3554201, 'Tejupá', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3554300, 'Teodoro Sampaio', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3554409, 'Terra Roxa ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3554508, 'Tietê', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3554607, 'Timburi', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3554656, 'Torre de Pedra', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3554706, 'Torrinha', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3554755, 'Trabiju', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3554805, 'Tremembé', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3554904   ,'Três Fronteiras', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3554953, 'Tuiuti', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3555000, 'Tupã', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3555109, 'Tupi Paulista  ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3555208, 'Turiúba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3555307, 'Turmalina', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3555356, 'Ubarana', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3555406, 'Ubatuba', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3555505, 'Ubirajara', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3555604, 'Uchoa', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3555703, 'União Paulista ', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3555802, 'Urânia', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3555901, 'Uru', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3556008, 'Urupês', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3556107, 'Valentim Gentil', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3556206, 'Valinhos', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3556305, 'Valparaíso', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3556354, 'Vargem', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3556404, 'Vargem Grande do Sul', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3556453, 'Vargem Grande Paulista', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3556503, 'Várzea Paulista', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3556602, 'Vera Cruz  ', 35);"); 
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3556701, 'Vinhedo', 35);"); 
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3556800, 'Viradouro', 35);"); 
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3556909, 'Vista Alegre do Alto', 35);"); 
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3556958, 'Vitória Brasil ', 35);"); 
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3557006, 'Votorantim', 35);"); 
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3557105, 'Votuporanga', 35);");
                sb.AppendLine("insert into cidade(codigo_municipio, nome_municipio, fk_codigo_uf) values(3557154, 'Zacarias', 35);");

                #endregion
              //  Zenfox_Software_OO.helper.executa_comando_sql(sb.ToString());

            }

            //drop table configuracao_caixa


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
