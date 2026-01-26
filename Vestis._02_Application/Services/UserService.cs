using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using Vestis._02_Application.Common;
using Vestis._02_Application.CQRS.User.Commands;
using Vestis._02_Application.Models;
using Vestis._02_Application.Models.Auth;
using Vestis._02_Application.Services.Interfaces;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

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

    public async Task<UserModel> Create(RegisterDTO model)
    {
        var command = CreateCommand(model);
        var createdUser = await _mediator.Send(command);

        var responseModel = _mapper.Map<UserModel>(createdUser);
        return responseModel;

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

    [Obsolete("Método usado apenas para demonstração e testes.")]
    public async Task<UserModel> GetUserAsync()
    {
        const string url = "https://randomuser.me/api/?results=1";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        // Desserializa parcialmente (não precisa mapear tudo)
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;
        var user = root.GetProperty("results")[0];

        var id = user.GetProperty("login").GetProperty("uuid").GetString() ?? Guid.NewGuid().ToString();
        var firstName = user.GetProperty("name").GetProperty("first").GetString() ?? string.Empty;
        var lastName = user.GetProperty("name").GetProperty("last").GetString() ?? string.Empty;
        var email = user.GetProperty("email").GetString() ?? string.Empty;

        string profileImg = string.Empty;
        if (user.TryGetProperty("picture", out var picture))
        {
            profileImg =
                picture.GetProperty("large").GetString()
                ?? picture.GetProperty("medium").GetString()
                ?? picture.GetProperty("thumbnail").GetString()
                ?? string.Empty;
        }

        return new UserModel
        {
            Id = new Guid(id),
            Name = $"{firstName} {lastName}".Trim(),
            Role = email,
            ProfileImg = profileImg
        };
    }
}