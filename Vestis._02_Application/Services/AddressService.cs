using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Vestis._02_Application.Common;
using Vestis._02_Application.Models;
using Vestis._02_Application.Services.Interfaces;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.Services;

internal class AddressService : CRUDService<AddressModel, AddressEntity, Guid>, IAddressService
{

    public AddressModel GetAddressModel(AddressEntity addressEntity)
    {
        return new AddressModel
        {
            Id = addressEntity.Id,
            Street = addressEntity.Street,
            Number = addressEntity.Number,
            Complement = addressEntity.Complement,
            Neighborhood = addressEntity.Neighborhood,
            City = addressEntity.City,
            State = addressEntity.State,
            Country = addressEntity.Country,
            ZipCode = addressEntity.ZipCode
        };
    }

    public AddressService(
        IMapper mapper,
        IMediator mediator,
        BusinessNotificationContext businessNotificationContext,
        ILogger<CRUDService<AddressModel, AddressEntity, Guid>> logger,
        IRepository<AddressEntity, Guid> repository) : base(mapper, mediator, businessNotificationContext, logger, repository)
    {
    }
}
