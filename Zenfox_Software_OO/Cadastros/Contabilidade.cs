using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.Cadastros
{
    public class Contabilidade
    {

        public class Entidade
        {
            public Int32 id { get; set; }
            public String nome { get; set; }
            public String email { get; set; }
        }


        public Entidade seleciona(Entidade item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            sql.Comando = new Npgsql.NpgsqlCommand();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select * from contabilidade ");
            
            sql.Comando.CommandText = sb.ToString();
            sql.AbrirConexao();
            IDataReader dr = sql.RetornaDados_v2();

            Int32 id = dr.GetOrdinal("id");
            Int32 nome = dr.GetOrdinal("nome");
            Int32 email = dr.GetOrdinal("email");

            while (dr.Read())
            {
                if(!dr.IsDBNull(id))
                item.id = dr.GetInt32(id);
                if (!dr.IsDBNull(nome))
                    item.nome = dr.GetString(nome);
                if (!dr.IsDBNull(email))
                    item.email = dr.GetString(email);
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
                sb.AppendLine("INSERT INTO contabilidade (nome,email)");
                sb.AppendLine("VALUES (@nome,@email); ");

                sql.Comando = new Npgsql.NpgsqlCommand();

                sql.Comando.Parameters.AddWithValue("@nome", NpgsqlTypes.NpgsqlDbType.Varchar, item.nome);
                sql.Comando.Parameters.AddWithValue("@email", NpgsqlTypes.NpgsqlDbType.Varchar, item.email);
                
                sql.AbrirConexao();
                sql.Comando.CommandText = sb.ToString();
                sql.ExecutaComando_v2();
                sql.FechaConexao();
            }
            else
            {
                sb.AppendLine("update contabilidade set nome=@nome,email=@email ");
                sb.AppendLine("where id = @id");

                sql.Comando = new Npgsql.NpgsqlCommand();

                sql.Comando.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Integer, item.id);
                sql.Comando.Parameters.AddWithValue("@nome", NpgsqlTypes.NpgsqlDbType.Varchar, item.nome);
                sql.Comando.Parameters.AddWithValue("@cpf", NpgsqlTypes.NpgsqlDbType.Varchar, item.email);
                
                sql.AbrirConexao();
                sql.Comando.CommandText = sb.ToString();
                sql.ExecutaComando_v2();
                sql.FechaConexao();
            }


        }


        // Verifica se o mês da pesquisa ja foi feito o envio de arquivos para a contabilidade
        public static Boolean verifica_mes_envio_contabilidade(String mes)
        {
            Boolean x = true;
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            sql.Comando = new Npgsql.NpgsqlCommand();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select * from contabilidade_enviados where mes = '"+mes+"' ");

            sql.Comando.CommandText = sb.ToString();
            sql.AbrirConexao();
            IDataReader dr = sql.RetornaDados_v2();
            
            while (dr.Read()){
                x = false;
            }

            sql.FechaConexao();
            return x;
        }
    }
}
