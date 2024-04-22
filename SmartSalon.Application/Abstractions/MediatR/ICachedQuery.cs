namespace SmartSalon.Application.Abstractions.MediatR;

public interface ICachedQuery
{
    string CachingKey { get; }

    TimeSpan Expiration => TimeSpan.FromDays(7);
}

public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;