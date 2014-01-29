using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

namespace WallegNfe
{
    public class Nota
    {
        public Model.Nota.IDE ide { get; set; }
        public Model.Nota.EMIT emit { get; set; }
        public Model.Nota.DEST dest { get; set; }
        private List<Model.Nota.DET> detList = null;
        public Model.Nota.TOTAL total { get; set; }
        public Model.Nota.TRANSP transp { get; set; }
        public Model.Nota.COBR cobr { get; set; }
        public String infAdic { get; set; }

        private StringBuilder XmlString = null;

        public String CaminhoFisico = "";
        public String ArquivoNome = "";
        public String ConteudoXml = "";
        public String NotaId = "";
        public Model.NotaSituacao Situacao { get; set; }

        public Nota()
        {
            this.ide = new Model.Nota.IDE();
            this.emit = new Model.Nota.EMIT();
            this.dest = new Model.Nota.DEST();
            this.detList = new List<Model.Nota.DET>();
            this.total = new Model.Nota.TOTAL();
            this.transp = new Model.Nota.TRANSP();
            this.cobr = new Model.Nota.COBR();

            this.XmlString = new StringBuilder();

            Console.WriteLine("Iniciando");
        }

        public void AddDet(Model.Nota.DET det)
        {
            this.detList.Add(det);
        }

        public void SalvarNota(String caminho)
        {
            /*
            this.CaminhoFisico = "C:\\NFE\\nota-fixa.xml";

            System.IO.StreamReader SR = null;
            SR = System.IO.File.OpenText(this.CaminhoFisico);
            string xmlString = SR.ReadToEnd();
            SR.Close();
            SR = null;
            this.ConteudoXml = xmlString;

            return;
            */

            
            //#todo, fazer isso ser so em amb. de homologacao
            this.emit.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
            this.dest.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
            
            Random random = new Random();
            String codigoNumerico = random.Next(10000000, 99999999).ToString("D8");
            this.ide.cNF = codigoNumerico;

            String result = this.ide.cUF + this.ide.dEmi.Replace("-", "").Substring(2, 4) + this.emit.CNPJ +  System.Int32.Parse(this.ide.mod).ToString("D2") + System.Int32.Parse(this.ide.serie).ToString("D3") + System.Int32.Parse(this.ide.nNF).ToString("D9") + System.Int32.Parse(this.ide.tpEmis).ToString("D1") + codigoNumerico;
            String digitoVerificador =  this.GerarModulo11(result);

            result = result + digitoVerificador;
            this.NotaId = result;

            this.ide.cDV = digitoVerificador;

            //this.XmlString.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>"); não precisa pq o lote já tem isso

            this.XmlString.Append("<NFe xmlns=\"http://www.portalfiscal.inf.br/nfe\">");
            this.XmlString.Append("   <infNFe Id=\"NFe" + result + "\" versao=\"2.00\">");

            MontaIDE();
            MontaEMIT();
            MontaDEST();
            MontaDET();
            MontaTOTAL();
            MontaTRANSP();
            //MontaCOBR();


            if (!String.IsNullOrEmpty(this.infAdic))
            {
                this.XmlString.Append("<infAdic>");
                this.XmlString.Append("	<infCpl>");
                this.XmlString.Append(this.infAdic);
                this.XmlString.Append("	</infCpl>");
                this.XmlString.Append("</infAdic>");
            }
            


            this.XmlString.Append("   </infNFe>");
            //this.XmlString.Append("   <Signature></Signature>");
            this.XmlString.Append("</NFe>");

            XmlDocument xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.LoadXml(this.XmlString.ToString().Replace("&","&amp;"));
            }
            catch (Exception e)
            {

                /*
                String SignedFile = "C:\\nota-debug.XML";
                System.IO.StreamWriter SW_2 = System.IO.File.CreateText(SignedFile);
                SW_2.Write(this.XmlString);
                SW_2.Close();
                */

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

            this.CaminhoFisico = caminho;
            this.ConteudoXml = this.XmlString.ToString();
        }

