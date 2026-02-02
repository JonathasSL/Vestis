using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrastructure.Repositories.Interfaces;

public interface IStudioMembershipRepository : IRepository<StudioMembershipEntity, Guid>
{
    Task<IEnumerable<StudioMembershipEntity>> GetFromStudioIdAsync(Guid studioId, CancellationToken cancellationToken);
    Task<StudioMembershipEntity> GetByUserAndStudioAsync(Guid userId, Guid studioId, CancellationToken cancellationToken);
    Task<IEnumerable<StudioMembershipEntity>> GetFromUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
