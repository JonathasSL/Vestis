using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Data;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._04_Infrastructure.Repositories;

internal class AddressRepository : Repository<AddressEntity, Guid>, IAddressRepository
{
    public AddressRepository(ApplicationDbContext context) : base(context)
    {
    }
}
