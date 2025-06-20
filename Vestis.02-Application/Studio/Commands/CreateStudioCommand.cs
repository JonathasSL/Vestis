using MediatR;
using Vestis._02_Application.Address.Commands;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Studio.Commands;

public record CreateStudioCommand(
    string Name,
    string ContactEmail,
    string PhoneNumber,
    CreateAddressCommand AddressCommand) : IRequest<StudioEntity>;