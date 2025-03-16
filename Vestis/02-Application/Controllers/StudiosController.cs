using Microsoft.AspNetCore.Mvc;
using Vestis._01_Application.Models;
using Vestis._01_Application.Services.Interfaces;
using Vestis.Extensions;

namespace Vestis._01_Application.Controllers
{
    public class StudiosController : VestisController
    {
        private readonly ILogger<StudiosController> _logger;
        private readonly IStudioService _service;

        public StudiosController(ILogger<StudiosController> logger, IStudioService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var studios = (await _service.GetAllAsync()).ToList();

                if (studios.Any())
                    return Ok(studios);
                else
                    return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ExceptionStack(out _));
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var studio = (await _service.GetById(id));
                if (studio is not null)
                    return Ok(studio);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ExceptionStack(out _));
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudioModel studioModel)
        {
            try
            {
                var studio = (await _service.Create(studioModel));
                if (studio is null)
                    return BadRequest();
                else
                    return Ok(studio);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ExceptionStack(out _));
                return BadRequest();
            }
        }
    }
}
