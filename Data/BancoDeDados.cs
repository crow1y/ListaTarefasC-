namespace ListaTarefas.Data;

// Cria a tabela na primeira vez que o programa roda
public static class BancoDeDados
{
    public static void Inicializar()
    {
        using var conexao = ConexaoFactory.CriarConexao();
        conexao.Open();

        using var comando = conexao.CreateCommand();
        comando.CommandText = @"
            CREATE TABLE IF NOT EXISTS Tarefas (
                Id            INTEGER PRIMARY KEY AUTOINCREMENT,
                Titulo        TEXT    NOT NULL,
                Descricao     TEXT    NULL,
                DataInicio    TEXT    NULL,
                DataFim       TEXT    NULL,
                Ordem         INTEGER NOT NULL,
                Concluida     INTEGER NOT NULL DEFAULT 0,
                DataConclusao TEXT    NULL
            );";
        comando.ExecuteNonQuery();
    }
}
