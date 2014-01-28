using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Trabalhar com o certificado
using System.Security.Cryptography.X509Certificates;

namespace WallegNfe
{
    public class Nfe
    {
        private String ArquivoCertificado = "";
        private X509Certificate2 Certificado = null;
        private Boolean Producao = false;

        public Nfe(bool producao)
        {
            this.Inicializa();
        }

        public Nfe(bool producao, String caminhoArquivoCertificado)
        {
            this.ArquivoCertificado = caminhoArquivoCertificado;

            this.Inicializa();
        }

        private void Inicializa()
        {
            this.Certificado = this.carregarCertificado();

        }

        public X509Certificate2 GetCertificado()
        {
            return this.Certificado;
        }

        public bool GetProducao()
        {
            return this.Producao;
        }
        /// <summary>
        /// Carrega um certificado para trabalhar com a NFe
        /// </summary>
        /// <returns></returns>
        private X509Certificate2 carregarCertificado()
        {
            Bll.Certificado bllCertificado = new Bll.Certificado();
            X509Certificate2 certificado = null;

            //Operação se é para carregar um certificado que já existe
            if (1==2 && !String.IsNullOrEmpty(this.ArquivoCertificado))
            {
                //verifica se o certificado já existe e funciona
                if (Bll.Arquivo.ExisteArquivo(this.ArquivoCertificado))
                {
                    try
                    {
                        certificado = bllCertificado.SelecionarPorArquivo(this.ArquivoCertificado);
                    }
                    catch { }

                    //retorna o certificado carregado
                    if (certificado != null)
                    {
                        return certificado;
                    }
                }
            }

            //Se a operação acima não retornar nada é porque o certificado precisa ser selecionado
            certificado = bllCertificado.SelecionarPorWindows();

            //certificado encontrado
            if (certificado != null)
            {
                //verifica se é para salvar o certificado em um arquivo
                if (!Bll.Arquivo.ExisteArquivo(this.ArquivoCertificado))
                {
                    bllCertificado.SalvarCertificado(certificado, this.ArquivoCertificado);
                }

                //retorna o certificado encontrado
                return certificado;
            }
            else
            {
                //não achou nenhum certificado, não é possivel prosseguir
                throw new Exception("Nenhum certificado encontrado, não será possível prosseguir com a Nota Fiscal Eletrônica.");
            }
            
        }

    }
}
