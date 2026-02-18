namespace Vestis._04_Infrastructure.Email.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(EmailMessage message, CancellationToken cancellationToken);
}
