using FluentResults;
using FluentValidation;
using MediatR;
using SmartSalon.Application.Abstractions;
using SmartSalon.Shared.Extensions;

namespace SmartSalon.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>, ITransientLifetime
    where TRequest : IRequest<TResponse>
    //TODO debug why changing ResultBase to Result makes the PipelineBehaviour unreachable
    where TResponse : ResultBase
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .Select(failure => new Error(failure.ErrorMessage))
            .Distinct()
            .ToList();

        if (errors.Any())
        {
            return CreateFailedResult<TResponse>(errors);
        }

        return await next();
    }

    private static TResult CreateFailedResult<TResult>(IEnumerable<IError> errors)
        where TResult : ResultBase
    {
        var tResult = typeof(TResult);
        var nonGenericResult = typeof(Result);
        var genericResult = typeof(Result<>);

        if (tResult == nonGenericResult)
        {
            return new Result().WithErrors(errors).CastTo<TResult>();
        }

        return genericResult
            .GetGenericTypeDefinition()
            .MakeGenericType(tResult.GenericTypeArguments[0])
            .GetMethod("WithErrors")!
            .Invoke(null, errors.ToArray())!
            .CastTo<TResult>();
    }
}