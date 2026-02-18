namespace Vestis._04_Infrastructure.Email.Settings;

public sealed class EmailSettings
{
    public string Provider { get; set; } = default!;
    public string SenderAddress { get; set; } = default!;
    public string SenderName { get; set; } = default!;

    public AzureEmailSettings Azure { get; set; } = default!;
}
