using System;

namespace WallegNFe.Model.Nota
{
    public class IDE
    {
        public IDE()
        {
            cUF = "35";
            mod = "55";
            serie = "1";
            tpImp = "1";
            tpEmis = "1";
            cDV = "0";
            tpAmb = "2";
            finNFe = "1";
            procEmi = "3";
            verProc = "2.2.19";
        }

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

        //valores padrão
    }
}