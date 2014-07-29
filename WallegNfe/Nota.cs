using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using WallegNFe.ModeloNota;
using WallegNFe.Versao;

namespace WallegNFe
{
    public class Nota
    {
        private readonly StringBuilder xmlString;
        private readonly List<DET> detList;
        private readonly INFeContexto nFeContexto;
        public String ArquivoNome = "";
        public String CaminhoFisico = "";
        public String ConteudoXml = "";
        public String NotaId = "";

        public Nota(INFeContexto nFeContexto)
        {
            this.nFeContexto = nFeContexto;

            ide = new IDE();
            emit = new EMIT();
            dest = new DEST();
            detList = new List<DET>();
            total = new TOTAL();
            transp = new TRANSP();
            cobr = new COBR();

            xmlString = new StringBuilder();

            if (this.nFeContexto.Producao)
                ide.tpAmb = "1";
            else
                ide.tpAmb = "2";
        }

        public Nota(String arquivoNotaXml)
        {
            if (!File.Exists(arquivoNotaXml))
            {
                throw new Exception("O arquivo de nota para envio não existe: " + arquivoNotaXml);
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(arquivoNotaXml);

            this.CaminhoFisico = arquivoNotaXml;
            this.ConteudoXml = xmlDoc.ToString();
        }

        public IDE ide { get; set; }
        public EMIT emit { get; set; }
        public DEST dest { get; set; }
        public TOTAL total { get; set; }
        public TRANSP transp { get; set; }
        public COBR cobr { get; set; }
        public String infAdic { get; set; }

        public void AddDet(DET det)
        {
            detList.Add(det);
        }


        public String GerarCodigoDaNota()
        {
            if (!nFeContexto.Producao)
            {
                emit.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
                dest.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
            }

            var random = new Random();
            String codigoNumerico = random.Next(10000000, 99999999).ToString("D8");
            ide.cNF = codigoNumerico;

            String result = ide.cUF + ide.dEmi.Replace("-", "").Substring(2, 4) + emit.CNPJ +
                            Int32.Parse(ide.mod).ToString("D2") + Int32.Parse(ide.serie).ToString("D3") +
                            Int32.Parse(ide.nNF).ToString("D9") + Int32.Parse(ide.tpEmis).ToString("D1") +
                            codigoNumerico;
            String digitoVerificador = Util.GerarModulo11(result);

            result = result + digitoVerificador;
            NotaId = result;

            ide.cDV = digitoVerificador;

            return NotaId;
        }

        public void SalvarNota(String caminho)
        {
            //GerarCodigoDaNota();


            xmlString.Append("<NFe xmlns=\"http://www.portalfiscal.inf.br/nfe\">");
            xmlString.Append("   <infNFe Id=\"NFe" + NotaId + "\" versao=\"" + nFeContexto.Versao.VersaoString + "\">");

            MontaIDE();
            MontaEMIT();
            MontaDEST();
            MontaDET();
            MontaTOTAL();
            MontaTRANSP();
            MontaCOBR();

            if (!String.IsNullOrEmpty(infAdic))
            {
                xmlString.Append("<infAdic>");
                xmlString.Append("	<infCpl>");
                xmlString.Append(infAdic);
                xmlString.Append("	</infCpl>");
                xmlString.Append("</infAdic>");
            }

            xmlString.Append("   </infNFe>");
            //this.XmlString.Append("   <Signature></Signature>"); acho que não precisa disso
            xmlString.Append("</NFe>");

            var xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.LoadXml(xmlString.ToString().Replace("&", "&amp;"));
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao gerar a nota como XML 2: " + e.Message);
            }

            try
            {
                xmlDocument.Save(caminho);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao salvar a nota como XML: " + e.Message);
            }

            CaminhoFisico = caminho;
            ConteudoXml = xmlString.ToString();
        }

