using Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vestis._02_Application.Services.Interfaces;

namespace Vestis._01_Presentation.Controllers;

[Authorize]
[Route("api/studios/{studioId:Guid}/[controller]")]
public class ClientsController : VestisController
{
	private readonly IClientService _service;

	[HttpGet]
	public async Task<IActionResult> SearchClients(string studioId, CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(studioId))
			return BadRequest("StudioId is required.");
		
		if (!Guid.TryParse(studioId, out var studioGuid))
			return BadRequest("Invalid GUID format for studioId."); 
		
		var filters = Request.Query.ToDictionary(
			q => q.Key, 
			q => q.Value.ToString());

		var result = await _service.GetClientsByStudioWithFilters(studioGuid, filters, cancellationToken);

		return FromResult(result);
	}

	public ClientsController(IClientService service)
	{
		_service = service;
	}
}
