using System;

namespace NFeEletronica.Consulta
{
    public class Inutilizacao
    {
        public Inutilizacao(String justificativa, String cnpj, String uf)
        {
            Justificativa = justificativa;
            CNPJ = cnpj;
            UF = uf;
        }

        public String Justificativa { get; private set; }
        public String CNPJ { get; private set; }
        public String UF { get; private set; }
        public String Ano { get; set; }
        public String Mod { get; set; }
        public String Serie { get; set; }
        public String NumeroNfeInicial { get; set; }
        public String NumeroNfeFinal { get; set; }
    }
}