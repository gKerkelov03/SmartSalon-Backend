using SmartSalon.Application.Abstractions;

namespace SmartSalon.Application.Queries;

public class GetByIdQuery : IQuery<GetByIdQueryResponse>
{
    public GetByIdQuery(Id id)
        => Id = id;

    public Id Id { get; set; }
}