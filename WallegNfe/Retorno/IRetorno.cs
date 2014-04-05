using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallegNFe.Retorno
{
    public interface IRetorno
    {
        String Status { get; set; }
        String Motivo { get; set; }
    }
}
