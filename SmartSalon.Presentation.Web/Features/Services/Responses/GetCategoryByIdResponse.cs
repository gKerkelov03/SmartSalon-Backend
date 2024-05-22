using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Queries;

namespace SmartSalon.Presentation.Web.Features.Services.Responses;

public class GetCategoryByIdResponse : IMapFrom<GetCategoryByIdQueryResponse>
{
    public Id Id { get; set; }
    public Id SalonId { get; set; }
    public Id SectionId { get; set; }
    public required string Name { get; set; }
    public required int Order { get; set; }
}