using Vestis._02_Application.Models;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Services.Interfaces;

public interface IAddressService : ICRUDService<AddressModel, AddressEntity, Guid>
{
    AddressModel GetAddressModel(AddressEntity addressEntity);
}
