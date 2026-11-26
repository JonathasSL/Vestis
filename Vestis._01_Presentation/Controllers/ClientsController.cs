using Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vestis._02_Application.Common;
using Vestis._02_Application.Models.Client;
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
		var errorsList = new List<string>();

		if (string.IsNullOrWhiteSpace(studioId))
			errorsList.Add("StudioId is required.");
		
		if (!Guid.TryParse(studioId, out var studioGuid))
			errorsList.Add("Invalid GUID format for studioId."); 
		
		if (errorsList.Any())
			return FromResult(CommandResult<IEnumerable<ClientSearchResultsModel>>.Failure(errorsList));

		var filters = Request.Query.ToDictionary(
			q => q.Key, 
			q => q.Value.ToString());

		var result = await _service.GetClientsByStudioWithFilters(studioGuid, filters, cancellationToken);

		return FromResult(result);
	}

	[HttpGet("{clientId}")]
	public async Task<IActionResult> GetClientById(string studioId, string clientId, CancellationToken cancellationToken)
	{
		var errorsList = new List<string>();

		if (string.IsNullOrWhiteSpace(studioId))
			errorsList.Add("StudioId is required.");
		if (!Guid.TryParse(studioId, out var studioGuid))
			errorsList.Add("Invalid GUID format for studioId.");

		if (string.IsNullOrWhiteSpace(clientId))
			errorsList.Add("ClientId is required.");
		if (!Guid.TryParse(clientId, out var clientGuid))
			errorsList.Add("Invalid GUID format for clientId.");

		if (errorsList.Any())
			return FromResult(CommandResult<ClientModel>.Failure(errorsList));

		var result = await _service.GetClientByIdAndStudioId(clientGuid, studioGuid, cancellationToken);
		return FromResult(result);
	}


	[HttpPost]
	public async Task<IActionResult> CreateClient(string studioId, [FromBody] CreateClientRequestModel request, CancellationToken cancellationToken)
	{
		var errorsList = new List<string>();

		if (string.IsNullOrWhiteSpace(studioId))
			errorsList.Add("StudioId is required.");
		if (!Guid.TryParse(studioId, out var studioGuid))
			errorsList.Add("Invalid GUID format for studioId.");

		if (errorsList.Any())
			return FromResult(CommandResult<ClientModel>.Failure(errorsList));

		var result = await _service.CreateClient(studioGuid, request, cancellationToken);
		return FromResult(result);
	}


	public ClientsController(IClientService service)
	{
		_service = service;
	}
}
