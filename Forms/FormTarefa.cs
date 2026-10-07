using System.Globalization;
using ListaTarefas.Exceptions;
using ListaTarefas.Models;
using ListaTarefas.Services;

namespace ListaTarefas.Forms;

// Tela para incluir ou editar uma tarefa.
// Aqui só tem código de TELA: ler campos, validar formato, mostrar mensagens.
// Regras de negócio e banco ficam no TarefaService.
// Os controles estão em FormTarefa.Designer.cs (editável no modo Design).
public partial class FormTarefa : Form
{
    private static readonly CultureInfo CulturaBr = CultureInfo.GetCultureInfo("pt-BR");

    private readonly TarefaService _servico;
    private readonly Tarefa _tarefa;

    // tarefaParaEditar = null significa "nova tarefa"
    public FormTarefa(TarefaService servico, Tarefa? tarefaParaEditar = null)
    {
        InitializeComponent();

        _servico = servico;
        _tarefa = tarefaParaEditar ?? new Tarefa();

        Text = _tarefa.Id == 0 ? "Nova tarefa" : "Editar tarefa";
        txtTitulo.MaxLength = TarefaService.TamanhoMaximoTitulo;
        txtDescricao.MaxLength = TarefaService.TamanhoMaximoDescricao;

        PreencherCampos();
        ActiveControl = txtTitulo; // foco inicial no título
    }

    // Mostra os dados da tarefa (quando é edição)
    private void PreencherCampos()
    {
        txtTitulo.Text = _tarefa.Titulo;
        txtDescricao.Text = _tarefa.Descricao ?? string.Empty;
        mtbDataInicio.Text = _tarefa.DataInicio?.ToString("dd/MM/yyyy", CulturaBr) ?? string.Empty;
        mtbDataFim.Text = _tarefa.DataFim?.ToString("dd/MM/yyyy", CulturaBr) ?? string.Empty;
    }

    private void btnSalvar_Click(object? sender, EventArgs e)
    {
        // 1) Validação de FORMATO (na tela)
        if (!ValidarCampos(out DateTime? dataInicio, out DateTime? dataFim))
            return;

        // 2) Monta o objeto e manda para o serviço (que valida as REGRAS de negócio)
        _tarefa.Titulo = txtTitulo.Text.Trim();
        _tarefa.Descricao = string.IsNullOrWhiteSpace(txtDescricao.Text) ? null : txtDescricao.Text.Trim();
        _tarefa.DataInicio = dataInicio;
        _tarefa.DataFim = dataFim;

        try
        {
            _servico.Salvar(_tarefa);
            DialogResult = DialogResult.OK; // fecha a tela avisando que salvou
        }
        catch (RegraNegocioException ex)
        {
            MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (DadosException ex)
        {
            LogErros.Registrar(ex);
            MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // Confere se os campos estão preenchidos no formato certo.
    // Retorna false (e mostra o ErrorProvider) se algo estiver errado.
    private bool ValidarCampos(out DateTime? dataInicio, out DateTime? dataFim)
    {
        errorProvider.Clear();
        bool valido = true;

        if (string.IsNullOrWhiteSpace(txtTitulo.Text))
        {
            errorProvider.SetError(txtTitulo, "O título é obrigatório.");
            valido = false;
        }

        if (!TentarLerData(mtbDataInicio, out dataInicio))
        {
            errorProvider.SetError(mtbDataInicio, "Data inválida. Use dd/mm/aaaa.");
            valido = false;
        }

        if (!TentarLerData(mtbDataFim, out dataFim))
        {
            errorProvider.SetError(mtbDataFim, "Data inválida. Use dd/mm/aaaa.");
            valido = false;
        }
        else if (dataInicio.HasValue && dataFim.HasValue && dataFim < dataInicio)
        {
            errorProvider.SetError(mtbDataFim, "O término não pode ser antes do início.");
            valido = false;
        }

        if (!valido)
            FocarPrimeiroCampoComErro();

        return valido;
    }

    // Conversão SEGURA com TryParseExact: não lança erro se a data for inválida (ex.: 31/02/2026)
    private static bool TentarLerData(MaskedTextBox campo, out DateTime? data)
    {
        data = null;

        // Campo vazio = sem data (é permitido)
        string somenteNumeros = campo.Text.Replace("/", "").Trim();
        if (somenteNumeros.Length == 0)
            return true;

        if (DateTime.TryParseExact(campo.Text, "dd/MM/yyyy", CulturaBr, DateTimeStyles.None, out DateTime convertida))
        {
            data = convertida;
            return true;
        }

        return false;
    }

    private void FocarPrimeiroCampoComErro()
    {
        Control[] ordem = { txtTitulo, mtbDataInicio, mtbDataFim };
        Control? comErro = ordem.FirstOrDefault(c => errorProvider.GetError(c).Length > 0);
        comErro?.Focus();
    }
}
