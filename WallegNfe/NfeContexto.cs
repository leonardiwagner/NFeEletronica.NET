//Trabalhar com o certificado
using System;
using System.Security.Cryptography.X509Certificates;
using WallegNFe.Bll;
using WallegNFe.Versao;

namespace WallegNFe
{
    public class NFeContexto
    {
        private readonly bool _Producao = false;
        private readonly VersaoAbstract _Versao;
        private X509Certificate2 _Certificado;

        public NFeContexto(bool producao, NFeVersao versao)
        {
            if (versao == NFeVersao.VERSAO_3_1_0)
            {
                _Versao = new Versao3_1_0();
            }
            else
            {
                _Versao = new Versao2_0_0();
            }

            Inicializar();
        }

        public bool Producao
        {
            get { return _Producao; }
        }

        public VersaoAbstract Versao
        {
            get { return _Versao; }
        }

        public X509Certificate2 Certificado
        {
            get { return _Certificado; }
        }

        private void Inicializar()
        {
            _Certificado = carregarCertificado();
        }

        /// <summary>
        ///     Carrega um certificado para trabalhar com a NFe
        /// </summary>
        /// <returns></returns>
        private X509Certificate2 carregarCertificado()
        {
            var bllCertificado = new Certificado();
            X509Certificate2 certificado = null;


            //Abre uma janela para selecionar o certificado instalado no computador
            certificado = bllCertificado.SelecionarPorWindows();

            if (certificado != null)
            {
                return certificado;
            }
            throw new Exception(
                "Nenhum certificado encontrado, não será possível prosseguir com a Nota Fiscal Eletrônica.");
        }
    }
}