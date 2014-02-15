using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallegNfe.Model
{
    public class Evento
    {
        public String NumeroLote { get; set; }
        public String CNPJ { get; set; }
        public String ChaveAcesso { get; set; }

        public String Protocolo { get; set; }
        public String Justificativa { get; set; }
    }
}
