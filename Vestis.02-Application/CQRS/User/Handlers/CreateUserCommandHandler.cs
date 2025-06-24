using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestis._02_Application.CQRS.User.Commands;
using Vestis._02_Application.Services;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

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
            hasher.Hash(request.Password));

        user = await _repository.CreateAsync(user);
        
        return user;
    }
}
