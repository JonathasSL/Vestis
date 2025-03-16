using Vestis.Entities;

namespace Vestis._02_Domain.Repositories.Interfaces;

public interface IUserRepository : IRepository<UserEntity, Guid>
{
    Task<bool> Exists(string email);
    Task<UserEntity> GetByEmailAsync(string email);
}
