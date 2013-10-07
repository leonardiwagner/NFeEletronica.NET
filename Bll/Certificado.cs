using System;
using System.Collections.Generic;
using System.Text;

//Trabalhar com o certificado
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;

namespace Bll
{
    public class Certificado
    {
        /// <summary>
        /// Abre uma janela para selecionar um certificado
        /// </summary>
        /// <returns>Certificado para assinatura</returns>
        public X509Certificate2 GetByWindows()
        {
            X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(this.GetCollection(), "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo", X509SelectionFlag.SingleSelection);

            if (scollection == null || scollection.Count == 0)
                return null;
            else
                return scollection[0];
        }

        /// <summary>
        /// Retorna uma lista com todos os certificados (válidos) disponiveis na maquina
        /// </summary>
        /// <returns>Certificado para assinatura</returns>
        public X509Certificate2Collection GetCollection()
        {
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
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

    }
}
