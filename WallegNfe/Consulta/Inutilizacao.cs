using System;

namespace WallegNFe.Consulta
{
    public class Inutilizacao
    {
        public String Justificativa { get; private set; }
        public String CNPJ { get; private set; }
        public String UF { get; private set; }
        public String Ano { get; set; }
        public String Mod { get; set; }
        public String Serie { get; set; }
        public String NumeroNfeInicial { get; set; }
        public String NumeroNfeFinal { get; set; }

        public Inutilizacao(String justificativa, String cnpj, String uf)
        {
            this.Justificativa = justificativa;
            this.CNPJ = cnpj;
            this.UF = uf;
        }
    }
}