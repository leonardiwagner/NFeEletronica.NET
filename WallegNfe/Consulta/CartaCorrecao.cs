using System;

namespace WallegNFe.Consulta
{
    public class CartaCorrecao
    {
        public String NumeroLote { get; private set; }
        public String NotaChaveAcesso { get; private set; }
        public String Correcao { get; private set; }
        public String CNPJ { get; private set; }
        public String CodigoUF { get; private set; }
        public DateTime DataEvento { get; set; }

        public CartaCorrecao(String numeroLote, String notaChaveAcesso, String correcao, String cnpj, String codigoUF)
        {
            this.NumeroLote = numeroLote;
            this.NotaChaveAcesso = notaChaveAcesso;
            this.Correcao = correcao;
            this.CNPJ = cnpj;
            this.CodigoUF = codigoUF;
        }
    }
}