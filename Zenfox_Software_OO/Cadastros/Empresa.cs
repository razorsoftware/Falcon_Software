using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.Cadastros
{
    public class Empresa
    {
        public class Entidade
        {
            public String fantasia { get; set; }
            public String cnpj{get;set;}
            public String ie { get; set; }
            public String versao_cupom { get; set; }
            public Double aliquota_icms { get; set; }
            public String codigo_vinculacao { get; set; }
            public String software_house { get; set; }
        }      

        public Boolean verifica_primeiro_acesso()
        {

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();

            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "select * from empresa";

            DataTable dt = sql.RetornaDados_v2_dt();
            // sql.ExecutaComando_v2()
            // DataTable dt = sql.selectQuery("select * from empresa");
            sql.FechaConexao();

            if (dt.Rows.Count > 0)
                return false; //Não é o primeiro acesso
            else
                return true; // Sim, é o primeiro acesso
        }

        public Entidade seleciona()
        {
            Entidade item = new Entidade();
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();

            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "select * from empresa";

            IDataReader dr = sql.RetornaDados_v2();
            Int32 fantasia = dr.GetOrdinal("fantasia");
            Int32 cpf_cnpj = dr.GetOrdinal("cpf_cnpj");
            Int32 versao_cupom = dr.GetOrdinal("versao_cupom");
            Int32 ie = dr.GetOrdinal("ie");
            Int32 aliquota_icms = dr.GetOrdinal("aliquota_icms");
            Int32 codigo_vinculacao = dr.GetOrdinal("codigo_vinculacao");
            Int32 software_house = dr.GetOrdinal("software_house");

            while (dr.Read())
            {
                item.fantasia = dr.GetString(fantasia);
                item.cnpj = dr.GetString(cpf_cnpj);
                item.versao_cupom = dr.GetString(versao_cupom);
                item.ie = dr.GetString(ie);
                item.aliquota_icms = dr.GetDouble(aliquota_icms);
                item.codigo_vinculacao = dr.GetString(codigo_vinculacao);
                item.software_house = dr.GetString(software_house);
            }

            // sql.ExecutaComando_v2()
            // DataTable dt = sql.selectQuery("select * from empresa");
            sql.FechaConexao();
            return item;            
        }

        public void vincula_empresa(Int32 id,String cpf_cnpj)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO empresa (id,cpf_cnpj)");
            sb.AppendLine("VALUES (" + id + ",'"+cpf_cnpj+"');");

            sql.AbrirConexao();
            sql.ExecutaComando(sb.ToString());
            sql.FechaConexao();


        }

    }
}
