using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bll.Servicos
{
    /// <summary>
    /// Consulta Processamento de Lote de NF-e
    /// </summary>
    public class RetRecepcao
    {
        public void NfeRetRecepcao2(String numeroRecibo)
        {
            //Monta corpo do xml de envio
            StringBuilder xmlString = new StringBuilder();
            xmlString.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xmlString.Append("<consReciNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"2.00\">");
            xmlString.Append("    <tpAmb>2</tpAmb>");
            xmlString.Append("    <nRec>" + numeroRecibo + "</nRec>");
            xmlString.Append("</consReciNFe>");

            

            throw new NotImplementedException();
        }
    }
}
