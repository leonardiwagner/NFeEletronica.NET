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
            for (int i = 0; i < this.NotaEnvio.Count(); i++)
            {
                bllNFe.AdicionaNotaLote(this.NotaEnvio[i].ArquivoFisico);
                
            }

            bllNFe.Enviar();

        

           
        }


        private void LerNotas()
        {

            Bll.Arquivo bllArquivo = new Bll.Arquivo();
            this.NotaEnvio = bllArquivo.LeNota(Model.NotaSituacao.DEFAULT);



            grdEnvio.AutoGenerateColumns = true;
            grdEnvio.AllowUserToAddRows = false;
            grdEnvio.DataSource = this.NotaEnvio.Where(i => i.Situacao != Model.NotaSituacao.ENVIADA).ToList();
        }

    }
}
