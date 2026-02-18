namespace Vestis._04_Infrastructure.Email;

public class EmailMessage
{
    public string Subject { get; }
    public string Body { get; }
    public bool IsHtml { get; }

    public IReadOnlyCollection<string> To { get; }
    public IReadOnlyCollection<string>? Cc { get; }
    public IReadOnlyCollection<string>? Bcc { get; }

    public EmailMessage(
        string subject,
        string body,
        bool isHtml,
        IEnumerable<string> to,
        IEnumerable<string>? cc = null,
        IEnumerable<string>? bcc = null)
    {
        if (string.IsNullOrWhiteSpace(subject))
            throw new ArgumentException("Subject is required.");

        if (to == null || !to.Any())
            throw new ArgumentException("At least one recipient is required.");

        Subject = subject;
        Body = body;
        IsHtml = isHtml;
        To = to.Select(e => e.Trim()).ToList();
        Cc = cc?.Select(e => e.Trim()).ToList();
        Bcc = bcc?.Select(e => e.Trim()).ToList();
    }
}
