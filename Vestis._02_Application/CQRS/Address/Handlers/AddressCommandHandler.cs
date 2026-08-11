using MediatR;
using Vestis._02_Application.CQRS.Address.Commands;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.CQRS.Address.Handlers;

public class AddressCommandHandler : 
    IRequestHandler<CreateAddressCommand, AddressEntity>,
    IRequestHandler<UpdateAddressCommand, AddressEntity>
{
    private readonly IAddressRepository _repository;
    
    public AddressCommandHandler(IAddressRepository repository)
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
            request.ZipCode,
            request.Complement,
            request.Country
        );

        address = await _repository.CreateAsync(address, cancellationToken);
        return address;
    }

    public async Task<AddressEntity?> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _repository.GetByIdAsync(request.Id);

        if (address is null)
            return null;

        address.ChangeStreet(request.Street)
            .ChangeNumber(request.Number)
            .ChangeComplement(request.Complement)
            .ChangeNeighborhood(request.Neighborhood)
            .ChangeCity(request.City)
            .ChangeState(request.State)
            .ChangeCountry(request.Country)
            .ChangeZipCode(request.ZipCode);

        address = await _repository.Update(address);
        return address;
    }
}