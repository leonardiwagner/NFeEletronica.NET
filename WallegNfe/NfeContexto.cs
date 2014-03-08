using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Trabalhar com o certificado
using System.Security.Cryptography.X509Certificates;

namespace WallegNFe
{
    public class NfeContexto
    {
        private X509Certificate2 _Certificado = null;
        private readonly bool _Producao = false;

        public NfeContexto(bool producao)
        {
            this.Inicializar();
        }

        public bool Producao
        {
            get
            {
                return this._Producao;
            }
        }

        public X509Certificate2 Certificado
        {
            get
            {
                return this._Certificado;
            }
        }

        private void Inicializar()
        {
            this._Certificado = this.carregarCertificado();
        }

        /// <summary>
        /// Carrega um certificado para trabalhar com a NFe
        /// </summary>
        /// <returns></returns>
        private X509Certificate2 carregarCertificado()
        {
            Bll.Certificado bllCertificado = new Bll.Certificado();
            X509Certificate2 certificado = null;

  
            //Abre uma janela para selecionar o certificado instalado no computador
            certificado = bllCertificado.SelecionarPorWindows();

            if (certificado != null)
            {
                return certificado;
            }
            else
            {
                throw new Exception("Nenhum certificado encontrado, não será possível prosseguir com a Nota Fiscal Eletrônica.");
            }
        }

    }
}
