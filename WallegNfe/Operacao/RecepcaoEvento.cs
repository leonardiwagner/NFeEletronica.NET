using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WallegNFe.Bll;
using WallegNFe.Model;

namespace WallegNFe.Operacao
{
    public class RecepcaoEvento : BaseOperacao
    {
        private readonly String ArquivoSchema = "";
        private long NumeroLote = 0;
        private Nota nota;

        public RecepcaoEvento(NFeContexto nfe)
            : base(nfe)
        {
            //this.ArquivoSchema = "nfe_v2.00.xsd";
            ArquivoSchema = "envEventoCancNFe_v1.00.xsd";

        }

        public void Cancelar(String contenudoXml)
        {
            var bllXml = new Xml();

            var xmlString = new StringBuilder();
            xmlString.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xmlString.Append("<envEvento versao=\"1.00\" xmlns=\"http://www.portalfiscal.inf.br/nfe\">");
            xmlString.Append("	<idLote>0131318</idLote>");
            xmlString.Append(contenudoXml);
            xmlString.Append("</envEvento>");

            StreamWriter SW_2 = File.CreateText(nota.CaminhoFisico);
            SW_2.Write(xmlString.ToString());
            SW_2.Close();

            //Verifica se a nota está de acordo com o schema, se não estiver vai disparar um erro
            try
            {
                
                bllXml.ValidaSchema(nota.CaminhoFisico,
                    Util.ContentFolderSchemaValidacao + "\\" + NFeContexto.Versao.PastaXML + "\\" + ArquivoSchema);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao validar Nota: " + e.Message);
            }

            
            var documentXml = Xml.StringToXml(xmlString.ToString());

            var recepcao = new RecepcaoEvento2.RecepcaoEvento();
            var cabecalho = new RecepcaoEvento2.nfeCabecMsg();
            cabecalho.cUF = "35";
            cabecalho.versaoDados = "1.00";

            recepcao.nfeCabecMsgValue = cabecalho;
            recepcao.ClientCertificates.Add(NFeContexto.Certificado);

            var resposta = recepcao.nfeRecepcaoEvento(documentXml);
            var retorno = new Model.Retorno.RetRecepcao();

            retorno.Status = resposta["cStat"].InnerText;
            retorno.Motivo = resposta["xMotivo"].InnerText;



        }

        public void AdicionarCancelamento(Evento eventoCancelamento, String caminhoXml)
        {
            String tpEvento = "110111";
            String id = "ID" + tpEvento + eventoCancelamento.ChaveAcesso + "01";


            var xmlString = new StringBuilder();
          //  xmlString.Append("<envEvento versao=\"1.00\" xmlns=\"http://www.portalfiscal.inf.br/nfe\">");
          //  xmlString.Append("	<idLote>0131318</idLote>");
            xmlString.Append("	<evento xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.00\">");
            xmlString.Append("		<infEvento Id=\"" + id + "\">");
            xmlString.Append("			<cOrgao>35</cOrgao>");
            xmlString.Append("			<tpAmb>" + (NFeContexto.Producao ? "1" : "2") + "</tpAmb>");
            xmlString.Append("			<CNPJ>" + eventoCancelamento.CNPJ + "</CNPJ>");
            xmlString.Append("			<chNFe>" + eventoCancelamento.ChaveAcesso + "</chNFe>");
            xmlString.Append("			<dhEvento>" + "2014-03-30T22:46:57-03:00" + "</dhEvento>");
                //2012-09-13T10:46:57-03:00
            xmlString.Append("			<tpEvento>" + tpEvento + "</tpEvento>");
            xmlString.Append("			<nSeqEvento>1</nSeqEvento>");
            xmlString.Append("			<verEvento>1.00</verEvento>");
            xmlString.Append("			<detEvento versao=\"1.00\">");
            xmlString.Append("				<descEvento>Cancelamento</descEvento>");
            xmlString.Append("				<nProt>" + eventoCancelamento.Protocolo + "</nProt>");
            xmlString.Append("				<xJust>" + eventoCancelamento.Justificativa + "</xJust>");
            xmlString.Append("			</detEvento>");
            xmlString.Append("		</infEvento>");
            //xmlString.Append("		<Signature></Signature>");
            xmlString.Append("	</evento>");
          //  xmlString.Append("</envEvento>");


            StreamWriter SW_2 = File.CreateText(caminhoXml);
            SW_2.Write(xmlString.ToString());
            SW_2.Close();

            var bllAssinatura = new Assinatura();
            this.nota = new Nota(this.NFeContexto) {CaminhoFisico = caminhoXml};

          
           
            //Assina a nota
            try
            {
                
                bllAssinatura.AssinarXml(
                    nota ,
                    NFeContexto.Certificado, "evento", "#" + id);
                 
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao assinar Nota: " + e.Message);
            }

            

            this.Cancelar(nota.ConteudoXml);
        }
    }
}