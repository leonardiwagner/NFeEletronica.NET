using System;
using System.Collections.Generic;

namespace NFeEletronica.NotaFiscal
{
    public class COBR
    {
        public COBR()
        {
            dup = new List<DUP>();
        }

        public String nFat { get; set; }
        public String vOrig { get; set; }
        public String vLiq { get; set; }
        public List<DUP> dup { get; set; }
    }

    public class DUP
    {
        public String nDup { get; set; }
        public String dVenc { get; set; }
        public String vDup { get; set; }
    }
}