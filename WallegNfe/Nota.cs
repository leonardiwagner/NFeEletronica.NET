using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using WallegNFe.Bll;
using WallegNFe.Model;
using WallegNFe.Model.Nota;
using WallegNFe.Model.Nota.Enum;
using WallegNFe.Versao;

namespace WallegNFe
{
    public class Nota
    {
        private readonly NFeContexto NFeContexto;
        private readonly StringBuilder XmlString;
        private readonly List<DET> detList;
        public String ArquivoNome = "";
        public String CaminhoFisico = "";
        public String ConteudoXml = "";
        public String NotaId = "";

        public Nota(NFeContexto NFeContexto)
        {
            this.NFeContexto = NFeContexto;

            ide = new IDE();
            emit = new EMIT();
            dest = new DEST();
            detList = new List<DET>();
            total = new TOTAL();
            transp = new TRANSP();
            cobr = new COBR();

            XmlString = new StringBuilder();

            if (this.NFeContexto.Producao)
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
            if (!NFeContexto.Producao)
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
            GerarCodigoDaNota();


            XmlString.Append("<NFe xmlns=\"http://www.portalfiscal.inf.br/nfe\">");
            XmlString.Append("   <infNFe Id=\"NFe" + NotaId + "\" versao=\"" + NFeContexto.Versao.VersaoString + "\">");

            MontaIDE();
            MontaEMIT();
            MontaDEST();
            MontaDET();
            MontaTOTAL();
            MontaTRANSP();
            MontaCOBR();

            if (!String.IsNullOrEmpty(infAdic))
            {
                XmlString.Append("<infAdic>");
                XmlString.Append("	<infCpl>");
                XmlString.Append(infAdic);
                XmlString.Append("	</infCpl>");
                XmlString.Append("</infAdic>");
            }

            XmlString.Append("   </infNFe>");
            //this.XmlString.Append("   <Signature></Signature>"); acho que não precisa disso
            XmlString.Append("</NFe>");

            var xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.LoadXml(XmlString.ToString().Replace("&", "&amp;"));
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
            ConteudoXml = XmlString.ToString();
        }

        private void MontaIDE()
        {
            XmlString.Append("<ide>");
            XmlString.Append("	<cUF>" + ide.cUF + "</cUF>");
            XmlString.Append("	<cNF>" + ide.cNF + "</cNF>");
            XmlString.Append("	<natOp>" + ide.natOp + "</natOp>");
            XmlString.Append("	<indPag>" + ide.indPag + "</indPag>"); //0 - a vista, 1 - a prazo, 2 - outros
            XmlString.Append("	<mod>" + ide.mod + "</mod>");
            XmlString.Append("	<serie>" + ide.serie + "</serie>");
            XmlString.Append("	<nNF>" + ide.nNF + "</nNF>");

            if (NFeContexto.Versao == NFeVersao.VERSAO_3_1_0)
            {
                XmlString.Append("	<dhEmi>" + ide.dhEmi + "</dhEmi>");
            }
            else
            {
                XmlString.Append("	<dEmi>" + ide.dEmi + "</dEmi>");
            }

            if (!String.IsNullOrEmpty(ide.dhSaiEnt))
                XmlString.Append("	<dhSaiEnt>" + ide.dhSaiEnt + "</dhSaiEnt>");

            XmlString.Append("	<tpNF>" + ide.tpNF + "</tpNF>");
            
            if(NFeContexto.Versao == NFeVersao.VERSAO_3_1_0)
                XmlString.Append("	<idDest>" + ide.idDest + "</idDest>");
            
            XmlString.Append("	<cMunFG>" + ide.cMunFG + "</cMunFG>");
            XmlString.Append("	<tpImp>" + ide.tpImp + "</tpImp>");
            XmlString.Append("	<tpEmis>" + ide.tpEmis + "</tpEmis>");
            XmlString.Append("	<cDV>" + ide.cDV + "</cDV>");
            XmlString.Append("	<tpAmb>" + ide.tpAmb + "</tpAmb>");

            XmlString.Append("	<finNFe>" + ide.finNFe + "</finNFe>");

            if (NFeContexto.Versao == NFeVersao.VERSAO_3_1_0)
            {
                //XmlString.Append("	<indFinal>" + ide.indFinal + "</indFinal>");
                //XmlString.Append("	<indPres>" + ide.indPres + "</indPres>");
            }

            XmlString.Append("	<procEmi>" + ide.procEmi + "</procEmi>");
            XmlString.Append("	<verProc>1</verProc>");
            XmlString.Append("</ide>");
        }

