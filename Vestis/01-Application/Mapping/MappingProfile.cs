using AutoMapper;
using Vestis._01_Application.Models;
using Vestis.Entities;

namespace Vestis._01_Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserModel>()
                .ForMember(entity => entity.Password, opt => opt.Ignore());
            CreateMap<UserModel, UserEntity>()
                .ForMember(entity => entity.Password, opt => opt.Ignore());

        }
    }
}
