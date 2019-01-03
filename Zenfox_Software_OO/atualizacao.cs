using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO
{
    public class atualizacao
    {
        public class Entidade
        {
            public Int32 id { get; set; }
            public String versao { get; set; }
            public Byte[] arquivo { get; set; }
            public String mac_addres { get; set; }
        }

        public Entidade seleciona()
        {
            Entidade item = new Entidade();
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select * from atualizacao ");

            sql.Comando.CommandText = sb.ToString();
            IDataReader dr = sql.RetornaDados_v2();

            Int32 versao = dr.GetOrdinal("versao");

            while (dr.Read())
            {
                item.versao = dr.GetString(versao);
            }

            sql.FechaConexao();
            return item;
        }

        public Entidade pega_ultima_atualizacao()
        {
            Entidade item = new Entidade();
            data.bd_postgres sql = new data.bd_postgres();
            sql.onlinedb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select * from atualizacao order by data_versao desc limit 1");

            sql.Comando.CommandText = sb.ToString();
            IDataReader dr = sql.RetornaDados_v2();

            Int32 codigo_versao = dr.GetOrdinal("codigo_versao");
            Int32 arquivo = dr.GetOrdinal("arquivo");

            while (dr.Read())
            {
                item.versao = dr.GetString(codigo_versao);
                byte[] myBytes = new byte[10000000];
                long bytesRead = dr.GetBytes(arquivo, 0, myBytes, 0, 10000000);
                item.arquivo = myBytes;
                //  FileStream fs = new FileStream("C:\\rede_sistema\\razor.rar", FileMode.Create);
                //  fs.Write(myBytes, 0, (int)bytesRead);
                //  fs.Close();
            }

            sql.FechaConexao();
            return item;
        }

        public void insere_atualizacao(Entidade item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("delete from atualizacao; insert into atualizacao(versao,arquivo) values('"+item.versao+ "',:bytesData);");
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = sb.ToString();

            sql.AbrirConexao();
            sql.insereBytea(sb.ToString(),item.arquivo);
            sql.FechaConexao();

        }

        public List<Entidade> verifica_existencia_nova_atualizacao(Entidade item)
        {

            List<Entidade> list = new List<Entidade>();
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select a.id,b.arquivo from razor_licencas as a ");
            sb.AppendLine("inner join atualizacao as b on a.versao != b.versao ");
            sb.AppendLine("where a.mac_addres = @mac_addres");

            sql.Comando.Parameters.AddWithValue("@mac_addres",item.mac_addres);

            sql.Comando.CommandText = sb.ToString();
            IDataReader dr = sql.RetornaDados_v2();
                        
            while (dr.Read())
            {
                Entidade _item = new Entidade();
                _item.id = dr.GetInt32(0);
                byte[] myBytes = new byte[10000000];
                long bytesRead = dr.GetBytes(1, 0, myBytes, 0, 10000000);
                _item.arquivo = myBytes;
                //    _item.expiracao_licenca = dr.GetDateTime(expiracao_licenca).ToShortDateString();
                list.Add(_item);
            }

            sql.FechaConexao();
            return list;

        }




    }
}
