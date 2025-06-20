using AutoMapper;
using Vestis._02_Application.Models;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Mapping.Address;

public class AddressEntityToAddressModelConverter : ITypeConverter<AddressEntity, AddressModel>
{
    public AddressModel Convert(AddressEntity source, AddressModel destination, ResolutionContext context)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));

        destination ??= new AddressModel();

        destination.Id = source.Id;
        destination.CreatedDate = source.CreatedDate;
        destination.UpdatedDate = source.UpdatedDate;
        destination.DeletedDate = source.DeletedDate;

        destination.Street = source.Street;
        destination.Number = source.Number;
        destination.Complement = source.Complement;
        destination.Neighborhood = source.Neighborhood;
        destination.City = source.City;
        destination.State = source.State;
        destination.Country = source.Country;
        destination.ZipCode = source.ZipCode;
        

        return destination;
    }
}