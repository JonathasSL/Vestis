using MediatR;
using Vestis._02_Application.CQRS.User.Commands;
using Vestis._02_Application.CQRS.User.Events;
using Vestis._02_Application.Services;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.CQRS.User.Handlers;

public class UserCommandHandler : IRequestHandler<CreateUserCommand, UserEntity>
{
    private readonly IUserRepository _repository;
    private readonly IMediator _mediator;

    public UserCommandHandler(IUserRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<UserEntity> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var hasher = new PasswordHasher();
        var user = new UserEntity(
            request.Name,
            request.Email,
            hasher.Hash(request.Password),
            request.ProfileImg);

        user = await _repository.CreateAsync(user, cancellationToken);

        await _mediator.Publish(new UserRegisteredEvent(user.Id, user.Email), cancellationToken);

        return user;
    }
}