        private void MontaIDE()
        {
            xmlString.Append("<ide>");
            xmlString.Append("	<cUF>" + ide.cUF + "</cUF>");
            xmlString.Append("	<cNF>" + ide.cNF + "</cNF>");
            xmlString.Append("	<natOp>" + ide.natOp + "</natOp>");
            xmlString.Append("	<indPag>" + ide.indPag + "</indPag>"); //0 - a vista, 1 - a prazo, 2 - outros
            xmlString.Append("	<mod>" + ide.mod + "</mod>");
            xmlString.Append("	<serie>" + ide.serie + "</serie>");
            xmlString.Append("	<nNF>" + ide.nNF + "</nNF>");

            if (nFeContexto.Versao == NFeVersao.VERSAO_3_1_0)
            {
                xmlString.Append("	<dhEmi>" + ide.dhEmi + "</dhEmi>");
            }
            else
            {
                xmlString.Append("	<dEmi>" + ide.dEmi + "</dEmi>");
            }

            if (!String.IsNullOrEmpty(ide.dhSaiEnt))
                xmlString.Append("	<dhSaiEnt>" + ide.dhSaiEnt + "</dhSaiEnt>");

            xmlString.Append("	<tpNF>" + ide.tpNF + "</tpNF>");

            if (nFeContexto.Versao == NFeVersao.VERSAO_3_1_0)
                xmlString.Append("	<idDest>" + ide.idDest + "</idDest>");

            xmlString.Append("	<cMunFG>" + ide.cMunFG + "</cMunFG>");
            xmlString.Append("	<tpImp>" + ide.tpImp + "</tpImp>");
            xmlString.Append("	<tpEmis>" + ide.tpEmis + "</tpEmis>");
            xmlString.Append("	<cDV>" + ide.cDV + "</cDV>");
            xmlString.Append("	<tpAmb>" + ide.tpAmb + "</tpAmb>");

            xmlString.Append("	<finNFe>" + ide.finNFe + "</finNFe>");

            if (nFeContexto.Versao == NFeVersao.VERSAO_3_1_0)
            {
                //XmlString.Append("	<indFinal>" + ide.indFinal + "</indFinal>");
                //XmlString.Append("	<indPres>" + ide.indPres + "</indPres>");
            }

            xmlString.Append("	<procEmi>" + ide.procEmi + "</procEmi>");
            xmlString.Append("	<verProc>1</verProc>");
            xmlString.Append("</ide>");
        }

        private void MontaEMIT()
        {
            xmlString.Append("<emit>");

            if (!String.IsNullOrEmpty(emit.CPF))
            {
                xmlString.Append("	<CPF>" + emit.CPF + "</CPF>");
            }
            if (!String.IsNullOrEmpty(emit.CNPJ))
            {
                xmlString.Append("	<CNPJ>" + emit.CNPJ + "</CNPJ>");
            }

            xmlString.Append("	<xNome>" + emit.xNome + "</xNome>");
            xmlString.Append("	<enderEmit>");
            xmlString.Append("		<xLgr>" + emit.xLgr + "</xLgr>");
            xmlString.Append("		<nro>" + emit.nro + "</nro>");
            xmlString.Append("		<xBairro>" + emit.xBairro + "</xBairro>");
            xmlString.Append("		<cMun>" + emit.cMun + "</cMun>");
            xmlString.Append("		<xMun>" + emit.xMun + "</xMun>");
            xmlString.Append("		<UF>" + emit.UF.Trim() + "</UF>");
            xmlString.Append("		<CEP>" + emit.CEP + "</CEP>");
            xmlString.Append("		<cPais>1058</cPais>");
            xmlString.Append("		<xPais>BRASIL</xPais>");

            if (!String.IsNullOrEmpty(emit.fone))
                xmlString.Append("		<fone>" + emit.fone + "</fone>");

            xmlString.Append("	</enderEmit>");

            xmlString.Append("	<IE>" + emit.IE + "</IE>");
            xmlString.Append("	<CRT>" + emit.CRT + "</CRT>");
            xmlString.Append("</emit>");
        }

