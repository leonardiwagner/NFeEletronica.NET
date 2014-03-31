namespace WallegNFe.Operacao
{
    public abstract class BaseOperacao
    {
        protected NFeContexto NFeContexto;

        public BaseOperacao(NFeContexto nfeContexto)
        {
            NFeContexto = nfeContexto;
        }
    }
}