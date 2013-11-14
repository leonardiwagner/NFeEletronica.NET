using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Trabalhar com XML e Schema
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;

//Trabalhar com arquivos
using System.IO;

namespace WallegNfe.Bll
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
        public void ValidaSchema(String arquivoXml, String arquivoSchema)
        {
            //Seleciona o arquivo de schema de acordo com o schema informado
            //arquivoSchema = Bll.Util.ContentFolderSchemaValidacao + "\\" + arquivoSchema;

            //Verifica se o arquivo de XML foi encontrado.
            if (!Bll.Arquivo.ExisteArquivo(arquivoXml)) throw new Exception("Arquivo de XML informado: \"" + arquivoXml + "\" não encontrado.");

            //Verifica se o arquivo de schema foi encontrado.
            if (!Bll.Arquivo.ExisteArquivo(arquivoSchema)) throw new Exception("Arquivo de schema: \"" + arquivoSchema + "\" não encontrado.");

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

            /*
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
             */
            if (ValidarResultado != "")
            {
                throw new Exception(ValidarResultado);
            }
        }

        /// <summary>
        /// Se der um erro na validação do schema, esse evento é disparado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        public static XmlDocument StringToXml(String stringText)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(stringText);

            return xml;
        }


    }
}
