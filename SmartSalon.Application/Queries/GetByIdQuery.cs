using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Queries.Responses;

namespace SmartSalon.Application.Queries;

public class GetByIdQuery : IQuery<GetByIdQueryResponse>
{
    public GetByIdQuery(Id id)
        => Id = id;

    public Id Id { get; set; }
}