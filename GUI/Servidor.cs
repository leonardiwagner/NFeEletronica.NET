using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//Trabalhar com o certificado
using System.Security.Cryptography.X509Certificates;

namespace GUI
{
    public partial class Servidor : Form
    {
        private readonly String arquivoCertificadoSalva = "C:\\certificado.pfx";

        public Servidor()
        {
            InitializeComponent();

            //Seleciona um certificado
            WallegNfe.Certificado wallegCertificado = new WallegNfe.Certificado();
            X509Certificate2 certificado = wallegCertificado.SelecionarPorWindows();
            //X509Certificate2 certificado = wallegCertificado.SelecionarPorArquivo("C:\\certificado.pfx");

            //Envia uma nota
            WallegNfe.Operacao.Recepcao recepcao = new WallegNfe.Operacao.Recepcao(certificado);
            recepcao.AdicionarNota("C:\\NFE\\Xml\\teste.xml");

            WallegNfe.Retorno retorno1 = recepcao.Enviar(3);

            WallegNfe.Operacao.RetRecepcao retRecepcao = new WallegNfe.Operacao.RetRecepcao(certificado);

            WallegNfe.Retorno retorno2 = retRecepcao.NfeRetRecepcao2(retorno1.Recibo);

        }
    }
}
