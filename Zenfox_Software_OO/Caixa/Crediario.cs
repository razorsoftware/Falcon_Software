using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.Caixa
{
    public class Crediario
    {

        public class Entidade
        {
            public Int32 id { get; set; }
            public Int32 crediario { get; set; }
            public status status { get; set; }
            public Int32 venda { get; set; }
            public Int32 cliente { get; set; }
            public String descricao { get; set; }
            public Int32 parcelas { get; set; }
            public String vencimento { get; set; }
            public Double valor { get; set; }
            public Double parcial { get; set; }
            public Int32 n_parcela { get; set; }
            public String data_inicial { get; set; }
            public String data_final { get; set; }
            public String data_criacao { get; set; }
            public Boolean pesquisa_atrasado { get; set; }
            public Boolean pesquisa_todas_do_cliente { get; set; }
        }

        public enum status
        {
            aberto = 0,
            pago = 1,
            cancelado = 2,
            parcial = 3
        }

        public static void baixa_parcial(Int32 id_duplicata, Double valor)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            StringBuilder sb = new StringBuilder();

            Zenfox_Software_OO.Caixa.Crediario cmd = new Crediario();
            valor += cmd.seleciona_crediario_detalhamento(new Entidade() { id = id_duplicata })[0].parcial;

            sb.AppendLine("update crediario_detalhamento set status = @status, parcial = @valor where id = @id");
            sql.Comando = new Npgsql.NpgsqlCommand();


            sql.Comando.Parameters.AddWithValue("@status", NpgsqlTypes.NpgsqlDbType.Integer, status.parcial.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@valor", NpgsqlTypes.NpgsqlDbType.Double, valor);
            sql.Comando.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Integer, id_duplicata);

            sql.AbrirConexao();
            sql.Comando.CommandText = sb.ToString();
            sql.ExecutaComando_v2();
            sql.FechaConexao();
        }

        public static void imprimir_crediario(Int32 id, Int32 id_usuario)
        {
            Zenfox_Software_OO.Caixa.Crediario cmd_crediario = new Crediario();
            Zenfox_Software_OO.Caixa.Crediario.Entidade crediario = cmd_crediario.seleciona_crediario(new Entidade() { id = id });
            List<Zenfox_Software_OO.Caixa.Crediario.Entidade> crediario_detalhe = cmd_crediario.seleciona_crediario_detalhamento(new Entidade() { crediario = id });

            Zenfox_Software_OO.Impressora impressora = new Impressora();

            impressora.operador = Zenfox_Software_OO.Cadastros.Usuario.seleciona_nome(new Cadastros.Entidade_Usuario() { id = id_usuario });

            //Selecionando empresa ==============================
            Zenfox_Software_OO.Cadastros.Empresa cmd_empresa = new Cadastros.Empresa();
            Zenfox_Software_OO.Cadastros.Empresa.Entidade empresa = cmd_empresa.seleciona();
            impressora.fantasia_empresa = empresa.fantasia;
            impressora.cnpj = empresa.cnpj;

            // Selecionando Cliente =============================
            Zenfox_Software_OO.Cadastros.Cliente cmd_cliente = new Cadastros.Cliente();
            Zenfox_Software_OO.Cadastros.Cliente.Entidade cliente = cmd_cliente.seleciona(new Cadastros.Cliente.Entidade() { id = crediario.cliente });
            impressora.nome_cliente = cliente.nome;
            impressora.cpf_cliente = cliente.cpf;

            // Selecionando duplicatas
            impressora.list_crediario = crediario_detalhe;


            impressora.imprime_crediario();

        }

        public List<Entidade> seleciona_crediario_detalhamento(Entidade item)
        {
            List<Entidade> list = new List<Entidade>();

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            String x = "select * from crediario_detalhamento where 1 = 1 ";

            if (item.crediario > 0)
                x += " and crediario = " + item.crediario + " ";

            if (item.id > 0)
                x += " and id = " + item.id + " ";

            x += " order by vencimento asc";

            sql.Comando.CommandText = x;
            IDataReader dr = sql.RetornaDados_v2();

            Int32 id = dr.GetOrdinal("id");
            Int32 status = dr.GetOrdinal("status");
            Int32 n_parcela = dr.GetOrdinal("n_parcela");
            Int32 vencimento = dr.GetOrdinal("vencimento");
            Int32 valor = dr.GetOrdinal("valor");
            Int32 parcial = dr.GetOrdinal("parcial");
            // Int32 data = dr.GetOrdinal("data");
            // Int32 data = dr.GetOrdinal("data");
            // Int32 data = dr.GetOrdinal("data");

            while (dr.Read())
            {
                Entidade _item = new Entidade();

                _item.id = dr.GetInt32(id);
                _item.status = (status)dr.GetInt32(status);
                _item.n_parcela = dr.GetInt32(n_parcela);
                _item.vencimento = dr.GetDateTime(vencimento).ToShortDateString();
                _item.valor = dr.GetDouble(valor);
                _item.parcial = dr.GetDouble(parcial);
                list.Add(_item);
            }
            sql.FechaConexao();
            return list;
        }


        public Entidade seleciona_crediario(Entidade item)
        {

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            String x = "select * from crediario where id = " + item.id;

            //  if (item.data_inicial.Length > 0)
            //      x += " and c.vencimento >= '" + item.data_inicial + "' ";
            //
            //  if (item.data_final.Length > 0)
            //     x += " and c.vencimento <= '" + item.data_final + "' ";


            sql.Comando.CommandText = x;
            IDataReader dr = sql.RetornaDados_v2();

            Int32 id = dr.GetOrdinal("id");
            Int32 data = dr.GetOrdinal("data");
            Int32 status = dr.GetOrdinal("status");
            Int32 descricao = dr.GetOrdinal("descricao");
            Int32 cliente = dr.GetOrdinal("cliente");
            // Int32 data = dr.GetOrdinal("data");
            // Int32 data = dr.GetOrdinal("data");
            // Int32 data = dr.GetOrdinal("data");

            while (dr.Read())
            {
                item.cliente = dr.GetInt32(cliente);
                item.data_criacao = dr.GetDateTime(data).ToString();
                item.id = dr.GetInt32(id);
                item.status = (status)dr.GetInt32(status);
                item.descricao = dr.GetString(descricao);
            }
            sql.FechaConexao();
            return item;
        }

        public DataTable seleciona_vendas_gerencia(Entidade item)
        {

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            String x = "";

            if (item.pesquisa_todas_do_cliente)
            {
                x += "select ";
                x += "    a.id, ";
                x += "    case when a.status = 0 then 'Aberto' when a.status = 1 then 'Pago' when a.status = 2 then 'Cancelado' end as status, ";
                x += "    cliente.nome, ";
                x += "    a.data, ";
                x += "    sum(total.valor) as total_crediario ";
                x += "from crediario as a ";
                x += "inner join cliente as cliente on cliente.id = a.cliente ";
                x += "inner join crediario_detalhamento as total on a.id = total.crediario ";
                x += "where a.cliente = "+item.cliente+" ";
                x += "group by 1,2,3,4 order by a.data desc";

            }
            else
            {

                x += " select ";
                x += "    b.id, ";

                x += "    case when a.status = 0 then 'Aberto' when a.status = 1 then 'Pago' when a.status = 2 then 'Cancelado' end as status, ";
                x += "    c.nome, ";
                x += "    a.vencimento, ";
                x += "    a,valor_duplicata, ";
                x += "    a.total_crediario, ";
                x += "    a.total_pago, ";
                x += "    (a.total_crediario - (case when a.total_pago is null then 0 end)) as total_saldo ";
                x += " from ";
                x += "(select a.id, a.status, a.crediario, a.vencimento, a.valor as valor_duplicata, sum(b_total_crediario.valor) as total_crediario, sum(b_pago.valor) as total_pago ";
                x += "from crediario_detalhamento as a ";
                x += "left join crediario_detalhamento as b_total_crediario on a.crediario = b_total_crediario.crediario ";
                x += "left join crediario_detalhamento as b_pago on a.crediario = b_pago.crediario and b_pago.status = 1 ";

                x += " where 1 = 1 ";

                if (item.pesquisa_atrasado)
                    x += "and a.vencimento < current_date and a.status = " + status.aberto.GetHashCode() + " ";
                else
                    x += " and a.vencimento >= '" + item.data_inicial + "' and a.vencimento <= '" + item.data_final + "' ";


                x += "group by 1, 2, 3, 4, 5) as a ";
                x += " ";
                x += "inner join crediario as b on b.id = a.crediario ";
                x += "inner join cliente as c on c.id = b.cliente ";

                if (item.pesquisa_todas_do_cliente)
                    x += "and c.id = " + item.cliente + " ";

                x += " order by a.vencimento desc  ";
            }


            sql.Comando.CommandText = x;
            IDataReader dr = sql.RetornaDados_v2();
            DataTable dttb = new DataTable();
            dttb.Load(dr);

            sql.FechaConexao();
            return dttb;
        }

        public Int32 salva(Entidade item)
        {

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO crediario (status,venda,cliente,descricao,parcelas,data)");
            sb.AppendLine("VALUES (@status,@venda,@cliente,@descricao,@parcelas,current_timestamp)returning id;");

            sql.Comando = new Npgsql.NpgsqlCommand();


            sql.Comando.Parameters.AddWithValue("@status", NpgsqlTypes.NpgsqlDbType.Integer, status.aberto.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@venda", NpgsqlTypes.NpgsqlDbType.Integer, item.venda);
            sql.Comando.Parameters.AddWithValue("@cliente", NpgsqlTypes.NpgsqlDbType.Integer, item.cliente);
            sql.Comando.Parameters.AddWithValue("@descricao", NpgsqlTypes.NpgsqlDbType.Varchar, item.descricao);
            sql.Comando.Parameters.AddWithValue("@parcelas", NpgsqlTypes.NpgsqlDbType.Integer, item.parcelas);

            sql.AbrirConexao();
            sql.Comando.CommandText = sb.ToString();
            Int32 id = sql.RetornaID_v2();
            sql.FechaConexao();
            return id;
        }

        public void salva_detalhamento(Entidade item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO crediario_detalhamento (status, crediario, vencimento, valor, n_parcela,parcial)");
            sb.AppendLine("VALUES (@status, @crediario,@vencimento, @valor, @n_parcela,0);");

            sql.Comando = new Npgsql.NpgsqlCommand();


            sql.Comando.Parameters.AddWithValue("@status", NpgsqlTypes.NpgsqlDbType.Integer, status.aberto.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@crediario", NpgsqlTypes.NpgsqlDbType.Integer, item.crediario);
            sql.Comando.Parameters.AddWithValue("@vencimento", NpgsqlTypes.NpgsqlDbType.Date, item.vencimento);
            sql.Comando.Parameters.AddWithValue("@valor", NpgsqlTypes.NpgsqlDbType.Double, item.valor);
            sql.Comando.Parameters.AddWithValue("@n_parcela", NpgsqlTypes.NpgsqlDbType.Integer, item.n_parcela);

            sql.AbrirConexao();
            sql.Comando.CommandText = sb.ToString();
            sql.ExecutaComando_v2();
            sql.FechaConexao();

        }

        public void dar_baixa(Entidade item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("update crediario_detalhamento set status = @status, parcial = (select valor from crediario_detalhamento where crediario = @crediario and n_parcela = @n_parcela) where crediario = @crediario and n_parcela = @n_parcela");
            sql.Comando = new Npgsql.NpgsqlCommand();


            sql.Comando.Parameters.AddWithValue("@status", NpgsqlTypes.NpgsqlDbType.Integer, status.pago.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@crediario", NpgsqlTypes.NpgsqlDbType.Integer, item.crediario);
            sql.Comando.Parameters.AddWithValue("@n_parcela", NpgsqlTypes.NpgsqlDbType.Integer, item.n_parcela);

            sql.AbrirConexao();
            sql.Comando.CommandText = sb.ToString();
            sql.ExecutaComando_v2();
            sql.FechaConexao();

            // Verifica se crediario ainda tem duplicata em aberta
            sb.Clear();
            sb.Append("select * from crediario_detalhamento where crediario = @crediario and status != @status  ");
            sql.Comando = new Npgsql.NpgsqlCommand();

            sql.Comando.Parameters.AddWithValue("@status", NpgsqlTypes.NpgsqlDbType.Integer, status.pago.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@crediario", NpgsqlTypes.NpgsqlDbType.Integer, item.crediario);

            sql.AbrirConexao();
            sql.Comando.CommandText = sb.ToString();
            IDataReader dr = sql.RetornaDados_v2();
            Boolean x = true;
            while (dr.Read())
            {
                x = false;
            }
            sql.FechaConexao();

            // Fechando crediario ==============================
            if (x){
                sb.Clear();
                sb.AppendLine("update crediario set status = @status where id = @id");
                sql.Comando = new Npgsql.NpgsqlCommand();
                sql.Comando.Parameters.AddWithValue("@status", NpgsqlTypes.NpgsqlDbType.Integer, status.pago.GetHashCode());
                sql.Comando.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Integer, item.crediario);

                sql.AbrirConexao();
                sql.Comando.CommandText = sb.ToString();
                sql.ExecutaComando_v2();
                sql.FechaConexao();
            }

        }
    }
}
