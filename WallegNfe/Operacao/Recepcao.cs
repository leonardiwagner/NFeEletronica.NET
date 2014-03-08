using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Trabalhar com o certificado
using System.Security.Cryptography.X509Certificates;

//Trabalhar com o xml
using System.Xml;

namespace WallegNFe.Operacao
{
    public class Recepcao : BaseOperacao
    {
        private List<WallegNFe.Nota> NotaLista = new List<WallegNFe.Nota>();

        public Recepcao(WallegNFe.NfeContexto nfe) : base(nfe) 
        {
            this.ArquivoSchema = "nfe_v3.10.xsd";
        }
        
        /// <summary>
        /// Adiciona e valida uma nota a ser enviada.
        /// </summary>
        /// <param name="nota"></param>
        public void AdicionarNota(WallegNFe.Nota nota)
        {
            
            Bll.Assinatura bllAssinatura = new Bll.Assinatura();
            Bll.Xml bllXml = new Bll.Xml();

            //Verifica se já passou o limite de notas por lote (regra do SEFAZ). 
            if (this.NotaLista.Count >= 50)
            {
                throw new Exception("Limite máximo por lote é de 50 arquivos");
            }

            //Assina a nota
            try
            {
                bllAssinatura.AssinarXml(nota, this.Certificado, "NFe");

            }
            catch (Exception e)
            {
                throw new Exception("Erro ao assinar Nota: " + e.Message);
            }

            //Verifica se a nota está de acordo com o schema, se não estiver vai disparar um erro
            try
            {
                bllXml.ValidaSchema(nota.CaminhoFisico, Bll.Util.ContentFolderSchemaValidacao + "\\" + this.ArquivoSchema);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao validar Nota: " + e.Message);
            }

            //Adiciona para a lista do lote a serem enviadas
            this.NotaLista.Add(nota);
        }

        /// <summary>
        /// Adiciona e valida uma nota a ser enviada apartir de um arquivo XML.
        /// </summary>
        /// <param name="arquivoCaminhoXml"></param>
        public void AdicionarNota(String arquivoCaminhoXml)
        {
            //Carrega uma nota XML e passa para um objeto Nota
            Bll.Nota bllNota = new Bll.Nota();
            WallegNFe.Nota nota = bllNota.Carregar(arquivoCaminhoXml);

            this.AdicionarNota(nota);
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
                String NotaString = this.NotaLista[i].ConteudoXml;

                //Identifica somente o conteudo entre a tag <NFe>
                int inicioTag = NotaString.IndexOf("<NFe");
                int fimTag = NotaString.Length - inicioTag;

                //Adiciona no arquivo de lote
                xmlString += NotaString.Substring(inicioTag, fimTag);
            }

            //Rodapé do lote
            xmlString += "</enviNFe>";


            Bll.Xml bllXml = new Bll.Xml();

            // Gravar o XML Assinado no HD
            String SignedFile = "C:\\NFE\\lote-fixo.xml";
            System.IO.StreamWriter SW_2 = System.IO.File.CreateText(SignedFile);
            SW_2.Write(xmlString);
            SW_2.Close();

            //Verifica se a nota está de acordo com o schema, se não estiver vai disparar um erro
            try
            {
                bllXml.ValidaSchema("C:\\NFE\\lote-fixo.xml", Bll.Util.ContentFolderSchemaValidacao + "\\enviNFe_v2.00.xsd");
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao validar Nota: " + e.Message);
            }

            return Bll.Xml.StringToXml(xmlString);
        }

        public Model.Retorno.Recepcao Enviar(long numeroLote, String cUF)
        {
            NfeRecepcao2.NfeRecepcao2 nfeRecepcao2 = new NfeRecepcao2.NfeRecepcao2();
            NfeRecepcao2.nfeCabecMsg nfeCabecalho = new NfeRecepcao2.nfeCabecMsg();

            //Informa dados no WS de cabecalho
            nfeCabecalho.cUF = cUF;
            nfeCabecalho.versaoDados = "2.00";

            nfeRecepcao2.nfeCabecMsgValue = nfeCabecalho;
            nfeRecepcao2.ClientCertificates.Add(this.Certificado);

            //Envia para o webservice e recebe a resposta
            XmlNode xmlResposta = nfeRecepcao2.nfeRecepcaoLote2(this.MontarXml(numeroLote).DocumentElement);

            WallegNFe.Model.Retorno.Recepcao retorno = new WallegNFe.Model.Retorno.Recepcao();
            retorno.Recibo = xmlResposta["infRec"]["nRec"].InnerText;
            retorno.Motivo = xmlResposta["xMotivo"].InnerText;

           return retorno;
        }
    }
}
