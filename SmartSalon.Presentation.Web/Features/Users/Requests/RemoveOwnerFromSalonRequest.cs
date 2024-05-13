using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class RemoveOwnerFromSalonRequest : IMapTo<RemoveOwnerFromSalonCommand>
{
    [FromRoute(Name = IdRouteParameterName)]
    public Id OwnerId { get; set; }
    public Id SalonId { get; set; }
}