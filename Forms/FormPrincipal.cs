using ListaTarefas.Exceptions;
using ListaTarefas.Models;
using ListaTarefas.Services;

namespace ListaTarefas.Forms;

// Tela inicial: lista as tarefas por fazer, em ordem de importância.
// Os controles (botões, grade...) estão em FormPrincipal.Designer.cs
// e podem ser editados pelo modo Design do Visual Studio.
public partial class FormPrincipal : Form
{
    private readonly TarefaService _servico;

    // Cores usadas no destaque
    private static readonly Color CorAtrasada = Color.FromArgb(255, 220, 220);
    private static readonly Color CorTextoAtrasada = Color.DarkRed;
    private static readonly Color CorTextoConcluida = Color.Gray;

    public FormPrincipal(TarefaService servico)
    {
        InitializeComponent();
        _servico = servico;
        ActiveControl = dgvTarefas; // foco inicial na lista
    }

    // Eventos da tela ligados no Designer

    private void FormPrincipal_Load(object? sender, EventArgs e) => CarregarTarefas();
    private void chkMostrarConcluidas_CheckedChanged(object? sender, EventArgs e) => CarregarTarefas();
    private void dgvTarefas_SelectionChanged(object? sender, EventArgs e) => AtualizarBotoes();

    private void dgvTarefas_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0) EditarTarefa();
    }

    private void btnNova_Click(object? sender, EventArgs e) => NovaTarefa();
    private void btnEditar_Click(object? sender, EventArgs e) => EditarTarefa();
    private void btnConcluir_Click(object? sender, EventArgs e) => ConcluirTarefa();
    private void btnSubir_Click(object? sender, EventArgs e) => MoverTarefa(-1);
    private void btnDescer_Click(object? sender, EventArgs e) => MoverTarefa(+1);
    private void btnExcluir_Click(object? sender, EventArgs e) => ExcluirTarefa();

    // Teclas de atalho da tela
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        bool listaComFoco = dgvTarefas.Focused;

        switch (keyData)
        {
            case Keys.Control | Keys.N: NovaTarefa(); return true;
            case Keys.F2: EditarTarefa(); return true;
            case Keys.F5: CarregarTarefas(); return true;
            case Keys.Control | Keys.Up: MoverTarefa(-1); return true;
            case Keys.Control | Keys.Down: MoverTarefa(+1); return true;
            case Keys.Enter when listaComFoco: EditarTarefa(); return true;
            case Keys.Space when listaComFoco: ConcluirTarefa(); return true;
            case Keys.Delete when listaComFoco: ExcluirTarefa(); return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    // Busca as tarefas e monta a grade. idParaSelecionar mantém a seleção após uma ação.
    private void CarregarTarefas(int? idParaSelecionar = null)
    {
        idParaSelecionar ??= TarefaSelecionada()?.Id;

        List<Tarefa> tarefas;
        try
        {
            tarefas = _servico.Listar(chkMostrarConcluidas.Checked);
        }
        catch (DadosException ex)
        {
            MostrarErroDeDados(ex);
            return;
        }

        dgvTarefas.Rows.Clear();
        int posicao = 1;
        foreach (Tarefa tarefa in tarefas)
        {
            int linha = dgvTarefas.Rows.Add(
                tarefa.Concluida ? "" : posicao++.ToString(),
                tarefa.Titulo,
                tarefa.DataInicio?.ToString("dd/MM/yyyy") ?? "-",
                tarefa.DataFim?.ToString("dd/MM/yyyy") ?? "-",
                DescreverSituacao(tarefa));

            DataGridViewRow row = dgvTarefas.Rows[linha];
            row.Tag = tarefa; // guarda o objeto na linha para usar depois
            AplicarDestaque(row, tarefa);
        }

        SelecionarTarefa(idParaSelecionar);
        AtualizarBotoes();
    }

    private void NovaTarefa()
    {
        using var form = new FormTarefa(_servico);
        if (form.ShowDialog(this) == DialogResult.OK)
            CarregarTarefas();
    }

    private void EditarTarefa()
    {
        Tarefa? tarefa = TarefaSelecionada();
        if (tarefa is null) return;

        using var form = new FormTarefa(_servico, tarefa);
        if (form.ShowDialog(this) == DialogResult.OK)
            CarregarTarefas(tarefa.Id);
    }

    private void ConcluirTarefa()
    {
        Tarefa? tarefa = TarefaSelecionada();
        if (tarefa is null || tarefa.Concluida) return;

        int? proximaParaSelecionar = IdDaTarefaVizinha();
        try
        {
            _servico.Concluir(tarefa.Id);
            CarregarTarefas(proximaParaSelecionar);
        }
        catch (RegraNegocioException ex) { MostrarAviso(ex.Message); }
        catch (DadosException ex) { MostrarErroDeDados(ex); }
    }

    private void ExcluirTarefa()
    {
        Tarefa? tarefa = TarefaSelecionada();
        if (tarefa is null) return;

        var resposta = MessageBox.Show($"Excluir a tarefa \"{tarefa.Titulo}\"?", "Confirmar exclusão",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        if (resposta != DialogResult.Yes) return;

        int? proximaParaSelecionar = IdDaTarefaVizinha();
        try
        {
            _servico.Excluir(tarefa.Id);
            CarregarTarefas(proximaParaSelecionar);
        }
        catch (DadosException ex) { MostrarErroDeDados(ex); }
    }

    // direcao: -1 = subir, +1 = descer
    private void MoverTarefa(int direcao)
    {
        if (dgvTarefas.CurrentRow is null) return;

        int indiceAtual = dgvTarefas.CurrentRow.Index;
        int indiceDestino = indiceAtual + direcao;
        if (indiceDestino < 0 || indiceDestino >= dgvTarefas.Rows.Count) return;

        var tarefa = (Tarefa)dgvTarefas.Rows[indiceAtual].Tag!;
        var vizinha = (Tarefa)dgvTarefas.Rows[indiceDestino].Tag!;
        if (tarefa.Concluida || vizinha.Concluida) return;

        try
        {
            _servico.TrocarPosicao(tarefa, vizinha);
            CarregarTarefas(tarefa.Id); // continua selecionada na nova posição
        }
        catch (RegraNegocioException ex) { MostrarAviso(ex.Message); }
        catch (DadosException ex) { MostrarErroDeDados(ex); }
    }

    // ---------- Auxiliares de tela ----------

    private Tarefa? TarefaSelecionada() => dgvTarefas.CurrentRow?.Tag as Tarefa;

    // Depois de concluir/excluir, seleciona a tarefa de baixo (ou a de cima, se era a última)
    private int? IdDaTarefaVizinha()
    {
        if (dgvTarefas.CurrentRow is null) return null;
        int indice = dgvTarefas.CurrentRow.Index;
        int vizinho = indice + 1 < dgvTarefas.Rows.Count ? indice + 1 : indice - 1;
        return vizinho >= 0 ? (dgvTarefas.Rows[vizinho].Tag as Tarefa)?.Id : null;
    }

    private void SelecionarTarefa(int? id)
    {
        if (dgvTarefas.Rows.Count == 0) return;

        DataGridViewRow alvo = dgvTarefas.Rows.Cast<DataGridViewRow>()
            .FirstOrDefault(r => (r.Tag as Tarefa)?.Id == id) ?? dgvTarefas.Rows[0];

        dgvTarefas.CurrentCell = alvo.Cells[0];
    }

    private void AtualizarBotoes()
    {
        Tarefa? tarefa = TarefaSelecionada();
        bool temSelecao = tarefa is not null;
        bool pendente = temSelecao && !tarefa!.Concluida;

        btnEditar.Enabled = temSelecao;
        btnExcluir.Enabled = temSelecao;
        btnConcluir.Enabled = pendente;
        btnSubir.Enabled = pendente;
        btnDescer.Enabled = pendente;
    }

    private static string DescreverSituacao(Tarefa tarefa)
    {
        if (tarefa.Concluida)
            return $"Concluída {tarefa.DataConclusao:dd/MM}";
        if (tarefa.EstaAtrasada)
            return "ATRASADA";
        return "Pendente";
    }

    // Tarefas atrasadas ficam em vermelho; concluídas ficam cinza e riscadas
    private void AplicarDestaque(DataGridViewRow row, Tarefa tarefa)
    {
        if (tarefa.EstaAtrasada)
        {
            row.DefaultCellStyle.BackColor = CorAtrasada;
            row.DefaultCellStyle.ForeColor = CorTextoAtrasada;
            row.DefaultCellStyle.Font = new Font(dgvTarefas.Font, FontStyle.Bold);
        }
        else if (tarefa.Concluida)
        {
            row.DefaultCellStyle.ForeColor = CorTextoConcluida;
            row.DefaultCellStyle.Font = new Font(dgvTarefas.Font, FontStyle.Strikeout);
        }
    }

    private static void MostrarAviso(string mensagem) =>
        MessageBox.Show(mensagem, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

    private static void MostrarErroDeDados(DadosException ex)
    {
        LogErros.Registrar(ex); // detalhes técnicos vão só para o arquivo de log
        MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
