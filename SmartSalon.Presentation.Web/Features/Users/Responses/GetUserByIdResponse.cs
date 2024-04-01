
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Queries;

namespace SmartSalon.Presentation.Web.Features.Users.Responses;

public class GetUserByIdResponse : IMapFrom<GetUserByIdQueryResponse>
{
    public required string Name { get; set; }

    public required int Age { get; set; }
}

