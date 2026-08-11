using MediatR;
using Vestis._02_Application.Common;
using Vestis._02_Application.Models;

namespace Vestis._02_Application.CQRS.Address.Query;

internal record GetAddressByIdQuery(Guid Id) : IRequest<CommandResult<AddressModel>>;