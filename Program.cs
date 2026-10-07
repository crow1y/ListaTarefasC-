using ListaTarefas.Data;
using ListaTarefas.Exceptions;
using ListaTarefas.Forms;
using ListaTarefas.Services;
using Microsoft.Extensions.Configuration;

namespace ListaTarefas;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Qualquer erro inesperado vira uma mensagem amigável (sem pilha de erros na tela)
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (s, e) => MostrarErroInesperado(e.Exception);

        var servico = new TarefaService(new TarefaRepository());

        try
        {
            // Lê a connection string do appsettings.json
            IConfiguration configuracao = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            ConexaoFactory.Configurar(configuracao);
            servico.PrepararBanco(); // cria a tabela se ainda não existir
        }
        catch (FileNotFoundException ex)
        {
            LogErros.Registrar(ex);
            MessageBox.Show("Arquivo de configuração (appsettings.json) não encontrado.",
                "Erro ao iniciar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        catch (InvalidOperationException ex)
        {
            LogErros.Registrar(ex);
            MessageBox.Show("A configuração do banco de dados está incompleta no appsettings.json.",
                "Erro ao iniciar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        catch (DadosException ex)
        {
            LogErros.Registrar(ex);
            MessageBox.Show(ex.Message, "Erro ao iniciar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Application.Run(new FormPrincipal(servico));
    }

    private static void MostrarErroInesperado(Exception erro)
    {
        LogErros.Registrar(erro);
        MessageBox.Show("Ocorreu um erro inesperado. Os detalhes foram salvos no arquivo erros.log.",
            "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
