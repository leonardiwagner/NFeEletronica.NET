using System;
using System.Security.Cryptography.X509Certificates;
using WallegNFe.Versao;

namespace WallegNFe
{
    public interface INFeContexto
    {
        bool Producao { get; }
        BaseVersao Versao { get; }
        X509Certificate2 Certificado { get; }
    }

    public class NFeContexto : INFeContexto
    {
        private readonly bool producao = false;
        private readonly BaseVersao versao;
        private X509Certificate2 certificado;

        public NFeContexto(bool producao, NFeVersao versao, IGerenciadorDeCertificado gerenciadorDeCertificado = null)
        {
            if (versao == NFeVersao.VERSAO_3_1_0)
            {
                this.versao = new Versao3_1_0();
            }
            else
            {
                this.versao = new Versao2_0_0();
            }

            //Abre uma janela para selecionar o certificado instalado no computador
            if (gerenciadorDeCertificado == null) gerenciadorDeCertificado = new GerenciadorDeCertificado();
            this.certificado = gerenciadorDeCertificado.SelecionarPorWindows();

            if (this.certificado == null)
                throw new Exception("Nenhum certificado encontrado, não será possível prosseguir com a Nota Fiscal Eletrônica.");
        }

        public bool Producao
        {
            get { return producao; }
        }

        public BaseVersao Versao
        {
            get { return versao; }
        }

        public X509Certificate2 Certificado
        {
            get { return certificado; }
        }
    }
}