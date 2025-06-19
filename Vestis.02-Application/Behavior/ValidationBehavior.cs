using FluentValidation;
using MediatR;
using Vestis._02_Application.Common;

namespace Vestis._02_Application.Behavior;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly BusinessNotificationContext _businessNotificationContext;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, BusinessNotificationContext notificationContext)
    {
        _validators = validators;
        _businessNotificationContext = notificationContext;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if(!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken))
            );

        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .ToList();

        if (failures.Count > 0)
        {
            _businessNotificationContext.AddRange(failures.Select(failure => failure.ErrorMessage));
            return default!;
        }

        return await next();
    }
}
