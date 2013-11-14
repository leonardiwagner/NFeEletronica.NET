using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;

namespace WallegNfe.Bll
{
    public class Util
    {
        public static String ContentFolderDefault = ConfigurationManager.AppSettings["WallegNFe.Pasta.Default"];
        public static String ContentFolderValidado = ConfigurationManager.AppSettings["WallegNFe.Pasta.Validado"];
        public static String ContentFolderAssinado = ConfigurationManager.AppSettings["WallegNFe.Pasta.Assinado"];
        public static String ContentFolderEnviado = ConfigurationManager.AppSettings["WallegNFe.Pasta.Enviado"];
        public static String ContentFolderRejeitado = ConfigurationManager.AppSettings["WallegNFe.Pasta.Rejeitado"];

        public static String ContentFolderSchemaValidacao = ConfigurationManager.AppSettings["WallegNFe.Pasta.SchemaValidacao"];

        

        
    }
}
