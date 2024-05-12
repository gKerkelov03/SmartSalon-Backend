using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Queries;

namespace SmartSalon.Presentation.Web.Features.Services.Responses;

public class CategoryResponse : IMapFrom<CategoryQueryResponse>
{
    public required string Name { get; set; }
    public required int Order { get; set; }
    public required IEnumerable<ServiceQueryResponse> Services { get; set; }
}