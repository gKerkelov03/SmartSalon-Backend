using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Queries.Responses;

namespace SmartSalon.Application.Queries;

public class GetAllQuery : ICachedQuery<IEnumerable<GetByIdQueryResponse>>
{
    public string CachingKey => "caching-test";
}