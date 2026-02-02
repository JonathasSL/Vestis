using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Data;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._04_Infrastructure.Repositories;

internal class StudioRepository : Repository<StudioEntity, Guid>, IStudioRepository
{
    public StudioRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<StudioEntity>> GetByUserAsyncReadOnly(Guid userId, CancellationToken cancellationToken)
    {
        var studio = BeginQueryReadOnly()
            .Where(s => s.StudioMemberships.Any(sm => sm.UserId == userId));

        return studio.ToList();
    }
}
