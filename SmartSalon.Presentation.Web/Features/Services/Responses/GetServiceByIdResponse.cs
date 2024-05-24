using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Queries;
using SmartSalon.Presentation.Web.Features.Salons.Responses;

namespace SmartSalon.Presentation.Web.Features.Services.Responses;

public class GetServiceByIdResponse : IMapFrom<GetServiceByIdQueryResponse>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public required int DurationInMinutes { get; set; }
    public required int Order { get; set; }
    public required ICollection<GetJobTitleByIdResponse>? JobTitles { get; set; }
}