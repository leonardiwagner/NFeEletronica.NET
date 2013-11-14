using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Trabalhar com o certificado
using System.Security.Cryptography.X509Certificates;

//Trabalhar com o xml
using System.Xml;

namespace WallegNfe.Operacao
{
    public class Recepcao
    {
        private readonly String ArquivoSchema = "nfe_v2.00.xsd";
        private X509Certificate2 Certificado = null;

        private List<Model.Nota> NotaLista = new List<Model.Nota>();

        public Recepcao(X509Certificate2 certificado)
        {
            this.Certificado = certificado;
        }

        /// <summary>
        /// Adiciona e valida uma nota ao lote para ser enviada. Caso for inválida irá gerar um erro.
        /// </summary>
        /// <param name="arquivoCaminhoXml"></param>
        public void AdicionarNota(String arquivoCaminhoXml)
        {
            Bll.Nota bllNota = new Bll.Nota();
            Bll.Assinatura bllAssinatura = new Bll.Assinatura();
            Bll.Xml bllXml = new Bll.Xml();

            //Verifica se já passou o limite de notas por lote (regra do SEFAZ). 
            if (this.NotaLista.Count >= 50)
            {
                throw new Exception("Limite máximo por lote é de 50 arquivos"); 
            }

            //Carrega uma nota XML e passa para um objeto Nota
            Model.Nota nota = bllNota.Carregar(arquivoCaminhoXml);

            //Assina a nota
            bllAssinatura.AssinarXml(nota.ArquivoFisicoCaminho, this.Certificado, "NFe", "infNFe");

            //Verifica se a nota está de acordo com o schema, se não estiver vai disparar um erro
            bllXml.ValidaSchema(arquivoCaminhoXml, Bll.Util.ContentFolderSchemaValidacao + "\\" + this.ArquivoSchema);

            //Se passou está validado, manda para a pasta de validados
            Bll.Nota.Move(nota, Model.NotaSituacao.VALIDADA);

            //Adiciona para a lista do lote a serem enviadas
            this.NotaLista.Add(nota);
        }

        public WallegNfe.Retorno Enviar(long numeroLote)
        {
            //Salva o lote assinado
            String ArquivoNome = DateTime.Now.ToString("yyyyMMdd-mmHH-ss-fffff") + ".xml";

            NfeRecepcao2.NfeRecepcao2 nfeRecepcao2 = new NfeRecepcao2.NfeRecepcao2();
            NfeRecepcao2.nfeCabecMsg nfeCabecalho = new NfeRecepcao2.nfeCabecMsg();

            //Informa dados no WS de cabecalho
            nfeCabecalho.cUF = "35";
            nfeCabecalho.versaoDados = "2.00";

            nfeRecepcao2.nfeCabecMsgValue = nfeCabecalho;
            nfeRecepcao2.ClientCertificates.Add(this.Certificado);

            //Envia para o webservice e recebe a resposta
            XmlNode xmlResposta = nfeRecepcao2.nfeRecepcaoLote2(this.MontarXml(numeroLote).DocumentElement);

            for (int i = 0; i < this.NotaLista.Count; i++)
            {
                Bll.Nota.Move(this.NotaLista[i], Model.NotaSituacao.ENVIADA);
            }

            WallegNfe.Retorno retorno = new WallegNfe.Retorno();
            retorno.Recibo = xmlResposta["infRec"]["nRec"].InnerText;

            return retorno;
        }

        private XmlDocument MontarXml(long numeroLote)
        {

            //Cabeçalho do lote
            String xmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xmlString += "<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + "2.00" + "\">";
            xmlString += "<idLote>" + numeroLote.ToString("000000000000000") + "</idLote>";

            //Adiciona as notas no lote
            for (int i = 0; i < this.NotaLista.Count; i++)
            {
                //Converte o Xml de uma nota em texto
                String NotaString = this.NotaLista[i].ArquivoFisicoLer();

                //Identifica somente o conteudo entre a tag <NFe>
                int inicioTag = NotaString.IndexOf("<NFe");
                int fimTag = NotaString.Length - inicioTag;

                //Adiciona no arquivo de lote
                xmlString += NotaString.Substring(inicioTag, fimTag);


                //Move a nota para assinadas
                Bll.Nota.Move(this.NotaLista[i], Model.NotaSituacao.ASSINADA);
            }

            //Rodapé do lote
            xmlString += "</enviNFe>";

            return Bll.Xml.StringToXml(xmlString);
        }

        public void NfeRecepcaoLote2()
        {
            throw new NotImplementedException();
        }
    }
}
