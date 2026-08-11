using MediatR;
using Vestis._02_Application.CQRS.Address.Commands;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.CQRS.Studio.Commands;

public record UpdateStudioCommand(
    Guid Id,
    string Name,
    string ContactEmail,
    string PhoneNumber,
    UpdateAddressCommand? AddressCommand) : IRequest<StudioEntity>;
