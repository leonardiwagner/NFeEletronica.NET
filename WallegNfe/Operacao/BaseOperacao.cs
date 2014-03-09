using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography.X509Certificates;

namespace WallegNFe.Operacao
{
    public abstract class BaseOperacao
    {
        protected WallegNFe.NFeContexto NFeContexto;

        public BaseOperacao(WallegNFe.NFeContexto nfeContexto)
        {
            this.NFeContexto = nfeContexto;
        }
    }
}
