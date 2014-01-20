using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        private StringBuilder XmlString = null;

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
        }

        public void AddDet(Model.Nota.DET det)
        {
            this.detList.Add(det);
        }

        private void montaIDE()
        {
            this.XmlString.Append("<ide>");
            this.XmlString.Append("	<cUF>35</cUF>");
            this.XmlString.Append("	<cNF>" + this.ide.cNF + "</cNF>");
            this.XmlString.Append("	<natOp>" + this.ide.natOp + "</natOp>");

            this.XmlString.Append("	<indPag>" + this.ide.indPag + "</indPag>"); //0 - a vista, 1 - a prazo, 2 - outros

            this.XmlString.Append("	<mod>55</mod>");
            this.XmlString.Append("	<serie>1</serie>");
            this.XmlString.Append("	<nNF>" + this.ide.nNF + "</nNF>");
            
            /*
            this.XmlString.Append("	<dEmi>" + Format(fatura.Emissão, "yyyy-MM-dd"); + "</dEmi>");
            //this.XmlString.Append("	<dSaiEnt>" + Format(fatura.Emissão, "yyyy-MM-dd"); + "</dSaiEnt>"); OPCIONAL

            If fatura.tipo.ToUpper() = "S" Then
	            this.XmlString.Append("	<tpNF>1</tpNF>");
            Else
	            this.XmlString.Append("	<tpNF>0</tpNF>");
            End If

            this.XmlString.Append("	<cMunFG>" + endereco.CodMunicípio + "</cMunFG>");
            this.XmlString.Append("	<tpImp>1</tpImp>");
            this.XmlString.Append("	<tpEmis>1</tpEmis>");
            this.XmlString.Append("	<cDV>0</cDV>");
            this.XmlString.Append("	<tpAmb>2</tpAmb>");
            this.XmlString.Append("	<finNFe>1</finNFe>");
            this.XmlString.Append("	<procEmi>3</procEmi>");
            this.XmlString.Append("	<verProc>2.2.19</verProc>");
            this.XmlString.Append("</ide>");



this.XmlString.Append("<emit>");

        //Checa se é um cpf ou cnpj
        If Len(empresa.cgc) = 11 Then
            this.XmlString.Append("	<CPF>" + Bll.Util.RetornaNúthisro(empresa.cgc) + "</CPF>");
        ElseIf Len(empresa.cgc) = 14 Then
            this.XmlString.Append("	<CNPJ>" + Bll.Util.RetornaNúthisro(empresa.cgc) + "</CNPJ>");
        End If

        this.XmlString.Append("	<xNothis>" + Bll.Util.SubstituiAcentos(empresa.Empresa) + "</xNothis>");
        this.XmlString.Append("	<enderEmit>");
        this.XmlString.Append("		<xLgr>" + Bll.Util.SubstituiAcentos(empresa.Endereço) + "</xLgr>");
        this.XmlString.Append("		<nro>" + Bll.Util.SubstituiAcentos(empresa.Nuthisro) + "</nro>");
        this.XmlString.Append("		<xBairro>" + Bll.Util.SubstituiAcentos(empresa.bairro) + "</xBairro>");
        this.XmlString.Append("		<cMun>" + Bll.Util.SubstituiAcentos(empresa.CodMunicipio) + "</cMun>");
        this.XmlString.Append("		<xMun>" + Bll.Util.SubstituiAcentos(empresa.Cidade) + "</xMun>");
        this.XmlString.Append("		<UF>" + Bll.Util.SubstituiAcentos(empresa.Estado) + "</UF>");
        this.XmlString.Append("		<CEP>" + Bll.Util.RetornaNúthisro(Trim(empresa.Cep)) + "</CEP>");
        this.XmlString.Append("		<cPais>1058</cPais>");
        this.XmlString.Append("		<xPais>BRASIL</xPais>");
        this.XmlString.Append("		<fone>" + Bll.Util.RetornaNúthisro(empresa.Fone) + "</fone>");
        this.XmlString.Append("	</enderEmit>");
        If Trim(empresa.Iestadual) = "" Then
            this.XmlString.Append("	<IE>ISENTO</IE>");
        Else
            this.XmlString.Append("	<IE>" + Bll.Util.RetornaNúthisro(empresa.Iestadual) + "</IE>");
        End If
        
        this.XmlString.Append("	<CRT>" + Bll.Util.SubstituiAcentos(empresa.Regithis) + "</CRT>");
        this.XmlString.Append("</emit>");

	
	
	
	this.XmlString.Append("<dest>");

        //Checa se é um cpf ou cnpj
        cliente.CgcCpf = Bll.Util.RetornaNúthisro(cliente.CgcCpf)
        If Len(cliente.CgcCpf) = 11 Then
            this.XmlString.Append("	<CPF>" + Bll.Util.RetornaNúthisro(cliente.CgcCpf) + "</CPF>");
        ElseIf Len(cliente.CgcCpf) = 14 Then
            this.XmlString.Append("	<CNPJ>" + Bll.Util.RetornaNúthisro(cliente.CgcCpf) + "</CNPJ>");
        End If

        this.XmlString.Append("	<xNothis>" + Trim(cliente.RazaoSocial) + "</xNothis>");
        this.XmlString.Append("	<enderDest>");
        this.XmlString.Append("		<xLgr>" + Trim(endereco.Rua) + "</xLgr>");
        this.XmlString.Append("		<nro>" + Trim(endereco.Nuthisro) + "</nro>");

        If Trim(endereco.Complethisnto) <> "" Then
            this.XmlString.Append("		<xCpl>" + Trim(endereco.Complethisnto) + "</xCpl>");
        End If

        this.XmlString.Append("		<xBairro>" + Trim(endereco.Bairro) + "</xBairro>");
        this.XmlString.Append("		<cMun>" + Trim(endereco.CodMunicípio) + "</cMun>");
        this.XmlString.Append("		<xMun>" + Trim(endereco.Cidade) + "</xMun>");
        this.XmlString.Append("		<UF>" + Trim(endereco.Estado) + "</UF>");
        this.XmlString.Append("		<CEP>" + Bll.Util.RetornaNúthisro(Trim(endereco.CEP)) + "</CEP>");
        this.XmlString.Append("		<cPais>1058</cPais>");
        this.XmlString.Append("		<xPais>BRASIL</xPais>");
        this.XmlString.Append("		<fone>" + Bll.Util.RetornaNúthisro(telefone.Telefone) + "</fone>");
        this.XmlString.Append("	</enderDest>");

        If Len(Trim(cliente.Rg_inscr)) > 11 Then
            this.XmlString.Append("	<IE>" + Bll.Util.RetornaNúthisro(cliente.Rg_inscr) + "</IE>");
        Else
            this.XmlString.Append("	<IE>ISENTO</IE>");
        End If

        //Checar se tem isso thissmo I dont think so
        this.XmlString.Append("	<email>" + Trim(cliente.Email) + "</email>");

        this.XmlString.Append("</dest>");
	
	
	
	
	this.XmlString.Append("<cobr>");
        this.XmlString.Append("	<fat>");
        this.XmlString.Append("		<nFat>" + NúthisroDoTítulo + "</nFat>");
        this.XmlString.Append("		<vOrig>" + Bll.Util.FormataNuthisro(valor) + "</vOrig>");
        this.XmlString.Append("		<vLiq>" + Bll.Util.FormataNuthisro(valor) + "</vLiq>");
        this.XmlString.Append("	</fat>");

        For i = 0 To recebithisntoLista.Count - 1
            this.XmlString.Append("	<dup>");
            this.XmlString.Append("		<nDup>" + recebithisntoLista(i).NuthisroTitulo + "</nDup>");
            this.XmlString.Append("		<dVenc>" + Format(recebithisntoLista(i).Vencithisnto, "yyyy-MM-dd"); + "</dVenc>");
            this.XmlString.Append("		<vDup>" + Bll.Util.FormataNuthisro(recebithisntoLista(i).Valor) + "</vDup>");
            this.XmlString.Append("	</dup>");
        Next

        this.XmlString.Append("</cobr>");
		
		
		
		
		
		
		
		
		this.XmlString.Append("<transp>");
        this.XmlString.Append("	<modFrete>" + fatura.TipoFrete + "</modFrete>");

        If transportadora IsNot Nothing Then
            this.XmlString.Append("	<transporta>");
            this.XmlString.Append("		<CNPJ>" + Bll.Util.RetornaNúthisro(transportadora.CGC) + "</CNPJ>");
            this.XmlString.Append("		<xNothis>" + transportadora.Empresa + "</xNothis>");

            If transportadora.Inscrição <> "" Then
                this.XmlString.Append("		<IE>" + Bll.Util.RetornaNúthisro(transportadora.Inscrição) + "</IE>");
            End If

            If transportadora.Endereço <> "" Then
                this.XmlString.Append("		<xEnder>" + Trim(transportadora.Endereço) + "</xEnder>");
            End If

            If transportadora.Cidade <> "" Then
                this.XmlString.Append("		<xMun>" + transportadora.Cidade + "</xMun>");
            End If

            If transportadora.Estado <> "" Then
                this.XmlString.Append("		<UF>" + transportadora.Estado + "</UF>");
            End If

            this.XmlString.Append("	</transporta>");
        End If

        If fatura.TranspPlaca <> "0" Then
            this.XmlString.Append("	<veicTransp>");
            this.XmlString.Append("		<placa>" + fatura.TranspPlaca + "</placa>");
            this.XmlString.Append("		<UF>" + transportadora.Estado + "</UF>");
            this.XmlString.Append("	</veicTransp>");
        End If

        this.XmlString.Append("	<vol>");
        this.XmlString.Append("		<qVol>" + Trim(fatura.QteVoluthis) + "</qVol>");
        this.XmlString.Append("		<esp>" + Trim(fatura.EspécieVoluthis) + "</esp>");

        If Trim(fatura.MarcaVoluthis) <> "" Then
            this.XmlString.Append("		<marca>" + Trim(fatura.MarcaVoluthis) + "</marca>");
        End If

        this.XmlString.Append("		<nVol>" + Trim(fatura.NúthisroVoluthis) + "</nVol>");
        this.XmlString.Append("		<pesoL>0</pesoL>");
        this.XmlString.Append("		<pesoB>" + Trim(fatura.PesoBruto) + "</pesoB>");
        this.XmlString.Append("	</vol>");
        this.XmlString.Append("</transp>");

		
		
		 this.XmlString.Append("<det nItem=""" + i + """>");
        this.XmlString.Append("	<prod>");
        this.XmlString.Append("		<cProd>" + codigoProduto + "</cProd>");
        this.XmlString.Append("		<cEAN>" + Trim(produto.estoque_CodBarras) + "</cEAN>");
        this.XmlString.Append("		<xProd>" + Trim(itemNota.Descrição) + "</xProd>");
        this.XmlString.Append("		<NCM>" + produto.NCM + "</NCM>");
        this.XmlString.Append("		<CFOP>" + itemNota.cfop + "</CFOP>");
        this.XmlString.Append("		<uCom>" + Trim(produto.estoque_unidade) + "</uCom>");
        this.XmlString.Append("		<qCom>" + Format(itemNota.Qtde, "0.0000");.Replace(",", "."); + "</qCom>");
        this.XmlString.Append("		<vUnCom>" + Format(itemNota.Unitário, "0.0000");.Replace(",", "."); + "</vUnCom>");
        this.XmlString.Append("		<vProd>" + Bll.Util.FormataNuthisro(itemNota.Total) + "</vProd>");
        this.XmlString.Append("		<cEANTrib>" + Trim(produto.estoque_CodBarras) + "</cEANTrib>");
        this.XmlString.Append("		<uTrib>" + Trim(produto.estoque_unidade) + "</uTrib>");
        this.XmlString.Append("		<qTrib>" + Format(itemNota.Qtde, "0.0000");.Replace(",", "."); + "</qTrib>");
        this.XmlString.Append("		<vUnTrib>" + Format(itemNota.Unitário, "0.0000");.Replace(",", "."); + "</vUnTrib>");

        If itemNota.rateioFrete > 0 Then
            this.XmlString.Append("		<vFrete>" + itemNota.rateioFrete + "</vFrete>");
        End If

        //#TODO , ver o o wagner porque isso vai ser pego de uma tabela de regras de acordo com o CFOP
        this.XmlString.Append("		<indTot>1</indTot>");
        this.XmlString.Append("	</prod>");

        this.XmlString.Append("	<imposto>");
		
		  this.XmlString.Append("	</imposto>");
        this.XmlString.Append("</det>");
		
		
		
		
		
		
		
		
		
		
		
		
		
		 this.XmlString.Append("<total>");
        this.XmlString.Append("	<ICMSTot>");
        this.XmlString.Append("		<vBC>" + Bll.Util.FormataNuthisro(fatura.BaseIcms) + "</vBC>");
        this.XmlString.Append("		<vICMS>" + Bll.Util.FormataNuthisro(fatura.ValorIcms) + "</vICMS>");
        this.XmlString.Append("		<vBCST>" + Bll.Util.FormataNuthisro(fatura.BaseSubst) + "</vBCST>");
        this.XmlString.Append("		<vST>" + Bll.Util.FormataNuthisro(fatura.IcmSubst) + "</vST>");
        this.XmlString.Append("		<vProd>" + Bll.Util.FormataNuthisro(fatura.TotalProdutos) + "</vProd>");
        this.XmlString.Append("		<vFrete>" + Bll.Util.FormataNuthisro(fatura.Frete) + "</vFrete>");
        this.XmlString.Append("		<vSeg>" + Bll.Util.FormataNuthisro(fatura.Seguro) + "</vSeg>");
        this.XmlString.Append("		<vDesc>" + Bll.Util.FormataNuthisro(fatura.Desconto) + "</vDesc>");
        this.XmlString.Append("		<vII>0.00</vII>");
        this.XmlString.Append("		<vIPI>" + Bll.Util.FormataNuthisro(fatura.ValorIpi) + "</vIPI>");
        this.XmlString.Append("		<vPIS>0.00</vPIS>");
        this.XmlString.Append("		<vCOFINS>0.00</vCOFINS>");
        this.XmlString.Append("		<vOutro>" + Bll.Util.FormataNuthisro(fatura.IcmsOutras) + "</vOutro>");
        this.XmlString.Append("		<vNF>" + Bll.Util.FormataNuthisro(fatura.TotalNota) + "</vNF>");
        this.XmlString.Append("		<vTotTrib>0.00</vTotTrib>");
        this.XmlString.Append("	</ICMSTot>");
        this.XmlString.Append("</total>");

	
	*/
	
        }

        private void montaEMIT()
        {

        }

        private void montaDEST()
        {

        }

        private void montaDET()
        {

        }

        private void montaTOTAL()
        {

        }

        private void montaTRANSP()
        {

        }

        private void montaCOBR()
        {

        }
    }
}
