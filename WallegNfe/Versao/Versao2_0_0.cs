using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallegNFe.Versao
{
    public class Versao2_0_0 : VersaoAbstract
    {
        public override string VersaoString
        {
            get { return "2.00"; }
        }

        public override string PastaXML
        {
            get { return "2.0.0"; }
        }

        public override NFeVersao Versao
        {
            get { return NFeVersao.VERSAO_2_0_0; }
        }
    }
}
