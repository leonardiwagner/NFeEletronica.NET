using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;

namespace Bll
{
    public class Util
    {
        public static String ContentFolder = ConfigurationManager.AppSettings["WallegNFe.Content.Path"];
        public static String ContentFolderOriginal = ContentFolder + "\\Original";
        public static String ContentFolderSigned = ContentFolder + "\\Assinado";
        public static String ContentFolderSent = ContentFolder + "\\Sent";
        public static String ContentFolderSchema = ContentFolder + "\\Schema";

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
