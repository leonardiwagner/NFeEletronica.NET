using System;

namespace Exemplo
{
    public class EnviarNota
    {
        public void Enviar()
        {
            //Cria o objeto de contexto, e seleciona o certificado no computador
            var nfeContexto = new WallegNFe.NFeContexto(false, WallegNFe.Versao.NFeVersao.VERSAO_2_0_0);

            //Cria a nota com o objeto "Nota"
            var nota = new WallegNFe.Nota(nfeContexto);

            nota.ide.cUF = "35";

            //cNF e cDV é gerado automaticamente, mas você pode inserir manualmente
            //nota.ide.cNF = "123";
            //nota.ide.cDV = "1";

            nota.ide.natOp = "Descrição da natureza";
            nota.ide.indPag = "0"; //'0 - a vista, 1 - a prazo, 2 - outros
            nota.ide.mod = "55";
            nota.ide.serie = "1";
            nota.ide.nNF = new Random().Next(1111,3333).ToString();
            nota.ide.dEmi = "2014-03-29";
            //dSaiEnt e hSaiEnt são opcionais
            nota.ide.tpNF = "1";
            nota.ide.cMunFG = "3550308";
            //NFref opcional
            nota.ide.tpImp = "1";
            nota.ide.tpEmis = "1";
            nota.ide.cDV = "0";
            nota.ide.idDest = "1";
            nota.ide.indFinal = "1";
            nota.ide.indPres = "0";
            //Não é necessário passar o tipo de ambiente, pois já pega automaticamente do contexto
            //nota.ide.tpAmb = "2";

            nota.ide.finNFe = "1";
            nota.ide.procEmi = "3";

            nota.emit.CNPJ = "07293766000150"; //ou emit.CNPJ
            nota.emit.xNome = "Carlos Montoya";
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
            nota.dest.xNome = "Renan Do Vidigal";
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
            nota.dest.indIEDest = "9";
            nota.dest.email = "teste@teste.com";

            var dup = new WallegNFe.ModeloNota.DUP();
            dup.nDup = "123";
            dup.dVenc = "2014-03-21";
            dup.vDup = "23.33";
            nota.cobr.dup.Add(dup);

            nota.transp.modFrete = "1";
            nota.transp.xNome = "Transportadora Ficticia";
            nota.transp.IE = "637322284114";
            nota.transp.xEnder = "Rua ficticia, 123";
            nota.transp.xMun = "São Lucas";
            nota.transp.UF = "RO";

            //início de um produto
            var notaProduto = new WallegNFe.ModeloNota.DET();
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

            notaProduto.icms = WallegNFe.ModeloNota.ICMS.ICMS102_400;
            notaProduto.icms_orig = "0";
            notaProduto.icms_CSOSN = "102";

            notaProduto.ipi = WallegNFe.ModeloNota.IPI.IPI00_49_50_99;
            notaProduto.ipi_CST = "99";
            notaProduto.ipi_vIPI = "0";

            notaProduto.pis = WallegNFe.ModeloNota.PIS.PIS01_02;
            notaProduto.pis_CST = "01";
            notaProduto.pis_vBC = "0";
            notaProduto.pis_pPIS = "0";
            notaProduto.pis_vPIS = "0";

            notaProduto.cofins = WallegNFe.ModeloNota.COFINS.CST01_02;
            notaProduto.cofins_CST = "01";
            notaProduto.cofins_vBC = "0";
            notaProduto.cofins_pCOFINS = "0";
            notaProduto.cofins_vCOFINS = "0";

            nota.AddDet(notaProduto);
            //fim de um produto

            nota.total.vBC = "0.00";
            nota.total.vICMS = "0.00";
            nota.total.vICMSDeson = "0.00";
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

            nota.SalvarNota(@"C:\NFE\teste-nota.xml");

            //Enviar a nota
            String codigoUF = nota.ide.cUF;

            int numeroDoLote = 1; //Para cada nota enviada, esse número deve ser único.
            //Pode usar o número da nota também: String numeroDoLote = Int32.Parse(nota.ide.cNF);

            var nfeRecepcao = new WallegNFe.Operacao.Recepcao(nfeContexto);
            nfeRecepcao.AdicionarNota(nota);

            WallegNFe.Retorno.Recepcao retornoRecepcao = nfeRecepcao.Enviar(numeroDoLote, codigoUF);

            //Consultar nota
            var nfeRetRecepcao = new WallegNFe.Operacao.RetRecepcao(nfeContexto);
            WallegNFe.Retorno.RetRecepcao retRetorno = nfeRetRecepcao.Enviar(retornoRecepcao.Recibo, codigoUF);

            //Inutilizar
            var inutilizacao = new WallegNFe.Consulta.Inutilizacao();
            inutilizacao.Ano = "14";
            inutilizacao.CNPJ = nota.emit.CNPJ;
            inutilizacao.Justificativa = "Cancelando por testedddddddddd fdf df";
            inutilizacao.Mod = nota.ide.mod;
            inutilizacao.Serie = nota.ide.serie;
            inutilizacao.UF = "35";
            inutilizacao.NumeroNfeInicial = nota.ide.nNF;
            inutilizacao.NumeroNfeFinal = nota.ide.nNF;

            var nfeInutilizacao = new WallegNFe.Operacao.Inutilizacao(nfeContexto);
            nfeInutilizacao.NfeInutilizacaoNF2(inutilizacao);

            //Cancelar a nota
            var nfeCancelamento = new WallegNFe.Consulta.Cancelamento();
            nfeCancelamento.CNPJ = nota.emit.CNPJ;
            nfeCancelamento.Justificativa = "Cancelando por testedddddddddd fdf df";//minimo 15 char
            nfeCancelamento.NumeroLote = numeroDoLote.ToString();
            nfeCancelamento.NotaChaveAcesso = nota.NotaId;
            nfeCancelamento.Protocolo = retRetorno.Protocolo;

            var nfeEvento = new WallegNFe.Operacao.RecepcaoEvento(nfeContexto);
            nfeEvento.Cancelar(nfeCancelamento, @"C:\NFE\cancelamento.xml");
        }
    }
}