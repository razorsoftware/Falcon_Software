using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.Cadastros
{

    public class Entidade_Configuracao
    {
        public Boolean valida_ncm { get; set; }
        public Boolean sat { get; set; }
        public Boolean impressora_pula_linha { get; set; }
        public String acbr { get; set; }
    }

    public class Configuracao{


        public Configuracao()
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "select * from configuracao";
            IDataReader dr = sql.RetornaDados_v2();

            Boolean x = true;
            while (dr.Read())
                x = false;

            if (x)
            {
                sql.FechaConexao();
                sql.Comando.CommandText = "insert into configuracao(validar_ncm,sat) values(true,false)";
                sql.AbrirConexao();
                sql.ExecutaComando_v2();
                sql.FechaConexao();
            }
        }

        public Entidade_Configuracao seleciona(Entidade_Configuracao item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "select * from configuracao";
            IDataReader dr = sql.RetornaDados_v2();

            Int32 validar_ncm = dr.GetOrdinal("validar_ncm");
            Int32 sat = dr.GetOrdinal("sat");
            Int32 impressora_pula_impressao = dr.GetOrdinal("impressora_pula_impressao");
            Int32 acbr = dr.GetOrdinal("acbr");

            while (dr.Read())
            {
                item.valida_ncm = dr.GetBoolean(validar_ncm);
                if(!dr.IsDBNull(sat))
                    item.sat = dr.GetBoolean(sat);
                if(!dr.IsDBNull(impressora_pula_impressao))
                    item.impressora_pula_linha = dr.GetBoolean(impressora_pula_impressao);
                if (!dr.IsDBNull(acbr))
                    item.acbr = dr.GetString(acbr);
            }

            sql.FechaConexao();
            return item;
        }

        public void atualiza(Entidade_Configuracao item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "update configuracao set acbr= '"+item.acbr+"',validar_ncm = " + item.valida_ncm + " ,sat = "+ item.sat + ", impressora_pula_impressao = " + item.impressora_pula_linha;
            sql.ExecutaComando_v2();
            sql.FechaConexao();
        }

    }
}
