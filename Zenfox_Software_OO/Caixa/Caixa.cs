using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.Caixa
{

    public class Entidade_Caixa
    {

        public Int32 id { get; set; }
        public Int32 usuario { get; set; }
        public String descricao { get; set; }
        public String data_abertura { get; set; }
        public String data_fechamento { get; set; }
        public Double xpledge { get; set; }
        public Double valor_abertura { get; set; }
          
    }

    public class Entidade_Caixa_pagamento
    {
        
        public forma_pagamento forma_pagamento { get; set; }
    
    }

    public enum status
    {
        aberto = 0,
        fechado = 1
    }

    public enum forma_pagamento{
        dinheiro = 1,
        cheque = 2,
        cartao_credito = 3,
        cartao_debito = 4
    }

    public enum tipo_operacao
    {
        acrescentar_caixa = 0,
        retirada_caixa = 1
    }

    public class Caixa
    {

        public Boolean verifica_caixa_aberto(Entidade_Caixa item){
            Boolean x = false;

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "select * from caixa where usuario = " + item.usuario + " and data_fechamento is null";
            IDataReader dr = sql.RetornaDados_v2();

            Int32 id = dr.GetOrdinal("id");

            while (dr.Read()){
                item.id = dr.GetInt32(id);
                x = true;
            }

            sql.FechaConexao();
            return x;
            //return item;

        }

        public Entidade_Caixa seleciona(Entidade_Caixa item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            StringBuilder sb = new StringBuilder();
            Int32 id = 0;

            sb.AppendLine("select * from caixa where status = " + Zenfox_Software_OO.Caixa.status.aberto.GetHashCode());

            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = sb.ToString();
            IDataReader dr = sql.RetornaDados_v2();
            while(dr.Read()){
                item.valor_abertura = dr.GetDouble(dr.GetOrdinal("valor_abertura"));
            }

            sql.FechaConexao();
            return item;
        }


        public void abrir_caixa(Entidade_Caixa item)
        {
            Boolean x = false;

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "INSERT INTO caixa(status,usuario, data_abertura, valor_abertura) VALUES (@status,@usuario,current_timestamp, @valor_abertura);";
            sql.Comando.Parameters.AddWithValue("@status", status.aberto.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@usuario",item.usuario);
            sql.Comando.Parameters.AddWithValue("@valor_abertura", item.valor_abertura);
            sql.ExecutaComando_v2();

            sql.FechaConexao();

            //return item;

        }

        public void retirar_dinheiro_caixa(Entidade_Caixa item)
        {

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "INSERT INTO caixa_operacoes(tipo_operacao,caixa,descricao,valor) VALUES (@tipo_operacao,(select id from caixa where status = @status_caixa),@descricao,@valor);";
            sql.Comando.Parameters.AddWithValue("@tipo_operacao", tipo_operacao.retirada_caixa.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@descricao", item.descricao);
            sql.Comando.Parameters.AddWithValue("@valor", item.valor_abertura);
            sql.Comando.Parameters.AddWithValue("@status_caixa",status.aberto.GetHashCode());
            sql.ExecutaComando_v2();
            sql.FechaConexao();

        }

        public void adiciona_dinheiro_caixa(Entidade_Caixa item)
        {

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "INSERT INTO caixa_operacoes(tipo_operacao,caixa,descricao,valor) VALUES (@tipo_operacao,(select id from caixa where status = @status_caixa),@descricao,@valor);";
            sql.Comando.Parameters.AddWithValue("@tipo_operacao", tipo_operacao.acrescentar_caixa.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@descricao", item.descricao);
            sql.Comando.Parameters.AddWithValue("@valor", item.valor_abertura);
            sql.Comando.Parameters.AddWithValue("@status_caixa", status.aberto.GetHashCode());
            sql.ExecutaComando_v2();
            sql.FechaConexao();

        }


        public static Double seleciona_retirada_caixa()
        {
            Double x = 0;
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "select sum(valor) from caixa_operacoes where tipo_operacao = @tipo_operacao and caixa = (select id from caixa where status = @status_caixa)";
            sql.Comando.Parameters.AddWithValue("@status_caixa", status.aberto.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@tipo_operacao", tipo_operacao.retirada_caixa.GetHashCode());
            
            IDataReader dr = sql.RetornaDados_v2();
            while (dr.Read())
            {
                if(!dr.IsDBNull(0))
                x = dr.GetDouble(0);
            }
            sql.FechaConexao();
            return x;
        }

        public static Double seleciona_acrescento_caixa(){

            Double x = 0;
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "select sum(valor) from caixa_operacoes where tipo_operacao = @tipo_operacao and caixa = (select id from caixa where status = @status_caixa)";
            sql.Comando.Parameters.AddWithValue("@status_caixa", status.aberto.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@tipo_operacao", tipo_operacao.acrescentar_caixa.GetHashCode());

            IDataReader dr = sql.RetornaDados_v2();
            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                    x = dr.GetDouble(0);
            }
            sql.FechaConexao();
            return x;
        }


        public void fechar_caixa(Entidade_Caixa item)
        {
            Boolean x = false;

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "update caixa set status = @status, data_fechamento = current_timestamp where status = @status_where";
            sql.Comando.Parameters.AddWithValue("@status", status.fechado.GetHashCode());
            sql.Comando.Parameters.AddWithValue("@status_where", status.aberto.GetHashCode());
            sql.ExecutaComando_v2();

            sql.FechaConexao();

            //return item;

        }

        public static Int32 seleciona_id_caixa()
        {
            data.bd_postgres sql = new data.bd_postgres();
            StringBuilder sb = new StringBuilder();
            Int32 id = 0;

            sb.AppendLine("select id from caixa where status = " + Zenfox_Software_OO.Caixa.status.aberto.GetHashCode());

            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = sb.ToString();
            DataTable dr = sql.RetornaDados_v2_dt();

            if (dr.Rows.Count > 0)
            {
                id =  Int32.Parse(dr.Rows[0].ItemArray[0].ToString());
            }

            sql.FechaConexao();
            return id;
        }
    }
}
