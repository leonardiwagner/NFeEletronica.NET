using System;
using WallegNFe.ModeloNota;

namespace WallegNFe.ModeloNota
{
    public class DET
    {
        public DET()
        {
            //valores padrão
            indTot = "1";
        }

        public String nItem { get; set; }
        public String cProd { get; set; }
        public String cEAN { get; set; }
        public String xProd { get; set; }
        public String NCM { get; set; }
        public String CFOP { get; set; }
        public String uCom { get; set; }
        public String qCom { get; set; }
        public String vUnCom { get; set; }
        public String vProd { get; set; }
        public String cEANTrib { get; set; }
        public String uTrib { get; set; }
        public String qTrib { get; set; }
        public String vUnTrib { get; set; }

        public String vFrete { get; set; }
        public String vDesc { get; set; }

        public String indTot { get; set; }

        public String vTotTrib { get; set; }

        //ICMS
        public ICMS icms { get; set; }
        public String icms_orig { get; set; }
        public String icms_CST { get; set; }
        public String icms_modBC { get; set; }
        public String icms_vBC { get; set; }
        public String icms_pICMS { get; set; }
        public String icms_vICMS { get; set; }
        public String icms_modBCST { get; set; }
        public String icms_pMVAST { get; set; }
        public String icms_pRedBCST { get; set; }
        public String icms_vBCST { get; set; }
        public String icms_pICMSST { get; set; }
        public String icms_vICMSST { get; set; }
        public String icms_pRedBC { get; set; }
        public String icms_CSOSN { get; set; }
        public String icms_pCredSN { get; set; }
        public String icms_vCredICMSSN { get; set; }
        public String icms_vBCSTRet { get; set; }
        public String icms_vICMSSTRet { get; set; }

        //CST
        public IPI ipi { get; set; }
        public String ipi_cIEnq { get; set; }
        public String ipi_CST { get; set; }
        public String ipi_vBC { get; set; }
        public String ipi_pIPI { get; set; }
        public String ipi_qUnid { get; set; }
        public String ipi_vUnid { get; set; }
        public String ipi_vIPI { get; set; }

        //PIS
        public PIS pis { get; set; }
        public String pis_CST { get; set; }
        public String pis_vBC { get; set; }
        public String pis_pPIS { get; set; }
        public String pis_vPIS { get; set; }
        public String pis_qBCProd { get; set; }
        public String pis_vAliqProd { get; set; }

        //COFINS
        public COFINS cofins { get; set; }
        public String cofins_CST { get; set; }
        public String cofins_vBC { get; set; }
        public String cofins_pCOFINS { get; set; }
        public String cofins_vCOFINS { get; set; }
        public String cofins_qBCProd { get; set; }
        public String cofins_vAliqProd { get; set; }
    }
}