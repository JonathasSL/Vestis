using Microsoft.IdentityModel.Tokens;

namespace Vestis._02_Application.Common;

public class CommandResult<T>
{
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public string? Message { get; private set; }
    public List<string> Errors { get; private set; }

    public static CommandResult<T> Success(T data, string? message = null)
    {
        return new CommandResult<T>
        {
            IsSuccess = true,
            Data = data,
            Message = message,
            Errors = new List<string>()
        };
    }

    public static CommandResult<T> Failure(string message, List<string> errors = null)
    {
        return new CommandResult<T>
        {
            IsSuccess = false,
            Data = default,
            Message = message,
            Errors = errors ?? new List<string>() { message }
        };
    }
}

