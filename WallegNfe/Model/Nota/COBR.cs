using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallegNFe.Model.Nota
{
    public class COBR
    {
        public String nFat { get; set; }
        public String vOrig { get; set; }
        public String vLiq { get; set; }

        public List<DUP> dup { get; set; }

        public COBR()
        {
            this.dup = new List<DUP>();
        }

        public void addDup(DUP dup)
        {
            this.dup.Add(dup);
        }

    }

    public class DUP
    {
        public String nDup { get; set; }
        public String dVenc { get; set; }
        public String vDup { get; set; }
    }
}
