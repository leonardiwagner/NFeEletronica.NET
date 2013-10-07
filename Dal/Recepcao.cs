using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Xml;

using System.Security.Cryptography.X509Certificates;

namespace Dal
{
    public class Recepcao
    {

        public String Envia(Model.Envio envio)
        {
            Dal.NfeRecepcao.NfeRecepcao2 nfeRecepcao = new Dal.NfeRecepcao.NfeRecepcao2();
            Dal.NfeRecepcao.nfeCabecMsg nfeCabecalho = new Dal.NfeRecepcao.nfeCabecMsg();

            

            //Informa dados no WS de cabecalho
            nfeCabecalho.cUF = "35";
            nfeCabecalho.versaoDados = "2.00";

            //Informa dados no WS de recepcao
            nfeRecepcao.nfeCabecMsgValue = nfeCabecalho;
            nfeRecepcao.ClientCertificates.Add(envio.Certificado);


            

            //Inclui no xml estado e versao no xml
            XmlNode xmlDados = envio.LoteXml.DocumentElement;


            XmlNode no_resp;
            no_resp = nfeRecepcao.nfeRecepcaoLote2(xmlDados);
            String rec = no_resp["infRec"]["nRec"].InnerText;

            return rec;
        }
    }
}
