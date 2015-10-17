using System;

namespace NFeEletronica.Consulta
{
    public class CartaCorrecao
    {
        public CartaCorrecao(String numeroLote, String notaChaveAcesso, String correcao, String cnpj, String codigoUF)
        {
            NumeroLote = numeroLote;
            NotaChaveAcesso = notaChaveAcesso;
            Correcao = correcao;
            CNPJ = cnpj;
            CodigoUF = codigoUF;
        }

        public String NumeroLote { get; private set; }
        public String NotaChaveAcesso { get; private set; }
        public String Correcao { get; private set; }
        public String CNPJ { get; private set; }
        public String CodigoUF { get; private set; }
        public DateTime DataEvento { get; set; }
    }
}