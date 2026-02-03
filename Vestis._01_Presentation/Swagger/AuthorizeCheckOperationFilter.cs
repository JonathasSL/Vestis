using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Vestis._01_Presentation.Swagger;

public sealed class AuthorizeCheckOperationFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		var hasAllowAnonymous =
			context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any()
			|| (context.MethodInfo.DeclaringType?.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any() ?? false);

		if (hasAllowAnonymous)
		{
			operation.Security?.Clear();
			return;
		}

		var hasAuthorize =
			context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
			|| (context.MethodInfo.DeclaringType?.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() ?? false);

		if (!hasAuthorize)
			return;

		operation.Security ??= new List<OpenApiSecurityRequirement>();

		var scheme = new OpenApiSecurityScheme
		{
			Reference = new OpenApiReference
			{
				Type = ReferenceType.SecurityScheme,
				Id = "Bearer"
			}
		};

		operation.Security.Add(new OpenApiSecurityRequirement
		{
			[scheme] = Array.Empty<string>()
		});
	}
}
