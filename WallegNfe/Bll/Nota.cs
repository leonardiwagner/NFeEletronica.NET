using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Trabalhar com Xml
using System.Xml;

namespace WallegNfe.Bll
{
    public class Nota
    {
        /// <summary>
        /// Carrega uma nota apartir de um arquivo XML
        /// </summary>
        /// <returns></returns>
        public WallegNfe.Nota Carregar(String arquivoNotaXml)
        {
            if (!Bll.Arquivo.ExisteArquivo(arquivoNotaXml))
            {
                throw new Exception("O arquivo de nota para envio não existe: " + arquivoNotaXml);
            }

            WallegNfe.Nota nota = new WallegNfe.Nota();

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

        public static void Move(WallegNfe.Nota nota, Model.NotaSituacao situacao)
        {
            String novoCaminho = Bll.Arquivo.PastaNota(situacao) + "\\" + nota.ArquivoNome;
            Bll.Arquivo.Move(nota.CaminhoFisico, novoCaminho);
            nota.CaminhoFisico = novoCaminho;
            nota.Situacao = situacao;
        }
    }
}
