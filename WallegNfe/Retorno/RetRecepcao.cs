using System;

namespace WallegNFe.Retorno
{
    public class RetRecepcao : IRetorno
    {
        public String NumeroNota { get; private set; }
        public String Protocolo { get; private set; }
        public String Status { get; private set; }
        public String Motivo { get; private set; }

        public RetRecepcao(String numeroNota, String protocolo, String status = "", String motivo = "")
        {
            this.NumeroNota = numeroNota;
            this.Protocolo = protocolo;
            this.Status = status;
            this.Motivo = Motivo;
        }
    }
}