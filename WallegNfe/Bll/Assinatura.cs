using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace WallegNFe.Bll
{
    public class Assinatura
    {

        /// <summary>
        ///     Assina um arquivo Xml
        /// </summary>
        /// <param name="arquivoNome"></param>
        /// <param name="operacao"></param>
        /// <param name="x509Cert"></param>
        public String AssinarXml(WallegNFe.Nota nota, X509Certificate2 x509Cert, String TagAssinatura, String URI = "")
        {
            StreamReader SR = null;

            try
            {
                //Abrir o arquivo XML a ser assinado e ler o seu conteúdo
                SR = File.OpenText(nota.CaminhoFisico);
                string xmlString = SR.ReadToEnd();
                SR.Close();
                SR = null;


                // Create a new XML document.
                var doc = new XmlDocument();

                // Format the document to ignore white spaces.
                doc.PreserveWhitespace = false;
                doc.LoadXml(xmlString);

                XmlDocument XMLDoc;


                var reference = new Reference();
                if(!String.IsNullOrEmpty(nota.NotaId))
                    reference.Uri = "#" + TagAssinatura + nota.NotaId;
                else if(!String.IsNullOrEmpty(URI))
                    reference.Uri = URI;

                // Create a SignedXml object.
                var signedXml = new SignedXml(doc);

                // Add the key to the SignedXml document
                signedXml.SigningKey = x509Cert.PrivateKey;

                // Add an enveloped transformation to the reference.
                var env = new XmlDsigEnvelopedSignatureTransform();
                reference.AddTransform(env);

                var c14 = new XmlDsigC14NTransform();
                reference.AddTransform(c14);

                // Add the reference to the SignedXml object.
                signedXml.AddReference(reference);

                // Create a new KeyInfo object
                var keyInfo = new KeyInfo();

                // Load the certificate into a KeyInfoX509Data object
                // and add it to the KeyInfo object.
                keyInfo.AddClause(new KeyInfoX509Data(x509Cert));

                // Add the KeyInfo object to the SignedXml object.
                signedXml.KeyInfo = keyInfo;
                signedXml.ComputeSignature();

                // Get the XML representation of the signature and save
                // it to an XmlElement object.
                XmlElement xmlDigitalSignature = signedXml.GetXml();

                // Gravar o elemento no documento XML
                XmlNodeList assinaturaNodes = doc.GetElementsByTagName(TagAssinatura);
                foreach (XmlNode nodes in assinaturaNodes)
                {
                    nodes.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
                    break;
                }


                XMLDoc = new XmlDocument();
                XMLDoc.PreserveWhitespace = false;
                XMLDoc = doc;


                // Atualizar a string do XML já assinada
                string StringXMLAssinado = XMLDoc.OuterXml;

                //Atualiza a nota assinada
                nota.ConteudoXml = StringXMLAssinado;

                // Gravar o XML Assinado no HD

                String SignedFile = nota.CaminhoFisico;
                StreamWriter SW_2 = File.CreateText(SignedFile);
                SW_2.Write(StringXMLAssinado);
                SW_2.Close();

                return SignedFile;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (SR != null)
                    SR.Close();
            }
        }
    }
}