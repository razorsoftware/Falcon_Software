﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.gerenciamento
{
    public class Estoque
    {

        public class Entidade
        {
            public Int32 id_produto { get; set; }
            public Double quantidade { get; set; }

        }

        public static void dar_baixa(Entidade item)
        {
            data.bd_postgres sql = new data.bd_postgres();
            sql.localdb();
            sql.AbrirConexao();
            sql.Comando = new Npgsql.NpgsqlCommand();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("update produto set estoque = @quantidade where id = @id");

            sql.Comando.Parameters.AddWithValue("@quantidade", NpgsqlTypes.NpgsqlDbType.Double, item.quantidade);
            sql.Comando.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Integer, item.id_produto);

            sql.Comando.CommandText = sb.ToString();
            sql.ExecutaComando_v2();
            sql.FechaConexao();
        }

    }
}
