using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallegNfe.Operacao
{
    public class RecepcaoEvento : BaseOperacao
    {
        private List<Model.Nota2> NotaLista = new List<Model.Nota2>();
        private long NumeroLote = 0;

        public RecepcaoEvento(WallegNfe.Nfe nfe)
            : base(nfe) 
        {
            //this.ArquivoSchema = "nfe_v2.00.xsd";
        }

        public void Enviar(Model.Cancelamento cancelamento)
        {
            
            
            StringBuilder xmlString = new StringBuilder();
            xmlString.Append("<envEvento versao=\"1.00\" xmlns=\"http://www.portalfiscal.inf.br/nfe\">");
            xmlString.Append("	<idLote>0131318</idLote>");
            xmlString.Append("	<evento xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.00\">");
            xmlString.Append("		<infEvento Id=\"ID1101114112097895696800018355005000003869112230706001\">");
            xmlString.Append("			<cOrgao>41</cOrgao>");
            xmlString.Append("			<tpAmb>2</tpAmb>");
            xmlString.Append("			<CNPJ>" +  cancelamento.CPFCNPJ + "</CNPJ>");
            xmlString.Append("			<chNFe>41120978956968000183550050000038691122307060</chNFe>");
            xmlString.Append("			<dhEvento>2012-09-13T10:46:57-03:00</dhEvento>");
            xmlString.Append("			<tpEvento>110111</tpEvento>");
            xmlString.Append("			<nSeqEvento>1</nSeqEvento>");
            xmlString.Append("			<verEvento>1.00</verEvento>");
            xmlString.Append("			<detEvento versao=\"1.00\">");
            xmlString.Append("				<descEvento>Cancelamento</descEvento>");
            xmlString.Append("				<nProt>141120001011473</nProt>");
            xmlString.Append("				<xJust>teste cancelamento por evento</xJust>");
            xmlString.Append("			</detEvento>");
            xmlString.Append("		</infEvento>");
            xmlString.Append("		<Signature></Signature>");
            xmlString.Append("	</evento>");
            xmlString.Append("</envEvento>");
            
        }
    }
}
