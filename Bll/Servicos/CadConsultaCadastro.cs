using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bll.Servicos
{
    public class CadConsultaCadastro
    {
        public void ConsultaCadastro2()
        {
            //Monta corpo do xml de envio
            StringBuilder xmlString = new StringBuilder();
            xmlString.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xmlString.Append("<ConsCad xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"2.00\">");
            xmlString.Append("    <infCons>");
            xmlString.Append("        <xServ>CONS-CAD</xServ>");
            xmlString.Append("        <UF>SP</UF>");
            xmlString.Append("        <CNPJ>58151556000169</CNPJ>");
            xmlString.Append("    </infCons>");
            xmlString.Append("</ConsCad>");

            throw new NotImplementedException();
        }
    }
}
