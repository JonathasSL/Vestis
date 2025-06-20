using AutoMapper;
using Vestis._02_Application.Models;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Mapping.Studio;

public class StudioEntityToStudioModelConverter : ITypeConverter<StudioEntity, StudioModel>
{
    public StudioModel Convert(StudioEntity source, StudioModel destination, ResolutionContext context)
    {
        destination ??= new StudioModel();
        
        destination.Name = source.Name;
        destination.ContactEmail = source.ContactEmail;
        destination.PhoneNumber = source.PhoneNumber;

        if (source.Address is not null)
            destination.Address = context.Mapper.Map<AddressModel>(source.Address);
        else
            destination.Address = null;

        return destination;
    }
}
