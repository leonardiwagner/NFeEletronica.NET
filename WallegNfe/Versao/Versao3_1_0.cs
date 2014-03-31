using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallegNFe.Versao
{
    public class Versao3_1_0 : VersaoAbstract
    {
        public override string VersaoString
        {
            get { return "3.10"; }
        }

        public override string PastaXML
        {
            get { return "3.1.0"; }
        }

        public override NFeVersao Versao
        {
            get { return NFeVersao.VERSAO_3_1_0; }
        }
    }
}
