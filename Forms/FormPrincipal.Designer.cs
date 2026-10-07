namespace ListaTarefas.Forms
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variável do designer (guarda componentes como o ToolTip).
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
            this.pnlTopo = new System.Windows.Forms.Panel();
            this.chkMostrarConcluidas = new System.Windows.Forms.CheckBox();
            this.lblCabecalho = new System.Windows.Forms.Label();
            this.dgvTarefas = new System.Windows.Forms.DataGridView();
            this.colPosicao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSituacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBotoes = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNova = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnConcluir = new System.Windows.Forms.Button();
            this.btnSubir = new System.Windows.Forms.Button();
            this.btnDescer = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.lblLegenda = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlTopo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarefas)).BeginInit();
            this.pnlBotoes.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTopo
            //
            this.pnlTopo.Controls.Add(this.chkMostrarConcluidas);
            this.pnlTopo.Controls.Add(this.lblCabecalho);
            this.pnlTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopo.Location = new System.Drawing.Point(0, 0);
            this.pnlTopo.Name = "pnlTopo";
            this.pnlTopo.Padding = new System.Windows.Forms.Padding(12, 10, 12, 0);
            this.pnlTopo.Size = new System.Drawing.Size(860, 45);
            this.pnlTopo.TabIndex = 2;
            //
            // chkMostrarConcluidas
            //
            this.chkMostrarConcluidas.AutoSize = true;
            this.chkMostrarConcluidas.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkMostrarConcluidas.Location = new System.Drawing.Point(702, 10);
            this.chkMostrarConcluidas.Name = "chkMostrarConcluidas";
            this.chkMostrarConcluidas.Size = new System.Drawing.Size(146, 35);
            this.chkMostrarConcluidas.TabIndex = 0;
            this.chkMostrarConcluidas.Text = "&Mostrar concluídas";
            this.chkMostrarConcluidas.UseVisualStyleBackColor = true;
            this.chkMostrarConcluidas.CheckedChanged += new System.EventHandler(this.chkMostrarConcluidas_CheckedChanged);
            //
            // lblCabecalho
            //
            this.lblCabecalho.AutoSize = true;
            this.lblCabecalho.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCabecalho.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblCabecalho.Location = new System.Drawing.Point(12, 10);
            this.lblCabecalho.Name = "lblCabecalho";
            this.lblCabecalho.Size = new System.Drawing.Size(156, 25);
            this.lblCabecalho.TabIndex = 1;
            this.lblCabecalho.Text = "Minhas tarefas";
            //
            // dgvTarefas
            //
            this.dgvTarefas.AllowUserToAddRows = false;
            this.dgvTarefas.AllowUserToDeleteRows = false;
            this.dgvTarefas.AllowUserToResizeRows = false;
            this.dgvTarefas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTarefas.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvTarefas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPosicao,
            this.colTitulo,
            this.colInicio,
            this.colFim,
            this.colSituacao});
            this.dgvTarefas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTarefas.Location = new System.Drawing.Point(0, 45);
            this.dgvTarefas.MultiSelect = false;
            this.dgvTarefas.Name = "dgvTarefas";
            this.dgvTarefas.ReadOnly = true;
            this.dgvTarefas.RowHeadersVisible = false;
            this.dgvTarefas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTarefas.Size = new System.Drawing.Size(710, 425);
            this.dgvTarefas.TabIndex = 0;
            this.dgvTarefas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTarefas_CellDoubleClick);
            this.dgvTarefas.SelectionChanged += new System.EventHandler(this.dgvTarefas_SelectionChanged);
            //
            // colPosicao
            //
            this.colPosicao.FillWeight = 8F;
            this.colPosicao.HeaderText = "#";
            this.colPosicao.Name = "colPosicao";
            this.colPosicao.ReadOnly = true;
            this.colPosicao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colTitulo
            //
            this.colTitulo.FillWeight = 45F;
            this.colTitulo.HeaderText = "Tarefa";
            this.colTitulo.Name = "colTitulo";
            this.colTitulo.ReadOnly = true;
            this.colTitulo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colInicio
            //
            this.colInicio.FillWeight = 15F;
            this.colInicio.HeaderText = "Início";
            this.colInicio.Name = "colInicio";
            this.colInicio.ReadOnly = true;
            this.colInicio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colFim
            //
            this.colFim.FillWeight = 15F;
            this.colFim.HeaderText = "Término";
            this.colFim.Name = "colFim";
            this.colFim.ReadOnly = true;
            this.colFim.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colSituacao
            //
            this.colSituacao.FillWeight = 17F;
            this.colSituacao.HeaderText = "Situação";
            this.colSituacao.Name = "colSituacao";
            this.colSituacao.ReadOnly = true;
            this.colSituacao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // pnlBotoes
            //
            this.pnlBotoes.Controls.Add(this.btnNova);
            this.pnlBotoes.Controls.Add(this.btnEditar);
            this.pnlBotoes.Controls.Add(this.btnConcluir);
            this.pnlBotoes.Controls.Add(this.btnSubir);
            this.pnlBotoes.Controls.Add(this.btnDescer);
            this.pnlBotoes.Controls.Add(this.btnExcluir);
            this.pnlBotoes.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBotoes.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlBotoes.Location = new System.Drawing.Point(710, 45);
            this.pnlBotoes.Name = "pnlBotoes";
            this.pnlBotoes.Padding = new System.Windows.Forms.Padding(10, 5, 10, 0);
            this.pnlBotoes.Size = new System.Drawing.Size(150, 425);
            this.pnlBotoes.TabIndex = 1;
            //
            // btnNova
            //
            this.btnNova.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnNova.Name = "btnNova";
            this.btnNova.Size = new System.Drawing.Size(125, 36);
            this.btnNova.TabIndex = 0;
            this.btnNova.Text = "&Nova tarefa";
            this.toolTip.SetToolTip(this.btnNova, "Ctrl+N");
            this.btnNova.UseVisualStyleBackColor = true;
            this.btnNova.Click += new System.EventHandler(this.btnNova_Click);
            //
            // btnEditar
            //
            this.btnEditar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(125, 36);
            this.btnEditar.TabIndex = 1;
            this.btnEditar.Text = "&Editar";
            this.toolTip.SetToolTip(this.btnEditar, "Enter ou F2");
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            //
            // btnConcluir
            //
            this.btnConcluir.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnConcluir.Name = "btnConcluir";
            this.btnConcluir.Size = new System.Drawing.Size(125, 36);
            this.btnConcluir.TabIndex = 2;
            this.btnConcluir.Text = "&Concluir ✔";
            this.toolTip.SetToolTip(this.btnConcluir, "Barra de espaço");
            this.btnConcluir.UseVisualStyleBackColor = true;
            this.btnConcluir.Click += new System.EventHandler(this.btnConcluir_Click);
            //
            // btnSubir
            //
            this.btnSubir.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnSubir.Name = "btnSubir";
            this.btnSubir.Size = new System.Drawing.Size(125, 36);
            this.btnSubir.TabIndex = 3;
            this.btnSubir.Text = "▲ &Subir";
            this.toolTip.SetToolTip(this.btnSubir, "Ctrl+↑");
            this.btnSubir.UseVisualStyleBackColor = true;
            this.btnSubir.Click += new System.EventHandler(this.btnSubir_Click);
            //
            // btnDescer
            //
            this.btnDescer.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnDescer.Name = "btnDescer";
            this.btnDescer.Size = new System.Drawing.Size(125, 36);
            this.btnDescer.TabIndex = 4;
            this.btnDescer.Text = "▼ &Descer";
            this.toolTip.SetToolTip(this.btnDescer, "Ctrl+↓");
            this.btnDescer.UseVisualStyleBackColor = true;
            this.btnDescer.Click += new System.EventHandler(this.btnDescer_Click);
            //
            // btnExcluir
            //
            this.btnExcluir.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(125, 36);
            this.btnExcluir.TabIndex = 5;
            this.btnExcluir.Text = "E&xcluir";
            this.toolTip.SetToolTip(this.btnExcluir, "Delete");
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            //
            // lblLegenda
            //
            this.lblLegenda.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblLegenda.ForeColor = System.Drawing.Color.DimGray;
            this.lblLegenda.Location = new System.Drawing.Point(0, 470);
            this.lblLegenda.Name = "lblLegenda";
            this.lblLegenda.Padding = new System.Windows.Forms.Padding(12, 6, 0, 0);
            this.lblLegenda.Size = new System.Drawing.Size(860, 30);
            this.lblLegenda.TabIndex = 3;
            this.lblLegenda.Text = "Em vermelho: tarefas atrasadas.   Atalhos: Ctrl+N nova · Enter editar · Espaço concluir · Ctrl+↑/↓ mover · Del excluir · F5 atualizar";
            //
            // FormPrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 500);
            this.Controls.Add(this.dgvTarefas);
            this.Controls.Add(this.pnlBotoes);
            this.Controls.Add(this.pnlTopo);
            this.Controls.Add(this.lblLegenda);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Tarefas";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.pnlTopo.ResumeLayout(false);
            this.pnlTopo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarefas)).EndInit();
            this.pnlBotoes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopo;
        private System.Windows.Forms.Label lblCabecalho;
        private System.Windows.Forms.CheckBox chkMostrarConcluidas;
        private System.Windows.Forms.DataGridView dgvTarefas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPosicao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFim;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSituacao;
        private System.Windows.Forms.FlowLayoutPanel pnlBotoes;
        private System.Windows.Forms.Button btnNova;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnConcluir;
        private System.Windows.Forms.Button btnSubir;
        private System.Windows.Forms.Button btnDescer;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Label lblLegenda;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
