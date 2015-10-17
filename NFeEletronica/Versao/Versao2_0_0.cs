namespace NFeEletronica.Versao
{
    public class Versao2_0_0 : BaseVersao
    {
        public override string VersaoString
        {
            get { return "2.00"; }
        }

        public override string PastaXml
        {
            get { return "2.0.0"; }
        }

        public override NFeVersao Versao
        {
            get { return NFeVersao.VERSAO_2_0_0; }
        }
    }
}