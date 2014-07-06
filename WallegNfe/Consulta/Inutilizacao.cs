using System;

namespace WallegNFe.Consulta
{
    public class Inutilizacao
    {
        public String UF { get; set; }
        public String Ano { get; set; }
        public String CNPJ { get; set; }
        public String Mod { get; set; }
        public String Serie { get; set; }
        public String NumeroNfeInicial { get; set; }
        public String NumeroNfeFinal { get; set; }
        public String Justificativa { get; set; }
    }
}