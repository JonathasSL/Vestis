using AutoMapper;
using Vestis._01_Application.Models;
using Vestis._01_Application.Services.Interfaces;
using Vestis._02_Domain.Entities;
using Vestis._02_Domain.Repositories.Interfaces;

namespace Vestis._01_Application.Services
{
    public class StudioService : CRUDService<StudioModel, StudioEntity, Guid>, IStudioService
    {
        public StudioService(IMapper mapper, ILogger<CRUDService<StudioModel, StudioEntity, Guid>> logger, IRepository<StudioEntity, Guid> repository) : base(mapper, logger, repository)
        {
        }
    }
}
