namespace ListaTarefas.Forms
{
    partial class FormTarefa
    {
        /// <summary>
        /// Variável do designer (guarda componentes como o ErrorProvider).
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Libera os recursos usados pela tela.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer do Windows Forms

        /// <summary>
        /// Método usado pelo Designer. Pode ser editado pelo modo Design do Visual Studio.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.lblDataInicio = new System.Windows.Forms.Label();
            this.mtbDataInicio = new System.Windows.Forms.MaskedTextBox();
            this.lblDataFim = new System.Windows.Forms.Label();
            this.mtbDataFim = new System.Windows.Forms.MaskedTextBox();
            this.lblDicaDatas = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitulo  (o "&" cria o atalho Alt+T, que leva ao campo seguinte)
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(15, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(51, 17);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "&Título *";
            //
            // txtTitulo
            //
            this.txtTitulo.Location = new System.Drawing.Point(15, 37);
            this.txtTitulo.MaxLength = 100;
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(370, 24);
            this.txtTitulo.TabIndex = 1;
            //
            // lblDescricao
            //
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Location = new System.Drawing.Point(15, 72);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(67, 17);
            this.lblDescricao.TabIndex = 2;
            this.lblDescricao.Text = "&Descrição";
            //
            // txtDescricao
            //
            this.txtDescricao.Location = new System.Drawing.Point(15, 94);
            this.txtDescricao.MaxLength = 500;
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescricao.Size = new System.Drawing.Size(370, 70);
            this.txtDescricao.TabIndex = 3;
            //
            // lblDataInicio
            //
            this.lblDataInicio.AutoSize = true;
            this.lblDataInicio.Location = new System.Drawing.Point(15, 178);
            this.lblDataInicio.Name = "lblDataInicio";
            this.lblDataInicio.Size = new System.Drawing.Size(91, 17);
            this.lblDataInicio.TabIndex = 4;
            this.lblDataInicio.Text = "Data de &início";
            //
            // mtbDataInicio  (máscara: só aceita números no formato de data)
            //
            this.mtbDataInicio.Location = new System.Drawing.Point(15, 200);
            this.mtbDataInicio.Mask = "00/00/0000";
            this.mtbDataInicio.Name = "mtbDataInicio";
            this.mtbDataInicio.Size = new System.Drawing.Size(120, 24);
            this.mtbDataInicio.TabIndex = 5;
            this.mtbDataInicio.ValidatingType = null;
            //
            // lblDataFim
            //
            this.lblDataFim.AutoSize = true;
            this.lblDataFim.Location = new System.Drawing.Point(200, 178);
            this.lblDataFim.Name = "lblDataFim";
            this.lblDataFim.Size = new System.Drawing.Size(101, 17);
            this.lblDataFim.TabIndex = 6;
            this.lblDataFim.Text = "Data de t&érmino";
            //
            // mtbDataFim
            //
            this.mtbDataFim.Location = new System.Drawing.Point(200, 200);
            this.mtbDataFim.Mask = "00/00/0000";
            this.mtbDataFim.Name = "mtbDataFim";
            this.mtbDataFim.Size = new System.Drawing.Size(120, 24);
            this.mtbDataFim.TabIndex = 7;
            this.mtbDataFim.ValidatingType = null;
            //
            // lblDicaDatas
            //
            this.lblDicaDatas.AutoSize = true;
            this.lblDicaDatas.ForeColor = System.Drawing.Color.DimGray;
            this.lblDicaDatas.Location = new System.Drawing.Point(15, 232);
            this.lblDicaDatas.Name = "lblDicaDatas";
            this.lblDicaDatas.Size = new System.Drawing.Size(360, 17);
            this.lblDicaDatas.TabIndex = 10;
            this.lblDicaDatas.Text = "As datas são opcionais. Deixe em branco se não quiser usar.";
            //
            // btnSalvar
            //
            this.btnSalvar.Location = new System.Drawing.Point(200, 275);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(90, 32);
            this.btnSalvar.TabIndex = 8;
            this.btnSalvar.Text = "&Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(295, 275);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 32);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            //
            // errorProvider  (mostra um ícone vermelho ao lado do campo com erro)
            //
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            //
            // FormTarefa
            //
            this.AcceptButton = this.btnSalvar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(420, 330);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtTitulo);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.lblDataInicio);
            this.Controls.Add(this.mtbDataInicio);
            this.Controls.Add(this.lblDataFim);
            this.Controls.Add(this.mtbDataFim);
            this.Controls.Add(this.lblDicaDatas);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTarefa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tarefa";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label lblDataInicio;
        private System.Windows.Forms.MaskedTextBox mtbDataInicio;
        private System.Windows.Forms.Label lblDataFim;
        private System.Windows.Forms.MaskedTextBox mtbDataFim;
        private System.Windows.Forms.Label lblDicaDatas;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
