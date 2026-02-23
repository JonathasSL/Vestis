using MediatR;
using Vestis._02_Application.CQRS.User.Commands;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.CQRS.User.Handlers;

internal class UserVerificationCommandHandler : IRequestHandler<UserEmailVerificationCreationCommand, EmailVerificationTokenEntity>
{
    private readonly IEmailVerificationTokenEntityRepository _repository;

    public UserVerificationCommandHandler(IEmailVerificationTokenEntityRepository repository)
    {
        _repository = repository;
    }

    public async Task<EmailVerificationTokenEntity> Handle(UserEmailVerificationCreationCommand request, CancellationToken cancellationToken)
    {
        var token = new EmailVerificationTokenEntity(
            request.UserId,
            request.Code,
            DateTime.UtcNow.AddMinutes(30)
        );

        var entity = await _repository.CreateAsync(token, cancellationToken);
        return entity;
    }
}
