namespace ListaTarefas.Services;

// Guarda os detalhes técnicos dos erros em um arquivo (erros.log),
// para o usuário nunca ver a pilha de erros na tela.
public static class LogErros
{
    public static void Registrar(Exception erro)
    {
        try
        {
            string caminho = Path.Combine(AppContext.BaseDirectory, "erros.log");
            File.AppendAllText(caminho, $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] {erro}{Environment.NewLine}{Environment.NewLine}");
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            // Se nem o log puder ser gravado, não há o que fazer; o programa segue.
        }
    }
}
