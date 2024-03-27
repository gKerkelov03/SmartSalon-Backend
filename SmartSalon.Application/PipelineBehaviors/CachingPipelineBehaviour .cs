using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.ResultObject;
using SmartSalon.Shared.Extensions;

namespace SmartSalon.Application.PipelineBehaviors;

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

            await AddResultValueToTheCacheAsync(result, query.CachingKey, query.Expiration);

            return result;
        }

        return ParseValueToResult(cachedValue).CastTo<TResult>();
    }

    private async Task AddResultValueToTheCacheAsync(IResult result, string key, TimeSpan expiration)
    {
        var popertyContainingTheValue = "Value";

        var value = result
            .GetType()
            .GetProperty(popertyContainingTheValue)
            !.GetValue(result);

        var json = JsonConvert.SerializeObject(value);

        await _cache.SetStringAsync(key, json, new() { AbsoluteExpirationRelativeToNow = expiration }, default);
    }

    private IResult ParseValueToResult(string value)
    {
        var resultGenericArgument = typeof(TResult).GetGenericArguments()[0];
        var deserialized = JsonConvert.DeserializeObject(value, resultGenericArgument);

        return typeof(Result<>)
            .MakeGenericType(resultGenericArgument)
            .GetMethod(nameof(Result.Success))
            !.Invoke(null, [deserialized])
            !.CastTo<TResult>();
    }
}