using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Queries;

namespace SmartSalon.Presentation.Web.Features.Salons.Responses;

public class GetJobTitleByIdResponse : IMapFrom<GetJobTitleByIdQueryResponse>
{
    public Id Id { get; set; }
    public required string Name { get; set; }
}