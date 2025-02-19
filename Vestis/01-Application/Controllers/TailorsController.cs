using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vestis._01_Application.Controllers;

namespace Vestis.Application.Controllers;

public class TailorsController : BaseController
{
    private readonly ILogger<TailorsController> _logger;

    public TailorsController(ILogger<TailorsController> logger)
    {
        _logger = logger;
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new { Message = "Lista de alfaiates retornada com sucesso!" });
    }

    [HttpGet("test")]
    public IActionResult GetTest()
    {
        return Ok(new { Message = "Lista de alfaiates retornada com sucesso!" });
    }
}
