using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace NFeEletronica.Certificado
{
    public class GerenciadorDeCertificado : IGerenciadorDeCertificado
    {
        /// <summary>
        ///     Abre uma janela do windows para selecionar um certificado
        /// </summary>
        /// <returns>Certificado para assinatura</returns>
        public X509Certificate2 SelecionarPorWindows()
        {
            var scollection = X509Certificate2UI.SelectFromCollection(ListaCertificados(),
                "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo",
                X509SelectionFlag.SingleSelection);
            if (scollection != null && scollection.Count == 0) return null;
            return scollection[0];
        }

        /// <summary>
        ///     Seleciona o certificado que provavelmente é do Sefaz. ATENÇÃO: esse método não é totalmente seguro.
        /// </summary>
        /// <returns>Certificado para assinatura</returns>
        public X509Certificate2 SelecionarPorOtmismo()
        {
            var colecao = ListaCertificados();
            foreach (var certificado in colecao)
            {
                if (certificado.IssuerName.Name.ToLower().Contains("Secretaria da Receita Federal do Brasil".ToLower()))
                {
                    return certificado;
                }
            }

            return null;
        }

        /// <summary>
        ///     Seleciona um certificado digital apartir de um arquivo
        /// </summary>
        /// <param name="arquivoCaminho"></param>
        /// <returns>Certificado para assinatura</returns>
        public X509Certificate2 SelecionarPorArquivo(String arquivoCaminho)
        {
            return new X509Certificate2(arquivoCaminho, "NFeEletronica");
        }

        /// <summary>
        ///     Retorna uma lista com todos os certificados (válidos) disponiveis na maquina
        /// </summary>
        /// <returns>Certificado para assinatura</returns>
        public X509Certificate2Collection ListaCertificados()
        {
            var store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadWrite);
            var collection = store.Certificates;
            X509Certificate2Collection collection1 = null;
            X509Certificate2Collection collection2 = null;

            if (collection != null)
            {
                collection1 = collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                if (collection1 != null)
                {
                    collection2 = collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);
                    return collection2;
                }
                return null;
            }
            return null;
        }

        /// <summary>
        ///     Salva um certificado digital como um arquivo
        /// </summary>
        /// <param name="certificado"></param>
        /// <param name="arquivoCaminho"></param>
        public void SalvarCertificado(X509Certificate2 certificado, String arquivoCaminho)
        {
            var certData = certificado.Export(X509ContentType.Cert, "NFeEletronica");
            File.WriteAllBytes(arquivoCaminho, certData);
        }
    }
}