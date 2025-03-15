using AutoMapper;
using Vestis._01_Application.Models;
using Vestis._02_Domain.Entities;
using Vestis.Application.Models;
using Vestis.Entities;

namespace Vestis._01_Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CustomMappings();
        StandardMappings();

        CreateMap<BaseEntity<Guid>, BaseModel<Guid>>();
        CreateMap<BaseModel<Guid>, BaseEntity<Guid>>()
            .IncludeAllDerived()
            .ConvertUsing<BaseModelToBaseEntityConverter<Guid>>();
    }

    private void StandardMappings()
    {

        CreateMap<UserEntity, UserModel>()
            .ForMember(entity => entity.Password, opt => opt.Ignore());
        CreateMap<UserModel, UserEntity>()
            .IncludeBase<BaseModel<Guid>, BaseEntity<Guid>>()
            .ConvertUsing<UserModelToUserEntityConverter>();

        CreateMap<StudioEntity, StudioModel>();
        CreateMap<StudioModel, StudioEntity>()
            .IncludeBase<BaseModel<Guid>, BaseEntity<Guid>>();

    }

    private void CustomMappings()
    {
    }
}
