using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.Cadastros
{
    public class Cliente
    {
        public class Entidade
        {

            public Int32 id { get; set; }
            public String nome { get; set; }
            public String cpf { get; set; }
            public String rg { get; set; }
            public String endereco { get; set; }
            public Int32 estado { get; set; }
            public Int32 cidade { get; set; }
            public String cep { get; set; }
            public String bairro { get; set; }
            public String telefone { get; set; }
            public String celular { get; set; }
            public String observacao { get; set; }
            public Boolean status { get; set; }
            public String numero { get; set; }
            public String complemento { get; set; }           

        }
        
        public enum tipo{
            pessoa_fisica = 0,
            pessoa_juridica = 1
        }

        public void salva(Entidade item)
        {

            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            StringBuilder sb = new StringBuilder();

            if (item.id == 0){
                sb.AppendLine("INSERT INTO cliente (nome,cpf,rg,endereco,cidade,estado,cep,bairro,telefone,celular,observacao,status,numero,complemento)");
                sb.AppendLine("VALUES (@nome,@cpf,@rg,@endereco,@cidade,@estado,@cep,@bairro,@telefone,@celular,@observacao,@status,@numero,@complemento);");

                sql.Comando = new Npgsql.NpgsqlCommand();
                
                sql.Comando.Parameters.AddWithValue("@nome", NpgsqlTypes.NpgsqlDbType.Varchar, item.nome);
                sql.Comando.Parameters.AddWithValue("@cpf", NpgsqlTypes.NpgsqlDbType.Varchar, item.cpf);
                sql.Comando.Parameters.AddWithValue("@rg", NpgsqlTypes.NpgsqlDbType.Varchar, item.rg);
                sql.Comando.Parameters.AddWithValue("@endereco", NpgsqlTypes.NpgsqlDbType.Varchar, item.endereco);
                sql.Comando.Parameters.AddWithValue("@cidade", NpgsqlTypes.NpgsqlDbType.Integer,item.cidade);
                sql.Comando.Parameters.AddWithValue("@estado", NpgsqlTypes.NpgsqlDbType.Integer, item.estado);
                sql.Comando.Parameters.AddWithValue("@cep", NpgsqlTypes.NpgsqlDbType.Varchar, item.cep);
                sql.Comando.Parameters.AddWithValue("@bairro", NpgsqlTypes.NpgsqlDbType.Varchar, item.bairro);
                sql.Comando.Parameters.AddWithValue("@telefone", NpgsqlTypes.NpgsqlDbType.Varchar, item.telefone);
                sql.Comando.Parameters.AddWithValue("@celular", NpgsqlTypes.NpgsqlDbType.Varchar, item.celular);
                sql.Comando.Parameters.AddWithValue("@observacao", NpgsqlTypes.NpgsqlDbType.Varchar, item.observacao);
                sql.Comando.Parameters.AddWithValue("@status", NpgsqlTypes.NpgsqlDbType.Boolean, true);
                sql.Comando.Parameters.AddWithValue("@numero", NpgsqlTypes.NpgsqlDbType.Varchar, item.numero);
                sql.Comando.Parameters.AddWithValue("@complemento", NpgsqlTypes.NpgsqlDbType.Varchar, item.complemento);

                sql.AbrirConexao();
                sql.Comando.CommandText = sb.ToString();
                sql.ExecutaComando_v2();
                sql.FechaConexao();
            }
            else{
                sb.AppendLine("update cliente set nome=@nome,cpf=@cpf,rg=@rg,endereco=@endereco,cidade=@cidade,");
                sb.AppendLine("estado=@estado,cep=@cep,bairro=@bairro,telefone=@telefone,celular=@celular,observacao=@observacao,");
                sb.AppendLine("numero=@numero,complemento=@complemento where id = @id");
                
                sql.Comando = new Npgsql.NpgsqlCommand();

                sql.Comando.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Integer, item.id);
                sql.Comando.Parameters.AddWithValue("@nome", NpgsqlTypes.NpgsqlDbType.Varchar, item.nome);
                sql.Comando.Parameters.AddWithValue("@cpf", NpgsqlTypes.NpgsqlDbType.Varchar, item.cpf);
                sql.Comando.Parameters.AddWithValue("@rg", NpgsqlTypes.NpgsqlDbType.Varchar, item.rg);
                sql.Comando.Parameters.AddWithValue("@endereco", NpgsqlTypes.NpgsqlDbType.Varchar, item.endereco);
                sql.Comando.Parameters.AddWithValue("@cidade", NpgsqlTypes.NpgsqlDbType.Integer, item.cidade);
                sql.Comando.Parameters.AddWithValue("@estado", NpgsqlTypes.NpgsqlDbType.Integer, item.estado);
                sql.Comando.Parameters.AddWithValue("@cep", NpgsqlTypes.NpgsqlDbType.Varchar, item.cep);
                sql.Comando.Parameters.AddWithValue("@bairro", NpgsqlTypes.NpgsqlDbType.Varchar, item.bairro);
                sql.Comando.Parameters.AddWithValue("@telefone", NpgsqlTypes.NpgsqlDbType.Varchar, item.telefone);
                sql.Comando.Parameters.AddWithValue("@celular", NpgsqlTypes.NpgsqlDbType.Varchar, item.celular);
                sql.Comando.Parameters.AddWithValue("@observacao", NpgsqlTypes.NpgsqlDbType.Varchar, item.observacao);
                sql.Comando.Parameters.AddWithValue("@numero", NpgsqlTypes.NpgsqlDbType.Varchar, item.numero);
                sql.Comando.Parameters.AddWithValue("@complemento", NpgsqlTypes.NpgsqlDbType.Varchar, item.complemento);

                sql.AbrirConexao();
                sql.Comando.CommandText = sb.ToString();
                sql.ExecutaComando_v2();
                sql.FechaConexao();
            }
          

        }

        public Entidade seleciona(Entidade item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            sql.Comando = new Npgsql.NpgsqlCommand();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select * from cliente where status = true ");

            if (item.id > 0)
            {
                sb.AppendLine("and id = @id ");
                sql.Comando.Parameters.AddWithValue("@id", item.id);
            }

            if(item.cpf != null)
            {
                sb.AppendLine("and id = (select id from (select replace(REPLACE(cpf,'.',''),'-','')as cpf,id from cliente) as a where cpf = @cpf)   ");
                sql.Comando.Parameters.AddWithValue("@cpf",NpgsqlTypes.NpgsqlDbType.Varchar ,item.cpf);
            }


            sql.Comando.CommandText = sb.ToString();
            sql.AbrirConexao();
            IDataReader dr = sql.RetornaDados_v2();

            Int32 id = dr.GetOrdinal("id");
            Int32 nome = dr.GetOrdinal("nome");
            Int32 cpf = dr.GetOrdinal("cpf");
            Int32 rg = dr.GetOrdinal("rg");
            Int32 endereco = dr.GetOrdinal("endereco");
            Int32 cidade = dr.GetOrdinal("cidade");
            Int32 estado = dr.GetOrdinal("estado");
            Int32 cep = dr.GetOrdinal("cep");
            Int32 bairro = dr.GetOrdinal("bairro");
            Int32 telefone = dr.GetOrdinal("telefone");
            Int32 celular = dr.GetOrdinal("celular");
            Int32 observacao = dr.GetOrdinal("observacao");
            Int32 status = dr.GetOrdinal("status");
            Int32 numero = dr.GetOrdinal("numero");
            Int32 complemento = dr.GetOrdinal("complemento");

            
            while (dr.Read())
            {
                item.id = dr.GetInt32(id);
                item.nome = dr.GetString(nome);
                item.cpf = dr.GetString(cpf);
                item.rg = dr.GetString(rg);
                item.endereco = dr.GetString(endereco);
                item.cidade = dr.GetInt32(cidade);
                item.estado = dr.GetInt32(estado);
                item.cep = dr.GetString(cep);
                item.bairro = dr.GetString(bairro);
                item.telefone = dr.GetString(telefone);
                item.celular = dr.GetString(celular);
                item.observacao = dr.GetString(observacao);
                item.numero = dr.GetString(numero);
                item.complemento = dr.GetString(complemento);

            }

            sql.FechaConexao();
            return item;
        }


        public DataTable seleciona_grid(String search)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();

            sql.Comando = new Npgsql.NpgsqlCommand();
            sql.Comando.CommandText = "select * from cliente where status = true and nome ilike '" + search + "%'";
            sql.AbrirConexao();
            DataTable dt = sql.RetornaDados_v2_dt();
            sql.FechaConexao();
            return dt;
        }


        public void apaga(Int32 id)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.Comando = new Npgsql.NpgsqlCommand();


            sql.Comando.Parameters.AddWithValue("@id", id);

            sql.Comando.CommandText = "update cliente set status = false where id = @id ";
            sql.AbrirConexao();
            sql.ExecutaComando_v2();
            sql.FechaConexao();
        }


    }
}
