using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Razor_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Teste_leitura_xml()
        {
            String xml = retornoSAT("XML");


        }


        private string retornoSAT(string chave)
        {
            string retorno = "";
            try
            {
                string[] lines = File.ReadAllLines("C:/Rede_Sistema/sai.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i == 221)
                        retorno = "";

                    if (lines[i].Contains(chave + "="))
                    {
                        retorno = lines[i].Substring(chave.Length + 1);

                        if (retorno.Length <= 100 && chave == "XML" || chave == "xml")
                        {
                            retorno += lines[i + 1].ToString();
                            retorno = retorno.Length.ToString();
                        }

                    }
                }
            }
            catch { retorno = "Erro Monitor"; }

            return retorno;
        }

    }
}
