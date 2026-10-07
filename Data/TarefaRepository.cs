using System.Globalization;
using ListaTarefas.Models;
using Microsoft.Data.Sqlite;

namespace ListaTarefas.Data;

// Camada de acesso a dados: é o ÚNICO lugar do projeto que tem SQL.
// - Toda conexão é aberta com "using" (fecha sozinha, mesmo se der erro)
// - Todo valor vindo do usuário vai por PARÂMETRO (@nome), nunca concatenado
//   na string do SQL. Isso evita SQL Injection.
public class TarefaRepository
{
    // Formato usado para guardar datas como texto no SQLite
    private const string FormatoData = "yyyy-MM-dd";

    public List<Tarefa> Listar(bool incluirConcluidas)
    {
        var tarefas = new List<Tarefa>();

        using var conexao = ConexaoFactory.CriarConexao();
        conexao.Open();

        using var comando = conexao.CreateCommand();
        comando.CommandText = @"
            SELECT Id, Titulo, Descricao, DataInicio, DataFim, Ordem, Concluida, DataConclusao
            FROM Tarefas
            WHERE Concluida = 0 OR @incluirConcluidas = 1
            ORDER BY Concluida, Ordem;";
        comando.Parameters.AddWithValue("@incluirConcluidas", incluirConcluidas ? 1 : 0);

        using var leitor = comando.ExecuteReader();
        while (leitor.Read())
            tarefas.Add(LerTarefa(leitor));

        return tarefas;
    }

    public Tarefa? ObterPorId(int id)
    {
        using var conexao = ConexaoFactory.CriarConexao();
        conexao.Open();

        using var comando = conexao.CreateCommand();
        comando.CommandText = @"
            SELECT Id, Titulo, Descricao, DataInicio, DataFim, Ordem, Concluida, DataConclusao
            FROM Tarefas WHERE Id = @id;";
        comando.Parameters.AddWithValue("@id", id);

        using var leitor = comando.ExecuteReader();
        return leitor.Read() ? LerTarefa(leitor) : null;
    }

    // Insere no FINAL da lista. Usa transação porque são 2 comandos dependentes:
    // descobrir a maior Ordem e inserir com Ordem + 1.
    public int Inserir(Tarefa tarefa)
    {
        using var conexao = ConexaoFactory.CriarConexao();
        conexao.Open();
        using var transacao = conexao.BeginTransaction();

        using var cmdOrdem = conexao.CreateCommand();
        cmdOrdem.Transaction = transacao;
        cmdOrdem.CommandText = "SELECT COALESCE(MAX(Ordem), 0) + 1 FROM Tarefas;";
        int proximaOrdem = Convert.ToInt32(cmdOrdem.ExecuteScalar());

        using var cmdInserir = conexao.CreateCommand();
        cmdInserir.Transaction = transacao;
        cmdInserir.CommandText = @"
            INSERT INTO Tarefas (Titulo, Descricao, DataInicio, DataFim, Ordem, Concluida)
            VALUES (@titulo, @descricao, @dataInicio, @dataFim, @ordem, 0);
            SELECT last_insert_rowid();";
        cmdInserir.Parameters.AddWithValue("@titulo", tarefa.Titulo);
        cmdInserir.Parameters.AddWithValue("@descricao", (object?)tarefa.Descricao ?? DBNull.Value);
        cmdInserir.Parameters.AddWithValue("@dataInicio", DataParaBanco(tarefa.DataInicio));
        cmdInserir.Parameters.AddWithValue("@dataFim", DataParaBanco(tarefa.DataFim));
        cmdInserir.Parameters.AddWithValue("@ordem", proximaOrdem);
        int novoId = Convert.ToInt32(cmdInserir.ExecuteScalar());

        transacao.Commit(); // só grava se os dois comandos deram certo
        return novoId;
    }

    public void Atualizar(Tarefa tarefa)
    {
        using var conexao = ConexaoFactory.CriarConexao();
        conexao.Open();

        using var comando = conexao.CreateCommand();
        comando.CommandText = @"
            UPDATE Tarefas
            SET Titulo = @titulo, Descricao = @descricao,
                DataInicio = @dataInicio, DataFim = @dataFim
            WHERE Id = @id;";
        comando.Parameters.AddWithValue("@titulo", tarefa.Titulo);
        comando.Parameters.AddWithValue("@descricao", (object?)tarefa.Descricao ?? DBNull.Value);
        comando.Parameters.AddWithValue("@dataInicio", DataParaBanco(tarefa.DataInicio));
        comando.Parameters.AddWithValue("@dataFim", DataParaBanco(tarefa.DataFim));
        comando.Parameters.AddWithValue("@id", tarefa.Id);
        comando.ExecuteNonQuery();
    }

