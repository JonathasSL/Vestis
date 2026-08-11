using System.Text.Json.Serialization;

namespace Vestis._02_Application.Common;

public class CommandResult<T>
{
    public bool IsSuccess { get; private set; }

    [JsonIgnore]
    public CommandResultStatus Status { get; private set; }
    public T? Data { get; private set; }
    public IEnumerable<string> Messages { get; private set; }

    public static CommandResult<T> Success(T data, string? message = null)
    {
        return new CommandResult<T>
        {
            IsSuccess = true,
            Status = CommandResultStatus.Success,
            Data = data,
            Messages = new List<string>() { message  }
        };
    }

    public static CommandResult<T> Failure(IEnumerable<string> errors)
    {
        return new CommandResult<T>
        {
            IsSuccess = false,
            Status = CommandResultStatus.ValidationFailure,
            Data = default,
            Messages = errors ?? new List<string>() { }
        };
    }

    [Obsolete("Use Failure(IEnumerable<string> errors) instead.")]
    public static CommandResult<T> Failure(string message, IEnumerable<string> errors = null)
    {
        if (errors is null)
            return Failure(errors ?? new List<string>() { message });
        else
        {
            var errorList = new List<string>(errors);
            if (!string.IsNullOrEmpty(message))
                errorList.Insert(0, message);

            return Failure(errorList);
        }
    }

    public static CommandResult<T> NotFound(string message = null)
    {
        return new CommandResult<T>
        {
            IsSuccess = true,
            Status = CommandResultStatus.NotFound,
            Data = default,
            Messages = new List<string>() { "Não foi possível encontrar o recurso." }
        };
    }
}

public enum CommandResultStatus
{
    Success,
    ValidationFailure,
    NotFound
}