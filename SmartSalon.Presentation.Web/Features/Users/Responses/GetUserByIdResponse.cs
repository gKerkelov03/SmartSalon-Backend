
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Queries.Handlers;

namespace SmartSalon.Presentation.Web.Features.Users.Responses;

public class GetUserByIdResponse : IMapFrom<GetUserByIdQueryResponse>
{
    public required string Name { get; set; }

    public required int Age { get; set; }
}

