using MediatR;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.CQRS.User.Commands;

public sealed record UserEmailVerificationCreationCommand(
    Guid UserId,
    string Code
) : IRequest<EmailVerificationTokenEntity>;