        private void MontaEMIT()
        {
            XmlString.Append("<emit>");

            if (!String.IsNullOrEmpty(emit.CPF))
            {
                XmlString.Append("	<CPF>" + emit.CPF + "</CPF>");
            }
            if (!String.IsNullOrEmpty(emit.CNPJ))
            {
                XmlString.Append("	<CNPJ>" + emit.CNPJ + "</CNPJ>");
            }

            XmlString.Append("	<xNome>" + emit.xNome + "</xNome>");
            XmlString.Append("	<enderEmit>");
            XmlString.Append("		<xLgr>" + emit.xLgr + "</xLgr>");
            XmlString.Append("		<nro>" + emit.nro + "</nro>");
            XmlString.Append("		<xBairro>" + emit.xBairro + "</xBairro>");
            XmlString.Append("		<cMun>" + emit.cMun + "</cMun>");
            XmlString.Append("		<xMun>" + emit.xMun + "</xMun>");
            XmlString.Append("		<UF>" + emit.UF.Trim() + "</UF>");
            XmlString.Append("		<CEP>" + emit.CEP + "</CEP>");
            XmlString.Append("		<cPais>1058</cPais>");
            XmlString.Append("		<xPais>BRASIL</xPais>");

            if (!String.IsNullOrEmpty(emit.fone))
                XmlString.Append("		<fone>" + emit.fone + "</fone>");

            XmlString.Append("	</enderEmit>");

            XmlString.Append("	<IE>" + emit.IE + "</IE>");
            XmlString.Append("	<CRT>" + emit.CRT + "</CRT>");
            XmlString.Append("</emit>");
        }

        private void MontaDEST()
        {
            XmlString.Append("<dest>");

            if (!String.IsNullOrEmpty(dest.CPF))
            {
                XmlString.Append("	<CPF>" + dest.CPF + "</CPF>");
            }

            if (!String.IsNullOrEmpty(dest.CNPJ))
            {
                XmlString.Append("	<CNPJ>" + dest.CNPJ + "</CNPJ>");
            }

            if (!String.IsNullOrEmpty(dest.idEstrangeiro))
            {
                XmlString.Append("	<idEstrangeiro>" + dest.idEstrangeiro + "</idEstrangeiro>");
            }

            XmlString.Append("	<xNome>" + dest.xNome + "</xNome>");
            XmlString.Append("	<enderDest>");
            XmlString.Append("		<xLgr>" + dest.xLgr + "</xLgr>");
            XmlString.Append("		<nro>" + dest.nro + "</nro>");

            if (!String.IsNullOrEmpty(dest.xCpl))
            {
                XmlString.Append("		<xCpl>" + dest.xCpl + "</xCpl>");
            }

            XmlString.Append("		<xBairro>" + dest.xBairro + "</xBairro>");
            XmlString.Append("		<cMun>" + dest.cMun + "</cMun>");
            XmlString.Append("		<xMun>" + dest.xMun + "</xMun>");
            XmlString.Append("		<UF>" + dest.UF.Trim() + "</UF>");

            if (!String.IsNullOrEmpty(dest.CEP))
                XmlString.Append("		<CEP>" + dest.CEP + "</CEP>");

            XmlString.Append("		<cPais>1058</cPais>");
            XmlString.Append("		<xPais>BRASIL</xPais>");

            if (!String.IsNullOrEmpty(dest.fone))
                XmlString.Append("		<fone>" + dest.fone + "</fone>");

            XmlString.Append("	</enderDest>");

            if (NFeContexto.Versao == NFeVersao.VERSAO_3_1_0)
            {
                XmlString.Append("	<indIEDest>" + dest.indIEDest + "</indIEDest>");
            }

            
            XmlString.Append("	<IE>" + dest.IE + "</IE>");
            

            if (NFeContexto.Versao == NFeVersao.VERSAO_3_1_0 && !String.IsNullOrEmpty(dest.email))
                XmlString.Append("	<email>" + dest.email + "</email>");

            XmlString.Append("</dest>");
        }

