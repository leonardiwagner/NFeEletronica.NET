using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using System.Xml;

namespace WallegNFe.Bll
{
    public class Arquivo
    {
       

        /*
        /// <summary>
        /// Retorna todos os arquivos *.xml de uma pasta
        /// </summary>
        /// <param name="pas"></param>
        /// <returns></returns>
        public static List<String> LePastaXml(String pasta)
        {
            return Directory.EnumerateFiles(pasta, "*.xml").ToList();
        }
        */


        /// <summary>
        /// Retorna o nome de um arquivo pelo seu caminho
        /// </summary>
        /// <param name="arquivoCaminho"></param>
        /// <returns></returns>
        public static String Nome(String arquivoCaminho)
        {
            return Path.GetFileName(arquivoCaminho);
        }

        /// <summary>
        /// Le o arquivo e retorna como texto
        /// </summary>
        /// <param name="arquivoCaminho"></param>
        /// <returns></returns>
        public static String Ler(String arquivoCaminho)
        {
            return File.ReadAllText(arquivoCaminho);
        }

        /// <summary>
        /// Salva um arquivo
        /// </summary>
        /// <param name="conteudo"></param>
        /// <param name="arquivoNome"></param>
        /// <returns></returns>
        public static String Salva(String conteudo, String arquivoCaminho)
        {
            StreamWriter SW_2 = File.CreateText(arquivoCaminho);
            SW_2.Write(conteudo);
            SW_2.Close();

            return arquivoCaminho;
        }

        /// <summary>
        /// Verifica se um arquivo existe
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static bool ExisteArquivo(String arquivoCaminho)
        {
            if (File.Exists(arquivoCaminho))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Verifica se uma pasta existe
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static bool ExistePasta(String pastaCaminho)
        {
            if (Directory.Exists(pastaCaminho))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Move um arquivo
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public static String Move(String arquivoDe, String arquivoPara)
        {
            File.Move(arquivoDe, arquivoPara);
            return arquivoPara;
        }

        /// <summary>
        /// Copia um arquivo
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public static String Copia(String arquivoDe, String arquivoPara)
        {
            File.Copy(arquivoDe, arquivoPara);
            return arquivoPara;
        }
        

        
    }
}
