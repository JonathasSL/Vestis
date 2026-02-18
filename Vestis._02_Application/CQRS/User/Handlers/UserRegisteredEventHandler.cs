using MediatR;
using Vestis._02_Application.CQRS.User.Events;
using Vestis._04_Infrastructure.Email;
using Vestis._04_Infrastructure.Email.Interfaces;

namespace Vestis._02_Application.CQRS.User.Handlers;

public class UserRegisteredEventHandler : INotificationHandler<UserRegisteredEvent>
{
    private readonly IEmailSender _emailSender;

    public UserRegisteredEventHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
    {
        var email = new EmailMessage(
            "Welcome to Vestis!",
            "Thank you for registering with Vestis. We're excited to have you on board! If you have any questions or need assistance, feel free to reach out to our support team.",
            false,
            new[] { notification.Email }
            );

        await _emailSender.SendEmailAsync(email, cancellationToken);
    }
}
