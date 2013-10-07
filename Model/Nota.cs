using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Nota
    {
        public String ArquivoFisico { get; set; }
        public String ArquivoFisicoConteudo { get; set; }
        public DateTime DataEmissao { get; set; }
        public String Numero { get; set; }
        public String DestinatarioNome { get; set; }
        public String DestinatarioCNPJ { get; set; }
        public NotaSituacao Situacao { get; set; }
    }

    public enum NotaSituacao
    {
        DEFAULT = 0,
        VALIDADA = 1,
        ASSINADA = 2,
        ENVIADA = 3,
        REJEITADA = 4
    }
}