        private void MontaIDE()
        {
            
            this.XmlString.Append("<ide>");
            this.XmlString.Append("	<cUF>" + this.ide.cUF + "</cUF>");
            this.XmlString.Append("	<cNF>" + this.ide.cNF + "</cNF>");
            this.XmlString.Append("	<natOp>" + this.ide.natOp + "</natOp>");
            this.XmlString.Append("	<indPag>" + this.ide.indPag + "</indPag>"); //0 - a vista, 1 - a prazo, 2 - outros
            this.XmlString.Append("	<mod>" + this.ide.mod + "</mod>");
            this.XmlString.Append("	<serie>" + this.ide.serie + "</serie>");
            this.XmlString.Append("	<nNF>" + this.ide.nNF + "</nNF>");
            this.XmlString.Append("	<dEmi>" + this.ide.dEmi  + "</dEmi>");

            if(!String.IsNullOrEmpty(this.ide.dSaiEnt))
                this.XmlString.Append("	<dSaiEnt>" + this.ide.dSaiEnt + "</dSaiEnt>");

            /*
            if (!String.IsNullOrEmpty(this.ide.hSaiEnt))
                this.XmlString.Append("	<hSaiEnt>" + this.ide.hSaiEnt + "</hSaiEnt>");
            */

            this.XmlString.Append("	<tpNF>" + this.ide.tpNF + "</tpNF>");
            this.XmlString.Append("	<cMunFG>" + this.ide.cMunFG + "</cMunFG>");
            this.XmlString.Append("	<tpImp>" + this.ide.tpImp   + "</tpImp>");
            this.XmlString.Append("	<tpEmis>" + this.ide.tpEmis   + "</tpEmis>");
            this.XmlString.Append("	<cDV>" + this.ide.cDV   + "</cDV>");
            this.XmlString.Append("	<tpAmb>" + this.ide.tpAmb   + "</tpAmb>");
            this.XmlString.Append("	<finNFe>" + this.ide.finNFe   + "</finNFe>");
            this.XmlString.Append("	<procEmi>" + this.ide.procEmi   + "</procEmi>");
            this.XmlString.Append("	<verProc>2.2.19</verProc>");
            this.XmlString.Append("</ide>");
        }

        private String GerarModulo11(String chaveAcesso)
        {
            // Cálculo do módulo 11.
            // DV = Digito verificador.

            // O peso é o multiplicador da expressão, deve ser somente de 2 à 9, então já iniciamos com 2.
            int peso = 2;
            // Somatória do resultado.
            int soma = 0;

            try
            {
                // Passa número a número da chave pegando da direita pra esquerda (pra isso o Reverse()).
                chaveAcesso.ToCharArray()
                    .Reverse()
                    .ToList()
                    .ForEach(f =>
                    {
                        // Acumula valores da soma gerada das multiplicações (peso).
                        soma += (Convert.ToInt32(f.ToString()) * peso);
                        // Como o peso pode ir somente até 9 é feito essa validação.
                        peso = (peso == 9) ? 2 : peso + 1;
                    });

                // DV = 11 - (resto da divisão)
                // Quando o resto da divisão for 0 (zero) ou 1 (um), o DV deverá ser igual a 0 (zero).
                return (11 - (soma % 11)) <= 1 ? "0" : (11 - (soma % 11)).ToString();
            }
            catch
            {
                return "ERRO: A chave de acesso deve conter apenas números.";
            }
        }

