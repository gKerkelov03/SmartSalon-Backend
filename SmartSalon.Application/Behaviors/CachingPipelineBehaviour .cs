using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.ResultObject;
using SmartSalon.Shared.Extensions;

namespace SmartSalon.Application.Behaviors;

public class CachingPipelineBehaviour<TQuery, TResult> : IPipelineBehavior<TQuery, TResult>
    where TQuery : ICachedQuery
    where TResult : IResult
{
    private readonly IDistributedCache _cache;

    public CachingPipelineBehaviour(IDistributedCache cache)
        => _cache = cache;

    public async Task<TResult> Handle(
        TQuery query,
        RequestHandlerDelegate<TResult> next,
        CancellationToken cancellationToken
    )
    {
        var cachedValue = await _cache.GetStringAsync(query.CachingKey);

        if (cachedValue is null)
        {
            var result = await next();
            var popertyContainingTheValue = "Value";

            var value = result
                .GetType()
                .GetProperty(popertyContainingTheValue)
                !.GetValue(result);

            var json = JsonConvert.SerializeObject(value);

            _cache.SetString(
                query.CachingKey,
                json,
                new() { AbsoluteExpirationRelativeToNow = query.Expiration }
            );

            return result;
        }

        var resultGenericArgument = typeof(TResult).GetGenericArguments()[0];
        var deserialized = JsonConvert.DeserializeObject(cachedValue, resultGenericArgument);

        return typeof(Result<>)
            .MakeGenericType(resultGenericArgument)
            .GetMethod(nameof(Result.Success))
            !.Invoke(null, [deserialized])
            !.CastTo<TResult>();
    }
}