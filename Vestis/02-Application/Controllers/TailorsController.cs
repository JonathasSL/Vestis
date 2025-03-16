using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vestis._01_Application.Controllers;

namespace Vestis.Application.Controllers;

[Authorize]
public class TailorsController : VestisController
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

    [HttpGet("test")]
    public IActionResult GetTest()
    {
        return Ok(new { Message = "Lista de alfaiates retornada com sucesso!" });
    }
}
