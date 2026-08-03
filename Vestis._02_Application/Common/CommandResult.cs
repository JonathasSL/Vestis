namespace Vestis._02_Application.Common;

// TODO [Passo 1 - baixo impacto]: Criar enum CommandResultStatus { Success, ValidationFailure, NotFound }
// para diferenciar erro de usuario (400) de recurso nao encontrado (404). Nao criar status para erro 500:
// excecoes nao tratadas devem propagar e ser tratadas pelo GlobalExceptionHandler (ver Program.cs).
public class CommandResult<T>
{
    public bool IsSuccess { get; private set; }
    public CommandResultStatus Status { get; private set; }
    public T? Data { get; private set; }
    public string? Message { get; private set; }
    public List<string> Errors { get; private set; }

    public static CommandResult<T> Success(T data, string? message = null)
    {
        return new CommandResult<T>
        {
            IsSuccess = true,
            Status = CommandResultStatus.Success,
            Data = data,
            Message = message,
            Errors = new List<string>()
        };
    }

    // TODO: Ajustar para setar Status = CommandResultStatus.ValidationFailure ao criar o Failure.
    // Failure deve representar exclusivamente erro do usuario (campo obrigatorio vazio, regra de negocio violada, etc).
    public static CommandResult<T> Failure(string message, List<string> errors = null)
    {
        return new CommandResult<T>
        {
            IsSuccess = false,
            Status = CommandResultStatus.ValidationFailure,
            Data = default,
            Message = message,
            Errors = errors ?? new List<string>()
        };
    }

    public static CommandResult<T> NotFound(string message = null)
    {
        return new CommandResult<T>
        {
            IsSuccess = false,
            Status = CommandResultStatus.NotFound,
            Data = default,
            Message = message ?? "Não foi possível encontrar o recurso.",
            Errors = new List<string>()
        };
    }
}

public enum CommandResultStatus
{
    Success,
    ValidationFailure,
    NotFound
}