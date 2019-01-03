using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.Cadastros
{

    public class Entidade_Usuario
    {

        public Int32 id { get; set; }
        public Boolean adm { get; set; }
        public String nome { get; set; }
        public String usuario { get; set; }
        public String senha { get; set; }
        public Boolean ativo { get; set; }

    }

    public class Usuario
    {

        public void insere_usuario(Entidade_Usuario item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            StringBuilder sb = new StringBuilder();

            if (item.id == 0)
            {

                sb.AppendLine("INSERT INTO usuario (adm,nome,usuario,senha,ativo)");
                sb.AppendLine("VALUES (" + item.adm + ",'" + item.nome + "','" + item.usuario + "','" + item.senha + "',true);");

            }
            else
            {

            }

            sql.AbrirConexao();
            sql.ExecutaComando(sb.ToString());
            sql.FechaConexao();


        }


        public Int32 autenticacao(Entidade_Usuario item)
        {
            Int32 id = 0;

            data.bd_postgres sql = new data.bd_postgres();

            StringBuilder sb = new StringBuilder();

            if (item.id == 0)
            {

                sb.AppendLine("select id from usuario where usuario = '" + item.usuario + "' and senha = '" + item.senha + "'");

                sql.localdb();
                sql.AbrirConexao();
                sql.Comando = new Npgsql.NpgsqlCommand();
                sql.Comando.CommandText = sb.ToString();
                DataTable dr = sql.RetornaDados_v2_dt();

                if (dr.Rows.Count > 0)
                {
                    id = Int32.Parse(dr.Rows[0].ItemArray[0].ToString());
                }
                sql.FechaConexao();
            }
            else
            {

            }

            // sql.abrir_conexao();
            // sql.selectQuery(sb.ToString());
            

            return id;
        }

        public static String seleciona_nome(Entidade_Usuario item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            StringBuilder sb = new StringBuilder();
            String nome = "";

            sb.AppendLine("select nome from usuario where id = " + item.id);

            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = sb.ToString();
            DataTable dr = sql.RetornaDados_v2_dt();

            if (dr.Rows.Count > 0)
            {
                nome = dr.Rows[0].ItemArray[0].ToString();
            }
            sql.FechaConexao();
            return nome;
        }

    }
}
