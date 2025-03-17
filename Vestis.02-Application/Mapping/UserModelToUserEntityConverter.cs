using AutoMapper;
using Vestis._02_Application.Services;
using Vestis._02_Application.Models;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Mapping;

public class UserModelToUserEntityConverter : ITypeConverter<UserModel, UserEntity>
{
    public UserEntity Convert(UserModel source, UserEntity destination, ResolutionContext context)
    {
        var hasher = new PasswordHasher();
        if (destination != null)
        {
            destination.ChangeName(source.Name);
            destination.ChangeEmail(source.Email);
            destination.ChangePassword(hasher.Hash(source.Password));
        }
        else
        {
            destination = new UserEntity(source.Name, source.Email, hasher.Hash(source.Password));
        }
        return destination;
    }
}
