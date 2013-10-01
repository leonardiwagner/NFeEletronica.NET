using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Operacao
    {
        public int Id { get; set; }
        public String Tag { get; set; }
        public String Schema { get; set; }
        public String TagAssinatura { get; set; }
        public String TagAtributoId { get; set; }
        public String Namespace { get; set; }
    }

    public enum OperacaoType
    {
        NFe = 1,
        Cancelamento = 2,
        EnvioLote = 3
    }
    
}
