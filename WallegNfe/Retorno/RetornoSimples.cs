using System;

namespace WallegNFe.Retorno
{
    public class RetornoSimples : IRetorno
    {
        public String Status { get; private set; }
        public String Motivo { get; private set; }

        public RetornoSimples(String status, String motivo)
        {
            this.Status = status;
            this.Motivo = motivo;
        }
    }
}