using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Zenfox_Software_OO.Cadastros
{

    public class Compras_Detalhamentos{
        // Detalhamento NFE
        public String Codigo_Produto_Fabrica { get; set; }
        public String Descricao_Produto { get; set; }
        public String NCM { get; set; }
        public String Unidade_Medida { get; set; }
        public decimal Quantidade { get; set; }
        public Double Valor_Compra { get; set; }
    }

    public class Compras
    {
        public String caminho_xml { get; set; }

        // Cabeçalho NFE
        public String Chave { get; set; }
        public String CNPJ_Fornecedor { get; set; }
                
        List<Compras_Detalhamentos> detalhamentos = new List<Compras_Detalhamentos>();


        public void realiza_leitura_nota_compra()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(this.caminho_xml);

            XmlElement root = xml.DocumentElement;

            #region Leitura Cabeçalho
            //CHAVE NFE ==============
            System.Xml.XmlNodeList nodeList = root.GetElementsByTagName("infNFe");
            this.Chave = nodeList[0].Attributes[0].Value.ToString().Replace("NFe", "");

            //CNPJ DESTINATARIO ==============
            nodeList = root.GetElementsByTagName("ide");
            nodeList = root.GetElementsByTagName("dest");
            try { this.CNPJ_Fornecedor = nodeList[0]["CNPJ"].InnerText; }
            catch { }
            #endregion

            #region Leitura Produtos
            nodeList = root.GetElementsByTagName("det");

            for (int i = 0; i < nodeList.Count; i++){
                Compras_Detalhamentos c_det = new Compras_Detalhamentos();

                // Código Produto Fabrica
                try { c_det.Codigo_Produto_Fabrica = nodeList[i]["prod"]["cProd"].InnerText; }catch { }
                
                // Descrição do Produto
                try { c_det.Descricao_Produto = nodeList[i]["prod"]["xProd"].InnerText; }catch { }

                // NCM
                try { c_det.NCM = nodeList[i]["prod"]["NCM"].InnerText; }catch { }

                // Unidade de Medida
                try { c_det.Unidade_Medida = nodeList[i]["prod"]["uCom"].InnerText; }catch { }

                // Quantidade
                try { c_det.Quantidade = decimal.Parse(nodeList[i]["prod"]["qCom"].InnerText); }catch { }

                // Valor Compra
                try { c_det.Valor_Compra = Double.Parse(nodeList[i]["prod"]["NCM"].InnerText); }catch { }

                this.detalhamentos.Add(c_det);

            }

            String depura = "";

            #endregion
        }

    }
}
