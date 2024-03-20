using FluentResults;
using FluentValidation;
using MediatR;
using SmartSalon.Application.Abstractions;
using SmartSalon.Shared.Extensions;

namespace SmartSalon.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>, ITransientLifetime
    where TRequest : IRequest<TResponse>
    //TODO debug why changing ResultBase to Result makes the PipelineBehaviour unreachable
    where TResponse : ResultBase, new()
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
        where TResult : ResultBase, new()
    {
        var tResult = typeof(TResult);
        var nonGenericResult = typeof(Result);
        var genericResult = typeof(Result<>);
        var enumerableOfError = typeof(IEnumerable<IError>);

        var failedResultFactoryMethod = "WithErrors";

        if (tResult == nonGenericResult)
        {
            return new Result().WithErrors(errors).CastTo<TResult>();
        }
        else if (tResult.IsGenericType && tResult.GetGenericTypeDefinition() == genericResult)
        {
            var genericArgumentType = tResult.GetGenericArguments()[0];
            var constructedResultType = genericResult.MakeGenericType(genericArgumentType);
            var method = constructedResultType.GetMethod(failedResultFactoryMethod, [enumerableOfError]);
            var resultInstance = Activator.CreateInstance(constructedResultType);

            //TODO: ask if you should remove the ! from below
            return method!.Invoke(resultInstance, [errors.ToArray()])!.CastTo<TResult>();
        }

        throw new ArgumentException("TResult must be either Result or Result<T>.");
    }
}