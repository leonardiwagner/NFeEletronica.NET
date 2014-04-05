using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace WallegNFe.Bll
{
    public class Util
    {
        public static String ContentFolderSchemaValidacao
        {
            get
            {
                if (ConfigurationManager.AppSettings["WallegNFe.Pasta.SchemaValidacao"] != null)
                {
                    return ConfigurationManager.AppSettings["WallegNFe.Pasta.SchemaValidacao"];
                }
                return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\NFeSchemas";
            }
        }

        public static String GerarModulo11(String chaveAcesso)
        {
            int total = 0;
            int multiplier = 2;

            for (int i = chaveAcesso.Length - 1; i >= 0; i--)
            {
                if (9 < multiplier)
                {
                    multiplier = 2;
                }

                int digit = Int32.Parse(chaveAcesso.Substring(i, 1));
                total += digit*multiplier++;
            }

            int remainder = (total%11);

            if (0 == remainder || 1 == remainder)
            {
                return "0";
            }

            return (11 - remainder).ToString();
        }

       
    }
}