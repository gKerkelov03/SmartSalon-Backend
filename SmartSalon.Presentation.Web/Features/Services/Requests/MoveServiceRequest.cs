using SmartSalon.Application.Abstractions.Mapping;
using SmartService.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class MoveServiceRequest : IMapTo<MoveServiceCommand>
{
    public required Id ServiceId { get; set; }
    public Id CategoryId { get; set; }
    public Id SalonId { get; set; }
    public required int Order { get; set; }
}