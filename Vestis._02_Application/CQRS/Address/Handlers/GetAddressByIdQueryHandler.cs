using MediatR;
using Vestis._02_Application.Common;
using Vestis._02_Application.CQRS.Address.Query;
using Vestis._02_Application.Models;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.CQRS.Address.Handlers;

internal class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, CommandResult<AddressModel>>
{
    private readonly IAddressRepository _repository;
    
    public async Task<CommandResult<AddressModel>> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var address = await _repository.GetByIdAsync(request.Id);

        if (address is not null)
        {
            var model = new AddressModel
            {
                Street = address.Street,
                Number = address.Number,
                Complement = address.Complement,
                Neighborhood = address.Neighborhood,
                City = address.City,
                State = address.State,
                Country = address.Country,
                ZipCode = address.ZipCode
            };

            return CommandResult<AddressModel>.Success(model);
        }
        else
        {
            return CommandResult<AddressModel>.NotFound($"No address found with the ID: {request.Id}");
        }
    }


    public GetAddressByIdQueryHandler(IAddressRepository repository)
    {
        _repository = repository;
    }
}
