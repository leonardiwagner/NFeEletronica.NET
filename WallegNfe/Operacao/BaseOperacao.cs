using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography.X509Certificates;

namespace WallegNfe.Operacao
{
    public abstract class BaseOperacao
    {
        protected X509Certificate2 Certificado = null;
        protected String ArquivoSchema = null;

        public BaseOperacao(WallegNfe.Nfe nfe)
        {
            this.Certificado = nfe.GetCertificado();
        }
    }
}
