using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bll.Servicos
{
    public class Inutilizacao
    {
        public void NfeInutilizacaoNF2()
        {
            //Monta corpo do xml de envio
            StringBuilder xmlString = new StringBuilder();
            xmlString.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xmlString.Append("<infInut xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"2.00\">");
            xmlString.Append("    <Id>2</Id>");
            xmlString.Append("    <tpAmp>2</tpAmb>");
            xmlString.Append("    <xServ>INUTILIZAR</xServ>");
            xmlString.Append("    <cUf></cUf>");
            xmlString.Append("    <ano></ano>");
            xmlString.Append("    <CNPJ></CNPJ>");
            xmlString.Append("    <mod></mod>");
            xmlString.Append("    <serie></serie>");
            xmlString.Append("    <nNFIni></nNFIni>");
            xmlString.Append("    <nNFFin></nNFFin>");
            xmlString.Append("    <xJust></xJust>");
            xmlString.Append("    <Signature></Signature>");
            xmlString.Append("</consReciNFe>");
        }
    }
}
