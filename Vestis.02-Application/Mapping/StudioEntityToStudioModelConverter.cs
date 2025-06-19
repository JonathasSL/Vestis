using AutoMapper;
using Vestis._02_Application.Models;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Mapping;

public class StudioEntityToStudioModelConverter : ITypeConverter<StudioEntity, StudioModel>
{
    public StudioModel Convert(StudioEntity source, StudioModel destination, ResolutionContext context)
    {
        destination ??= new StudioModel();
        
        destination.Name = source.Name;
        destination.ContactEmail = source.ContactEmail;
        destination.PhoneNumber = source.PhoneNumber;

        return destination;
    }
}
