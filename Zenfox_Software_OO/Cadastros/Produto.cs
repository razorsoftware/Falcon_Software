using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.Cadastros
{

    public class Entidade_Produto
    {

        public Int32 id { get; set; }
        public String codigo_balanca { get; set; }
        public Int32 fornecedor { get; set; }
        public Int32 grupo_produto { get; set; }
        public Int32 unidade_medida { get; set; }
        public String nome_produto { get; set; }
        public String ean { get; set; }
        public String ncm { get; set; }
        public Int32 cfop { get; set; }
        public String data_cadastro { get; set; }

        public String search { get; set; }

        public Double valor_compra { get; set; }
        public Double valor_venda { get; set; }
        public Double valor_venda_margem { get; set; }
        public Double valor_venda_atacado { get; set; }
        public Double valor_venda_atacado_margem { get; set; }

        public Double estoque { get; set; }
        public Double estoque_inicial { get; set; }
        public Double estoque_minimo { get; set; }
        public Double estoque_maximo { get; set; }

        public Int32 estoque_zerado { get; set; }
        public Int32 estoque_abaixo_minimo { get; set; }

    }

    public class Produto
    {


        public Entidade_Produto seleciona_entidade(Entidade_Produto item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("select * from produto where 1 = 1 ");

            if (item.id > 0)
                sb.AppendLine("and id = " + item.id + " ");
            if (item.ean != null)
                if (item.ean.Length > 0)
                    sb.AppendLine(" and ean = '" + item.ean + "' ");
            if (item.codigo_balanca != null)
                if (item.codigo_balanca.Length > 0)
                    sb.AppendLine(" and codigo_balanca = '" + item.codigo_balanca + "' ");

            sql.Comando.CommandText = sb.ToString();
            IDataReader dr = sql.RetornaDados_v2();

            Int32 id = dr.GetOrdinal("id");
            Int32 codigo_balanca = dr.GetOrdinal("codigo_balanca");
            Int32 fornecedor = dr.GetOrdinal("fornecedor");
            Int32 xnome = dr.GetOrdinal("nome");
            Int32 data_cadastro = dr.GetOrdinal("data_cadastro");
            Int32 ean = dr.GetOrdinal("ean");
            Int32 valor_compra = dr.GetOrdinal("valor_compra");
            Int32 valor_venda = dr.GetOrdinal("valor_venda");
            Int32 valor_venda_atacado = dr.GetOrdinal("valor_atacado");
            Int32 valor_venda_margem = dr.GetOrdinal("margem");
            Int32 valor_venda_atacado_margem = dr.GetOrdinal("margem_atacado");
            Int32 cfop = dr.GetOrdinal("cfop");
            Int32 ncm = dr.GetOrdinal("ncm");
            Int32 estoque = dr.GetOrdinal("estoque");
            Int32 estoque_minimo = dr.GetOrdinal("estoque_minimo");
            Int32 estoque_maximo = dr.GetOrdinal("estoque_maximo");
            Int32 grupo_produto = dr.GetOrdinal("grupo_produto");
            Int32 unidade_medida = dr.GetOrdinal("unidade_medida");


            while (dr.Read())
            {
                item.id = dr.GetInt32(id);

                if (!dr.IsDBNull(fornecedor))
                    item.fornecedor = dr.GetInt32(fornecedor);

                if (!dr.IsDBNull(xnome))
                    item.nome_produto = dr.GetString(xnome);
                else
                    item.nome_produto = "";

                item.data_cadastro = dr.GetDateTime(data_cadastro).ToShortDateString();
                if (!dr.IsDBNull(ean))
                    item.ean = dr.GetString(ean);

                if (!dr.IsDBNull(grupo_produto))
                    item.grupo_produto = dr.GetInt32(grupo_produto);

                if (!dr.IsDBNull(unidade_medida))
                    item.unidade_medida = dr.GetInt32(unidade_medida);

                item.valor_compra = dr.GetDouble(valor_compra);
                item.valor_venda = dr.GetDouble(valor_venda);
                item.valor_venda_atacado = dr.GetDouble(valor_venda_atacado);
                item.valor_venda_margem = dr.GetDouble(valor_venda_margem);
                item.valor_venda_atacado_margem = dr.GetDouble(valor_venda_atacado_margem);
                item.cfop = dr.GetInt32(cfop);
                item.ncm = dr.GetString(ncm);
                item.estoque = dr.GetDouble(estoque);
                item.estoque_minimo = dr.GetInt32(estoque_minimo);
                item.estoque_maximo = dr.GetInt32(estoque_maximo);
                if (!dr.IsDBNull(codigo_balanca))
                    item.codigo_balanca = dr.GetInt32(codigo_balanca).ToString();
            }

            sql.FechaConexao();
            return item;
        }

        public DataTable seleciona()
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "select * from produto";
            DataTable dt = sql.RetornaDados_v2_dt();
            sql.FechaConexao();
            return dt;
        }

        public DataTable seleciona_correcao(Boolean sem_categoria)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            String x = "select * from produto where 1=1 ";

            if (sem_categoria)
                x += "and grupo_produto = 0 ";

            sql.Comando.CommandText = x;
            DataTable dt = sql.RetornaDados_v2_dt();
            sql.FechaConexao();
            return dt;
        }

        public DataTable seleciona_listagem(Entidade_Produto item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();

            StringBuilder sb = new StringBuilder();
            sb.Append("select id,data_cadastro,nome,estoque,estoque_minimo,valor_compra,valor_venda,(estoque * valor_venda) as valor_total,ean,ncm,cfop from produto where status = true ");

            if (item.estoque_abaixo_minimo > 0)
                sb.Append(" and estoque < estoque_minimo ");

            if (item.estoque_zerado > 0)
                sb.Append(" and estoque <= 0 ");

            if (item.nome_produto != "" && item.nome_produto != null)
            {
                sb.Append(" and nome ilike '" + item.nome_produto + "%' ");
                sql.Comando.Parameters.AddWithValue("@nome", item.nome_produto);
            }

            if (item.grupo_produto > 0)
                sb.Append(" and grupo_produto = " + item.grupo_produto + " ");

            if (item.search != "" && item.search != null)
            {
                sb.Append(" and nome ilike '" + item.search + "%' or ean ilike '" + item.search + "%' ");
                //                sql.Comando.Parameters.AddWithValue("@nome", item.nome_produto);
            }

            sb.Append("  order by nome asc");
            sql.Comando.CommandText = sb.ToString();
            DataTable dt = sql.RetornaDados_v2_dt();
            sql.FechaConexao();
            return dt;
        }

        public DataTable seleciona_listagem_lite(Entidade_Produto item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();

            StringBuilder sb = new StringBuilder();
            sb.Append("select id,data_cadastro,nome,estoque,estoque_minimo,valor_compra,valor_venda,(estoque * valor_venda) as valor_total,ean,ncm,cfop from produto where status = true ");

            if (item.estoque_abaixo_minimo > 0)
                sb.Append(" and estoque < estoque_minimo ");

            if (item.estoque_zerado > 0)
                sb.Append(" and estoque <= 0 ");

            if (item.nome_produto != "" && item.nome_produto != null)
            {
                sb.Append(" and nome ilike '" + item.nome_produto + "%' ");
                sql.Comando.Parameters.AddWithValue("@nome", item.nome_produto);
            }

            if (item.unidade_medida > 0)
            {
                sb.Append(" and unidade_medida = 2 ");
            }

            if (item.grupo_produto > 0)
                sb.Append(" and grupo_produto = " + item.grupo_produto + " ");

            if (item.search != "" && item.search != null)
            {
                sb.Append(" and nome ilike '" + item.search + "%' or ean ilike '" + item.search + "%' ");
                //                sql.Comando.Parameters.AddWithValue("@nome", item.nome_produto);
            }

            sb.Append("  order by nome asc limit 50");
            sql.Comando.CommandText = sb.ToString();
            DataTable dt = sql.RetornaDados_v2_dt();
            sql.FechaConexao();
            return dt;
        }


        public void salva(Entidade_Produto item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.Comando = new Npgsql.NpgsqlCommand();

            StringBuilder sb = new StringBuilder();

            if (item.id > 0)
            {
                sb.AppendLine("update produto set codigo_balanca = @codigo_balanca,  unidade_medida = @unidade_medida, fornecedor=@fornecedor,nome=@nome,ean=@ean,valor_compra=@valor_compra,valor_venda=@valor_venda,grupo_produto = @grupo_produto");
                sb.AppendLine(",valor_atacado=@valor_atacado,margem=@margem,margem_atacado=@margem_atacado,cfop=@cfop,ncm=@ncm,estoque=@estoque,estoque_minimo=@estoque_minimo");
                sb.AppendLine(",estoque_maximo=@estoque_maximo where id = @id");

                sql.Comando.Parameters.AddWithValue("@id", item.id);

                sql.Comando.Parameters.AddWithValue("@codigo_balanca", NpgsqlTypes.NpgsqlDbType.Integer, item.codigo_balanca);
                sql.Comando.Parameters.AddWithValue("@unidade_medida", item.unidade_medida);
                sql.Comando.Parameters.AddWithValue("@fornecedor", item.fornecedor);
                sql.Comando.Parameters.AddWithValue("@grupo_produto", item.grupo_produto);
                sql.Comando.Parameters.AddWithValue("@nome", item.nome_produto);
                sql.Comando.Parameters.AddWithValue("@ean", item.ean);
                sql.Comando.Parameters.AddWithValue("@valor_compra", item.valor_compra);
                sql.Comando.Parameters.AddWithValue("@valor_venda", item.valor_venda);
                sql.Comando.Parameters.AddWithValue("@valor_atacado", item.valor_venda_atacado);
                sql.Comando.Parameters.AddWithValue("@margem", item.valor_venda_margem);
                sql.Comando.Parameters.AddWithValue("@margem_atacado", item.valor_venda_atacado_margem);
                sql.Comando.Parameters.AddWithValue("@cfop", item.cfop);
                sql.Comando.Parameters.AddWithValue("@ncm", item.ncm);
                sql.Comando.Parameters.AddWithValue("@estoque", item.estoque);
                sql.Comando.Parameters.AddWithValue("@estoque_minimo", item.estoque_minimo);
                sql.Comando.Parameters.AddWithValue("@estoque_maximo", item.estoque_maximo);



            }
            else
            {
                //Salva

                //INSERT INTO produto(
                //        id, nome, empresa, fornecedor, ean, tipo_produto, valor_compra, 
                //        venda, status, ncm, cfop, estoque, estoque_minimo, estoque_maximo, 
                //        sat_cfop, sat_ncm)
                //VALUES (?, ?, ?, ?, ?, ?, ?, 
                //        ?, ?, ?, ?, ?, ?, ?, 
                //        ?, ?);

                sb.AppendLine("INSERT INTO produto(codigo_balanca,grupo_produto,unidade_medida,fornecedor,nome,ean,valor_compra,valor_venda,valor_atacado,margem,margem_atacado,cfop,ncm,estoque,estoque_minimo,estoque_maximo)");
                sb.AppendLine("VALUES (@codigo_balanca, @grupo_produto,@unidade_medida,@fornecedor,@nome,@ean,@valor_compra,@valor_venda,@valor_atacado,@margem,@margem_atacado,@cfop,@ncm,@estoque,@estoque_minimo,@estoque_maximo)");

                sql.Comando.Parameters.AddWithValue("@grupo_produto", item.grupo_produto);
                sql.Comando.Parameters.AddWithValue("@codigo_balanca", NpgsqlTypes.NpgsqlDbType.Integer, item.codigo_balanca);
                sql.Comando.Parameters.AddWithValue("@unidade_medida", item.unidade_medida);
                sql.Comando.Parameters.AddWithValue("@fornecedor", item.fornecedor);
                sql.Comando.Parameters.AddWithValue("@nome", item.nome_produto);
                sql.Comando.Parameters.AddWithValue("@ean", item.ean);
                sql.Comando.Parameters.AddWithValue("@valor_compra", item.valor_compra);
                sql.Comando.Parameters.AddWithValue("@valor_venda", item.valor_venda);
                sql.Comando.Parameters.AddWithValue("@valor_atacado", item.valor_venda_atacado);
                sql.Comando.Parameters.AddWithValue("@margem", item.valor_venda_margem);
                sql.Comando.Parameters.AddWithValue("@margem_atacado", item.valor_venda_atacado_margem);
                sql.Comando.Parameters.AddWithValue("@cfop", item.cfop);
                sql.Comando.Parameters.AddWithValue("@ncm", item.ncm);
                sql.Comando.Parameters.AddWithValue("@estoque", item.estoque);
                sql.Comando.Parameters.AddWithValue("@estoque_minimo", item.estoque_minimo);
                sql.Comando.Parameters.AddWithValue("@estoque_maximo", item.estoque_maximo);

            }

            sql.Comando.CommandText = sb.ToString();
            sql.AbrirConexao();
            sql.ExecutaComando_v2();
            sql.FechaConexao();
        }


        public void apaga(Int32 id)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.Comando = new Npgsql.NpgsqlCommand();


            sql.Comando.Parameters.AddWithValue("@id", id);

            sql.Comando.CommandText = "update produto set status = false where id = @id ";
            sql.AbrirConexao();
            sql.ExecutaComando_v2();
            sql.FechaConexao();
        }


        public Entidade_Produto seleciona_totais_estoque(Entidade_Produto item)
        {

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            StringBuilder sb = new StringBuilder();

            sb.Append("select sum(estoque),sum(valor_venda * estoque) from produto where status = true ");

            if (item.estoque_abaixo_minimo > 0)
                sb.Append(" and estoque < estoque_minimo ");

            if (item.estoque_zerado > 0)
                sb.Append(" and estoque <= 0 ");

            if (item.nome_produto != "" && item.nome_produto != null)
            {
                sb.Append(" and nome ilike '" + item.nome_produto + "%' ");
                sql.Comando.Parameters.AddWithValue("@nome", item.nome_produto);
            }

            if (item.grupo_produto > 0)
                sb.Append(" and grupo_produto = " + item.grupo_produto + " ");

            if (item.search != "" && item.search != null)
            {
                sb.Append(" and nome ilike '" + item.search + "%' or ean ilike '" + item.search + "%' ");
                //                sql.Comando.Parameters.AddWithValue("@nome", item.nome_produto);
            }

            sql.Comando.CommandText = sb.ToString();
            IDataReader dr = sql.RetornaDados_v2();

            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                    item.estoque = dr.GetDouble(0);
                if (!dr.IsDBNull(0))
                    item.valor_venda = dr.GetDouble(1);
            }

            sql.FechaConexao();
            return item;
        }

        public void acerta_estoque(Entidade_Produto item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.Comando = new Npgsql.NpgsqlCommand();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("update produto set estoque=@estoque where id = @id");

            sql.Comando.Parameters.AddWithValue("@id", item.id);
            sql.Comando.Parameters.AddWithValue("@estoque", item.estoque);

            sql.Comando.CommandText = sb.ToString();
            sql.AbrirConexao();
            sql.ExecutaComando_v2();
            sql.FechaConexao();
        }


    }
}
