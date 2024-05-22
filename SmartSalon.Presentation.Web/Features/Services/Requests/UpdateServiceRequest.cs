using SmartSalon.Application.Abstractions.Mapping;
using SmartService.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class UpdateServiceRequest : IMapTo<UpdateServiceCommand>
{
    public Id ServiceId { get; set; }
    public Id SalonId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public required int DurationInMinutes { get; set; }
}