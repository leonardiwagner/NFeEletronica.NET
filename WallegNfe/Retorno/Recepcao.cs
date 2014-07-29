using System;

namespace WallegNFe.Retorno
{
    public class Recepcao : IRetorno
    {
        public String Recibo { get; private set; }
        public String Status { get; private set; }
        public String Motivo { get; private set; }

        public Recepcao(String recibo, String status, String motivo)
        {
            this.Recibo = recibo;
            this.Status = status;
            this.Motivo = motivo;
        }
    }
}