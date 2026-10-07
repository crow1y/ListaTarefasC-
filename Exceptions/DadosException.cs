namespace ListaTarefas.Exceptions;

// Erro ao acessar o banco. Guarda o erro original (InnerException) para o log,
// mas a mensagem mostrada ao usuário é amigável, sem detalhes técnicos.
public class DadosException : Exception
{
    public DadosException(string mensagem, Exception erroOriginal) : base(mensagem, erroOriginal) { }
}
