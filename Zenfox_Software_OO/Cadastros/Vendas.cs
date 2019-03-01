using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.Cadastros
{

    public class Entidade_Vendas
    {

        public Int32 id { get; set; }
        public Int32 id_produto { get; set; }
        public Int32 caixa { get; set; }
        public Status_venda status { get; set; }
        public Int32 ver_todos_status { get; set; } // 1 Para ver cancelados
        public String data { get; set; }
        public Double valor_total { get; set; }
        public Double desconto { get; set; }
        public Double crediario { get; set; }
        public Int32 usuario { get; set; }
        public Int32 vendedor { get; set; }
        public String xml { get; set; }
        public String xml_cancelamento { get; set; }
        public List<Entidade_Vendas_Produtos> produtos { get; set; }
        public Boolean para_cancelamento { get; set; }

        public String data_inicial { get; set; }
        public String data_final { get; set; }

        public Double dinheiro { get; set; }
        public Double debito { get; set; }
        public Double credito { get; set; }
        public Double cheque { get; set; }

        public Origem_venda origem { get; set; }


        public String nome { get; set; }
        public String ean { get; set; }
        public String ncm { get; set; }
        public Int32 cfop { get; set; }
        public Double quantidade { get; set; }

    }

    public class Entidade_Vendas_Produtos
    {
        public Int32 id { get; set; }
        public Int32 venda { get; set; }
        public Int32 produto { get; set; }
        public Double valor { get; set; }
        public Double quantidade { get; set; }
    }

    public enum Status_venda
    {
        aberto = 0,
        fechado = 1,
        cancelado = 2
    }

    public enum Origem_venda
    {
        caixa = 0,
        crediario = 1
    }


    public class Vendas
    {
        public List<Entidade_Vendas> seleciona_detalhamento(Entidade_Vendas item)
        {
            List<Entidade_Vendas> list = new List<Entidade_Vendas>();

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            String x = "select a.id,b.id as id_produto,b.ean,b.nome,b.ncm,b.cfop,a.quantidade,a.valor from vendas_itens as a ";

            x += "inner join produto as b on a.produto = b.id ";

            x += " where venda = " + item.id + "";


            sql.Comando.CommandText = x;
            IDataReader dr = sql.RetornaDados_v2();

            Int32 id = dr.GetOrdinal("id");
            Int32 id_produto = dr.GetOrdinal("id_produto");
            Int32 nome = dr.GetOrdinal("nome");
            Int32 ean = dr.GetOrdinal("ean");
            Int32 ncm = dr.GetOrdinal("ncm");
            Int32 cfop = dr.GetOrdinal("cfop");
            Int32 quantidade = dr.GetOrdinal("quantidade");
            Int32 valor = dr.GetOrdinal("valor");


            while (dr.Read())
            {
                Entidade_Vendas _item = new Entidade_Vendas();

                _item.id = dr.GetInt32(id);
                _item.id_produto = dr.GetInt32(id_produto);
                _item.nome = dr.GetString(nome);
                _item.ean = dr.GetString(ean);
                _item.ncm = dr.GetString(ncm);
                _item.cfop = dr.GetInt32(cfop);
                _item.quantidade = dr.GetDouble(quantidade);
                _item.valor_total = dr.GetDouble(valor);

                list.Add(_item);
            }

            return list;

        }

        public void cancela_venda(Entidade_Vendas item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            String x = "update vendas set status = @status,xml_cancelamento = @xml  where id = @id ";

            sql.Comando.Parameters.AddWithValue("@status", NpgsqlTypes.NpgsqlDbType.Integer, Status_venda.cancelado.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@xml", NpgsqlTypes.NpgsqlDbType.Varchar, item.xml);
            sql.Comando.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Integer, item.id);

            sql.Comando.CommandText = x;
            sql.ExecutaComando_v2();

            sql.FechaConexao();
        }


        public Entidade_Vendas seleciona_totais_cancelado(Entidade_Vendas item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            String x = "select sum(valor_total) as valor_total ";

            x += "from vendas ";
            x += "where status = " + Status_venda.cancelado.GetHashCode() + " ";

            if (item.para_cancelamento)
            {
                DateTime dt = DateTime.Now;
                dt = dt.AddMinutes(-30);
                x += " and data > '" + dt.ToString() + "' ";
            }

            if (item.data_inicial.Length > 0)
                x += " and data >= '" + item.data_inicial + "'";

            if (item.data_final.Length > 0)
                x += " and data <= '" + item.data_final + "'";

            if (item.caixa > 0)
                x += " and caixa = " + item.caixa + " ";

            sql.Comando.CommandText = x;
            IDataReader dr = sql.RetornaDados_v2();

            while (dr.Read())
            {

                if (!dr.IsDBNull(dr.GetOrdinal("valor_total")))
                    item.valor_total = dr.GetDouble(dr.GetOrdinal("valor_total"));
            }

            return item;

        }

        public Entidade_Vendas seleciona_totais(Entidade_Vendas item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            String x = "select sum(dinheiro) as dinheiro, sum(credito) as credito, sum(debito) as debito, sum(cheque) as cheque,sum(crediario) as crediario,sum(desconto) as desconto, sum(valor_total) as valor_total ";

            x += "from vendas ";


            x += "where status != " + Status_venda.aberto.GetHashCode() + " and status != "+Status_venda.cancelado.GetHashCode()+" ";
            
            if (item.ver_todos_status == 0)
                x += "and status != " + Status_venda.cancelado.GetHashCode() + " ";

            if (item.para_cancelamento)
            {
                DateTime dt = DateTime.Now;
                dt = dt.AddMinutes(-30);
                x += " and data > '" + dt.ToString() + "' ";
            }

            if (item.data_inicial.Length > 0)
                x += " and data >= '" + item.data_inicial + " 00:00:00'";

            if (item.data_final.Length > 0)
                x += " and data <= '" + item.data_final + " 23:59:59'";

            if (item.caixa > 0)
                x += " and caixa = " + item.caixa + " ";


            sql.Comando.CommandText = x;
            IDataReader dr = sql.RetornaDados_v2();

            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("dinheiro")))
                    item.dinheiro = dr.GetDouble(dr.GetOrdinal("dinheiro"));
                if (!dr.IsDBNull(dr.GetOrdinal("credito")))
                    item.credito = dr.GetDouble(dr.GetOrdinal("credito"));
                if (!dr.IsDBNull(dr.GetOrdinal("debito")))
                    item.debito = dr.GetDouble(dr.GetOrdinal("debito"));
                if (!dr.IsDBNull(dr.GetOrdinal("cheque")))
                    item.cheque = dr.GetDouble(dr.GetOrdinal("cheque"));
                if (!dr.IsDBNull(dr.GetOrdinal("desconto")))
                    item.desconto = dr.GetDouble(dr.GetOrdinal("desconto"));
                if (!dr.IsDBNull(dr.GetOrdinal("valor_total")))
                    item.valor_total = dr.GetDouble(dr.GetOrdinal("valor_total"));
                if (!dr.IsDBNull(dr.GetOrdinal("crediario")))
                    item.crediario = dr.GetDouble(dr.GetOrdinal("crediario"));
            }

            return item;

        }

        public Double seleciona_totais_crediario_vendido(Entidade_Vendas item)
        {
            Double total = 0;
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            String x = "select sum(valor_total) as total ";
            x += "from vendas ";
            x += "where status = " + Status_venda.fechado.GetHashCode() + " ";
            x += " and caixa = " + item.caixa + " ";
            x += " and origem = " + Origem_venda.crediario.GetHashCode() + " ";

            sql.Comando.CommandText = x;
            IDataReader dr = sql.RetornaDados_v2();

            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                    total = dr.GetDouble(0);
            }

            return total;

        }

        public DataTable seleciona_vendas_fechamento(Entidade_Vendas item)
        {

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            String x = "select ";
            x += "CASE WHEN status = 1 THEN 'Ativa' when status = 2 then 'Cancelado' end as status, ";
            x += "CASE WHEN origem = 0 THEN 'Caixa' when origem = 1 then 'Crediário' end as origem, ";
            x += "data,desconto,valor_total,(valor_total - desconto) as valor_vendido ";
            
            x += "from vendas ";
            x += "where 1 = 1 and status = " + Status_venda.fechado.GetHashCode() + " and origem != " + Origem_venda.crediario.GetHashCode() + " ";

            if (item.para_cancelamento)
            {
                DateTime dt = DateTime.Now;
                dt = dt.AddMinutes(-30);
                x += " and data > '" + dt.ToString() + "' ";
            }

            if (item.data_inicial.Length > 0)
                x += " and data >= '" + item.data_inicial + "' ";

            if (item.data_final.Length > 0)
                x += " and data <= '" + item.data_final + "' ";

            if (item.caixa > 0)
                x += " and caixa = " + item.caixa + " ";
            
            x += " order by data desc ";

            sql.Comando.CommandText = x;
            IDataReader dr = sql.RetornaDados_v2();
            DataTable dttb = new DataTable();
            dttb.Load(dr);

            sql.FechaConexao();
            return dttb;
        }



        public DataTable seleciona_vendas_gerencia(Entidade_Vendas item)
        {

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            String x = "select id, ";
            x += "CASE WHEN status = 1 THEN 'Ativa' when status = 2 then 'Cancelado' end as status, ";
            x += "CASE WHEN origem = 0 THEN 'Caixa' when origem = 1 then 'Crediário' end as origem, ";
            x += "data,dinheiro,debito,credito,cheque,crediario,desconto,valor_total ";


            x += "from vendas ";
            x += "where 1 = 1 and status != " + Status_venda.aberto.GetHashCode() + " ";

            if (item.para_cancelamento)
            {
                DateTime dt = DateTime.Now;
                dt = dt.AddMinutes(-30);
                x += " and data > '" + dt.ToString() + "' ";
            }

            if (item.data_inicial.Length > 0)
                x += " and data >= '" + item.data_inicial + " 00:00:00' ";

            if (item.data_final.Length > 0)
                x += " and data <= '" + item.data_final + " 23:59:59' ";

            if (item.caixa > 0)
                x += " and caixa = " + item.caixa + " ";

            x += " order by data desc ";

            sql.Comando.CommandText = x;
            IDataReader dr = sql.RetornaDados_v2();
            DataTable dttb = new DataTable();
            dttb.Load(dr);

            sql.FechaConexao();
            return dttb;
        }

        public DataTable seleciona_vendas(Entidade_Vendas item)
        {

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            String x = "select * from vendas where status = " + Status_venda.fechado.GetHashCode() + " ";

            if (item.para_cancelamento)
            {
                DateTime dt = DateTime.Now;
                dt = dt.AddMinutes(-30);
                x += " and data > @date";
                sql.Comando.Parameters.AddWithValue("@date",NpgsqlTypes.NpgsqlDbType.TimestampTZ,dt.ToString());
            }

            if (item.data_inicial != null)
                if (item.data_inicial.Length > 0)
                    x += " and data >= '" + item.data_inicial + "'";

            if (item.data_final != null)
                if (item.data_final.Length > 0)
                    x += " and data <= '" + item.data_final + "'";

            if (item.id > 0)
                x += " and id = " + item.id;

            sql.Comando.CommandText = x;
            IDataReader dr = sql.RetornaDados_v2();
            DataTable dttb = new DataTable();
            dttb.Load(dr);

            sql.FechaConexao();
            return dttb;
        }

        public Entidade_Vendas seleciona(Entidade_Vendas item)
        {

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            String x = "select * from vendas where id = " + item.id + " ";

            if (item.para_cancelamento)
            {
                DateTime dt = DateTime.Now;
                dt = dt.AddMinutes(-30);
                x += " and data > '" + dt.ToString() + "' ";
            }



            sql.Comando.CommandText = x;
            IDataReader dr = sql.RetornaDados_v2();

            Int32 id = dr.GetOrdinal("id");
            //Int32 status = dr.GetOrdinal("status");
            Int32 valor_total = dr.GetOrdinal("valor_total");
            Int32 xml = dr.GetOrdinal("xml");
            Int32 xml_cancelamento = dr.GetOrdinal("xml_cancelamento");

            while (dr.Read())
            {
                item.id = dr.GetInt32(id);
                item.valor_total = dr.GetDouble(valor_total);
                if (!dr.IsDBNull(xml))
                    item.xml = dr.GetString(xml);
                if (!dr.IsDBNull(xml_cancelamento))
                    item.xml_cancelamento = dr.GetString(xml_cancelamento);
            }

            sql.FechaConexao();
            return item;


        }

        public Int32 cadastra(Entidade_Vendas item)
        {

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO vendas (status,valor_total,usuario,vendedor)");
            sb.AppendLine("VALUES (0,@valor_total,0,0)returning id;");

            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = sb.ToString();

            sql.Comando.Parameters.AddWithValue("@valor_total", NpgsqlTypes.NpgsqlDbType.Double, item.valor_total);

            sql.AbrirConexao();
            Int32 id = sql.RetornaID_v2();
            sql.FechaConexao();

            foreach (var produto in item.produtos)
            {
                sb.Clear();
                sb.AppendLine("INSERT INTO public.vendas_itens(venda, produto, valor, quantidade) ");
                sb.AppendLine("VALUES(@id,@produto,@valor,@quantidade); ");

                sql.Comando = new Npgsql.NpgsqlCommand();
                sql.Comando.CommandText = sb.ToString();

                sql.Comando.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Integer, id);
                sql.Comando.Parameters.AddWithValue("@produto", NpgsqlTypes.NpgsqlDbType.Integer, produto.produto);
                sql.Comando.Parameters.AddWithValue("@valor", NpgsqlTypes.NpgsqlDbType.Double, produto.valor);
                sql.Comando.Parameters.AddWithValue("@quantidade", NpgsqlTypes.NpgsqlDbType.Double, produto.quantidade);

                sql.AbrirConexao();
                sql.ExecutaComando_v2();
                sql.FechaConexao();
            }


            return id;
        }

        public void fecha_venda(Entidade_Vendas item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("update vendas set status = @status ,origem =@origem,crediario = @crediario, data = current_timestamp,");
            sb.AppendLine("xml = @xml, dinheiro = @dinheiro, debito = @debito, credito = @credito, cheque = @cheque, desconto = @desconto ");
            sb.AppendLine(", caixa = (select id from caixa where status = @status_caixa) ");
            sb.AppendLine("where id = @id");

            sql.Comando.Parameters.AddWithValue("@status", NpgsqlTypes.NpgsqlDbType.Integer, Status_venda.fechado.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@xml", NpgsqlTypes.NpgsqlDbType.Varchar, item.xml);
            sql.Comando.Parameters.AddWithValue("@dinheiro", NpgsqlTypes.NpgsqlDbType.Double, item.dinheiro);
            sql.Comando.Parameters.AddWithValue("@debito", NpgsqlTypes.NpgsqlDbType.Double, item.debito);
            sql.Comando.Parameters.AddWithValue("@credito", NpgsqlTypes.NpgsqlDbType.Double, item.credito);
            sql.Comando.Parameters.AddWithValue("@cheque", NpgsqlTypes.NpgsqlDbType.Double, item.cheque);
            sql.Comando.Parameters.AddWithValue("@desconto", NpgsqlTypes.NpgsqlDbType.Double, item.desconto);
            sql.Comando.Parameters.AddWithValue("@crediario", NpgsqlTypes.NpgsqlDbType.Double, item.crediario);
            sql.Comando.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Integer, item.id);
            sql.Comando.Parameters.AddWithValue("@origem", NpgsqlTypes.NpgsqlDbType.Integer, item.origem.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@status_caixa", NpgsqlTypes.NpgsqlDbType.Integer, Zenfox_Software_OO.Caixa.status.aberto.GetHashCode());


            sql.Comando.CommandText = sb.ToString();
            sql.ExecutaComando_v2();
            sql.FechaConexao();

            sb.Clear();
            sb.AppendLine("update vendas set xml = @xml where id = @id");
            sql.Comando = new Npgsql.NpgsqlCommand();

            sql.Comando.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Integer, item.id);
            sql.Comando.Parameters.AddWithValue("@xml", NpgsqlTypes.NpgsqlDbType.Varchar, item.xml);

            sql.Comando.CommandText = sb.ToString();
            sql.AbrirConexao();
            sql.ExecutaComando_v2();
            sql.FechaConexao();
        }

    }
}
