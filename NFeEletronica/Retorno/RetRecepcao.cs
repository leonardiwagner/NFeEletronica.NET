using System;

namespace NFeEletronica.Retorno
{
    public class RetRecepcao : IRetorno
    {
        public RetRecepcao(String numeroNota, String protocolo, String status = "", String motivo = "")
        {
            NumeroNota = numeroNota;
            Protocolo = protocolo;
            Status = status;
            Motivo = Motivo;
        }

        public String NumeroNota { get; private set; }
        public String Protocolo { get; private set; }
        public String Status { get; }
        public String Motivo { get; }
    }
}