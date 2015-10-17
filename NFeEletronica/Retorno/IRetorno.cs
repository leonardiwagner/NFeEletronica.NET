using System;

namespace NFeEletronica.Retorno
{
    public interface IRetorno
    {
        String Status { get; }
        String Motivo { get; }
    }
}