using Vestis._02_Application.Mappings.Address;
using Vestis._02_Application.Models.Client;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Mappings.Client;

internal static class ClientMappings
{
    public static ClientModel? ToClientModel(this ClientEntity entity)
    {
        if (entity is null)
            return null;

        return new ClientModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            Address = entity.Address.ToModel()
        };
    }

    public static ClientSearchResultsModel ToClientSearchResultsModel(this ClientEntity entity)
    {
        if (entity is null)
            return null;

        return new ClientSearchResultsModel
        {
            Name= entity.Name,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            ClientSince = DateOnly.FromDateTime(entity.CreatedDate),
            TotalPurchases = 0
        };
    }   
}
