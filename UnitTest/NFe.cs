using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class NFe
    {
        [TestMethod]
        public void ValidaNota()
        {
            Bll.Xml bllXml = new Bll.Xml();
            bllXml.ValidaSchema("C:\\myfilename.xml", "C:\\nfe_v2.00.xsd");
        }

        [TestMethod]
        public void ListarNotas()
        {
            Bll.Arquivo bllArquivo = new Bll.Arquivo();
            List<Model.Nota> listaNota = new List<Model.Nota>();
            
        }
    }
}
