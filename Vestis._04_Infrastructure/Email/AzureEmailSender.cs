using Azure.Communication.Email;
using Vestis._04_Infrastructure.Email.Interfaces;

namespace Vestis._04_Infrastructure.Email;

public sealed class AzureEmailSender : IEmailSender
{
    private readonly EmailClient _Client;
    private readonly EmailSettings _settings;

    public AzureEmailSender(EmailSettings settings)
    {
        _settings = settings;
        _Client = new EmailClient(_settings.ConnectionString);
    }

    public async Task SendEmailAsync(EmailMessage email, CancellationToken cancellationToke)
    {
        var content = new EmailContent(email.Subject)
        {
            PlainText = email.IsHtml ? null : email.Body,
            Html = email.IsHtml ? email.Body : null
        };

        if (email.IsHtml)
            content.Html = email.Body;
        else
            content.PlainText = email.Body;

        var recipients = new EmailRecipients(
            email.ToEmail.Select(e => new EmailAddress(e)).ToList()
            );
        
        var emailMessage = new Azure.Communication.Email.EmailMessage(
            _settings.SenderAddress,
            recipients,
            content
        );

        await _Client.SendAsync(
                Azure.WaitUntil.Completed,
                emailMessage,
                cancellationToke
            );
    }
}
