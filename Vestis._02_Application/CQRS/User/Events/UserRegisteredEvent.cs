using MediatR;

namespace Vestis._02_Application.CQRS.User.Events;

public sealed record UserRegisteredEvent(
    string Email,
    string VerificationCode) 
    : INotification;
