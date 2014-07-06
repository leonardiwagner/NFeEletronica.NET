using System;

namespace WallegNFe.Retorno
{
    public interface IRetorno
    {
        String Status { get; set; }
        String Motivo { get; set; }
    }
}