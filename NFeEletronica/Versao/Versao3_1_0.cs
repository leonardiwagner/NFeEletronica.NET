namespace NFeEletronica.Versao
{
    public class Versao3_1_0 : BaseVersao
    {
        public override string VersaoString
        {
            get { return "3.10"; }
        }

        public override string PastaXml
        {
            get { return "3.1.0"; }
        }

        public override NFeVersao Versao
        {
            get { return NFeVersao.VERSAO_3_1_0; }
        }
    }
}