using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Data;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._04_Infrasctructure.Repositories;

internal class AddressRepository : Repository<AddressEntity, Guid>, IAddressRepository
{
    public AddressRepository(ApplicationDbContext context) : base(context)
    {
    }
}
