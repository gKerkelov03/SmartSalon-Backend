using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class DeleteServiceRequest : IMapTo<DeleteServiceCommand>
{
    public Id ServiceId { get; set; }
    public Id SalonId { get; set; }
}