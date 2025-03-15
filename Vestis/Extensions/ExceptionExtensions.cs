using Humanizer;

namespace Vestis.Extensions;

public static class ExceptionExtensions
{
    public static string ExceptionStack(this Exception exception, out int sequence)
    {
        sequence = 0;
        if (exception.InnerException == null)
        {
            sequence = 1;
            return $"[{sequence.Ordinalize()}] {exception.Message}";
        }

        var stackedMessages = exception.InnerException.ExceptionStack(out sequence);
        sequence++;

        return $"{stackedMessages}\n\n[{sequence.Ordinalize()}] {exception.Message}\n";
    }
}
