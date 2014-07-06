using System;

namespace WallegNFe.Retorno
{
    public class RetornoSimples : IRetorno
    {
        public String Status { get; set; }
        public String Motivo { get; set; }
    }
}