using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Trabalhar com o certificado
using System.Security.Cryptography.X509Certificates;

namespace Exemplo
{
    class Program
    {
        static void Main(string[] args)
        {
            EnviarNota enviarNota = new EnviarNota();
            enviarNota.Enviar();

            /*

            WallegNFe.NfeContexto nfeContexto = new WallegNFe.NfeContexto(false);
            
            WallegNFe.Operacao.RecepcaoEvento operacaoEvento = new WallegNFe.Operacao.RecepcaoEvento(nfeContexto);

            operacaoEvento.Cancelar(new WallegNFe.Model.Evento()
            {
                 ChaveAcesso = "35140207293766000150550010000414621933552767",
                 CNPJ = "07293766000150",
                 Justificativa = "teste",
                 Protocolo = ""
            },"C:\\cancela.xml");

            return;

            

            


            

            

            Console.ReadLine();
            */
        }
    }
}
