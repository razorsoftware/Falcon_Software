using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software_OO
{

    public class ACBR
    {


        public Boolean valida_ncm(String ncm)
        {
            Boolean acbr_return = false;
            Boolean ncm_valido = false;

            Zenfox_Software_OO.Cadastros.Configuracao cmd = new Cadastros.Configuracao();
            Cadastros.Entidade_Configuracao item = cmd.seleciona(new Cadastros.Entidade_Configuracao());

            if (item.valida_ncm)
            {


                if (!Directory.Exists("C:/Rede_Sistema"))
                    Directory.CreateDirectory("C:/Rede_Sistema");

                String texto = "NCM.Validar('" + ncm + "')";
                System.IO.File.WriteAllText(@"C:\Rede_Sistema\ENT.txt", texto);

                Thread.Sleep(200);

                if (!acbr_return)
                {
                    Thread.Sleep(500);
                    String[] ncm_result;
                    try
                    {
                        ncm_result = System.IO.File.ReadAllText(@"C:\Rede_Sistema\sai.txt").Split(':');
                    }
                    catch
                    {
                        throw new Exception("Falha ao tentar se comunicar com o SAT !");
                    }
                    if (ncm_result.Length > 0)
                    {
                        acbr_return = true;
                        System.IO.File.Delete(@"C:\Rede_Sistema\SAI.txt");
                        if (ncm_result[0].ToString() == "OK")
                            ncm_valido = true;
                    }
                }


                if (!acbr_return)
                {
                    Thread.Sleep(2000);
                    String[] ncm_result = System.IO.File.ReadAllText(@"C:\Rede_Sistema\SAI.txt").Split(':');

                    if (ncm_result.Length > 0)
                    {
                        acbr_return = true;
                        System.IO.File.Delete(@"C:\Rede_Sistema\SAI.txt");
                        if (ncm_result[0].ToString() == "OK")
                            ncm_valido = true;
                    }
                }

                if (!acbr_return)
                {
                    Thread.Sleep(5000);
                    String[] ncm_result = System.IO.File.ReadAllText(@"C:\Rede_Sistema\SAI.txt").Split(':');

                    if (ncm_result.Length > 0)
                    {
                        acbr_return = true;
                        System.IO.File.Delete(@"C:\Rede_Sistema\SAI.txt");
                        if (ncm_result[0].ToString() == "OK")
                            ncm_valido = true;
                    }
                }


            }
            else
            {
                ncm_valido = true;
            }
            return ncm_valido;
        }

        public void configura_acbr(String cnpj, String ie, String im, String codigo_vinculacao)
        {

            System.IO.StreamReader arquivo = File.OpenText(@"C:\ACBrMonitorPLUS\ACBrMonitor.ini");
            System.IO.StreamWriter novoArquivo = File.CreateText(@"C:\ACBrMonitorPLUS\ACBrMonitorx.ini");
            string depura = "";
            string linha = "";

            Boolean flag = false; //flag para quando o arquivo chegou na parte do sat
            Boolean flag_monitor = false;
            Boolean flag_emitente = false; // Flag para quando chegar no arquivo de configuração do emitente
            Boolean flag_software_house = false;
            Boolean flag_impressao = false;

            //Cria os diretórios
            if (!Directory.Exists(@"C:\Rede_Sistema"))
                Directory.CreateDirectory(@"C:\Rede_Sistema");

            while ((linha = arquivo.ReadLine()) != null)
            {
                if (linha.Trim() == "[ACBrMonitor]")
                    flag_monitor = true;

                if (linha.Trim() == "[SAT]")
                    flag = true;

                if (linha.Trim() == "[SATEmit]")
                    flag_emitente = true;

                if (linha.Trim() == "[SATSwH]")
                    flag_software_house = true;

                if (linha.Trim() == "[SATFortes]")
                    flag_impressao = true;

                if (flag_monitor)
                {
                    if (linha.Split('=')[0].ToString() == "TXT_Entrada")
                        linha = "TXT_Entrada = C:/Rede_Sistema/ent.txt";

                    if (linha.Split('=')[0].ToString() == "TXT_Saida")
                    {
                        linha = "TXT_Saida = C:/Rede_Sistema/sai.txt";
                        flag_monitor = false;
                    }

                }


                if (flag)
                {

                    if (linha.Split('=')[0].ToString() == "CodigoAtivacao")
                    {
                        linha = "CodigoAtivacao = 123";
                        flag = false;
                    }
                }

                if (flag_emitente)
                {

                    if (linha.Split('=')[0].ToString() == "CNPJ")
                        linha = "CNPJ = " + cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Replace(",", "");

                    if (linha.Split('=')[0].ToString() == "IE")
                        linha = "IE = " + ie.Replace(".", "");

                    if (linha.Split('=')[0].ToString() == "IM")
                    {
                        linha = "IM = " + im;
                        flag_emitente = false;
                    }
                }

                if (flag_software_house)
                {

                    if (linha.Split('=')[0].ToString() == "CNPJ")
                        linha = "CNPJ = 11444406000180";

                    if (linha.Split('=')[0].ToString() == "Assinatura")
                    {
                        linha = "Assinatura = " + codigo_vinculacao;
                        flag_software_house = false;
                    }
                }


                if (flag_impressao)
                {
                    if (linha.Split('=')[0].ToString() == "Largura")
                    {
                        linha = "Largura = 280";
                        flag_impressao = false;
                    }

                }
                novoArquivo.WriteLine(linha);
            }

            arquivo.Close();
            novoArquivo.Dispose();
            novoArquivo.Close();
            File.Replace(@"C:\ACBrMonitorPLUS\ACBrMonitorx.ini", @"C:\ACBrMonitorPLUS\ACBrMonitor.ini", @"C:\ACBrMonitorPLUS\ACBrMonitorbkp.ini");
        }



        public Boolean inicializa_sat()
        {
            if (!Directory.Exists("C:/Rede_Sistema"))
                Directory.CreateDirectory("C:/Rede_Sistema");

            String texto = "SAT.Inicializar";
            System.IO.File.WriteAllText(@"C:\Rede_Sistema\ENT.txt", texto);

            return true;
        }

        // SAT ============================
        // ==============================

        /* 
         *   1 - Iniciar Montagem
         *   2 - Identificação
         *   3 - Emitente
         *   4 - Destinatario
         *   5 - Adiciona Produto
         *   6 - Total
         *   7 - Pagamentos
         *   8 - Dados Adicionais
         *   9 - Gera Cupom
        */


        public String inicia_montagem(String versao)
        {
            return @"
                SAT.CriarEnviarCfe(""[infCFe]
            versao = " + versao + @" ";
        }

        public String identificacao(String cnpj_sh, String codigo_vinculacao, String numero_caixa)
        {
            String texto = @" 
            [IDentificacao]
            CNPJ = " + cnpj_sh.Replace(".", "").Replace("/", "") + @"
            signAC = " + codigo_vinculacao + @"
            numeroCaixa = " + numero_caixa;
            return texto;


        }

        public String emitente(String cnpj, String ie, String im)
        {
            String text = @" 
            [Emitente]
            CNPJ = " + cnpj + @"
            IE = " + ie + @"
            IM = " + im + @"
            indRatISSQN = S " + @"";
            return text;
        }

        public String destinatario(String cpf_cnpj, String nome)
        {
            String text = @" 
            [Destinatario]
            CNPJCPF = " + cpf_cnpj + @" 
            xNome = " + nome + " ";
            return text;
        }

        public String adiciona_produto(Int32 numero, String codigo, String infAdicional, String ean, String nome, String ncm, String cfop, Double quantidade, Double valor,
            Double desconto, Double vOutro, Double icms)
        {
            ean = "";

            String text = @" 
            [Produto" + ((numero).ToString("000")) + @"]
            cProd = " + codigo + @"
            infAdProd = " + infAdicional + @"
            cEAN = " + ean + @"
            xProd = " + nome + @"
            NCM = " + ncm + @"
            CFOP = " + cfop + @"
            uCom = UN" + @"
            Combustivel = 0 
            qCom = " + quantidade + @"
            vUnCom = " + valor + @"
            indRegra = A 
            vDesc = " + desconto + @"         
            vOutro = " + vOutro + @"
            vItem12741 = " + (valor * quantidade) + @"
            [ObsFiscoDet" + ((numero).ToString("000")) + @"]
            xCampoDet = Cod. CEST
            xTextoDet = ";

            text += @" 
            [ICMS" + ((numero).ToString("000")) + @"]
            Orig = 10 ";

            if (true)
            {

                text += @"
                CSOSN = 900 ";
                text += @"
                pICMS = " + icms + " ";

                text += @"
                [PIS" + ((numero).ToString("000")) + @"]
                CST = 49 
                [COFINS" + ((numero).ToString("000")) + @"] 
                CST = 49 ";

            }
            else
            {

                //text += @"[ICMS" + ((numero).ToString("000")) + @"]
                //                    Orig = 0
                //                    CST = 40
                //                    [PIS" + ((numero).ToString("000")) + @"]
                //                    CST = 01
                //                    [COFINS" + ((numero).ToString("000")) + @"]
                //                    CST = 01";
            }

            return text;
        }

        public String total(Double aliquotatotal, Double totaldesconto)
        {
           // String text = @" 
           // [Total]
           // vCFeLei12741 = " + aliquotatotal + @"
           // [DescAcrEntr]
           // vDescSubtot = " + totaldesconto + " ";

            String text = @"
            [Total]
            vCFeLei12741 = " + aliquotatotal + @"
              [DescAcrEntr]
            vDescSubtot = " + totaldesconto;

            return text;
        }

        public String formas_pagamento(Int32 codigo_pagamento, Int32 pagamento, Double total)
        {
            String text = "";


            text = @" 
            [Pagto" + ((codigo_pagamento).ToString("000")) + @"]
            cMP = " + int.Parse(pagamento.ToString()).ToString("00") + @"
            vMP = " + total;

            return text;
        }

        public String dados_adicionais(String mensagem)
        {
            String text = "";

            text += @"
              [DadosAdicionais]
              infCpl = " + mensagem + @"
                [ObsFisco001]
              xCampo = 
              xTexto = "")";
            return text;
        }

        public String emite_sat(Int32 id_venda, String texto)
        {
            //MessageBox.Show("Vai escrever o arquivo TXT");
            if (File.Exists("C:/Rede_Sistema/sai.txt"))
                File.Delete("C:/Rede_Sistema/sai.txt");


            System.IO.File.WriteAllText(@"C:\Rede_Sistema\ENT.txt", texto, Encoding.UTF8);


            String xml = "";
            String xml_impressao = "";
            Boolean arquivo_existe = false;

            #region verificando se arquivo existe

            Thread.Sleep(1000);
            if (File.Exists("C:/Rede_Sistema/sai.txt"))
                arquivo_existe = true;

            if (!arquivo_existe)
                Thread.Sleep(1000);
            if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                arquivo_existe = true;

            if (!arquivo_existe)
                Thread.Sleep(2000);
            if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                arquivo_existe = true;

            if (!arquivo_existe)
                Thread.Sleep(2000);
            if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                arquivo_existe = true;

            if (!arquivo_existe)
                Thread.Sleep(3000);
            if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                arquivo_existe = true;

            if (!arquivo_existe)
                Thread.Sleep(3000);
            if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                arquivo_existe = true;

            if (!arquivo_existe)
                Thread.Sleep(5000);
            if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                arquivo_existe = true;

            if (!arquivo_existe)
                Thread.Sleep(5000);
            if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                arquivo_existe = true;

            if (!arquivo_existe)
                Thread.Sleep(10000);
            if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                arquivo_existe = true;

            if (!arquivo_existe)
                Thread.Sleep(10000);
            if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                arquivo_existe = true;

            #endregion


            string codigoDeRetorno = retornoSAT("codigoDeRetorno");
            if (codigoDeRetorno != "6000")
            {
                int retorno = 0;
                try
                {
                    retorno = int.Parse(codigoDeRetorno);
                }
                catch { }
                MessageBox.Show(retornaMSGSat(retorno));
            }
            else
            {

                xml = retornoSAT("XML");

                //xml_impressao = "SAT.ImprimirExtratoResumido(\"" + xml + "\");";
                xml_impressao = "SAT.ImprimirExtratoVenda(\"" + xml + "\");";
                System.IO.File.WriteAllText(@"C:\Rede_Sistema\ENT.txt", xml_impressao);

                // if (MessageBox.Show("Deseja imprimir uma segunda via ?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                //     System.IO.File.WriteAllText(@"C:\Rede_Sistema\ENT.txt", xml_impressao);

                /// if (xml.Length < 70)
                /// {
                ///     for (int i = 0; i < lines.Length; i++)
                ///     {
                ///         if (lines[i].Contains("XML="))
                ///         {
                ///             xml = lines[i].Substring(4);
                ///
                ///             if (xml.Length < 70)
                ///             {
                ///                 xml = lines[i + 1].ToString();
                ///             }
                ///         }
                ///     }
                /// }

                if (File.Exists("C:/Rede_Sistema/sai.txt"))
                    File.Delete("C:/Rede_Sistema/sai.txt");

            }

            return xml;
        }


        public Zenfox_Software_OO.Cadastros.Entidade_Vendas cancela_cupom_fiscal(Zenfox_Software_OO.Cadastros.Entidade_Vendas item)
        {
            try
            {

                String xml = "SAT.CancelarCFe(\"" + item.xml + "\");";



                System.IO.File.WriteAllText("C:/Rede_Sistema/ENT.txt", xml.Replace("\\\"", "'"));


                #region verificando se arquivo existe
                Boolean arquivo_existe = false;

                Thread.Sleep(1000);
                if (File.Exists("C:/Rede_Sistema/sai.txt"))
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(1000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(2000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(2000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(3000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(3000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(5000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(5000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(10000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                if (!arquivo_existe)
                    Thread.Sleep(10000);
                if (File.Exists("C:/Rede_Sistema/sai.txt") && arquivo_existe == false)
                    arquivo_existe = true;

                #endregion

                string[] lines = File.ReadAllLines("C:/Rede_Sistema/sai.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("codigoDeRetorno="))
                    {
                        xml = lines[i].Substring(4);
                    }
                }

                if (xml.Contains("7000"))
                {
                    //excluirsat(ven_id);
                    xml = "SAT.ImprimirExtratoCancelamento(\"" + item.xml + "\");";
                    System.IO.File.WriteAllText(@"C:\Rede_Sistema\ENT.txt", xml);
                    item.xml = xml;
                    //return ok;
                }
                else
                {
                    MessageBox.Show("Não foi possivel cancelas esse cupom fiscal, verifique se ele esta dentro do período de cancelamento !");
                    item.xml = "";
                }

                File.Delete("C:/Rede_Sistema/sai.txt");
                return item;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return item;
            }
        }

        private string retornoSAT(string chave)
        {
            string retorno = "";
            try
            {
                string[] lines = File.ReadAllLines("C:/Rede_Sistema/sai.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(chave + "="))
                    {
                        retorno = lines[i].Substring(chave.Length + 1);

                        if (retorno.Length <= 100 && chave == "XML" || chave == "xml")
                        {
                            retorno += lines[i + 1].ToString();
                        }

                    }
                }
            }
            catch { retorno = "Erro Monitor"; }

            return retorno;
        }

        private string retornaMSGSat(int codigodeRetorno)
        {
            string msg = "";
            string[] msgSat = new string[20000];

            msgSat[100] = "CF-e-SAT processado com sucesso";
            msgSat[101] = "CF-e-SAT de cancelamento processado com sucesso";
            msgSat[102] = "CF-e-SAT processado – verificar inconsistências";
            msgSat[103] = "CF-e-SAT de cancelamento processado – verificar inconsistências";
            msgSat[104] = "Não Existe Atualização do Software";
            msgSat[105] = "Lote recebido com sucesso";
            msgSat[106] = "Lote Processado";
            msgSat[107] = "Lote em Processamento";
            msgSat[108] = "Lote não localizado";
            msgSat[109] = "Serviço em Operação";
            msgSat[110] = "Status SAT recebido com sucesso";
            msgSat[112] = "Assinatura do AC Registrada";
            msgSat[113] = "Consulta cadastro com uma ocorrência";
            msgSat[114] = "Consulta cadastro com mais de uma ocorrência";
            msgSat[115] = "Solicitação de dados efetuada com sucesso";
            msgSat[116] = "Atualização do SB pendente";
            msgSat[117] = "Solicitação de Arquivo de Parametrização efetuada com sucesso";
            msgSat[118] = "Logs extraídos com sucesso";
            msgSat[119] = "Comandos da SEFAZ pendentes";
            msgSat[120] = "Não existem comandos da SEFAZ pendentes";
            msgSat[121] = "Certificado Digital criado com sucesso";
            msgSat[122] = "CRT recebido com sucesso";
            msgSat[123] = "Adiar transmissão do lote";
            msgSat[124] = "Adiar transmissão do CF-e";
            msgSat[125] = "CF-e de teste de produção emitido com sucesso";
            msgSat[126] = "CF-e de teste de ativação emitido com sucesso";
            msgSat[127] = "Erro na emissão de CF-e de teste de produção";
            msgSat[128] = "Erro na emissão de CF-e de teste de ativação";
            msgSat[129] = "Solicitações de emissão de certificados excedidas. (Somente ocorrerá no ambiente de testes)";
            msgSat[200] = "Rejeição: Status do equipamento SAT difere do esperado";
            msgSat[201] = "Rejeição: Falha na Verificação da Assinatura do Número de segurança";
            msgSat[202] = "Rejeição: Falha no reconhecimento da autoria ou integridade do arquivo digital";
            msgSat[203] = "Rejeição: Emissor não Autorizado para emissão da CF-e-SAT";
            msgSat[204] = "Rejeição: Duplicidade de CF-e-SAT";
            msgSat[205] = "Rejeição: Equipamento SAT encontra-se Ativo";
            msgSat[206] = "Rejeição: Hora de Emissão do CF-e-SAT posterior à hora de recebimento.";
            msgSat[207] = "Rejeição: CNPJ do emitente inválido";
            msgSat[208] = "Rejeição: Equipamento SAT encontra-se Desativado";
            msgSat[209] = "Rejeição: IE do emitente inválida";
            msgSat[210] = "Rejeição: Intervalo de tempo entre o CF-e-SAT emitido e a emissão do respectivo CF-e-SAT de cancelamento é maior que 30 (trinta) minutos.";
            msgSat[211] = "Rejeição: CNPJ não corresponde ao informado no processo de transferência.";
            msgSat[212] = "Rejeição: Data de Emissão do CF-e-SAT posterior à data de recebimento.";
            msgSat[213] = "Rejeição: CNPJ-Base do Emitente difere do CNPJ-Base do Certificado Digital";
            msgSat[214] = "Rejeição: Tamanho da mensagem excedeu o limite estabelecido";
            msgSat[215] = "Rejeição: Falha no schema XML";
            msgSat[216] = "Rejeição: Chave de Acesso difere da cadastrada";
            msgSat[217] = "Rejeição: CF-e-SAT não consta na base de dados da SEFAZ";
            msgSat[218] = "Rejeição: CF-e-SAT já esta cancelado na base de dados da SEFAZ";
            msgSat[219] = "Rejeição: CNPJ não corresponde ao informado no processo de declaração de posse.";
            msgSat[220] = "Rejeição: Valor do rateio do desconto sobre subtotal do item (N) inválido.";
            msgSat[221] = "Rejeição: Aplicativo Comercial não vinculado ao SAT";
            msgSat[222] = "Rejeição: Assinatura do Aplicativo Comercial inválida";
            msgSat[223] = "Rejeição: CNPJ do transmissor do lote difere do CNPJ do transmissor da consulta";
            msgSat[224] = "Rejeição: CNPJ da Software House inválido";
            msgSat[225] = "Rejeição: Falha no Schema XML do lote de CFe";
            msgSat[226] = "Rejeição: Código da UF do Emitente diverge da UF receptora";
            msgSat[227] = "Rejeição: Erro na Chave de Acesso - Campo Id – falta a literal CFe";
            msgSat[228] = "Rejeição: Valor do rateio do acréscimo sobre subtotal do item (N) inválido.";
            msgSat[229] = "Rejeição: IE do emitente não informada";
            msgSat[230] = "Rejeição: IE do emitente não autorizada para uso do SAT";
            msgSat[231] = "Rejeição: IE do emitente não vinculada ao CNPJ";
            msgSat[232] = "Rejeição: CNPJ do destinatário do CF-e-SAT de cancelamento diferente daquele do CF-e-SAT a ser cancelado.";
            msgSat[233] = "Rejeição: CPF do destinatário do CF-e-SAT de cancelamento diferente daquele do CF-e-SAT a ser cancelado.";
            msgSat[234] = "Alerta: Razão Social/Nome do destinatário em branco";
            msgSat[235] = "Rejeição: CNPJ do destinatario Invalido";
            msgSat[236] = "Rejeição: Chave de Acesso com dígito verificador inválido";
            msgSat[237] = "Rejeição: CPF do destinatario Invalido";
            msgSat[238] = "Rejeição: CNPJ do emitente do CF-e-SAT de cancelamento diferente do CNPJ do CF-e-SAT a ser cancelado.";
            msgSat[239] = "Rejeição: Versão do arquivo XML não suportada";
            msgSat[240] = "Rejeição: Valor total do CF-e-SAT de cancelamento diferente do Valor total do CF-e-SAT a ser cancelado.";
            msgSat[241] = "Rejeição: diferença de transmissão e recebimento da mensagem superior a 5 minutos.";
            msgSat[242] = "Alerta: CFe dentro do lote estão fora de ordem.";
            msgSat[243] = "Rejeição: XML Mal Formado";
            msgSat[244] = "Rejeição: CNPJ do Certificado Digital difere do CNPJ da Matriz e do CNPJ do Emitente";
            msgSat[245] = "Rejeição: CNPJ Emitente não autorizado para uso do SAT";
            msgSat[246] = "Rejeição: Campo cUF inexistente no elemento cfeCabecMsg do SOAP Header";
            msgSat[247] = "Rejeição: Sigla da UF do Emitente diverge da UF receptora";
            msgSat[248] = "Rejeição: UF do Recibo diverge da UF autorizadora";
            msgSat[249] = "Rejeição: UF da Chave de Acesso diverge da UF receptora";
            msgSat[250] = "Rejeição: UF informada pelo SAT, não é atendida pelo Web Service";
            msgSat[251] = "Rejeição: Certificado enviado não confere com o escolhido na declaração de posse";
            msgSat[252] = "Rejeição: Ambiente informado diverge do Ambiente de recebimento";
            msgSat[253] = "Rejeição: Digito Verificador da chave de acesso composta inválida";
            msgSat[254] = "Rejeição: Elemento cfeCabecMsg inexistente no SOAP Header";
            msgSat[255] = "Rejeição: CSR enviado inválido";
            msgSat[256] = "Rejeição: CRT enviado inválido";
            msgSat[257] = "Rejeição: Número do série do equipamento inválido";
            msgSat[258] = "Rejeição: Data e/ou hora do envio inválida";
            msgSat[259] = "Rejeição: Versão do leiaute inválida";
            msgSat[260] = "Rejeição: UF inexistente";
            msgSat[261] = "Rejeição: Assinatura digital não encontrada";
            msgSat[262] = "Rejeição: CNPJ da software house não está ativo";
            msgSat[263] = "Rejeição: CNPJ do contribuinte não está ativo";
            msgSat[264] = "Rejeição: Base da receita federal está indisponível";
            msgSat[265] = "Rejeição: Número de série inexistente no cadastro do equipamento";
            msgSat[266] = "Falha na comunicação com a AC-SAT";
            msgSat[267] = "Erro desconhecido na geração do certificado pela AC-SAT";
            msgSat[268] = "Rejeição: Certificado está fora da data de validade.";
            msgSat[269] = "Rejeição: Tipo de atividade inválida";
            msgSat[270] = "Rejeição: Chave de acesso do CFe a ser cancelado inválido.";
            msgSat[271] = "Rejeição: Ambiente informado no CF-e difere do Ambiente de recebimento cadastrado.";
            msgSat[272] = "Rejeição: Valor do troco negativo.";
            msgSat[273] = "Rejeição: Serviço Solicitado Inválido";
            msgSat[274] = "Rejeição: Equipamento não possui declaração de posse";
            msgSat[275] = "Rejeição: Status do equipamento diferente de Fabricado";
            msgSat[276] = "Rejeição: Diferença de dias entre a data de emissão e de recepção maior que o prazo legal";
            msgSat[277] = "Rejeição: CNPJ do emitente não está ativo junto à Sefaz na data de emissão";
            msgSat[278] = "Rejeição: IE do emitente não está ativa junto à Sefaz na data de emissão";
            msgSat[280] = "Rejeição: Certificado Transmissor Inválido";
            msgSat[281] = "Rejeição: Certificado Transmissor Data Validade";
            msgSat[282] = "Rejeição: Certificado Transmissor sem CNPJ";
            msgSat[283] = "Rejeição: Certificado Transmissor - erro Cadeia de Certificação";
            msgSat[284] = "Rejeição: Certificado Transmissor revogado";
            msgSat[285] = "Rejeição: Certificado Transmissor difere ICP-Brasil";
            msgSat[286] = "Rejeição: Certificado Transmissor erro no acesso a LCR";
            msgSat[287] = "Rejeição: Código Município do FG - ISSQN: dígito inválido. Exceto os códigos descritos no Anexo 2 que apresentam dígito inválido.";
            msgSat[288] = "Rejeição: Data de emissão do CF-e-SAT a ser cancelado inválida";
            msgSat[289] = "Rejeição: Código da UF informada diverge da UF solicitada";
            msgSat[290] = "Rejeição: Certificado Assinatura inválido";
            msgSat[291] = "Rejeição: Certificado Assinatura Data Validade";
            msgSat[292] = "Rejeição: Certificado Assinatura sem CNPJ";
            msgSat[293] = "Rejeição: Certificado Assinatura - erro Cadeia de Certificação";
            msgSat[294] = "Rejeição: Certificado Assinatura revogado";
            msgSat[295] = "Rejeição: Certificado Raiz difere dos Válidos";
            msgSat[296] = "Rejeição: Certificado Assinatura erro no acesso a LCR";
            msgSat[297] = "Rejeição: Assinatura difere do calculado";
            msgSat[298] = "Rejeição: Assinatura difere do padrão do Projeto";
            msgSat[299] = "Rejeição: Hora de emissão do CF-e-SAT a ser cancelado inválida";
            msgSat[402] = "Rejeição: XML da área de dados com codificação diferente de UTF-8";
            msgSat[403] = "Rejeição: Versão do leiaute do CF-e-SAT não é válida";
            msgSat[404] = "Rejeição: Uso de prefixo de namespace não permitido";
            msgSat[405] = "Alerta: Versão do leiaute do CF-e-SAT não é a mais atual";
            msgSat[406] = "Rejeição: Versão do Software Básico do SAT não é valida.";
            msgSat[407] = "Rejeição: Indicador de CF-e-SAT cancelamento inválido (diferente de „C? e „?)";
            msgSat[408] = "Rejeição: Valor total do CF-e-SAT maior que o somatório dos valores de Meio de Pagamento empregados em seu pagamento.";
            msgSat[409] = "Rejeição: Valor total do CF-e-SAT supera o máximo permitido no arquivo de Parametrização de Uso";
            msgSat[410] = "Rejeição: UF informada no campo cUF não é atendida pelo Web Service";
            msgSat[411] = "Rejeição: Campo versaoDados inexistente no elemento cfeCabecMsg do SOAP Header";
            msgSat[412] = "Rejeição: CFe de cancelamento não corresponde ao CFe anteriormente gerado";
            msgSat[420] = "Rejeição: Cancelamento para CF-e-SAT já cancelado";
            msgSat[450] = "Rejeição: Modelo da CF-e-SAT diferente de 59";
            msgSat[452] = "Rejeição: número de série do SAT inválido ou não autorizado.";
            msgSat[453] = "Rejeição: Ambiente de processamento inválido (diferente de 1 e 2)";
            msgSat[454] = "Rejeição: CNPJ da Software House inválido";
            msgSat[455] = "Rejeição: Assinatura do Aplicativo Comercial não é válida.";
            msgSat[456] = "Rejeição: Código de Regime tributário invalido";
            msgSat[457] = "Rejeição: Código de Natureza da Operação para ISSQN inválido";
            msgSat[458] = "Rejeição: Razão Social/Nome do destinatário em branco";
            msgSat[459] = "Rejeição: Código do produto ou serviço em branco";
            msgSat[460] = "Rejeição: GTIN do item (N) inválido";
            msgSat[461] = "Rejeição: Descrição do produto ou serviço em branco";
            msgSat[462] = "Rejeição: CFOP não é de operação de saída prevista para CF-e-SAT";
            msgSat[463] = "Rejeição: Unidade comercial do produto ou serviço em branco";
            msgSat[464] = "Rejeição: Quantidade Comercial do item (N) inválido";
            msgSat[465] = "Rejeição: Valor unitário do item (N) inválido";
            msgSat[466] = "Rejeição: Valor bruto do item (N) difere de quantidade * Valor Unitário, considerando regra de arred/trunc.";
            msgSat[467] = "Rejeição: Regra de calculo do item (N) inválida";
            msgSat[468] = "Rejeição: Valor do desconto do item (N) inválido";
            msgSat[469] = "Rejeição: Valor de outras despesas acessórias do item (N) inválido.";
            msgSat[470] = "Rejeição: Valor líquido do Item do CF-e difere de Valor Bruto de Produtos e Serviços - desconto + Outras Despesas Acessórias – rateio do desconto sobre subtotal + rateio do acréscimo sobre subtotal ";
            msgSat[471] = "Rejeição: origem da mercadoria do item (N) inválido (difere de 0, 1, 2, 3, 4, 5, 6 e 7)";
            msgSat[472] = "Rejeição: CST do Item (N) inválido (diferente de 00, 20, 90)";
            msgSat[473] = "Rejeição: Alíquota efetiva do ICMS do item (N) inválido.";
            msgSat[474] = "Rejeição: Valor líquido do ICMS do Item (N) difere de Valor do Item * Aliquota Efetiva";
            msgSat[475] = "Rejeição: CST do Item (N) inválido (diferente de 40 e 41 e 50 e 60)";
            msgSat[476] = "Rejeição: Código de situação da operação - Simples Nacional - do Item (N) inválido (diferente de 102, 300 e 500)";
            msgSat[477] = "Rejeição: Código de situação da operação - Simples Nacional - do Item (N) inválido (diferente de 900)";
            msgSat[478] = "Rejeição: Código de Situação Tributária do PIS Inválido (diferente de 01 e 02)";
            msgSat[479] = "Rejeição: Base de cálculo do PIS do item (N) inválido.";
            msgSat[480] = "Rejeição: Alíquota do PIS do item (N) inválido.";
            msgSat[481] = "Rejeição: Valor do PIS do Item (N) difere de Base de Calculo * Aliquota do PIS";
            msgSat[482] = "Rejeição: Código de Situação Tributária do PIS Inválido (diferente de 03)";
            msgSat[483] = "Rejeição: Qtde Vendida do item (N) inválido.";
            msgSat[484] = "Rejeição: Alíquota do PIS em R$ do item (N) inválido.";
            msgSat[485] = "Rejeição: Valor do PIS do Item (N) difere de Qtde Vendida* Aliquota do PIS em R$";
            msgSat[486] = "Rejeição: Código de Situação Tributária do PIS Inválido (diferente de 04, 06, 07, 08 e 09)";
            msgSat[487] = "Rejeição: Código de Situação Tributária do PIS inválido (diferente de 49)";
            msgSat[488] = "Rejeição: Código de Situação Tributária do PIS Inválido (diferente de 99)";
            msgSat[489] = "Rejeição: Valor do PIS do Item (N) difere de Qtde Vendida* Aliquota do PIS em R$ e difere de Base de Calculo * Aliquota do PIS";
            msgSat[490] = "Rejeição: Código de Situação Tributária da COFINS Inválido (diferente de 01 e 02)";
            msgSat[491] = "Rejeição: Base de cálculo do COFINS do item (N) inválido.";
            msgSat[492] = "Rejeição: Alíquota da COFINS do item (N) inválido.";
            msgSat[493] = "Rejeição: Valor da COFINS do Item (N) difere de Base de Calculo * Aliquota da COFINS";
            msgSat[494] = "Rejeição: Código de Situação Tributária da COFINS Inválido (diferente de 03)";
            msgSat[495] = "Rejeição: Valor do COFINS do Item (N) difere de Qtde Vendida* Aliquota do COFINS em R$ e difere de Base de Calculo * Aliquota do COFINS";
            msgSat[496] = "Rejeição: Alíquota da COFINS em R$ do item (N) inválido.";
            msgSat[497] = "Rejeição: Valor da COFINS do Item (N) difere de Qtde Vendida* Aliquota da COFINS em R$";
            msgSat[498] = "Rejeição: Código de Situação Tributária da COFINS Inválido (diferente de 04, 06, 07, 08 e 09)";
            msgSat[499] = "Rejeição: Código de Situação Tributária da COFINS Inválido (diferente de 49)";
            msgSat[500] = "Rejeição: Código de Situação Tributária da COFINS Inválido (diferente de 99)";
            msgSat[501] = "Rejeição: Operação com tributação de ISSQN sem informar a Inscrição Municipal";
            msgSat[502] = "Rejeição: Erro na Chave de Acesso - Campo Id não corresponde à concatenação dos campos correspondentes";
            msgSat[503] = "Rejeição: Valor das deduções para o ISSQN do item (N) inválido.";
            msgSat[504] = "Rejeição: Valor da Base de Calculo do ISSQN do Item (N) difere de Valor do Item - Valor das deduções";
            msgSat[505] = "Rejeição: Alíquota efetiva do ISSQN do item (N) não é maior ou igual a 2,00 (2%) e menor ou igual a 5,00 (5%).";
            msgSat[506] = "Valor do ISSQN do Item (N) difere de Valor da Base de Calculo do ISSQN * Alíquota Efetiva do ISSQN";
            msgSat[507] = "Rejeição: Indicador de rateio para ISSQN inválido";
            msgSat[508] = "Rejeição: Item da lista de Serviços do ISSQN do item (N) inválido.";
            msgSat[509] = "Rejeição: Código municipal de Tributação do ISSQN do Item (N) em branco.";
            msgSat[510] = "Rejeição: Código de Natureza da Operação para ISSQN inválido";
            msgSat[511] = "Rejeição: Indicador de Incentivo Fiscal do ISSQN do item (N) inválido (diferente de 1 e 2)";
            msgSat[512] = "Rejeição: Total do PIS difere do somatório do PIS dos itens";
            msgSat[513] = "Rejeição: Total do COFINS difere do somatório do COFINS dos itens";
            msgSat[514] = "Rejeição: Total do PIS-ST difere do somatório do PIS-ST dos itens";
            msgSat[515] = "Rejeição: Total do COFINS-STdifere do somatório do COFINS-ST dos itens";
            msgSat[516] = "Rejeição: Total de Outras Despesas Acessórias difere do somatório de Outras Despesas Acessórias (acréscimo) dos itens";
            msgSat[517] = "Rejeição: Total dos Itens difere do somatório do valor líquido dos itens";
            msgSat[518] = "Rejeição: Informado grupo de totais do ISSQN sem informar grupo de valores de ISSQN";
            msgSat[519] = "Rejeição: Total da BC do ISSQN difere do somatório da BC do ISSQN dos itens";
            msgSat[520] = "Rejeição: Total do ISSQN difere do somatório do ISSQN dos itens";
            msgSat[521] = "Rejeição: Total do PIS sobre serviços difere do somatório do PIS dos itens de serviços";
            msgSat[522] = "Rejeição: Total do COFINS sobre serviços difere do somatório do COFINS dos itens de serviços";
            msgSat[523] = "Rejeição: Total do PIS-ST sobre serviços difere do somatório do PIS-ST dos itens de serviços";
            msgSat[524] = "Rejeição: Total do COFINS-ST sobre serviços difere do somatório do COFINS-ST dos itens de serviços";
            msgSat[525] = "Rejeição: Valor de Desconto sobre total inválido.";
            msgSat[526] = "Rejeição: Valor de Acréscimo sobre total inválido.";
            msgSat[527] = "Rejeição: Código do Meio de Pagamento inválido";
            msgSat[528] = "Rejeição: Valor do Meio de Pagamento inválido.";
            msgSat[529] = "Rejeição: Valor de desconto sobre subtotal difere do somatório dos seus rateios nos itens.";
            msgSat[530] = "Rejeição: Operação com tributação de ISSQN sem informar a Inscrição Municipal";
            msgSat[531] = "Rejeição: Valor de acréscimo sobre subtotal difere do somatório dos seus rateios nos itens.";
            msgSat[532] = "Rejeição: Total do ICMS difere do somatório dos itens";
            msgSat[533] = "Rejeição: Valor aproximado dos tributos do CF-e-SAT – Lei 12741/12 inválido";
            msgSat[534] = "Rejeição: Valor aproximado dos tributos do Produto ou serviço – Lei 12741/12 inválido.";
            msgSat[535] = "Rejeição: código da credenciadora de cartão de débito ou crédito inválido";
            msgSat[537] = "Rejeição: Total do Desconto difere do somatório dos itens";
            msgSat[539] = "Rejeição: Duplicidade de CF-e-SAT, com diferença na Chave de Acesso [99999999999999999999999999999999999999999]";
            msgSat[540] = "Rejeição: CNPJ da Software House + CNPJ do emitente assinado no campo “signAC” difere do informado no campo “CNPJvalue” ";
            msgSat[555] = "Rejeição: Tipo autorizador do protocolo diverge do Órgão Autorizador";
            msgSat[564] = "Rejeição: Total dos Produtos ou Serviços difere do somatório do valor dos Produtos ou Serviços dos itens";
            msgSat[600] = "Serviço Temporariamente Indisponível";
            msgSat[601] = "CF-e-SAT inidôneo por recepção fora do prazo";
            msgSat[602] = "Rejeição: Status do equipamento não permite ativação";
            msgSat[603] = "Arquivo inválido";
            msgSat[604] = "Erro desconhecido na verificação de comandos";
            msgSat[605] = "Tamanho do arquivo inválido";
            msgSat[999] = "Rejeição: Erro não catalogado";

            msgSat[04000] = "Ativado corretamente SAT Ativado com Sucesso.";
            msgSat[04001] = "Erro na criação do certificado processo de ativação foi interrompido.";
            msgSat[04002] = "SEFAZ não reconhece este SAT (CNPJ inválido) Verificar junto a SEFAZ o CNPJ cadastrado.";
            msgSat[04003] = "SAT já ativado SAT disponível para uso.";
            msgSat[04004] = "SAT com uso cessado SAT bloqueado por cessação de uso.";
            msgSat[04005] = "Erro de comunicação com a SEFAZ Tentar novamente.";
            msgSat[04006] = "CSR ICP-BRASIL criado com sucesso Processo de criação do CSR para certificação ICP-BRASIL com sucesso";
            msgSat[04007] = "Erro na criação do CSR ICP-BRASIL Processo de criação do CSR para certificação ICP-BRASIL com erro";
            msgSat[04098] = "SAT em processamento. Tente novamente.";
            msgSat[04099] = "Erro desconhecido na ativação Informar ao administrador.";
            msgSat[05000] = "Certificado transmitido com Sucesso ";
            msgSat[05001] = "Código de ativação inválido.";
            msgSat[05002] = "Erro de comunicação com a SEFAZ. Tentar novamente.";
            msgSat[05003] = "Certificado Inválido ";
            msgSat[05098] = "SAT em processamento.";
            msgSat[05099] = "Erro desconhecido Informar o administrador.";
            msgSat[06000] = "Emitido com sucesso + conteúdo notas. Retorno CF-e-SAT ao AC para contingência.";
            msgSat[06001] = "Código de ativação inválido.";
            msgSat[06002] = "SAT ainda não ativado. Efetuar ativação.";
            msgSat[06003] = "SAT não vinculado ao AC Efetuar vinculação";
            msgSat[06004] = "Vinculação do AC não confere Efetuar vinculação";
            msgSat[06005] = "Tamanho do CF-e-SAT superior a 1.500KB";
            msgSat[06006] = "SAT bloqueado pelo contribuinte";
            msgSat[06007] = "SAT bloqueado pela SEFAZ";
            msgSat[06008] = "SAT bloqueado por falta de comunicação";
            msgSat[06009] = "SAT bloqueado, código de ativação incorreto";
            msgSat[06010] = "Erro de validação do conteúdo.";
            msgSat[06098] = "SAT em processamento.";
            msgSat[06099] = "Erro desconhecido na emissão. Informar o administrador.";
            msgSat[07000] = "Cupom cancelado com sucesso + conteúdo CF-eSAT cancelado.";
            msgSat[07001] = "Código ativação inválido Verificar o código e tentar mais uma vez.";
            msgSat[07002] = "Cupom inválido Informar o administrador.";
            msgSat[07003] = "SAT bloqueado pelo contribuinte";
            msgSat[07004] = "SAT bloqueado pela SEFAZ";
            msgSat[07005] = "SAT bloqueado por falta de comunicação";
            msgSat[07006] = "SAT bloqueado, código de ativação incorreto";
            msgSat[07007] = "Erro de validação do conteúdo";
            msgSat[07098] = "SAT em processamento.";
            msgSat[07099] = "Erro desconhecido no cancelamento.";
            msgSat[08000] = "SAT em operação. Verifica se o SAT está ativo.";
            msgSat[08098] = "SAT em processamento.";
            msgSat[08099] = "Erro desconhecido. Informar o administrador.";
            msgSat[09000] = "Emitido com sucesso Gera e envia um cupom de teste para SEFAZ, para verificar a comunicação.";
            msgSat[09001] = "código ativação inválido Verificar o código e tentar mais uma vez.";
            msgSat[09002] = "SAT ainda não ativado. Efetuar ativação ";
            msgSat[09098] = "SAT em processamento.";
            msgSat[09099] = "Erro desconhecido Informar o ";
            msgSat[10000] = "Resposta com Sucesso. Informações de status do SAT.";
            msgSat[10001] = "Código de ativação inválido";
            msgSat[10098] = "SAT em processamento.";
            msgSat[10099] = "Erro desconhecido Informar o administrador.";
            msgSat[11000] = "Emitido com sucesso Retorna o conteúdo do CF-ao AC.";
            msgSat[11001] = "código ativação inválido Verificar o código e tentar mais uma vez.";
            msgSat[11002] = "SAT ainda não ativado. Efetuar ativação.";
            msgSat[11003] = "Sessão não existe. AC deve executar a sessão novamente.";
            msgSat[11098] = "SAT em processamento.";
            msgSat[11099] = "Erro desconhecido. Informar o administrador.";
            msgSat[12000] = "Rede Configurada com Sucesso";
            msgSat[12001] = "código ativação inválido Verificar o código e tentar mais uma vez.";
            msgSat[12002] = "Dados fora do padrão a ser informado Corrigir dados";
            msgSat[12098] = "SAT em processamento.";
            msgSat[12099] = "Erro desconhecido Informar o administrador.";
            msgSat[13000] = "Assinatura do AC";
            msgSat[13001] = "código ativação inválido Verificar o código e tentar mais uma vez.";
            msgSat[13002] = "Erro de comunicação com a SEFAZ";
            msgSat[13003] = "Assinatura fora do padrão informado Corrigir dados";
            msgSat[13004] = "CNPJ da Software House + CNPJ do emitente assinado no campo “signAC” difere do informado no campo “CNPJvalue” Corrigir dados";
            msgSat[13098] = "SAT em processamento.";
            msgSat[13099] = "Erro desconhecido Informar o administrador.";
            msgSat[14000] = "Software Atualizado com Sucesso ";
            msgSat[14001] = "Código de ativação inválido.";
            msgSat[14002] = "Atualização em Andamento";
            msgSat[14003] = "Erro na atualização Não foi possível Atualizar o SAT.";
            msgSat[14004] = "Arquivo de atualização inválido";
            msgSat[14098] = "SAT em processamento.";
            msgSat[14099] = "Erro desconhecido Informar o administrador.";
            msgSat[15000] = "Transferência completa Arquivos de Logs extraídos";
            msgSat[15001] = "Código de ativação inválido.";
            msgSat[15002] = "Transferência em andamento";
            msgSat[15098] = "SAT em processamento.";
            msgSat[15099] = "Erro desconhecido Informar o administrador.";
            msgSat[16000] = "Equipamento SAT bloqueado com sucesso.";
            msgSat[16001] = "Código de ativação inválido.";
            msgSat[16002] = "Equipamento SAT já está bloqueado.";
            msgSat[16003] = "Erro de comunicação com a SEFAZ";
            msgSat[16004] = "Não existe parametrização de bloqueio disponível.";
            msgSat[16098] = "SAT em processamento.";
            msgSat[16099] = "Erro desconhecido Informar o administrador.";
            msgSat[17000] = "Equipamento SAT desbloqueado com sucesso.";
            msgSat[17001] = "Código de ativação inválido.";
            msgSat[17002] = "SAT bloqueado pelo contribuinte. Verifique configurações na SEFAZ";
            msgSat[17003] = "SAT bloqueado pela SEFAZ";
            msgSat[17004] = "Erro de comunicação com a SEFAZ";
            msgSat[17098] = "SAT em processamento.";
            msgSat[17099] = "Erro desconhecido Informar o administrador.";
            msgSat[18000] = "Código de ativação alterado com sucesso.";
            msgSat[18001] = "Código de ativação inválido.";
            msgSat[18002] = "Código de ativação de emergência Incorreto.";
            msgSat[18098] = "SAT em processamento.";
            msgSat[18099] = "Erro desconhecido Informar o administrador.";

            try
            {
                msg = msgSat[codigodeRetorno];
            }
            catch { }

            return msg;

        }

    }
}
