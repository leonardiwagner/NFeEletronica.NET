using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bll
{
    public class Nota
    {
        /// <summary>
        /// Muda a pasta de uma nota
        /// </summary>
        /// <param name="nota"></param>
        /// <param name="situacao"></param>
        public void Move(Model.Nota nota, Model.NotaSituacao novaSituacao)
        {
            Bll.Arquivo.Move(Bll.Arquivo.PastaNota(nota.Situacao) + "/" + nota.ArquivoFisicoNome, Bll.Arquivo.PastaNota(novaSituacao) + "/" + nota.ArquivoFisicoNome);

            nota.Situacao = novaSituacao;
        }

        /// <summary>
        /// Retorna notas de uma determinada situação
        /// </summary>
        /// <param name="situacao"></param>
        /// <returns></returns>
        public List<Model.Nota> Carregar(Model.NotaSituacao situacao)
        {
            Bll.Xml bllXml = new Xml();
            String pastaSituacao = Bll.Arquivo.PastaNota(situacao);

            List<Model.Nota> notaLista = new List<Model.Nota>();
            foreach (String arquivoCaminho in Bll.Arquivo.LePastaXml(pastaSituacao))
            {
                //Transforma uma nota *.xml para um objeto Nota
                Model.Nota nota = bllXml.XmlToNota(arquivoCaminho);
                nota.ArquivoFisicoCaminho = arquivoCaminho;
                nota.ArquivoFisicoNome = Bll.Arquivo.Nome(arquivoCaminho);
                nota.Situacao = situacao;
                

                //Carrega dados de xml
                notaLista.Add(nota);
            }

            return notaLista; 
        }
    }
}
