using Vestis._02_Application.Common;
using Vestis._02_Application.Models;
using Vestis._02_Application.Models.Auth;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Services.Interfaces.User;

public interface IUserService : ICRUDService<UserModel, UserEntity, Guid>
{
    Task<CommandResult<UserModel>> Create(RegisterDTO model);
    Task<CommandResult<string>> AuthenticateAsync(string email, string password);
    Task<CommandResult<List<UserModel>>> GetTestUserAsync(int count);
}
