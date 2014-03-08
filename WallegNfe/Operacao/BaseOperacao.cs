using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography.X509Certificates;

namespace WallegNFe.Operacao
{
    public abstract class BaseOperacao
    {
        protected readonly X509Certificate2 Certificado;
        protected String ArquivoSchema = null;

        public BaseOperacao(WallegNFe.NfeContexto nfe)
        {
            this.Certificado = nfe.Certificado;
        }
    }
}