    public void MarcarComoConcluida(int id, DateTime dataConclusao)
    {
        using var conexao = ConexaoFactory.CriarConexao();
        conexao.Open();

        using var comando = conexao.CreateCommand();
        comando.CommandText = @"
            UPDATE Tarefas SET Concluida = 1, DataConclusao = @dataConclusao
            WHERE Id = @id;";
        comando.Parameters.AddWithValue("@dataConclusao", DataParaBanco(dataConclusao));
        comando.Parameters.AddWithValue("@id", id);
        comando.ExecuteNonQuery();
    }

    public void Excluir(int id)
    {
        using var conexao = ConexaoFactory.CriarConexao();
        conexao.Open();

        using var comando = conexao.CreateCommand();
        comando.CommandText = "DELETE FROM Tarefas WHERE Id = @id;";
        comando.Parameters.AddWithValue("@id", id);
        comando.ExecuteNonQuery();
    }

    // Troca a posição de duas tarefas na lista.
    // Usa TRANSAÇÃO: ou as duas são atualizadas, ou nenhuma (evita ordem duplicada).
    public void TrocarOrdem(int idTarefaA, int idTarefaB)
    {
        using var conexao = ConexaoFactory.CriarConexao();
        conexao.Open();
        using var transacao = conexao.BeginTransaction();

        int ordemA = BuscarOrdem(conexao, transacao, idTarefaA);
        int ordemB = BuscarOrdem(conexao, transacao, idTarefaB);

        AtualizarOrdem(conexao, transacao, idTarefaA, ordemB);
        AtualizarOrdem(conexao, transacao, idTarefaB, ordemA);

        transacao.Commit();
    }

    // ---------- Métodos auxiliares ----------

    private static int BuscarOrdem(SqliteConnection conexao, SqliteTransaction transacao, int id)
    {
        using var comando = conexao.CreateCommand();
        comando.Transaction = transacao;
        comando.CommandText = "SELECT Ordem FROM Tarefas WHERE Id = @id;";
        comando.Parameters.AddWithValue("@id", id);

        object? resultado = comando.ExecuteScalar();
        if (resultado is null)
            throw new InvalidOperationException($"Tarefa {id} não encontrada.");

        return Convert.ToInt32(resultado);
    }

    private static void AtualizarOrdem(SqliteConnection conexao, SqliteTransaction transacao, int id, int novaOrdem)
    {
        using var comando = conexao.CreateCommand();
        comando.Transaction = transacao;
        comando.CommandText = "UPDATE Tarefas SET Ordem = @ordem WHERE Id = @id;";
        comando.Parameters.AddWithValue("@ordem", novaOrdem);
        comando.Parameters.AddWithValue("@id", id);
        comando.ExecuteNonQuery();
    }

    // Converte uma linha do banco em um objeto Tarefa
    private static Tarefa LerTarefa(SqliteDataReader leitor)
    {
        return new Tarefa
        {
            Id = leitor.GetInt32(leitor.GetOrdinal("Id")),
            Titulo = leitor.GetString(leitor.GetOrdinal("Titulo")),
            Descricao = LerTextoOuNulo(leitor, "Descricao"),
            DataInicio = DataDoBanco(LerTextoOuNulo(leitor, "DataInicio")),
            DataFim = DataDoBanco(LerTextoOuNulo(leitor, "DataFim")),
            Ordem = leitor.GetInt32(leitor.GetOrdinal("Ordem")),
            Concluida = leitor.GetInt32(leitor.GetOrdinal("Concluida")) == 1,
            DataConclusao = DataDoBanco(LerTextoOuNulo(leitor, "DataConclusao"))
        };
    }

    private static string? LerTextoOuNulo(SqliteDataReader leitor, string coluna)
    {
        int indice = leitor.GetOrdinal(coluna);
        return leitor.IsDBNull(indice) ? null : leitor.GetString(indice);
    }

    private static object DataParaBanco(DateTime? data) =>
        data.HasValue ? data.Value.ToString(FormatoData, CultureInfo.InvariantCulture) : DBNull.Value;

    private static DateTime? DataDoBanco(string? texto) =>
        DateTime.TryParseExact(texto, FormatoData, CultureInfo.InvariantCulture, DateTimeStyles.None, out var data)
            ? data
            : null;
}
