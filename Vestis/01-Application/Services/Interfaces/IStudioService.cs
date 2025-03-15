using Vestis._01_Application.Models;
using Vestis._02_Domain.Entities;

namespace Vestis._01_Application.Services.Interfaces
{
    public interface IStudioService : ICRUDService<StudioModel, StudioEntity, Guid>
    {
    }
}
