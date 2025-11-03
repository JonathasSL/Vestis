using AutoMapper;
using Vestis._02_Application.Models;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Mapping.StudioMembership;

public class StudioMembershipEntityToStudioMembershipModel : ITypeConverter<StudioMembershipEntity, StudioMembershipModel>
{
    public StudioMembershipModel Convert(StudioMembershipEntity source, StudioMembershipModel destination, ResolutionContext context)
    {
        if (source == null) return null;

        return new StudioMembershipModel
        {
            UserId = source.UserId,
            StudioId = source.StudioId,
            Role = source.Role
        };
    }
}
