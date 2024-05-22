using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class CreateServiceRequest : IMapTo<CreateServiceCommand>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public required int DurationInMinutes { get; set; }
    public Id SalonId { get; set; }
    public Id CategoryId { get; set; }
}

