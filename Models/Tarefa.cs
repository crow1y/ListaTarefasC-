namespace ListaTarefas.Models;

// Representa uma tarefa da lista (espelha a tabela Tarefas do banco)
public class Tarefa
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }

    // Posição na lista: quanto menor, mais no alto (mais importante)
    public int Ordem { get; set; }

    public bool Concluida { get; set; }
    public DateTime? DataConclusao { get; set; }

    // Regra: está atrasada se não foi concluída e a data de fim já passou
    public bool EstaAtrasada =>
        !Concluida && DataFim.HasValue && DataFim.Value.Date < DateTime.Today;
}
