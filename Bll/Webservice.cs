using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Xml;

using System.Security.Cryptography.X509Certificates;




namespace Bll
{
    public class Webservice
    {

        public String  Enviar(Model.Envio envio)
        {
            Dal.Recepcao dalRecepcao = new Dal.Recepcao();
            return dalRecepcao.Envia(envio);
            
           
            //guardar numero do recibo no banco de dados
        }
    }
}
