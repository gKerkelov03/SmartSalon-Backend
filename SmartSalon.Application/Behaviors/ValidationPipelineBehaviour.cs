using FluentValidation;
using MediatR;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;
using SmartSalon.Shared.Extensions;

namespace SmartSalon.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    //TODO debug why changing ResultBase to Result makes the PipelineBehaviour unreachable
    where TResponse : IResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var validationErrors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .Select(failure => Error.Validation(failure.PropertyName, failure.ErrorMessage));

        if (validationErrors.Any())
        {
            return CreateFailedResult<TResponse>(validationErrors.First());
        }

        return await next();
    }

    private static TResult CreateFailedResult<TResult>(Error error)
        where TResult : IResult
    {
        var tResult = typeof(TResult);
        var nonGenericResult = typeof(Result);
        var genericResult = typeof(Result<>);
        var enumerableOfError = typeof(IEnumerable<Error>);

        var failedResultFactoryMethod = nameof(Result.Failure);

        if (tResult == nonGenericResult)
        {
            return Result.Failure(error).CastTo<TResult>();
        }
        else if (tResult == genericResult)
        {
            var genericArgument = tResult.GetGenericArguments()[0];
            var genericResultWithTypeParameter = genericResult.MakeGenericType(genericArgument);
            var method = genericResultWithTypeParameter.GetMethod(failedResultFactoryMethod, [enumerableOfError]);

            return method!.Invoke(null, [error])!.CastTo<TResult>();
        }

        throw new ArgumentException("TResult must be either Result, Result<TValue> or a type that inherits from them. If not you should configure the ValidationPipelineBehaviour's CreateFailedResult method to be able to construct the desired type of yours");
    }
}