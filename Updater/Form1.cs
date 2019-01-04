using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Threading;

namespace Updater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            lbl_descricao.Text = "Iniciando Atualização";
        }

        // ROTINA DE ATUALIZAÇÂO

        // 1 - Iniciando
        // 2 - Verifica Versão atual
        // 3 - Transforma Bytea da base em arquivo executavel em rede_sistema/versao/instalador.msi
        // 4 - Fecha todas instancias do sistema em aberto
        // 5 - Inicia desinstalação do produto atual
        // 6 - Inicia nova instalação do sistema
        // 7 - Reporta atualização do sistema 
        // 8 - 
        // 8 - Finaliza atualizador
        // 9 - Abre o programa atualizado

        public Int32 step = 0;
        public List<string> list_softwares;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (step == 10)
            {
                step++;
                progressBar1.Value = 100;
                lbl_descricao.Text = "Desinstalação Concluida !!!";
                backgroundWorker1.RunWorkerAsync();
                
            }

            if(step == 9)
            {
                step++;
                progressBar1.Value = 90;
                lbl_descricao.Text = "Finalizando";
            }

            if(step == 7)
            {
                step++;
                progressBar1.Value = 70;
                backgroundWorker1.RunWorkerAsync();
                //_DoWork(sender, new DoWorkEventArgs(new object()));
                lbl_descricao.Text = "Iniciando nova Instalação";
            }


            if (step == 5)
            {
                step++;
                progressBar1.Value = 50;
                backgroundWorker1.RunWorkerAsync();
                //_DoWork(sender, new DoWorkEventArgs(new object()));
                lbl_descricao.Text = "Validando desinstalação";
            }



            if (step == 3)
            {

                step++;
                progressBar1.Value = 30;
                backgroundWorker1.RunWorkerAsync();
                //_DoWork(sender, new DoWorkEventArgs(new object()));
                lbl_descricao.Text = "Desinstalando versão atual";

            }


            if (step == 1)
            {
                step++;
                backgroundWorker1.RunWorkerAsync();
                progressBar1.Value = 20;
                lbl_descricao.Text = "Coletando dados da versão atual";
            }

            if (step == 0)
            {
                lbl_descricao.Text = "Estamos preparando tudo para você !";
                this.step++;
                progressBar1.Value = 10;
            }




        }

        private void lbl_descricao_Click(object sender, EventArgs e)
        {

        }

        private List<string> ListPrograms()
        {
            List<string> programs = new List<string>();

            try
            {
                ManagementObjectSearcher mos =
                  new ManagementObjectSearcher("SELECT * FROM Win32_Product");
                foreach (ManagementObject mo in mos.Get())
                {
                    try
                    {
                        //more properties:
                        //http://msdn.microsoft.com/en-us/library/windows/desktop/aa394378(v=vs.85).aspx
                        programs.Add(mo["Name"].ToString());

                    }
                    catch (Exception ex)
                    {
                        //this program may not have a name property
                    }
                }

                return programs;

            }
            catch (Exception ex)
            {
                return programs;
            }
        }

        private bool UninstallProgram(string ProgramName)
        {
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher(
                  "SELECT * FROM Win32_Product WHERE Name = '" + ProgramName + "'");
                foreach (ManagementObject mo in mos.Get())
                {
                    try
                    {
                        if (mo["Name"].ToString() == ProgramName)
                        {
                            //ManagementBaseObject outParams = mo.InvokeMethod("Uninstall", null);
                            //Console.WriteLine("The Uninstall method result: {0}", outParams["ReturnValue"]);

                            string arqui_bat = "C:/Rede_Sistema/uninstall.bat";
                            if (!File.Exists(arqui_bat))
                            {
                                File.Create(arqui_bat).Close();
                                TextWriter escrever = File.AppendText(arqui_bat);
                                escrever.WriteLine("@echo off");
                                escrever.WriteLine("msiexec /x {1C72DCF2-AC6B-4B2D-BFCF-1B80E16865A0}");


                                // File.Delete(arqui_bat);
                                escrever.Close();
                                //Process.Start .StartInfo.FileName = arqui_bat;
                                //Process.Start();
                            }


                            if (MessageBox.Show("Para que a gente possa prosseguir com a instalação, pedimos que clique somente em avançar na caixa de desinstalação ou em next caso seu computador esteja em inglês.", "Instrução de Atualização",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                            {

                                Process proc = new Process();
                                proc.StartInfo.FileName = "uninstall.bat";
                                proc.StartInfo.WorkingDirectory = "C:/Rede_Sistema";
                                proc.StartInfo.CreateNoWindow = false;
                                proc.Start();
                                proc.WaitForExit();
                                int ExitCode = proc.ExitCode;
                                proc.Close();
                            }
                            else
                            {
                                MessageBox.Show("Infelizmente não podemos prosseguir com a instalação e este assistente de atualização será encerrado !");
                                Application.Exit();
                            }


                            //   object hr = mo.InvokeMethod("Uninstall",null);
                            //   return (bool)hr;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.step--;
                        //this program may not have a name property, so an exception will be thrown
                    }
                }

                //was not found...
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool verifica_programa_instalado(string ProgramName)
        {
            try
            {
                Boolean x = false;

                ManagementObjectSearcher mos = new ManagementObjectSearcher(
                  "SELECT * FROM Win32_Product WHERE Name = '" + ProgramName + "'");
                foreach (ManagementObject mo in mos.Get())
                {
                    try
                    {
                        if (mo["Name"].ToString() == ProgramName)
                            x = true;

                       
                    }
                    catch (Exception ex)
                    {
                        return false;
                        //this program may not have a name property, so an exception will be thrown
                    }
                }

                //was not found...
                return x;

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public void instalacao()
        {
            if (MessageBox.Show("Para que a gente possa prosseguir com a instalação, pedimos que clique somente em avançar na caixa de instalação ou em next caso seu computador esteja em inglês.", "Instrução de Atualização",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question,
               MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {

                Process proc = new Process();
                proc.StartInfo.FileName = "atualizacao.msi";
                proc.StartInfo.WorkingDirectory = "C:/Rede_Sistema";
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();
                proc.WaitForExit();
                int ExitCode = proc.ExitCode;
                proc.Close();
            }
            else
            {
                MessageBox.Show("Infelizmente não podemos prosseguir com a instalação e este assistente de atualização será encerrado !");
                Application.Exit();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Coletar lista de programas
            if (this.step == 2)
            {
                this.list_softwares = ListPrograms();
                this.step++;
            }

            // Desinstalação do sistema
            if (this.step == 4)
            {
                UninstallProgram("Razor");
                this.step++;
            }

            // Valida que o programa não esta instalado
            if(this.step == 6)
            {
                Boolean x = verifica_programa_instalado("Razor");
                if (x)
                    this.step = 4;
                else
                    this.step++;
            }

            if(this.step == 8)
            {
                Thread.Sleep(2000);
                instalacao();
                this.step++;
            }

            if(this.step > 10)
            {
                this.step++;
                string arqui_bat = "C:/Rede_Sistema/starter.bat";
                if (!File.Exists(arqui_bat))
                {
                    File.Create(arqui_bat).Close();
                    TextWriter escrever = File.AppendText(arqui_bat);
                    //escrever.WriteLine("@echo off");
                   // escrever.WriteLine("START Instalador_Trend_SAT");

                    escrever.WriteLine("@echo off");
                    escrever.WriteLine("cd C:/Program Files (x86)/Razor/Razor");
                    escrever.WriteLine("Start /b Zenfox_Software.exe");

                    // File.Delete(arqui_bat);
                    escrever.Close();
                    //Process.Start .StartInfo.FileName = arqui_bat;
                    //Process.Start();
                }

                Process proc = new Process();
                proc.StartInfo.FileName = "starter.bat";
                proc.StartInfo.WorkingDirectory = "C:/Rede_Sistema";
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                proc.WaitForExit();
                int ExitCode = proc.ExitCode;
                proc.Close();

                Application.Exit();

            }

        }
    }
}
