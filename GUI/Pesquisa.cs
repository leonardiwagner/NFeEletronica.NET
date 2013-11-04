using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Text.RegularExpressions;

namespace GUI
{
    public partial class Pesquisa : Form
    {

        List<Model.Nota> notaList = new List<Model.Nota>();
        List<Model.Nota> notaFiltradaList = new List<Model.Nota>();

        public Pesquisa()
        {
            InitializeComponent();

            this.lblInfo.Text = "Pronto";

            try
            {
                this.CarregarNota();
            }
            catch (Exception e)
            {
                this.lblInfo.Text = "Erro ao carregar notas: " + e.Message;
            }

            Dictionary<int, String> situacaoList = new Dictionary<int, string>();
            situacaoList.Add((int)Model.NotaSituacao.DEFAULT, "Todas");
            situacaoList.Add((int)Model.NotaSituacao.ASSINADA, "Assinadas");
            situacaoList.Add((int)Model.NotaSituacao.ENVIADA, "Enviadas");
            situacaoList.Add((int)Model.NotaSituacao.REJEITADA, "Rejeitadas");
            situacaoList.Add((int)Model.NotaSituacao.VALIDADA, "Validadas");

            this.fldNotaSituacao.DataSource = new BindingSource(situacaoList, null);
            this.fldNotaSituacao.ValueMember = "Key";
            this.fldNotaSituacao.DisplayMember = "Value";
        }

        public void CarregarNota()
        {
            Bll.Arquivo bllArquivo = new Bll.Arquivo();
            //this.notaList.AddRange(bllArquivo.LeNota(Model.NotaSituacao.DEFAULT));
            //this.notaList.AddRange(bllArquivo.LeNota(Model.NotaSituacao.ASSINADA));
        }

        public void Filtrar()
        {
            this.notaFiltradaList = (from nL in this.notaList
                                     where ((int)this.fldNotaSituacao.SelectedValue == 0 || nL.Situacao == (Model.NotaSituacao)((int)this.fldNotaSituacao.SelectedValue)) &&
                                    nL.DataEmissao < DateTime.Parse(this.fldPeriodoFim.Text) &&
                                    nL.DataEmissao > DateTime.Parse(this.fldPeriodoInicio.Text) &&
                                    (this.fldDestinatarioCNPJ.Text == "" || nL.DestinatarioCNPJ.ToLower().StartsWith(this.fldDestinatarioCNPJ.Text.ToLower())) &&
                                    (this.fldDestinatarioNome.Text == "" || nL.DestinatarioNome.ToLower().Contains(this.fldDestinatarioNome.Text.ToLower())) //&&
                                    
                                    //(this.fldNumeroInicio.Text == "" || nL.Numero > Int32.Parse(this.fldNumeroInicio.Text)) &&
                                    //(this.fldNumeroInicio.Text == "" || nL.Numero > Int32.Parse(this.fldNumeroInicio.Text)) &&
                                    select nL).ToList();

            this.grdResultado.DataSource = this.notaFiltradaList;

            this.lblInfo.Text = "Mostrando " + this.notaFiltradaList.Count() + " de " + this.notaList.Count() + " notas";
        }

        public void LimparFiltro()
        {
            this.fldPeriodoInicio.Value = DateTime.Now.AddMonths(-1);
            this.fldPeriodoFim.Value = DateTime.Now;

            this.fldDestinatarioCNPJ.Text = "";
            this.fldDestinatarioNome.Text = "";
            
            this.fldNumeroInicio.Text = "";
            this.fldNumeroFim.Text = "";

            this.fldNotaSituacao.SelectedIndex = 0;

            this.lblInfo.Text = "Pesquisa reiniciada";
        }

       

        private void btnPesquisarNovo_Click(object sender, EventArgs e)
        {
            this.LimparFiltro();
            this.Filtrar();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            this.Filtrar();
        }

    }
}
