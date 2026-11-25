using Vestis._02_Application.Common;
using Vestis._02_Application.Models.Client;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Services.Interfaces;

public interface IClientService : ICRUDService<ClientModel, ClientEntity, Guid>
{
    Task<CommandResult<IEnumerable<ClientSearchResultsModel>>> GetClientsByStudioWithFilters(Guid studioGuid, Dictionary<string, string> filters, CancellationToken cancellationToken);
}
