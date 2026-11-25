using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Data;
using Vestis._04_Infrastructure.ObjectQuery.Filters;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._04_Infrastructure.Repositories;

internal class ClientRepository : Repository<ClientEntity, Guid>, IClientRepository
{
    public ClientRepository(ApplicationDbContext context) : base(context)
    {
    }

    public List<ClientEntity> SearchStudioClientsWithfilters(ClientFilters filters, CancellationToken cancellationToken)
    {

        var query = BeginQueryReadOnly()
            .Where(client => client.StudioId == filters.StudioId);


        if (!string.IsNullOrWhiteSpace(filters.SearchTerm))
        {
            var term = filters.SearchTerm!.Trim();
            query = query.Where(client => 
                client.Name.Contains(term) 
                || (client.Email != null && client.Email.Contains(term)) 
                || (client.PhoneNumber != null && client.PhoneNumber.Contains(term)));
        }

        if (filters.ClientSinceStart.HasValue)
            query = query.Where(client => client.CreatedDate >= filters.ClientSinceStart.Value.ToDateTime(new TimeOnly(23, 59, 59)));

        if (filters.ClientSinceEnd.HasValue)
            query = query.Where(client => client.CreatedDate <= filters.ClientSinceEnd.Value.ToDateTime(new TimeOnly(0, 0, 0)));

        if (filters.MinTotalPurchases.HasValue) { }
            // Implement logic to filter by minimum total purchases when applicable.

        if (filters.MaxTotalPurchases.HasValue) { }
            // Implement logic to filter by maximum total purchases when applicable.

        return query.ToList();
    }
}
