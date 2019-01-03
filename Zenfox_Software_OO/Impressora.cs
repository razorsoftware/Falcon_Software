using CDSSoftware;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zenfox_Software_OO
{
    public class Impressora
    {

        public class entidade_orcamento
        {
            public String codigo { get; set; }
            public String descricao { get; set; }
            public String cnpj { get; set; }
            public String empresa { get; set; }
            public decimal quantidade { get; set; }
            public decimal valor { get; set; }
            public decimal total { get; set; }
        }

        public class Entidade_Crediario
        {
            public Caixa.Crediario.status status_crediario { get; set; }
            public Double total { get; set; }
        }
        public Impressora()
        {
            this.item = cmd.seleciona(item);
        }

        Zenfox_Software_OO.Cadastros.Entidade_Configuracao item = new Zenfox_Software_OO.Cadastros.Entidade_Configuracao();
        Zenfox_Software_OO.Cadastros.Configuracao cmd = new Zenfox_Software_OO.Cadastros.Configuracao();

        Int32 linha = 0;

        public String fantasia_empresa = "";
        public String cnpj = "";
        public String nome_cliente = "";
        public String cpf_cliente = "";
        public List<entidade_orcamento> list_orcamento = new List<entidade_orcamento>();
        public List<Caixa.Crediario.Entidade> list_crediario = new List<Caixa.Crediario.Entidade>();
        public String operador = "";
        public Double total_abertura_caixa = 0;
        public Double total_sangria = 0;
        public Double total_suprimento = 0;
        public Double total_cancelado = 0;
        public Double total_reais = 0;
        public Double total_desconto = 0;
        public Double total_geral = 0;
        public Double total_credito = 0;
        public Double total_debito = 0;
        public Double total_crediario = 0;
        public Double total_cheque = 0;

        public void imprime_fechamento_caixa()
        {
            ImprimeTexto imp = new ImprimeTexto();



            using (PrintDocument print = new PrintDocument())
            {
                print.PrintPage += fechamento_caixa;
                print.PrinterSettings.PrinterName = Zenfox_Software_OO.Configuracao.seleciona_impressora();
                print.Print();

            }

            this.linha = 20;

            using (PrintDocument print = new PrintDocument())
            {
                print.PrintPage += encerra_impressao;
                print.PrinterSettings.PrinterName = Zenfox_Software_OO.Configuracao.seleciona_impressora();
                print.Print();

            }

        }

        public void imprime_abertura_caixa()
        {
            ImprimeTexto imp = new ImprimeTexto();


            using (PrintDocument print = new PrintDocument())
            {
                print.PrintPage += abertura_caixa;
                print.PrinterSettings.PrinterName = Zenfox_Software_OO.Configuracao.seleciona_impressora();
                print.Print();
            }

            this.linha = 20;

            using (PrintDocument print = new PrintDocument())
            {
                print.PrintPage += encerra_impressao;
                print.PrinterSettings.PrinterName = Zenfox_Software_OO.Configuracao.seleciona_impressora();
                print.Print();
            }


        }

        public void imprime_orcamento()
        {
            ImprimeTexto imp = new ImprimeTexto();



            using (PrintDocument print = new PrintDocument())
            {
                print.PrintPage += orcamento;
                print.PrinterSettings.PrinterName = Zenfox_Software_OO.Configuracao.seleciona_impressora();
                print.Print();


            }
            this.linha = 20;
            using (PrintDocument print = new PrintDocument())
            {
                print.PrintPage += encerra_impressao;
                print.PrinterSettings.PrinterName = Zenfox_Software_OO.Configuracao.seleciona_impressora();
                print.Print();


            }

        }

        public void imprime_crediario()
        {
            ImprimeTexto imp = new ImprimeTexto();

            using (PrintDocument print = new PrintDocument())
            {
                print.PrintPage += crediario;
                print.PrinterSettings.PrinterName = Zenfox_Software_OO.Configuracao.seleciona_impressora();
                print.Print();
            }
            this.linha = 20;
            using (PrintDocument print4 = new PrintDocument())
            {
                print4.PrintPage += encerra_impressao_sem_linha;
                print4.PrinterSettings.PrinterName = Zenfox_Software_OO.Configuracao.seleciona_impressora();
                print4.Print();
            }
            this.linha = 20;
            using (PrintDocument print3 = new PrintDocument())
            {
                print3.PrintPage += assinatura_cliente;
                print3.PrinterSettings.PrinterName = Zenfox_Software_OO.Configuracao.seleciona_impressora();
                print3.Print();
            }
            this.linha = 20;
            using (PrintDocument print2 = new PrintDocument())
            {
                print2.PrintPage += encerra_impressao_sem_linha;
                print2.PrinterSettings.PrinterName = Zenfox_Software_OO.Configuracao.seleciona_impressora();
                print2.Print();
            }

        }



        // IMPRESSÔES =========================================================================
        #region Impressões 

        private void encerra_impressao(object sender, PrintPageEventArgs e)
        {
            this.linha = 20;
            Graphics g = e.Graphics;
            g = escreve_texto("--------------------------------------------------------------", g);
            g = quebra_linha(g);
        }
        private void encerra_impressao_sem_linha(object sender, PrintPageEventArgs e)
        {
            this.linha = 20;
            Graphics g = e.Graphics;
            g = escreve_texto("", g);
            g = quebra_linha(g);
        }

        private void fechamento_caixa(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            g = escreve_titulo("    Impressão de Fechamento Caixa", g);
            g = quebra_linha(g);
            g = escreve_texto("--------------------- [Informações] ---------------------", g);
            g = quebra_linha(g);
            g = escreve_texto("Data/Hora Fechamento: " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year + " " + DateTime.Now.ToShortTimeString(), g);
            // = escreve_texto("Operador: " + this.operador, g);

            g = quebra_linha(g);
            g = escreve_texto("-------------------------- [Totais] --------------------------", g);
            g = quebra_linha(g);
            g = quebra_linha(g);
            g = escreve_mini_titulo("Fundo de Caixa - inicial", g);
            g = escreve_texto("R$ " + this.total_abertura_caixa.ToString("F2"), g);
            g = escreve_mini_titulo("Total em Dinheiro", g);
            g = escreve_texto("R$ " + this.total_reais.ToString("F2"), g);
            g = escreve_mini_titulo("Total em Débito", g);
            g = escreve_texto("R$ " + this.total_debito.ToString("F2"), g);
            g = escreve_mini_titulo("Total em Crédito", g);
            g = escreve_texto("R$ " + this.total_credito.ToString("F2"), g);
            g = escreve_mini_titulo("Total em Cheque", g);
            g = escreve_texto("R$ " + this.total_cheque.ToString("F2"), g);
            g = escreve_mini_titulo("Total de Acrescimo Caixa/Suprimento", g);
            g = escreve_texto("R$ " + this.total_suprimento.ToString("F2"), g);
            g = escreve_mini_titulo("Total de Retirada/Sangria", g);
            g = escreve_texto("R$ " + this.total_sangria.ToString("F2"), g);
            g = escreve_mini_titulo("Total em Crediário/Promissória", g);
            g = escreve_texto("R$ " + this.total_crediario.ToString("F2"), g);
            g = escreve_mini_titulo("Total Cancelado", g);
            g = escreve_texto("R$ " + this.total_cancelado.ToString("F2"), g);
            g = escreve_mini_titulo("Total Desconto", g);
            g = escreve_texto("R$ " + this.total_desconto.ToString("F2"), g);
            g = quebra_linha(g);
            g = escreve_mini_titulo("Total Vendas", g);
            g = escreve_texto("R$ " + (this.total_geral - this.total_cancelado).ToString("F2"), g);
            g = quebra_linha(g);
            g = escreve_texto("---------------------- [Total Geral] --------------------------", g);
            g = quebra_linha(g);
            g = escreve_mini_titulo("Total Bruto", g);
            this.total_geral += this.total_abertura_caixa;
            this.total_geral += this.total_suprimento;
            g = escreve_texto("R$ " + (this.total_geral).ToString("F2"), g);
            g = escreve_mini_titulo("Total Geral em Caixa", g);
            g = escreve_texto("R$ " + (this.total_geral - (total_desconto + total_sangria + total_cancelado)).ToString("F2"), g);


            //g = escreve_texto("Data Hora Fechamento: " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year, g);
            //g = escreve_texto("Operador: Natanniel Alves", g);
            //g = quebra_linha(g);
            //g = escreve_titulo("Totais", g);
            //g = escreve_titulo("------------------------------------------------------", g);            
            //g = escreve_mini_titulo("Fundo de Caixa - inicial", g);
            //g = escreve_texto("R$ 0.00", g);
            //g = escreve_mini_titulo("Total de Retirada/Sangria", g);
            //g = escreve_texto("R$ 0.00", g);
            //g = escreve_mini_titulo("Total em Dinheiro", g);
            //g = escreve_texto("R$ 0.00", g);
            //g = escreve_mini_titulo("Total em Crédito", g);
            //g = escreve_texto("R$ 0.00", g);
            //g = escreve_mini_titulo("Total em Débito", g);
            //g = escreve_texto("R$ 0.00", g);
            //g = escreve_mini_titulo("Total em Crediário/Promissória", g);
            //g = escreve_texto("R$ 0.00", g);
            //g = escreve_mini_titulo("Total em Cheque", g);
            //g = escreve_texto("R$ 0.00", g);
            //g = escreve_mini_titulo("Total Desconto", g);
            //g = escreve_texto("R$ 0.00", g);
            //g = escreve_mini_titulo("Total Vendas", g);
            //g = escreve_texto("R$ 0.00", g);


            //v = v.Replace("TOTAL DE RETIRADA (R$ )", "Total de retirada (R$)");
            //        v = v.Replace("TOTAL CRÉDITO CAIXA (R$ )", "Total credito caixa (R$)");
            //        v = v.Replace("TOTAL VENDAS REALIZADAS (R$ )", "Total vendas realizadas (R$)");
            //        v = v.Replace("TOTAL DESCONTO", "Total desconto");
            //        v = v.Replace("TOTAL GERAL FINAL (R$ )", "Total geral final (R$)");
            //
            //
            //g.DrawString("------------------------------------------------------", titulo, Brushes.Wheat, 0, 400);
            //g.DrawImage(image, 10, 183);
        }

        private void abertura_caixa(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            g = escreve_titulo("      Impressão de Abertura Caixa", g);
            g = quebra_linha(g);
            g = escreve_texto("--------------------- [Informações] ---------------------", g);
            g = quebra_linha(g);
            g = escreve_texto("Data/Hora Abertura: " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year + " " + DateTime.Now.ToShortTimeString(), g);
            g = escreve_texto("Operador: " + this.operador, g);
            g = quebra_linha(g);
            g = escreve_texto("Valor Inicial: R$ " + this.total_abertura_caixa.ToString("F2"), g);
            g = quebra_linha(g);


        }

        private void orcamento(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            g = escreve_titulo("         Impressão de Orçamento", g);
            g = quebra_linha(g);
            g = escreve_texto("--------------------- [Informações] ---------------------", g);
            g = quebra_linha(g);
            g = escreve_texto("Data/Hora Orçamento: " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year + " " + DateTime.Now.ToShortTimeString(), g);
            g = escreve_texto("Operador: " + this.operador, g);
            g = quebra_linha(g);
            g = escreve_texto("--------------------- [Orçamento] ---------------------", g);
            g = quebra_linha(g);


            foreach (var item in this.list_orcamento)
            {
                g = escreve_texto(item.quantidade.ToString("F2") + " - " + item.descricao + " R$ " + item.total.ToString("F2"), g);
                g = quebra_linha(g);
            }

            g = quebra_linha(g);
            g = escreve_texto("Valor Total: R$ " + this.total_reais.ToString("F2"), g);
            g = quebra_linha(g);

        }

        private void crediario(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            g = escreve_titulo("           Impressão de Crediário", g);
            g = quebra_linha(g);
            g = escreve_texto(this.fantasia_empresa, g);
            g = escreve_texto(this.cnpj, g);
            g = escreve_texto("Data/Hora: " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year + " " + DateTime.Now.ToShortTimeString(), g);
            g = escreve_texto("Operador: " + this.operador, g);
            g = quebra_linha(g);
            g = escreve_texto("-------------- [Informações Cliente] --------------", g);
            g = quebra_linha(g);
            g = escreve_texto(this.nome_cliente, g);
            g = escreve_texto(this.cpf_cliente, g);
            g = quebra_linha(g);
            g = escreve_texto("----------------- [Parcelas Pagas] -----------------", g);
            g = quebra_linha(g);

            Double total_pago = 0;

            foreach (var item in this.list_crediario)
            {
                if (item.status == Caixa.Crediario.status.pago)
                {
                    g = escreve_texto(item.vencimento.ToString() + " - R$ " + item.valor.ToString("f2"), g);
                    total_pago += item.valor;
                }

                if (item.status == Caixa.Crediario.status.parcial)
                {
                    g = escreve_texto(item.vencimento.ToString() + " - R$ " + item.parcial.ToString("f2") + " *PARCIAL*", g);
                    total_pago += item.parcial;
                }
            }

            g = quebra_linha(g);
            g = escreve_texto("--------------- [Parcelas Pendentes] ---------------", g);
            g = quebra_linha(g);

            Double total_pendente = 0;


            foreach (var item in this.list_crediario)
            {
                if (item.status == Caixa.Crediario.status.aberto)
                {
                    g = escreve_texto(item.vencimento.ToString() + " - R$ " + item.valor.ToString("f2"), g);
                    total_pendente += item.valor;
                }

                if (item.status == Caixa.Crediario.status.parcial)
                {
                    g = escreve_texto(item.vencimento.ToString() + " - R$ " + (item.valor - item.parcial).ToString("f2") + " *RESTANTE*", g);
                    total_pendente += (item.valor - item.parcial);
                }
            }

            g = quebra_linha(g);
            g = quebra_linha(g);
            g = escreve_texto("------------------------ [Totais] ------------------------", g);
            g = quebra_linha(g);
            g = escreve_texto("Total         R$ " + (total_pago + total_pendente).ToString("f2"), g);
            g = escreve_texto("Pago         R$ " + total_pago.ToString("f2"), g);
            g = escreve_texto("Restante   R$ " + total_pendente.ToString("f2"), g);
            g = quebra_linha(g);

        }

        private void assinatura_cliente(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g = escreve_texto("_____________________________________________", g);
            g = escreve_texto("Assinatura Cliente", g);
            g = quebra_linha(g);

        }
        #endregion

        #region Impressões Impressoras que quebram muitas linhas a cada impressão

        private void fechamento_caixa_sem_pular(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            g = escreve_titulo("    Impressão de Fechamento Caixa", g);
            g = quebra_linha(g);
            g = escreve_texto("--------------------- [Informações] ---------------------", g);
            g = quebra_linha(g);
            g = escreve_texto("Data/Hora Fechamento: " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year + " " + DateTime.Now.ToShortTimeString(), g);
            // = escreve_texto("Operador: " + this.operador, g);

            g = quebra_linha(g);
            g = escreve_texto("-------------------------- [Totais] --------------------------", g);
            g = quebra_linha(g);
            g = quebra_linha(g);
            g = escreve_mini_titulo("Fundo de Caixa - inicial", g);
            g = escreve_texto("R$ " + this.total_abertura_caixa.ToString("F2"), g);
            g = escreve_mini_titulo("Total em Dinheiro", g);
            g = escreve_texto("R$ " + this.total_reais.ToString("F2"), g);
            g = escreve_mini_titulo("Total em Débito", g);
            g = escreve_texto("R$ " + this.total_debito.ToString("F2"), g);
            g = escreve_mini_titulo("Total em Crédito", g);
            g = escreve_texto("R$ " + this.total_credito.ToString("F2"), g);
            g = escreve_mini_titulo("Total em Cheque", g);
            g = escreve_texto("R$ " + this.total_cheque.ToString("F2"), g);
            g = escreve_mini_titulo("Total de Acrescimo Caixa/Suprimento", g);
            g = escreve_texto("R$ " + this.total_suprimento.ToString("F2"), g);
            g = escreve_mini_titulo("Total de Retirada/Sangria", g);
            g = escreve_texto("R$ " + this.total_sangria.ToString("F2"), g);
            g = escreve_mini_titulo("Total em Crediário/Promissória", g);
            g = escreve_texto("R$ " + this.total_crediario.ToString("F2"), g);
            g = escreve_mini_titulo("Total Cancelado", g);
            g = escreve_texto("R$ " + this.total_cancelado.ToString("F2"), g);
            g = escreve_mini_titulo("Total Desconto", g);
            g = escreve_texto("R$ " + this.total_desconto.ToString("F2"), g);
            g = quebra_linha(g);
            g = escreve_mini_titulo("Total Vendas", g);
            g = escreve_texto("R$ " + (this.total_geral - this.total_cancelado).ToString("F2"), g);
            g = quebra_linha(g);
            g = escreve_texto("---------------------- [Total Geral] --------------------------", g);
            g = quebra_linha(g);
            g = escreve_mini_titulo("Total Bruto", g);
            this.total_geral += this.total_abertura_caixa;
            this.total_geral += this.total_suprimento;
            g = escreve_texto("R$ " + (this.total_geral).ToString("F2"), g);
            g = escreve_mini_titulo("Total Geral em Caixa", g);
            g = escreve_texto("R$ " + (this.total_geral - (total_desconto + total_sangria + total_cancelado)).ToString("F2"), g);
            g = quebra_linha(g);
            g = escreve_texto("--------------------------------------------------------------", g);
            g = quebra_linha(g);
        }

        private void abertura_caixa_sem_pular(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            g = escreve_titulo("      Impressão de Abertura Caixa", g);
            g = quebra_linha(g);
            g = escreve_texto("--------------------- [Informações] ---------------------", g);
            g = quebra_linha(g);
            g = escreve_texto("Data/Hora Abertura: " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year + " " + DateTime.Now.ToShortTimeString(), g);
            g = escreve_texto("Operador: " + this.operador, g);
            g = quebra_linha(g);
            g = escreve_texto("Valor Inicial: R$ " + this.total_abertura_caixa.ToString("F2"), g);
            g = quebra_linha(g);
            g = escreve_texto("--------------------------------------------------------------", g);
            g = quebra_linha(g);

        }

        private void orcamento_sem_pular(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            g = escreve_titulo("         Impressão de Orçamento", g);
            g = quebra_linha(g);
            g = escreve_texto("--------------------- [Informações] ---------------------", g);
            g = quebra_linha(g);
            g = escreve_texto("Data/Hora Orçamento: " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year + " " + DateTime.Now.ToShortTimeString(), g);
            g = escreve_texto("Operador: " + this.operador, g);
            g = quebra_linha(g);
            g = escreve_texto("--------------------- [Orçamento] ---------------------", g);
            g = quebra_linha(g);


            foreach (var item in this.list_orcamento)
            {
                g = escreve_texto(item.quantidade.ToString("F2") + " - " + item.descricao + " R$ " + item.total.ToString("F2"), g);
                g = quebra_linha(g);
            }

            g = quebra_linha(g);
            g = escreve_texto("Valor Total: R$ " + this.total_reais.ToString("F2"), g);
            g = quebra_linha(g);
            g = escreve_texto("--------------------------------------------------------------", g);
            g = quebra_linha(g);

        }

        private void crediario_sem_pular(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            g = escreve_titulo("           Impressão de Crediário", g);
            g = quebra_linha(g);
            g = escreve_texto(this.fantasia_empresa, g);
            g = escreve_texto(this.cnpj, g);
            g = escreve_texto("Data/Hora: " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year + " " + DateTime.Now.ToShortTimeString(), g);
            g = escreve_texto("Operador: " + this.operador, g);
            g = quebra_linha(g);
            g = escreve_texto("-------------- [Informações Cliente] --------------", g);
            g = quebra_linha(g);
            g = escreve_texto(this.nome_cliente, g);
            g = escreve_texto(this.cpf_cliente, g);
            g = quebra_linha(g);
            g = escreve_texto("----------------- [Parcelas Pagas] -----------------", g);
            g = quebra_linha(g);

            Double total_pago = 0;

            foreach (var item in this.list_crediario)
            {
                if (item.status == Caixa.Crediario.status.pago)
                {
                    g = escreve_texto(item.vencimento.ToString() + " - R$ " + item.valor.ToString("f2"), g);
                    total_pago += item.valor;
                }

                if (item.status == Caixa.Crediario.status.parcial)
                {
                    g = escreve_texto(item.vencimento.ToString() + " - R$ " + item.parcial.ToString("f2") + " *PARCIAL*", g);
                    total_pago += item.parcial;
                }
            }

            g = quebra_linha(g);
            g = escreve_texto("--------------- [Parcelas Pendentes] ---------------", g);
            g = quebra_linha(g);

            Double total_pendente = 0;


            foreach (var item in this.list_crediario)
            {
                if (item.status == Caixa.Crediario.status.aberto)
                {
                    g = escreve_texto(item.vencimento.ToString() + " - R$ " + item.valor.ToString("f2"), g);
                    total_pendente += item.valor;
                }

                if (item.status == Caixa.Crediario.status.parcial)
                {
                    g = escreve_texto(item.vencimento.ToString() + " - R$ " + (item.valor - item.parcial).ToString("f2") + " *RESTANTE*", g);
                    total_pendente += (item.valor - item.parcial);
                }
            }

            g = quebra_linha(g);
            g = quebra_linha(g);
            g = escreve_texto("------------------------ [Totais] ------------------------", g);
            g = quebra_linha(g);
            g = escreve_texto("Total         R$ " + (total_pago + total_pendente).ToString("f2"), g);
            g = escreve_texto("Pago         R$ " + total_pago.ToString("f2"), g);
            g = escreve_texto("Restante   R$ " + total_pendente.ToString("f2"), g);
            g = quebra_linha(g);
            g = quebra_linha(g);
            g = quebra_linha(g);
            g = escreve_texto("_____________________________________________", g);
            g = escreve_texto("Assinatura Cliente", g);
            g = quebra_linha(g);
            g = quebra_linha(g);
            g = quebra_linha(g);
            g = escreve_texto("--------------------------------------------------------------", g);
            g = quebra_linha(g);

        }


        #endregion

        // COMANDOS DE ESCRITA ================================================================

        private Graphics escreve_titulo(String texto, Graphics g)
        {
            Font f = new Font("Arial", 12);
            g.DrawString(texto, f, Brushes.Black, 0, this.linha);
            this.linha += 17;
            return g;
        }

        private Graphics escreve_texto(String texto, Graphics g)
        {
            Font f = new Font("Arial", 10);
            g.DrawString(texto, f, Brushes.Black, 0, this.linha);
            this.linha += 17;
            return g;
        }

        private Graphics escreve_mini_titulo(String texto, Graphics g)
        {
            Font f = new Font("Arial", 10, FontStyle.Bold);
            g.DrawString(texto, f, Brushes.Black, 0, this.linha);
            this.linha += 17;
            return g;
        }

        public Graphics quebra_linha(Graphics g)
        {
            Font f = new Font("Arial", 10);
            g.DrawString(" ", f, Brushes.Black, 0, this.linha);
            this.linha += 17;
            return g;
        }



    }
}
