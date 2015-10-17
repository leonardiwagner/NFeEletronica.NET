using System;
using System.Text;

namespace NFeEletronica.Operacao
{
    public class StatusServico
    {
        public void NfeStatusServico2()
        {
            //todo: precisa implementar essa operação

            //Monta corpo do xml de envio
            var xmlString = new StringBuilder();
            xmlString.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xmlString.Append("<consStatServ versao=\"2.00\" xmlns=\"http://www.portalfiscal.inf.br/nfe\">");
            xmlString.Append("    <tpAmp>2</tpAmb>");
            xmlString.Append("    <cUF>35</cUF>");
            xmlString.Append("    <xServ>STATUS</xServ>");
            xmlString.Append("</consStatServ>");

            throw new NotImplementedException();
        }
    }
}