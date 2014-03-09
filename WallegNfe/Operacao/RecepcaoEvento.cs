using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallegNFe.Operacao
{
    public class RecepcaoEvento : BaseOperacao
    {
        private List<Model.Nota2> NotaLista = new List<Model.Nota2>();
        private long NumeroLote = 0;

        public RecepcaoEvento(WallegNFe.NFeContexto nfe)
            : base(nfe) 
        {
            //this.ArquivoSchema = "nfe_v2.00.xsd";
        }

        public void Cancelar(Model.Evento eventoCancelamento , String caminhoXml)
        {
            String tpEvento = "110111";
            String id = "ID" + tpEvento + eventoCancelamento.ChaveAcesso + "1";
            
            
            StringBuilder xmlString = new StringBuilder();
            xmlString.Append("<envEvento versao=\"1.00\" xmlns=\"http://www.portalfiscal.inf.br/nfe\">");
            xmlString.Append("	<idLote>0131318</idLote>");
            xmlString.Append("	<evento xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + this.NFeContexto.VersaoString + "\">");
            xmlString.Append("		<infEvento Id=\"" + id  + "\">");
            xmlString.Append("			<cOrgao>41</cOrgao>");
            xmlString.Append("			<tpAmb>" +  (this.NFeContexto.Producao ? "1" : "2") +  "</tpAmb>");
            xmlString.Append("			<CNPJ>" + eventoCancelamento.CNPJ + "</CNPJ>");
            xmlString.Append("			<chNFe>" + eventoCancelamento.ChaveAcesso   + "</chNFe>");
            xmlString.Append("			<dhEvento>"  + DateTime.Now.ToFileTimeUtc()  + "</dhEvento>"); //2012-09-13T10:46:57-03:00
            xmlString.Append("			<tpEvento>" + tpEvento  + "</tpEvento>");
            xmlString.Append("			<nSeqEvento>1</nSeqEvento>");
            xmlString.Append("			<verEvento>1.00</verEvento>");
            xmlString.Append("			<detEvento versao=\"" + this.NFeContexto.VersaoString + "\">");
            xmlString.Append("				<descEvento>Cancelamento</descEvento>");
            xmlString.Append("				<nProt>" + eventoCancelamento.Protocolo + "</nProt>");
            xmlString.Append("				<xJust>" + eventoCancelamento.Justificativa + "</xJust>");
            xmlString.Append("			</detEvento>");
            xmlString.Append("		</infEvento>");
            //xmlString.Append("		<Signature></Signature>");
            xmlString.Append("	</evento>");
            xmlString.Append("</envEvento>");


            System.IO.StreamWriter SW_2 = System.IO.File.CreateText(caminhoXml);
            SW_2.Write(xmlString.ToString());
            SW_2.Close();

            Bll.Assinatura bllAssinatura = new Bll.Assinatura();

            //Assina a nota
            try
            {
                bllAssinatura.AssinarXml(new Nota(null) { NotaId = eventoCancelamento.ChaveAcesso, CaminhoFisico = caminhoXml }, this.NFeContexto.Certificado, "envEvento");

            }
            catch (Exception e)
            {
                throw new Exception("Erro ao assinar Nota: " + e.Message);
            }
            
        }
    }
}
