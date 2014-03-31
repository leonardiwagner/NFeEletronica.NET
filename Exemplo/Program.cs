//Trabalhar com o certificado

namespace Exemplo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var enviarNota = new EnviarNota();
            enviarNota.Enviar();

            /*

            WallegNFe.NFeContexto NFeContexto = new WallegNFe.NFeContexto(false);
            
            WallegNFe.Operacao.RecepcaoEvento operacaoEvento = new WallegNFe.Operacao.RecepcaoEvento(NFeContexto);

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