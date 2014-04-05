using System;

namespace WallegNFe.Retorno
{
    public class RetRecepcao : IRetorno
    {
        public String Status { get; set; }
        public String Motivo { get; set; }
        public String NumeroNota { get; set; }
        public String Protocolo { get; set; }
    }
}