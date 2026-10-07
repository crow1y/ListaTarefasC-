namespace ListaTarefas.Exceptions;

// Erro de regra de negócio 
// A mensagem é amigável e pode ser mostrada direto ao usuário.
public class RegraNegocioException : Exception
{
    public RegraNegocioException(string mensagem) : base(mensagem) { }
}
