using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Trabalhar com o certificado
using System.Security.Cryptography.X509Certificates;

namespace WallegNFe
{
    public class NFeContexto
    {
        private X509Certificate2 _Certificado = null;
        private readonly bool _Producao = false;
        private readonly String _VersaoString;
        private readonly NFeVersao _Versao;

        public NFeContexto(bool producao, NFeVersao versao)
        {
            this._Versao = versao;
            if (this._Versao == NFeVersao.VERSAO_3_1_0)
            {
                this._VersaoString = "3.10";
            }
            else
            {
                this._VersaoString = "2.00";
            }

            this.Inicializar();
        }

        public bool Producao
        {
            get
            {
                return this._Producao;
            }
        }

        public NFeVersao Versao
        {
            get
            {
                return this._Versao;
            }
        }

        public String VersaoString
        {
            get
            {
                return this._VersaoString;
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
