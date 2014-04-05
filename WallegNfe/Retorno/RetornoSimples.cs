using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallegNFe.Retorno
{
    public class RetornoSimples : IRetorno
    {
        public String Status { get; set; }
        public String Motivo { get; set; }
    }
}
