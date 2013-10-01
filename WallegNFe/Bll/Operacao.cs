using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bll
{
    public class Operacao
    {
        /// <summary>
        /// Carrega os dados de uma determinada operação
        /// </summary>
        /// <param name="Operacao"></param>
        /// <returns></returns>
        public Model.Operacao Selecionar(Model.OperacaoType operacaoTipo)
        {
            switch (operacaoTipo)
            {
                case Model.OperacaoType.NFe:
                    return new Model.Operacao()
                    {
                        Id = (int)Model.OperacaoType.NFe,
                        Schema = "nfe_v2.00.xsd",
                        TagAssinatura = "NFe",
                        TagAtributoId = "infNFe"
                    };
                case Model.OperacaoType.Cancelamento:
                    return new Model.Operacao()
                    {
                        Id = (int)Model.OperacaoType.Cancelamento,
                        Schema = "cancNFe_v2.00.xsd",
                        TagAssinatura = "cancNFe",
                        TagAtributoId = "infCanc"
                    };
                case Model.OperacaoType.EnvioLote:
                    return new Model.Operacao()
                    {
                        Id = (int)Model.OperacaoType.EnvioLote,
                        Schema = "enviNFe_v2.00.xsd"
                    };
            }

            return null;
        }
    }
}
