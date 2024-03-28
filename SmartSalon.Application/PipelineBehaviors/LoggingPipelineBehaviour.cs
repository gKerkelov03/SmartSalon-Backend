using MediatR;
using Microsoft.Extensions.Logging;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.PipelineBehaviors;

public class LoggingPipelineBehaviour<TRequest, TResponse>(ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> _logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("Processing request {RequestName}", requestName);

        var result = await next();

        if (result.IsSuccess)
        {
            _logger.LogInformation("Completed handler for {RequestName}", requestName);
        }
        else
        {
            var errorDescriptions = result.Errors!.Select(error => error.Description);

            _logger.LogError("Completed handler for {RequestName} with errors: {Errors}", requestName, errorDescriptions);
        }

        return result;
    }
}