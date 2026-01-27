using Vestis._02_Application.Models;
using Vestis._02_Application.Models.Auth;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Services.Interfaces;

public interface IUserService : ICRUDService<UserModel, UserEntity, Guid>
{
    Task<UserModel> Create(RegisterDTO model);
    Task<bool> ExistsAsync(string email);
    Task<string> AuthenticateAsync(string email, string password);
    Task<List<UserModel>> GetTestUserAsync(int count);
}
