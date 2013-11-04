using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bll
{
    public class Operacao
    {
        public int Id { get; set; }
        public String Tag { get; set; }
        public String Schema { get; set; }
        public String TagAssinatura { get; set; }
        public String TagAtributoId { get; set; }
        public String Namespace { get; set; }

        /// <summary>
        /// Carrega os dados de uma determinada operação
        /// </summary>
        /// <param name="Operacao"></param>
        /// <returns></returns>
        public Operacao(Model.OperacaoType operacaoTipo)
        {
            switch (operacaoTipo)
            {
                case Model.OperacaoType.NFe:
                    this.Id = (int)Model.OperacaoType.NFe;
                    this.Schema = "nfe_v2.00.xsd";
                    this.TagAssinatura = "NFe";
                    this.TagAtributoId = "infNFe";
                    break;
                case Model.OperacaoType.Cancelamento:
                    this.Id = (int)Model.OperacaoType.Cancelamento;
                    this.Schema = "cancNFe_v2.00.xsd";
                    this.TagAssinatura = "cancNFe";
                    this.TagAtributoId = "infCanc";
                    break;
                case Model.OperacaoType.EnvioLote:
                    this.Id = (int)Model.OperacaoType.EnvioLote;
                    this.Schema = "enviNFe_v2.00.xsd";
                    break;
            }

        }
    }

    

        
    
}
