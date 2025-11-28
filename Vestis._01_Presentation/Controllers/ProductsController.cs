using Controllers;
using Microsoft.AspNetCore.Mvc;
using Vestis._02_Application.Models.Product;
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

			studioProducts =_service.GetProductsByStudioWithFiltersAsync(studioGuid, filters);

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

		if (!Guid.TryParse(studioId, out var studioGuid) || !Guid.TryParse(productId, out var productGuid))
			return BadRequest($"Invalid GUID format. studioId: {studioId} - productId: {productId}");
		
		try
		{
			var product = _service.GetProductByStudio(productGuid, studioGuid).Result;

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
	public async Task<IActionResult> Register(string studioId, [FromBody] ProductModel requestModel)
	{
		try
		{
			var responseModel = _service.RegisterProduct(requestModel);
			if (responseModel is null)
				return BadRequest("Could not register product");
			else
				return CreatedAtAction(nameof(Register), new { studioId = studioId, productId = responseModel.Id }, responseModel);
		} catch (Exception e)
		{
			_logger.LogError(e.ExceptionStack(out _));
			return StatusCode(500);
		}
	}

		[HttpPut("{id}")]
	public async Task<IActionResult> Put(string studioId, int id, [FromBody] ProductModel requestModel)
	{
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(string studioId, Guid id)
	{
		if (string.IsNullOrEmpty(studioId))
			return BadRequest("StudioId is required");

		if (!Guid.TryParse(studioId, out var studioGuid))
			return BadRequest("Invalid GUID format.");

		try
		{
			_service.Delete(id);
			return Ok();
		}
		catch (Exception e)
		{

			throw;
		}
	}

	private IProductService _service;
	private readonly ILogger<ProductsController> _logger;
	public ProductsController(IProductService service, ILogger<ProductsController> logger)
	{
		_service = service;
		_logger = logger;
	}
}
