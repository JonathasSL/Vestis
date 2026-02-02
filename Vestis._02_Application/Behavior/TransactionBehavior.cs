using MediatR;
using Microsoft.Extensions.Logging;
using Vestis._04_Infrastructure.Data;

namespace Vestis._02_Application.Behavior;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : notnull
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

    public TransactionBehavior(ApplicationDbContext dbContext, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_dbContext.Database.CurrentTransaction != null)
            return await next();

        TResponse response;
        try
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            _logger.LogInformation("Transaction started for request: {Command}", typeof(TRequest).Name);

            response = await next();

            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Transaction committed for request: {Command}", typeof(TRequest).Name);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Transaction failed for request: {Command}", typeof(TRequest).Name);
            throw;
        }

        return response;
    }
}
