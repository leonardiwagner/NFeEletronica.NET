using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using System.Xml;

namespace Bll
{
    public class Arquivo
    {

        public List<Model.Nota> LeNota(Model.NotaSituacao NotaSituacao)
        {
            switch (NotaSituacao)
            {
                case Model.NotaSituacao.DEFAULT:
                    return this.LeNotaPasta(Bll.Util.ContentFolderDefault, NotaSituacao);
                case Model.NotaSituacao.VALIDADA:
                    return this.LeNotaPasta(Bll.Util.ContentFolderValidado, NotaSituacao);
                case Model.NotaSituacao.ASSINADA:
                    return this.LeNotaPasta(Bll.Util.ContentFolderAssinado, NotaSituacao);
                case Model.NotaSituacao.ENVIADA:
                    return this.LeNotaPasta(Bll.Util.ContentFolderEnviado, NotaSituacao);
                case Model.NotaSituacao.REJEITADA:
                    return this.LeNotaPasta(Bll.Util.ContentFolderRejeitado, NotaSituacao);
            }

            return null;
        }

        /// <summary>
        /// Retorna todos os arquivos de uma pasta com sua determinada situação
        /// </summary>
        /// <param name="Pasta"></param>
        /// <param name="PastaSituacao"></param>
        /// <returns></returns>
        private List<Model.Nota> LeNotaPasta(String Pasta, Model.NotaSituacao PastaSituacao)
        {
            Bll.Xml bllXml = new Xml();

            List<Model.Nota> NotaLista = new List<Model.Nota>();
            foreach (String Arquivo in Directory.EnumerateFiles(Pasta, "*.xml"))
            {

                Model.Nota nota = bllXml.XmlToNota(Arquivo);
                nota.Situacao = PastaSituacao;
                NotaLista.Add(nota);

                
            }

            return NotaLista;
        }

        

        
    }
}
