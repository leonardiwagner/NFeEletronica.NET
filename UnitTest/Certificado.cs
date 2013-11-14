using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//Trabalhar com o certificado
using System.Security.Cryptography.X509Certificates;

namespace UnitTest
{
    [TestClass]
    public class Certificado
    {
        private readonly String arquivoCertificadoSalva = "C:\\certificado.pfx";

        [TestMethod]
        public void CertificadoSalvarLer()
        {
            WallegNfe.Certificado wallegCertificado = new WallegNfe.Certificado();
            X509Certificate2 certificado = wallegCertificado.SelecionarPorOtmismo();
            wallegCertificado.SalvarCertificado(certificado, arquivoCertificadoSalva);

            X509Certificate2 certificadoSalvo = wallegCertificado.SelecionarPorArquivo(arquivoCertificadoSalva);

            Assert.AreNotEqual(certificadoSalvo, null);
        }
    }
}
