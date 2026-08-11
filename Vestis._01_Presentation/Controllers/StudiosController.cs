using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vestis._02_Application.Models.Studio;
using Vestis._02_Application.Services.Interfaces;
using Vestis.Shared.Extensions;

namespace Controllers;

[Authorize]
public class StudiosController : VestisController
{
    private readonly IStudioService _service;

    public StudiosController(IStudioService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var studio = await _service.GetById(id);
		return FromResult(studio);
	}

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudioModel studioModel, CancellationToken cancellationToken)
    {
        var userId = User?.GetUserId();

        var result = await _service.Create(userId.Value, studioModel);
            
		return FromResult(result);
    }

	[HttpGet]
	public async Task<IActionResult> GetMyStudios(CancellationToken cancellationToken)
	{
		var userId = User?.GetUserId();
		var result = _service.GetStudiosByUserId(userId.Value, cancellationToken);
		return FromResult(result);
	}

	[HttpPut]
	public async Task<IActionResult> Update([FromBody] StudioModel studioModel, CancellationToken cancellationToken)
	{
		var userId = User?.GetUserId();
		if (!userId.HasValue)
			return Unauthorized();

		var result = await _service.Update(userId.Value, studioModel);
		return FromResult(result);
	}
}
