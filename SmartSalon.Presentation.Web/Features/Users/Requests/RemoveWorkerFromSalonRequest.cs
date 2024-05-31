using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Users.Requests;

public class RemoveWorkerFromSalonRequest : IMapTo<RemoveWorkerFromSalonCommand>
{
    [ComesFromRoute(IdRoute)]
    public required Id WorkerId { get; set; }
    public required Id SalonId { get; set; }
}