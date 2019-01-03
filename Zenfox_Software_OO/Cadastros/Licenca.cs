using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.Cadastros
{

    public class Licenca
    {

        public class Entidade
        {
            public Int32 id { get; set; }
            public String expiracao_licenca { get; set; }
            public String mac_addres { get; set; }
        }

        public List<Entidade> seleciona(Entidade item)
        {
            List<Entidade> list = new List<Entidade>();
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select * from razor_licencas where 1 = 1 ");
          //  if(item.mac_addres != "" && item.mac_addres != null)
          //  {
          //      sb.AppendLine("and mac_addres = @mac");
          //      sql.Comando.Parameters.AddWithValue("@mac",item.mac_addres);
          //  }
            sql.Comando.CommandText = sb.ToString();
            IDataReader dr = sql.RetornaDados_v2();

            Int32 expiracao_licenca = dr.GetOrdinal("expiracao_licenca");

            while (dr.Read())
            {
                Entidade _item = new Entidade();
                _item.expiracao_licenca = dr.GetDateTime(expiracao_licenca).ToShortDateString();
                list.Add(_item);
            }

            sql.FechaConexao();
            return list;
        }

        public void atualiza_licenca(String data)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();           
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "update razor_licencas set expiracao_licenca = '"+data+"'";
            sql.AbrirConexao();
            sql.ExecutaComando_v2();
            sql.FechaConexao();
        }


    }

}
