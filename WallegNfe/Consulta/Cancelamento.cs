using System;

namespace WallegNFe.Consulta
{
    public class Cancelamento
    {
        public String NumeroLote { get; set; }
        public String CNPJ { get; set; }
        public String NotaChaveAcesso { get; set; }
        public String Protocolo { get; set; }
        public String Justificativa { get; set; }
        public DateTime DataEvento { get; set; }
    }
}