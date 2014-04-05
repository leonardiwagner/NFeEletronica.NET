using System;
using System.Text;
using System.Threading;
using System.Xml;
using WallegNFe.Bll;
using WallegNFe.Retorno;
using WallegNFe.NfeRetRecepcao2;

namespace WallegNFe.Operacao
{
    /// <summary>
    ///     Consulta Processamento de Lote de NF-e
    /// </summary>
    public class RetRecepcao : BaseOperacao
    {
        public RetRecepcao(NFeContexto nfe)
            : base(nfe)
        {
        }

        public Retorno.RetRecepcao Enviar(String numeroRecibo, String cUF)
        {
            //Monta corpo do xml de envio
            var xmlString = new StringBuilder();
            xmlString.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xmlString.Append("<consReciNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" +
                             NFeContexto.Versao.VersaoString + "\">");
            xmlString.Append("    <tpAmb>" + (NFeContexto.Producao ? "1" : "2") + "</tpAmb>");
            xmlString.Append("    <nRec>" + numeroRecibo + "</nRec>");
            xmlString.Append("</consReciNFe>");

            XmlNode consultaXml = Xml.StringToXml(xmlString.ToString());


            var nfeRetRecepcao2 = new NfeRetRecepcao2.NfeRetRecepcao2();
            var nfeCabecalho = new nfeCabecMsg();

            //Informa dados no WS de cabecalho
            nfeCabecalho.cUF = cUF;
            nfeCabecalho.versaoDados = NFeContexto.Versao.VersaoString;

            nfeRetRecepcao2.nfeCabecMsgValue = nfeCabecalho;
            nfeRetRecepcao2.ClientCertificates.Add(NFeContexto.Certificado);


            var retorno = new Retorno.RetRecepcao();
            XmlNode respostaXml = null;

            bool isEmProcessamento = true;

            //Verifica a resposta de envio da sefaz e aguarda até quando estiver processado
            do
            {
                respostaXml = nfeRetRecepcao2.nfeRetRecepcao2(consultaXml);

                //Esse e o resultado só do lote (cabeçado e tal)
                retorno.Status = respostaXml["cStat"].InnerText;
                retorno.Motivo = respostaXml["xMotivo"].InnerText;

                if (retorno.Status != "105")
                {
                    isEmProcessamento = false;
                }
                else
                {
                    Thread.Sleep(5000);
                }
            } while (isEmProcessamento);


            if (retorno.Status != "225")
            {
                //Isso aqui é o resultado de CADA NFe, mas como por enquanto pra cada lote só manda 1 nota, entao segue assim por enquanto #todo
                if (retorno.Status != "100" && retorno.Status != "104")
                {
                    throw new Exception("Lote não processado: " + retorno.Status + " - " + retorno.Motivo);
                }
                String protocolo = "";
                String status = "";
                String motivo = "";

                try
                {
                    motivo = respostaXml["protNFe"]["infProt"]["xMotivo"].InnerText;
                    status = respostaXml["protNFe"]["infProt"]["cStat"].InnerText;
                    protocolo = respostaXml["protNFe"]["infProt"]["nProt"].InnerText;
                }
                catch
                {
                }

                //Caso deu algum problema e nao veio o protocolo, mas veio a descrição do problema
                if (String.IsNullOrEmpty(protocolo) && (!String.IsNullOrEmpty(status) && !String.IsNullOrEmpty(motivo)))
                {
                    throw new Exception("Erro de retorno: " + status + " - " + motivo);
                }

                try
                {
                    return new Retorno.RetRecepcao()
                    {
                        Motivo = respostaXml["protNFe"]["infProt"]["xMotivo"].InnerText,
                        NumeroNota = respostaXml["protNFe"]["infProt"]["chNFe"].InnerText,
                        Protocolo = respostaXml["protNFe"]["infProt"]["nProt"].InnerText,
                        Status = respostaXml["protNFe"]["infProt"]["cStat"].InnerText
                    };
                }
                catch (Exception e)
                {
                    throw new Exception("Erro ler resposta de envio: " + e.Message);
                }

                return retorno;
            }
            throw new Exception("Erro ao enviar lote XML: " + retorno.Motivo);
        }
    }
}