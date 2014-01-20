using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Trabalhar com o certificado
using System.Security.Cryptography.X509Certificates;

namespace Exemplo
{
    class Program
    {
        static void Main(string[] args)
        {
            WallegNfe.Nfe nfe = new WallegNfe.Nfe(false, "C:\\certificado2.pfx");

            //Enviar uma nota
            WallegNfe.Operacao.Recepcao nfeRecepcao = new WallegNfe.Operacao.Recepcao(nfe);

            nfeRecepcao.AdicionarNota("C:\\teste.xml");
            String numeroRecibo = nfeRecepcao.Enviar(1).Recibo;

            //Consultar nota
            WallegNfe.Operacao.RetRecepcao nfeRetRecepcao = new WallegNfe.Operacao.RetRecepcao(nfe);
            WallegNfe.Model.Retorno.RetRecepcao retRetorno = nfeRetRecepcao.Enviar(numeroRecibo);

            //Cancelar nota
            WallegNfe.Operacao.RecepcaoEvento nfeRecepcaoEvento = new WallegNfe.Operacao.RecepcaoEvento(nfe);




            Console.ReadLine();
        }
    }
}
