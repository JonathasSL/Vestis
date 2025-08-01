using Microsoft.EntityFrameworkCore;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Data;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._04_Infrasctructure.Repositories;

internal class StudioMembershipRepository : Repository<StudioMembershipEntity, Guid>, IStudioMembershipRepository
{
    public StudioMembershipRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<StudioMembershipEntity>> GetFromStudioIdAsync(Guid studioId, CancellationToken cancellationToken)
    {
        var query =  BeginQuery()
            .Where(membership => membership.StudioId == studioId);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<StudioMembershipEntity> GetByUserAndStudioAsync(Guid userId, Guid studioId, CancellationToken cancellationToken)
    {
        var query = BeginQuery()
            .Where(membership => 
            membership.UserId == userId &&
            membership.StudioId == studioId);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<StudioMembershipEntity>> GetFromUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var query = BeginQuery()
            .Where(membership => membership.UserId == userId);

        return await query.ToListAsync(cancellationToken);
    }
}
