using System;
using System.Security.Cryptography.X509Certificates;
using NFeEletronica.Certificado;
using NFeEletronica.Versao;

namespace NFeEletronica.Contexto
{
    public class NFeContexto : INFeContexto
    {
        public bool Producao { get; } = false;
        public BaseVersao Versao { get; }
        public X509Certificate2 Certificado { get; }
        
        public NFeContexto(bool producao, NFeVersao versao, IGerenciadorDeCertificado gerenciadorDeCertificado = null)
        {
            if (versao == NFeVersao.VERSAO_3_1_0)
            {
                Versao = new Versao3_1_0();
            }
            else
            {
                Versao = new Versao2_0_0();
            }

            //Abre uma janela para selecionar o certificado instalado no computador
            if (gerenciadorDeCertificado == null) gerenciadorDeCertificado = new GerenciadorDeCertificado();
            Certificado = gerenciadorDeCertificado.SelecionarPorWindows();

            if (Certificado == null)
                throw new Exception(
                    "Nenhum certificado encontrado, não será possível prosseguir com a Nota Fiscal Eletrônica.");
        }
    }
}
