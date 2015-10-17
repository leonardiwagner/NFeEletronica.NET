using System;

namespace NFeEletronica.Retorno
{
    public class RetornoSimples : IRetorno
    {
        public RetornoSimples(String status, String motivo)
        {
            Status = status;
            Motivo = motivo;
        }

        public String Status { get; }
        public String Motivo { get; }
    }
}