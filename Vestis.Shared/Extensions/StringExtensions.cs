namespace Vestis.Shared.Extensions;

public static class StringExtensions
{
    public static string? EmptyToNull(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;
        else
            return value;
    }
}
