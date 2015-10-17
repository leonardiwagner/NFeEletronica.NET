using System;

namespace NFeEletronica.Retorno
{
    public class Recepcao : IRetorno
    {
        public Recepcao(String recibo, String status, String motivo)
        {
            Recibo = recibo;
            Status = status;
            Motivo = motivo;
        }

        public String Recibo { get; private set; }
        public String Status { get; }
        public String Motivo { get; }
    }
}