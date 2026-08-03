using Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vestis._02_Application.Models.Product;
using Vestis._02_Application.Services.Interfaces;
using Vestis.Shared.Extensions;

namespace Vestis._01_Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/studios/{studioId:Guid}/[controller]")]
public class ProductsController : VestisController
{
	[HttpGet]
	public async Task<IActionResult> GetAll(string studioId)
	{
		if (string.IsNullOrEmpty(studioId))
			return BadRequest("StudioId is required");

		if (!Guid.TryParse(studioId, out var studioGuid))
			return BadRequest("Invalid GUID format.");

		List<ProductModel> studioProducts;
		try
		{
			var filters = Request.Query.ToDictionary(
				q => q.Key, 
				q => q.Value.ToString());

			studioProducts =_service.GetProductsByStudioWithFiltersAsync(studioGuid, filters);

			if (studioProducts.Any())
				return Ok(studioProducts.ToList());
			else
				return NoContent();
		}
		catch (Exception e)
		{
			_logger.LogError(e.ExceptionStack(out _));
			return StatusCode(500);
		}
	}

	[HttpGet("{productId}")]
	public async Task<IActionResult> Get(string studioId, string productId)
	{
		if (string.IsNullOrEmpty(studioId))
			return BadRequest("StudioId is required");

		if (string.IsNullOrEmpty(productId))
			return BadRequest("ProductId is required");

		if (!Guid.TryParse(studioId, out var studioGuid) || !Guid.TryParse(productId, out var productGuid))
			return BadRequest($"Invalid GUID format. studioId: {studioId} - productId: {productId}");
		
		var product = await _service.GetProductByStudio(productGuid, studioGuid);

		return FromResult(product);
	}

	[HttpPost]
	public async Task<IActionResult> Register(string studioId, [FromBody] ProductModel requestModel)
	{
		var responseModel = _service.RegisterProduct(requestModel);
		return FromResult(responseModel);

	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Put(string studioId, int id, [FromBody] ProductModel requestModel)
	{
		throw new NotImplementedException();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(string studioId, Guid id)
	{
		if (string.IsNullOrEmpty(studioId))
			return BadRequest("StudioId is required");

		if (!Guid.TryParse(studioId, out var studioGuid))
			return BadRequest("Invalid GUID format.");

		_service.DeleteProduct(id, studioGuid);
		return Ok();
	}

	private IProductService _service;
	private readonly ILogger<ProductsController> _logger;
	public ProductsController(IProductService service, ILogger<ProductsController> logger)
	{
		_service = service;
		_logger = logger;
	}
}
