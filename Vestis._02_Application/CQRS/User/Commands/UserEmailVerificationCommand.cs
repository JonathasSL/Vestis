using MediatR;

namespace Vestis._02_Application.CQRS.User.Commands;

public sealed record UserEmailVerificationCommand(
    string Email,
    string Code
) : IRequest<string?>;
