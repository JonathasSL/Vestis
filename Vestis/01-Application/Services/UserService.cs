using AutoMapper;
using Vestis._01_Application.Models;
using Vestis._01_Application.Services.Interfaces;
using Vestis._02_Domain.Repositories.Interfaces;
using Vestis.Entities;

namespace Vestis._01_Application.Services;

public class UserService : CRUDService<UserModel, UserEntity, Guid>, IUserService
{
    private readonly IUserRepository _repository;
    private readonly JwtService _jwtService;

    public UserService(
        IUserRepository repository,
        IMapper mapper,
        JwtService jwtService) : base(repository, mapper)
    {
        _repository = repository;
        _jwtService = jwtService;
    }

    public override async Task<UserModel> Create(UserModel model)
    {
        var user = _mapper.Map<UserEntity>(model);
        user.ChangePassword(new PasswordHasher().Hash(model.Password));
        var createdUser = await _repository.CreateAsync(user);
        
        var responseModel = _mapper.Map<UserModel>(createdUser);
        return responseModel;
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