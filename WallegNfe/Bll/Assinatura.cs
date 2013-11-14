using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Trabalhar com o certificado
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;

//Trabalhar com o XML e arquivos
using System.IO;
using System.Xml;

namespace WallegNfe.Bll
{
    public class Assinatura
    {

        /// <summary>
        /// Assina um arquivo Xml
        /// </summary>
        /// <param name="arquivoNome"></param>
        /// <param name="operacao"></param>
        /// <param name="x509Cert"></param>
        public String AssinarXml(string arquivoNome, X509Certificate2 x509Cert, String TagAssinatura, String TagAtributoId)
        {
            StreamReader SR = null;

            try
            {
                //Abrir o arquivo XML a ser assinado e ler o seu conteúdo
                SR = File.OpenText(arquivoNome);
                string xmlString = SR.ReadToEnd();
                SR.Close();
                SR = null;

                try
                {
                    // Create a new XML document.
                    XmlDocument doc = new XmlDocument();

                    // Format the document to ignore white spaces.
                    doc.PreserveWhitespace = false;

                    // Load the passed XML file using it’s name.
                    try
                    {
                        doc.LoadXml(xmlString);

                        if (doc.GetElementsByTagName(TagAssinatura).Count == 0)
                        {
                            throw new Exception("A tag de assinatura " + TagAssinatura.Trim() + " não existe no XML. (Código do Erro: 5)");
                        }
                        else if (doc.GetElementsByTagName(TagAtributoId).Count == 0)
                        {
                            throw new Exception("A tag de assinatura " + TagAtributoId.Trim() + " não existe no XML. (Código do Erro: 4)");
                        }
                        // Existe mais de uma tag a ser assinada
                        else
                        {
                            try
                            {
                                XmlDocument XMLDoc;

                                XmlNodeList lists = doc.GetElementsByTagName(TagAssinatura);
                                foreach (XmlNode nodes in lists)
                                {
                                    foreach (XmlNode childNodes in nodes.ChildNodes)
                                    {
                                        if (!childNodes.Name.Equals(TagAtributoId))
                                            continue;

                                        if (childNodes.NextSibling != null && childNodes.NextSibling.Name.Equals("Signature"))
                                            continue;

                                        // Create a reference to be signed
                                        Reference reference = new Reference();
                                        reference.Uri = "";

                                        // pega o uri que deve ser assinada                                       
                                        XmlElement childElemen = (XmlElement)childNodes;
                                        if (childElemen.GetAttributeNode("Id") != null)
                                        {
                                            reference.Uri = "#" + childElemen.GetAttributeNode("Id").Value;
                                        }
                                        else if (childElemen.GetAttributeNode("id") != null)
                                        {
                                            reference.Uri = "#" + childElemen.GetAttributeNode("id").Value;
                                        }

                                        // Create a SignedXml object.
                                        SignedXml signedXml = new SignedXml(doc);

                                        // Add the key to the SignedXml document
                                        signedXml.SigningKey = x509Cert.PrivateKey;

                                        // Add an enveloped transformation to the reference.
                                        XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                                        reference.AddTransform(env);

                                        XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
                                        reference.AddTransform(c14);

                                        // Add the reference to the SignedXml object.
                                        signedXml.AddReference(reference);

                                        // Create a new KeyInfo object
                                        KeyInfo keyInfo = new KeyInfo();

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
                                        nodes.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
                                    }
                                }

                                XMLDoc = new XmlDocument();
                                XMLDoc.PreserveWhitespace = false;
                                XMLDoc = doc;

                                
                                // Atualizar a string do XML já assinada
                                string StringXMLAssinado = XMLDoc.OuterXml;

                                // Gravar o XML Assinado no HD
                                String SignedFile = arquivoNome;
                                StreamWriter SW_2 = File.CreateText(SignedFile);
                                SW_2.Write(StringXMLAssinado);
                                SW_2.Close();

                                return SignedFile;
                                
                            }
                            catch (Exception caught)
                            {
                                throw (caught);
                            }
                        }
                    }
                    catch (Exception caught)
                    {
                        throw (caught);
                    }
                }
                catch (Exception caught)
                {
                    throw (caught);
                }
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
