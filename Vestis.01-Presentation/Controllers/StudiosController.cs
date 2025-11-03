using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vestis._02_Application.Models;
using Vestis._02_Application.Services.Interfaces;
using Vestis.Shared.Extensions;

namespace Vestis._01_Presentation.Controllers;

[Authorize]
public class StudiosController : VestisController
{
    private readonly ILogger<StudiosController> _logger;
    private readonly IStudioService _service;

    public StudiosController(ILogger<StudiosController> logger, IStudioService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var studio = await _service.GetById(id);
            if (studio is not null)
                return Ok(studio);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e.ExceptionStack(out _));
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudioModel studioModel)
    {
        try
        {
            var userId = User?.GetUserId();

            var result = await _service.Create(userId.Value, studioModel);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ExceptionStack(out _));
            return StatusCode(500);
        }
    }
}
