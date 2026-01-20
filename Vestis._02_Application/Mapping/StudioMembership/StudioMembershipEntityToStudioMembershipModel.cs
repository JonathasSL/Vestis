using AutoMapper;
using Vestis._02_Application.Models.Studio;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Mapping.StudioMembership;

public class StudioMembershipEntityToStudioMembershipModel : ITypeConverter<StudioMembershipEntity, StudioMembershipModel>
{
    public StudioMembershipModel Convert(StudioMembershipEntity source, StudioMembershipModel destination, ResolutionContext context)
    {
        if (source == null) return null;

        if (destination == null)
            destination = new StudioMembershipModel();

        destination.Role = source.Role;

        return destination;
    }
}
