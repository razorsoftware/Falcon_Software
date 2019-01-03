using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenfox_Software_OO.data
{
    public class bd
    {
        private SQLiteConnection sqlite;

        public bd()
        {
            //This part killed me in the beginning.  I was specifying "DataSource"
            //instead of "Data Source"
            sqlite = new SQLiteConnection("Data Source=database.db");

        }

        public void abrir_conexao()
        {
            sqlite.Open();
        }

        public void fecha_conexao()
        {
            sqlite.Close();
        }


        public DataTable selectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;              
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
            }
            return dt;
        }

        public void Execute_Command(String query)
        {

            try
            {
                SQLiteCommand cmd;
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
            }
        }

    }
}
