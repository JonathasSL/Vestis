using AutoMapper;
using Microsoft.Extensions.Logging;
using Vestis._02_Application.Models;
using Vestis._02_Application.Services.Interfaces;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._02_Application.Services;

public class StudioService : CRUDService<StudioModel, StudioEntity, Guid>, IStudioService
{
    public StudioService(IMapper mapper, ILogger<CRUDService<StudioModel, StudioEntity, Guid>> logger, IRepository<StudioEntity, Guid> repository) : base(mapper, logger, repository)
    {
    }
}
