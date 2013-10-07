namespace GUI
{
    partial class Server
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.barProgress = new System.Windows.Forms.ProgressBar();
            this.grdEnvio = new System.Windows.Forms.DataGridView();
            this.tmrEnvio = new System.Windows.Forms.Timer(this.components);
            this.fldStatus = new System.Windows.Forms.Label();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.grdEnviada = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdEnvio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEnviada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 93);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Notas para envio";
            // 
            // barProgress
            // 
            this.barProgress.Location = new System.Drawing.Point(17, 42);
            this.barProgress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barProgress.Name = "barProgress";
            this.barProgress.Size = new System.Drawing.Size(443, 32);
            this.barProgress.TabIndex = 1;
            // 
            // grdEnvio
            // 
            this.grdEnvio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdEnvio.Location = new System.Drawing.Point(20, 114);
            this.grdEnvio.Name = "grdEnvio";
            this.grdEnvio.Size = new System.Drawing.Size(579, 115);
            this.grdEnvio.TabIndex = 2;
            // 
            // fldStatus
            // 
            this.fldStatus.AutoSize = true;
            this.fldStatus.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fldStatus.Location = new System.Drawing.Point(12, 9);
            this.fldStatus.Name = "fldStatus";
            this.fldStatus.Size = new System.Drawing.Size(158, 29);
            this.fldStatus.TabIndex = 3;
            this.fldStatus.Text = "Status: Parado";
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(467, 42);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(135, 32);
            this.btnIniciar.TabIndex = 4;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // grdEnviada
            // 
            this.grdEnviada.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdEnviada.Location = new System.Drawing.Point(20, 260);
            this.grdEnviada.Name = "grdEnviada";
            this.grdEnviada.Size = new System.Drawing.Size(579, 115);
            this.grdEnviada.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 239);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Notas enviadas";
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(20, 405);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(579, 115);
            this.dataGridView3.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 384);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Notas Rejeitadas";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 537);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grdEnviada);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.fldStatus);
            this.Controls.Add(this.grdEnvio);
            this.Controls.Add(this.barProgress);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Server";
            this.Text = "WallegNFe";
            this.Load += new System.EventHandler(this.Server_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdEnvio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEnviada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar barProgress;
        private System.Windows.Forms.DataGridView grdEnvio;
        private System.Windows.Forms.Timer tmrEnvio;
        private System.Windows.Forms.Label fldStatus;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.DataGridView grdEnviada;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label3;
    }
}