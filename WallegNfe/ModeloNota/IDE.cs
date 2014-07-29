using System;

namespace WallegNFe.ModeloNota
{
    public class IDE
    {
        public String cUF { get; set; }
        public String cNF { get; set; }
        public String natOp { get; set; }
        public String indPag { get; set; }
        public String mod { get; set; }
        public String serie { get; set; }
        public String nNF { get; set; }
        public String dEmi { get; set; }
        public String dhEmi { get; set; }
        public String dhSaiEnt { get; set; }
        public String tpNF { get; set; }
        public String idDest { get; set; }
        public String cMunFG { get; set; }
        public String tpImp { get; set; }
        public String tpEmis { get; set; }
        public String cDV { get; set; }
        public String tpAmb { get; set; }
        public String finNFe { get; set; }
        public String indFinal { get; set; }
        public String indPres { get; set; }
        public String procEmi { get; set; }
        public String verProc { get; set; }

        public IDE()
        {
            //valores padrão
            this.cUF = "35";
            this.mod = "55";
            this.serie = "1";
            this.tpImp = "1";
            this.tpEmis = "1";
            this.cDV = "0";
            this.tpAmb = "2";
            this.finNFe = "1";
            this.procEmi = "3";
            this.verProc = "2.2.19";
        }
    }
}