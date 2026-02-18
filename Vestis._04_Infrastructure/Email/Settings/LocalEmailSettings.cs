namespace Vestis._04_Infrastructure.Email.Settings;

public sealed class LocalEmailSettings
{
    public string Host { get; set; } = default!;
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
}
