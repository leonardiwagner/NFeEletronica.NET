using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Security.Cryptography.X509Certificates;

namespace Bll
{
    public class NFe
    {
        private List<String> NotaList = new List<String>();
        private X509Certificate2 Certificado = null;

        /// <summary>
        /// Adiciona uma nota no lote para envio
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        public void AdicionaNotaLote(String caminhoArquivo)
        {
            if (this.NotaList.Count >= 50)
            {
                throw new Exception("Limite máximo por lote é de 50 arquivos"); //Limitado pela sefaz
            }
            else
            {
                this.NotaList.Add(caminhoArquivo);
            }
        }

        /// <summary>
        /// Monta um lote para envio
        /// </summary>
        /// <param name="numeroLote"></param>
        /// <returns>Xml do lote</returns>
        public String MontaLote(int numeroLote)
        {
            //Cabeçalho do lote
            String xmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xmlString += "<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + "2.00" + "\">";
            xmlString += "<idLote>" + numeroLote.ToString("000000000000000") + "</idLote>";

            //Adiciona as notas no lote
            for (int i = 0; i < this.NotaList.Count; i++)
            {
                //Converte o Xml de uma nota em texto
                String notaString = this.NotaList[i];

                //Identifica somente o conteudo entre a tag <NFe>
                int inicioTag = notaString.IndexOf("<NFe");
                int fimTag = notaString.Length - inicioTag;

                //Adiciona no arquivo de lote
                xmlString += notaString.Substring(inicioTag, fimTag);
            }

            //Rodapé do lote
            xmlString += "</enviNFe>";

            return xmlString;
        }

        /// <summary>
        /// Envia as notas fiscais ao SEFAZ
        /// </summary>
        public void Enviar()
        {
            List<String> NotaAssinadaLista = null;

            //Assina as notas
            try
            {
                NotaAssinadaLista = this.AssinarNotaLista(this.NotaList);
            }
            catch (Exception Erro)
            {
                throw Erro;
            }


            //Faz um lote com as notas
            Bll.EnviarLote bllEnviarLote = new Bll.EnviarLote();
            String ArquivoLote = this.MontarXml(1, NotaAssinadaLista);

            //Salva o lote assinado
            String ArquivoNome = DateTime.Now.ToString("yyyyMMdd-mmHH-ss-fffff") + ".xml";
            String ArquivoAssinado = Bll.Arquivo.Salva(ArquivoLote, Bll.Util.ContentFolderAssinado + "\\" + ArquivoNome);

            Model.Envio envio = new Model.Envio();
            envio.LoteXml = new XmlDocument();
            envio.LoteXml.PreserveWhitespace = true;
            envio.LoteXml.Load(ArquivoAssinado);
            envio.Certificado = this.Certificado;

            //Valida o arquivo assinado de lote
            try
            {
                Bll.Xml bllValidaXml = new Bll.Xml();
                //Bll.Operacao bllOperacao = new Bll.Operacao();
                String ErroValidaXml = ""; // bllValidaXml.ValidaSchema(ArquivoAssinado, Bll.Operacao.Selecionar(Model.OperacaoType.EnvioLote).Schema);
                if (!String.IsNullOrEmpty(ErroValidaXml)) throw new Exception(ErroValidaXml);
            }
            catch (Exception Erro)
            {
                throw new Exception("Erro ao validar lote: " + Erro.Message);
            }

            Bll.Webservice bllWebservice = new Bll.Webservice();
            String recibo = bllWebservice.Enviar(envio);

            
            

        }

        public String MontarXml(int NumeroLote, List<String> NotaList)
        {
            //Cabeçalho do lote
            String XmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            XmlString += "<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + "2.00" + "\">";
            XmlString += "<idLote>" + NumeroLote.ToString("000000000000000") + "</idLote>";

            //Adiciona as notas no lote
            for (int i = 0; i < NotaList.Count; i++)
            {
                //Converte o Xml de uma nota em texto
                String NotaString = NotaList[i];

                //Identifica somente o conteudo entre a tag <NFe>
                int inicioTag = NotaString.IndexOf("<NFe");
                int fimTag = NotaString.Length - inicioTag;

                //Adiciona no arquivo de lote
                XmlString += NotaString.Substring(inicioTag, fimTag);
            }

            //Rodapé do lote
            XmlString += "</enviNFe>";

            return XmlString;
        }

        private List<String> AssinarNotaLista(List<String> NotaLista)
        {
            //Verifica se já tem um certificado para assinar as notas
            if (this.Certificado == null)
            {
                //Caso não tenha, dispara uma janela do windows para selecionar
                Bll.Certificado bllCertificado = new Bll.Certificado();
                this.Certificado = bllCertificado.GetByWindows();

                //Caso nenhum certificado for selecionado, dispara o erro
                if (this.Certificado == null)
                    throw new Exception("Erro ao selecionar certificado: Nenhum certificado selecionado");
            }

            //Assina todas as notas
            List<String> NotaAssinadaLista = new List<string>();
            //Bll.Operacao bllOperacao = new Bll.Operacao();
            Bll.Assinatura bllAssinatura = new Bll.Assinatura();
            for (int i = 0; i < this.NotaList.Count; i++)
            {
                //Assina a nota
                String ArquivoAssinado = "";
                try
                {
                    ArquivoAssinado = ""; // bllAssinatura.AssinarXml(NotaLista[i], bllOperacao.Selecionar(Model.OperacaoType.NFe), this.Certificado);
                }
                catch (Exception Erro)
                {
                    throw new Exception("Erro ao assinar a nota (" + NotaLista[i] + "): " + Erro.Message);
                }

                //Adiciona a uma lista de notas assinadas
                NotaAssinadaLista.Add(ArquivoAssinado);
            }

            //Retorna a lista de notas assinadas
            return NotaAssinadaLista;
        }

        /// <summary>
        /// Escolhe um certificado para assinar as notas do lote
        /// </summary>
        /// <param name="Certificado"></param>
        public void SetCertificado(X509Certificate2 Certificado)
        {
            this.Certificado = Certificado;
        }

    }
}
