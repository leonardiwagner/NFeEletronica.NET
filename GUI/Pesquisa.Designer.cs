namespace GUI
{
    partial class Pesquisa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grdResultado = new System.Windows.Forms.DataGridView();
            this.fldNotaSituacao = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fldPeriodoFim = new System.Windows.Forms.DateTimePicker();
            this.pnlFiltro = new System.Windows.Forms.GroupBox();
            this.btnPesquisarNovo = new System.Windows.Forms.Button();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.fldNumeroFim = new System.Windows.Forms.TextBox();
            this.fldNumeroInicio = new System.Windows.Forms.TextBox();
            this.fldDestinatarioNome = new System.Windows.Forms.TextBox();
            this.fldDestinatarioCNPJ = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fldPeriodoInicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdResultado)).BeginInit();
            this.pnlFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdResultado
            // 
            this.grdResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResultado.Location = new System.Drawing.Point(13, 219);
            this.grdResultado.Margin = new System.Windows.Forms.Padding(4);
            this.grdResultado.Name = "grdResultado";
            this.grdResultado.Size = new System.Drawing.Size(772, 245);
            this.grdResultado.TabIndex = 0;
            // 
            // fldNotaSituacao
            // 
            this.fldNotaSituacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fldNotaSituacao.FormattingEnabled = true;
            this.fldNotaSituacao.Location = new System.Drawing.Point(567, 101);
            this.fldNotaSituacao.Margin = new System.Windows.Forms.Padding(4);
            this.fldNotaSituacao.Name = "fldNotaSituacao";
            this.fldNotaSituacao.Size = new System.Drawing.Size(189, 26);
            this.fldNotaSituacao.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(395, 109);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Situação da NFe";
            // 
            // fldPeriodoFim
            // 
            this.fldPeriodoFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fldPeriodoFim.Location = new System.Drawing.Point(248, 33);
            this.fldPeriodoFim.Margin = new System.Windows.Forms.Padding(4);
            this.fldPeriodoFim.Name = "fldPeriodoFim";
            this.fldPeriodoFim.Size = new System.Drawing.Size(136, 26);
            this.fldPeriodoFim.TabIndex = 5;
            // 
            // pnlFiltro
            // 
            this.pnlFiltro.Controls.Add(this.btnPesquisarNovo);
            this.pnlFiltro.Controls.Add(this.btnPesquisar);
            this.pnlFiltro.Controls.Add(this.label7);
            this.pnlFiltro.Controls.Add(this.fldNumeroFim);
            this.pnlFiltro.Controls.Add(this.fldNumeroInicio);
            this.pnlFiltro.Controls.Add(this.fldDestinatarioNome);
            this.pnlFiltro.Controls.Add(this.fldDestinatarioCNPJ);
            this.pnlFiltro.Controls.Add(this.label6);
            this.pnlFiltro.Controls.Add(this.label5);
            this.pnlFiltro.Controls.Add(this.label4);
            this.pnlFiltro.Controls.Add(this.label3);
            this.pnlFiltro.Controls.Add(this.fldPeriodoInicio);
            this.pnlFiltro.Controls.Add(this.fldPeriodoFim);
            this.pnlFiltro.Controls.Add(this.label2);
            this.pnlFiltro.Controls.Add(this.fldNotaSituacao);
            this.pnlFiltro.Controls.Add(this.label1);
            this.pnlFiltro.Location = new System.Drawing.Point(13, 13);
            this.pnlFiltro.Margin = new System.Windows.Forms.Padding(4);
            this.pnlFiltro.Name = "pnlFiltro";
            this.pnlFiltro.Padding = new System.Windows.Forms.Padding(4);
            this.pnlFiltro.Size = new System.Drawing.Size(772, 198);
            this.pnlFiltro.TabIndex = 6;
            this.pnlFiltro.TabStop = false;
            this.pnlFiltro.Text = "Filtro";
            // 
            // btnPesquisarNovo
            // 
            this.btnPesquisarNovo.Location = new System.Drawing.Point(567, 146);
            this.btnPesquisarNovo.Margin = new System.Windows.Forms.Padding(4);
            this.btnPesquisarNovo.Name = "btnPesquisarNovo";
            this.btnPesquisarNovo.Size = new System.Drawing.Size(189, 39);
            this.btnPesquisarNovo.TabIndex = 17;
            this.btnPesquisarNovo.Text = "Nova Pesquisa";
            this.btnPesquisarNovo.UseVisualStyleBackColor = true;
            this.btnPesquisarNovo.Click += new System.EventHandler(this.btnPesquisarNovo_Click);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Location = new System.Drawing.Point(397, 146);
            this.btnPesquisar.Margin = new System.Windows.Forms.Padding(4);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(161, 39);
            this.btnPesquisar.TabIndex = 16;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = true;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(230, 71);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 18);
            this.label7.TabIndex = 15;
            this.label7.Text = "a";
            // 
            // fldNumeroFim
            // 
            this.fldNumeroFim.Location = new System.Drawing.Point(248, 67);
            this.fldNumeroFim.Margin = new System.Windows.Forms.Padding(4);
            this.fldNumeroFim.Name = "fldNumeroFim";
            this.fldNumeroFim.Size = new System.Drawing.Size(135, 26);
            this.fldNumeroFim.TabIndex = 14;
            // 
            // fldNumeroInicio
            // 
            this.fldNumeroInicio.Location = new System.Drawing.Point(91, 67);
            this.fldNumeroInicio.Margin = new System.Windows.Forms.Padding(4);
            this.fldNumeroInicio.Name = "fldNumeroInicio";
            this.fldNumeroInicio.Size = new System.Drawing.Size(135, 26);
            this.fldNumeroInicio.TabIndex = 13;
            // 
            // fldDestinatarioNome
            // 
            this.fldDestinatarioNome.Location = new System.Drawing.Point(567, 33);
            this.fldDestinatarioNome.Margin = new System.Windows.Forms.Padding(4);
            this.fldDestinatarioNome.Name = "fldDestinatarioNome";
            this.fldDestinatarioNome.Size = new System.Drawing.Size(189, 26);
            this.fldDestinatarioNome.TabIndex = 12;
            // 
            // fldDestinatarioCNPJ
            // 
            this.fldDestinatarioCNPJ.Location = new System.Drawing.Point(567, 67);
            this.fldDestinatarioCNPJ.Margin = new System.Windows.Forms.Padding(4);
            this.fldDestinatarioCNPJ.Name = "fldDestinatarioCNPJ";
            this.fldDestinatarioCNPJ.Size = new System.Drawing.Size(189, 26);
            this.fldDestinatarioCNPJ.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(394, 33);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 18);
            this.label6.TabIndex = 10;
            this.label6.Text = "Nome do Destinatário";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(395, 70);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "CNPJ/CPF do Destinatário";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 70);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "Número";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "a";
            // 
            // fldPeriodoInicio
            // 
            this.fldPeriodoInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fldPeriodoInicio.Location = new System.Drawing.Point(91, 33);
            this.fldPeriodoInicio.Margin = new System.Windows.Forms.Padding(4);
            this.fldPeriodoInicio.Name = "fldPeriodoInicio";
            this.fldPeriodoInicio.Size = new System.Drawing.Size(135, 26);
            this.fldPeriodoInicio.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Período";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(13, 472);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(46, 18);
            this.lblInfo.TabIndex = 7;
            this.lblInfo.Text = "label8";
            // 
            // Pesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 495);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.pnlFiltro);
            this.Controls.Add(this.grdResultado);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Pesquisa";
            this.Text = "WallegNFe - Pesquisar";
            ((System.ComponentModel.ISupportInitialize)(this.grdResultado)).EndInit();
            this.pnlFiltro.ResumeLayout(false);
            this.pnlFiltro.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdResultado;
        private System.Windows.Forms.ComboBox fldNotaSituacao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker fldPeriodoFim;
        private System.Windows.Forms.GroupBox pnlFiltro;
        private System.Windows.Forms.Button btnPesquisarNovo;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox fldNumeroFim;
        private System.Windows.Forms.TextBox fldNumeroInicio;
        private System.Windows.Forms.TextBox fldDestinatarioNome;
        private System.Windows.Forms.TextBox fldDestinatarioCNPJ;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker fldPeriodoInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblInfo;
    }
}

