using System;
using System.IO;
using System.Reflection;
using System.Text;
using WallegNFe;
using WallegNFe.Bll;
using WallegNFe.NfeInutilizacao21;
using WallegNFe.Operacao;
using System.Xml;

namespace WallegNFe.Operacao
{
    public class Inutilizacao : BaseOperacao
    {
        public readonly String ArquivoSchema ;
        public Inutilizacao(NFeContexto nfeContexto) : base(nfeContexto)
        {
            ArquivoSchema = "inutNFe_v2.00.xsd";
        }

        public void NfeInutilizacaoNF2(WallegNFe.Consulta.Inutilizacao inutilizacao)
        {
            WallegNFe.NfeInutilizacao21.NfeInutilizacao2 webservice = new NfeInutilizacao2();
            var cabecalho = new WallegNFe.NfeInutilizacao21.nfeCabecMsg();

            cabecalho.cUF = inutilizacao.UF;
            cabecalho.versaoDados = NFeContexto.Versao.VersaoString;
            String id = "ID" + inutilizacao.UF + inutilizacao.Ano + inutilizacao.CNPJ + Int32.Parse(inutilizacao.Mod).ToString("D2") + Int32.Parse(inutilizacao.Serie).ToString("D3") + Int32.Parse(inutilizacao.NumeroNfeInicial).ToString("D9") + Int32.Parse(inutilizacao.NumeroNfeFinal).ToString("D9");
            //Monta corpo do xml de envio
            var xmlString = new StringBuilder();
            xmlString.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xmlString.Append("<inutNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"2.00\">");
            xmlString.Append("<infInut Id=\"" + id + "\">");
            xmlString.Append("    <tpAmb>" + (NFeContexto.Producao ? "1" : "2") + "</tpAmb>");
            xmlString.Append("    <xServ>INUTILIZAR</xServ>");
            xmlString.Append("    <cUF>" + inutilizacao.UF + "</cUF>");
            xmlString.Append("    <ano>" + inutilizacao.Ano + "</ano>");
            xmlString.Append("    <CNPJ>" + inutilizacao.CNPJ + "</CNPJ>");
            xmlString.Append("    <mod>" + inutilizacao.Mod + "</mod>");
            xmlString.Append("    <serie>" + inutilizacao.Serie + "</serie>");
            xmlString.Append("    <nNFIni>" + inutilizacao.NumeroNfeInicial + "</nNFIni>");
            xmlString.Append("    <nNFFin>" + inutilizacao.NumeroNfeFinal + "</nNFFin>");
            xmlString.Append("    <xJust>" + inutilizacao.Justificativa + "</xJust>");
            xmlString.Append("</infInut>");
            xmlString.Append("</inutNFe>");

            webservice.nfeCabecMsgValue = cabecalho;
            var resultado = webservice.nfeInutilizacaoNF2(Assinar(xmlString, id));
        }

        private XmlDocument Assinar(StringBuilder xmlStringBuilder, String id)
        {
            var bllXml = new WallegNFe.Bll.Xml();
            String arquivoTemporario = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\temp.xml";
            StreamWriter SW_2 = File.CreateText(arquivoTemporario);
            SW_2.Write(xmlStringBuilder.ToString());
            SW_2.Close();

            var nota = new Nota(this.NFeContexto) { CaminhoFisico = arquivoTemporario };
          
            //Assina a nota
            var bllAssinatura = new WallegNFe.Bll.Assinatura();
            try
            {

                bllAssinatura.AssinarXml(
                    nota,
                    NFeContexto.Certificado, "inutNFe", "#" + id);

            }
            catch (Exception e)
            {
                throw new Exception("Erro ao assinar Nota: " + e.Message);
            }


            //Verifica se a nota está de acordo com o schema, se não estiver vai disparar um erro
            try
            {

                bllXml.ValidaSchema(arquivoTemporario,
                    Util.ContentFolderSchemaValidacao + "\\" + NFeContexto.Versao.PastaXML + "\\" + ArquivoSchema);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao validar Nota: " + e.Message);
            }

           

            return Xml.StringToXml(xmlStringBuilder.ToString());
        }
    }
}