using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Trabalhar com o certificado
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;

//Trabalhar com arquivos
using System.IO;

namespace WallegNfe
{
    public class Certificado
    {
        /// <summary>
        /// Abre uma janela para selecionar um certificado
        /// </summary>
        /// <returns>Certificado para assinatura</returns>
        public X509Certificate2 SelecionarPorWindows()
        {
            X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(this.SelecionarColecao(), "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo", X509SelectionFlag.SingleSelection);

            if (scollection == null || scollection.Count == 0)
                return null;
            else
                return scollection[0];
        }

        /// <summary>
        /// Retorna uma lista com todos os certificados (válidos) disponiveis na maquina
        /// </summary>
        /// <returns>Certificado para assinatura</returns>
        public X509Certificate2Collection SelecionarColecao()
        {
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadWrite);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = null;
            X509Certificate2Collection collection2 = null;

            if (collection != null)
            {
                collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                if (collection1 != null)
                {
                    collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);
                    return collection2;
                }
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Seleciona o certificado que provavelmente é do Sefaz. ATENÇÃO: esse método não é totalmente seguro.
        /// </summary>
        /// <returns></returns>
        public X509Certificate2 SelecionarPorOtmismo()
        {
            X509Certificate2Collection colecao = this.SelecionarColecao();
            foreach (X509Certificate2 certificado in colecao)
            {
                if (certificado.IssuerName.Name.ToLower().Contains("Secretaria da Receita Federal do Brasil".ToLower()))
                {
                    return certificado;
                }
            }

            return null;
        }

        /// <summary>
        /// Salva um certificado digital como um arquivo
        /// </summary>
        /// <param name="certificado"></param>
        /// <param name="arquivoCaminho"></param>
        /// <returns></returns>
        public String SalvarCertificado(X509Certificate2 certificado, String arquivoCaminho)
        {
            byte[] certData = certificado.Export(X509ContentType.Cert, "WallegNfe");
            File.WriteAllBytes(arquivoCaminho, certData);

            return arquivoCaminho;
        }

        /// <summary>
        /// Seleciona um certificado digital apartir de um arquivo
        /// </summary>
        /// <param name="arquivoCaminho"></param>
        /// <returns></returns>
        public X509Certificate2 SelecionarPorArquivo(String arquivoCaminho)
        {
            return new X509Certificate2(arquivoCaminho, "WallegNfe");
        }

    }
}
