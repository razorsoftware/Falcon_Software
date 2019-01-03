using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.tabelas_sistema
{
    public class Grupo_produto
    {
        public class Entidade
        {
            public Int32 id { get; set; }
            public status status { get; set; }
            public String descricao { get; set; }
        }

        public enum status
        {
            ativo = 1,
            inativo = 0
        }


        public DataTable seleciona_grid(String search)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "select id,descricao from grupo_produto where status = "+status.ativo.GetHashCode()+" order by descricao asc";
            sql.AbrirConexao();
            DataTable dt = sql.RetornaDados_v2_dt();
            sql.FechaConexao();
            return dt;
        }

        public Entidade seleciona(Entidade item)
        {
            List<Entidade> list = new List<Entidade>();

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            sql.Comando = new Npgsql.NpgsqlCommand();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select * from grupo_produto where status = @status ");

            if (item.id > 0)
            {
                sb.AppendLine("and id = @id ");
                sql.Comando.Parameters.AddWithValue("@id", item.id);
            }

            sql.Comando.CommandText = sb.ToString();
            sql.AbrirConexao();
            System.Data.IDataReader dr = sql.RetornaDados_v2();

            Int32 id = dr.GetOrdinal("id");
            Int32 descricao = dr.GetOrdinal("descricao");

            while (dr.Read())
            {
                item.id = dr.GetInt32(id);
                item.descricao = dr.GetString(descricao);
            }

            sql.FechaConexao();
            return item;
        }

        public void salva(Entidade item)
        {

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            StringBuilder sb = new StringBuilder();

            if (item.id == 0)
            {
                sb.AppendLine("INSERT INTO grupo_produto (status,descricao)");
                sb.AppendLine("VALUES (@status,@descricao);");

                sql.Comando = new Npgsql.NpgsqlCommand();
                sql.Comando.Parameters.AddWithValue("@status", NpgsqlTypes.NpgsqlDbType.Integer, status.ativo.GetHashCode());
                sql.Comando.Parameters.AddWithValue("@descricao", NpgsqlTypes.NpgsqlDbType.Varchar, item.descricao);
               
                sql.AbrirConexao();
                sql.Comando.CommandText = sb.ToString();
                sql.ExecutaComando_v2();
                sql.FechaConexao();
            }
            else
            {
                sb.AppendLine("update grupo_produto set descricao = @descricao ");
                sb.AppendLine(" where id = @id;");

                sql.Comando = new Npgsql.NpgsqlCommand();
                sql.Comando.Parameters.AddWithValue("@descricao", NpgsqlTypes.NpgsqlDbType.Varchar, item.descricao);
                sql.Comando.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Integer, item.id);

                sql.AbrirConexao();
                sql.Comando.CommandText = sb.ToString();
                sql.ExecutaComando_v2();
                sql.FechaConexao();
            }


        }

        public void delete(Entidade item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("update grupo_produto set status =@status ");
            sb.AppendLine(" where id = @id;");

            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.Parameters.AddWithValue("@status", NpgsqlTypes.NpgsqlDbType.Integer, status.inativo.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Integer, item.id);

            sql.AbrirConexao();
            sql.Comando.CommandText = sb.ToString();
            sql.ExecutaComando_v2();
            sql.FechaConexao();
        }

    }
}
