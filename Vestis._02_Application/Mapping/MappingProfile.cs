using AutoMapper;
using Vestis._02_Application.Mapping.Address;
using Vestis._02_Application.Mapping.Studio;
using Vestis._02_Application.Mapping.StudioMembership;
using Vestis._02_Application.Models;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BaseEntity<Guid>, BaseModel<Guid>>()
            .IncludeAllDerived()
            .ConvertUsing<BaseEntityToBaseModelConverter>();

        CreateMap<AddressEntity, AddressModel>()
            .ConvertUsing<AddressEntityToAddressModelConverter>();

        CreateMap<StudioEntity, StudioModel>()
            .ConvertUsing<StudioEntityToStudioModelConverter>();

        CreateMap<StudioMembershipEntity, StudioMembershipModel>()
            .ConvertUsing<StudioMembershipEntityToStudioMembershipModel>();

        CreateMap<UserEntity, UserModel>()
            .ForMember(entity => entity.Password, opt => opt.Ignore());

        CreateMap<ProductEntity, ProductModel>();
    }
}