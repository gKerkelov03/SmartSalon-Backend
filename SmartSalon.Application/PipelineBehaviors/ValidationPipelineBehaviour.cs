using FluentValidation;
using MediatR;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.PipelineBehaviors;

internal class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> _validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        var validationErrors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .Select(failure => Error.Validation(failure.PropertyName, failure.ErrorMessage));

        if (validationErrors.Any())
        {
            return CreateFailedResult<TResponse>(validationErrors);
        }

        return await next();
    }

    private static TResult CreateFailedResult<TResult>(IEnumerable<Error> errors)
        where TResult : IResult
    {
        var tResult = typeof(TResult);
        var factoryMethod = nameof(Result.Failure);
        var factoryMethodParameter = new[] { typeof(IEnumerable<Error>) };

        if (tResult == typeof(Result))
        {
            return Result.Failure(errors).CastTo<TResult>();
        }

        var genericResult = typeof(Result<>).MakeGenericType(tResult.GetGenericArguments()[0]);

        if (tResult == genericResult)
        {
            return genericResult
                .GetMethod(factoryMethod, factoryMethodParameter)
                !.Invoke(null, [errors])
                !.CastTo<TResult>();
        }

        throw new ArgumentException("TResult must be either Result, Result<TValue> or a type that inherits from them. If not, you should configure the ValidationPipelineBehaviour's CreateFailedResult method to be able to construct the desired type of yours");
    }
}