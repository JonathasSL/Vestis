using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.ObjectQuery.Filters;

namespace Vestis._04_Infrastructure.Repositories.Interfaces;

public interface IClientRepository : IRepository<ClientEntity, Guid>
{
    List<ClientEntity> SearchStudioClientsWithfilters(ClientFilters filters, CancellationToken cancellationToken);
}
