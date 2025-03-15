using Vestis._02_Domain.Entities;
using Vestis._02_Domain.Repositories.Interfaces;
using Vestis.Data;

namespace Vestis._02_Domain.Repositories;

public class StudioRepository : Repository<StudioEntity, Guid>, IStudioRepository
{
    public StudioRepository(ApplicationDbContext context) : base(context)
    {
    }
}
