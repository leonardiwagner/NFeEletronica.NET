using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallegNfe.Model.Nota
{
    public class DET
    {
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

        public String indTot { get; set; }

        //ICMS
        public Enum.ICMS icms {get;set;}
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

        //CST
        public Enum.CST cst { get; set; }
        public String cst_vBCSTRet { get; set; }         // N26  - Valor da BC do ICMS ST cobrado anteriormente por ST
        public String cst_vICMSSTRet { get; set; }       // N27  - Valor do ICMS ST cobrado anteriormente por ST
        public String cst_pCredSN { get; set; }          // N29        - Alíquota aplicável de cálculo do crédito (Simples Nacional).
        public String cst_vCredICMSSN { get; set; }

        
    }
}
