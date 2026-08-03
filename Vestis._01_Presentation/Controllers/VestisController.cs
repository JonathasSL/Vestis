using Microsoft.AspNetCore.Mvc;
using Vestis._02_Application.Common;

namespace Controllers;

[ApiController]
[Route("api/[controller]/[Action]")]
public class VestisController : ControllerBase
{
	protected IActionResult FromResult<T>(CommandResult<T> result)
	{
		switch (result.Status)
		{
			case CommandResultStatus.Success:
				return Ok(result);
			case CommandResultStatus.NotFound:
				return NotFound(result);
			case CommandResultStatus.ValidationFailure:
				return BadRequest(result);
			default:
				return StatusCode(500, result);
		}		
	}
}