        private void MontaEMIT()
        {
            this.XmlString.Append("<emit>");

            if(!String.IsNullOrEmpty(this.emit.CPF))
            {
                this.XmlString.Append("	<CPF>" + this.emit.CPF  + "</CPF>");
            }
             if(!String.IsNullOrEmpty(this.emit.CNPJ))
            {
                this.XmlString.Append("	<CNPJ>" + this.emit.CNPJ + "</CNPJ>");
            }

            this.XmlString.Append("	<xNome>" + this.emit.xNome + "</xNome>");
            this.XmlString.Append("	<enderEmit>");
            this.XmlString.Append("		<xLgr>" + this.emit.xLgr + "</xLgr>");
            this.XmlString.Append("		<nro>" + this.emit.nro + "</nro>");
            this.XmlString.Append("		<xBairro>" +this.emit.xBairro + "</xBairro>");
            this.XmlString.Append("		<cMun>" + this.emit.cMun + "</cMun>");
            this.XmlString.Append("		<xMun>" +this.emit.xMun + "</xMun>");
            this.XmlString.Append("		<UF>" + this.emit.UF.Trim() + "</UF>");
            this.XmlString.Append("		<CEP>" + this.emit.CEP + "</CEP>");
            this.XmlString.Append("		<cPais>1058</cPais>");
            this.XmlString.Append("		<xPais>BRASIL</xPais>");

            if(!String.IsNullOrEmpty(this.emit.fone))
                this.XmlString.Append("		<fone>" + this.emit.fone + "</fone>");

            this.XmlString.Append("	</enderEmit>");

            this.XmlString.Append("	<IE>" + this.emit.IE + "</IE>");
            this.XmlString.Append("	<CRT>" + this.emit.CRT + "</CRT>");
            this.XmlString.Append("</emit>");
        }

        private void MontaDEST()
        {
            this.XmlString.Append("<dest>");

            if(!String.IsNullOrEmpty(this.dest.CPF))
            {
                this.XmlString.Append("	<CPF>" + this.dest.CPF  + "</CPF>");
            }
            
            if(!String.IsNullOrEmpty(this.dest.CNPJ))
            {
                this.XmlString.Append("	<CNPJ>" + this.dest.CNPJ + "</CNPJ>");
            }

            this.XmlString.Append("	<xNome>" + this.dest.xNome + "</xNome>");
            this.XmlString.Append("	<enderDest>");
            this.XmlString.Append("		<xLgr>" + this.dest.xLgr + "</xLgr>");
            this.XmlString.Append("		<nro>" + this.dest.nro + "</nro>");

            if (!String.IsNullOrEmpty(this.dest.xCpl))
            {
                this.XmlString.Append("		<xCpl>" + this.dest.xCpl + "</xCpl>");
            }

            this.XmlString.Append("		<xBairro>" + this.dest.xBairro + "</xBairro>");
            this.XmlString.Append("		<cMun>" + this.dest.cMun + "</cMun>");
            this.XmlString.Append("		<xMun>" + this.dest.xMun + "</xMun>");
            this.XmlString.Append("		<UF>" + this.dest.UF.Trim() + "</UF>");

            if(!String.IsNullOrEmpty(this.dest.CEP))
                this.XmlString.Append("		<CEP>" + this.dest.CEP + "</CEP>");

            this.XmlString.Append("		<cPais>1058</cPais>");
            this.XmlString.Append("		<xPais>BRASIL</xPais>");

            if(!String.IsNullOrEmpty(this.dest.fone))
                this.XmlString.Append("		<fone>" + this.dest.fone + "</fone>");

            this.XmlString.Append("	</enderDest>");

            this.XmlString.Append("	<IE>" + this.dest.IE + "</IE>");    
   
            if(!String.IsNullOrEmpty(this.dest.email))
                this.XmlString.Append("	<email>" + this.dest.email + "</email>");

            this.XmlString.Append("</dest>");
        }