        private void MontaDEST()
        {
            xmlString.Append("<dest>");

            if (!String.IsNullOrEmpty(dest.CPF))
            {
                xmlString.Append("	<CPF>" + dest.CPF + "</CPF>");
            }

            if (!String.IsNullOrEmpty(dest.CNPJ))
            {
                xmlString.Append("	<CNPJ>" + dest.CNPJ + "</CNPJ>");
            }

            if (!String.IsNullOrEmpty(dest.idEstrangeiro))
            {
                xmlString.Append("	<idEstrangeiro>" + dest.idEstrangeiro + "</idEstrangeiro>");
            }

            xmlString.Append("	<xNome>" + dest.xNome + "</xNome>");
            xmlString.Append("	<enderDest>");
            xmlString.Append("		<xLgr>" + dest.xLgr + "</xLgr>");
            xmlString.Append("		<nro>" + dest.nro + "</nro>");

            if (!String.IsNullOrEmpty(dest.xCpl))
            {
                xmlString.Append("		<xCpl>" + dest.xCpl + "</xCpl>");
            }

            xmlString.Append("		<xBairro>" + dest.xBairro + "</xBairro>");
            xmlString.Append("		<cMun>" + dest.cMun + "</cMun>");
            xmlString.Append("		<xMun>" + dest.xMun + "</xMun>");
            xmlString.Append("		<UF>" + dest.UF.Trim() + "</UF>");

            if (!String.IsNullOrEmpty(dest.CEP))
                xmlString.Append("		<CEP>" + dest.CEP + "</CEP>");

            xmlString.Append("		<cPais>1058</cPais>");
            xmlString.Append("		<xPais>BRASIL</xPais>");

            if (!String.IsNullOrEmpty(dest.fone))
                xmlString.Append("		<fone>" + dest.fone + "</fone>");

            xmlString.Append("	</enderDest>");

            if (nFeContexto.Versao == NFeVersao.VERSAO_3_1_0)
            {
                xmlString.Append("	<indIEDest>" + dest.indIEDest + "</indIEDest>");
            }


            xmlString.Append("	<IE>" + dest.IE + "</IE>");


            if (nFeContexto.Versao == NFeVersao.VERSAO_3_1_0 && !String.IsNullOrEmpty(dest.email))
                xmlString.Append("	<email>" + dest.email + "</email>");

            xmlString.Append("</dest>");
        }

        private void MontaDET()
        {
            for (int i = 0; i < detList.Count; i++)
            {
                xmlString.Append("<det nItem=\"" + (i + 1) + "\">");
                xmlString.Append("	<prod>");
                xmlString.Append("		<cProd>" + detList[i].cProd.Trim() + "</cProd>");
                xmlString.Append("		<cEAN>" + detList[i].cEAN + "</cEAN>");
                xmlString.Append("		<xProd>" + detList[i].xProd + "</xProd>");
                xmlString.Append("		<NCM>" + detList[i].NCM + "</NCM>");
                xmlString.Append("		<CFOP>" + detList[i].CFOP + "</CFOP>");
                xmlString.Append("		<uCom>" + detList[i].uCom + "</uCom>");
                xmlString.Append("		<qCom>" + detList[i].qCom + "</qCom>");
                xmlString.Append("		<vUnCom>" + detList[i].vUnCom + "</vUnCom>");
                xmlString.Append("		<vProd>" + detList[i].vProd + "</vProd>");
                xmlString.Append("		<cEANTrib>" + detList[i].cEANTrib + "</cEANTrib>");
                xmlString.Append("		<uTrib>" + detList[i].uTrib + "</uTrib>");
                xmlString.Append("		<qTrib>" + detList[i].qTrib + "</qTrib>");
                xmlString.Append("		<vUnTrib>" + detList[i].vUnTrib + "</vUnTrib>");

                if (!String.IsNullOrEmpty(detList[i].vFrete))
                {
                    xmlString.Append("		<vFrete>" + detList[i].vFrete + "</vFrete>");
                }
                if (!String.IsNullOrEmpty(detList[i].vDesc))
                {
                    xmlString.Append("		<vDesc>" + detList[i].vDesc + "</vDesc>");
                }

                xmlString.Append("		<indTot>" + detList[i].indTot + "</indTot>");
                xmlString.Append("	</prod>");

                xmlString.Append("	<imposto>");

                if (!string.IsNullOrEmpty(detList[i].vTotTrib))
                    xmlString.Append("	<vTotTrib>" + detList[i].vTotTrib + "</vTotTrib>");

                MontaDET_ICMS(detList[i]);
                MontaDET_IPI(detList[i]);
                MontaDET_PIS(detList[i]);
                MontaDET_COFINS(detList[i]);

                xmlString.Append("	</imposto>");

                xmlString.Append("</det>");
            }
        }

