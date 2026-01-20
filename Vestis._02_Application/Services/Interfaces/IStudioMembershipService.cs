using Vestis._02_Application.Models.Studio;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Services.Interfaces;

public interface IStudioMembershipService : ICRUDService<StudioMembershipModel, StudioMembershipEntity, Guid>
{
    Task<IEnumerable<StudioMembershipModel>?> GetFromStudioId(Guid studioId, CancellationToken cancellationToken);
    Task<IEnumerable<StudioMembershipModel>> GetFromUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<StudioMembershipModel> GetByUserAndStudioAsync(Guid userId, Guid studioId, CancellationToken cancellationToken);
}
