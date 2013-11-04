using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class Server : Form
    {
        private List<Model.Nota> NotaEnvio = new List<Model.Nota>();

        public Server()
        {
            InitializeComponent();

            this.LerNotas();
        }

        private void Server_Load(object sender, EventArgs e)
        {
            this.tmrEnvio.Interval = 60000;
            this.tmrEnvio.Enabled = true;
        }

        private void AtualizaStatus(String Texto)
        {
            this.fldStatus.Text = "Status: " + Texto;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            Bll.NFe bllNFe = new Bll.NFe();

            //Le as notas para envio
            
            for (int i = 0; i < this.NotaEnvio.Count(); i++)
            {
                //bllNFe.AdicionaNotaLote(this.NotaEnvio[i].ArquivoFisico);
                
            }

            bllNFe.Enviar();

        }


        private void LerNotas()
        {
            Bll.Nota bllNota = new Bll.Nota();
            Bll.Xml bllXml = new Bll.Xml();

            //Carrega as notas para envio
            List<Model.Nota> notaList = bllNota.Carregar(Model.NotaSituacao.DEFAULT);

            //Verifica se as notas estão de acordo com o schema
            String arquivoSchemaNota = new Bll.Operacao(Model.OperacaoType.NFe).Schema;
            for (int i = 0; i < notaList.Count(); i++)
            {
                try
                {
                    bllXml.ValidaSchema(notaList[i].ArquivoFisicoCaminho, arquivoSchemaNota);

                    //move para validadas
                    bllNota.Move(notaList[i], Model.NotaSituacao.VALIDADA);
                }
                catch
                {
                    //não bateu com o schema
                    bllNota.Move(notaList[i], Model.NotaSituacao.INVALIDA);
                }
            }

            //Carrega as notas validadas
            Bll.Assinatura bllAssinatura = new Bll.Assinatura();
            List<Model.Nota> notaValidadaList = bllNota.Carregar(Model.NotaSituacao.VALIDADA);
            for(int i=0;i<this.NotaEnvio.Count(); i++)
            {
                //bllAssinatura.AssinarXml(notaValidadaList[i].ArquivoFisicoCaminho, new Bll.Operacao(Model.OperacaoType.EnvioLote), null);
            }
            



            grdEnvio.AutoGenerateColumns = true;
            grdEnvio.AllowUserToAddRows = false;
            grdEnvio.DataSource = this.NotaEnvio.Where(i => i.Situacao != Model.NotaSituacao.ENVIADA).ToList();
        }

    }
}
