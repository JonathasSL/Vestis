using AutoMapper;
using Vestis._02_Application.Mapping.Address;
using Vestis._02_Application.Models;
using Vestis._02_Application.Studio.Commands;

namespace Vestis._02_Application.Mapping.Studio;

public class CreateStudioCommandToStudioModelConverter : ITypeConverter<CreateStudioCommand, StudioModel>
{
    public StudioModel Convert(CreateStudioCommand source, StudioModel destination, ResolutionContext context)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        destination ??= new StudioModel();

        destination.Name = source.Name;
        destination.ContactEmail = source.ContactEmail;
        destination.PhoneNumber = source.PhoneNumber;
        
        //destination.Address = context.Mapper.Map<AddressModel>(source.Address);

        return destination;
    }
}