using System;
using System.Text;

namespace NFeEletronica.Operacao
{
    public class CadConsultaCadastro
    {
        public void ConsultaCadastro2()
        {
            //todo: Precisa implementar essa operação
            var xmlString = new StringBuilder();
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