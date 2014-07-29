using System;

namespace WallegNFe.Retorno
{
    public interface IRetorno
    {
        String Status { get; }
        String Motivo { get; }
    }
}