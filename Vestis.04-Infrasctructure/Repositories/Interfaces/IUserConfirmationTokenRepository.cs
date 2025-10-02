using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrasctructure.Repositories.Interfaces;

public interface IUserConfirmationTokenRepository : IRepository<UserConfirmationTokenEntity, Guid>
{
    Task<UserConfirmationTokenEntity?> GetByTokenAsync(string token, CancellationToken cancellationToken);
    Task CreateAsync(UserConfirmationTokenEntity tokenEntity, CancellationToken cancellationToken);
    Task MarkAsUsedAsync(Guid id, CancellationToken cancellationToken);
}