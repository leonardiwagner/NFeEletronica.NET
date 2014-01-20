using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

using System.Security.Cryptography.X509Certificates;

namespace WallegNfe.Operacao
{
    /// <summary>
    /// Consulta Processamento de Lote de NF-e
    /// </summary>
    public class RetRecepcao : BaseOperacao
    {
        public RetRecepcao(WallegNfe.Nfe nfe)
            : base(nfe) 
        {}

        public WallegNfe.Model.Retorno.RetRecepcao  Enviar(String numeroRecibo)
        {
            //Monta corpo do xml de envio
            StringBuilder xmlString = new StringBuilder();
            xmlString.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xmlString.Append("<consReciNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"2.00\">");
            xmlString.Append("    <tpAmb>2</tpAmb>");
            xmlString.Append("    <nRec>" + numeroRecibo + "</nRec>");
            xmlString.Append("</consReciNFe>");

            XmlNode consultaXml = Bll.Xml.StringToXml(xmlString.ToString());

            NfeRetRecepcao2.NfeRetRecepcao2 nfeRetRecepcao2 = new NfeRetRecepcao2.NfeRetRecepcao2();
            NfeRetRecepcao2.nfeCabecMsg nfeCabecalho = new NfeRetRecepcao2.nfeCabecMsg();

            //Informa dados no WS de cabecalho
            nfeCabecalho.cUF = "35";
            nfeCabecalho.versaoDados = "2.00";

            nfeRetRecepcao2.nfeCabecMsgValue = nfeCabecalho;
            nfeRetRecepcao2.ClientCertificates.Add(this.Certificado);

            XmlNode respostaXml = nfeRetRecepcao2.nfeRetRecepcao2(consultaXml);

            //Esse e o resultado só do lote (cabeçado e tal)
            WallegNfe.Model.Retorno.RetRecepcao retorno = new WallegNfe.Model.Retorno.RetRecepcao();
            retorno.Status = respostaXml["cStat"].InnerText;
            retorno.Motivo = respostaXml["xMotivo"].InnerText;

            //Isso aqui é o resultado de CADA NFe, mas como por enquanto pra cada lote só manda 1 nota, entao segue assim por enquanto #todo
            retorno.Status = respostaXml["protNFe"]["infProt"]["cStat"].InnerText;
            retorno.Motivo = respostaXml["protNFe"]["infProt"]["xMotivo"].InnerText;

            return retorno;
            
        }

    }
}
