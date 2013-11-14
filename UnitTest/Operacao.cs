using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//Trabalhar com o certificado
using System.Security.Cryptography.X509Certificates;

namespace UnitTest
{
    [TestClass]
    public class Operacao
    {
        [TestMethod]
        public void Recepcao()
        {
            //Seleciona um certificado
            WallegNfe.Certificado wallegCertificado = new WallegNfe.Certificado();
            X509Certificate2 certificado = wallegCertificado.SelecionarPorArquivo("C:\\certificado.pfx");

            //Envia uma nota
            WallegNfe.Operacao.Recepcao recepcao = new WallegNfe.Operacao.Recepcao(certificado);
            recepcao.AdicionarNota("C:\\NFE\\Xml\\teste.xml");

           // String retorno = recepcao.Enviar(1);
        }
    }
}
