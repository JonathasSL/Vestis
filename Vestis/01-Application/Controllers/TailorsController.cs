using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vestis.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TailorsController : ControllerBase
    {
        private readonly ILogger<TailorsController> _logger;

        public TailorsController(ILogger<TailorsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { Message = "Lista de alfaiates retornada com sucesso!" });
        }
    }
}
