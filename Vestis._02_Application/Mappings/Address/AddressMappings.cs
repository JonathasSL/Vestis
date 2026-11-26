using Vestis._02_Application.Models;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Mappings.Address;

internal static class AddressMappings
{
    public static AddressModel? ToModel(this AddressEntity entity)
    {
        if (entity is null)
            return null;

        return new AddressModel
        {
            Id = entity.Id,
            CreatedDate = entity.CreatedDate,
            UpdatedDate = entity.UpdatedDate,
            DeletedDate = entity.DeletedDate,

            Street = entity.Street,
            Number = entity.Number,
            Complement = entity.Complement,
            Neighborhood = entity.Neighborhood,
            City = entity.City,
            State = entity.State,
            ZipCode = entity.ZipCode,
            Country = entity.Country
        };
    }
}
