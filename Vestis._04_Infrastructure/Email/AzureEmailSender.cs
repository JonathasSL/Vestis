using Azure.Communication.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Vestis._04_Infrastructure.Email.Interfaces;
using Vestis._04_Infrastructure.Email.Settings;

namespace Vestis._04_Infrastructure.Email;

public sealed class AzureEmailSender : IEmailSender
{
    private readonly EmailClient _Client;
    private readonly EmailSettings _settings;
    private readonly ILogger<AzureEmailSender> _logger;

    public AzureEmailSender(EmailSettings emailSettings, ILogger<AzureEmailSender> logger)
    {
        _settings = emailSettings;
        _Client = new EmailClient(_settings.Azure.ConnectionString);
        _logger = logger;
    }

    public async Task SendEmailAsync(EmailMessage message, CancellationToken cancellationToke)
    {
        var content = new EmailContent(message.Subject)
        {
            PlainText = message.IsHtml ? null : message.Body,
            Html = message.IsHtml ? message.Body : null
        };

        var recipients = new EmailRecipients(
                message.To.Select(email => new EmailAddress(email.Trim())),
                message.Cc?.Select(email => new EmailAddress(email.Trim())),
                message.Bcc?.Select(email => new EmailAddress(email.Trim()))
            );
        
        var emailMessage = new Azure.Communication.Email.EmailMessage(
            _settings.SenderAddress,
            recipients,
            content
        );

        var operation = await _Client.SendAsync(
                Azure.WaitUntil.Completed,
                emailMessage,
                cancellationToke
            );

        // implement logging for email failiure
        if (operation.HasCompleted && operation.Value.Status == EmailSendStatus.Failed)
        {
            // Log the failure details here
            var errorMessage = $"Failed to send email. Operation ID: {operation.Id}, Status: {operation.Value.Status}";
            // You can use your preferred logging framework to log this error message
            _logger.LogError(errorMessage, operation.GetRawResponse().ReasonPhrase, operation.GetRawResponse().Content);
        }   
    }
}
