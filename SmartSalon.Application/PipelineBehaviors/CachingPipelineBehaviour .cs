using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.PipelineBehaviors;

internal class CachingPipelineBehaviour<TQuery, TResult>(IDistributedCache _cache) : IPipelineBehavior<TQuery, TResult>
    where TQuery : ICachedQuery
    where TResult : IResult
{
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