using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vestis._02_Application.Models;
using Vestis._02_Application.Services.Interfaces;
using Vestis.Shared.Extensions;

namespace Controllers;

[Authorize]
public class StudioMembershipController : VestisController
{
    private readonly IStudioMembershipService _studioMembershipService;
    public StudioMembershipController(IStudioMembershipService studioMembershipService)
    {
        _studioMembershipService = studioMembershipService ?? throw new ArgumentNullException(nameof(studioMembershipService));
    }

    [HttpGet("{studioId}")]
    public async Task<IActionResult> GetFromStudioId(Guid studioId, CancellationToken cancellationToken)
    {
        if (studioId == Guid.Empty)
            return BadRequest();

        return _studioMembershipService.GetFromStudioId(studioId, cancellationToken) is { } memberships
            ? Ok(memberships)
            : NotFound();
    }

    [HttpGet("{studioId}")]
    public async Task<IActionResult> GetFromUserInStudio(Guid studioId, CancellationToken cancellationToken)
    {
        if (studioId == Guid.Empty)
            return BadRequest();

        var userId = User.GetUserId().Value;

        if (await _studioMembershipService.GetByUserAndStudioAsync(userId, studioId, cancellationToken) is { } membership)
            return Ok(membership);
        else
            return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] StudioMembershipModel value, CancellationToken cancellationToken)
    {
        if (value != null)
            return BadRequest("Value cannot be null or empty.");

        var newMembership = await _studioMembershipService.CreateByMapping(value, cancellationToken);

        if  (newMembership is { })
            return Ok(newMembership);
        else
            return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] StudioMembershipModel value, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty || value is not { })
            return BadRequest("Id cannot be empty.");

        var updatedMembership = await _studioMembershipService.Update(id, value);
        if (updatedMembership is { })
            return Ok(updatedMembership);
        else
            return NotFound();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty || userId == Guid.Empty)
            return BadRequest("Id cannot be empty.");

        await _studioMembershipService.Delete(id);
        return Ok();
    }
}
