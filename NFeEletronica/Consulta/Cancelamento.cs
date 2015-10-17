using System;

namespace NFeEletronica.Consulta
{
    public class Cancelamento
    {
        public Cancelamento(String numeroLote, String notaChaveAcesso, String justificativa, String protocolo,
            String cnpj)
        {
            NumeroLote = numeroLote;
            NotaChaveAcesso = notaChaveAcesso;
            Justificativa = justificativa;
            Protocolo = protocolo;
            CNPJ = cnpj;
        }

        public String NumeroLote { get; private set; }
        public String NotaChaveAcesso { get; private set; }
        public String Justificativa { get; private set; }
        public String Protocolo { get; private set; }
        public String CNPJ { get; private set; }
        public DateTime DataEvento { get; set; }
    }
}