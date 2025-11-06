using Controllers;
using Microsoft.AspNetCore.Mvc;
using Vestis._02_Application.Models;
using Vestis._02_Application.Services.Interfaces;
using Vestis.Shared.Extensions;

namespace Vestis._01_Presentation.Controllers;

[ApiController]
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

				studioProducts =_service.GetProductsByStudioWithFilters(studioGuid, filters).ToList();

			if (studioProducts.Any())
				return Ok(studioProducts.ToList());
			else
				return NotFound();
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

		if (!Guid.TryParse(studioId, out var studioGuid) || !Guid.TryParse(studioId, out var productGuid))
			return BadRequest($"Invalid GUID format. studioId: {studioId} - productId: {productId}");
		
		try
		{
			var product = _service.GetProductByStudio(productGuid, studioGuid);

			if (product is null)
				return NotFound();
			else
				return Ok(product);
		}
		catch (Exception e)
		{
			_logger.LogError(e.ExceptionStack(out _));
			return StatusCode(500);
		}
	}

	[HttpPost]
	public async Task<IActionResult> Post(string studioId, [FromBody] string value)
	{
		return NoContent();
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Put(string studioId, int id, [FromBody] string value)
	{
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(string studioId, int id)
	{
		return NoContent();
	}

	private IProductService _service;
	private readonly ILogger<ProductsController> _logger;
	public ProductsController(IProductService service, ILogger<ProductsController> logger)
	{
		_service = service;
		_logger = logger;
	}
}
