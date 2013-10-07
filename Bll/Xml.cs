using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Xml.Linq;

namespace Bll
{
    public class Xml
    {
        private String ValidarResultado = "";

        /// <summary>
        /// Valida se um Xml está seguindo de acordo um Schema
        /// </summary>
        /// <param name="arquivoXml">Arquivo Xml</param>
        /// <param name="arquivoSchema">Arquivo de Schema</param>
        /// <returns>True se estiver certo, Erro se estiver errado</returns>
        public String ValidaSchema(String arquivoXml, String arquivoSchema)
        {
            //Seleciona o arquivo de schema de acordo com o schema informado
            arquivoSchema = Bll.Util.ContentFolderSchemaValidacao + "\\" + arquivoSchema;

            //Verifica se o arquivo de XML foi encontrado.
            if (!Bll.Util.FileExists(arquivoXml)) throw new Exception("Arquivo de XML informado: \"" + arquivoXml + "\" não encontrado.");

            //Verifica se o arquivo de schema foi encontrado.
            if (!Bll.Util.FileExists(arquivoSchema)) throw new Exception("Arquivo de schema: \"" + arquivoSchema + "\" não encontrado.");

            // Cria um novo XMLValidatingReader
            XmlValidatingReader reader = new XmlValidatingReader(new XmlTextReader(new StreamReader(arquivoXml)));
            // Cria um schemacollection
            XmlSchemaCollection schemaCollection = new XmlSchemaCollection();
            //Adiciona o XSD e o namespace
            schemaCollection.Add("http://www.portalfiscal.inf.br/nfe", arquivoSchema);
            // Adiciona o schema ao ValidatingReader
            reader.Schemas.Add(schemaCollection);
            //Evento que retorna a mensagem de validacao
            reader.ValidationEventHandler += new ValidationEventHandler(reader_ValidationEventHandler);
            //Percorre o XML
            while (reader.Read()) { }
            reader.Close(); //Fecha o arquivo.
            //O Resultado é preenchido no reader_ValidationEventHandler
            if (ValidarResultado == "")
            {
                return ValidarResultado;
            }
            else
            {
                throw new Exception(ValidarResultado);
            }
        }

        private void reader_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            // Como sera exibida a mensagem de ERROS de validacao
            ValidarResultado = ValidarResultado + String.Format("\rLinha:{1}" + System.Environment.NewLine +
                                                  "\rColuna:{0}" + System.Environment.NewLine +
                                                  "\rErro:{2}" + System.Environment.NewLine,
                                                  e.Exception.LinePosition,
                                                  e.Exception.LineNumber,
                                                  e.Exception.Message);
        }

        public Model.Nota XmlToNota(String caminhoXml)
        {
            Model.Nota nota = new Model.Nota();


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(caminhoXml);


            nota.DestinatarioCNPJ = xmlDoc.GetElementsByTagName("emit")[0].ChildNodes[0].InnerText;
            nota.DestinatarioNome = xmlDoc.GetElementsByTagName("emit")[0].ChildNodes[1].InnerText;

            nota.DataEmissao = DateTime.Parse(xmlDoc.GetElementsByTagName("ide")[0].ChildNodes[7].InnerText);
            nota.Numero = xmlDoc.GetElementsByTagName("ide")[0].ChildNodes[6].InnerText;        


            return nota;
        }
    }
}