        private void MontaDET()
        {
            for (int i = 0; i < detList.Count; i++)
            {
                XmlString.Append("<det nItem=\"" + (i + 1) + "\">");
                XmlString.Append("	<prod>");
                XmlString.Append("		<cProd>" + detList[i].cProd.Trim() + "</cProd>");
                XmlString.Append("		<cEAN>" + detList[i].cEAN + "</cEAN>");
                XmlString.Append("		<xProd>" + detList[i].xProd + "</xProd>");
                XmlString.Append("		<NCM>" + detList[i].NCM + "</NCM>");
                XmlString.Append("		<CFOP>" + detList[i].CFOP + "</CFOP>");
                XmlString.Append("		<uCom>" + detList[i].uCom + "</uCom>");
                XmlString.Append("		<qCom>" + detList[i].qCom + "</qCom>");
                XmlString.Append("		<vUnCom>" + detList[i].vUnCom + "</vUnCom>");
                XmlString.Append("		<vProd>" + detList[i].vProd + "</vProd>");
                XmlString.Append("		<cEANTrib>" + detList[i].cEANTrib + "</cEANTrib>");
                XmlString.Append("		<uTrib>" + detList[i].uTrib + "</uTrib>");
                XmlString.Append("		<qTrib>" + detList[i].qTrib + "</qTrib>");
                XmlString.Append("		<vUnTrib>" + detList[i].vUnTrib + "</vUnTrib>");

                if (!String.IsNullOrEmpty(detList[i].vFrete))
                {
                    XmlString.Append("		<vFrete>" + detList[i].vFrete + "</vFrete>");
                }
                if (!String.IsNullOrEmpty(detList[i].vDesc))
                {
                    XmlString.Append("		<vDesc>" + detList[i].vDesc + "</vDesc>");
                }

                XmlString.Append("		<indTot>" + detList[i].indTot + "</indTot>");
                XmlString.Append("	</prod>");

                XmlString.Append("	<imposto>");

                if (!string.IsNullOrEmpty(detList[i].vTotTrib))
                    XmlString.Append("	<vTotTrib>" + detList[i].vTotTrib + "</vTotTrib>");

                MontaDET_ICMS(detList[i]);
                MontaDET_IPI(detList[i]);
                MontaDET_PIS(detList[i]);
                MontaDET_COFINS(detList[i]);

                XmlString.Append("	</imposto>");

                XmlString.Append("</det>");
            }
        }

