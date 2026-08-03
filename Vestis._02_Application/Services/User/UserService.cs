using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using Vestis._02_Application.Common;
using Vestis._02_Application.CQRS.User.Commands;
using Vestis._02_Application.Models;
using Vestis._02_Application.Models.Auth;
using Vestis._02_Application.Services.Interfaces.User;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.Services;

public class UserService : CRUDService<UserModel, UserEntity, Guid>, IUserService
{
    private readonly IUserRepository _repository;
    private readonly JwtService _jwtService;
    private readonly HttpClient _httpClient;

    public UserService(
        IUserRepository repository,
        IMapper mapper,
        IMediator mediator,
        BusinessNotificationContext businessNotificationContext,
        ILogger<UserService> logger,
        JwtService jwtService,
        HttpClient httpClient) : base(mapper, mediator, businessNotificationContext, logger, repository)
    {
        _repository = repository;
        _jwtService = jwtService;
        _httpClient = httpClient;
    }

    public async Task<CommandResult<UserModel>> Create(RegisterDTO model)
    {
        if (await _repository.Exists(model.Email))
            return CommandResult<UserModel>.Failure("Já existe um usuário cadastrado com este email.");

        var command = CreateCommand(model);
        var createdUser = await _mediator.Send(command);

        if (_businessNotificationContext.HasNotifications) {
            return CommandResult<UserModel>.Failure("Não foi possível criar o usuário.", _businessNotificationContext.Notifications.ToList());
        }

        var responseModel = _mapper.Map<UserModel>(createdUser);
        return CommandResult<UserModel>.Success(responseModel);

        CreateUserCommand CreateCommand(RegisterDTO model)
        {
            if (model is null)
                return null;
            
            return new CreateUserCommand(
                model.Name,
                model.Email,
                model.Password,
                model.ProfileImg);
        }
    }

    public override async Task<CommandResult<UserModel>> Update(Guid id, UserModel model)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return CommandResult<UserModel>.NotFound("Usuário não encontrado.");

        _mapper.Map(model, entity);
        entity.ChangePassword(new PasswordHasher().Hash(model.Password));
        var result = await _repository.Update(entity);

        return CommandResult<UserModel>.Success(_mapper.Map<UserModel>(result));
    }

    public async Task<CommandResult<string>> AuthenticateAsync(string email, string password)
    {
        var user = await _repository.GetByEmailAsync(email);

        if (user == null || !new PasswordHasher().Verify(password, user.Password))
            return CommandResult<string>.Failure("Credenciais inválidas.");

        return CommandResult<string>.Success(_jwtService.GenerateToken(user.Id.ToString(), user.Email));
    }

    [Obsolete("Método usado apenas para demonstração e testes.")]
    public async Task<CommandResult<List<UserModel>>> GetTestUserAsync(int count)
    {
        var json = await requestUserData();
        var data = json.GetProperty("results").EnumerateArray().ToList();

        var users = new List<UserModel>();

        foreach (var user in data)
        {
            var id = user.GetProperty("login").GetProperty("uuid").GetString() ?? Guid.NewGuid().ToString();
            var firstName = user.GetProperty("name").GetProperty("first").GetString().Trim() ?? string.Empty;
            var lastName = user.GetProperty("name").GetProperty("last").GetString().Trim() ?? string.Empty;
            var email = user.GetProperty("email").GetString().Trim() ?? string.Empty;

            string profileImg = string.Empty;
            if (user.TryGetProperty("picture", out var picture))
            {
                profileImg =
                    picture.GetProperty("large").GetString()
                    ?? picture.GetProperty("medium").GetString()
                    ?? picture.GetProperty("thumbnail").GetString()
                    ?? string.Empty;
            }

            users.Add(new UserModel
            {
                Id = new Guid(id),
                Name = $"{firstName} {lastName}".Trim(),
                Role = email,
                ProfileImg = profileImg
            });
        }

        return CommandResult<List<UserModel>>.Success(users);

        async Task<JsonElement> requestUserData()
        {
            string url = $"https://randomuser.me/api/?results={count}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonDocument.Parse(json).RootElement;
        }
    }
}