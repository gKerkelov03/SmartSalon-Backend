using SmartSalon.Application.Abstractions;

namespace SmartSalon.Application.Queries;

public class GetAllQuery : ICachedQuery<IEnumerable<GetByIdQueryResponse>>
{
    public string CachingKey => "caching-test";
}