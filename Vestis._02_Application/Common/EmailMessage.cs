namespace Vestis._02_Application.Common;

public class EmailMessage
{
    public IReadOnlyCollection<string> ToEmail { get; private set; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public bool IsHtml { get; private set; }

    public EmailMessage(IEnumerable<string> toEmail, string subject, string body, bool isHtml = false)
    {
        ToEmail = toEmail.ToList().AsReadOnly();
        Subject = subject;
        Body = body;
        IsHtml = isHtml;
    }
}
