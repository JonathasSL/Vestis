using MediatR;
using Vestis._02_Application.CQRS.User.Commands;
using Vestis._02_Application.Services;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.CQRS.User.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserEntity>
{
    private readonly IUserRepository _repository;

    public CreateUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
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
        
        return user;
    }
}
