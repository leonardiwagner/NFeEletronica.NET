using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bll
{
    public class NFe
    {
        private List<String> NotaList = new List<String>();

        /// <summary>
        /// Adiciona uma nota no lote para envio
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        public void AdicionaNotaLote(String caminhoArquivo)
        {
            if (this.NotaList.Count >= 50)
            {
                throw new Exception("Limite máximo por lote é de 50 arquivos"); //Limitado pela sefaz
            }
            else
            {
                this.NotaList.Add(caminhoArquivo);
            }
        }

        /// <summary>
        /// Monta um lote para envio
        /// </summary>
        /// <param name="numeroLote"></param>
        /// <returns>Xml do lote</returns>
        public String MontaLote(int numeroLote)
        {
            //Cabeçalho do lote
            String xmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xmlString += "<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + "2.00" + "\">";
            xmlString += "<idLote>" + numeroLote.ToString("000000000000000") + "</idLote>";

            //Adiciona as notas no lote
            for (int i = 0; i < this.NotaList.Count; i++)
            {
                //Converte o Xml de uma nota em texto
                String notaString = this.NotaList[i];

                //Identifica somente o conteudo entre a tag <NFe>
                int inicioTag = notaString.IndexOf("<NFe");
                int fimTag = notaString.Length - inicioTag;

                //Adiciona no arquivo de lote
                xmlString += notaString.Substring(inicioTag, fimTag);
            }

            //Rodapé do lote
            xmlString += "</enviNFe>";

            return xmlString;
        }

    }
}