        private void MontaDET_ICMS(DET det)
        {
            xmlString.Append("<ICMS>");

            switch (det.icms)
            {
                case ICMS.ICMS00:
                    xmlString.Append("<ICMS00>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    xmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    xmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    xmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    xmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    xmlString.Append("</ICMS00>");
                    break;
                case ICMS.ICMS10:
                    xmlString.Append("<ICMS10>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    xmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    xmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    xmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    xmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    xmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        xmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        xmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    xmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    xmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    xmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");

                    xmlString.Append("</ICMS10>");
                    break;
                case ICMS.ICMS20:
                    xmlString.Append("<ICMS20>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    xmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    xmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    xmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    xmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    xmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    xmlString.Append("</ICMS20>");
                    break;
                case ICMS.ICMS30:
                    xmlString.Append("<ICMS30>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    xmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");
                    xmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    xmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    xmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    xmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    xmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");
                    xmlString.Append("</ICMS30>");
                    break;
                case ICMS.ICMS40_50:
                    xmlString.Append("<ICMS40>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    xmlString.Append("</ICMS40>");
                    break;
                case ICMS.ICMS51:
                    xmlString.Append("<ICMS51>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    xmlString.Append("</ICMS51>");

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        xmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        xmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        xmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        xmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        xmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    }
                    break;
                case ICMS.ICMS60:
                    xmlString.Append("<ICMS60>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    xmlString.Append("</ICMS60>");
                    break;
                case ICMS.ICMS70:
                    xmlString.Append("<ICMS70>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    xmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    xmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    xmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    xmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    xmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    xmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        xmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        xmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    xmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    xmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    xmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");
                    xmlString.Append("</ICMS70>");
                    break;
                case ICMS.ICMS90:
                    xmlString.Append("<ICMS90>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    xmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    xmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");

                    if (!String.IsNullOrEmpty(det.icms_pRedBC))
                    {
                        xmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    }

                    xmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    xmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    xmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        xmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        xmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    xmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    xmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    xmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");
                    xmlString.Append("</ICMS90>");
                    break;
                case ICMS.ICMS101:
                    xmlString.Append("<ICMSSN101>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    xmlString.Append("    <pCredSN>" + det.icms_pCredSN + "</pCredSN>");
                    xmlString.Append("    <vCredICMSSN>" + det.icms_vCredICMSSN + "</vCredICMSSN>");
                    xmlString.Append("</ICMSSN101>");
                    break;
                case ICMS.ICMS102_400:
                    xmlString.Append("<ICMSSN102>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    xmlString.Append("</ICMSSN102>");
                    break;
                case ICMS.ICMS201:
                    xmlString.Append("<ICMSSN201>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    xmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        xmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        xmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    xmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    xmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    xmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");

                    xmlString.Append("</ICMSSN201>");
                    break;
                case ICMS.ICMS202:
                    xmlString.Append("<ICMSSN202>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    xmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        xmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        xmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    xmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    xmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    xmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");

                    xmlString.Append("</ICMSSN202>");
                    break;
                case ICMS.ICMS500:
                    xmlString.Append("<ICMSSN500>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    xmlString.Append("    <vBCSTRet>" + det.icms_vBCSTRet + "</vBCSTRet>");
                    xmlString.Append("    <vICMSSTRet>" + det.icms_vICMSSTRet + "</vICMSSTRet>");
                    xmlString.Append("</ICMSSN500>");
                    break;
                case ICMS.ICMS900:
                    xmlString.Append("<ICMSSN900>");
                    xmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    xmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    xmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    xmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");

                    if (!String.IsNullOrEmpty(det.icms_pRedBC))
                    {
                        xmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    }

                    xmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    xmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    xmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        xmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        xmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    xmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    xmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    xmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");
                    xmlString.Append("    <pCredSN>" + det.icms_pCredSN + "</pCredSN>");
                    xmlString.Append("    <vCredICMSSN>" + det.icms_vCredICMSSN + "</vCredICMSSN>");
                    xmlString.Append("</ICMSSN900>");
                    break;
            }

            xmlString.Append("</ICMS>");
        }

        private void MontaDET_IPI(DET det)
        {
            if (!String.IsNullOrEmpty(det.ipi_cIEnq))
            {
                xmlString.Append("<IPI>");

                xmlString.Append("<cEnq>" + det.ipi_cIEnq + "</cEnq>");

                switch (det.ipi)
                {
                    case IPI.IPI00_49_50_99:
                        xmlString.Append("<IPITrib>");

                        xmlString.Append("    <CST>" + det.ipi_CST + "</CST>");

                        if (!String.IsNullOrEmpty(det.ipi_vBC))
                        {
                            xmlString.Append("    <vBC>" + det.ipi_vBC + "</vBC>");
                            xmlString.Append("    <pIPI>" + det.ipi_pIPI + "</pIPI>");
                        }
                        else
                        {
                            xmlString.Append("    <qUnid>" + det.ipi_qUnid + "</qUnid>");
                            xmlString.Append("    <vUnid>" + det.ipi_vUnid + "</vUnid>");
                        }

                        xmlString.Append("    <vIPI>" + det.ipi_vIPI + "</vIPI>");

                        xmlString.Append("</IPITrib>");
                        break;
                    case IPI.IPI01_55:
                        xmlString.Append("<IPINT>");
                        xmlString.Append("    <CST>" + det.ipi_CST + "</CST>");
                        xmlString.Append("</IPINT>");
                        break;
                }

                xmlString.Append("</IPI>");
            }
        }

        private void MontaDET_PIS(DET det)
        {
            xmlString.Append("<PIS>");

            switch (det.pis)
            {
                case PIS.PIS01_02:
                    xmlString.Append("<PISAliq>");
                    xmlString.Append("    <CST>" + det.pis_CST + "</CST>");
                    xmlString.Append("    <vBC>" + det.pis_vBC + "</vBC>");
                    xmlString.Append("    <pPIS>" + det.pis_pPIS + "</pPIS>");
                    xmlString.Append("    <vPIS>" + det.pis_vPIS + "</vPIS>");
                    xmlString.Append("</PISAliq>");
                    break;
                case PIS.PIS03:
                    xmlString.Append("<PISQtde>");
                    xmlString.Append("    <CST>" + det.pis_CST + "</CST>");
                    xmlString.Append("    <qBCProd>" + det.pis_qBCProd + "</qBCProd>");
                    xmlString.Append("    <vAliqProd>" + det.pis_vAliqProd + "</vAliqProd>");
                    xmlString.Append("    <vPIS>" + det.pis_vPIS + "</vPIS>");
                    xmlString.Append("</PISQtde>");
                    break;
                case PIS.PIS04_09:
                    xmlString.Append("<PISNT>");
                    xmlString.Append("    <CST>" + det.pis_CST + "</CST>");
                    xmlString.Append("</PISNT>");
                    break;
                case PIS.PIS99:
                    xmlString.Append("<PISOutr>");
                    xmlString.Append("    <CST>" + det.pis_CST + "</CST>");

                    if (!String.IsNullOrEmpty(det.pis_vBC) && !String.IsNullOrEmpty(det.pis_pPIS))
                    {
                        xmlString.Append("    <vBC>" + det.pis_vBC + "</vBC>");
                        xmlString.Append("    <pPIS>" + det.pis_pPIS + "</pPIS>");
                    }
                    else
                    {
                        xmlString.Append("    <qBCProd>" + det.pis_qBCProd + "</qBCProd>");
                        xmlString.Append("    <vAliqProd>" + det.pis_vAliqProd + "</vAliqProd>");
                    }

                    xmlString.Append("    <vPIS>" + det.pis_vPIS + "</vPIS>");
                    xmlString.Append("</PISOutr>");
                    break;
            }

            xmlString.Append("</PIS>");
        }


        private void MontaDET_COFINS(DET det)
        {
            xmlString.Append("<COFINS>");

            switch (det.cofins)
            {
                case COFINS.CST01_02:
                    xmlString.Append("<COFINSAliq>");
                    xmlString.Append("    <CST>" + det.cofins_CST + "</CST>");
                    xmlString.Append("    <vBC>" + det.cofins_vBC + "</vBC>");
                    xmlString.Append("    <pCOFINS>" + det.cofins_pCOFINS + "</pCOFINS>");
                    xmlString.Append("    <vCOFINS>" + det.cofins_vCOFINS + "</vCOFINS>");
                    xmlString.Append("</COFINSAliq>");
                    break;
                case COFINS.CST03:
                    xmlString.Append("<COFINSQtde>");
                    xmlString.Append("    <CST>" + det.cofins_CST + "</CST>");
                    xmlString.Append("    <qBCProd>" + det.cofins_qBCProd + "</qBCProd>");
                    xmlString.Append("    <vAliqProd>" + det.cofins_vAliqProd + "</vAliqProd>");
                    xmlString.Append("    <vCOFINS>" + det.cofins_vCOFINS + "</vCOFINS>");
                    xmlString.Append("</COFINSQtde>");
                    break;
                case COFINS.CST04_09:
                    xmlString.Append("<COFINSNT>");
                    xmlString.Append("    <CST>" + det.cofins_CST + "</CST>");
                    xmlString.Append("</COFINSNT>");
                    break;
                case COFINS.CST99:
                    xmlString.Append("<COFINSOutr>");
                    xmlString.Append("    <CST>" + det.cofins_CST + "</CST>");

                    if (!String.IsNullOrEmpty(det.cofins_vBC) && !String.IsNullOrEmpty(det.cofins_pCOFINS))
                    {
                        xmlString.Append("    <vBC>" + det.cofins_vBC + "</vBC>");
                        xmlString.Append("    <pCOFINS>" + det.cofins_pCOFINS + "</pCOFINS>");
                    }
                    else
                    {
                        xmlString.Append("    <qBCProd>" + det.cofins_qBCProd + "</qBCProd>");
                        xmlString.Append("    <vAliqProd>" + det.cofins_vAliqProd + "</vAliqProd>");
                    }

                    xmlString.Append("    <vCOFINS>" + det.cofins_vCOFINS + "</vCOFINS>");
                    xmlString.Append("</COFINSOutr>");
                    break;
            }

            xmlString.Append("</COFINS>");
        }

        private void MontaTOTAL()
        {
            xmlString.Append("<total>");
            xmlString.Append("	<ICMSTot>");
            xmlString.Append("		<vBC>" + total.vBC + "</vBC>");
            xmlString.Append("		<vICMS>" + total.vICMS + "</vICMS>");
            if (nFeContexto.Versao == NFeVersao.VERSAO_3_1_0)
                xmlString.Append("		<vICMSDeson>" + total.vICMSDeson + "</vICMSDeson>");
            xmlString.Append("		<vBCST>" + total.vBCST + "</vBCST>");
            xmlString.Append("		<vST>" + total.vST + "</vST>");
            xmlString.Append("		<vProd>" + total.vProd + "</vProd>");
            xmlString.Append("		<vFrete>" + total.vFrete + "</vFrete>");
            xmlString.Append("		<vSeg>" + total.vSeg + "</vSeg>");
            xmlString.Append("		<vDesc>" + total.vDesc + "</vDesc>");
            xmlString.Append("		<vII>0.00</vII>");
            xmlString.Append("		<vIPI>" + total.vIPI + "</vIPI>");
            xmlString.Append("		<vPIS>0.00</vPIS>");
            xmlString.Append("		<vCOFINS>0.00</vCOFINS>");
            xmlString.Append("		<vOutro>" + total.vOutro + "</vOutro>");
            xmlString.Append("		<vNF>" + total.vNF + "</vNF>");

            if (!string.IsNullOrEmpty(total.vTotTrib))
                xmlString.Append("		<vTotTrib>" + total.vTotTrib + "</vTotTrib>");

            xmlString.Append("	</ICMSTot>");
            xmlString.Append("</total>");
        }

        private void MontaTRANSP()
        {
            xmlString.Append("<transp>");
            xmlString.Append("	<modFrete>" + transp.modFrete + "</modFrete>");

            if (!String.IsNullOrEmpty(transp.CNPJ))
            {
                xmlString.Append("	<transporta>");

                if (!String.IsNullOrEmpty(transp.CNPJ))
                    xmlString.Append("		<CNPJ>" + transp.CNPJ + "</CNPJ>");

                /* 
                if(!String.IsNullOrEmpty(this.transp.CPF))
                    this.XmlString.Append("		<CPF>" + this.transp.CPF + "</CPF>");
                */

                if (!String.IsNullOrEmpty(transp.xNome))
                    xmlString.Append("		<xNome>" + transp.xNome + "</xNome>");

                if (!String.IsNullOrEmpty(transp.IE))
                {
                    xmlString.Append("		<IE>" + transp.IE + "</IE>");
                }

                if (!String.IsNullOrEmpty(transp.xEnder))
                {
                    xmlString.Append("		<xEnder>" + transp.xEnder + "</xEnder>");
                }

                if (!String.IsNullOrEmpty(transp.xMun))
                {
                    xmlString.Append("		<xMun>" + transp.xMun + "</xMun>");
                }

                if (!String.IsNullOrEmpty(transp.UF))
                {
                    xmlString.Append("		<UF>" + transp.UF.Trim() + "</UF>");
                }

                xmlString.Append("	</transporta>");
            }
            if (!String.IsNullOrEmpty(transp.veic_placa))
            {
                xmlString.Append("	<veicTransp>");
                xmlString.Append("		<placa>" + transp.veic_placa + "</placa>");
                xmlString.Append("		<UF>" + transp.veic_UF + "</UF>");
                xmlString.Append("	</veicTransp>");
            }

            if (!String.IsNullOrEmpty(transp.qVol))
            {
                xmlString.Append("	<vol>");
                xmlString.Append("		<qVol>" + transp.qVol + "</qVol>");
                xmlString.Append("		<esp>" + transp.esp + "</esp>");


                if (!String.IsNullOrEmpty(transp.marca))
                {
                    xmlString.Append("		<marca>" + transp.marca + "</marca>");
                }

                if (!String.IsNullOrEmpty(transp.nVol))
                    xmlString.Append("		<nVol>" + transp.nVol + "</nVol>");

                if (!String.IsNullOrEmpty(transp.pesoL))
                    xmlString.Append("		<pesoL>" + transp.pesoL + "</pesoL>");

                if (!String.IsNullOrEmpty(transp.pesoB))
                    xmlString.Append("		<pesoB>" + transp.pesoB + "</pesoB>");

                xmlString.Append("	</vol>");
            }
            xmlString.Append("</transp>");
        }

        private void MontaCOBR()
        {
            if (!String.IsNullOrEmpty(cobr.nFat))
            {
                xmlString.Append("<cobr>");
                xmlString.Append("	<fat>");
                xmlString.Append("		<nFat>" + cobr.nFat + "</nFat>");
                xmlString.Append("		<vOrig>" + cobr.vOrig + "</vOrig>");
                xmlString.Append("		<vLiq>" + cobr.vLiq + "</vLiq>");
                xmlString.Append("	</fat>");

                for (int i = 0; i < cobr.dup.Count; i++)
                {
                    xmlString.Append("	<dup>");
                    xmlString.Append("		<nDup>" + cobr.dup[i].nDup + "</nDup>");
                    xmlString.Append("		<dVenc>" + cobr.dup[i].dVenc + "</dVenc>");
                    xmlString.Append("		<vDup>" + cobr.dup[i].vDup + "</vDup>");
                    xmlString.Append("	</dup>");
                }

                xmlString.Append("</cobr>");
            }
        }
    }
}