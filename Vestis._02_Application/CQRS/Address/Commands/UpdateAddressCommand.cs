using MediatR;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.CQRS.Address.Commands;

public record UpdateAddressCommand(
    Guid Id,
    string Street,
    string Number,
    string? Complement,
    string Neighborhood,
    string City,
    string State,
    string ZipCode,
    string Country = "BR") : IRequest<AddressEntity>;