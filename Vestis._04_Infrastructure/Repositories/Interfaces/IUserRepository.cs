using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrastructure.Repositories.Interfaces;

public interface IUserRepository : IRepository<UserEntity, Guid>
{
    Task<bool> Exists(string email);
    Task<UserEntity> GetByEmailAsync(string email);
}
