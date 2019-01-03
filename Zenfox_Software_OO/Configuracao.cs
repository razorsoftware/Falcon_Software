using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO
{
    public class Configuracao
    {

        public static String seleciona_impressora()
        {
            String impressora = "";

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            sql.Comando = new Npgsql.NpgsqlCommand();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select impressora from configuracao ");
            
            sql.Comando.CommandText = sb.ToString();
            sql.AbrirConexao();
            IDataReader dr = sql.RetornaDados_v2();
            
            while (dr.Read()){
                impressora = dr.GetString(0);
            }

            sql.FechaConexao();
            return impressora;

        }

    }
}
