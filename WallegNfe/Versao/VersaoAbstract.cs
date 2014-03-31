using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallegNFe.Versao
{
    public abstract class VersaoAbstract
    {
        public abstract NFeVersao Versao { get; }
        public abstract String VersaoString { get; }
        public abstract String PastaXML { get; }

        public static bool operator ==(VersaoAbstract versaoAbstract, NFeVersao nfeVersao)
        {
            return (versaoAbstract.Versao == nfeVersao);
        }

        public static bool operator !=(VersaoAbstract versaoAbstract, NFeVersao nfeVersao)
        {
            return (versaoAbstract.Versao != nfeVersao);
        }

        public bool Equals(NFeVersao obj)
        {
            return (obj == this.Versao);
        }
    }
}
