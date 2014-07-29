using System;
using System.Collections.Generic;
using System.Xml;
using WallegNFe.NfeRecepcao2;
using WallegNFe.Versao;

namespace WallegNFe.Operacao
{
    public class Recepcao : BaseOperacao
    {
        private readonly String arquivoSchema = "";
        private readonly List<Nota> notaLista = new List<Nota>();
        private readonly bool sincrono;

        public Recepcao(INFeContexto nfeContexto) : base(nfeContexto)
        {
            arquivoSchema = "nfe_v" + nfeContexto.Versao.VersaoString + ".xsd";

            sincrono = false;
        }

        /// <summary>
        ///     Adiciona e valida uma nota a ser enviada.
        /// </summary>
        /// <param name="nota"></param>
        public void AdicionarNota(Nota nota)
        {
            var bllAssinatura = new AssinaturaDeXml();
            var bllXml = new Xml();

            //Verifica se já passou o limite de notas por lote (regra do SEFAZ). 
            if (notaLista.Count >= 50)
            {
                throw new Exception("Limite máximo por lote é de 50 arquivos");
            }

            //Assina a nota
            try
            {
                bllAssinatura.AssinarNota(nota, NFeContexto.Certificado, "NFe");
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao assinar Nota: " + e.Message);
            }

            //Verifica se a nota está de acordo com o schema, se não estiver vai disparar um erro
            try
            {
                bllXml.ValidaSchema(nota.CaminhoFisico,
                    Util.ContentFolderSchemaValidacao + "\\" + NFeContexto.Versao.PastaXml + "\\" + arquivoSchema);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao validar Nota: " + e.Message);
            }

            //Adiciona para a lista do lote a serem enviadas
            notaLista.Add(nota);
        }

        /// <summary>
        ///     Adiciona e valida uma nota a ser enviada apartir de um arquivo XML.
        /// </summary>
        /// <param name="arquivoCaminhoXml"></param>
        public void AdicionarNota(String arquivoCaminhoXml)
        {
            //Carrega uma nota XML e passa para um objeto Nota
            var nota = new Nota(arquivoCaminhoXml);

            AdicionarNota(nota);
        }

        private XmlDocument MontarXml(long numeroLote)
        {
            //Cabeçalho do lote
            String xmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xmlString += "<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" +
                         NFeContexto.Versao.VersaoString +
                         "\">";

            xmlString += "<idLote>" + numeroLote.ToString("000000000000000") + "</idLote>";

            if (NFeContexto.Versao == NFeVersao.VERSAO_3_1_0)
            {
                if (sincrono)
                {
                    xmlString += "<indSinc>1</indSinc>";
                }
                else
                {
                    xmlString += "<indSinc>0</indSinc>";
                }
            }

            //Adiciona as notas no lote
            for (int i = 0; i < notaLista.Count; i++)
            {
                //Converte o Xml de uma nota em texto
                String NotaString = notaLista[i].ConteudoXml;

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

        public Retorno.Recepcao Enviar(long numeroLote, String cUF)
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

            var recibo = xmlResposta["infRec"]["nRec"].InnerText;
            var motivo = xmlResposta["xMotivo"].InnerText;

            return new Retorno.Recepcao(recibo, "", motivo);
        }
    }
}