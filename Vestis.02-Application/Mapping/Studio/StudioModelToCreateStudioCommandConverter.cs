using AutoMapper;
using Vestis._02_Application.Models;
using Vestis._02_Application.Studio.Commands;

namespace Vestis._02_Application.Mapping.Studio;

public class StudioModelToCreateStudioCommandConverter : ITypeConverter<StudioModel, CreateStudioCommand>
{
    public CreateStudioCommand Convert(StudioModel source, CreateStudioCommand destination, ResolutionContext context)
    {
        if (destination == null)
            return new CreateStudioCommand(
                source.Name,
                source.ContactEmail,
                source.PhoneNumber);
        else
            return destination with
            {
                Name = source.Name,
                ContactEmail = source.ContactEmail,
                PhoneNumber = source.PhoneNumber
            };
    }
}
