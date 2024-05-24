using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Queries;

namespace SmartSalon.Presentation.Web.Features.Services.Responses;

public class GetSectionByIdResponse : IMapFrom<GetSectionByIdQueryResponse>
{
    public Id Id { get; set; }
    public Id SalonId { get; set; }
    public required string Name { get; set; }
    public required int Order { get; set; }
    public required string PictureUrl { get; set; }
    public required IEnumerable<GetCategoryByIdQueryResponse> Categories { get; set; }
}