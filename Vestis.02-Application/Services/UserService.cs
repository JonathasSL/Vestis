using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Vestis._02_Application.Common;
using Vestis._02_Application.CQRS.User.Commands;
using Vestis._02_Application.Models;
using Vestis._02_Application.Services.Interfaces;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._02_Application.Services;

public class UserService : CRUDService<UserModel, UserEntity, Guid>, IUserService
{
    private readonly IUserRepository _repository;
    private readonly JwtService _jwtService;

    public UserService(
        IUserRepository repository,
        IMapper mapper,
        IMediator mediator,
        BusinessNotificationContext businessNotificationContext,
        ILogger<UserService> logger,
        JwtService jwtService) : base(mapper, mediator, businessNotificationContext, logger, repository)
    {
        _repository = repository;
        _jwtService = jwtService;
    }

    public async Task<UserModel> Create(UserModel model)
    {
        var command = CreateCommand(model);
        var createdUser = await _mediator.Send(command);
        //var user = _mapper.Map<UserEntity>(model);
        //var createdUser = await _repository.CreateAsync(user);

        var responseModel = _mapper.Map<UserModel>(createdUser);
        return responseModel;

        CreateUserCommand CreateCommand(UserModel model)
        {
            if (model is null)
                return null;
            
            return new CreateUserCommand(
                model.Name,
                model.Email,
                model.Password);
        }
    }

    public override async Task<UserModel> Update(Guid id, UserModel model)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return null;

        _mapper.Map(model, entity);
        entity.ChangePassword(new PasswordHasher().Hash(model.Password));
        var result = await _repository.Update(entity);

        return _mapper.Map<UserModel>(result);
    }

    public async Task<bool> ExistsAsync(string email)
    {
        return await _repository.Exists(email);
    }

    public async Task<string> AuthenticateAsync(string email, string password)
    {
        var user = await _repository.GetByEmailAsync(email);

        if (user == null || !new PasswordHasher().Verify(password, user.Password))
            return null;

        return _jwtService.GenerateToken(user.Id.ToString(), user.Email);
    }
}