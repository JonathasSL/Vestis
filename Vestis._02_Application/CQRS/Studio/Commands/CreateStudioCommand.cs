using MediatR;
using Vestis._02_Application.CQRS.Address.Commands;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.CQRS.Studio.Commands;

public record CreateStudioCommand(
    Guid UserId,
    string Name,
    string ContactEmail,
    string PhoneNumber,
    CreateAddressCommand? AddressCommand) : IRequest<StudioEntity>;