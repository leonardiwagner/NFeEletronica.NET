using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Trabalhar com Xml
using System.Xml;

namespace WallegNFe.Bll
{
    public class Nota
    {
        /// <summary>
        /// Carrega uma nota apartir de um arquivo XML
        /// </summary>
        /// <returns></returns>
        public WallegNFe.Nota Carregar(String arquivoNotaXml)
        {
            if (!Bll.Arquivo.ExisteArquivo(arquivoNotaXml))
            {
                throw new Exception("O arquivo de nota para envio não existe: " + arquivoNotaXml);
            }

            WallegNFe.Nota nota = new WallegNFe.Nota(null);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(arquivoNotaXml);

            /*
            nota.DestinatarioCNPJ = xmlDoc.GetElementsByTagName("emit")[0].ChildNodes[0].InnerText;
            nota.DestinatarioNome = xmlDoc.GetElementsByTagName("emit")[0].ChildNodes[1].InnerText;

            nota.DataEmissao = DateTime.Parse(xmlDoc.GetElementsByTagName("ide")[0].ChildNodes[7].InnerText);
            nota.Numero = xmlDoc.GetElementsByTagName("ide")[0].ChildNodes[6].InnerText;
            */

            nota.CaminhoFisico = arquivoNotaXml;
            nota.ConteudoXml = xmlDoc.ToString();
            //nota.ArquivoFisicoNome = Bll.Arquivo.Nome(nota.ArquivoFisicoCaminho);

            return nota;
        }

    }
}
