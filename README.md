# Lista de Tarefas — C# WinForms + SQLite

Sistema "to-do" desktop: cadastra tarefas com datas opcionais, marca como concluída,
reordena por importância e destaca em vermelho as tarefas atrasadas.

---

## 1. O que instalar (uma vez só)

1. **Visual Studio 2022** (versão 17.8 ou mais nova — a Community é gratuita).
2. No instalador do Visual Studio, marque a carga de trabalho
   **"Desenvolvimento para desktop com .NET"** (já inclui o .NET 8).

> Para conferir/adicionar depois: abra o **Visual Studio Installer** → **Modificar**.

Não precisa instalar banco de dados: o SQLite é só um arquivo (`tarefas.db`),
criado automaticamente na primeira execução.

## 2. Como rodar

1. Descompacte o ZIP.
2. Dê dois cliques em **`ListaTarefas.sln`** (abre no Visual Studio).
3. Aperte **F5** (ou o botão verde ▶ **ListaTarefas**).

Na primeira vez o Visual Studio baixa os pacotes NuGet sozinho (precisa de internet).
Se aparecer erro de pacote, clique com o botão direito na **Solução** →
**Restaurar Pacotes NuGet**, e depois F5 de novo.

## 2.1 Editando as telas no modo Design

Cada tela tem dois arquivos:

| Arquivo | Para que serve |
|---|---|
| `FormPrincipal.cs` | Código da tela (o que acontece ao clicar etc.) |
| `FormPrincipal.Designer.cs` | Controles da tela (gerado pelo Designer — **não edite à mão**) |

- **Shift+F7** (ou dois cliques no `FormPrincipal.cs`) → abre o **modo Design** (arrastar e soltar).
- **F7** → volta para o código.
- Para ver/ajustar a ordem do Tab: no modo Design, menu **Exibir → Ordem de Tabulação**.

## 3. Como usar

| Ação | Botão | Atalho |
|---|---|---|
| Nova tarefa | Nova tarefa | **Ctrl+N** ou Alt+N |
| Editar | Editar | **Enter**, **F2** ou duplo clique |
| Concluir | Concluir ✔ | **Espaço** |
| Subir na lista | ▲ Subir | **Ctrl+↑** |
| Descer na lista | ▼ Descer | **Ctrl+↓** |
| Excluir | Excluir | **Delete** |
| Atualizar lista | — | **F5** |

Na tela de tarefa: **Enter** salva, **Esc** cancela. Datas no formato dd/mm/aaaa (opcionais).

---

## 4. Estrutura do projeto (camadas)

```
ListaTarefas/
├── ListaTarefas.sln        → Abra este arquivo no Visual Studio
├── Program.cs              → Ponto de entrada: lê o appsettings.json e abre a tela
├── appsettings.json        → Connection string (fora do código)
├── Models/
│   └── Tarefa.cs           → Classe que representa uma tarefa
├── Forms/                  → CAMADA DE TELA (só interface, nada de SQL)
│   ├── FormPrincipal.cs (+ .Designer.cs)  → Lista de tarefas
│   └── FormTarefa.cs    (+ .Designer.cs)  → Incluir / editar tarefa
├── Services/               → CAMADA DE REGRAS DE NEGÓCIO
│   ├── TarefaService.cs    → Valida regras e converte erros do banco em mensagens amigáveis
│   └── LogErros.cs         → Grava detalhes técnicos de erros em erros.log
├── Data/                   → CAMADA DE ACESSO A DADOS (único lugar com SQL)
│   ├── ConexaoFactory.cs   → Cria conexões a partir do appsettings.json
│   ├── BancoDeDados.cs     → Cria a tabela na primeira execução
│   └── TarefaRepository.cs → SELECT / INSERT / UPDATE / DELETE
└── Exceptions/
    ├── RegraNegocioException.cs
    └── DadosException.cs
```

Fluxo: **Form → Service → Repository → Banco**. O formulário nunca fala direto com o banco.

---

## 5. Como cada critério de avaliação foi atendido

**Arquitetura e Separação de Camadas**
- Forms só cuidam da tela; regras ficam em `TarefaService`; SQL só em `TarefaRepository`.

**Acesso a Dados e Persistência**
- Toda conexão usa `using` → é fechada sozinha, mesmo com erro.
- Todos os valores vão por parâmetros (`@titulo`, `@id`...) → proteção contra SQL Injection.
- **Transações** em operações que dependem uma da outra:
  - `Inserir`: busca a maior ordem + insere com ordem + 1.
  - `TrocarOrdem`: atualiza as duas tarefas (ou nenhuma).

**Validação e Integridade**
- Na tela (`FormTarefa`): `ErrorProvider` (ícone vermelho no campo), `MaskedTextBox`
  com máscara `00/00/0000` e conversão segura com `DateTime.TryParseExact`.
- No serviço (antes do banco): título obrigatório, tamanho máximo, término ≥ início,
  não concluir tarefa já concluída.

**Tratamento de Exceções**
- `SqliteException` é capturada no serviço e vira `DadosException` com mensagem amigável.
- Erros de regra viram `RegraNegocioException` (aviso amarelo).
- Nenhum `catch` vazio. A pilha de erros nunca aparece na tela: vai para `erros.log`.
- Erros inesperados são tratados em `Program.cs` (`Application.ThreadException`).

**Segurança e Configuração**
- Connection string em `appsettings.json`, lida com `ConfigurationBuilder`.

**Ergonomia (UX Desktop)**
- `TabIndex` em ordem lógica, foco inicial definido (`ActiveControl`).
- Teclas de atalho (tabela acima) e letras sublinhadas com Alt (`&` no texto).
- `AcceptButton` (Enter) e `CancelButton` (Esc) na tela de cadastro.
- Tarefas atrasadas em vermelho/negrito; concluídas em cinza e riscadas.

**Qualidade do Código**
- Prefixos nos controles (`txt`, `mtb`, `btn`, `lbl`, `dgv`, `chk`), nomes em português,
  pastas por camada e comentários explicando cada parte.

---

## 6. Banco de dados

Tabela `Tarefas`:

| Coluna | Tipo | Observação |
|---|---|---|
| Id | INTEGER | Chave primária, automática |
| Titulo | TEXT | Obrigatório |
| Descricao | TEXT | Opcional |
| DataInicio | TEXT | Opcional (aaaa-mm-dd) |
| DataFim | TEXT | Opcional (aaaa-mm-dd) |
| Ordem | INTEGER | Posição na lista (menor = mais importante) |
| Concluida | INTEGER | 0 = não, 1 = sim |
| DataConclusao | TEXT | Preenchida ao concluir |

O arquivo `tarefas.db` fica em `bin\Debug\net8.0-windows\` (no Visual Studio: botão direito
no projeto → **Abrir Pasta no Gerenciador de Arquivos**). Para "zerar" os dados, é só apagá-lo.
