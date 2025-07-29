using System.Security.Claims;

namespace Vestis.Shared.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal claims)
    {
        var idClaim = claims.FindFirst(ClaimTypes.NameIdentifier);

        if (idClaim is null)
            return null;

        return Guid.Parse(idClaim.Value);
    }
}