        private void MontaDET()
        {
            for(int i=0;i< this.detList.Count;i++)
            {
                this.XmlString.Append("<det nItem=\"" + (i + 1).ToString() + "\">");
                this.XmlString.Append("	<prod>");
                this.XmlString.Append("		<cProd>" + this.detList[i].cProd.Trim() + "</cProd>");
                this.XmlString.Append("		<cEAN>" + this.detList[i].cEAN + "</cEAN>");
                this.XmlString.Append("		<xProd>" + this.detList[i].xProd + "</xProd>");
                this.XmlString.Append("		<NCM>" + this.detList[i].NCM + "</NCM>");
                this.XmlString.Append("		<CFOP>" + this.detList[i].CFOP + "</CFOP>");
                this.XmlString.Append("		<uCom>" + this.detList[i].uCom + "</uCom>");
                this.XmlString.Append("		<qCom>" + this.detList[i].qCom + "</qCom>");
                this.XmlString.Append("		<vUnCom>" + this.detList[i].vUnCom + "</vUnCom>");
                this.XmlString.Append("		<vProd>" + this.detList[i].vProd + "</vProd>");
                this.XmlString.Append("		<cEANTrib>" + this.detList[i].cEANTrib + "</cEANTrib>");
                this.XmlString.Append("		<uTrib>" + this.detList[i].uTrib + "</uTrib>");
                this.XmlString.Append("		<qTrib>" + this.detList[i].qTrib + "</qTrib>");
                this.XmlString.Append("		<vUnTrib>" + this.detList[i].vUnTrib + "</vUnTrib>");

                if(!String.IsNullOrEmpty(this.detList[i].vFrete))
                {
                    this.XmlString.Append("		<vFrete>" + this.detList[i].vFrete + "</vFrete>");
                }
                if (!String.IsNullOrEmpty(this.detList[i].vDesc))
                {
                    this.XmlString.Append("		<vDesc>" + this.detList[i].vDesc + "</vDesc>");
                }
                //#TODO , ver o o wagner porque isso vai ser pego de uma tabela de regras de acordo com o CFOP
                this.XmlString.Append("		<indTot>1</indTot>");
                this.XmlString.Append("	</prod>");

                this.XmlString.Append("	<imposto>");

                this.MontaDET_ICMS(this.detList[i]);
                this.MontaDET_IPI(this.detList[i]);
                this.MontaDET_PIS(this.detList[i]);
                this.MontaDET_COFINS(this.detList[i]);

                this.XmlString.Append("	</imposto>");

                this.XmlString.Append("</det>");
            }
        }

