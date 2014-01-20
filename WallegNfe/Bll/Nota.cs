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
        public Model.Nota2 Carregar(String arquivoNotaXml)
        {
            if (!Bll.Arquivo.ExisteArquivo(arquivoNotaXml))
            {
                throw new Exception("O arquivo de nota para envio não existe: " + arquivoNotaXml);
            }

            Model.Nota2 nota = new Model.Nota2();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(arquivoNotaXml);

            nota.DestinatarioCNPJ = xmlDoc.GetElementsByTagName("emit")[0].ChildNodes[0].InnerText;
            nota.DestinatarioNome = xmlDoc.GetElementsByTagName("emit")[0].ChildNodes[1].InnerText;

            nota.DataEmissao = DateTime.Parse(xmlDoc.GetElementsByTagName("ide")[0].ChildNodes[7].InnerText);
            nota.Numero = xmlDoc.GetElementsByTagName("ide")[0].ChildNodes[6].InnerText;

            nota.ArquivoFisicoCaminho = arquivoNotaXml;
            nota.ArquivoFisicoNome = Bll.Arquivo.Nome(nota.ArquivoFisicoCaminho);

            return nota;
        }

        public static void Move(Model.Nota2 nota, Model.NotaSituacao situacao)
        {
            String novoCaminho = Bll.Arquivo.PastaNota(situacao) + "\\" + nota.ArquivoFisicoNome;
            Bll.Arquivo.Move(nota.ArquivoFisicoCaminho, novoCaminho);
            nota.ArquivoFisicoCaminho = novoCaminho;
            nota.Situacao = situacao;
        }
    }
}
