using Vestis._02_Application.Common;
using Vestis._02_Application.Models.Studio;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Services.Interfaces;

public interface IStudioService : ICRUDService<StudioModel, StudioEntity, Guid>
{
    Task<CommandResult<StudioModel>> Create(Guid contextUser, StudioModel model);
    CommandResult<List<StudioSummaryModel>> GetStudiosByUserId(Guid userId, CancellationToken cancellationToken);
}
