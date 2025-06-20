using MediatR;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Address.Commands;

public record CreateAddressCommand(
    string Street,
    string Number,
    string Neighborhood,
    string City,
    string State,
    string ZipCode,
    string? Complement = null,
    string Country = "BR"
) : IRequest<AddressEntity>;
