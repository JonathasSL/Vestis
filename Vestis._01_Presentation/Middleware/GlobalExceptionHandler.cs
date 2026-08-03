using Microsoft.AspNetCore.Diagnostics;
using Vestis.Shared.Extensions;

namespace Vestis._01_Presentation.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
	private readonly ILogger<GlobalExceptionHandler> _logger;

	public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
	{
		_logger = logger;
	}

	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		_logger.LogError(exception.ExceptionStack(out _));

		httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

		await httpContext.Response.WriteAsJsonAsync(new
		{
			Message = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde."
		}, cancellationToken);
		
		return true;
	}
}
