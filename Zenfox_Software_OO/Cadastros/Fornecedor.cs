using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.Cadastros
{
    public class Entidade_Fornecedor
    {

        public Int32 id { get; set; }
        public Boolean status { get; set; }
        public tipo tipo_pessoa { get; set; }
        public Int32 id_nuvem { get; set; }
        public String razao_social { get; set; }
        public String fantasia { get; set; }
        public String cpf_cnpj { get; set; }
        public String ie { get; set; }
        public String endereco { get; set; }
        public String n { get; set; }
        public String complemento { get; set; }
        public Int32 estado { get; set; }
        public Int32 cidade { get; set; }
        public String cep { get; set; }
        public String bairro { get; set; }
        public String telefone { get; set; }
        public String representante { get; set; }
        public String email { get; set; }
        public String observacao { get; set; }
        
    }

    public enum tipo
    {
        pessoa_fisica = 0,
        pessoa_juridica = 1
    }

    public class Fornecedor
    {

        public void salva(Entidade_Fornecedor item){
            
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            StringBuilder sb = new StringBuilder();

            if (item.id == 0)
            {
                sb.AppendLine("INSERT INTO fornecedor (status,tipo_pessoa,razao_social,fantasia_nome,cpf_cnpj,ie,endereco,estado,cidade,cep,bairro,telefone,representante,email,numero,complemento,observacao)");
                sb.AppendLine("VALUES (true," + item.tipo_pessoa.GetHashCode() + ",'" + item.razao_social + "','" + item.fantasia + "','" + item.cpf_cnpj + "','" + item.ie + "','"+item.endereco+"',"+item.estado+","+item.cidade+",'"+item.cep+"','"+item.bairro+"','','','','"+item.n+"','"+item.complemento+"','');");
            }else{                
                sb.AppendLine("update fornecedor set tipo_pessoa = "+item.tipo_pessoa.GetHashCode()+", fantasia_nome = '" + item.fantasia +"' ");
                sb.AppendLine(",razao_social='"+item.razao_social+"',cpf_cnpj='"+item.cpf_cnpj+"',ie='"+item.ie+"', ");
                sb.AppendLine("endereco='" + item.endereco +"',bairro='"+item.endereco+"',numero='"+item.n+"',complemento='"+item.complemento+"',cep = '"+item.cep+"',estado="+item.estado+",cidade="+item.cidade+" ");
                sb.AppendLine("where id = " + item.id);
            }
            sql.AbrirConexao();
            sql.ExecutaComando(sb.ToString());
            sql.FechaConexao();
            
        }

        public Entidade_Fornecedor seleciona(Entidade_Fornecedor item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            sql.Comando = new Npgsql.NpgsqlCommand();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select * from fornecedor where status = true ");

            if (item.id > 0){
                sb.AppendLine("and id = @id ");
                sql.Comando.Parameters.AddWithValue("@id",item.id);
            }

            sql.Comando.CommandText = sb.ToString();
            sql.AbrirConexao();
            IDataReader dr = sql.RetornaDados_v2();

            Int32 razao = dr.GetOrdinal("razao_social");
            Int32 fantasia = dr.GetOrdinal("fantasia_nome");
            Int32 tipo_pessoa = dr.GetOrdinal("tipo_pessoa");
            Int32 cpf_cnpj = dr.GetOrdinal("cpf_cnpj");
            Int32 ie = dr.GetOrdinal("ie");
           
            Int32 endereco = dr.GetOrdinal("endereco");
            Int32 bairro = dr.GetOrdinal("bairro");
            Int32 n = dr.GetOrdinal("numero");
            Int32 complemento = dr.GetOrdinal("complemento");
            Int32 cep = dr.GetOrdinal("cep");
            Int32 estado = dr.GetOrdinal("estado");
            Int32 cidade = dr.GetOrdinal("cidade");

            while (dr.Read()){
                item.fantasia = dr.GetString(fantasia);
                item.tipo_pessoa = (tipo)dr.GetInt32(tipo_pessoa);
                item.razao_social = dr.GetString(razao);
                item.cpf_cnpj = dr.GetString(cpf_cnpj);
                item.ie = dr.GetString(ie);
                item.endereco = dr.GetString(endereco);
                item.bairro = dr.GetString(bairro);
                item.n = dr.GetString(n);
                item.complemento = dr.GetString(complemento);
                item.cep = dr.GetString(cep);
                item.estado = dr.GetInt32(estado);
                item.cidade = dr.GetInt32(cidade);

            }

            sql.FechaConexao();
            return item;
        }


        public DataTable seleciona_grid(String search){
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "select * from fornecedor where fantasia_nome ilike '"+search+"%'";
            sql.AbrirConexao();
            DataTable dt = sql.RetornaDados_v2_dt();
            sql.FechaConexao();
            return dt;
        }


    }
}
