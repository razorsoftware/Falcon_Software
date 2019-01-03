using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software
{
    public partial class Sincronizacao : Form
    {

        public Int32 id { get; set; }

        public Sincronizacao()
        {
            InitializeComponent();

            progressBar1.Maximum = 1;
            progressBar1.Value = 0;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            Zenfox_Software_OO.data.bd_postgres bd = new Zenfox_Software_OO.data.bd_postgres();
            Zenfox_Software_OO.data.bd local_db = new Zenfox_Software_OO.data.bd();

            StringBuilder sb = new StringBuilder();

            // Sincronizando produto ==============================================================
            bd.AbrirConexao();
            IDataReader dr = bd.RetornaDados("select * from produto where empresa = "+ this.id +"");

            Int32 nome = dr.GetOrdinal("nome");
            Int32 ean = dr.GetOrdinal("ean");
            Int32 valor = dr.GetOrdinal("venda");
            Int32 ncm = dr.GetOrdinal("ncm");
            Int32 cfop = dr.GetOrdinal("cfop");

            while (dr.Read()){

                String xnome = "";
                String xean = "";
                String xncm = "";

                if (!dr.IsDBNull(nome))
                    xnome = dr.GetString(nome);

                if (!dr.IsDBNull(ean))
                    xean = dr.GetString(ean);
                
                if (!dr.IsDBNull(ncm))
                    xncm = dr.GetString(ncm);

                sb.AppendLine("INSERT INTO produtos(nome, ean, valor, ncm, cfop) VALUES('" + xnome + "','" + xean +"',"+dr.GetDouble(valor)+", '"+xncm+"',"+dr.GetInt32(cfop)+");");
            
            }
            bd.FechaConexao();

            local_db.abrir_conexao();
            local_db.Execute_Command(sb.ToString());
            local_db.fecha_conexao();

            backgroundWorker1.ReportProgress(1);
            
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Sincronização realizada com sucesso !");
            this.Close();
        }
    }
}
