using MediatR;

namespace Vestis._02_Application.CQRS.User.Events;

public sealed class UserRegisteredEvent : INotification
{
    public Guid UserId { get; }
    public string Email { get; }

    public UserRegisteredEvent(Guid userId, string email)
    {
        UserId = userId;
        Email = email;
    }
}

