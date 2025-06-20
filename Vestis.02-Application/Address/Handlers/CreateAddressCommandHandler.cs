using MediatR;
using Vestis._02_Application.Address.Commands;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._02_Application.Address.Handlers;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, AddressEntity>
{
    private readonly IAddressRepository _repository;
    
    public CreateAddressCommandHandler(IAddressRepository repository)
    {
        _repository = repository;
    }

    public async Task<AddressEntity> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = new AddressEntity(
            request.Street,
            request.Number,
            request.Neighborhood,
            request.City,
            request.State,
            request.ZipCode
        );
        
        address.ChangeComplement(request.Complement);
        address.ChangeCountry(request.Country);

        address = await _repository.CreateAsync(address);
        return address;
    }
}