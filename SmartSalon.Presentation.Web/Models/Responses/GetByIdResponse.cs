
using SmartSalon.Application.Mapping;
using SmartSalon.Application.Queries.Responses;

namespace SmartSalon.Presentation.Web.Models.Responses;

public class GetByIdResponse : IMapFrom<GetByIdQueryResponse>
{
    public required string Name { get; set; }

    public required int Age { get; set; }
}

