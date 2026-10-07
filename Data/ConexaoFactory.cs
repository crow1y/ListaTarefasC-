using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace ListaTarefas.Data;

// Cria as conexões com o banco usando a connection string do appsettings.json.
// Nenhuma credencial ou caminho fica "fixo" (hardcoded) no código.
public static class ConexaoFactory
{
    private static string? _connectionString;

    // Chamado uma vez no início do programa (Program.cs)
    public static void Configurar(IConfiguration configuracao)
    {
        string? cs = configuracao.GetConnectionString("BancoTarefas");
        if (string.IsNullOrWhiteSpace(cs))
            throw new InvalidOperationException("Connection string 'BancoTarefas' não encontrada no appsettings.json.");

        // Se o caminho do arquivo .db for relativo, o banco fica na pasta do executável
        var builder = new SqliteConnectionStringBuilder(cs);
        if (!Path.IsPathRooted(builder.DataSource))
            builder.DataSource = Path.Combine(AppContext.BaseDirectory, builder.DataSource);

        _connectionString = builder.ToString();
    }

    // Sempre devolve uma conexão NOVA. Quem chama usa "using" para fechá-la.
    public static SqliteConnection CriarConexao()
    {
        if (_connectionString is null)
            throw new InvalidOperationException("ConexaoFactory não foi configurada.");

        return new SqliteConnection(_connectionString);
    }
}
