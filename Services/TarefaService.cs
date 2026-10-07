using ListaTarefas.Data;
using ListaTarefas.Exceptions;
using ListaTarefas.Models;
using Microsoft.Data.Sqlite;

namespace ListaTarefas.Services;

// Camada de regras de negócio.
// Os formulários só conversam com esta classe — nunca com o banco direto.
public class TarefaService
{
    public const int TamanhoMaximoTitulo = 100;
    public const int TamanhoMaximoDescricao = 500;

    private readonly TarefaRepository _repositorio;

    public TarefaService(TarefaRepository repositorio)
    {
        _repositorio = repositorio;
    }

    public void PrepararBanco() =>
        ExecutarNoBanco(BancoDeDados.Inicializar);

    public List<Tarefa> Listar(bool incluirConcluidas) =>
        ExecutarNoBanco(() => _repositorio.Listar(incluirConcluidas));

    // Decide sozinho se é inclusão (Id = 0) ou alteração
    public void Salvar(Tarefa tarefa)
    {
        Validar(tarefa);

        ExecutarNoBanco(() =>
        {
            if (tarefa.Id == 0)
                tarefa.Id = _repositorio.Inserir(tarefa);
            else
                _repositorio.Atualizar(tarefa);
        });
    }

    public void Concluir(int idTarefa)
    {
        ExecutarNoBanco(() =>
        {
            Tarefa tarefa = _repositorio.ObterPorId(idTarefa)
                ?? throw new RegraNegocioException("A tarefa não existe mais.");

            if (tarefa.Concluida)
                throw new RegraNegocioException("Esta tarefa já está concluída.");

            _repositorio.MarcarComoConcluida(idTarefa, DateTime.Today);
        });
    }

    public void Excluir(int idTarefa) =>
        ExecutarNoBanco(() => _repositorio.Excluir(idTarefa));

    // Troca duas tarefas de lugar (usado pelos botões Subir/Descer)
    public void TrocarPosicao(Tarefa tarefaA, Tarefa tarefaB)
    {
        if (tarefaA.Concluida || tarefaB.Concluida)
            throw new RegraNegocioException("Só é possível reordenar tarefas pendentes.");

        ExecutarNoBanco(() => _repositorio.TrocarOrdem(tarefaA.Id, tarefaB.Id));
    }

    // Regras de negócio, conferidas ANTES de mandar ao banco
    private static void Validar(Tarefa tarefa)
    {
        if (string.IsNullOrWhiteSpace(tarefa.Titulo))
            throw new RegraNegocioException("Informe o título da tarefa.");

        if (tarefa.Titulo.Length > TamanhoMaximoTitulo)
            throw new RegraNegocioException($"O título pode ter no máximo {TamanhoMaximoTitulo} caracteres.");

        if (tarefa.Descricao?.Length > TamanhoMaximoDescricao)
            throw new RegraNegocioException($"A descrição pode ter no máximo {TamanhoMaximoDescricao} caracteres.");

        if (tarefa.DataInicio.HasValue && tarefa.DataFim.HasValue && tarefa.DataFim < tarefa.DataInicio)
            throw new RegraNegocioException("A data de término não pode ser anterior à data de início.");
    }

    // Executa uma ação no banco e troca o erro técnico (SqliteException)
    // por um erro com mensagem amigável. O erro original fica guardado para o log.
    private static void ExecutarNoBanco(Action acao)
    {
        try
        {
            acao();
        }
        catch (SqliteException ex)
        {
            throw new DadosException("Não foi possível acessar o banco de dados. Tente novamente.", ex);
        }
    }

    private static T ExecutarNoBanco<T>(Func<T> funcao)
    {
        try
        {
            return funcao();
        }
        catch (SqliteException ex)
        {
            throw new DadosException("Não foi possível acessar o banco de dados. Tente novamente.", ex);
        }
    }
}