        private void MontaDET_ICMS(Model.Nota.DET det)
        {
            this.XmlString.Append("<ICMS>");

            switch (det.icms)
            {
                case Model.Nota.Enum.ICMS.ICMS00:
                    this.XmlString.Append("<ICMS00>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    this.XmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    this.XmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    this.XmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    this.XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    this.XmlString.Append("</ICMS00>");
                    break;
                case Model.Nota.Enum.ICMS.ICMS10:
                    this.XmlString.Append("<ICMS10>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    this.XmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    this.XmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    this.XmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    this.XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");

                    this.XmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        this.XmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        this.XmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    this.XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    this.XmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    this.XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");

                    this.XmlString.Append("</ICMS10>");
                    break;
                case Model.Nota.Enum.ICMS.ICMS20:
                    this.XmlString.Append("<ICMS20>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    this.XmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    this.XmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    this.XmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    this.XmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    this.XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    this.XmlString.Append("</ICMS20>");
                    break;
                case Model.Nota.Enum.ICMS.ICMS30:
                    this.XmlString.Append("<ICMS30>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    this.XmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");
                    this.XmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    this.XmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    this.XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    this.XmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    this.XmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");
                    this.XmlString.Append("</ICMS30>");
                    break;
                case Model.Nota.Enum.ICMS.ICMS40_50:
                    this.XmlString.Append("<ICMS40>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    this.XmlString.Append("</ICMS40>");
                    break;
                case Model.Nota.Enum.ICMS.ICMS51:
                    this.XmlString.Append("<ICMS51>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    this.XmlString.Append("</ICMS51>");

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        this.XmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        this.XmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        this.XmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        this.XmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        this.XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    }
                    break;
                case Model.Nota.Enum.ICMS.ICMS60:
                    this.XmlString.Append("<ICMS60>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    this.XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    this.XmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");
                    this.XmlString.Append("</ICMS60>");
                    break;
                case Model.Nota.Enum.ICMS.ICMS70:
                    this.XmlString.Append("<ICMS70>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    this.XmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    this.XmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    this.XmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");
                    this.XmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    this.XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    this.XmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        this.XmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        this.XmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    this.XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    this.XmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    this.XmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");
                    this.XmlString.Append("</ICMS70>");
                    break;
                case Model.Nota.Enum.ICMS.ICMS90:
                    this.XmlString.Append("<ICMS90>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CST>" + det.icms_CST + "</CST>");
                    this.XmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    this.XmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");

                    if (!String.IsNullOrEmpty(det.icms_pRedBC))
                    {
                        this.XmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    }

                    this.XmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    this.XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    this.XmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        this.XmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        this.XmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    this.XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    this.XmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    this.XmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");
                    this.XmlString.Append("</ICMS90>");
                    break;
                case Model.Nota.Enum.ICMS.ICMS101:
                    this.XmlString.Append("<ICMSSN101>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    this.XmlString.Append("    <pCredSN>" + det.icms_pCredSN + "</pCredSN>");
                    this.XmlString.Append("    <vCredICMSSN>" + det.icms_vCredICMSSN + "</vCredICMSSN>");
                    this.XmlString.Append("</ICMSSN101>");
                    break;
                case Model.Nota.Enum.ICMS.ICMS102_400:
                    this.XmlString.Append("<ICMSSN102>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    this.XmlString.Append("</ICMSSN102>");
                    break;
                case Model.Nota.Enum.ICMS.ICMS201:
                    this.XmlString.Append("<ICMSSN201>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    this.XmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        this.XmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        this.XmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    this.XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    this.XmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    this.XmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");

                    this.XmlString.Append("</ICMSSN201>");
                    break;
                case Model.Nota.Enum.ICMS.ICMS202:
                    this.XmlString.Append("<ICMSSN202>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    this.XmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        this.XmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        this.XmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    this.XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    this.XmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    this.XmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");

                    this.XmlString.Append("</ICMSSN202>");
                    break;
                case Model.Nota.Enum.ICMS.ICMS500:
                    this.XmlString.Append("<ICMSSN500>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    this.XmlString.Append("    <vBCSTRet>" + det.icms_vBCSTRet + "</vBCSTRet>");
                    this.XmlString.Append("    <vICMSSTRet>" + det.icms_vICMSSTRet + "</vICMSSTRet>");
                    this.XmlString.Append("</ICMSSN500>");
                    break;
                case Model.Nota.Enum.ICMS.ICMS900:
                    this.XmlString.Append("<ICMSSN900>");
                    this.XmlString.Append("    <orig>" + det.icms_orig + "</orig>");
                    this.XmlString.Append("    <CSOSN>" + det.icms_CSOSN + "</CSOSN>");
                    this.XmlString.Append("    <modBC>" + det.icms_modBC + "</modBC>");
                    this.XmlString.Append("    <vBC>" + det.icms_vBC + "</vBC>");

                    if (!String.IsNullOrEmpty(det.icms_pRedBC))
                    {
                        this.XmlString.Append("    <pRedBC>" + det.icms_pRedBC + "</pRedBC>");
                    }

                    this.XmlString.Append("    <pICMS>" + det.icms_pICMS + "</pICMS>");
                    this.XmlString.Append("    <vICMS>" + det.icms_vICMS + "</vICMS>");
                    this.XmlString.Append("    <modBCST>" + det.icms_modBCST + "</modBCST>");

                    if (!String.IsNullOrEmpty(det.icms_pMVAST))
                    {
                        this.XmlString.Append("    <pMVAST>" + det.icms_pMVAST + "</pMVAST>");
                    }

                    if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                    {
                        this.XmlString.Append("    <pRedBCST>" + det.icms_pRedBCST + "</pRedBCST>");
                    }

                    this.XmlString.Append("    <vBCST>" + det.icms_vBCST + "</vBCST>");
                    this.XmlString.Append("    <pICMSST>" + det.icms_pICMSST + "</pICMSST>");
                    this.XmlString.Append("    <vICMSST>" + det.icms_vICMSST + "</vICMSST>");
                    this.XmlString.Append("    <pCredSN>" + det.icms_pCredSN + "</pCredSN>");
                    this.XmlString.Append("    <vCredICMSSN>" + det.icms_vCredICMSSN + "</vCredICMSSN>");
                    this.XmlString.Append("</ICMSSN900>");
                    break;
                    
            }
            this.XmlString.Append("</ICMS>");
        }

        private void MontaDET_IPI(Model.Nota.DET det)
        {
            if (!String.IsNullOrEmpty(det.ipi_cIEnq))
            {

                this.XmlString.Append("<IPI>");

                this.XmlString.Append("<cIEnq>" + det.ipi_cIEnq + "</cIEnq>");


                switch (det.ipi)
                {

                    case Model.Nota.Enum.IPI.IPI00_49_50_99:
                        this.XmlString.Append("<IPITrib>");

                        this.XmlString.Append("    <CST>" + det.ipi_CST + "</CST>");

                        if (!String.IsNullOrEmpty(det.icms_pRedBCST))
                        {
                            this.XmlString.Append("    <vBC>" + det.ipi_vBC + "</vBC>");
                            this.XmlString.Append("    <pIPI>" + det.ipi_pIPI + "</pIPI>");
                        }
                        else
                        {
                            this.XmlString.Append("    <qUnid>" + det.ipi_qUnid + "</qUnid>");
                            this.XmlString.Append("    <vUnid>" + det.ipi_vUnid + "</vUnid>");
                        }

                        this.XmlString.Append("    <vIPI>" + det.ipi_vIPI + "</vIPI>");

                        this.XmlString.Append("</IPITrib>");
                        break;
                    case Model.Nota.Enum.IPI.IPI01_55:
                        this.XmlString.Append("<IPINT>");
                        this.XmlString.Append("    <CST>" + det.ipi_CST + "</CST>");
                        this.XmlString.Append("</IPINT>");
                        break;
                }

                this.XmlString.Append("</IPI>");
            }
        }

        private void MontaDET_PIS(Model.Nota.DET det)
        {
            this.XmlString.Append("<PIS>");

            switch (det.pis)
            {
                case Model.Nota.Enum.PIS.PIS01_02:
                    this.XmlString.Append("<PISAliq>");
                    this.XmlString.Append("    <CST>" + det.pis_CST + "</CST>");
                    this.XmlString.Append("    <vBC>" + det.pis_vBC + "</vBC>");
                    this.XmlString.Append("    <pPIS>" + det.pis_pPIS + "</pPIS>");
                    this.XmlString.Append("    <vPIS>" + det.pis_vPIS + "</vPIS>");
                    this.XmlString.Append("</PISAliq>");
                    break;
                case Model.Nota.Enum.PIS.PIS03:
                    this.XmlString.Append("<PISQtde>");
                    this.XmlString.Append("    <CST>" + det.pis_CST + "</CST>");
                    this.XmlString.Append("    <qBCProd>" + det.pis_qBCProd + "</qBCProd>");
                    this.XmlString.Append("    <vAliqProd>" + det.pis_vAliqProd + "</vAliqProd>");
                    this.XmlString.Append("    <vPIS>" + det.pis_vPIS + "</vPIS>");
                    this.XmlString.Append("</PISQtde>");
                    break;
                case Model.Nota.Enum.PIS.PIS04_09:
                    this.XmlString.Append("<PISNT>");
                    this.XmlString.Append("    <CST>" + det.pis_CST + "</CST>");
                    this.XmlString.Append("</PISNT>");
                    break;
                case Model.Nota.Enum.PIS.PIS99:
                    this.XmlString.Append("<PISOutr>");
                    this.XmlString.Append("    <CST>" + det.pis_CST + "</CST>");

                    if (!String.IsNullOrEmpty(det.pis_vBC) && !String.IsNullOrEmpty(det.pis_pPIS))
                    {
                        this.XmlString.Append("    <vBC>" + det.pis_vBC + "</vBC>");
                        this.XmlString.Append("    <pPIS>" + det.pis_pPIS + "</pPIS>");
                    }
                    else
                    {
                        this.XmlString.Append("    <qBCProd>" + det.pis_qBCProd + "</qBCProd>");
                        this.XmlString.Append("    <vAliqProd>" + det.pis_vAliqProd + "</vAliqProd>");
                    }

                    this.XmlString.Append("    <vPIS>" + det.pis_vPIS + "</vPIS>");
                    this.XmlString.Append("</PISOutr>");
                    break;
            }

            this.XmlString.Append("</PIS>");
        }


        private void MontaDET_COFINS(Model.Nota.DET det)
        {
            this.XmlString.Append("<COFINS>");

            switch (det.cofins)
            {
                case Model.Nota.Enum.COFINS.CST01_02:
                    this.XmlString.Append("<COFINSAliq>");
                    this.XmlString.Append("    <CST>" + det.cofins_CST + "</CST>");
                    this.XmlString.Append("    <vBC>" + det.cofins_vBC + "</vBC>");
                    this.XmlString.Append("    <pCOFINS>" + det.cofins_pCOFINS + "</pCOFINS>");
                    this.XmlString.Append("    <vCOFINS>" + det.cofins_vCOFINS + "</vCOFINS>");
                    this.XmlString.Append("</COFINSAliq>");
                    break;
                case Model.Nota.Enum.COFINS.CST03:
                    this.XmlString.Append("<COFINSQtde>");
                    this.XmlString.Append("    <CST>" + det.cofins_CST + "</CST>");
                    this.XmlString.Append("    <qBCProd>" + det.cofins_qBCProd + "</qBCProd>");
                    this.XmlString.Append("    <vAliqProd>" + det.cofins_vAliqProd + "</vAliqProd>");
                    this.XmlString.Append("    <vCOFINS>" + det.cofins_vCOFINS + "</vCOFINS>");
                    this.XmlString.Append("</COFINSQtde>");
                    break;
                case Model.Nota.Enum.COFINS.CST04_09:
                    this.XmlString.Append("<COFINSNT>");
                    this.XmlString.Append("    <CST>" + det.cofins_CST + "</CST>");
                    this.XmlString.Append("</COFINSNT>");
                    break;
                case Model.Nota.Enum.COFINS.CST99:
                    this.XmlString.Append("<COFINSOutr>");
                    this.XmlString.Append("    <CST>" + det.cofins_CST + "</CST>");

                    if (!String.IsNullOrEmpty(det.cofins_vBC) && !String.IsNullOrEmpty(det.cofins_pCOFINS))
                    {
                        this.XmlString.Append("    <vBC>" + det.cofins_vBC + "</vBC>");
                        this.XmlString.Append("    <pCOFINS>" + det.cofins_pCOFINS + "</pCOFINS>");
                    }
                    else
                    {
                        this.XmlString.Append("    <qBCProd>" + det.cofins_qBCProd + "</qBCProd>");
                        this.XmlString.Append("    <vAliqProd>" + det.cofins_vAliqProd + "</vAliqProd>");
                    }

                    this.XmlString.Append("    <vCOFINS>" + det.cofins_vCOFINS + "</vCOFINS>");
                    this.XmlString.Append("</COFINSOutr>");
                    break;
            }

            this.XmlString.Append("</COFINS>");
        }

        private void MontaTOTAL()
        {
            this.XmlString.Append("<total>");
            this.XmlString.Append("	<ICMSTot>");
            this.XmlString.Append("		<vBC>" + this.total.vBC + "</vBC>");
            this.XmlString.Append("		<vICMS>" + this.total.vICMS + "</vICMS>");
            this.XmlString.Append("		<vBCST>" + this.total.vBCST + "</vBCST>");
            this.XmlString.Append("		<vST>" + this.total.vST + "</vST>");
            this.XmlString.Append("		<vProd>" + this.total.vProd + "</vProd>");
            this.XmlString.Append("		<vFrete>" + this.total.vFrete + "</vFrete>");
            this.XmlString.Append("		<vSeg>" + this.total.vSeg + "</vSeg>");
            this.XmlString.Append("		<vDesc>" + this.total.vDesc + "</vDesc>");
            this.XmlString.Append("		<vII>0.00</vII>");
            this.XmlString.Append("		<vIPI>" + this.total.vIPI + "</vIPI>");
            this.XmlString.Append("		<vPIS>0.00</vPIS>");
            this.XmlString.Append("		<vCOFINS>0.00</vCOFINS>");
            this.XmlString.Append("		<vOutro>" + this.total.vOutro + "</vOutro>");
            this.XmlString.Append("		<vNF>" + this.total.vNF + "</vNF>");
            this.XmlString.Append("		<vTotTrib>0.00</vTotTrib>");
            this.XmlString.Append("	</ICMSTot>");
            this.XmlString.Append("</total>");
        }

        private void MontaTRANSP()
        {
            this.XmlString.Append("<transp>");
            this.XmlString.Append("	<modFrete>" + this.transp.modFrete + "</modFrete>");

            if(!String.IsNullOrEmpty(this.transp.CNPJ)){
                this.XmlString.Append("	<transporta>");
                
                if(!String.IsNullOrEmpty(this.transp.CNPJ))
                    this.XmlString.Append("		<CNPJ>" + this.transp.CNPJ + "</CNPJ>");

                /*
                if(!String.IsNullOrEmpty(this.transp.CPF))
                    this.XmlString.Append("		<CPF>" + this.transp.CPF + "</CPF>");
                */

                if(!String.IsNullOrEmpty(this.transp.xNome))
                    this.XmlString.Append("		<xNome>" + this.transp.xNome + "</xNome>");

                if(!String.IsNullOrEmpty(this.transp.IE)){
                    this.XmlString.Append("		<IE>" + this.transp.IE + "</IE>");
                }

                if(!String.IsNullOrEmpty(this.transp.xEnder)){
                    this.XmlString.Append("		<xEnder>" + this.transp.xEnder + "</xEnder>");
                }

                if(!String.IsNullOrEmpty(this.transp.xMun)){
                    this.XmlString.Append("		<xMun>" + this.transp.xMun + "</xMun>");
                }

                if(!String.IsNullOrEmpty(this.transp.UF)){
                    this.XmlString.Append("		<UF>" + this.transp.UF.Trim() + "</UF>");
                }

                this.XmlString.Append("	</transporta>");
            }
            if(!String.IsNullOrEmpty(this.transp.veic_placa)){
                this.XmlString.Append("	<veicTransp>");
                this.XmlString.Append("		<placa>" + this.transp.veic_placa + "</placa>");
                this.XmlString.Append("		<UF>" + this.transp.veic_UF + "</UF>");
                this.XmlString.Append("	</veicTransp>");
            }

            if (!String.IsNullOrEmpty(this.transp.qVol))
            {
                this.XmlString.Append("	<vol>");
                this.XmlString.Append("		<qVol>" + this.transp.qVol + "</qVol>");
                this.XmlString.Append("		<esp>" + this.transp.esp + "</esp>");
            

                if (!String.IsNullOrEmpty(this.transp.marca))
                {
                    this.XmlString.Append("		<marca>" + this.transp.marca + "</marca>");
                }

                if(!String.IsNullOrEmpty(this.transp.nVol))
                    this.XmlString.Append("		<nVol>" + this.transp.nVol + "</nVol>");

                if(!String.IsNullOrEmpty(this.transp.pesoL))
                    this.XmlString.Append("		<pesoL>" + this.transp.pesoL + "</pesoL>");

                if(!String.IsNullOrEmpty(this.transp.pesoB))
                    this.XmlString.Append("		<pesoB>" + this.transp.pesoB + "</pesoB>");

                this.XmlString.Append("	</vol>");
            }
            this.XmlString.Append("</transp>");
        }

        private void MontaCOBR()
        {
            return; //por enquanto deixar isso de lado porque precisa ser opcional a cobrança

            this.XmlString.Append("<cobr>");
            this.XmlString.Append("	<fat>");
            this.XmlString.Append("		<nFat>" + this.cobr.nFat + "</nFat>");
            this.XmlString.Append("		<vOrig>" + this.cobr.vOrig + "</vOrig>");
            this.XmlString.Append("		<vLiq>" + this.cobr.vLiq + "</vLiq>");
            this.XmlString.Append("	</fat>");

            for (int i = 0; i < this.cobr.dup.Count; i++)
            {
                this.XmlString.Append("	<dup>");
                this.XmlString.Append("		<nDup>" + this.cobr.dup[i].nDup + "</nDup>");
                this.XmlString.Append("		<dVenc>" + this.cobr.dup[i].dVenc + "</dVenc>");
                this.XmlString.Append("		<vDup>" + this.cobr.dup[i].vDup + "</vDup>");
                this.XmlString.Append("	</dup>");
            }

            this.XmlString.Append("</cobr>");
        }
    }
}
