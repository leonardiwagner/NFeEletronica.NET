using System;

namespace WallegNFe.Consulta
{
    public class Cancelamento
    {
        public String NumeroLote { get; private set; }
        public String NotaChaveAcesso { get; private set; }
        public String Justificativa { get; private set; }
        public String Protocolo { get; private set; }
        public String CNPJ { get; private set; }
        public DateTime DataEvento { get; set; }

        public Cancelamento(String numeroLote, String notaChaveAcesso, String justificativa, String protocolo, String cnpj)
        {
            this.NumeroLote = numeroLote;
            this.NotaChaveAcesso = notaChaveAcesso;
            this.Justificativa = justificativa;
            this.Protocolo = protocolo;
            this.CNPJ = cnpj;
        }
    }
}