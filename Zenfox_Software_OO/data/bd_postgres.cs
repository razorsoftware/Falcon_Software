using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.data
{
    public class bd_postgres
    {

        private NpgsqlConnection Conexao;
        private NpgsqlTransaction Trans { get; set; }
        public NpgsqlCommand Comando { get; set; }
        public string ConnectionString { get; set; }

        public bd_postgres()
        {
            this.ConnectionString = "DATABASE=topfox;Pooling=False;SERVER=177.125.27.78;PORT=5433;UID=sistema;Password=q2aw3@se4;";
        }


        public void testa(String x)
        {
            this.ConnectionString = "DATABASE=razor_sat;Pooling=False;" + x + ";UID=razor_sat;Password=Pk192168@q2aw3@se4;";
        }

        public void localdb()
        {
            String localdb = System.IO.File.ReadAllText(@System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "bd.txt");
            this.ConnectionString = "DATABASE=razor_sat;Pooling=False;" + localdb + ";UID=razor_sat;Password=Pk192168@q2aw3@se4;";

        }

        public void onlinedb()
        {
            String localdb = System.IO.File.ReadAllText(@System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "bd.txt");
            this.ConnectionString = "DATABASE=erp;Pooling=False;SERVER=erp.postgres.uhserver.com;PORT=5432;UID=razor_erp;Password=Pk192168@;";

        }

        public static String getip()
        {
            return System.IO.File.ReadAllText(@System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "bd.txt");
        }


        public void AbrirConexao()
        {
            if (string.IsNullOrEmpty(this.ConnectionString)) throw new Exception("Não foi informado a ConnectionString.");
            try
            {
                Conexao = new NpgsqlConnection();
                Conexao.ConnectionString = this.ConnectionString;
                Conexao.Open();
            }
            catch (NpgsqlException e)
            {
                throw new Exception("Status atual do banco :" + Conexao.FullState.ToString() + " </ br> Descrição do erro: " + e.Message);
            }
        }
        public void FechaConexao()
        {
            if (Conexao != null && Conexao.State == ConnectionState.Open)
            {
                Conexao.Close();
            }
        }

        #region Transation
        public void Transation_Begin()
        {
            if (Conexao == null || Conexao.State == ConnectionState.Closed) throw new Exception("A conexão fechada. Execute o comando AbrirConexao e não se esqueça de FecharConexao no final.");

            try
            {
                this.Trans = Conexao.BeginTransaction();
            }
            catch (Exception e)
            {
                throw new Exception("Status atual do banco :" + Conexao.FullState.ToString() + " </ br> Descrição do erro: " + e.Message);
            }
        }
        public void Transation_Commit()
        {
            if (Conexao == null || Conexao.State == ConnectionState.Closed) throw new Exception("A conexão fechada. Execute o comando AbrirConexao e não se esqueça de FecharConexao no final.");
            if (Trans == null) throw new Exception("Trasação não iniciada. Execute o comando 'Transation_Begin' e não se esqueça de FecharConexao no final.");

            try
            {
                this.Trans.Commit();
            }
            catch (Exception e)
            {
                throw new Exception("Status atual do banco :" + Conexao.FullState.ToString() + " </ br> Descrição do erro: " + e.Message);
            }
        }
        public void Transation_Rollback()
        {
            if (Conexao == null || Conexao.State == ConnectionState.Closed) throw new Exception("A conexão fechada. Execute o comando AbrirConexao e não se esqueça de FecharConexao no final.");
            if (Trans == null) throw new Exception("Trasação não iniciada. Execute o comando 'Transation_Begin' e não se esqueça de FecharConexao no final.");

            try
            {
                this.Trans.Rollback();
            }
            catch (Exception e)
            {
                throw new Exception("Status atual do banco :" + Conexao.FullState.ToString() + " </ br> Descrição do erro: " + e.Message);
            }
        }
        #endregion


        public IDataReader RetornaDados(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new Exception("Não foi informado a query SQL.");
            if (Conexao == null || Conexao.State == ConnectionState.Closed) throw new Exception("A conexão fechada. Execute o comando AbrirConexao e não se esqueça de FecharConexao no final.");

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = this.Conexao;
            command.CommandText = sql;
            IDataReader reader = command.ExecuteReader();
            return reader;
        }

        public IDataReader RetornaDados_v2()
        {
            if (Conexao == null || Conexao.State == ConnectionState.Closed)
                throw new Exception("A conexão fechada. Execute o comando AbrirConexao e não se esqueça de FecharConexao no final.");
            if (Comando == null || Comando.CommandText.Length < 5)
                throw new Exception("Não ha instrução a ser executada.");

            this.Comando.Connection = this.Conexao;
            IDataReader reader = this.Comando.ExecuteReader();
            return reader;
        }
        public DataTable RetornaDados_v2_dt()
        {
            if (Conexao == null || Conexao.State == ConnectionState.Closed)
                throw new Exception("A conexão fechada. Execute o comando AbrirConexao e não se esqueça de FecharConexao no final.");
            if (Comando == null || Comando.CommandText.Length < 5)
                throw new Exception("Não ha instrução a ser executada.");

            this.Comando.Connection = this.Conexao;
            NpgsqlDataReader reader = this.Comando.ExecuteReader();

            DataTable x = new DataTable();
            x.Load(reader);
            return x;
        }
        public IDataReader RetornaDados_Procedure(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new Exception("Não foi informado a query SQL.");
            if (Conexao == null || Conexao.State == ConnectionState.Closed) throw new Exception("A conexão fechada. Execute o comando AbrirConexao e não se esqueça de FecharConexao no final.");

            NpgsqlCommand command = new NpgsqlCommand(sql, this.Conexao);
            command.CommandType = CommandType.StoredProcedure;
            IDataReader reader = command.ExecuteReader();

            return reader;
        }
        public IDataReader RetornaDados_Procedure()
        {
            if (Conexao == null || Conexao.State == ConnectionState.Closed)
                throw new Exception("A conexão fechada. Execute o comando AbrirConexao e não se esqueça de FecharConexao no final.");
            if (Comando == null || Comando.CommandText.Length < 5)
                throw new Exception("Não ha instrução a ser executada.");

            this.Comando.Connection = this.Conexao;
            this.Comando.CommandType = CommandType.StoredProcedure;
            IDataReader reader = this.Comando.ExecuteReader();

            return reader;
        }

        public int ExecutaComando(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new Exception("Não foi informado a query SQL.");
            if (Conexao == null || Conexao.State == ConnectionState.Closed) throw new Exception("A conexão fechada. Execute o comando AbrirConexao e não se esqueça de FecharConexao no final.");

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = this.Conexao;
            command.CommandText = sql;
            int result = 0;
            result = command.ExecuteNonQuery();

            return result;
        }
        public int ExecutaComando_v2()
        {
            if (Conexao == null || Conexao.State == ConnectionState.Closed)
                throw new Exception("A conexão fechada. Execute o comando AbrirConexao e não se esqueça de FecharConexao no final.");
            if (Comando == null || Comando.CommandText.Length < 5)
                throw new Exception("Não ha instrução a ser executada.");

            this.Comando.Connection = this.Conexao;
            int result = 0;
            result = this.Comando.ExecuteNonQuery();
            return result;
        }

        public int RetornaID(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new Exception("Não foi informado a query SQL.");
            if (Conexao == null || Conexao.State == ConnectionState.Closed) throw new Exception("A conexão fechada. Execute o comando AbrirConexao e não se esqueça de FecharConexao no final.");

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = this.Conexao;
            command.CommandText = sql;
            return Convert.ToInt32(command.ExecuteScalar());
        }
        public int RetornaID_v2()
        {
            if (Conexao == null || Conexao.State == ConnectionState.Closed)
                throw new Exception("A conexão fechada. Execute o comando AbrirConexao e não se esqueça de FecharConexao no final.");
            if (Comando == null || Comando.CommandText.Length < 5)
                throw new Exception("Não ha instrução a ser executada.");

            this.Comando.Connection = this.Conexao;
            return Convert.ToInt32(this.Comando.ExecuteScalar());
        }

        public int ExecutaCount(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new Exception("Não foi informado a query SQL.");
            if (Conexao == null || Conexao.State == ConnectionState.Closed) throw new Exception("A conexão fechada. Execute o comando AbrirConexao e não se esqueça de FecharConexao no final.");

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = this.Conexao;
            command.CommandText = sql;
            int result = Convert.ToInt32(command.ExecuteScalar());

            return result;

        }

        public Byte[] retornarBytea(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new Exception("Não foi informado a query SQL.");
            if (Conexao == null || Conexao.State == ConnectionState.Closed) throw new Exception("A conexão fechada. Execute o comando Abrir Conexao e não se esqueça de FecharConexao no final.");

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = this.Conexao;
            command.CommandText = sql;
            Byte[] result = (Byte[])command.ExecuteScalar();
            return result;
        }

        /// <summary>
        /// Executa comando
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>Retorna o total de linhas afetadas</returns>
        public int insereBytea(string sql, Byte[] bytes)
        {
            NpgsqlCommand command = new NpgsqlCommand(sql, this.Conexao);
            NpgsqlParameter param = new NpgsqlParameter(":bytesData", NpgsqlDbType.Bytea);
            param.Value = bytes;
            command.Parameters.Add(param);
            return Convert.ToInt32(command.ExecuteNonQuery());
        }
    }
}

