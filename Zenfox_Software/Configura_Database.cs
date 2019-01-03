using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software
{
    public partial class Configura_Database : Form
    {
        public Configura_Database()
        {
            InitializeComponent();
        }

        private void Configura_Database_Load(object sender, EventArgs e)
        {
            Boolean x = false;
            Zenfox_Software_OO.data.bd_postgres sql = new Zenfox_Software_OO.data.bd_postgres();

            // Porta 5432
            if (System.IO.File.Exists("bd.txt"))
                System.IO.File.Delete("bd.txt");
            System.IO.File.WriteAllText("bd.txt", "SERVER=localhost;PORT=5432");
            sql.localdb();
            try
            {
                sql.AbrirConexao();
                sql.FechaConexao();
                x = true;
                this.Close();
            }
            catch
            {

            }

            // Porta 5433
            if (!x)
            {

                if (System.IO.File.Exists("bd.txt"))
                    System.IO.File.Delete("bd.txt");
                System.IO.File.WriteAllText("bd.txt", "SERVER=localhost;PORT=5433");
                sql.localdb();
                try
                {
                    sql.AbrirConexao();
                    sql.FechaConexao();
                    x = true;
                    this.Close();

                }
                catch
                {

                }
            }

            // Porta 5434
            if (!x)
            {
                if (System.IO.File.Exists("bd.txt"))
                    System.IO.File.Delete("bd.txt");
                System.IO.File.WriteAllText("bd.txt", "SERVER=localhost;PORT=5434");
                sql.localdb();
                try
                {
                    sql.AbrirConexao();
                    sql.FechaConexao();
                    x = true;
                    this.Close();
                }
                catch
                {

                }
            }

        }

        private void txt_ip_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("bd.txt"))
                System.IO.File.Delete("bd.txt");

            System.IO.File.WriteAllText("bd.txt", "SERVER=" + txt_ip.Text + ";PORT=" + txt_porta.Text);

            try
            {
                Zenfox_Software_OO.data.bd_postgres sql = new Zenfox_Software_OO.data.bd_postgres();
                sql.localdb();
                sql.AbrirConexao();
                sql.FechaConexao();
                MessageBox.Show("Conexão realizada com sucesso !");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            //SERVER="++";PORT=5433
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Zenfox_Software_OO.data.bd_postgres bd = new Zenfox_Software_OO.data.bd_postgres();
                bd.testa("SERVER=" + txt_ip.Text + ";PORT=" + txt_porta.Text);
                bd.AbrirConexao();
                bd.FechaConexao();
                MessageBox.Show("Conexão realizada com sucesso !");
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
