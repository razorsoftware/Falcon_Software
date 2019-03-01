using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.Caixa
{
    public enum enum_caixa_configuracao
    {
        manual = 1,
        ler_ean13 = 2
    }
    
    public class Configuracao{

        public class Entidade{
            public enum_caixa_configuracao configuracao_balanca { get; set; }
            public Int32 numero_caracteres_peso { get; set; }
            public Boolean exibir_balanca_pdv { get; set; }
        }

        public void atualiza(Entidade item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("update configuracao_caixa set configuracao_balanca = @configuracao_balanca,exibir_balanca_pdv = @exibir_balanca_pdv, quantidade_caracteres_peso = @qtd_caracteres_peso  ");
            sql.Comando.Parameters.AddWithValue("@configuracao_balanca", NpgsqlTypes.NpgsqlDbType.Integer, item.configuracao_balanca.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@exibir_balanca_pdv", NpgsqlTypes.NpgsqlDbType.Boolean, item.exibir_balanca_pdv);
            sql.Comando.Parameters.AddWithValue("@qtd_caracteres_peso", NpgsqlTypes.NpgsqlDbType.Integer, item.numero_caracteres_peso);
            
            sql.Comando.CommandText = sb.ToString();
            sql.ExecutaComando_v2();
            sql.FechaConexao();
        }

        public Entidade seleciona()
        {
            Entidade item = new Entidade();
            data.bd_postgres sql = new data.bd_postgres();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando.CommandText = "select * from configuracao_caixa ";
            IDataReader dr = sql.RetornaDados_v2();
            while(dr.Read()){
                item.configuracao_balanca = (enum_caixa_configuracao)dr.GetInt32(0);
                item.exibir_balanca_pdv = dr.GetBoolean(1);
                item.numero_caracteres_peso = dr.GetInt32(2);
            }
            sql.FechaConexao();
            return item;
        }

    }
}
