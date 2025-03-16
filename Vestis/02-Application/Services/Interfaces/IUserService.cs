using Vestis._01_Application.Models;
using Vestis.Entities;

namespace Vestis._01_Application.Services.Interfaces
{
    public interface IUserService : ICRUDService<UserModel, UserEntity, Guid>
    {
        Task<bool> ExistsAsync(string email);
        Task<string> AuthenticateAsync(string email, string password);
    }
}
