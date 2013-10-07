using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;

namespace Bll
{
    public class Util
    {
        public static String ContentFolderDefault = ConfigurationManager.AppSettings["WallegNFe.Pasta.Default"];
        public static String ContentFolderValidado = ConfigurationManager.AppSettings["WallegNFe.Pasta.Validado"];
        public static String ContentFolderAssinado = ConfigurationManager.AppSettings["WallegNFe.Pasta.Assinado"];
        public static String ContentFolderEnviado = ConfigurationManager.AppSettings["WallegNFe.Pasta.Enviado"];
        public static String ContentFolderRejeitado = ConfigurationManager.AppSettings["WallegNFe.Pasta.Rejeitado"];

        public static String ContentFolderSchemaValidacao = ConfigurationManager.AppSettings["WallegNFe.Pasta.Rejeitado"];

        
        

        public static String XmlToString(string parNomeArquivo)
        {
            string conteudo_xml = string.Empty;

            StreamReader SR = null;
            try
            {
                SR = File.OpenText(parNomeArquivo);
                conteudo_xml = SR.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SR.Close();
            }

            return conteudo_xml;
        }

        public static String SalvaArquivo(String Conteudo, String NomeArquivo)
        {
            StreamWriter SW_2 = File.CreateText(NomeArquivo);
            SW_2.Write(Conteudo);
            SW_2.Close();

            return NomeArquivo;
        }

        public static bool FileExists(String FilePath)
        {
            if (File.Exists(FilePath))
                return true;
            else
                return false;
        }

        public static String FileMove(String From, String To)
        {
            File.Move(From, To);
            return To;
        }

        public static String FileCopy(String From, String To)
        {
            File.Copy(From, To);
            return To;
        }
    }
}
