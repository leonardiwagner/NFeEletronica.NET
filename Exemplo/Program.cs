using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Trabalhar com o certificado
using System.Security.Cryptography.X509Certificates;

namespace Exemplo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Cria a nota com o objeto "Nota"
            WallegNfe.Nota nota = new WallegNfe.Nota();

            nota.ide.cUF = "35";
            //cNF gerado automaticamente, mas voce pode inserir um custom com .cNF
            nota.ide.natOp = "Descrição da natureza";
            nota.ide.indPag = "0"; //'0 - a vista, 1 - a prazo, 2 - outros
            nota.ide.mod = "55";
            nota.ide.serie = "1";
            nota.ide.nNF = "12345";
            nota.ide.dEmi = "2014-01-23";
            //dSaiEnt e hSaiEnt são opcionais
            nota.ide.tpNF = "1";
            nota.ide.cMunFG = "3550308";
            //NFref opcional
            nota.ide.tpImp = "1";
            nota.ide.tpEmis = "1";
            nota.ide.cDV = "0";
            nota.ide.tpAmb = "2";
            nota.ide.finNFe = "1";
            nota.ide.procEmi = "3";

            nota.emit.CNPJ = "07293766000150"; //ou emit.CNPJ
            nota.emit.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
            nota.emit.xLgr = "R. da bavária";
            nota.emit.nro = "123";
            nota.emit.xBairro = "Santa Claus";
            nota.emit.cMun = "3550308";
            nota.emit.xMun = "São Paulo";
            nota.emit.UF = "SP";
            nota.emit.CEP = "30720360";
            nota.emit.cPais = "1058";
            nota.emit.xPais = "BRASIL";
            nota.emit.fone = "1331231231";
            nota.emit.IE = "117013148112";
            nota.emit.CRT = "1";

            nota.dest.CPF = "73696773204";
            nota.dest.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
            nota.dest.xLgr = "R. Logradouro Teste";
            nota.dest.nro = "123";
            nota.dest.xBairro = "Bairro da Luz";
            nota.dest.cMun = "3550308";
            nota.dest.xMun = "São Paulo";
            nota.dest.UF = "SP";
            nota.dest.CEP = "30720360";
            nota.dest.cPais = "1058";
            nota.dest.xPais = "BRASIL";
            nota.dest.fone = "1129148627";
            nota.dest.IE = "ISENTO";
            nota.dest.email = "teste@teste.com";

            WallegNfe.Model.Nota.DUP dup = new WallegNfe.Model.Nota.DUP();
            dup.nDup = "123";
            dup.dVenc = "2014-03-21";
            dup.vDup = "23.33";
            nota.cobr.addDup(dup);


            nota.transp.modFrete = "1";
            nota.transp.xNome = "Transportadora Ficticia";
            nota.transp.IE = "637322284114";
            nota.transp.xEnder = "Rua ficticia, 123";
            nota.transp.xMun = "São Lucas";
            nota.transp.UF = "RO";

            WallegNfe.Model.Nota.DET notaProduto = new WallegNfe.Model.Nota.DET();
            notaProduto.cProd = "123";
            notaProduto.cEAN = "7896090701049";
            notaProduto.xProd = "Produto de teste";
            notaProduto.NCM = "22071090";
            notaProduto.CFOP = "5401";
            notaProduto.uCom = "CX";
            notaProduto.qCom = "1.0000";
            notaProduto.vUnCom = "1.00000000";
            notaProduto.vProd = "1.00";
            notaProduto.cEANTrib = "7896090701049";
            notaProduto.uTrib = "CX";
            notaProduto.qTrib = "1.0000";
            notaProduto.vUnTrib = "1.00000000";
            notaProduto.indTot = "1";


            notaProduto.icms = WallegNfe.Model.Nota.Enum.ICMS.ICMS102_400 ;

            
            notaProduto.icms_orig = "0";
            notaProduto.icms_CSOSN = "102";
            

            /*
            notaProduto.ipi = WallegNfe.Model.Nota.Enum.IPI.IPI00_49_50_99;
            notaProduto.ipi_CST = "99";
            notaProduto.ipi_vIPI = "0";
            */

            notaProduto.pis = WallegNfe.Model.Nota.Enum.PIS.PIS01_02;
            notaProduto.pis_CST = "01";
            notaProduto.pis_vBC = "0";
            notaProduto.pis_pPIS = "0";
            notaProduto.pis_vPIS = "0";
            notaProduto.cofins = WallegNfe.Model.Nota.Enum.COFINS.CST01_02;
            notaProduto.cofins_CST = "01";
            notaProduto.cofins_vBC = "0";
            notaProduto.cofins_pCOFINS = "0";
            notaProduto.cofins_vCOFINS = "0";
            
            nota.AddDet(notaProduto);


            nota.total.vBC ="0.00";
            nota.total.vICMS = "0.00";
            nota.total.vBCST = "0.00";
            nota.total.vST = "0.00";
            nota.total.vProd = "1.00";
            nota.total.vFrete = "0.00";
            nota.total.vSeg = "0.00";
            nota.total.vDesc = "0.00";
            nota.total.vII = "0.00";
            nota.total.vIPI = "0.00";
            nota.total.vPIS = "0.00";
            nota.total.vCOFINS = "0.00";
            nota.total.vOutro = "0.00";
            nota.total.vNF = "1.00";
            nota.total.vTotTrib = "0.00";


            nota.SalvarNota("C:\\NFE\\teste-nota.xml");

            WallegNfe.Nfe nfe = new WallegNfe.Nfe(false, "C:\\certificado3.pfx");


            //Enviar uma nota
            WallegNfe.Operacao.Recepcao nfeRecepcao = new WallegNfe.Operacao.Recepcao(nfe);

            nfeRecepcao.AdicionarNota(nota);
            String numeroRecibo = nfeRecepcao.Enviar(1).Recibo;

            //Consultar nota
            WallegNfe.Operacao.RetRecepcao nfeRetRecepcao = new WallegNfe.Operacao.RetRecepcao(nfe);
            WallegNfe.Model.Retorno.RetRecepcao retRetorno = nfeRetRecepcao.Enviar(numeroRecibo);

            /*
            //Cancelar nota
            WallegNfe.Operacao.RecepcaoEvento nfeRecepcaoEvento = new WallegNfe.Operacao.RecepcaoEvento(nfe);
            */

            Console.ReadLine();
        }
    }
}
