using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using Vestis._04_Infrastructure.Email.Interfaces;
using Vestis._04_Infrastructure.Email.Settings;

namespace Vestis._04_Infrastructure.Email;

public class LocalEmailSender : IEmailSender
{
    private readonly LocalEmailSettings _settings;
    private readonly ILogger<LocalEmailSender> _logger;
    private readonly MailboxAddress _from;

    public LocalEmailSender(LocalEmailSettings settings, ILogger<LocalEmailSender> logger)
    {
        _settings = settings;
        _logger = logger;
        _from = new MailboxAddress("Vestis", "no-reply@vestis.com");
    }

    public async Task SendEmailAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        var email = new MimeMessage();
        
        email.From.Add(_from);

        email.To.AddRange(message.To.Select(address => MailboxAddress.Parse(address)));
        email.Cc.AddRange(message.Cc?.Select(address => MailboxAddress.Parse(address)) ?? Array.Empty<MailboxAddress>());
        email.Bcc.AddRange(message.Bcc?.Select(address => MailboxAddress.Parse(address)) ?? Array.Empty<MailboxAddress>());

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = message.IsHtml ? message.Body : null,
            TextBody = message.IsHtml ? null : message.Body
        };

        email.Body = bodyBuilder.ToMessageBody();

        var email2 = new MimeMessage(_from, message.To.Select(address => MailboxAddress.Parse(address)), bodyBuilder.ToMessageBody());

        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.Host, _settings.Port);
            await client.SendAsync(email, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to send email to {Recipients}", string.Join(", ", message.To));
        }
    }
}
