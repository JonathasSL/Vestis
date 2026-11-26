using MediatR;
using Vestis._02_Application.Common;
using Vestis._02_Application.CQRS.Address.Commands;
using Vestis._02_Application.Models.Client;

namespace Vestis._02_Application.CQRS.Client.Commands;

public record CreateClientCommand(string Name, string Email, string PhoneNumber, Guid FromStudioId, CreateAddressCommand? Address) : IRequest<CommandResult<ClientModel>>;