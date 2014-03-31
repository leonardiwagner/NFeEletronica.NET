//Trabalhar com o certificado

//Trabalhar com o xml
using System;
using System.Collections.Generic;
using System.Xml;
using WallegNFe.Bll;
using WallegNFe.NfeRecepcao2;
using WallegNFe.Versao;

namespace WallegNFe.Operacao
{
    public class Recepcao : BaseOperacao
    {
        private readonly String ArquivoSchema = "";
        private readonly List<Nota> NotaLista = new List<Nota>();
        private readonly bool Sincrono;


        public Recepcao(NFeContexto nfeContexto) : base(nfeContexto)
        {
            
            ArquivoSchema = "nfe_v" +  nfeContexto.Versao.VersaoString + ".xsd";

            Sincrono = false;
        }

        /// <summary>
        ///     Adiciona e valida uma nota a ser enviada.
        /// </summary>
        /// <param name="nota"></param>
        public void AdicionarNota(Nota nota)
        {
            var bllAssinatura = new Assinatura();
            var bllXml = new Xml();

            //Verifica se já passou o limite de notas por lote (regra do SEFAZ). 
            if (NotaLista.Count >= 50)
            {
                throw new Exception("Limite máximo por lote é de 50 arquivos");
            }

            //Assina a nota
            try
            {
                bllAssinatura.AssinarXml(nota, NFeContexto.Certificado, "NFe");
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao assinar Nota: " + e.Message);
            }

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

            //Adiciona para a lista do lote a serem enviadas
            NotaLista.Add(nota);
        }

        /// <summary>
        ///     Adiciona e valida uma nota a ser enviada apartir de um arquivo XML.
        /// </summary>
        /// <param name="arquivoCaminhoXml"></param>
        public void AdicionarNota(String arquivoCaminhoXml)
        {
            //Carrega uma nota XML e passa para um objeto Nota
            Nota nota = new Nota(arquivoCaminhoXml);

            AdicionarNota(nota);
        }

        private XmlDocument MontarXml(long numeroLote)
        {
            //Cabeçalho do lote
            String xmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xmlString += "<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + NFeContexto.Versao.VersaoString +
                         "\">";

            xmlString += "<idLote>" + numeroLote.ToString("000000000000000") + "</idLote>";

            if (NFeContexto.Versao == NFeVersao.VERSAO_3_1_0)
            {
                if (Sincrono)
                {
                    xmlString += "<indSinc>1</indSinc>";
                }
                else
                {
                    xmlString += "<indSinc>0</indSinc>";
                }
            }

            //Adiciona as notas no lote
            for (int i = 0; i < NotaLista.Count; i++)
            {
                //Converte o Xml de uma nota em texto
                String NotaString = NotaLista[i].ConteudoXml;

                //Identifica somente o conteudo entre a tag <NFe>
                int inicioTag = NotaString.IndexOf("<NFe");
                int fimTag = NotaString.Length - inicioTag;

                //Adiciona no arquivo de lote
                xmlString += NotaString.Substring(inicioTag, fimTag);
            }

            //Rodapé do lote
            xmlString += "</enviNFe>";


            var bllXml = new Xml();
            return Xml.StringToXml(xmlString);
        }

        public Model.Retorno.Recepcao Enviar(long numeroLote, String cUF)
        {
            var nfeRecepcao2 = new NfeRecepcao2.NfeRecepcao2();
            var nfeCabecalho = new nfeCabecMsg();

            //Informa dados no WS de cabecalho
            nfeCabecalho.cUF = cUF;
            nfeCabecalho.versaoDados = NFeContexto.Versao.VersaoString;

            nfeRecepcao2.nfeCabecMsgValue = nfeCabecalho;
            nfeRecepcao2.ClientCertificates.Add(NFeContexto.Certificado);

            //Envia para o webservice e recebe a resposta
            XmlNode xmlResposta = nfeRecepcao2.nfeRecepcaoLote2(MontarXml(numeroLote).DocumentElement);

            var retorno = new Model.Retorno.Recepcao();
            retorno.Recibo = xmlResposta["infRec"]["nRec"].InnerText;
            retorno.Motivo = xmlResposta["xMotivo"].InnerText;

            return retorno;
        }
    }
}