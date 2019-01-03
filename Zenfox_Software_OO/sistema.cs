using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software_OO
{
    public class sistema
    {



        public void preenche_combobox(ComboBox cb, String nometabela, String campocodigo, String descricao, String filtro, String ordem)
        {
            try
            {
                String sql = "SELECT 0 as "+campocodigo+", 'AAAAA' as "+descricao+" union all select "+campocodigo+","+descricao+" from " + nometabela;

                if (filtro != "")
                    sql += " where " + filtro;

                if (ordem != "")
                    sql += " order by " + ordem;

                data.bd_postgres cmd_sql = new data.bd_postgres();
                cmd_sql.localdb();
                cmd_sql.Comando = new Npgsql.NpgsqlCommand();
                cmd_sql.Comando.CommandText = sql;

                cmd_sql.AbrirConexao();
                DataTable tab = cmd_sql.RetornaDados_v2_dt();
                tab.Rows[0].SetField(1, "Selecione ...");
                cmd_sql.FechaConexao();
                cb.DataSource = tab;
                cb.ValueMember = campocodigo;
                cb.DisplayMember = descricao;
                cb.Text = "Selecione ...";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
