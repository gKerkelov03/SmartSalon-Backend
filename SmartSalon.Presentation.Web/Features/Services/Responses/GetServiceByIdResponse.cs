
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Queries;

namespace SmartSalon.Presentation.Web.Features.Services.Responses;

public class GetServiceByIdResponse : IMapFrom<GetServiceByIdQueryResponse>
{
    public Id Id { get; set; }
    public Id SalonId { get; set; }
    public Id CategoryId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public required int DurationInMinutes { get; set; }
    public required int Order { get; set; }
}