using System;

namespace NFeEletronica.Versao
{
    public abstract class BaseVersao
    {
        public abstract NFeVersao Versao { get; }
        public abstract String VersaoString { get; }
        public abstract String PastaXml { get; }

        public static bool operator ==(BaseVersao versaoAbstract, NFeVersao nfeVersao)
        {
            return (versaoAbstract.Versao == nfeVersao);
        }

        public static bool operator !=(BaseVersao versaoAbstract, NFeVersao nfeVersao)
        {
            return (versaoAbstract.Versao != nfeVersao);
        }

        public bool Equals(NFeVersao obj)
        {
            return (obj == Versao);
        }
    }
}