        private void MontaDET_ICMS(DET det)
        {
            XmlString.Append("<ICMS>");

            switch (det.icms)
            {
                case ICMS.ICMS00:
                    XmlString.Append("<ICMS00>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    XmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    XmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    XmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    XmlString.Append("</ICMS00>");
                    break;
                case ICMS.ICMS10:
                    XmlString.Append("<ICMS10>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    XmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    XmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    XmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    XmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        XmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        XmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    XmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    XmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");

                    XmlString.Append("</ICMS10>");
                    break;
                case ICMS.ICMS20:
                    XmlString.Append("<ICMS20>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    XmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    XmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    XmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    XmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    XmlString.Append("</ICMS20>");
                    break;
                case ICMS.ICMS30:
                    XmlString.Append("<ICMS30>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    XmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");
                    XmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    XmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    XmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    XmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");
                    XmlString.Append("</ICMS30>");
                    break;
                case ICMS.ICMS40_50:
                    XmlString.Append("<ICMS40>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    XmlString.Append("</ICMS40>");
                    break;
                case ICMS.ICMS51:
                    XmlString.Append("<ICMS51>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    XmlString.Append("</ICMS51>");

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        XmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        XmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        XmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        XmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    }
                    break;
                case ICMS.ICMS60:
                    XmlString.Append("<ICMS60>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    XmlString.Append("</ICMS60>");
                    break;
                case ICMS.ICMS70:
                    XmlString.Append("<ICMS70>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    XmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    XmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    XmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    XmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    XmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        XmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        XmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    XmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    XmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");
                    XmlString.Append("</ICMS70>");
                    break;
                case ICMS.ICMS90:
                    XmlString.Append("<ICMS90>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    XmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    XmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");

                    if (!String.IsNullOrEmpty(det.icms_pRedBC))
                    {
                        XmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    }

                    XmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    XmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        XmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        XmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    XmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    XmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");
                    XmlString.Append("</ICMS90>");
                    break;
                case ICMS.ICMS101:
                    XmlString.Append("<ICMSSN101>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    XmlString.Append("    <pCredSN>" + det.icms_pCredSN + "</pCredSN>");
                    XmlString.Append("    <vCredICMSSN>" + det.icms_vCredICMSSN + "</vCredICMSSN>");
                    XmlString.Append("</ICMSSN101>");
                    break;
                case ICMS.ICMS102_400:
                    XmlString.Append("<ICMSSN102>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    XmlString.Append("</ICMSSN102>");
                    break;
                case ICMS.ICMS201:
                    XmlString.Append("<ICMSSN201>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    XmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        XmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        XmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    XmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    XmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");

                    XmlString.Append("</ICMSSN201>");
                    break;
                case ICMS.ICMS202:
                    XmlString.Append("<ICMSSN202>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    XmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        XmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        XmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    XmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    XmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");

                    XmlString.Append("</ICMSSN202>");
                    break;
                case ICMS.ICMS500:
                    XmlString.Append("<ICMSSN500>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    XmlString.Append("    <vBCSTRet>" + det.icms_vBCSTRet + "</vBCSTRet>");
                    XmlString.Append("    <vICMSSTRet>" + det.icms_vICMSSTRet + "</vICMSSTRet>");
                    XmlString.Append("</ICMSSN500>");
                    break;
                case ICMS.ICMS900:
                    XmlString.Append("<ICMSSN900>");
                    XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    XmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    XmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    XmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");

                    if (!String.IsNullOrEmpty(det.icms_pRedBC))
                    {
                        XmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    }

                    XmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    XmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        XmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        XmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    XmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    XmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");
                    XmlString.Append("    <pCredSN>" + det.icms_pCredSN + "</pCredSN>");
                    XmlString.Append("    <vCredICMSSN>" + det.icms_vCredICMSSN + "</vCredICMSSN>");
                    XmlString.Append("</ICMSSN900>");
                    break;
            }

            XmlString.Append("</ICMS>");
        }

        private void MontaDET_IPI(DET det)
        {
            if (!String.IsNullOrEmpty(det.ipi_cIEnq))
            {
                XmlString.Append("<IPI>");

                XmlString.Append("<cEnq>" + det.ipi_cIEnq + "</cEnq>");

                switch (det.ipi)
                {
                    case IPI.IPI00_49_50_99:
                        XmlString.Append("<IPITrib>");

                        XmlString.Append("    <CST>" + det.ipi_CST + "</CST>");

                        if (!String.IsNullOrEmpty(det.ipi_vBC))
                        {
                            XmlString.Append("    <vBC>" + det.ipi_vBC + "</vBC>");
                            XmlString.Append("    <pIPI>" + det.ipi_pIPI + "</pIPI>");
                        }
                        else
                        {
                            XmlString.Append("    <qUnid>" + det.ipi_qUnid + "</qUnid>");
                            XmlString.Append("    <vUnid>" + det.ipi_vUnid + "</vUnid>");
                        }

                        XmlString.Append("    <vIPI>" + det.ipi_vIPI + "</vIPI>");

                        XmlString.Append("</IPITrib>");
                        break;
                    case IPI.IPI01_55:
                        XmlString.Append("<IPINT>");
                        XmlString.Append("    <CST>" + det.ipi_CST + "</CST>");
                        XmlString.Append("</IPINT>");
                        break;
                }

                XmlString.Append("</IPI>");
            }
        }

        private void MontaDET_PIS(DET det)
        {
            XmlString.Append("<PIS>");

            switch (det.pis)
            {
                case PIS.PIS01_02:
                    XmlString.Append("<PISAliq>");
                    XmlString.Append("    <CST>" + det.pis_CST + "</CST>");
                    XmlString.Append("    <vBC>" + det.pis_vBC + "</vBC>");
                    XmlString.Append("    <pPIS>" + det.pis_pPIS + "</pPIS>");
                    XmlString.Append("    <vPIS>" + det.pis_vPIS + "</vPIS>");
                    XmlString.Append("</PISAliq>");
                    break;
                case PIS.PIS03:
                    XmlString.Append("<PISQtde>");
                    XmlString.Append("    <CST>" + det.pis_CST + "</CST>");
                    XmlString.Append("    <qBCProd>" + det.pis_qBCProd + "</qBCProd>");
                    XmlString.Append("    <vAliqProd>" + det.pis_vAliqProd + "</vAliqProd>");
                    XmlString.Append("    <vPIS>" + det.pis_vPIS + "</vPIS>");
                    XmlString.Append("</PISQtde>");
                    break;
                case PIS.PIS04_09:
                    XmlString.Append("<PISNT>");
                    XmlString.Append("    <CST>" + det.pis_CST + "</CST>");
                    XmlString.Append("</PISNT>");
                    break;
                case PIS.PIS99:
                    XmlString.Append("<PISOutr>");
                    XmlString.Append("    <CST>" + det.pis_CST + "</CST>");

                    if (!String.IsNullOrEmpty(det.pis_vBC) && !String.IsNullOrEmpty(det.pis_pPIS))
                    {
                        XmlString.Append("    <vBC>" + det.pis_vBC + "</vBC>");
                        XmlString.Append("    <pPIS>" + det.pis_pPIS + "</pPIS>");
                    }
                    else
                    {
                        XmlString.Append("    <qBCProd>" + det.pis_qBCProd + "</qBCProd>");
                        XmlString.Append("    <vAliqProd>" + det.pis_vAliqProd + "</vAliqProd>");
                    }

                    XmlString.Append("    <vPIS>" + det.pis_vPIS + "</vPIS>");
                    XmlString.Append("</PISOutr>");
                    break;
            }

            XmlString.Append("</PIS>");
        }


        private void MontaDET_COFINS(DET det)
        {
            XmlString.Append("<COFINS>");

            switch (det.cofins)
            {
                case COFINS.CST01_02:
                    XmlString.Append("<COFINSAliq>");
                    XmlString.Append("    <CST>" + det.cofins_CST + "</CST>");
                    XmlString.Append("    <vBC>" + det.cofins_vBC + "</vBC>");
                    XmlString.Append("    <pCOFINS>" + det.cofins_pCOFINS + "</pCOFINS>");
                    XmlString.Append("    <vCOFINS>" + det.cofins_vCOFINS + "</vCOFINS>");
                    XmlString.Append("</COFINSAliq>");
                    break;
                case COFINS.CST03:
                    XmlString.Append("<COFINSQtde>");
                    XmlString.Append("    <CST>" + det.cofins_CST + "</CST>");
                    XmlString.Append("    <qBCProd>" + det.cofins_qBCProd + "</qBCProd>");
                    XmlString.Append("    <vAliqProd>" + det.cofins_vAliqProd + "</vAliqProd>");
                    XmlString.Append("    <vCOFINS>" + det.cofins_vCOFINS + "</vCOFINS>");
                    XmlString.Append("</COFINSQtde>");
                    break;
                case COFINS.CST04_09:
                    XmlString.Append("<COFINSNT>");
                    XmlString.Append("    <CST>" + det.cofins_CST + "</CST>");
                    XmlString.Append("</COFINSNT>");
                    break;
                case COFINS.CST99:
                    XmlString.Append("<COFINSOutr>");
                    XmlString.Append("    <CST>" + det.cofins_CST + "</CST>");

                    if (!String.IsNullOrEmpty(det.cofins_vBC) && !String.IsNullOrEmpty(det.cofins_pCOFINS))
                    {
                        XmlString.Append("    <vBC>" + det.cofins_vBC + "</vBC>");
                        XmlString.Append("    <pCOFINS>" + det.cofins_pCOFINS + "</pCOFINS>");
                    }
                    else
                    {
                        XmlString.Append("    <qBCProd>" + det.cofins_qBCProd + "</qBCProd>");
                        XmlString.Append("    <vAliqProd>" + det.cofins_vAliqProd + "</vAliqProd>");
                    }

                    XmlString.Append("    <vCOFINS>" + det.cofins_vCOFINS + "</vCOFINS>");
                    XmlString.Append("</COFINSOutr>");
                    break;
            }

            XmlString.Append("</COFINS>");
        }

        private void MontaTOTAL()
        {
            XmlString.Append("<total>");
            XmlString.Append("	<ICMSTot>");
            XmlString.Append("		<vBC>" + total.vBC + "</vBC>");
            XmlString.Append("		<vICMS>" + total.vICMS + "</vICMS>");
            if (NFeContexto.Versao == NFeVersao.VERSAO_3_1_0)
                XmlString.Append("		<vICMSDeson>" + total.vICMSDeson + "</vICMSDeson>");
            XmlString.Append("		<vBCST>" + total.vBCST + "</vBCST>");
            XmlString.Append("		<vST>" + total.vST + "</vST>");
            XmlString.Append("		<vProd>" + total.vProd + "</vProd>");
            XmlString.Append("		<vFrete>" + total.vFrete + "</vFrete>");
            XmlString.Append("		<vSeg>" + total.vSeg + "</vSeg>");
            XmlString.Append("		<vDesc>" + total.vDesc + "</vDesc>");
            XmlString.Append("		<vII>0.00</vII>");
            XmlString.Append("		<vIPI>" + total.vIPI + "</vIPI>");
            XmlString.Append("		<vPIS>0.00</vPIS>");
            XmlString.Append("		<vCOFINS>0.00</vCOFINS>");
            XmlString.Append("		<vOutro>" + total.vOutro + "</vOutro>");
            XmlString.Append("		<vNF>" + total.vNF + "</vNF>");

            if (!string.IsNullOrEmpty(total.vTotTrib))
                XmlString.Append("		<vTotTrib>" + total.vTotTrib + "</vTotTrib>");

            XmlString.Append("	</ICMSTot>");
            XmlString.Append("</total>");
        }

        private void MontaTRANSP()
        {
            XmlString.Append("<transp>");
            XmlString.Append("	<modFrete>" + transp.modFrete + "</modFrete>");

            if (!String.IsNullOrEmpty(transp.CNPJ))
            {
                XmlString.Append("	<transporta>");

                if (!String.IsNullOrEmpty(transp.CNPJ))
                    XmlString.Append("		<CNPJ>" + transp.CNPJ + "</CNPJ>");

                /* 
                if(!String.IsNullOrEmpty(this.transp.CPF))
                    this.XmlString.Append("		<CPF>" + this.transp.CPF + "</CPF>");
                */

                if (!String.IsNullOrEmpty(transp.xNome))
                    XmlString.Append("		<xNome>" + transp.xNome + "</xNome>");

                if (!String.IsNullOrEmpty(transp.IE))
                {
                    XmlString.Append("		<IE>" + transp.IE + "</IE>");
                }

                if (!String.IsNullOrEmpty(transp.xEnder))
                {
                    XmlString.Append("		<xEnder>" + transp.xEnder + "</xEnder>");
                }

                if (!String.IsNullOrEmpty(transp.xMun))
                {
                    XmlString.Append("		<xMun>" + transp.xMun + "</xMun>");
                }

                if (!String.IsNullOrEmpty(transp.UF))
                {
                    XmlString.Append("		<UF>" + transp.UF.Trim() + "</UF>");
                }

                XmlString.Append("	</transporta>");
            }
            if (!String.IsNullOrEmpty(transp.veic_placa))
            {
                XmlString.Append("	<veicTransp>");
                XmlString.Append("		<placa>" + transp.veic_placa + "</placa>");
                XmlString.Append("		<UF>" + transp.veic_UF + "</UF>");
                XmlString.Append("	</veicTransp>");
            }

            if (!String.IsNullOrEmpty(transp.qVol))
            {
                XmlString.Append("	<vol>");
                XmlString.Append("		<qVol>" + transp.qVol + "</qVol>");
                XmlString.Append("		<esp>" + transp.esp + "</esp>");


                if (!String.IsNullOrEmpty(transp.marca))
                {
                    XmlString.Append("		<marca>" + transp.marca + "</marca>");
                }

                if (!String.IsNullOrEmpty(transp.nVol))
                    XmlString.Append("		<nVol>" + transp.nVol + "</nVol>");

                if (!String.IsNullOrEmpty(transp.pesoL))
                    XmlString.Append("		<pesoL>" + transp.pesoL + "</pesoL>");

                if (!String.IsNullOrEmpty(transp.pesoB))
                    XmlString.Append("		<pesoB>" + transp.pesoB + "</pesoB>");

                XmlString.Append("	</vol>");
            }
            XmlString.Append("</transp>");
        }

        private void MontaCOBR()
        {
            if (!String.IsNullOrEmpty(cobr.nFat))
            {
                XmlString.Append("<cobr>");
                XmlString.Append("	<fat>");
                XmlString.Append("		<nFat>" + cobr.nFat + "</nFat>");
                XmlString.Append("		<vOrig>" + cobr.vOrig + "</vOrig>");
                XmlString.Append("		<vLiq>" + cobr.vLiq + "</vLiq>");
                XmlString.Append("	</fat>");

                for (int i = 0; i < cobr.dup.Count; i++)
                {
                    XmlString.Append("	<dup>");
                    XmlString.Append("		<nDup>" + cobr.dup[i].nDup + "</nDup>");
                    XmlString.Append("		<dVenc>" + cobr.dup[i].dVenc + "</dVenc>");
                    XmlString.Append("		<vDup>" + cobr.dup[i].vDup + "</vDup>");
                    XmlString.Append("	</dup>");
                }

                XmlString.Append("</cobr>");
            }
        }
    }
}