using AutoMapper;
using Vestis._02_Application.Models;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Mapping;

public class StudioModelToStudioEntityConverter : ITypeConverter<StudioModel, StudioEntity>
{
    public StudioEntity Convert(StudioModel source, StudioEntity destination, ResolutionContext context)
    {
        if (destination == null) 
            destination ??= new StudioEntity(source.Name);
        else
        {
            destination.ChangeName(source.Name);
            destination.ChangeContactEmail(source.ContactEmail);
            destination.ChangePhoneNumber(source.PhoneNumber);
        }

        return destination;
    }
}
