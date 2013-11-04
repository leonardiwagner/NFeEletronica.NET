using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Security.Cryptography.X509Certificates;

namespace Model
{
    public class Envio
    {
        //public Operacao TipoOperacao { get; set; }
        public X509Certificate2 Certificado { get; set; }
        public XmlDocument LoteXml { get; set; }
    }
}
